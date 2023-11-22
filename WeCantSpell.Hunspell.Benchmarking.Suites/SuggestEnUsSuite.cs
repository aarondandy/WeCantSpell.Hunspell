using System.Collections.Generic;
using System.Linq;

using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Data;
using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.Suites;

[SimpleJob(id: "Suggestions en-US")]
[MinWarmupCount(1), MaxWarmupCount(5)]
[MinIterationCount(1), MaxIterationCount(20), MinInvokeCount(1), IterationTime(250)]
public class SuggestEnUsSuite
{
    protected WordList WordList;
    protected CategorizedWordData Data;
    protected List<string> AssortedWords;

    [GlobalSetup]
    public void Setup()
    {
        WordList = EnUsTestData.CreateDictionary();
        Data = EnUsTestData.Data;
        AssortedWords = Data.AllWords.Where(static (_, i) => i % 100 == 0).ToList();
    }

    [Benchmark(Description = "Suggest assorted", Baseline = true)]
    public void SuggestAssortedWords()
    {
        foreach (var word in AssortedWords)
        {
            _ = WordList.Suggest(word);
        }
    }
}
