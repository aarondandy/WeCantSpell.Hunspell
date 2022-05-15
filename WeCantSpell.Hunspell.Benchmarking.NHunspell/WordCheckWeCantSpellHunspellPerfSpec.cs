using System.IO;

using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

public class WordCheckWeCantSpellHunspellPerfSpec : EnWordPerfBase
{
    private Counter WordsChecked;
    private WordList _checker;

    [PerfSetup]
    public override void Setup(BenchmarkContext context)
    {
        base.Setup(context);

        var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
        var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
        _checker = WordList.CreateFromFiles(Path.Combine(filesDirectory, "English (American).dic"));

        WordsChecked = context.GetCounter(nameof(WordsChecked));
    }

    [PerfBenchmark(
        Description = "How fast can this project check English (US) words?",
        NumberOfIterations = 3,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
    [CounterThroughputAssertion(nameof(WordsChecked), MustBe.GreaterThanOrEqualTo, 250_000)]
    public void Benchmark(BenchmarkContext context)
    {
        foreach (var word in Words)
        {
            _ = _checker.Check(word);
            WordsChecked.Increment();
        }
    }
}
