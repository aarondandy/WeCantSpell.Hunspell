using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public partial class WordListTests
{
    public class Builder : WordListTests
    {
        [Fact]
        public void can_handle_add_for_ignore_chars()
        {
            var affix = new AffixConfig.Builder()
            {
                IgnoredChars = CharacterSet.Create("xyz")
            }.Build();
            var builder = new WordList.Builder(affix);

            builder.Add("abcxyz");

            builder.Build().Check("abc", TestCancellation).ShouldBeTrue();
        }

        [Fact]
        public async Task chan_handle_add_for_complex_prefixes()
        {
            var affix = await AffixReader.ReadFileAsync("files/alias3.aff", TestCancellation);
            var builder = new WordList.Builder(affix);

            builder.Add("abc");

            builder.Build().Check("abc", TestCancellation).ShouldBeTrue();
        }

        [Fact]
        public void remove_root_from_empty_does_nothing()
        {
            var builder = new WordList.Builder();

            builder.Remove("not-found").ShouldBe(0);
        }

        [Fact]
        public void can_remove_root_with_single_detail()
        {
            var builder = new WordList.Builder();
            builder.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

            builder.Remove("word").ShouldBe(1);
        }

        [Fact]
        public void can_remove_root_with_multiple_detailst()
        {
            var builder = new WordList.Builder();
            builder.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            builder.Add("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            builder.Remove("word").ShouldBe(2);
        }

        [Fact]
        public void remove_details_from_empty_does_nothing()
        {
            var builder = new WordList.Builder();

            builder.Remove("not-found", new WordEntryDetail()).ShouldBeFalse();
        }

        [Fact]
        public void can_remove_single_detail()
        {
            var builder = new WordList.Builder();
            builder.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

            builder.Remove("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
        }

        [Fact]
        public void can_remove_each_detail_when_root_has_many()
        {
            var builder = new WordList.Builder();
            builder.Add("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            builder.Add("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            builder.Add("word", FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            builder.Add("word", FlagSet.Create('C'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();

            builder.Remove("word", FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue("can remove first");
            builder.Remove("word", FlagSet.Create('C'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue("can remove last");
            builder.Remove("word", FlagSet.Create('A'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
            builder.Remove("word", FlagSet.Create('B'), MorphSet.Empty, WordEntryOptions.None).ShouldBeTrue();
        }
    }
}
