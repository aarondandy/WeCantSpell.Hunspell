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

            [Theory, MemberData("AllGoodFilePaths")]
            public async Task can_find_good_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var dictionary = await DictionaryReader.ReadFileAsync(dictionaryFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeTrue();
            }

            [Theory, MemberData("AllWrongFilePaths")]
            public async Task cant_find_wrong_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var dictionary = await DictionaryReader.ReadFileAsync(dictionaryFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeFalse();
            }

            public static IEnumerable<object[]> AllGoodFilePaths => GetWordCheckParameters("*.good");

            public static IEnumerable<object[]> AllWrongFilePaths => GetWordCheckParameters("*.wrong");

            protected static IEnumerable<string[]> GetWordCheckParameters(string searchPattern)
            {
                return Directory.GetFiles("files/", searchPattern)
                    .SelectMany(wordFilePath =>
                    {
                        var dictionaryPath = Path.ChangeExtension(wordFilePath, "dic");
                        return File.ReadLines(wordFilePath, Encoding.UTF8)
                            .Select(word => new string[] { dictionaryPath, word });
                    });
            }
        }
    }
}
