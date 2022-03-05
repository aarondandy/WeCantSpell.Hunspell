using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

internal class LineReader
{
    private static readonly Encoding[] PreambleEncodings;
    private static readonly int MaxPreambleLengthInBytes = 4;

    static LineReader()
    {
        // NOTE: the order of these encodings must be preserved
        PreambleEncodings =
            new Encoding[]
            {
                new UnicodeEncoding(true, true)
                ,new UnicodeEncoding(false, true)
                ,new UTF32Encoding(false, true)
                ,Encoding.UTF8
                ,new UTF32Encoding(true, true)
            };

        MaxPreambleLengthInBytes = 4;
        foreach (var e in PreambleEncodings)
        {
            var length = e.GetPreamble().Length;
            if (length > MaxPreambleLengthInBytes)
            {
                MaxPreambleLengthInBytes = length;
            }
        }
    }

    public static LineReader Create(Stream stream, Encoding encoding) => new LineReader(stream, encoding);

    private LineReader(Stream stream, Encoding encoding)
    {
        _stream = stream;
        _encoding = encoding;
        _decoder = encoding.GetDecoder();
        _fileReadByteBuffer = new byte[4096]; // TODO: would it be better to rent from the memory pool for this?
    }

    private readonly Stream _stream;
    private readonly byte[] _fileReadByteBuffer;

    private Encoding _encoding;
    private Decoder _decoder;
    private bool _hasReadPreambles = false;
    private DecodedTextSegment? _head = null;
    private DecodedTextSegment? _tail = null;
    private ReadOnlySequence<char> _currentSequence = new ReadOnlySequence<char>(ReadOnlyMemory<char>.Empty);
    private IMemoryOwner<char>? _splitBufferMemoryRental = null;

    public ReadOnlyMemory<char> Current { get; private set; } = ReadOnlyMemory<char>.Empty;

    public bool MoveNext()
    {
        var lineBreakPosition = PrepareForNextLine();
        return SetCurrentForNextLine(lineBreakPosition);
    }

    public async Task<bool> MoveNextAsync(CancellationToken ct)
    {
        var lineBreakPosition = await PrepareForNextLineAsync(ct);
        return SetCurrentForNextLine(lineBreakPosition);
    }

    public void Dispose()
    {
        _splitBufferMemoryRental?.Dispose();
        _splitBufferMemoryRental = null;
        while (_head is not null)
        {
            _head.Dispose();
            _head = (DecodedTextSegment?)_head.Next;
        }
    }

    private bool SetCurrentForNextLine(SequencePosition? lineBreakPosition)
    {
        _splitBufferMemoryRental?.Dispose();
        ReadOnlySequence<char> lineSequence;
        if (lineBreakPosition.HasValue)
        {
            lineSequence = _currentSequence.Slice(0, lineBreakPosition.Value);
        }
        else
        {
            if (_currentSequence.IsEmpty)
            {
                return false;
            }

            lineSequence = _currentSequence;
        }

        if (lineSequence.IsSingleSegment)
        {
            Current = lineSequence.First;
        }
        else
        {
            _splitBufferMemoryRental = MemoryPool<char>.Shared.Rent((int)lineSequence.Length);
            var _splitBufferMemory = _splitBufferMemoryRental.Memory.Slice(0, (int)lineSequence.Length);
            lineSequence.CopyTo(_splitBufferMemoryRental.Memory.Span);
            Current = _splitBufferMemory;
        }

        if (Current.EndsWith('\r'))
        {
            Current = Current.Slice(0, Current.Length - 1);
        }

        _currentSequence = lineSequence.Length < _currentSequence.Length
            ? _currentSequence.Slice(lineSequence.Length + 1)
            : new ReadOnlySequence<char>(ReadOnlyMemory<char>.Empty);

        var nextHead = _currentSequence.Start.GetObject() as DecodedTextSegment;

        while (_head is not null && _head != nextHead)
        {
            _head.Dispose();
            _head = (DecodedTextSegment?)_head.Next;
        }

        _head = _head?.CloneAt(_currentSequence.Start.GetInteger());

        return true;
    }

    private SequencePosition? PrepareForNextLine()
    {
        var lineBreakPosition = _currentSequence.PositionOf('\n');
        if (lineBreakPosition.HasValue)
        {
            return lineBreakPosition;
        }

        do
        {
            if (! LoadMoreBytesIntoTextSequences())
            {
                break;
            }

            lineBreakPosition = _currentSequence.PositionOf('\n');
            if (lineBreakPosition.HasValue)
            {
                break;
            }
        }
        while (true);

        return lineBreakPosition;
    }

#if NO_VALUETASK
    private Task<SequencePosition?> PrepareForNextLineAsync(CancellationToken ct)
    {
        var quickPosition = _currentSequence.PositionOf('\n');
        return quickPosition.HasValue
            ? Task.FromResult(quickPosition)
            : PrepareForNextLineWithSearchAsync(ct);
    }
#else
    private ValueTask<SequencePosition?> PrepareForNextLineAsync(CancellationToken ct)
    {
        var quickPosition = _currentSequence.PositionOf('\n');
        return quickPosition.HasValue
            ? ValueTask.FromResult(quickPosition)
            : new ValueTask<SequencePosition?>(PrepareForNextLineWithSearchAsync(ct));
    }
#endif

    private async Task<SequencePosition?> PrepareForNextLineWithSearchAsync(CancellationToken ct)
    {
        SequencePosition? lineBreakPosition = null;
        do
        {
            if (!await LoadMoreBytesIntoTextSequencesAsync(ct))
            {
                break;
            }

            lineBreakPosition = _currentSequence.PositionOf('\n');
            if (lineBreakPosition.HasValue)
            {
                break;
            }
        }
        while (true);

        return lineBreakPosition;
    }

    private bool LoadMoreBytesIntoTextSequences()
    {
        var fileBytesRead = _stream.Read(_fileReadByteBuffer, 0, _fileReadByteBuffer.Length);
        if (fileBytesRead == 0)
        {
            return false;
        }

        DecodeIntoSequences(_fileReadByteBuffer.AsSpan(0, fileBytesRead));

        return true;
    }

    private async Task<bool> LoadMoreBytesIntoTextSequencesAsync(CancellationToken ct)
    {
        var fileBytesRead = await _stream.ReadAsync(_fileReadByteBuffer, 0, _fileReadByteBuffer.Length, ct);
        if (fileBytesRead == 0)
        {
            return false;
        }

        DecodeIntoSequences(_fileReadByteBuffer, fileBytesRead);

        return true;
    }

    private void DecodeIntoSequences(byte[] fileReadByteBuffer, int fileBytesRead)
    {
        DecodeIntoSequences(fileReadByteBuffer.AsSpan(0, fileBytesRead));
    }

    private void DecodeIntoSequences(ReadOnlySpan<byte> fileBytes)
    {
        if (!_hasReadPreambles)
        {
            ReadPreamble(ref fileBytes);
        }

        while (!fileBytes.IsEmpty)
        {
            var textBufferRental = MemoryPool<char>.Shared.Rent(fileBytes.Length * 2);
            var textBuffer = textBufferRental.Memory;

            _decoder.Convert(fileBytes, textBuffer.Span, flush: false, out var bytesConsumed, out var charsProduced, out _);

#if DEBUG
            if (bytesConsumed == 0)
            {
                throw new InvalidOperationException();
            }
#endif

            fileBytes = fileBytes.Slice(bytesConsumed);

            if (charsProduced == 0)
            {
                textBufferRental.Dispose();
                continue;
            }

            textBuffer = textBuffer.Slice(0, charsProduced);

            if (_head is null)
            {
                _head = new DecodedTextSegment(textBufferRental, textBuffer);
                _tail = _head;
            }
            else
            {
                _tail = _tail!.Append(textBufferRental, textBuffer);
            }
        }

        if (_head is null)
        {
            _currentSequence = new ReadOnlySequence<char>(ReadOnlyMemory<char>.Empty);
        }
        else
        {
            _currentSequence = new ReadOnlySequence<char>(_head, 0, _tail!, _tail!.Memory.Length);
        }
    }

    private void ReadPreamble(ref ReadOnlySpan<byte> fileBytes)
    {
        if (fileBytes.IsEmpty)
        {
            return;
        }

        _hasReadPreambles = true;

        foreach (var candidateEncoding in PreambleEncodings)
        {
            var encodingPreamble = candidateEncoding.GetPreamble();
            if (encodingPreamble is not { Length: > 0 })
            {
                continue;
            }

            if (fileBytes.StartsWith(encodingPreamble.AsSpan()))
            {
                fileBytes = fileBytes.Slice(encodingPreamble.Length);
                ChangeEncoding(candidateEncoding);
                return;
            }
        }
    }

    private void ChangeEncoding(Encoding? newEncoding)
    {
        if (newEncoding is null)
        {
            return;
        }

        if (_encoding is not null && (ReferenceEquals(newEncoding, _encoding) || _encoding.Equals(newEncoding)))
        {
            return;
        }

        _encoding = newEncoding;
        _decoder = newEncoding.GetDecoder();
    }

    private sealed class DecodedTextSegment : ReadOnlySequenceSegment<char>, IDisposable
    {
        public DecodedTextSegment(IMemoryOwner<char> rentedBuffer, ReadOnlyMemory<char> memory)
        {
            _rentedBuffer = rentedBuffer;
            Memory = memory;
        }

        IMemoryOwner<char> _rentedBuffer;

        public DecodedTextSegment Append(IMemoryOwner<char> rentedBuffer, ReadOnlyMemory<char> memory)
        {
            var next = new DecodedTextSegment(rentedBuffer, memory)
            {
                RunningIndex = RunningIndex + Memory.Length
            };
            Next = next;
            return next;
        }

        public DecodedTextSegment CloneAt(int position)
        {
            return new DecodedTextSegment(_rentedBuffer, Memory.Slice(position))
            {
                RunningIndex = RunningIndex + position
            };
        }

        public void Dispose()
        {
            _rentedBuffer.Dispose();
        }
    }
}
