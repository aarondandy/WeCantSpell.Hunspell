using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class WordListTests
{
    public class Constructors : WordListTests
    {
        [Fact]
        public void contains_words_when_constructed_from_list()
        {
            var words = "The quick brown fox jumps over the lazy dog".Split(' ');

            var wordList = WordList.CreateFromWords(words);

            foreach (var word in words)
            {
                var entry = wordList[word];
                entry.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void can_check_words_when_constructed_from_list()
        {
            var words = "The quick brown fox jumps over the lazy dog".Split(' ');

            var wordList = WordList.CreateFromWords(words);

            foreach(var word in words)
            {
                wordList.Check(word).Should().BeTrue();
            }
            wordList.Check("missing").Should().BeFalse();
            wordList.Check("Wot?").Should().BeFalse();
        }

        [Theory]
        [InlineData("Teh", "The")]
        [InlineData("bworn", "brown")]
        [InlineData("jumsp", "jumps")]
        [InlineData("eht", "the")]
        [InlineData("odg", "dog")]
        public void can_suggest_typos_when_constructed_from_list(string given, string expected)
        {
            var words = "The quick brown fox jumps over the lazy dog".Split(' ');
            var wordList = WordList.CreateFromWords(words);

            var suggestions = wordList.Suggest(given);

            wordList.Check(given).Should().BeFalse();
            suggestions.Should().Contain(expected);
        }

        [Fact]
        public void wordlist_with_blank_word_entry_does_not_crash_suggest()
        {
            var wordList = WordList.CreateFromWords(new[] { "" });

            var suggestions = wordList.Suggest("test");

            suggestions.Should().BeEmpty();
        }
    }
}
