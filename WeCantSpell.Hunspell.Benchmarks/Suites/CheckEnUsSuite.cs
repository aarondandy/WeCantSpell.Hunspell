﻿using System.Collections.Generic;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using WeCantSpell.Hunspell.Benchmarks.Helpers;

namespace WeCantSpell.Hunspell.Benchmarks.Suites;

[SimpleJob(id: "Check en-US", runtimeMoniker: RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(id: "Check en-US", runtimeMoniker: RuntimeMoniker.Net60)]
[MinColumn, MeanColumn, MedianColumn]
public class CheckEnUsSuite
{
    protected WordList WordList;

    [GlobalSetup]
    public void Setup()
    {
        WordList = EnUsTestData.CreateDictionary();
    }

    public IEnumerable<object[]> CheckData()
    {
        var wordData = EnUsTestData.Data;
        yield return new object[] { "All", wordData.AllWords.ToArray() };
        yield return new object[] { "Roots", wordData.RootWords.ToArray() };
        yield return new object[] { "Correct", wordData.CorrectWords.ToArray() };
        yield return new object[] { "Wrong", wordData.WrongWords.ToArray() };
    }

    [Benchmark(Description = "Check words")]
    [ArgumentsSource(nameof(CheckData))]
    public void CheckWords(string set, string[] words)
    {
        foreach (var word in words)
        {
            _ = WordList.Check(word);
        }
    }
}
