﻿using System.Linq;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.LongRunning;

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
        NumberOfIterations = 1,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    [CounterThroughputAssertion(nameof(SuggestionQueries), MustBe.GreaterThanOrEqualTo, 30)]
    public void Benchmark(BenchmarkContext context)
    {
        foreach (var word in Words.Take(1000)) // TODO: remove the limit to allow the entire list when performance allows
        {
            _ = Checker.Suggest(word);
            SuggestionQueries.Increment();
        }
    }
}
