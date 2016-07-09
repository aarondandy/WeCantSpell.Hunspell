using System.Threading.Tasks;

namespace Hunspell
{
    public interface IAffixFileLineReader
    {
        Task<string> ReadLineAsync();
    }
}
