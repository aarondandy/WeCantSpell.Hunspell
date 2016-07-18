using System.Linq;
using FluentAssertions;
using Xunit;
using System;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Tests
{
    public class AffixFileReaderTests
    {
        public class Constructors
        {
            [Fact]
            public void null_line_reader_throws()
            {
                IAffixFileLineReader lineReader = null;

                Action act = () => new AffixFileReader(lineReader);

                act.ShouldThrow<ArgumentNullException>();
            }
        }

        public class ReadAsync
        {
            [Fact]
            public async Task can_read_1463589_aff()
            {
                var filePath = @"files/1463589.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(1);
            }

            [Fact]
            public async Task can_read_1463589_utf_aff()
            {
                var filePath = @"files/1463589_utf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("UTF-8");
                actual.MaxNgramSuggestions.Should().Be(1);
            }

            [Fact]
            public async Task can_read_1592880_aff()
            {
                var filePath = @"files/1592880.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("ISO8859-1");

                actual.Suffixes.Should().HaveCount(4);

                var suffixGroup1 = actual.Suffixes[0];
                suffixGroup1.AFlag.Should().Be('N');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup1.Entries.Should().HaveCount(1);
                suffixGroup1.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup1.Entries.Single().Append.Should().Be("n");
                suffixGroup1.Entries.Single().ConditionText.Should().Be(".");

                var suffixGroup2 = actual.Suffixes[1];
                suffixGroup2.AFlag.Should().Be('S');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup2.Entries.Should().HaveCount(1);
                suffixGroup2.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup2.Entries.Single().Append.Should().Be("s");
                suffixGroup2.Entries.Single().ConditionText.Should().Be(".");

                var suffixGroup3 = actual.Suffixes[2];
                suffixGroup3.AFlag.Should().Be('P');
                suffixGroup3.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup3.Entries.Should().HaveCount(1);
                suffixGroup3.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup3.Entries.Single().Append.Should().Be("en");
                suffixGroup3.Entries.Single().ConditionText.Should().Be(".");

                var suffixGroup4 = actual.Suffixes[3];
                suffixGroup4.AFlag.Should().Be('Q');
                suffixGroup4.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup4.Entries.Should().HaveCount(2);
                suffixGroup4.Entries.First().Strip.Should().BeEmpty();
                suffixGroup4.Entries.First().Append.Should().Be("e");
                suffixGroup4.Entries.First().ConditionText.Should().Be(".");
                suffixGroup4.Entries.Last().Strip.Should().BeEmpty();
                suffixGroup4.Entries.Last().Append.Should().Be("en");
                suffixGroup4.Entries.Last().ConditionText.Should().Be(".");

                actual.CompoundEnd.Should().Be('z');
                actual.CompoundPermitFlag.Should().Be('c');
                actual.OnlyInCompound.Should().Be('o');
            }

            [Fact]
            public async Task can_read_1695964_aff()
            {
                var filePath = @"files/1695964.aff";

                var actual = await ReadFileAsync(filePath);

                actual.TryString.Should().Be("esianrtolcdugmphbyfvkwESIANRTOLCDUGMPHBYFVKW");
                actual.MaxNgramSuggestions.Should().Be(0);
                actual.NeedAffix.Should().Be('h');
                actual.Suffixes.Should().HaveCount(2);
                var suffixGroup1 = actual.Suffixes[0];
                suffixGroup1.AFlag.Should().Be('S');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup1.Entries.Should().HaveCount(1);
                suffixGroup1.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup1.Entries.Single().Append.Should().Be("s");
                suffixGroup1.Entries.Single().ConditionText.Should().Be(".");
                var suffixGroup2 = actual.Suffixes[1];
                suffixGroup2.AFlag.Should().Be('e');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup2.Entries.Should().HaveCount(1);
                suffixGroup2.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup2.Entries.Single().Append.Should().Be("e");
                suffixGroup2.Entries.Single().ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_1706659_aff()
            {
                var filePath = @"files/1706659.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("ISO8859-1");
                actual.TryString.Should().Be("esijanrtolcdugmphbyfvkwqxz");
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('A');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.Single().Entries.Should().HaveCount(5);
                actual.Suffixes.Single().Entries.Select(e => e.Append).ShouldBeEquivalentTo(new[]
                {
                    "e",
                    "er",
                    "en",
                    "em",
                    "es"
                });
                actual.Suffixes.Single().Entries.Should().OnlyContain(e => e.Strip == string.Empty);
                actual.Suffixes.Single().Entries.Should().OnlyContain(e => e.ConditionText == ".");

                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().ShouldBeEquivalentTo(new[] { 'v', 'w' });
            }

            [Fact]
            public async Task can_read_1975530_aff()
            {
                var filePath = @"files/1975530.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("UTF-8");
                actual.IgnoredChars.Should().BeEquivalentTo("ٌٍَُِّْـ".ToCharArray());
                actual.Prefixes.Should().HaveCount(1);
                var prefixGroup1 = actual.Prefixes.Single();
                prefixGroup1.AFlag.Should().Be('x');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.None);
                prefixGroup1.Entries.Should().HaveCount(1);
                var prefixEntry = prefixGroup1.Entries.Single();
                prefixEntry.Append.Should().Be("ت");
                prefixEntry.ConditionText.Should().Be("أ[^ي]");
                prefixEntry.Strip.Should().Be("أ");
            }

            [Fact]
            public async Task can_read_2970240_aff()
            {
                var filePath = @"files/2970240.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('c');
                actual.CompoundPatterns.Should().HaveCount(1);
                var pattern = actual.CompoundPatterns.Single();
                pattern.Pattern.Should().Be("le");
                pattern.Pattern2.Should().Be("fi");
            }

            [Fact]
            public async Task can_read_2970242_aff()
            {
                var filePath = @"files/2970242.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundPatterns.Should().HaveCount(1);
                var pattern = actual.CompoundPatterns.Single();
                pattern.Pattern.Should().BeEmpty();
                pattern.Condition.Should().Be('a');
                pattern.Pattern2.Should().BeEmpty();
                pattern.Condition2.Should().Be('b');
                pattern.Pattern3.Should().BeNullOrEmpty();
                actual.CompoundFlag.Should().Be('c');
            }

            [Fact]
            public async Task can_read_2999225_aff()
            {
                var filePath = @"files/2999225.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().ShouldBeEquivalentTo(new[] { 'a', 'b' });
                actual.CompoundBegin.Should().Be('A');
                actual.CompoundEnd.Should().Be('B');
            }

            [Fact]
            public async Task can_read_affixes_aff()
            {
                var filePath = @"files/affixes.aff";

                var actual = await ReadFileAsync(filePath);

                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be('A');
                actual.Prefixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Single().Entries.Should().HaveCount(1);
                var prefixEntry = actual.Prefixes.Single().Entries.Single();
                prefixEntry.Strip.Should().BeEmpty();
                prefixEntry.Append.Should().Be("re");
                prefixEntry.ConditionText.Should().Be(".");

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('B');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.Single().Entries.Should().HaveCount(2);
                var suffixEntry1 = actual.Suffixes.Single().Entries.First();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("ed");
                suffixEntry1.ConditionText.Should().Be("[^y]");
                var suffixEntry2 = actual.Suffixes.Single().Entries.Last();
                suffixEntry2.Strip.Should().Be("y");
                suffixEntry2.Append.Should().Be("ied");
                suffixEntry2.ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_alias_aff()
            {
                var filePath = @"files/alias.aff";

                var actual = await ReadFileAsync(filePath);

                actual.AliasF.Should().HaveCount(2);
                actual.AliasF[0].ShouldBeEquivalentTo(new int[] { 'A', 'B' });
                actual.AliasF[1].ShouldBeEquivalentTo(new int[] { 'A' });
                actual.Suffixes.Should().HaveCount(2);
                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                actual.Suffixes.First().Entries.Single().Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries.Single().Append.Should().Be("x");
                actual.Suffixes.First().Entries.Single().ConditionText.Should().Be(".");
                actual.Suffixes.Last().AFlag.Should().Be('B');
                actual.Suffixes.Last().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
                actual.Suffixes.Last().Entries.Should().HaveCount(1);
                actual.Suffixes.Last().Entries.Single().Strip.Should().BeEmpty();
                actual.Suffixes.Last().Entries.Single().Append.Should().Be("y");
                actual.Suffixes.Last().Entries.Single().ContClass.ShouldBeEquivalentTo(new[] { 'A' });
                actual.Suffixes.Last().Entries.Single().ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_alias2_aff()
            {
                var filePath = @"files/alias2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.AliasF.Should().HaveCount(2);
                actual.AliasF[0].ShouldBeEquivalentTo(new int[] { 'A', 'B' });
                actual.AliasF[1].ShouldBeEquivalentTo(new int[] { 'A' });

                actual.AliasM.Should().HaveCount(3);
                actual.AliasM[0].Should().Be("is:affix_x");
                actual.AliasM[1].Should().Be("ds:affix_y");
                actual.AliasM[2].Should().Be("po:noun xx:other_data");

                actual.Suffixes.Should().HaveCount(2);

                actual.Suffixes[0].AFlag.Should().Be('A');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
                actual.Suffixes[0].Entries.Should().HaveCount(1);
                var suffixEntry1 = actual.Suffixes[0].Entries.Single();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("x");
                suffixEntry1.ConditionText.Should().Be(".");
                suffixEntry1.MorphCode.Should().Be("is:affix_x");

                actual.Suffixes[1].AFlag.Should().Be('B');
                actual.Suffixes[1].Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
                actual.Suffixes[1].Entries.Should().HaveCount(1);
                var suffixEntry2 = actual.Suffixes[1].Entries.Single();
                suffixEntry2.Strip.Should().BeEmpty();
                suffixEntry2.Append.Should().Be("y");
                suffixEntry2.ContClass.ShouldBeEquivalentTo(new int[] { 'A' });
                suffixEntry2.ConditionText.Should().Be(".");
                suffixEntry2.MorphCode.Should().Be("ds:affix_y");
            }

            [Fact]
            public async Task can_read_sug_aff()
            {
                var filePath = @"files/sug.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().NotBeNull();
                var entry = actual.Replacements.Should().HaveCount(1).And.Subject.Single();
                entry.Pattern.Should().Be("alot");
                entry.Med.Should().Be("a lot");
                actual.KeyString.Should().Be("qwertzuiop|asdfghjkl|yxcvbnm|aq");
                actual.WordChars.Should().BeEquivalentTo(new[] { '.' });
                actual.ForbiddenWord.Should().Be((ushort)'?');
            }

            private async Task<AffixFile> ReadFileAsync(string filePath)
            {
                using (var reader = new AffixFileReader(new AffixUtfStreamLineReader(filePath)))
                {
                    return await reader.GetOrReadAsync();
                }
            }
        }
    }
}
