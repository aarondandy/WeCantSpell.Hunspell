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
            public async Task can_read_alias3_aff()
            {
                var filePath = @"files/alias3.aff";

                var actual = await ReadFileAsync(filePath);

                actual.ComplexPrefixes.Should().BeTrue();
                actual.WordChars.Should().BeEquivalentTo(new[] { '_' });
                actual.AliasM.Should().HaveCount(4);
                actual.AliasM.ShouldBeEquivalentTo(new[]
                {
                    Reversed(@"affix_1/"),
                    Reversed(@"affix_2/"),
                    Reversed(@"/suffix_1"),
                    Reversed(@"[stem_1]")
                });

                actual.Prefixes.Should().HaveCount(2);
                var prefixGroup1 = actual.Prefixes[0];
                prefixGroup1.AFlag.Should().Be('A');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                prefixGroup1.Entries.Should().HaveCount(1);
                var prefixEntry1 = prefixGroup1.Entries.Single();
                prefixEntry1.Strip.Should().BeEmpty();
                prefixEntry1.Append.Should().Be("ket");
                prefixEntry1.ConditionText.Should().Be(".");
                prefixEntry1.MorphCode.Should().Be(Reversed(@"affix_1/"));
                var prefixGroup2 = actual.Prefixes[1];
                prefixGroup2.AFlag.Should().Be('B');
                prefixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                prefixGroup2.Entries.Should().HaveCount(1);
                var prefixEntry2 = prefixGroup2.Entries.Single();
                prefixEntry2.Strip.Should().BeEmpty();
                prefixEntry2.Append.Should().Be("tem");
                prefixEntry2.ContClass.ShouldBeEquivalentTo(new int[] { 'A' });
                prefixEntry2.ConditionText.Should().Be(".");
                prefixEntry2.MorphCode.Should().Be(Reversed(@"affix_2/"));
                actual.Suffixes.Should().HaveCount(1);
                var suffixGroup1 = actual.Suffixes[0];
                suffixGroup1.AFlag.Should().Be('C');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                suffixGroup1.Entries.Should().HaveCount(1);
                var suffixEntry1 = suffixGroup1.Entries.Single();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("_tset_");
                suffixEntry1.ConditionText.Should().Be(".");
                suffixEntry1.MorphCode.Should().Be(Reversed(@"/suffix_1"));
            }

            [Fact]
            public async Task can_read_allcaps_aff()
            {
                var filePath = @"files/allcaps.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'', '.' });

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('S');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("'s");
                entry1.ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps_utf_aff()
            {
                var filePath = @"files/allcaps_utf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("UTF-8");

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'', '.' });

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('S');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("'s");
                entry1.ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps2_aff()
            {
                var filePath = @"files/allcaps2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.ForbiddenWord.Should().Be('*');

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('s');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("os");
                entry1.ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps3_aff()
            {
                var filePath = @"files/allcaps3.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'' });

                actual.Suffixes.Should().HaveCount(2);
                var suffixGroup1 = actual.Suffixes[0];
                suffixGroup1.AFlag.Should().Be('s');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.None);
                suffixGroup1.Entries.Should().HaveCount(1);
                var entry1 = suffixGroup1.Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("s");
                entry1.ConditionText.Should().Be(".");
                var suffixGroup2 = actual.Suffixes[1];
                suffixGroup2.AFlag.Should().Be('S');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.None);
                suffixGroup2.Entries.Should().HaveCount(1);
                var entry2 = suffixGroup2.Entries.Single();
                entry2.Strip.Should().BeEmpty();
                entry2.Append.Should().Be("\'s");
                entry2.ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_arabic_aff()
            {
                var filePath = @"files/arabic.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("UTF-8");
                actual.TryString.Should().Be("أ");
                actual.IgnoredChars.ShouldBeEquivalentTo(new[] { 'ّ', 'َ', 'ُ', 'ٌ', 'ْ', 'ِ', 'ٍ' });

                actual.Prefixes.Should().HaveCount(1);
                var group1 = actual.Prefixes.Single();
                group1.AFlag.Should().Be((char)('A' | ('a' << 8)));
                group1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                group1.Entries.Should().HaveCount(1);
                var entry1 = group1.Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().BeEmpty();
                entry1.ContClass.ShouldBeEquivalentTo(new[] { 'X', '0' });
                entry1.ConditionText.Should().Be("أ[^ي]");
            }

            [Fact]
            public async Task can_read_base_aff()
            {
                var filePath = @"files/base.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("ISO8859-1");
                actual.WordChars.ShouldBeEquivalentTo(new[] { '.', '\'' });
                actual.TryString.Should().Be("esianrtolcdugmphbyfvkwz'");

                actual.Prefixes.Should().HaveCount(7);
                var prefixGroup1 = actual.Prefixes.First();
                prefixGroup1.AFlag.Should().Be('A');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                prefixGroup1.Entries.Should().HaveCount(1);
                var entry1 = prefixGroup1.Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("re");
                entry1.ConditionText.Should().Be(".");

                actual.Suffixes.Should().HaveCount(16);

                actual.Replacements.Should().HaveCount(88);
                actual.Replacements[0].Pattern.Should().Be("a");
                actual.Replacements[0].OutStrings[0].Should().Be("ei");
                actual.Replacements[87].Pattern.Should().Be("shun");
                actual.Replacements[87].OutStrings[0].Should().Be("cion");
            }

            [Fact]
            public async Task can_read_base_utf_aff()
            {
                var filePath = @"files/base_utf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.TryString.Should().Be("esianrtolcdugmphbyfvkwzESIANRTOLCDUGMPHBYFVKWZ'");
                actual.MaxNgramSuggestions.Should().Be(1);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '.', '\'', '’' });
                actual.Prefixes.Should().HaveCount(7);
                actual.Suffixes.Should().HaveCount(16);
                actual.Replacements.Should().HaveCount(88);
            }

            [Fact]
            public async Task can_read_break_aff()
            {
                var filePath = @"files/break.aff";

                var actual = await ReadFileAsync(filePath);

                actual.BreakTable.ShouldBeEquivalentTo(new[]
                {
                    "-",
                    "–"
                });

                actual.WordChars.ShouldBeEquivalentTo(new[] { '-', '–' });
            }

            [Fact]
            public async Task can_read_breakdefault_aff()
            {
                var filePath = @"files/breakdefault.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.TryString.Should().Be("ot");
            }

            [Fact]
            public async Task can_read_breakoff_aff()
            {
                var filePath = @"files/breakoff.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.TryString.Should().Be("ot");
                actual.BreakTable.Should().HaveCount(0);
            }

            [Fact]
            public async Task can_read_checkcompoundcase_aff()
            {
                var filePath = @"files/checkcompoundcase.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckCompoundCase.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checkcompounddup_aff()
            {
                var filePath = @"files/checkcompounddup.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckCompoundDup.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checkcompoundpattern_aff()
            {
                var filePath = @"files/checkcompoundpattern.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns[0].Pattern.Should().Be("nny");
                actual.CompoundPatterns[0].Pattern2.Should().Be("ny");
                actual.CompoundPatterns[1].Pattern.Should().Be("ssz");
                actual.CompoundPatterns[1].Pattern2.Should().Be("sz");
            }

            [Fact]
            public async Task can_read_checkcompoundpattern2_aff()
            {
                var filePath = @"files/checkcompoundpattern2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns[0].Pattern.Should().Be("o");
                actual.CompoundPatterns[0].Pattern2.Should().Be("b");
                actual.CompoundPatterns[0].Pattern3.Should().Be("z");
                actual.CompoundPatterns[1].Pattern.Should().Be("oo");
                actual.CompoundPatterns[1].Pattern2.Should().Be("ba");
                actual.CompoundPatterns[1].Pattern3.Should().Be("u");
                actual.CompoundMin.Should().Be(1);
            }

            [Fact]
            public async Task can_read_checkcompoundpattern3_aff()
            {
                var filePath = @"files/checkcompoundpattern3.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns[0].Pattern.Should().Be("o");
                actual.CompoundPatterns[0].Condition.Should().Be('X');
                actual.CompoundPatterns[0].Pattern2.Should().Be("b");
                actual.CompoundPatterns[0].Condition2.Should().Be('Y');
                actual.CompoundPatterns[0].Pattern3.Should().Be("z");
                actual.CompoundMin.Should().Be(1);
            }

            [Fact]
            public async Task can_read_checkcompoundpattern4_aff()
            {
                var filePath = @"files/checkcompoundpattern4.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('x');
                actual.CompoundMin.Should().Be(1);
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns[0].Pattern.Should().Be("a");
                actual.CompoundPatterns[0].Condition.Should().Be('A');
                actual.CompoundPatterns[0].Pattern2.Should().Be("u");
                actual.CompoundPatterns[0].Condition2.Should().Be('A');
                actual.CompoundPatterns[0].Pattern3.Should().Be("O");
                actual.CompoundPatterns[1].Pattern.Should().Be("u");
                actual.CompoundPatterns[1].Condition.Should().Be('B');
                actual.CompoundPatterns[1].Pattern2.Should().Be("u");
                actual.CompoundPatterns[1].Condition2.Should().Be('B');
                actual.CompoundPatterns[1].Pattern3.Should().Be("u");
            }

            [Fact]
            public async Task can_read_checkcompoundtriple_aff()
            {
                var filePath = @"files/checkcompoundtriple.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckCompoundTriple.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checksharpsutf_aff()
            {
                var filePath = @"files/checksharpsutf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckSharps.Should().BeTrue();
                actual.WordChars.ShouldBeEquivalentTo(new[] { 'ß', '.' });
                actual.KeepCase.Should().Be('k');
            }

            [Fact]
            public async Task can_read_circumfix_aff()
            {
                var filePath = @"files/circumfix.aff";

                var actual = await ReadFileAsync(filePath);

                actual.Circumfix.Should().Be('X');

                actual.Prefixes.Should().HaveCount(2);
                actual.Prefixes[0].AFlag.Should().Be('A');
                actual.Prefixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes[0].Entries.Should().HaveCount(1);
                var entry1 = actual.Prefixes[0].Entries[0];
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("leg");
                entry1.ContClass.ShouldBeEquivalentTo(new[] { 'X' });
                entry1.ConditionText.Should().Be(".");
                actual.Prefixes[1].AFlag.Should().Be('B');
                actual.Prefixes[1].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes[1].Entries.Should().HaveCount(1);
                var entry2 = actual.Prefixes[1].Entries[0];
                entry2.Strip.Should().BeEmpty();
                entry2.Append.Should().Be("legesleg");
                entry2.ContClass.ShouldBeEquivalentTo(new[] { 'X' });
                entry2.ConditionText.Should().Be(".");

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes[0].AFlag.Should().Be('C');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[0].Entries.Should().HaveCount(3);
                var entry3 = actual.Suffixes[0].Entries[0];
                entry3.Strip.Should().BeEmpty();
                entry3.Append.Should().Be("obb");
                entry3.ConditionText.Should().Be(".");
                entry3.MorphCode.Should().Be("is:COMPARATIVE");
                var entry4 = actual.Suffixes[0].Entries[1];
                entry4.Strip.Should().BeEmpty();
                entry4.Append.Should().Be("obb");
                entry4.ContClass.ShouldBeEquivalentTo(new[] { 'A', 'X' });
                entry4.ConditionText.Should().Be(".");
                entry4.MorphCode.Should().Be("is:SUPERLATIVE");
                var entry5 = actual.Suffixes[0].Entries[2];
                entry5.Strip.Should().BeEmpty();
                entry5.Append.Should().Be("obb");
                entry5.ContClass.ShouldBeEquivalentTo(new[] { 'B', 'X' });
                entry5.ConditionText.Should().Be(".");
                entry5.MorphCode.Should().Be("is:SUPERSUPERLATIVE");
            }

            [Fact]
            public async Task can_read_colons_in_words_aff()
            {
                var filePath = @"files/colons_in_words.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { ':' });
            }

            [Fact]
            public async Task can_read_compoundaffix2_aff()
            {
                var filePath = @"files/compoundaffix2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('X');
                actual.CompoundPermitFlag.Should().Be('Y');
                actual.Prefixes.Should().HaveCount(1);
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_compoundaffix3_aff()
            {
                var filePath = @"files/compoundaffix3.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('X');
                actual.CompoundForbidFlag.Should().Be('Z');
                actual.Prefixes.Should().HaveCount(1);
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_compoundrule2_aff()
            {
                var filePath = @"files/compoundrule2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 'A', '*', 'B', '*', 'C', '*' });
            }

            [Fact]
            public async Task can_read_compoundrule3_aff()
            {
                var filePath = @"files/compoundrule3.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 'A', '?', 'B', '?', 'C', '?' });
            }

            [Fact]
            public async Task can_read_compoundrule4_aff()
            {
                var filePath = @"files/compoundrule4.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be('c');
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 'n', '*', '1', 't' });
                actual.CompoundRules[1].ShouldBeEquivalentTo(new[] { 'n', '*', 'm', 'p' });
            }

            [Fact]
            public async Task can_read_compoundrule5_aff()
            {
                var filePath = @"files/compoundrule5.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules[0].ShouldBeEquivalentTo("N*%?".ToCharArray());
                actual.CompoundRules[1].ShouldBeEquivalentTo("NN*.NN*%?".ToCharArray());
                actual.WordChars.ShouldBeEquivalentTo("0123456789‰.".ToCharArray());
            }

            [Fact]
            public async Task can_read_compoundrule6_aff()
            {
                var filePath = @"files/compoundrule6.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules[0].ShouldBeEquivalentTo("A*A".ToCharArray());
                actual.CompoundRules[1].ShouldBeEquivalentTo("A*AAB*BBBC*C".ToCharArray());
            }

            [Fact]
            public async Task can_read_compoundrule7_aff()
            {
                var filePath = @"files/compoundrule7.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be('c' << 8 | 'c');
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 'n' << 8 | 'n', '*', '1' << 8 | '1', 't' << 8 | 't' });
                actual.CompoundRules[1].ShouldBeEquivalentTo(new[] { 'n' << 8 | 'n', '*', 'm' << 8 | 'm', 'p' << 8 | 'p' });
            }

            [Fact]
            public async Task can_read_compoundrule8_aff()
            {
                var filePath = @"files/compoundrule8.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be(1000);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 1001, '*', 1002, 2001 });
                actual.CompoundRules[1].ShouldBeEquivalentTo(new[] { 1001, '*', 2002, 2000 });
            }

            [Fact]
            public async Task can_read_condition_aff()
            {
                var filePath = @"files/condition.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.Suffixes.Should().HaveCount(4);
                actual.Prefixes.Should().HaveCount(3);
            }

            [Fact]
            public async Task can_read_condition_utf_aff()
            {
                var filePath = @"files/condition_utf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.Suffixes.Should().HaveCount(1);
                actual.Prefixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_digits_in_words_aff()
            {
                var filePath = @"files/digits_in_words.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules[0].ShouldBeEquivalentTo(new[] { 'a', '*', 'b' });
                actual.OnlyInCompound.Should().Be('c');
                actual.WordChars.ShouldBeEquivalentTo("0123456789-".ToCharArray());
            }

            [Fact]
            public async Task can_read_encoding_aff()
            {
                var filePath = @"files/encoding.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("ISO-8859-15");
            }

            [Fact]
            public async Task can_read_flagnum_aff()
            {
                var filePath = @"files/flagnum.aff";

                var actual = await ReadFileAsync(filePath);

                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes[0].AFlag.Should().Be((char)999);
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[1].AFlag.Should().Be((char)214);
                actual.Suffixes[1].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[2].AFlag.Should().Be((char)216);
                actual.Suffixes[2].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes[0].AFlag.Should().Be((char)54321);
                actual.Prefixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
            }

            [Fact]
            public async Task can_read_fogemorpheme_aff()
            {
                var filePath = @"files/fogemorpheme.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('X');
                actual.CompoundBegin.Should().Be('Y');
                actual.OnlyInCompound.Should().Be('Z');
                actual.CompoundPermitFlag.Should().Be('P');
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_forbiddenword_aff()
            {
                var filePath = @"files/forbiddenword.aff";

                var actual = await ReadFileAsync(filePath);

                actual.ForbiddenWord.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_forceucase_aff()
            {
                var filePath = @"files/forceucase.aff";

                var actual = await ReadFileAsync(filePath);

                actual.TryString.Should().Be("F");
                actual.ForceUpperCase.Should().Be('A');
                actual.CompoundFlag.Should().Be('C');
            }

            [Fact]
            public async Task can_read_fullstrip_aff()
            {
                var filePath = @"files/fullstrip.aff";

                var actual = await ReadFileAsync(filePath);

                actual.FullStrip.Should().BeTrue();
                actual.TryString.Should().Be("aioertnsclmdpgubzfvhàq'ACMSkBGPLxEyRTVòIODNwFéùèìjUZKHWJYQX");
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes[0].AFlag.Should().Be('A');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                var entry1 = actual.Suffixes[0].Entries[0];
                entry1.Strip.Should().Be("andare");
                entry1.Append.Should().Be("vado");
                entry1.ConditionText.Should().Be(".");
                var entry2 = actual.Suffixes[0].Entries[1];
                entry2.Strip.Should().Be("andare");
                entry2.Append.Should().Be("va");
                entry2.ConditionText.Should().Be(".");
                var entry3 = actual.Suffixes[0].Entries[2];
                entry3.Strip.Should().Be("are");
                entry3.Append.Should().Be("iamo");
                entry3.ConditionText.Should().Be("eradna");
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
                actual.ForbiddenWord.Should().Be('?');
            }

            private async Task<AffixFile> ReadFileAsync(string filePath)
            {
                using (var reader = new AffixFileReader(new AffixUtfStreamLineReader(filePath)))
                {
                    return await reader.GetOrReadAsync();
                }
            }

            private string Reversed(string text)
            {
                var letters = text.ToCharArray();
                Array.Reverse(letters);
                return new string(letters);
            }
        }
    }
}
