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
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            ChangeEncoding(initialEncoding ?? throw new ArgumentNullException(nameof(initialEncoding)));
        }

        private readonly Stream stream;
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

        public Encoding CurrentEncoding { get; private set; }

        public static List<string> ReadLines(string filePath, Encoding defaultEncoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? Encoding.UTF8))
            {
                return reader.ReadLines().ToList();
            }
        }

        public static async Task<IEnumerable<string>> ReadLinesAsync(string filePath, Encoding defaultEncoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            using (var reader = new DynamicEncodingLineReader(stream, defaultEncoding ?? Encoding.UTF8))
            {
                return await reader.ReadLinesAsync().ConfigureAwait(false);
            }
        }

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
                StringBuilderPool.Return(builder);
                return null;
            }

            return ProcessLine(StringBuilderPool.GetStringAndReturn(builder));
        }

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

        private readonly byte[] singleDecoderByteArray = new byte[1];

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

        private bool ReadPreamble() =>
            HandlePreambleBytes(ReadBytes(MaxPreambleLengthInBytes));

        private async Task<bool> ReadPreambleAsync() =>
            HandlePreambleBytes(await ReadBytesAsync(MaxPreambleLengthInBytes).ConfigureAwait(false));

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
            if (CurrentEncoding != null && (newEncoding == null || ReferenceEquals(newEncoding, CurrentEncoding) || CurrentEncoding.Equals(newEncoding)))
            {
                return;
            }

            decoder = newEncoding.GetDecoder();
            CurrentEncoding = newEncoding;
            maxSingleCharBytes = CurrentEncoding.GetMaxByteCount(1);
            maxSingleCharResultsCount = CurrentEncoding.GetMaxCharCount(maxSingleCharBytes);
        }

        public void Dispose() =>
            stream.Dispose();
    }
}
