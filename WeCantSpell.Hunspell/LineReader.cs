using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

internal sealed class LineReader : IDisposable
{
    private const char CharacterCr = '\r';
    private const char CharacterLf = '\n';
    private const int DefaultBufferSize = 4096 * 2;
    private static readonly Encoding[] PreambleEncodings;

    static LineReader()
    {
        // NOTE: the order of these encodings must be preserved
        PreambleEncodings =
            new Encoding[]
            {
                new UnicodeEncoding(true, true),
                new UnicodeEncoding(false, true),
                new UTF32Encoding(false, true),
                Encoding.UTF8,
                new UTF32Encoding(true, true)
            };
    }

    internal LineReader(Stream stream, Encoding encoding, bool allowEncodingChanges = false, bool ownsStream = false)
    {
        _stream = stream;
        _encoding = encoding;
        _decoder = encoding.GetDecoder();
        _allowEncodingChanges = allowEncodingChanges;
        _reusableFileReadBuffer = new byte[DefaultBufferSize];
        _buffers = new(2);
        _ownsStream = ownsStream;
    }

    private readonly Stream _stream;
    private readonly byte[] _reusableFileReadBuffer;
    private readonly List<TextBufferLine> _buffers;
    private readonly bool _ownsStream;
    private bool _allowEncodingChanges;
    private Encoding _encoding;
    private Decoder _decoder;
    private bool _hasReadPreamble = false;
    private BufferPosition _position = default;
    private IMemoryOwner<char>? _tempJoinBuffer = null;

    public ReadOnlyMemory<char> Current { get; private set; } = ReadOnlyMemory<char>.Empty;

    public bool ReadNext()
    {
        var lineTerminalPosition = ScanForwardToNextLineBreak();

        while (!lineTerminalPosition.HasValue)
        {
            var newBufferLines = ReadNewTextBufferLines();
            if (newBufferLines == 0)
            {
                break;
            }

            lineTerminalPosition = ScanForwardToNextLineBreak(_buffers.Count - newBufferLines, 0);
        }

        return UpdateAfterRead(lineTerminalPosition ?? GetTerminalBufferPosition());
    }

#if NO_VALUETASK
    public Task<bool> ReadNextAsync(CancellationToken ct)
    {
        if (ScanForwardToNextLineBreak() is { } lineTerminalPosition)
        {
            return Task.FromResult(UpdateAfterRead(lineTerminalPosition));
        }
        else
        {
            return ReadNextAsyncWithReads(ct);
        }
    }
#else
    public ValueTask<bool> ReadNextAsync(CancellationToken ct)
    {
        if (ScanForwardToNextLineBreak() is { } lineTerminalPosition)
        {
            return ValueTask.FromResult(UpdateAfterRead(lineTerminalPosition));
        }
        else
        {
            return ReadNextAsyncWithReads(ct);
        }
    }
#endif

#if NO_VALUETASK
    private async Task<bool> ReadNextAsyncWithReads(CancellationToken ct)
#else
    private async ValueTask<bool> ReadNextAsyncWithReads(CancellationToken ct)
#endif
    {
        BufferPosition? lineTerminalPosition = null;
        do
        {
            var newBufferLines = await ReadNewTextBufferLinesAsync(ct);
            if (newBufferLines == 0)
            {
                break;
            }

            lineTerminalPosition = ScanForwardToNextLineBreak(_buffers.Count - newBufferLines, 0);
        }
        while (!lineTerminalPosition.HasValue);

        return UpdateAfterRead(lineTerminalPosition ?? GetTerminalBufferPosition());
    }

    private bool UpdateAfterRead(BufferPosition lineTerminalPosition)
    {
        if (_position.BufferIndex >= _buffers.Count)
        {
            return false;
        }

        if (_tempJoinBuffer is not null)
        {
            _tempJoinBuffer.Dispose();
            _tempJoinBuffer = null;
        }

        TextBufferLine buffer;
        if (_position.BufferIndex == lineTerminalPosition.BufferIndex)
        {
            buffer = _buffers[_position.BufferIndex];
            Current = buffer.Memory.Slice(_position.SubIndex, lineTerminalPosition.SubIndex - _position.SubIndex);
        }
        else
        {
            buffer = UpdateAfterReadWithJoinInternal(lineTerminalPosition);
        }

        if (Current.EndsWith(CharacterCr))
        {
            Current = Current.Slice(0, Current.Length - 1);
        }

        _position = lineTerminalPosition;
        _position.SubIndex++;
        if (_position.SubIndex >= buffer.Length)
        {
            _position.BufferIndex++;
            _position.SubIndex = 0;
        }

        if (_allowEncodingChanges)
        {
            tryChangeEncoding();
            void tryChangeEncoding()
            {
                if (readSetEncoding(Current.Span) is { IsEmpty: false } encodingSpan)
                {
                    ChangeEncoding(encodingSpan);
                    _allowEncodingChanges = false; // Only expect one encoding switch per file
                }
            }

            static ReadOnlySpan<char> readSetEncoding(ReadOnlySpan<char> span)
            {
                var startIndex = 0;
                for (; startIndex < span.Length && span[startIndex].IsTabOrSpace(); startIndex++) ;

                if (startIndex > 0)
                {
                    span = span.Slice(startIndex);
                }

                if (span.Length >= 5
                    && span[0] is 'S' or 's'
                    && span[1] is 'E' or 'e'
                    && span[2] is 'T' or 't'
                    && span[3].IsTabOrSpace())
                {
                    for (startIndex = 4; startIndex < span.Length && span[startIndex].IsTabOrSpace(); startIndex++) ;

                    var endIndex = span.Length - 1;
                    for (; endIndex > startIndex && span[endIndex].IsTabOrSpace(); endIndex--) ;

                    return span.Slice(startIndex, endIndex - startIndex + 1);
                }

                return ReadOnlySpan<char>.Empty;
            }
        }

        return true;
    }

    private TextBufferLine UpdateAfterReadWithJoinInternal(BufferPosition lineTerminalPosition)
    {
        int i;
        var requiredBufferSize = _buffers[_position.BufferIndex].Length - _position.SubIndex + lineTerminalPosition.SubIndex;
        for (i = _position.BufferIndex + 1; i < lineTerminalPosition.BufferIndex; i++)
        {
            requiredBufferSize += _buffers[i].Length;
        }

        _tempJoinBuffer = MemoryPool<char>.Shared.Rent(requiredBufferSize);

        var joinBufferMemory = _tempJoinBuffer.Memory.Slice(0, requiredBufferSize);
        var joinWriteSpan = joinBufferMemory.Span;

        var buffer = _buffers[_position.BufferIndex];
        buffer.ReadSpan.Slice(_position.SubIndex).CopyTo(joinWriteSpan);
        joinWriteSpan = joinWriteSpan.Slice(buffer.Length - _position.SubIndex);

        for (i = _position.BufferIndex + 1; i < lineTerminalPosition.BufferIndex; i++)
        {
            buffer = _buffers[i];
            buffer.ReadSpan.CopyTo(joinWriteSpan);
            joinWriteSpan = joinWriteSpan.Slice(buffer.Length);
        }

        buffer = _buffers[lineTerminalPosition.BufferIndex];
        buffer.ReadSpan.Slice(0, lineTerminalPosition.SubIndex).CopyTo(joinWriteSpan);

        Current = joinBufferMemory;

        return buffer;
    }

    private BufferPosition GetTerminalBufferPosition()
    {
        var lastBufferIndex = _buffers.Count - 1;
        return new(lastBufferIndex, lastBufferIndex >= 0 ? _buffers[lastBufferIndex].Memory.Length : 0);
    }

    private BufferPosition? ScanForwardToNextLineBreak() => ScanForwardToNextLineBreak(_position.BufferIndex, _position.SubIndex);

    private BufferPosition? ScanForwardToNextLineBreak(int bufferIndex, int subIndex)
    {
        for (; bufferIndex < _buffers.Count; bufferIndex++)
        {
            subIndex = _buffers[bufferIndex].FindLineBreak(subIndex);

            if (subIndex >= 0)
            {
                return new(bufferIndex, subIndex);
            }

            subIndex = 0;
        }

        return null;
    }

#if NO_STREAM_SYSMEM
    private int ReadNewTextBufferLines()
    {
        var fileBytesRead = _stream.Read(_reusableFileReadBuffer, 0, _reusableFileReadBuffer.Length);
        if (fileBytesRead == 0)
        {
            return 0;
        }

        return DecodeIntoBufferLines(_reusableFileReadBuffer.AsSpan(0, fileBytesRead));
    }
#else
    private int ReadNewTextBufferLines()
    {
        var fileReadBuffer = _reusableFileReadBuffer.AsSpan();
        var fileBytesRead = _stream.Read(fileReadBuffer);
        if (fileBytesRead == 0)
        {
            return 0;
        }

        return DecodeIntoBufferLines(fileReadBuffer.Slice(0, fileBytesRead));
    }
#endif

#if NO_VALUETASK || NO_STREAM_SYSMEM
    private async Task<int> ReadNewTextBufferLinesAsync(CancellationToken ct)
    {
        var fileBytesRead = await _stream.ReadAsync(_reusableFileReadBuffer, 0, _reusableFileReadBuffer.Length, ct);
        if (fileBytesRead == 0)
        {
            return 0;
        }

        return DecodeIntoBufferLines(_reusableFileReadBuffer.AsSpan(0, fileBytesRead));
    }
#else
    private async ValueTask<int> ReadNewTextBufferLinesAsync(CancellationToken ct)
    {
        var fileReadBuffer = _reusableFileReadBuffer.AsMemory();
        var fileBytesRead = await _stream.ReadAsync(fileReadBuffer, ct);
        if (fileBytesRead == 0)
        {
            return 0;
        }

        return DecodeIntoBufferLines(fileReadBuffer.Span.Slice(0, fileBytesRead));
    }
#endif

    private int DecodeIntoBufferLines(ReadOnlySpan<byte> fileReadByteBuffer)
    {
        var linesAdded = 0;

        if (!_hasReadPreamble)
        {
            ReadPreamble(ref fileReadByteBuffer);
        }

        while (!fileReadByteBuffer.IsEmpty)
        {
            var textBuffer = AllocateBufferForNewWrites();

            _decoder.Convert(
                fileReadByteBuffer,
                textBuffer.WriteSpan,
                flush: false,
                out var bytesConsumed,
                out var charsProduced,
                out _);

#if DEBUG
            if (bytesConsumed == 0) throw new InvalidOperationException();
#endif

            fileReadByteBuffer = fileReadByteBuffer.Slice(bytesConsumed);

            if (charsProduced == 0)
            {
                continue;
            }

            textBuffer.PrepareMemoryForUse(charsProduced);

            _buffers.Add(textBuffer);
            linesAdded++;
        }

        return linesAdded;
    }

    private TextBufferLine AllocateBufferForNewWrites()
    {
        TextBufferLine buffer;

        while (_position.BufferIndex > 0 && _buffers.Count > 0)
        {
            buffer = _buffers[0];

            _buffers.RemoveAt(0);
            _position.BufferIndex--;

            if (!buffer.PreventRecycle)
            {
                buffer.ResetMemory();
                return buffer;
            }
        }

        return new(DefaultBufferSize);
    }

    private void ReadPreamble(ref ReadOnlySpan<byte> fileBytes)
    {
        if (!fileBytes.IsEmpty)
        {
            _hasReadPreamble = true;

            foreach (var candidateEncoding in PreambleEncodings)
            {
                if (
                    candidateEncoding.GetPreamble() is { Length: > 0 } encodingPreamble
                    && fileBytes.StartsWith(encodingPreamble.AsSpan())
                )
                {
                    ChangeEncoding(candidateEncoding);
                    fileBytes = fileBytes.Slice(encodingPreamble.Length);
                }
            }
        }
    }

    private void ChangeEncoding(ReadOnlySpan<char> encodingName)
    {
        if (EncodingEx.GetEncodingByName(encodingName) is { } newEncoding)
        {
            ChangeEncoding(newEncoding);
        }
    }

    private void ChangeEncoding(Encoding newEncoding)
    {
        if (_encoding is not null && (ReferenceEquals(newEncoding, _encoding) || _encoding.Equals(newEncoding)))
        {
            return;
        }

        var oldEncoding = _encoding ?? Encoding.UTF8;

        _encoding = newEncoding;
        _decoder = newEncoding.GetDecoder();

        if (_position.BufferIndex < _buffers.Count)
        {
            // The characters that were loaded after the encoding change need to be re-decoded
            for (var bufferIndex = _position.BufferIndex; bufferIndex < _buffers.Count; bufferIndex++)
            {
                var oldCharacters = _buffers[bufferIndex].Memory;

                if (_position.BufferIndex == bufferIndex)
                {
                    oldCharacters = oldCharacters.Slice(_position.SubIndex);
                }

#if NO_ENCODING_SPANS
                var restoredBytes = oldEncoding.GetBytes(oldCharacters.ToArray());
                var newCharacters = newEncoding.GetChars(restoredBytes);
#else
                var restoredBytesRaw = new byte[_reusableFileReadBuffer.Length];
                var restoredBytesCount = oldEncoding.GetBytes(oldCharacters.Span, restoredBytesRaw.AsSpan());
                var restoredBytes = restoredBytesRaw.AsSpan(0, restoredBytesCount);

                var newCharacters = new char[newEncoding.GetCharCount(restoredBytes)];
                var newCharactersCount = newEncoding.GetChars(restoredBytes, newCharacters.AsSpan());

#if DEBUG
                if (newCharactersCount != newCharacters.Length) throw new InvalidOperationException();
#endif
#endif
                _buffers[bufferIndex] = new TextBufferLine(newCharacters)
                {
                    PreventRecycle = true
                };

            }

            _position.SubIndex = 0;
        }
    }

    public void Dispose()
    {
        _tempJoinBuffer?.Dispose();

        if (_ownsStream)
        {
            _stream.Dispose();
        }
    }

    private struct TextBufferLine
    {
        public TextBufferLine(int rawBufferSize)
        {
            Raw = new char[rawBufferSize];
        }

        public TextBufferLine(char[] raw)
        {
            Raw = raw;
            Memory = raw.AsMemory();
        }

        public char[] Raw;
        public ReadOnlyMemory<char> Memory = ReadOnlyMemory<char>.Empty;
        public bool PreventRecycle = false;

        public int Length => Memory.Length;

        public Span<char> WriteSpan => Raw.AsSpan();

        public ReadOnlySpan<char> ReadSpan => Memory.Span;

        public int FindLineBreak() => Memory.Span.IndexOf(CharacterLf);

        public int FindLineBreak(int startIndex) => Memory.Span.IndexOf(CharacterLf, startIndex);

        public void PrepareMemoryForUse(int valueSize)
        {
            Memory = Raw.AsMemory(0, valueSize);
        }

        public void ResetMemory()
        {
            Memory = ReadOnlyMemory<char>.Empty;
        }
    }

    private struct BufferPosition
    {
        public BufferPosition(int bufferIndex, int subIndex)
        {
            BufferIndex = bufferIndex;
            SubIndex = subIndex;
        }

        public int BufferIndex;
        public int SubIndex;

        public void Deconstruct(out int bufferIndex, out int subIndex)
        {
            bufferIndex = BufferIndex;
            subIndex = SubIndex;
        }
    }
}
