using BenchmarkDotNet.Attributes;
using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.Suites;

[SimpleJob(id: "Suggest en-US")]
[MinWarmupCount(1), MaxWarmupCount(5)]
[MinIterationCount(1), MaxIterationCount(20), MinInvokeCount(1), IterationTime(250)]
public class SuggestEnUsSuite
{
    protected WordList WordList;
    protected CategorizedWordData WordData = EnUsTestData.Data;

    [GlobalSetup]
    public void Setup()
    {
        WordList = EnUsTestData.CreateDictionary();
    }

    [Benchmark(Description = "Suggest words", Baseline = true)]
    public void SuggestAssortedWords()
    {
        foreach (var word in WordData.SmallSampling)
        {
            _ = WordList.Suggest(word);
        }
    }
}
