using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class EnUsWordsTests
{
    [Fact]
    public async Task most_wrong_words_are_not_found()
    {
        var (words, spell) = await LoadMistakeTestData();
        var wrongCount = 0;

        Parallel.ForEach(
            words,
            new() { CancellationToken = TestContext.Current.CancellationToken },
            word =>
            {
                if (spell.Check(word.Wrong))
                {
                    Interlocked.Increment(ref wrongCount);
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(words.Count / 10);
    }

    [Fact]
    public async Task most_correct_words_are_found()
    {
        var (words, spell) = await LoadMistakeTestData();
        var wrongCount = 0;

        Parallel.ForEach(
            words,
            new() { CancellationToken = TestContext.Current.CancellationToken },
            word =>
            {
                if (!spell.Check(word.Correct))
                {
                    Interlocked.Increment(ref wrongCount);
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(words.Count / 10);
    }

    [Fact]
    public async Task most_correct_words_are_suggested_for_wrong_words()
    {
        var (words, spell) = await LoadMistakeTestData();
        words = words.Where(static (_,i) => i % 11 == 0).Take(10).ToList();
        var wrongCount = 0;

        Parallel.ForEach(
            words,
            new() { CancellationToken = TestContext.Current.CancellationToken },
            word =>
            {
                if (spell.Check(word.Correct) && !spell.Check(word.Wrong))
                {
                    var suggestions = spell.Suggest(word.Wrong, new QueryOptions()
                    {
                        TimeLimitSuggestGlobal = TimeSpan.FromSeconds(10)
                    });

                    if (!suggestions.Contains(word.Correct))
                    {
                        Interlocked.Increment(ref wrongCount);
                    }
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(words.Count / 10);
    }

    protected static async Task<(List<CommonSpellingMistake>, WordList)> LoadMistakeTestData()
    {
        var wordsTask = loadMistakesAsync();
        var spellTask = WordList.CreateFromFilesAsync("files/English (American).dic");
        return (await wordsTask.ConfigureAwait(false), await spellTask);

        static async Task<List<CommonSpellingMistake>> loadMistakesAsync()
        {
            // NOTE: not all of these words may be wrong with respect to the dictionary used
            var results = new List<CommonSpellingMistake>();
            using var fileStream = new FileStream("files/List_of_common_misspellings.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            using var fileReader = new StreamReader(fileStream, Encoding.UTF8, true);

            string line;
            while ((line = await fileReader.ReadLineAsync().ConfigureAwait(false)) is not null)
            {
                if (string.IsNullOrWhiteSpace(line) || line[0] is '#' or '[')
                {
                    continue;
                }

                var parts = line.Split(null);
                if (parts.Length != 2)
                {
                    continue;
                }

                results.Add(new CommonSpellingMistake
                {
                    Wrong = parts[0],
                    Correct = parts[1]
                });
            }

            return results;
        }
    }

    protected struct CommonSpellingMistake
    {
        public string Wrong;
        public string Correct;
    }
}
