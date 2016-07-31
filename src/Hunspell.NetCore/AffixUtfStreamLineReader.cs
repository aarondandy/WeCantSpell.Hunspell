using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed class AffixUtfStreamLineReader : IAffixFileLineReader, IDisposable
    {
        public AffixUtfStreamLineReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.stream = stream;
            reader = new StreamReader(stream, Encoding.UTF8, true);
        }

        public AffixUtfStreamLineReader(string filePath)
            : this(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        private readonly Stream stream;
        private readonly StreamReader reader;

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
