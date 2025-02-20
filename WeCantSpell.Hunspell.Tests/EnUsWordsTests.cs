using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class EnUsWordsTests
{
    [Fact]
    public async Task most_wrong_words_are_not_found()
    {
        var wordsTask = LoadMistakesAsync();
        var spellTask = LoadEnUsAsync();

        await Task.WhenAll(wordsTask, spellTask);

        var words = await wordsTask;
        var spell = await spellTask;

        var negativeCases = new ConcurrentBag<CommonSpellingMistake>();
        Parallel.ForEach(words, word =>
        {
            if (spell.Check(word.Wrong))
            {
                negativeCases.Add(word);
            }
        });

        negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
    }

    [Fact]
    public async Task most_correct_words_are_found()
    {
        var wordsTask = LoadMistakesAsync();
        var spellTask = LoadEnUsAsync();

        await Task.WhenAll(wordsTask, spellTask);

        var words = await wordsTask;
        var spell = await spellTask;

        var negativeCases = new ConcurrentBag<CommonSpellingMistake>();
        Parallel.ForEach(words, word =>
        {
            if (!spell.Check(word.Correct))
            {
                negativeCases.Add(word);
            }
        });

        negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
    }

    [Fact]
    public async Task most_correct_words_are_suggested_for_wrong_words()
    {
        var wordsTask = LoadMistakesAsync();
        var spellTask = LoadEnUsAsync();

        await Task.WhenAll(wordsTask, spellTask);

        var words = (await wordsTask).Where(static (_,i) => i % 11 == 0).Take(10).ToList();
        var spell = await spellTask;

        var negativeCases = new ConcurrentBag<CommonSpellingMistake>();
        Parallel.ForEach(words, word =>
        {
            if (spell.Check(word.Correct) && !spell.Check(word.Wrong))
            {
                var suggestions = spell.Suggest(word.Wrong, new QueryOptions()
                {
                    TimeLimitSuggestGlobal = TimeSpan.FromSeconds(10)
                });
                if (!suggestions.Contains(word.Correct))
                {
                    negativeCases.Add(word);
                }
            }
        });

        negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
    }

    protected Task<WordList> LoadEnUsAsync() =>
        WordList.CreateFromFilesAsync("files/English (American).dic");

    protected async Task<List<CommonSpellingMistake>> LoadMistakesAsync()
    {
        // NOTE: not all of these words may be wrong with respect to the dictionary used
        var results = new List<CommonSpellingMistake>();
        using var fileStream = new FileStream("files/List_of_common_misspellings.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var fileReader = new StreamReader(fileStream, Encoding.UTF8, true);

        string line;
        while ((line = await fileReader.ReadLineAsync().ConfigureAwait(false)) is not null)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith("["))
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

    protected struct CommonSpellingMistake
    {
        public string Wrong;

        public string Correct;
    }
}
