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

namespace WeCantSpell.Hunspell
{
    public sealed class DynamicEncodingLineReader : IHunspellLineReader, IDisposable
    {
        static DynamicEncodingLineReader()
        {
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

            MaxPreambleBytes = PreambleEncodings.Max(e => e.GetPreamble().Length);
        }

        private static readonly Regex SetEncodingRegex = new Regex(
            @"^[\t ]*SET[\t ]+([^\t ]+)[\t ]*$",
#if !NO_COMPILEDREGEX
            RegexOptions.Compiled |
#endif
            RegexOptions.CultureInvariant);

        private static readonly Encoding[] PreambleEncodings;

        private static readonly int MaxPreambleBytes;

        public DynamicEncodingLineReader(Stream stream, Encoding initialEncoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (initialEncoding == null)
            {
                throw new ArgumentNullException(nameof(initialEncoding));
            }

            this.stream = stream;
            encoding = initialEncoding;
            decoder = initialEncoding.GetDecoder();
        }

        private readonly Stream stream;
        private Encoding encoding;
        private Decoder decoder;

        private readonly int bufferMaxSize = 4096;
        private byte[] buffer = null;
        private int bufferIndex = -1;
        private bool hasCheckedForPreamble = false;

        public Encoding CurrentEncoding => encoding;

#if !NO_IO_FILE
        public static List<string> ReadLines(string filePath, Encoding defaultEncoding)
        {
            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding))
            {
                return reader.ReadLines().ToList();
            }
        }

#if !NO_ASYNC
        public static async Task<List<string>> ReadLinesAsync(string filePath, Encoding defaultEncoding)
        {
            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding))
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
            char[] readChars = null;
            while ((readChars = ReadNextChars()) != null)
            {
                if (ProcessCharsForLine(readChars, builder))
                {
                    break;
                }
            }

            if (readChars == null && builder.Length == 0)
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
            char[] readChars = null;
            while ((readChars = await ReadNextCharsAsync().ConfigureAwait(false)) != null)
            {
                if (ProcessCharsForLine(readChars, builder))
                {
                    break;
                }
            }

            if (readChars == null && builder.Length == 0)
            {
                return null;
            }

            return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
        }
#endif

        private bool ProcessCharsForLine(char[] readChars, StringBuilder builder)
        {
            var firstNonLineBreakCharacter = -1;
            var lastNonLineBreakCharacter = -1;

            for (var i = 0; i < readChars.Length; i++)
            {
                var charValue = readChars[i];
                if (charValue != '\r' && charValue != '\n')
                {
                    firstNonLineBreakCharacter = i;
                    break;
                }
            }

            for (var i = readChars.Length - 1; i >= 0; i--)
            {
                var charValue = readChars[i];
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
                builder.Append(readChars, firstNonLineBreakCharacter, lastNonLineBreakCharacter - firstNonLineBreakCharacter + 1);
                return lastNonLineBreakCharacter != readChars.Length - 1;
            }
        }

        private char[] ReadNextChars()
        {
            var maxBytes = encoding.GetMaxByteCount(1);
            var bytesReadIntoCharBuffer = 0;
            var charOutBuffer = new char[encoding.GetMaxCharCount(maxBytes)];

            while (bytesReadIntoCharBuffer < maxBytes)
            {
                var nextBytes = ReadBytes(1);
                if (nextBytes == null || nextBytes.Length == 0)
                {
                    return null;
                }

                bytesReadIntoCharBuffer += nextBytes.Length;

                var charsProduced = TryDecode(nextBytes, charOutBuffer);
                if (charsProduced > 0)
                {
                    if (charOutBuffer.Length != charsProduced)
                    {
                        Array.Resize(ref charOutBuffer, charsProduced);
                    }

                    return charOutBuffer;
                }
            }

            return null;
        }

#if !NO_ASYNC
        private async Task<char[]> ReadNextCharsAsync()
        {
            var maxBytes = encoding.GetMaxByteCount(1);
            var bytesConsumed = 0;
            var charOutBuffer = new char[encoding.GetMaxCharCount(maxBytes)];

            while (bytesConsumed < maxBytes)
            {
                var nextBytes = await ReadBytesAsync(1).ConfigureAwait(false);
                if (nextBytes == null || nextBytes.Length == 0)
                {
                    return null;
                }

                bytesConsumed += nextBytes.Length;

                var charsProduced = TryDecode(nextBytes, charOutBuffer);
                if (charsProduced > 0)
                {
                    if (charOutBuffer.Length != charsProduced)
                    {
                        Array.Resize(ref charOutBuffer, charsProduced);
                    }

                    return charOutBuffer;
                }
            }

            return null;
        }
#endif

        private int TryDecode(byte[] bytes, char[] chars)
        {
            int bytesConverted;
            int charsProduced;
            bool completed;

            decoder.Convert(
                    bytes,
                    0,
                    bytes.Length,
                    chars,
                    0,
                    chars.Length,
                    false,
                    out bytesConverted,
                    out charsProduced,
                    out completed);

            return charsProduced;
        }

        private bool ReadPreamble()
        {
            var possiblePreambleBytes = ReadBytes(MaxPreambleBytes);
            return HandlePreambleBytes(possiblePreambleBytes);
        }

#if !NO_ASYNC
        private async Task<bool> ReadPreambleAsync()
        {
            var possiblePreambleBytes = await ReadBytesAsync(MaxPreambleBytes).ConfigureAwait(false);
            return HandlePreambleBytes(possiblePreambleBytes);
        }
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
                    encoding = candidateEncoding;
                    break;
                }
            }

            RevertReadBytes(bytesToRestore ?? possiblePreambleBytes.Length);

            hasCheckedForPreamble = true;
            return true;
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

        private void HandleReadBytesIncrement(byte[] result, ref int bytesNeeded, ref int resultOffset)
        {
            var bytesLeftInBuffer = buffer.Length - bufferIndex;
            if (bytesNeeded >= bytesLeftInBuffer)
            {
                Buffer.BlockCopy(buffer, bufferIndex, result, resultOffset, bytesLeftInBuffer);
                bufferIndex = buffer.Length;
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
            if (buffer != null && bufferIndex < buffer.Length)
            {
                return true;
            }

            buffer = new byte[bufferMaxSize];
            var readBytesCount = stream.Read(buffer, 0, buffer.Length);

            return HandlePrepareBufferRead(readBytesCount);
        }

#if !NO_ASYNC
        private async Task<bool> PrepareBufferAsync()
        {
            if (buffer != null && bufferIndex < buffer.Length)
            {
                return true;
            }

            buffer = new byte[bufferMaxSize];
            var readBytesCount = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            return HandlePrepareBufferRead(readBytesCount);
        }
#endif

        private bool HandlePrepareBufferRead(int readBytesCount)
        {
            if (readBytesCount != buffer.Length)
            {
                Array.Resize(ref buffer, readBytesCount);
            }

            bufferIndex = 0;
            return readBytesCount != 0;
        }

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
            if (revertedIndex < 0 || revertedIndex >= buffer.Length)
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
            if (newEncoding == null || ReferenceEquals(newEncoding, encoding) || encoding.Equals(newEncoding))
            {
                return;
            }

            decoder = newEncoding.GetDecoder();
            encoding = newEncoding;
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
