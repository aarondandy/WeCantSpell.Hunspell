using System.IO;

using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites;

[SimpleJob]
public class EnUsWordListCheckSuite
{
    private WordList _wordList;
    private CategorizedWordData _wordData;

    [GlobalSetup]
    public void Setup()
    {
        _wordList = WordList.CreateFromFiles(Path.Combine(DataFilePaths.TestFilesFolderPath, "English (American).dic"));

        _wordData = CategorizedWordData.Create(
            CategorizedWordData.GetAssortedEnUsWords(),
            isCorrect: _wordList.Check,
            isRoot: _wordList.ContainsEntriesForRootWord);
    }


    [Benchmark(Description = "Check an assortment of words")]
    public void CheckAllWords()
    {
        foreach (var word in _wordData.AllWords)
        {
            _ = _wordList.Check(word);
        }
    }

    [Benchmark(Description = "Check root words", Baseline = true)]
    public void CheckRootWords()
    {
        foreach (var word in _wordData.RootWords)
        {
            _ = _wordList.Check(word);
        }
    }

    [Benchmark(Description = "Check correct words")]
    public void CheckCorrectWords()
    {
        foreach (var word in _wordData.CorrectWords)
        {
            _ = _wordList.Check(word);
        }
    }

    [Benchmark(Description = "Check wrong words")]
    public void CheckWrongWords()
    {
        foreach (var word in _wordData.WrongWords)
        {
            _ = _wordList.Check(word);
        }
    }
}
