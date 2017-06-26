using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites
{
    [SimpleJob]
    public class EnUsWordListCheckSuite
    {
        private WordList WordList;

        private CategorizedWordData WordData;

        [GlobalSetup]
        public void Setup()
        {
            WordList = WordList.CreateFromFiles(Path.Combine(DataFilePaths.TestFilesFolderPath, "English (American).dic"));

            WordData = CategorizedWordData.Create(
                CategorizedWordData.GetAssortedEnUsWords(),
                isCorrect: WordList.Check,
                isRoot: WordList.ContainsEntriesForRootWord);
        }


        [Benchmark(Description = "Check an assortment of words")]
        public void CheckAllWords()
        {
            foreach (var word in WordData.AllWords)
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check root words", Baseline = true)]
        public void CheckRootWords()
        {
            foreach (var word in WordData.RootWords)
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check correct words")]
        public void CheckCorrectWords()
        {
            foreach (var word in WordData.CorrectWords)
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check wrong words")]
        public void CheckWrongWords()
        {
            foreach (var word in WordData.WrongWords)
            {
                var result = WordList.Check(word);
            }
        }
    }
}
