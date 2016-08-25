using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class HunspellTests
    {
        public class Check : HunspellTests
        {
            [Theory]
            [InlineData("aardvark")]
            [InlineData("Aardvark")]
            [InlineData("")]
            [InlineData("-")]
            public void cant_find_words_in_empty_dictioanry(string word)
            {
                var dictionary = new Dictionary.Builder().ToDictionary();
                var hunspell = new Hunspell(dictionary);

                var actual = hunspell.Check(word);

                actual.Should().BeFalse();
            }

            [Theory]
            [InlineData("aardvark", "bat")]
            [InlineData("bat", "aardvark")]
            [InlineData("aardvark", "aardvark")]
            [InlineData("", "aardvark")]
            [InlineData("aardvark", "")]
            public void can_find_words_in_single_word_dictioanry(string searchWord, string dictionaryWord)
            {
                var expected = searchWord == dictionaryWord;
                var dictionary = new Dictionary.Builder
                {
                    Entries = new Dictionary<string, List<DictionaryEntry>>
                    {
                        [dictionaryWord] = new List<DictionaryEntry>
                        {
                            new DictionaryEntry(dictionaryWord, Enumerable.Empty<FlagValue>(), Enumerable.Empty<string>(), DictionaryEntryOptions.None)
                        }
                    }
                }.ToDictionary();
                var hunspell = new Hunspell(dictionary);

                var actual = hunspell.Check(searchWord);

                actual.Should().Be(expected);
            }

            public static IEnumerable<object[]> can_find_good_words_in_dictionary_data =>
                GetAllDataFilePaths("*.good")
                    .SelectMany(ToDictionaryWordTestData);

            [Theory, MemberData(nameof(can_find_good_words_in_dictionary_data))]
            public async Task can_find_good_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var dictionary = await DictionaryReader.ReadFileAsync(dictionaryFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeTrue();
            }

            public static IEnumerable<object[]> cant_find_wrong_words_in_dictionary_data =>
                GetAllDataFilePaths("*.wrong")
                    .SelectMany(ToDictionaryWordTestData);

            [Theory, MemberData(nameof(cant_find_wrong_words_in_dictionary_data))]
            public async Task cant_find_wrong_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var dictionary = await DictionaryReader.ReadFileAsync(dictionaryFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeFalse();
            }

            protected static IEnumerable<string> GetAllDataFilePaths(string searchPattern)
            {
                return Directory.GetFiles("files/", searchPattern);
            }

            private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };

            protected static IEnumerable<object[]> ToDictionaryWordTestData(string wordFilePath)
            {
                var dictionaryPath = Path.ChangeExtension(wordFilePath, "dic");
                var affix = AffixReader.ReadFile(Path.ChangeExtension(wordFilePath, "aff"));

                return StaticEncodingLineReader.ReadLines(wordFilePath, affix.Encoding)
                    .Where(line => line != null)
                    .SelectMany(line => line.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries))
                    .Distinct()
                    .OrderBy(w => w, StringComparer.Ordinal)
                    .Select(line => new object[] { dictionaryPath, line });
            }
        }
    }
}
