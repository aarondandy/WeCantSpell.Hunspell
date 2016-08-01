using System.Threading.Tasks;

namespace Hunspell
{
    /// <summary>
    /// Defines operations to read lines from a dictionary stream or file sequentially.
    /// </summary>
    public interface IDictionaryLineReader
    {
        /// <summary>
        /// Reads the next line from a dictionary file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The reult value will contain the contents of the next line as a string or the value <c>null</c> indicating there are no more lines to be read.</returns>
        Task<string> ReadLineAsync();
    }
}
