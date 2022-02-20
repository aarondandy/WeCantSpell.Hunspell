using System.IO;
using System.Linq;

using BenchmarkDotNet.Attributes;

using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites;

[SimpleJob]
public class EnUsWordListSuggestSuite
{
    private const int MaxWords = 5;

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


    [Benchmark(Description = "Suggest an assortment of words")]
    public void CheckAllWords()
    {
        foreach (var word in _wordData.AllWords.Take(MaxWords))
        {
            _ = _wordList.Suggest(word);
        }
    }

    [Benchmark(Description = "Suggest root words", Baseline = true)]
    public void CheckRootWords()
    {
        foreach (var word in _wordData.RootWords.Take(MaxWords))
        {
            _ = _wordList.Suggest(word);
        }
    }

    [Benchmark(Description = "Suggest correct words")]
    public void CheckCorrectWords()
    {
        foreach (var word in _wordData.CorrectWords.Take(MaxWords))
        {
            _ = _wordList.Suggest(word);
        }
    }

    [Benchmark(Description = "Suggest wrong words")]
    public void CheckWrongWords()
    {
        foreach (var word in _wordData.WrongWords.Take(MaxWords))
        {
            _ = _wordList.Suggest(word);
        }
    }
}
