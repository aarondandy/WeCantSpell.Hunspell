using System.IO;
using NBench;

namespace Hunspell.NetCore.Performance.Comparison
{
    public class WordCheckHunspellNetCorePerfSpec : EnWordPerfBase
    {
        private Counter WordsChecked;

        private Hunspell Checker;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            Checker = Hunspell.FromFileAsync(Path.Combine(filesDirectory, "English (American).dic")).Result;

            WordsChecked = context.GetCounter(nameof(WordsChecked));
        }

        [PerfBenchmark(
            Description = "How fast can Hunspell.NetCore check English (US) words?",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(WordsChecked))]
        public void Benchmark(BenchmarkContext context)
        {
            foreach (var word in Words)
            {
                var result = Checker.Check(word);
                WordsChecked.Increment();
            }
        }
    }
}
