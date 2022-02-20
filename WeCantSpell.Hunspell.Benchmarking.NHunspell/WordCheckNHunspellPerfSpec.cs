using System;
using System.IO;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class WordCheckNHunspellPerfSpec : EnWordPerfBase, IDisposable
{
    static WordCheckNHunspellPerfSpec()
    {
        Utilities.ApplyCultureHacks();
    }

    private Counter _wordsChecked;
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

        _wordsChecked = context.GetCounter(nameof(_wordsChecked));
    }

    [PerfCleanup]
    public void Dispose()
    {
        _checker?.Dispose();
    }

    [PerfBenchmark(
        Description = "How fast can NHunspell check English (US) words?",
        NumberOfIterations = 3,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    [TimingMeasurement]
    [CounterMeasurement(nameof(_wordsChecked))]
    public void Benchmark(BenchmarkContext context)
    {
        foreach (var word in Words)
        {
            _ = _checker.Spell(word);
            _wordsChecked.Increment();
        }
    }
}
