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
    }
}
