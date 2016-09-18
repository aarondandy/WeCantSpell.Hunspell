using System.IO;
using NBench;

namespace Hunspell.NetCore.Performance.Comparison
{
    public class WordCheckNHunspellPerfSpec : EnWordPerfBase
    {
        private Counter WordsChecked;

        private NHunspell.Hunspell Checker;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePath = Path.Combine(filesDirectory, "English (American).dic");
            var affixFilePath = Path.ChangeExtension(dictionaryFilePath, "aff");
            Checker = new NHunspell.Hunspell(affixFilePath, dictionaryFilePath);

            WordsChecked = context.GetCounter(nameof(WordsChecked));
        }

        [PerfCleanup]
        public void Cleanup()
        {
            Checker?.Dispose();
        }

        [PerfBenchmark(
            Description = "How fast can NHunspell check English (US) words?",
            NumberOfIterations = 10,
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
                var result = Checker.Spell(word);
                WordsChecked.Increment();
            }
        }
    }
}
