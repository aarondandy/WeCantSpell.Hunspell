using System;
using System.Buffers;
using System.IO;
using System.Text;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

internal struct LineReader
{
    public static LineReader Create(Stream stream, Encoding encoding) => new LineReader(stream, encoding);

    private LineReader(Stream stream, Encoding encoding)
    {
        _stream = stream;
        _decoder = encoding.GetDecoder();
        _fileReadByteBuffer = new byte[4096]; // TODO: would it be better to rent from the memory pool for this?
    }

    private readonly Stream _stream;
    private readonly Decoder _decoder;
    private readonly byte[] _fileReadByteBuffer;

    private DecodedTextSegment? _head = null;
    private DecodedTextSegment? _tail = null;
    private ReadOnlySequence<char> _currentSequence = new ReadOnlySequence<char>(ReadOnlyMemory<char>.Empty);
    private IMemoryOwner<char>? _splitBufferMemoryRental = null;

    public ReadOnlyMemory<char> Current { get; private set; } = ReadOnlyMemory<char>.Empty;

    public bool MoveNext()
    {
        _splitBufferMemoryRental?.Dispose();

        SequencePosition? lineBreakPosition = null;
        do
        {
            if (_currentSequence.IsEmpty)
            {
                if (!LoadMoreBytesIntoTextSequences())
                {
                    break;
                }
            }

            lineBreakPosition = _currentSequence.PositionOf('\n');
            if (lineBreakPosition.HasValue)
            {
                break;
            }
            else if (!LoadMoreBytesIntoTextSequences())
            {
                break;
            }
        }
        while (true);

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

    private bool LoadMoreBytesIntoTextSequences()
    {
        var fileBytesRead = _stream.Read(_fileReadByteBuffer, 0, _fileReadByteBuffer.Length);
        if (fileBytesRead == 0)
        {
            return false;
        }

        var fileBytes = _fileReadByteBuffer.AsSpan(0, fileBytesRead);

        while (!fileBytes.IsEmpty)
        {
            var textBufferRental = MemoryPool<char>.Shared.Rent(fileBytes.Length * 2);
            var textBuffer = textBufferRental.Memory;

            _decoder.Convert(fileBytes, textBuffer.Span, flush: false, out var bytesConsumed, out var charsProduced, out _);

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

            if (bytesConsumed == 0)
            {
                throw new InvalidOperationException();
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

        return true;
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
