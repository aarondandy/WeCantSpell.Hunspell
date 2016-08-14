using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hunspell
{
    /// <summary>
    /// Defines operations to read affix or dictionary lines from a stream sequentially.
    /// </summary>
    public interface IHunspellFileLineReader
    {
        /// <summary>
        /// Reads the next line from a stream.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The reult value will contain the contents of the next line as a string or the value <c>null</c> indicating there are no more lines to be read.</returns>
        Task<string> ReadLineAsync();

        /// <summary>
        /// Reads the next line from a stream.
        /// </summary>
        /// <returns></returns>
        string ReadLine();
    }

    public static class HunspellFileLineReaderExtensions
    {
        public static async Task<List<string>> ReadLinesAsync(this IHunspellFileLineReader reader)
        {
            var lines = new List<string>();

            string line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                lines.Add(line);
            }

            return lines;
        }

        public static IEnumerable<string> ReadLines(this IHunspellFileLineReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
