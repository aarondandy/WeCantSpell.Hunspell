using System.IO;
using NBench;

namespace WeCantSpell.Hunspell.Performance.Comparison
{
    public class WordCheckWeCantSpellHunspellPerfSpec : EnWordPerfBase
    {
        private Counter WordsChecked;

        private HunspellDictionary Checker;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            Checker = HunspellDictionary.FromFileAsync(Path.Combine(filesDirectory, "English (American).dic")).Result;

            WordsChecked = context.GetCounter(nameof(WordsChecked));
        }

        [PerfBenchmark(
            Description = "How fast can this project check English (US) words?",
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
