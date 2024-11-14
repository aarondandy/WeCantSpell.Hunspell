using System;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.Suites;

[SimpleJob(id: "Suggest en-US", runtimeMoniker: RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(id: "Suggest en-US", runtimeMoniker: RuntimeMoniker.Net90)]
[MinColumn, MeanColumn, MedianColumn]
public class SuggestEnUsSuite
{
    protected WordList WordList;
    protected CategorizedWordData WordData = EnUsTestData.Data;

    [GlobalSetup]
    public void Setup()
    {
        WordList = EnUsTestData.CreateDictionary();
        Console.WriteLine($"Suggest data SmallSampling: {WordData.SmallSuggestSampling.Length}");
    }

    [Benchmark(Description = "Suggest words", Baseline = true)]
    public void SuggestAssortedWords()
    {
        foreach (var word in WordData.SmallSuggestSampling)
        {
            _ = WordList.Suggest(word);
        }
    }
}
