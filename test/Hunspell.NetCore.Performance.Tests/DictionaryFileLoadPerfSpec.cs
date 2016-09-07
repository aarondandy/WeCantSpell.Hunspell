using NBench;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Performance.Tests
{
    public class DictionaryFileLoadPerfSpec
    {
        protected Counter DictionaryFilesLoaded;
        protected DictionaryLoadData[] DictionaryLoadArguments;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePaths = Directory.GetFiles(filesDirectory, "*.dic")
                .OrderBy(p => p);
            DictionaryLoadArguments = Task.WhenAll(dictionaryFilePaths
                .Select(async dicFilePath => new DictionaryLoadData
                {
                    DictionaryFilePath = dicFilePath,
                    Affix = await AffixReader.ReadFileAsync(Path.ChangeExtension(dicFilePath, "aff"))
                }))
                .Result;
            DictionaryFilesLoaded = context.GetCounter(nameof(DictionaryFilesLoaded));
        }

        [PerfBenchmark(
            Description = "Ensure that dictionary files can be loaded quickly.",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(DictionaryFilesLoaded))]
        [CounterThroughputAssertion(nameof(DictionaryFilesLoaded), MustBe.GreaterThanOrEqualTo, 3)]
        public void Benchmark()
        {
            Task.WhenAll(DictionaryLoadArguments.Select(async testItem =>
            {
                await DictionaryReader.ReadFileAsync(testItem.DictionaryFilePath, testItem.Affix);
                DictionaryFilesLoaded.Increment();
            })).Wait();
        }

        protected struct DictionaryLoadData
        {
            public AffixConfig Affix;

            public string DictionaryFilePath;
        }
    }
}
