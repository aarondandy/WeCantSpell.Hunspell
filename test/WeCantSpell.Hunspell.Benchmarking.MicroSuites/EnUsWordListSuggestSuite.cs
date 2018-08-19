using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites
{
    [SimpleJob]
    public class EnUsWordListSuggestSuite
    {
        private WordList WordList;

        private CategorizedWordData WordData;

        private const int MaxWords = 5;

        [GlobalSetup]
        public void Setup()
        {
            WordList = WordList.CreateFromFiles(Path.Combine(DataFilePaths.TestFilesFolderPath, "English (American).dic"));

            WordData = CategorizedWordData.Create(
                CategorizedWordData.GetAssortedEnUsWords(),
                isCorrect: WordList.Check,
                isRoot: WordList.ContainsEntriesForRootWord);
        }


        [Benchmark(Description = "Suggest an assortment of words")]
        public void CheckAllWords()
        {
            foreach (var word in WordData.AllWords.Take(MaxWords))
            {
                var result = WordList.Suggest(word);
            }
        }

        [Benchmark(Description = "Suggest root words", Baseline = true)]
        public void CheckRootWords()
        {
            foreach (var word in WordData.RootWords.Take(MaxWords))
            {
                var result = WordList.Suggest(word);
            }
        }

        [Benchmark(Description = "Suggest correct words")]
        public void CheckCorrectWords()
        {
            foreach (var word in WordData.CorrectWords.Take(MaxWords))
            {
                var result = WordList.Suggest(word);
            }
        }

        [Benchmark(Description = "Suggest wrong words")]
        public void CheckWrongWords()
        {
            foreach (var word in WordData.WrongWords.Take(MaxWords))
            {
                var result = WordList.Suggest(word);
            }
        }
    }
}
