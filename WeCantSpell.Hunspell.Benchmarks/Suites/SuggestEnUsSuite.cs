using BenchmarkDotNet.Attributes;
using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.Suites;

[SimpleJob(id: "Suggest en-US")]
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
