using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
