using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.LongRunning;

public abstract class EnWordPerfBase
{
    protected WordList Checker;
    protected List<string> Words;

    protected EnWordPerfBase()
    {
    }

    [PerfSetup]
    public virtual void Setup(BenchmarkContext context)
    {
        var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
        var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");

        Task.WhenAll(
            Task.Run(LoadChecker),
            Task.Run(LoadWords))
            .GetAwaiter().GetResult();

        void LoadChecker()
        {
            Checker = WordList.CreateFromFiles(Path.Combine(filesDirectory, "English (American).dic"));
        }

        void LoadWords()
        {
            Words = new List<string>();
            using var stram = new FileStream(Path.Combine(filesDirectory, "List_of_common_misspellings.txt"), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            using var reader = new StreamReader(stram, Encoding.UTF8, true);
            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                line = line.Trim();

                if (line.Length == 0 || line.StartsWith("#") || line.StartsWith("["))
                {
                    continue;
                }

                Words.AddRange(line.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }
}
