using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public sealed class DynamicEncodingLineReader : IHunspellLineReader, IDisposable
{
    static DynamicEncodingLineReader()
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

#if DEBUG
        var max = 0;
        foreach (var e in PreambleEncodings)
        {
            max = Math.Max(max, e.GetPreamble().Length);
        }

        if (max != MaxPreambleLengthInBytes)
        {
            throw new InvalidOperationException();
        }
#endif
    }

    private static readonly Encoding[] PreambleEncodings;
    private const int MaxPreambleLengthInBytes = 4;

    public DynamicEncodingLineReader(Stream stream, Encoding initialEncoding)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        ChangeEncoding(initialEncoding ?? throw new ArgumentNullException(nameof(initialEncoding)));
    }

    private readonly Stream _stream;
    private Decoder _decoder;
    private int _maxSingleCharBytes;
    private int _maxSingleCharResultsCount;

    private readonly int _bufferMaxSize = 4096;
    private char[] _charBuffer = null;
    private int _charBufferUsedSize = 0;
    private byte[] _buffer = null;
    private int _byteBufferUsedSize = 0;
    private int _bufferIndex = -1;
    private bool _hasCheckedForPreamble = false;

    private readonly byte[] _singleDecoderByteArray = new byte[1];

    public Encoding CurrentEncoding { get; private set; }

    public static List<string> ReadLines(string filePath, Encoding defaultEncoding)
    {
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenReadFileStream(filePath);
        using var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? Encoding.UTF8);
        return reader.ReadLines().ToList();
    }

    public static async Task<IEnumerable<string>> ReadLinesAsync(string filePath, Encoding defaultEncoding)
    {
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenAsyncReadFileStream(filePath);
        using var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? Encoding.UTF8);
        return await reader.ReadLinesAsync().ConfigureAwait(false);
    }

    public string ReadLine()
    {
        if (!_hasCheckedForPreamble)
        {
            ReadPreamble();
        }

        var builder = StringBuilderPool.Get();
        while (ReadNextChars())
        {
            if (ProcessCharsForLine(builder))
            {
                break;
            }
        }

        if (_charBuffer is null && builder.Length == 0)
        {
            return null;
        }

        return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
    }

    public async Task<string> ReadLineAsync()
    {
        if (!_hasCheckedForPreamble)
        {
            await ReadPreambleAsync().ConfigureAwait(false);
        }

        var builder = StringBuilderPool.Get();
        while (await ReadNextCharsAsync().ConfigureAwait(false))
        {
            if (ProcessCharsForLine(builder))
            {
                break;
            }
        }

        if (_charBuffer is null && builder.Length == 0)
        {
            StringBuilderPool.Return(builder);
            return null;
        }

        return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
    }

    private bool ProcessCharsForLine(StringBuilder builder)
    {
        var firstNonLineBreakCharacter = -1;
        var lastNonLineBreakCharacter = -1;

        for (var i = 0; i < _charBufferUsedSize; i++)
        {
            var charValue = _charBuffer[i];
            if (charValue != '\r' && charValue != '\n')
            {
                firstNonLineBreakCharacter = i;
                break;
            }
        }

        for (var i = _charBufferUsedSize - 1; i >= 0; i--)
        {
            var charValue = _charBuffer[i];
            if (charValue != '\r' && charValue != '\n')
            {
                lastNonLineBreakCharacter = i;
                break;
            }
        }

        if (firstNonLineBreakCharacter == -1 || lastNonLineBreakCharacter == -1)
        {
            return true;
        }
        else
        {
            builder.Append(_charBuffer, firstNonLineBreakCharacter, lastNonLineBreakCharacter - firstNonLineBreakCharacter + 1);
            return lastNonLineBreakCharacter != _charBufferUsedSize - 1;
        }
    }

    private bool ReadNextChars()
    {
        if (_charBuffer is null || _charBuffer.Length < _maxSingleCharResultsCount)
        {
            _charBuffer = new char[_maxSingleCharResultsCount];
        }

        var bytesConsumed = 0;
        while (bytesConsumed < _maxSingleCharBytes)
        {
            var nextByte = ReadByte();
            if (nextByte < 0)
            {
                break;
            }

            bytesConsumed++;

            var charsProduced = TryDecode((byte)nextByte, _charBuffer);
            if (charsProduced > 0)
            {
                _charBufferUsedSize = charsProduced;
                return true;
            }
        }

        _charBuffer = null;
        _charBufferUsedSize = 0;
        return false;
    }

    private async Task<bool> ReadNextCharsAsync()
    {
        if (_charBuffer is null || _charBuffer.Length < _maxSingleCharResultsCount)
        {
            _charBuffer = new char[_maxSingleCharResultsCount];
        }

        var bytesConsumed = 0;
        while (bytesConsumed < _maxSingleCharBytes)
        {
            var nextByte = await ReadByteAsync().ConfigureAwait(false);
            if (nextByte < 0)
            {
                break;
            }

            bytesConsumed++;

            var charsProduced = TryDecode((byte)nextByte, _charBuffer);
            if (charsProduced > 0)
            {
                _charBufferUsedSize = charsProduced;
                return true;
            }
        }

        _charBuffer = null;
        _charBufferUsedSize = 0;
        return false;
    }

    private int TryDecode(byte byteValue, char[] chars)
    {
        _singleDecoderByteArray[0] = byteValue;
        _decoder.Convert(
                _singleDecoderByteArray,
                0,
                1,
                chars,
                0,
                chars.Length,
                false,
                out int bytesConverted,
                out int charsProduced,
                out bool completed);

        return charsProduced;
    }

    private bool ReadPreamble() =>
        HandlePreambleBytes(ReadBytes(MaxPreambleLengthInBytes));

    private async Task<bool> ReadPreambleAsync() =>
        HandlePreambleBytes(await ReadBytesAsync(MaxPreambleLengthInBytes).ConfigureAwait(false));

    private bool HandlePreambleBytes(byte[] possiblePreambleBytes)
    {
        if (possiblePreambleBytes is null || possiblePreambleBytes.Length == 0)
        {
            return false;
        }

        int? bytesToRestore = null;
        foreach (var candidateEncoding in PreambleEncodings)
        {
            var encodingPreamble = candidateEncoding.GetPreamble();
            if (encodingPreamble is null || encodingPreamble.Length == 0)
            {
                continue;
            }

            if (
                possiblePreambleBytes.Length >= encodingPreamble.Length
                &&
                possiblePreambleBytes.AsSpan(0, encodingPreamble.Length).SequenceEqual(encodingPreamble.AsSpan())
            )
            {
                bytesToRestore = possiblePreambleBytes.Length - encodingPreamble.Length;
                ChangeEncoding(candidateEncoding);
                break;
            }
        }

        RevertReadBytes(bytesToRestore ?? possiblePreambleBytes.Length);

        _hasCheckedForPreamble = true;
        return true;
    }

    private int ReadByte()
    {
        if (!PrepareBuffer())
        {
            return -1;
        }

        return HandleReadByteIncrement();
    }

    private byte[] ReadBytes(int count)
    {
        var result = new byte[count];
        var resultOffset = 0;
        var bytesNeeded = result.Length;

        while (bytesNeeded > 0)
        {
            if (!PrepareBuffer())
            {
                return null;
            }

            HandleReadBytesIncrement(result, ref bytesNeeded, ref resultOffset);
        }

        return result;
    }


    private async Task<int> ReadByteAsync()
    {
        if (!await PrepareBufferAsync().ConfigureAwait(false))
        {
            return -1;
        }

        return HandleReadByteIncrement();
    }

    private async Task<byte[]> ReadBytesAsync(int count)
    {
        var result = new byte[count];
        var resultOffset = 0;
        var bytesNeeded = result.Length;

        while (bytesNeeded > 0)
        {
            if (!await PrepareBufferAsync().ConfigureAwait(false))
            {
                return null;
            }

            HandleReadBytesIncrement(result, ref bytesNeeded, ref resultOffset);
        }

        return result;
    }

    private int HandleReadByteIncrement()
    {
        var result = _buffer[_bufferIndex];
        if (1 >= _byteBufferUsedSize - _bufferIndex)
        {
            _bufferIndex = _byteBufferUsedSize;
        }
        else
        {
            _bufferIndex++;
        }

        return result;
    }

    private void HandleReadBytesIncrement(byte[] result, ref int bytesNeeded, ref int resultOffset)
    {
        var bytesLeftInBuffer = _byteBufferUsedSize - _bufferIndex;
        if (bytesNeeded >= bytesLeftInBuffer)
        {
            Buffer.BlockCopy(_buffer, _bufferIndex, result, resultOffset, bytesLeftInBuffer);
            _bufferIndex = _byteBufferUsedSize;
            resultOffset += bytesLeftInBuffer;
            bytesNeeded -= bytesLeftInBuffer;
        }
        else
        {
            Buffer.BlockCopy(_buffer, _bufferIndex, result, resultOffset, bytesNeeded);
            _bufferIndex += bytesNeeded;
            resultOffset += bytesNeeded;
            bytesNeeded = 0;
        }
    }

    private bool PrepareBuffer()
    {
        if (_buffer is null)
        {
            _buffer = new byte[_bufferMaxSize];
        }
        else if (_bufferIndex < _byteBufferUsedSize)
        {
            return true;
        }

        _bufferIndex = 0;
        _byteBufferUsedSize = _stream.Read(_buffer, 0, _buffer.Length);
        return _byteBufferUsedSize != 0;
    }

    private async Task<bool> PrepareBufferAsync()
    {
        if (_buffer is null)
        {
            _buffer = new byte[_bufferMaxSize];
        }
        else if (_bufferIndex < _byteBufferUsedSize)
        {
            return true;
        }

        _bufferIndex = 0;
        _byteBufferUsedSize = await _stream.ReadAsync(_buffer, 0, _buffer.Length).ConfigureAwait(false);
        return _byteBufferUsedSize != 0;
    }

    private void RevertReadBytes(int count)
    {
        if (count == 0)
        {
            return;
        }

        if (_buffer is null) throw new InvalidOperationException();

        var revertedIndex = _bufferIndex - count;
        if (revertedIndex < 0 || revertedIndex >= _byteBufferUsedSize)
        {
            throw new InvalidOperationException();
        }

        _bufferIndex = revertedIndex;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    private string ProcessLine(string rawLine)
    {
        HandleLineForEncoding(rawLine);
        return rawLine;
    }

    private void HandleLineForEncoding(string line)
    {
        // read through the initial whitespace
        var startIndex = 0;
        for (; startIndex < line.Length && line[startIndex].IsTabOrSpace(); startIndex++) ;

        if (startIndex == line.Length)
        {
            return; // empty or whitespace
        }

        if (line.Length - startIndex < 5
            || line[startIndex] != 'S'
            || line[++startIndex] != 'E'
            || line[++startIndex] != 'T')
        {
            return; // not a set command
        }

        startIndex++;

        // read the whitespace to find the encoding name
        for (; startIndex < line.Length && line[startIndex].IsTabOrSpace(); startIndex++) ;
        
        // read through the final trailing whitespace if any
        var endIndex = line.Length - 1;
        for (; endIndex > startIndex && line[endIndex].IsTabOrSpace(); endIndex--) ;

        ChangeEncoding(line.AsSpan(startIndex, endIndex - startIndex + 1));
    }

    private void ChangeEncoding(ReadOnlySpan<char> encodingName)
    {
        var newEncoding = EncodingEx.GetEncodingByName(encodingName);
        ChangeEncoding(newEncoding);
    }

    private void ChangeEncoding(Encoding newEncoding)
    {
        if (CurrentEncoding is not null && (newEncoding is null || ReferenceEquals(newEncoding, CurrentEncoding) || CurrentEncoding.Equals(newEncoding)))
        {
            return;
        }

        _decoder = newEncoding.GetDecoder();
        CurrentEncoding = newEncoding;
        _maxSingleCharBytes = CurrentEncoding.GetMaxByteCount(1);
        _maxSingleCharResultsCount = CurrentEncoding.GetMaxCharCount(_maxSingleCharBytes);
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}
