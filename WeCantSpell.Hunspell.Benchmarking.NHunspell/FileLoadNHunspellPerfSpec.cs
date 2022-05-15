using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class FileLoadNHunspellPerfSpec : FileLoadPerfBase
{
    static FileLoadNHunspellPerfSpec()
    {
        Utilities.ApplyCultureHacks();
    }

    protected Counter FilePairsLoaded;

    [PerfSetup]
    public override void Setup(BenchmarkContext context)
    {
        base.Setup(context);
        FilePairsLoaded = context.GetCounter(nameof(FilePairsLoaded));
    }

    [PerfBenchmark(
        Description = "How fast can NHunspell load files?",
        NumberOfIterations = 1,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    [CounterThroughputAssertion(nameof(FilePairsLoaded), MustBe.GreaterThanOrEqualTo, 5)]
    public void Benchmark(BenchmarkContext context)
    {
        foreach (var filePair in TestFiles)
        {
            var checker = new global::NHunspell.Hunspell(filePair.DictionaryFilePath, filePair.AffixFilePath);
            _ = checker.Spell(TestWord);
            FilePairsLoaded.Increment();
        }
    }
}
