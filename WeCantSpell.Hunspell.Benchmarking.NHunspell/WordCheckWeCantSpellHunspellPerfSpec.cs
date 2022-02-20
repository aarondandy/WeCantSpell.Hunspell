using System.IO;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class WordCheckWeCantSpellHunspellPerfSpec : EnWordPerfBase
{
    private Counter _wordsChecked;
    private WordList _checker;

    [PerfSetup]
    public override void Setup(BenchmarkContext context)
    {
        base.Setup(context);

        var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
        var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
        _checker = WordList.CreateFromFiles(Path.Combine(filesDirectory, "English (American).dic"));

        _wordsChecked = context.GetCounter(nameof(_wordsChecked));
    }

    [PerfBenchmark(
        Description = "How fast can this project check English (US) words?",
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
            _ = _checker.Check(word);
            _wordsChecked.Increment();
        }
    }
}
