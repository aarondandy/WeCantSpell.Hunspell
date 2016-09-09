using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Hunspell.NetCore.Performance.TestHarness
{
    public class Program
    {
        private static readonly char[] CommonWordSplitChars = new[] { ' ', '\t', ',' };

        static void Main(string[] args)
        {
            var hunspell = Hunspell.FromFile("English (American).dic");
            var words = File.ReadAllLines("List_of_common_misspellings.txt", Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .SelectMany(line => line.Split(CommonWordSplitChars, StringSplitOptions.RemoveEmptyEntries))
                .Take(50)
                .ToList();

            foreach(var word in words)
            {
                var isFound = hunspell.Check(word);
                if (!isFound)
                {
                    var suggestions = hunspell.Suggest(word);
                }
            }
        }
    }
}
