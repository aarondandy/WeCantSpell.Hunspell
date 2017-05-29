using System.Collections.Generic;
using System.Text;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace WeCantSpell.Hunspell
{
    /// <summary>
    /// Defines operations to read affix or dictionary lines from a stream sequentially.
    /// </summary>
    public interface IHunspellLineReader
    {
#if !NO_ASYNC
        /// <summary>
        /// Reads the next line from a stream.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The reult value will contain the contents of the next line as a string or the value <c>null</c> indicating there are no more lines to be read.</returns>
        Task<string> ReadLineAsync();
#endif

        /// <summary>
        /// Reads the next line from a stream.
        /// </summary>
        /// <returns></returns>
        string ReadLine();

        /// <summary>
        /// Gets the current encoding that the reader is using to decode text.
        /// </summary>
        Encoding CurrentEncoding { get; }
    }

    public static class HunspellLineReaderExtensions
    {
#if !NO_ASYNC
        public static async Task<List<string>> ReadLinesAsync(this IHunspellLineReader reader)
        {
            var lines = new List<string>();

            string line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                lines.Add(line);
            }

            return lines;
        }
#endif

        public static IEnumerable<string> ReadLines(this IHunspellLineReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
