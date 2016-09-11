using System.Linq;
using NBench;

namespace Hunspell.NetCore.Performance.Tests
{
    public class EnWordSuggestPerfSpec : EnWordPerfBase
    {
        protected Counter SuggestionQueries;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);
            SuggestionQueries = context.GetCounter(nameof(SuggestionQueries));
        }

        [PerfBenchmark(
            Description = "Ensure that words can be suggested quickly.",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(SuggestionQueries))]
        [CounterThroughputAssertion(nameof(SuggestionQueries), MustBe.GreaterThanOrEqualTo, 30)]
        public void Benchmark(BenchmarkContext context)
        {
            foreach (var word in Words.Take(100)) // TODO: remove the limit to allow the entire list
            {
                var result = Checker.Suggest(word);
                SuggestionQueries.Increment();
            }
        }
    }
}
