using NBench;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeCantSpell.Hunspell.Benchmarking.LongRunning
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

            DictionaryLoadArguments = dictionaryFilePaths
                .Select(dicFilePath =>
                    new DictionaryLoadData
                    {
                        DictionaryFilePath = dicFilePath,
                        Affix = AffixReader.ReadFile(Path.ChangeExtension(dicFilePath, "aff"))
                    })
                .ToArray();

            DictionaryFilesLoaded = context.GetCounter(nameof(DictionaryFilesLoaded));
        }

        [PerfBenchmark(
            Description = "Ensure that dictionary files can be loaded quickly.",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(DictionaryFilesLoaded))]
        [CounterThroughputAssertion(nameof(DictionaryFilesLoaded), MustBe.GreaterThanOrEqualTo, 2)]
        public void BenchmarkSync(BenchmarkContext context)
        {
            foreach (var testItem in DictionaryLoadArguments)
            {
                WordListReader.ReadFile(testItem.DictionaryFilePath, testItem.Affix);
                DictionaryFilesLoaded.Increment();
            }
        }

        [PerfBenchmark(
            Description = "Ensure that dictionary files can be loaded asynchronously and quickly.",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement,
            Skip = "All the perf improvements so far will impact sync")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(DictionaryFilesLoaded))]
        [CounterThroughputAssertion(nameof(DictionaryFilesLoaded), MustBe.GreaterThanOrEqualTo, 2)]
        public void BenchmarkAsync(BenchmarkContext context)
        {
            BenchmarkAsync_Impl(context).GetAwaiter().GetResult();
        }

        private async Task BenchmarkAsync_Impl(BenchmarkContext context)
        {
            foreach (var testItem in DictionaryLoadArguments)
            {
                await WordListReader.ReadFileAsync(testItem.DictionaryFilePath, testItem.Affix).ConfigureAwait(false);
                DictionaryFilesLoaded.Increment();
            }
        }

        protected struct DictionaryLoadData
        {
            public AffixConfig Affix;

            public string DictionaryFilePath;
        }
    }
}
