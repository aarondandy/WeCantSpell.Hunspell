using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NBench;

namespace WeCantSpell.Hunspell.Performance.Comparison
{
    public abstract class EnWordPerfBase
    {
        protected static readonly char[] WordSplitChars = { ' ', '\t', ',' };

        protected List<string> Words;

        protected EnWordPerfBase()
        {
        }

        [PerfSetup]
        public virtual void Setup(BenchmarkContext context)
        {
            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");

            Words = new List<string>();
            using (var reader = new StreamReader(Path.Combine(filesDirectory, "List_of_common_misspellings.txt"), Encoding.UTF8, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0 || line.StartsWith("#") || line.StartsWith("["))
                    {
                        continue;
                    }

                    Words.AddRange(line.Split(WordSplitChars, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
    }
}
