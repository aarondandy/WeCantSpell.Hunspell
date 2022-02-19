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
    public sealed class StaticEncodingLineReader : IHunspellLineReader, IDisposable
    {
        public StaticEncodingLineReader(Stream stream, Encoding encoding)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            reader = new StreamReader(stream, encoding ?? Encoding.UTF8, true);
        }

        private readonly Stream stream;
        private readonly StreamReader reader;

        public Encoding CurrentEncoding => reader.CurrentEncoding;

        public static List<string> ReadLines(string filePath, Encoding encoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            using (var reader = new StaticEncodingLineReader(stream, encoding))
            {
                return reader.ReadLines().ToList();
            }
        }

        public static async Task<IEnumerable<string>> ReadLinesAsync(string filePath, Encoding encoding)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            using (var reader = new StaticEncodingLineReader(stream, encoding))
            {
                return await reader.ReadLinesAsync().ConfigureAwait(false);
            }
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public string ReadLine() => reader.ReadLine();

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Task<string> ReadLineAsync() => reader.ReadLineAsync();

        public void Dispose()
        {
            reader.Dispose();
            stream.Dispose();
        }
    }
}
