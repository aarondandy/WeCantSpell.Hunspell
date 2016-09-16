using NBench;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Performance.Tests
{
    public class AffixFileLoadPerfSpecs
    {
        protected Counter AffixFilesLoaded;
        protected string[] AffixFilePaths;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            AffixFilePaths = Directory.GetFiles(filesDirectory, "*.aff")
                .OrderBy(p => p)
                .Where((_,i) => i % 10 == 0)
                .ToArray();
            AffixFilesLoaded = context.GetCounter(nameof(AffixFilesLoaded));
        }

        [PerfBenchmark(
            Description = "Ensure that affix files can be loaded quickly.",
            NumberOfIterations = 2,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(AffixFilesLoaded))]
        [CounterThroughputAssertion(nameof(AffixFilesLoaded), MustBe.GreaterThanOrEqualTo, 5)]
        public void Benchmark(BenchmarkContext context)
        {
            Task.WhenAll(AffixFilePaths.Select(async filePath =>
            {
                await AffixReader.ReadFileAsync(filePath).ConfigureAwait(false);
                AffixFilesLoaded.Increment();
            })).Wait();
        }
    }
}
