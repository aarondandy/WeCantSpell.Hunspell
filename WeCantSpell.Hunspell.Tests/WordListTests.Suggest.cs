using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public partial class WordListTests
{
    public class Suggest : WordListTests
    {
        [Fact]
        public void can_ngram_suggest_when_all_word_detail_not_restricted()
        {
            var given = "ord";
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.AliasM));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain("word");
        }

        [Fact]
        public void can_ngram_suggest_when_first_word_detail_restricted()
        {
            var given = "ord";
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain("word");
        }

        [Fact]
        public void can_ngram_suggest_when_last_word_detail_restricted()
        {
            var given = "ord";
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain("word");
        }

        [Fact]
        public void can_ngram_suggest_when_half_details_restricted_group_1()
        {
            var given = "ord";
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                ForbiddenWord = (FlagValue)'A',
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.AliasM));
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.AliasM));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.InitCap));
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.InitCap));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain("word");
        }

        [Fact]
        public void can_ngram_suggest_when_half_details_restricted_group_2()
        {
            var given = "ord";
            var word = "word";
            var affix = new AffixConfig.Builder()
            {
                ForbiddenWord = (FlagValue)'A',
                NoSuggest = (FlagValue)'B',
            }.Build();
            var wordList = new WordList.Builder(affix).Build();
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.AliasM));
            wordList.Add(word, new(FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None));
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.AliasM));
            wordList.Add(word, new(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.InitCap));
            wordList.Add(word, new(FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.InitCap));

            var actual = wordList.Suggest(given, TestCancellation);

            actual.ShouldContain("word");
        }
    }
}
