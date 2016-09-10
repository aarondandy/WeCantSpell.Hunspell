using NBench;

namespace Hunspell.NetCore.Performance.Tests
{
    public class EnWordCheckPerfSpecs : EnWordPerfBase
    {
        protected Counter WordsChecked;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);
            WordsChecked = context.GetCounter(nameof(WordsChecked));
        }

        [PerfBenchmark(
            Description = "Ensure that words can be checked quickly.",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(WordsChecked))]
        [CounterThroughputAssertion(nameof(WordsChecked), MustBe.GreaterThanOrEqualTo, 200000)]
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
