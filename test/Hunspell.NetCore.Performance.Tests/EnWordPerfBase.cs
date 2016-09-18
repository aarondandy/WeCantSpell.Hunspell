using NBench;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Performance.Tests
{
    public abstract class EnWordPerfBase
    {
        protected static readonly char[] WordSplitChars = new[] { ' ', '\t', ',' };

        protected Hunspell Checker;
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
                new[]
                {
                    new Func<Task>(async () =>
                    {
                        Checker = await Hunspell.FromFileAsync(Path.Combine(filesDirectory, "English (American).dic")).ConfigureAwait(false);
                    }),
                    new Func<Task>(async () =>
                    {
                        Words = new List<string>();
                        using(var reader = new StreamReader(Path.Combine(filesDirectory, "List_of_common_misspellings.txt"), Encoding.UTF8, true))
                        {
                            string line;
                            while((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                            {
                                line = line.Trim();

                                if(line.Length == 0 || line.StartsWith("#") || line.StartsWith("["))
                                {
                                    continue;
                                }

                                Words.AddRange(line.Split(WordSplitChars, StringSplitOptions.RemoveEmptyEntries));
                            }
                        }
                    })
                }
                .Select(f => f())
            ).Wait();
        }
    }
}
