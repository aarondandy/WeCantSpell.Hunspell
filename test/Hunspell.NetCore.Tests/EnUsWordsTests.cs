using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class EnUsWordsTests
    {
        [Fact]
        public async Task most_wrong_words_are_not_found()
        {
            var words = await LoadMistakesAsync().ConfigureAwait(false);
            var spell = await LoadEnUsAsync().ConfigureAwait(false);

            var negativeCases = new List<CommonSpellingMistake>();
            foreach(var word in words)
            {
                if(spell.Check(word.Wrong))
                {
                    negativeCases.Add(word);
                }
            }

            negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
        }

        [Fact]
        public async Task most_correct_words_are_found()
        {
            var words = await LoadMistakesAsync().ConfigureAwait(false);
            var spell = await LoadEnUsAsync().ConfigureAwait(false);

            var negativeCases = new List<CommonSpellingMistake>();
            foreach (var word in words)
            {
                if (!spell.Check(word.Correct))
                {
                    negativeCases.Add(word);
                }
            }

            negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
        }

        [Fact]
        public async Task most_correct_words_are_suggested_for_wrong_words()
        {
            var words = await LoadMistakesAsync().ConfigureAwait(false);
            words = words.Where((_,i) => i % 11 == 0).Take(10).ToList();
            var spell = await LoadEnUsAsync().ConfigureAwait(false);

            var negativeCases = new List<CommonSpellingMistake>();
            foreach (var word in words)
            {
                if (spell.Check(word.Correct) && !spell.Check(word.Wrong))
                {
                    var suggestions = spell.Suggest(word.Wrong);
                    if (!suggestions.Contains(word.Correct))
                    {
                        negativeCases.Add(word);
                    }
                }
            }

            negativeCases.Count.Should().BeLessOrEqualTo(words.Count / 10);
        }

        protected Task<HunspellDictionary> LoadEnUsAsync()
        {
            return HunspellDictionary.FromFileAsync("files/English (American).dic");
        }

        protected async Task<List<CommonSpellingMistake>> LoadMistakesAsync()
        {
            var results = new List<CommonSpellingMistake>();
            using (var fileReader = new StreamReader("files/List_of_common_misspellings.txt", Encoding.UTF8, true))
            {
                string line;
                while ((line = await fileReader.ReadLineAsync().ConfigureAwait(false)) != null)
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
            }

            return results;
        }

        protected struct CommonSpellingMistake
        {
            public string Wrong;

            public string Correct;
        }
    }
}
