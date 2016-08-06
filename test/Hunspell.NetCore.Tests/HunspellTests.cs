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
                            new DictionaryEntry(dictionaryWord, Enumerable.Empty<int>(), Enumerable.Empty<string>(), DictionaryEntryOptions.None)
                        }
                    }
                }.ToDictionary();
                var hunspell = new Hunspell(dictionary);

                var actual = hunspell.Check(searchWord);

                actual.Should().Be(expected);
            }

            [Theory, MemberData("AllGoodFilePaths")]
            public async Task can_find_good_words_in_dictionary(string goodWordsFilePath)
            {
                var dicFilePath = Path.ChangeExtension(goodWordsFilePath, "dic");
                var dictionary = await DictionaryReader.ReadFileAsync(dicFilePath);
                var goodWords = File.ReadAllLines(goodWordsFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResults = Array.ConvertAll(goodWords, hunspell.Check);
                checkResults.Should().OnlyContain(b => b);
            }

            [Theory, MemberData("AllWrongFilePaths")]
            public async Task cant_find_wrong_words_in_dictionary(string wrongWordsFilePath)
            {
                var dicFilePath = Path.ChangeExtension(wrongWordsFilePath, "dic");
                var dictionary = await DictionaryReader.ReadFileAsync(dicFilePath);
                var wrongWords = File.ReadAllLines(wrongWordsFilePath);
                var hunspell = new Hunspell(dictionary);

                var checkResults = Array.ConvertAll(wrongWords, hunspell.Check);
                checkResults.Should().OnlyContain(b => !b);
            }

            public static IEnumerable<object[]> AllGoodFilePaths =>
                Array.ConvertAll(Directory.GetFiles("files/", "*.good"), filePath => new object[] { filePath });

            public static IEnumerable<object[]> AllWrongFilePaths =>
                Array.ConvertAll(Directory.GetFiles("files/", "*.wrong"), filePath => new object[] { filePath });
        }
    }
}
