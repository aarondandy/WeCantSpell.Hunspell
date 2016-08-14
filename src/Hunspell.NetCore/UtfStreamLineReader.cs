using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed class UtfStreamLineReader : IHunspellFileLineReader, IDisposable
    {
        public UtfStreamLineReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.stream = stream;
            reader = new StreamReader(stream, Encoding.UTF8, true);
        }

        public UtfStreamLineReader(string filePath)
            : this(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        private readonly Stream stream;
        private readonly StreamReader reader;

        public static List<string> ReadLines(string filePath)
        {
            using (var reader = new UtfStreamLineReader(filePath))
            {
                return reader.ReadLines().ToList();
            }
        }

        public static async Task<List<string>> ReadLinesAsync(string filePath)
        {
            using (var reader = new UtfStreamLineReader(filePath))
            {
                return await reader.ReadLinesAsync().ConfigureAwait(false);
            }
        }

        public Task<string> ReadLineAsync() => reader.ReadLineAsync();

        public string ReadLine() => reader.ReadLine();

        public void Dispose()
        {
            reader.Dispose();
            stream.Dispose();
        }
    }
}
