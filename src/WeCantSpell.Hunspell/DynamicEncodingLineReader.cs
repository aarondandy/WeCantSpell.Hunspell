using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
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
#if !NO_UTF32
                    ,new UTF32Encoding(false, true)
#endif
                    ,Encoding.UTF8

#if !NO_UTF32
                    ,new UTF32Encoding(true, true)
#endif
                };

            MaxPreambleLengthInBytes = PreambleEncodings.Max(e => e.GetPreamble().Length);
        }

        private static readonly Regex SetEncodingRegex = new Regex(
            @"^[\t ]*SET[\t ]+([^\t ]+)[\t ]*$",
#if !NO_COMPILEDREGEX
            RegexOptions.Compiled |
#endif
            RegexOptions.CultureInvariant);

        private static readonly Encoding[] PreambleEncodings;
        private static readonly int MaxPreambleLengthInBytes;

        public DynamicEncodingLineReader(Stream stream, Encoding initialEncoding)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            ChangeEncoding(initialEncoding ?? throw new ArgumentNullException(nameof(initialEncoding)));
        }

        private readonly Stream stream;
        private Encoding encoding;
        private Decoder decoder;
        private int maxSingleCharBytes;
        private int maxSingleCharResultsCount;

        private readonly int bufferMaxSize = 4096;
        private char[] charBuffer = null;
        private int charBufferUsedSize = 0;
        private byte[] buffer = null;
        private int byteBufferUsedSize = 0;
        private int bufferIndex = -1;
        private bool hasCheckedForPreamble = false;

        public Encoding CurrentEncoding => encoding;

#if !NO_IO_FILE
        public static List<string> ReadLines(string filePath, Encoding defaultEncoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? EncodingEx.DefaultReadEncoding))
            {
                return reader.ReadLines().ToList();
            }
        }

#if !NO_ASYNC
        public static async Task<List<string>> ReadLinesAsync(string filePath, Encoding defaultEncoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? EncodingEx.DefaultReadEncoding))
            {
                return await reader.ReadLinesAsync().ConfigureAwait(false);
            }
        }
#endif

#endif

        public string ReadLine()
        {
            if (!hasCheckedForPreamble)
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

            if (charBuffer == null && builder.Length == 0)
            {
                return null;
            }

            return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
        }

#if !NO_ASYNC
        public async Task<string> ReadLineAsync()
        {
            if (!hasCheckedForPreamble)
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

            if (charBuffer == null && builder.Length == 0)
            {
                return null;
            }

            return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
        }
#endif

        private bool ProcessCharsForLine(StringBuilder builder)
        {
            var firstNonLineBreakCharacter = -1;
            var lastNonLineBreakCharacter = -1;

            for (var i = 0; i < charBufferUsedSize; i++)
            {
                var charValue = charBuffer[i];
                if (charValue != '\r' && charValue != '\n')
                {
                    firstNonLineBreakCharacter = i;
                    break;
                }
            }

            for (var i = charBufferUsedSize - 1; i >= 0; i--)
            {
                var charValue = charBuffer[i];
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
                builder.Append(charBuffer, firstNonLineBreakCharacter, lastNonLineBreakCharacter - firstNonLineBreakCharacter + 1);
                return lastNonLineBreakCharacter != charBufferUsedSize - 1;
            }
        }

        private bool ReadNextChars()
        {
            if (charBuffer == null || charBuffer.Length < maxSingleCharResultsCount)
            {
                charBuffer = new char[maxSingleCharResultsCount];
            }

            var bytesConsumed = 0;
            while (bytesConsumed < maxSingleCharBytes)
            {
                var nextByte = ReadByte();
                if (nextByte < 0)
                {
                    break;
                }

                bytesConsumed++;

                var charsProduced = TryDecode((byte)nextByte, charBuffer);
                if (charsProduced > 0)
                {
                    charBufferUsedSize = charsProduced;
                    return true;
                }
            }

            charBuffer = null;
            charBufferUsedSize = 0;
            return false;
        }

#if !NO_ASYNC
        private async Task<bool> ReadNextCharsAsync()
        {
            if (charBuffer == null || charBuffer.Length < maxSingleCharResultsCount)
            {
                charBuffer = new char[maxSingleCharResultsCount];
            }

            var bytesConsumed = 0;
            while (bytesConsumed < maxSingleCharBytes)
            {
                var nextByte = await ReadByteAsync().ConfigureAwait(false);
                if (nextByte < 0)
                {
                    break;
                }

                bytesConsumed++;

                var charsProduced = TryDecode((byte)nextByte, charBuffer);
                if (charsProduced > 0)
                {
                    charBufferUsedSize = charsProduced;
                    return true;
                }
            }

            charBuffer = null;
            charBufferUsedSize = 0;
            return false;
        }
#endif

        private byte[] singleDecoderByteArray = new byte[1];

        private int TryDecode(byte byteValue, char[] chars)
        {
            singleDecoderByteArray[0] = byteValue;
            decoder.Convert(
                    singleDecoderByteArray,
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

        private int TryDecode(byte[] bytes, char[] chars)
        {
            decoder.Convert(
                    bytes,
                    0,
                    bytes.Length,
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

#if !NO_ASYNC
        private async Task<bool> ReadPreambleAsync() =>
            HandlePreambleBytes(await ReadBytesAsync(MaxPreambleLengthInBytes).ConfigureAwait(false));
#endif

        private bool HandlePreambleBytes(byte[] possiblePreambleBytes)
        {
            if (possiblePreambleBytes == null || possiblePreambleBytes.Length == 0)
            {
                return false;
            }

            int? bytesToRestore = null;
            foreach (var candidateEncoding in PreambleEncodings)
            {
                var encodingPreamble = candidateEncoding.GetPreamble();
                if (encodingPreamble == null || encodingPreamble.Length == 0)
                {
                    continue;
                }

                if (
                    possiblePreambleBytes.Length >= encodingPreamble.Length
                    && ArrayComparer<byte>.Default.Equals(possiblePreambleBytes, 0, encodingPreamble, 0, encodingPreamble.Length)
                )
                {
                    bytesToRestore = possiblePreambleBytes.Length - encodingPreamble.Length;
                    ChangeEncoding(candidateEncoding);
                    break;
                }
            }

            RevertReadBytes(bytesToRestore ?? possiblePreambleBytes.Length);

            hasCheckedForPreamble = true;
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

#if !NO_ASYNC

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
#endif

        private int HandleReadByteIncrement()
        {
            var result = buffer[bufferIndex];
            if (1 >= byteBufferUsedSize - bufferIndex)
            {
                bufferIndex = byteBufferUsedSize;
            }
            else
            {
                bufferIndex++;
            }

            return result;
        }

        private void HandleReadBytesIncrement(byte[] result, ref int bytesNeeded, ref int resultOffset)
        {
            var bytesLeftInBuffer = byteBufferUsedSize - bufferIndex;
            if (bytesNeeded >= bytesLeftInBuffer)
            {
                Buffer.BlockCopy(buffer, bufferIndex, result, resultOffset, bytesLeftInBuffer);
                bufferIndex = byteBufferUsedSize;
                resultOffset += bytesLeftInBuffer;
                bytesNeeded -= bytesLeftInBuffer;
            }
            else
            {
                Buffer.BlockCopy(buffer, bufferIndex, result, resultOffset, bytesNeeded);
                bufferIndex += bytesNeeded;
                resultOffset += bytesNeeded;
                bytesNeeded = 0;
            }
        }

        private bool PrepareBuffer()
        {
            if (buffer == null)
            {
                buffer = new byte[bufferMaxSize];
            }
            else if (bufferIndex < byteBufferUsedSize)
            {
                return true;
            }

            bufferIndex = 0;
            byteBufferUsedSize = stream.Read(buffer, 0, buffer.Length);
            return byteBufferUsedSize != 0;
        }

#if !NO_ASYNC
        private async Task<bool> PrepareBufferAsync()
        {
            if (buffer == null)
            {
                buffer = new byte[bufferMaxSize];
            }
            else if (bufferIndex < byteBufferUsedSize)
            {
                return true;
            }

            bufferIndex = 0;
            byteBufferUsedSize = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            return byteBufferUsedSize != 0;
        }
#endif
        
        private void RevertReadBytes(int count)
        {
            if (count == 0)
            {
                return;
            }

            if (buffer == null)
            {
                throw new InvalidOperationException();
            }

            var revertedIndex = bufferIndex - count;
            if (revertedIndex < 0 || revertedIndex >= byteBufferUsedSize)
            {
                throw new InvalidOperationException();
            }

            bufferIndex = revertedIndex;
        }

        private string ProcessLine(string rawLine)
        {
            var setEncodingMatch = SetEncodingRegex.Match(rawLine);
            if (setEncodingMatch.Success)
            {
                ChangeEncoding(setEncodingMatch.Groups[1].Value);
            }

            return rawLine;
        }

        private void ChangeEncoding(string encodingName)
        {
            var newEncoding = EncodingEx.GetEncodingByName(encodingName);
            ChangeEncoding(newEncoding);
        }

        private void ChangeEncoding(Encoding newEncoding)
        {
            if (encoding != null && (newEncoding == null || ReferenceEquals(newEncoding, encoding) || encoding.Equals(newEncoding)))
            {
                return;
            }

            decoder = newEncoding.GetDecoder();
            encoding = newEncoding;
            maxSingleCharBytes = encoding.GetMaxByteCount(1);
            maxSingleCharResultsCount = encoding.GetMaxCharCount(maxSingleCharBytes);
        }

        public void Dispose() =>
            stream.Dispose();
    }
}
