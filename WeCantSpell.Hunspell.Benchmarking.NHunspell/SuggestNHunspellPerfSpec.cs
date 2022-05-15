using System;
using System.IO;
using System.Linq;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class SuggestNHunspellPerfSpec : EnWordPerfBase, IDisposable
{
    static SuggestNHunspellPerfSpec()
    {
        Utilities.ApplyCultureHacks();
    }

    public Counter SuggestionQueries;
    private global::NHunspell.Hunspell _checker;

    [PerfSetup]
    public override void Setup(BenchmarkContext context)
    {
        base.Setup(context);

        var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
        var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
        var dictionaryFilePath = Path.Combine(filesDirectory, "English (American).dic");
        var affixFilePath = Path.ChangeExtension(dictionaryFilePath, "aff");
        _checker = new global::NHunspell.Hunspell(affixFilePath, dictionaryFilePath);

        SuggestionQueries = context.GetCounter(nameof(SuggestionQueries));
    }

    [PerfCleanup]
    public void Dispose()
    {
        _checker?.Dispose();
    }

    [PerfBenchmark(
        Description = "How fast can NHunspell suggest English (US) words?",
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
