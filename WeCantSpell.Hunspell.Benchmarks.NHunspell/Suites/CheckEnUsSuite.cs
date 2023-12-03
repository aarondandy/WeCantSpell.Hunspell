using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.NHunspell.Suites;

[SimpleJob(id: "Check en-US")]
[MinColumn, MeanColumn, MedianColumn]
public class CheckEnUsSuite
{
    private CategorizedWordData WordData => EnUsTestData.Data;

    private WordList _dictionary;
    private global::NHunspell.Hunspell _dictionaryNHunspell;

    [GlobalSetup]
    public void Setup()
    {
        _dictionary = EnUsTestData.CreateDictionary();
        _dictionaryNHunspell = new global::NHunspell.Hunspell(EnUsTestData.FilePathAff, EnUsTestData.FilePathDic);
    }

    [Benchmark(Description = "Check words: WeCantSpell", Baseline = true)]
    public void All_WeCantSpell()
    {
        foreach (var word in WordData.AllWords)
        {
            _ = _dictionary.Check(word);
        }
    }

    [Benchmark(Description = "Check words: NHunspell")]
    public void All_NHunspell()
    {

        foreach (var word in WordData.AllWords)
        {
            _ = _dictionaryNHunspell.Spell(word);
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _dictionaryNHunspell?.Dispose();
    }
}
