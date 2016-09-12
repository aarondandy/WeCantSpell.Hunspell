using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Performance.TestHarness
{
    public class Program
    {
        private static readonly char[] CommonWordSplitChars = new[] { ' ', '\t', ',' };

        static void Main(string[] args)
        {
            //DictionaryLoads();
            Suggestions();
        }

        static void DictionaryLoads()
        {
            var testAssemblyPath = Path.GetFullPath(typeof(Program).Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePaths = Directory.GetFiles(filesDirectory, "*.dic").OrderBy(p => p);

            Task.WhenAll(dictionaryFilePaths.Select(Hunspell.FromFileAsync)).Wait();
        }

        static void Suggestions()
        {
            var hunspell = Hunspell.FromFile("files/English (American).dic");
            var words = File.ReadAllLines("files/List_of_common_misspellings.txt", Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .SelectMany(line => line.Split(CommonWordSplitChars, StringSplitOptions.RemoveEmptyEntries))
                .Take(500)
                .ToList();

            foreach (var word in words)
            {
                var isFound = hunspell.Check(word);
                var suggestions = hunspell.Suggest(word);
            }
        }
    }
}
