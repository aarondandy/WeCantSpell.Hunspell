using System;
using System.IO;
using System.Reflection;

namespace WeCantSpell.Hunspell.Performance.TestHarness.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var exeDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filePath = Path.Combine(exeDirectory, "files/be-official.dic");

            for(var i = 0; i < 10; i++)
            {
                Console.Write($"Loading {Path.GetFileName(filePath)} :");

                var file = WordListReader.ReadFile(filePath);

                Console.WriteLine("Done");
            }
        }
    }
}