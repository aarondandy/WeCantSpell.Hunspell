using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.NHunspell.Suites;

[SimpleJob(id: "Suggest en-US")]
[MinColumn, MeanColumn, MedianColumn]
public class SuggestEnUsSuite
{
    protected CategorizedWordData WordData = EnUsTestData.Data;

    private WordList _dictionary;
    private global::NHunspell.Hunspell _dictionaryNHunspell;

    [GlobalSetup]
    public void Setup()
    {
        _dictionary = EnUsTestData.CreateDictionary();
        _dictionaryNHunspell = new global::NHunspell.Hunspell(EnUsTestData.FilePathAff, EnUsTestData.FilePathDic);
    }

    [Benchmark(Description = "Suggest words: WeCantSpell", Baseline = true)]
    public void All_WeCantSpell()
    {
        foreach (var word in WordData.SmallSuggestSampling)
        {
            _ = _dictionary.Suggest(word);
        }
    }

    [Benchmark(Description = "Suggest words: NHunspell")]
    public void All_NHunspell()
    {

        foreach (var word in WordData.SmallSuggestSampling)
        {
            _ = _dictionaryNHunspell.Suggest(word);
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _dictionaryNHunspell?.Dispose();
    }
}
