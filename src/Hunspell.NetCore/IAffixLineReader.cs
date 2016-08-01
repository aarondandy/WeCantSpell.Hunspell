using System.Threading.Tasks;

namespace Hunspell
{
    /// <summary>
    /// Defines operations to read line from an affix stream or file sequentially.
    /// </summary>
    public interface IAffixLineReader
    {
        /// <summary>
        /// Reads the next line from an affix file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The reult value will contain the contents of the next line as a string or the value <c>null</c> indicating there are no more lines to be read.</returns>
        Task<string> ReadLineAsync();
    }
}
