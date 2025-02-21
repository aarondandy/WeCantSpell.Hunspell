using System.Threading;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class WordListTests
{
    static CancellationToken TestCancellation => TestContext.Current.CancellationToken;

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
                entry.ShouldNotBeNull();
                entry.ShouldNotBeEmpty();
            }
        }

        [Fact]
        public void can_check_words_when_constructed_from_list()
        {
            var words = "The quick brown fox jumps over the lazy dog".Split(' ');

            var wordList = WordList.CreateFromWords(words);

            foreach(var word in words)
            {
                wordList.Check(word, TestCancellation).ShouldBeTrue();
            }
            wordList.Check("missing", TestCancellation).ShouldBeFalse();
            wordList.Check("Wot?", TestCancellation).ShouldBeFalse();
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

            var suggestions = wordList.Suggest(given, TestCancellation);

            wordList.Check(given, TestCancellation).ShouldBeFalse();
            suggestions.ShouldContain(expected);
        }

        [Fact]
        public void wordlist_with_blank_word_entry_does_not_crash_suggest()
        {
            var wordList = WordList.CreateFromWords([""]);

            var suggestions = wordList.Suggest("test", TestCancellation);

            suggestions.ShouldBeEmpty();
        }
    }

    public class CheckTests : WordListTests
    {
        [Fact]
        public void can_check_with_appended_special_chars()
        {
            var wordList = WordList.CreateFromWords(["Word"]);

            var actual = wordList.Check("  Word..", TestCancellation);

            actual.ShouldBeTrue();
        }
    }
}
