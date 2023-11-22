using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Data;
using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell.Suites;

[SimpleJob]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
[MinWarmupCount(1), MaxWarmupCount(5)]
[MinIterationCount(1), MaxIterationCount(20), MinInvokeCount(1), IterationTime(1000)]
public class SuggestSuite
{
    private CategorizedWordData WordData => EnUsTestData.Data;

    private List<string> _testWords;
    private WordList _dictionary;
    private global::NHunspell.Hunspell _dictionaryNHunspell;

    [GlobalSetup]
    public void Setup()
    {
        _testWords = WordData.AllWords.Where(static (_, i) => i % 50 == 0).ToList();
        _dictionary = EnUsTestData.CreateDictionary();
        _dictionaryNHunspell = new global::NHunspell.Hunspell(EnUsTestData.FilePathAff, EnUsTestData.FilePathDic);
    }

    [Benchmark(Description = "Suggest words: WeCantSpell", Baseline = true)]
    public void All_WeCantSpell()
    {
        foreach (var word in _testWords)
        {
            _ = _dictionary.Suggest(word);
        }
    }

    [Benchmark(Description = "Suggest words: NHunspell")]
    public void All_NHunspell()
    {

        foreach (var word in _testWords)
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
