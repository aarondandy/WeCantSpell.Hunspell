using NBench;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Performance.Tests
{
    public class AffixFileLoadPerfSpecs
    {
        internal const string CounterNameAffixFilesLoaded = "AffixFilesLoaded";

        private Counter _filesLoadedCounter;
        private string[] _affixFilePaths;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            _affixFilePaths = Directory.GetFiles(filesDirectory, "*.aff");
            _filesLoadedCounter = context.GetCounter(CounterNameAffixFilesLoaded);
        }

        [PerfBenchmark(
            Description = "Ensure that affix files can be loaded quickly.",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(CounterNameAffixFilesLoaded)]
        [CounterThroughputAssertion(CounterNameAffixFilesLoaded, MustBe.GreaterThanOrEqualTo, 2)]
        public void Benchmark()
        {
            Task.WhenAll(_affixFilePaths.Select(async (filePath) =>
            {
                await AffixReader.ReadFileAsync(filePath);
                _filesLoadedCounter.Increment();
            })).Wait();
        }
    }
}
