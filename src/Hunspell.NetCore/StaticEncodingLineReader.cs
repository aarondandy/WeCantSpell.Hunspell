using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed class StaticEncodingLineReader : IHunspellLineReader, IDisposable
    {
        public StaticEncodingLineReader(Stream stream, Encoding encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            this.stream = stream;
            reader = new StreamReader(stream, encoding, true);
        }

        private readonly Stream stream;
        private readonly StreamReader reader;

        public Encoding CurrentEncoding => reader.CurrentEncoding;

        public static List<string> ReadLines(string filePath, Encoding defaultEncoding)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StaticEncodingLineReader(stream, defaultEncoding))
            {
                return reader.ReadLines().ToList();
            }
        }

        public static async Task<List<string>> ReadLinesAsync(string filePath, Encoding defaultEncoding)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StaticEncodingLineReader(stream, defaultEncoding))
            {
                return await reader.ReadLinesAsync().ConfigureAwait(false);
            }
        }

        public string ReadLine()
        {
            return reader.ReadLine();
        }

        public Task<string> ReadLineAsync()
        {
            return reader.ReadLineAsync();
        }

        public void Dispose()
        {
            reader.Dispose();
            stream.Dispose();
        }
    }
}
