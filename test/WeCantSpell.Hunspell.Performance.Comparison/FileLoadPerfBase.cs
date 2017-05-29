using System.IO;
using System.Linq;
using NBench;

namespace WeCantSpell.Hunspell.Performance.Comparison
{
    public abstract class FileLoadPerfBase
    {
        protected FileLoadTest[] TestFiles;

        protected const string TestWord = "test";

        protected FileLoadPerfBase()
        {
        }

        [PerfSetup]
        public virtual void Setup(BenchmarkContext context)
        {
            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePaths = Directory.GetFiles(filesDirectory, "*.dic")
                .OrderBy(p => p);

            TestFiles = dictionaryFilePaths
                .Select(dfp => new FileLoadTest
                {
                    DictionaryFilePath = dfp,
                    AffixFilePath = Path.ChangeExtension(dfp, "aff")
                })
                .ToArray();
        }

        protected struct FileLoadTest
        {
            public string AffixFilePath;

            public string DictionaryFilePath;
        }
    }
}
