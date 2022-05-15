using System.IO;
using System.Linq;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class SuggestWeCantSpellHunspellPerfSpec : EnWordPerfBase
{
    public Counter SuggestionQueries;
    private WordList _checker;

    [PerfSetup]
    public override void Setup(BenchmarkContext context)
    {
        base.Setup(context);

        var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
        var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
        _checker = WordList.CreateFromFiles(Path.Combine(filesDirectory, "English (American).dic"));

        SuggestionQueries = context.GetCounter(nameof(SuggestionQueries));
    }

    [PerfBenchmark(
        Description = "How fast can this project suggest English (US) words?",
        NumberOfIterations = 1,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    [CounterThroughputAssertion(nameof(SuggestionQueries), MustBe.GreaterThanOrEqualTo, 100)]
    public void Benchmark(BenchmarkContext context)
    {
        foreach (var word in Words.Take(1000))
        {
            _ = _checker.Suggest(word);
            SuggestionQueries.Increment();
        }
    }
}
