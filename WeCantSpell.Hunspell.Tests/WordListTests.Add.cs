using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public partial class WordListTests
{
    public class Add : WordListTests
    {
        [Fact]
        public void can_add_to_empty_word_list_and_check()
        {
            var word = "word";
            var wordList = new WordList.Builder().Build();
            wordList.Check(word, TestCancellation).ShouldBeFalse();

            wordList.Add(word);

            wordList.Check(word, TestCancellation).ShouldBeTrue();
        }

        [Fact]
        public void adding_twice_doesnt_add_duplicates()
        {
            var word = "word";
            var wordList = new WordList.Builder().Build();

            wordList.Add(word).ShouldBeTrue();
            wordList.Add(word).ShouldBeFalse();
        }

        [Fact]
        public void adding_twice_with_same_details_doesnt_add_duplicates()
        {
            var word = "word";
            var wordList = new WordList.Builder().Build();

            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeTrue();
            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeFalse();
        }

        [Fact]
        public void adding_twice_with_different_details_adds_duplicates()
        {
            var word = "word";
            var wordList = new WordList.Builder().Build();

            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeTrue();
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeTrue();
        }

        [Fact]
        public void adding_ngram_restricted_twice_with_different_details_adds_restricted_duplicates()
        {
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                ForbiddenWord = (FlagValue)'A',
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();

            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeTrue();
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None)).ShouldBeTrue();
        }

        [Fact]
        public void can_add_to_empty_word_list_and_suggest()
        {
            var given = "ord";
            var word = "word";
            var wordList = new WordList.Builder().Build();
            wordList.Suggest(given, TestCancellation).ShouldNotContain(word);
            wordList.Add(word);

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain(word);
        }

        [Fact]
        public async Task can_add_to_en_us_word_list_for_check()
        {
            var word = "qwertyuiop";
            var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", TestCancellation);
            wordList.Check(word, TestCancellation).ShouldBeFalse();
            wordList.Add(word);

            var actual = wordList.Check(word, TestCancellation);

            actual.ShouldBeTrue();
        }

        [Fact]
        public async Task can_add_to_en_us_word_list_for_suggest()
        {
            var given = "qwertyuio";
            var word = "qwertyuiop";
            var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", TestCancellation);
            wordList.Suggest(given, TestCancellation).ShouldNotContain(word);
            wordList.Add(word);

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain(word);
        }

        [Fact]
        public async Task added_forbidden_word_is_not_suggested()
        {
            var given = "hecin";
            var word = "heckin";
            var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", TestCancellation);
            wordList.Check(word, TestCancellation).ShouldBeFalse();
            wordList.Affix.ForbiddenWord.HasValue.ShouldBeTrue();
            wordList.Add(word, new(FlagSet.Create(wordList.Affix.ForbiddenWord), MorphSet.Empty, WordEntryOptions.None));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldNotContain(word);
        }

        [Fact]
        public async Task added_nosuggest_word_is_not_suggested()
        {
            var given = "tarnaton";
            var word = "tarnation";
            var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", TestCancellation);
            wordList.Check(word, TestCancellation).ShouldBeFalse();
            wordList.Affix.NoSuggest.HasValue.ShouldBeTrue();
            wordList.Add(word, new(FlagSet.Create(wordList.Affix.NoSuggest), MorphSet.Empty, WordEntryOptions.None));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldNotContain(word);
        }
    }
}
