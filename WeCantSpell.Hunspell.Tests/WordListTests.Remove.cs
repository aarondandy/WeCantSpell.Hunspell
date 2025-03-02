using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public partial class WordListTests
{
    public class Remove : WordListTests
    {
        [Fact]
        public void remove_root_from_empty_does_nothing()
        {
            var wordList = new WordList.Builder().Build();

            wordList.Remove("not-found").ShouldBe(0);

            wordList.RootCount.ShouldBe(0);
        }

        [Fact]
        public void can_remove_root_with_single_detail()
        {
            var wordList = new WordList.Builder().Build();
            wordList.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

            wordList.Remove("word").ShouldBe(1);

            wordList.RootCount.ShouldBe(0);
        }

        [Fact]
        public void can_remove_root_with_multiple_detailst()
        {
            var wordList = new WordList.Builder().Build();
            wordList.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            wordList.Add("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            wordList.Remove("word").ShouldBe(2);

            wordList.RootCount.ShouldBe(0);
        }

        [Fact]
        public void remove_details_from_empty_does_nothing()
        {
            var wordList = new WordList.Builder().Build();

            wordList.Remove("not-found", new WordEntryDetail()).ShouldBeFalse();

            wordList.RootCount.ShouldBe(0);
        }

        [Fact]
        public void can_remove_single_detail()
        {
            var wordList = new WordList.Builder().Build();
            wordList.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

            wordList.Remove("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            wordList.RootCount.ShouldBe(0);
        }

        [Fact]
        public void can_remove_each_detail_when_root_has_many()
        {
            var wordList = new WordList.Builder().Build();
            wordList.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            wordList.Add("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            wordList.Add("word", FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            wordList.Add("word", FlagSet.Create('C'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            wordList.Remove("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue("can remove first");
            wordList.Remove("word", FlagSet.Create('C'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue("can remove last");
            wordList.Remove("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            wordList.Remove("word", FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            wordList.RootCount.ShouldBe(0);
        }
    }
}
