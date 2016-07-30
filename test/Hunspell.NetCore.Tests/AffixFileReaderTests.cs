using System.Linq;
using FluentAssertions;
using Xunit;
using System;
using System.Threading.Tasks;
using Hunspell.NetCore.Tests.Utilities;

namespace Hunspell.NetCore.Tests
{
    public class AffixFileReaderTests
    {
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
                actual.Replacements["a"].Pattern.Should().Be("a");
                actual.Replacements["a"].OutStrings[0].Should().Be("ei");
                actual.Replacements["shun"].Pattern.Should().Be("shun");
                actual.Replacements["shun"].OutStrings[0].Should().Be("cion");
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
            public async Task can_read_checkcompoundrep_aff()
            {
                var filePath = @"files/checkcompoundrep.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckCompoundRep.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements.Values.Single().Pattern.Should().NotBeNullOrEmpty();
                actual.Replacements.Values.Single().OutStrings[0].Should().Be("i");
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
            public async Task can_read_germancompounding_aff()
            {
                var filePath = @"files/germancompounding.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckSharps.Should().BeTrue();
                actual.CompoundBegin.Should().Be('U');
                actual.CompoundMiddle.Should().Be('V');
                actual.CompoundEnd.Should().Be('W');
                actual.CompoundPermitFlag.Should().Be('P');
                actual.OnlyInCompound.Should().Be('X');
                actual.CompoundMin.Should().Be(1);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });

                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes[0].AFlag.Should().Be('A');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[0].Entries.Should().HaveCount(3);
                actual.Suffixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[0].Entries[0].Append.Should().Be("s");
                actual.Suffixes[0].Entries[0].ContClass.ShouldBeEquivalentTo(new[] { 'U', 'P', 'X' });
                actual.Suffixes[0].Entries[0].ConditionText.Should().Be(".");

                actual.ForbiddenWord.Should().Be('Z');

                actual.Prefixes.Should().HaveCount(2);
                actual.Prefixes[0].AFlag.Should().Be('-');
                actual.Prefixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes[0].Entries.Should().HaveCount(1);
                actual.Prefixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Prefixes[0].Entries[0].Append.Should().Be("-");
                actual.Prefixes[0].Entries[0].ContClass.ShouldBeEquivalentTo(new[] { 'P' });
                actual.Prefixes[0].Entries[0].ConditionText.Should().Be(".");
                actual.Prefixes[1].Entries.Should().HaveCount(29);
            }

            [Fact]
            public async Task can_read_iconv_aff()
            {
                var filePath = @"files/iconv.aff";

                var actual = await ReadFileAsync(filePath);

                actual.InputConversions.Should().HaveCount(4);
                actual.InputConversions.ContainsKey("ş");
                actual.InputConversions["ş"].OutStrings[0].Should().Be("ș");
                actual.InputConversions.ContainsKey("ţ");
                actual.InputConversions["ţ"].OutStrings[0].Should().Be("ț");
                actual.InputConversions.ContainsKey("Ş");
                actual.InputConversions["Ş"].OutStrings[0].Should().Be("Ș");
                actual.InputConversions.ContainsKey("Ţ");
                actual.InputConversions["Ţ"].OutStrings[0].Should().Be("Ț");
            }

            [Fact]
            public async Task can_read_ignore_aff()
            {
                var filePath = @"files/ignore.aff";

                var actual = await ReadFileAsync(filePath);

                actual.IgnoredChars.ShouldBeEquivalentTo("aeiou".ToCharArray());
                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes[0].AFlag.Should().Be('A');
                actual.Prefixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes[0].Entries.Should().HaveCount(1);
                actual.Prefixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Prefixes[0].Entries[0].Append.Should().Be("r");
                actual.Prefixes[0].Entries[0].ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_ignoreutf_aff()
            {
                var filePath = @"files/ignoreutf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.IgnoredChars.ShouldBeEquivalentTo("ًٌٍَُِّْ".ToCharArray());
                actual.WordChars.ShouldBeEquivalentTo("ًٌٍَُِّْ".ToCharArray());
            }

            [Fact]
            public async Task can_read_maputf_aff()
            {
                var filePath = @"files/maputf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.MapTable.Should().HaveCount(3);
                actual.MapTable[0].ShouldBeEquivalentTo(new[] { "u", "ú", "ü" });
                actual.MapTable[1].ShouldBeEquivalentTo(new[] { "ö", "ó", "o" });
                actual.MapTable[2].ShouldBeEquivalentTo(new[] { "ß", "ss" });
            }

            [Fact]
            public async Task can_read_needaffix_aff()
            {
                var filePath = @"files/needaffix.aff";

                var actual = await ReadFileAsync(filePath);

                actual.NeedAffix.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes[0].Entries.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_nepali_aff()
            {
                var filePath = @"files/nepali.aff";

                var actual = await ReadFileAsync(filePath);

                actual.IgnoredChars.Should().BeEquivalentTo(new[] { '￰' });
                actual.WordChars.Should().BeEquivalentTo("ःािीॉॊोौॎॏॕॖॗ‌‍".ToCharArray());
                actual.InputConversions.Should().HaveCount(3);
                var key1 = "‌"; // NOTE: this is not the empty string
                var value1_1 = "￰";
                var value1_2 = "‌"; // NOTE: this is not the empty string
                key1.Should().NotBeEmpty();
                value1_2.Should().NotBeEmpty();
                actual.InputConversions[key1].OutStrings[0].Should().Be("￰");
                actual.InputConversions[key1].OutStrings[2].Should().Be(value1_1);
                actual.InputConversions[key1].OutStrings[2].Should().Be(value1_2);
                actual.InputConversions["र्‌य"].OutStrings[0].Should().Be("र्‌य");
                actual.InputConversions["र्‌व"].OutStrings[0].Should().Be("र्‌व");
            }

            [Fact]
            public async Task can_read_oconv_aff()
            {
                var filePath = @"files/oconv.aff";

                var actual = await ReadFileAsync(filePath);

                actual.OutputConversions.Should().HaveCount(7);
                actual.OutputConversions["a"].OutStrings[0].Should().Be("A");
                actual.OutputConversions["á"].OutStrings[0].Should().Be("Á");
                actual.OutputConversions["b"].OutStrings[0].Should().Be("B");
                actual.OutputConversions["c"].OutStrings[0].Should().Be("C");
                actual.OutputConversions["d"].OutStrings[0].Should().Be("D");
                actual.OutputConversions["e"].OutStrings[0].Should().Be("E");
                actual.OutputConversions["é"].OutStrings[0].Should().Be("É");
            }

            [Fact]
            public async Task can_read_onlyincompound2_aff()
            {
                var filePath = @"files/onlyincompound2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.OnlyInCompound.Should().Be('O');
                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPermitFlag.Should().Be('P');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes[0].AFlag.Should().Be('B');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[0].Entries.Should().HaveCount(1);
                actual.Suffixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[0].Entries[0].Append.Should().Be("s");
                actual.Suffixes[0].Entries[0].ContClass.ShouldBeEquivalentTo(new[] { 'O', 'P' });
                actual.Suffixes[0].Entries[0].ConditionText.Should().Be(".");
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns[0].Pattern.Should().Be("0");
                actual.CompoundPatterns[0].Condition.Should().Be('B');
                actual.CompoundPatterns[0].Pattern2.Should().BeEmpty();
                actual.CompoundPatterns[0].Condition2.Should().Be('A');
            }

            [Fact]
            public async Task can_read_opentaal_cpdpat_aff()
            {
                var filePath = @"files/opentaal_cpdpat.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundBegin.Should().Be('C' << 8 | 'a');
                actual.CompoundMiddle.Should().Be('C' << 8 | 'b');
                actual.CompoundEnd.Should().Be('C' << 8 | 'c');
                actual.CompoundPermitFlag.Should().Be('C' << 8 | 'p');
                actual.OnlyInCompound.Should().Be('C' << 8 | 'x');
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns[0].Pattern.Should().BeEmpty();
                actual.CompoundPatterns[0].Condition.Should().Be('C' << 8 | 'h');
                actual.CompoundPatterns[0].Pattern2.Should().BeEmpty();
                actual.CompoundPatterns[0].Condition2.Should().Be('X' << 8 | 's');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes[0].AFlag.Should().Be('C' << 8 | 'h');
                actual.Suffixes[0].Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes[0].Entries.Should().HaveCount(2);
                actual.Suffixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[0].Entries[0].Append.Should().Be("s");
                actual.Suffixes[0].Entries[0].ContClass.ShouldBeEquivalentTo(new[] { 'C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'x', 'C' << 8 | 'p' });
                actual.Suffixes[0].Entries[0].ConditionText.Should().Be(".");
                actual.Suffixes[0].Entries[1].Strip.Should().BeEmpty();
                actual.Suffixes[0].Entries[1].Append.Should().Be("s-");
                actual.Suffixes[0].Entries[1].ContClass.ShouldBeEquivalentTo(new[] { 'C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'c', 'C' << 8 | 'p' });
                actual.Suffixes[0].Entries[1].ConditionText.Should().Be(".");
            }

            [Fact]
            public async Task can_read_opentaal_cpdpat2_aff()
            {
                var filePath = @"files/opentaal_cpdpat2.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.NoSplitSuggestions.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_phone_add()
            {
                var filePath = @"files/phone.aff";

                var actual = await ReadFileAsync(filePath);

                actual.Phone.Should().HaveCount(105);
                actual.Phone.First().Rule.Should().Be("AH(AEIOUY)-^");
                actual.Phone.First().Replace.Should().Be("*H");
                actual.Phone.Last().Rule.Should().Be("Z");
                actual.Phone.Last().Replace.Should().Be("S");
            }

            [Fact]
            public async Task can_read_rep_aff()
            {
                var filePath = @"files/rep.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().HaveCount(8);
                actual.Replacements["f"].Pattern.Should().Be("f");
                actual.Replacements["f"].OutStrings[0].Should().Be("ph");
                actual.Replacements["s"].Pattern.Should().Be("s");
                actual.Replacements["s"].OutStrings[0].Should().Be("'s");
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('A');
                actual.WordChars.Should().BeEquivalentTo(new[] { '\'' });
            }

            [Fact]
            public async Task can_read_reputf_aff()
            {
                var filePath = @"files/reputf.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements["oo"].Pattern.Should().Be("oo");
                actual.Replacements["oo"].OutStrings[0].Should().Be("őő");
            }

            [Fact]
            public async Task can_read_simplifiedtriple_aff()
            {
                var filePath = @"files/simplifiedtriple.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CheckCompoundTriple.Should().BeTrue();
                actual.SimplifiedTriple.Should().BeTrue();
                actual.CompoundMin.Should().Be(2);
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_slash_aff()
            {
                var filePath = @"files/slash.aff";

                var actual = await ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(@"/:".ToCharArray());
            }

            [Fact]
            public async Task can_read_sug_aff()
            {
                var filePath = @"files/sug.aff";

                var actual = await ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().NotBeNull();
                var entry = actual.Replacements.Should().HaveCount(1).And.Subject.Values.Single();
                entry.Pattern.Should().Be("alot");
                entry.Med.Should().Be("a lot");
                actual.KeyString.Should().Be("qwertzuiop|asdfghjkl|yxcvbnm|aq");
                actual.WordChars.Should().BeEquivalentTo(new[] { '.' });
                actual.ForbiddenWord.Should().Be('?');
            }

            [Fact]
            public async Task can_read_utf8_bom_aff()
            {
                var filePath = @"files/utf8_bom.aff";

                var actual = await ReadFileAsync(filePath);

                actual.RequestedEncoding.Should().Be("UTF-8");
            }

            [Fact]
            public async Task can_read_utfcompound_aff()
            {
                var filePath = @"files/utfcompound.aff";

                var actual = await ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(3);
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_warn_add()
            {
                var filePath = @"files/warn.aff";

                var actual = await ReadFileAsync(filePath);

                actual.Warn.Should().Be('W');
                actual.Suffixes.Should().HaveCount(1);
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements["foo"].Pattern.Should().Be("foo");
                actual.Replacements["foo"].OutStrings[0].Should().Be("bar");
            }

            [Fact]
            public async Task can_read_zeroaffix_aff()
            {
                var filePath = @"files/zeroaffix.aff";

                var actual = await ReadFileAsync(filePath);

                actual.NeedAffix.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');

                actual.Suffixes.Should().HaveCount(3);

                actual.Suffixes[0].AFlag.Should().Be('A');
                actual.Suffixes[0].Entries.Should().HaveCount(1);
                actual.Suffixes[0].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[0].Entries[0].Append.Should().BeEmpty();
                actual.Suffixes[0].Entries[0].ConditionText.Should().Be(".");
                actual.Suffixes[0].Entries[0].MorphCode.Should().Be(">");

                actual.Suffixes[1].AFlag.Should().Be('B');
                actual.Suffixes[1].Entries.Should().HaveCount(1);
                actual.Suffixes[1].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[1].Entries[0].Append.Should().BeEmpty();
                actual.Suffixes[1].Entries[0].ConditionText.Should().Be(".");
                actual.Suffixes[1].Entries[0].MorphCode.Should().Be("<ZERO>>");

                actual.Suffixes[2].AFlag.Should().Be('C');
                actual.Suffixes[2].Entries.Should().HaveCount(2);
                actual.Suffixes[2].Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes[2].Entries[0].Append.Should().BeEmpty();
                actual.Suffixes[2].Entries[0].ContClass.ShouldBeEquivalentTo(new[] { 'X', 'A', 'B' });
                actual.Suffixes[2].Entries[0].ConditionText.Should().Be(".");
                actual.Suffixes[2].Entries[0].MorphCode.Should().Be("<ZERODERIV>");
                actual.Suffixes[2].Entries[1].Strip.Should().BeEmpty();
                actual.Suffixes[2].Entries[1].Append.Should().Be("baz");
                actual.Suffixes[2].Entries[1].ContClass.ShouldBeEquivalentTo(new[] { 'X', 'A', 'B' });
                actual.Suffixes[2].Entries[1].ConditionText.Should().Be(".");
                actual.Suffixes[2].Entries[1].MorphCode.Should().Be("<DERIV>");
            }

            [Theory]
            [InlineData("")]
            [InlineData("de-DE")]
            [InlineData("de")]
            [InlineData("en-US")]
            [InlineData("tr_TR")]
            [InlineData("en-UK")]
            [InlineData("hu-HU")]
            [InlineData("it")]
            [InlineData("ar")]
            [InlineData("uk")]
            [InlineData("xx")]
            public async Task can_read_all_languages(string langCode)
            {
                var textFileContents = "LANG " + langCode;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.Language.Should().Be(langCode);
                actual.Culture.Should().NotBeNull();
                actual.Culture.Name.Should().Be(langCode.Replace('_', '-'));
            }

            [Theory]
            [InlineData("klmc")]
            [InlineData("ःािीॉॊोौॎॏॕॖॗ‌‍")]
            public async Task can_read_syllablenum(string parameters)
            {
                var textFileContents = "SYLLABLENUM " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CompoundSyllableNum.Should().Be(parameters);
            }

            [Theory]
            [InlineData("0", 0)]
            [InlineData("5", 5)]
            [InlineData("words", 0)]
            public async Task can_read_compoundwordmax(string parameters, int expected)
            {
                var textFileContents = "COMPOUNDWORDMAX " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CompoundWordMax.Should().Be(expected);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("Z", 'Z')]
            [InlineData("AB", 'A' << 8 | 'B')]
            [InlineData("y", 'y')]
            public async Task can_read_compoundroot(string parameters, int expected)
            {
                var textFileContents = string.Empty;
                if (parameters.Length > 1)
                {
                    textFileContents += "FLAG LONG\n";
                }
                textFileContents += "COMPOUNDROOT " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CompoundRoot.Should().Be(expected);
            }

            [Theory]
            [InlineData("6 aáeéiíoóöőuúüű", 6, "aáeéiíoóöőuúüű")]
            [InlineData("1 abc", 1, "abc")]
            public async Task can_read_compoundsyllable(string parameters, int expectedNumber, string expectedLettersText)
            {
                var textFileContents = "COMPOUNDSYLLABLE " + parameters;
                var expectedLetters = expectedLettersText.ToCharArray();
                Array.Sort(expectedLetters);

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CompoundMaxSyllable.Should().Be(expectedNumber);
                actual.CompoundVowels.ShouldBeEquivalentTo(expectedLetters);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("=", '=')]
            public async Task can_read_nosuggest(string parameters, int expectedFlag)
            {
                var textFileContents = "NOSUGGEST " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.NoSuggest.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("=", '=')]
            public async Task can_read_nongramsuggest(string parameters, int expectedFlag)
            {
                var textFileContents = "NONGRAMSUGGEST " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.NoNgramSuggest.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData(")", ')')]
            public async Task can_read_lemma_present(string parameters, int expectedFlag)
            {
                var textFileContents = "LEMMA_PRESENT " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.LemmaPresent.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("Magyar 1.6", "Magyar 1.6")]
            [InlineData("", null)]
            public async Task can_read_version(string parameters, string expected)
            {
                var textFileContents = "VERSION " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.Version.Should().Be(expected);
            }

            [Theory]
            [InlineData("1", 1)]
            [InlineData("0", 0)]
            [InlineData("", 0)]
            public async Task can_read_maxdiff(string parameters, int expected)
            {
                var textFileContents = "MAXDIFF " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.MaxDifferency.Should().Be(expected);
            }

            [Theory]
            [InlineData("1", 1)]
            [InlineData("0", 0)]
            [InlineData("", 0)]
            public async Task can_read_maxcpdsugs(string parameters, int expected)
            {
                var textFileContents = "MAXCPDSUGS " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.MaxCompoundSuggestions.Should().Be(expected);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("&", '&')]
            public async Task can_read_substandard(string parameters, int expectedFlag)
            {
                var textFileContents = "SUBSTANDARD " + parameters;

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.SubStandard.Should().Be(expectedFlag);
            }

            [Fact]
            public async Task can_read_compoundmoresuffixes()
            {
                var textFileContents = "COMPOUNDMORESUFFIXES";

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CompoundMoreSuffixes.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_checknum()
            {
                var textFileContents = "CHECKNUM";

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.CheckNum.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_onlymaxdiff()
            {
                var textFileContents = "ONLYMAXDIFF";

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.OnlyMaxDiff.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_sugswithdots()
            {
                var textFileContents = "SUGSWITHDOTS";

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.SuggestWithDots.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_forbidwarn()
            {
                var textFileContents = "FORBIDWARN";

                var actual = await AffixFileReader.ReadAsync(new StringLineReader(textFileContents));

                actual.ForbidWarn.Should().BeTrue();
            }

            private async Task<AffixConfig> ReadFileAsync(string filePath)
            {
                using (var lineReader = new AffixUtfStreamLineReader(filePath))
                {
                    return await AffixFileReader.ReadAsync(lineReader);
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
