using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.Suites;

[SimpleJob(id: "Suggest en-US", runtimeMoniker: RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(id: "Suggest en-US", runtimeMoniker: RuntimeMoniker.Net60)]
[MinColumn, MeanColumn, MedianColumn]
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
