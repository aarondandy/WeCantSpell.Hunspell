using System.Linq;
using FluentAssertions;
using Xunit;
using System;
using System.Threading.Tasks;
using WeCantSpell.Hunspell.Tests.Utilities;
using System.IO;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Tests
{
    public class AffixReaderTests
    {
        static AffixReaderTests()
        {
            EncodingHelpers.EnsureEncodingsReady();
        }

        public class ReadFileAsync : AffixReaderTests
        {
            [Fact]
            public async Task can_read_1463589_aff()
            {
                var filePath = @"files/1463589.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(1);
            }

            [Fact]
            public async Task can_read_1463589_utf_aff()
            {
                var filePath = @"files/1463589_utf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");
                actual.MaxNgramSuggestions.Should().Be(1);
            }

            [Fact]
            public async Task can_read_1592880_aff()
            {
                var filePath = @"files/1592880.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-1");

                actual.Suffixes.Should().HaveCount(4);
                var suffixes = actual.Suffixes.ToList();

                var suffixGroup1 = suffixes[0];
                suffixGroup1.AFlag.Should().Be('N');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup1.Entries.Should().HaveCount(1);
                suffixGroup1.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup1.Entries.Single().Append.Should().Be("n");
                suffixGroup1.Entries.Single().Conditions.GetEncoded().Should().Be(".");

                var suffixGroup2 = suffixes[1];
                suffixGroup2.AFlag.Should().Be('S');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup2.Entries.Should().HaveCount(1);
                suffixGroup2.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup2.Entries.Single().Append.Should().Be("s");
                suffixGroup2.Entries.Single().Conditions.GetEncoded().Should().Be(".");

                var suffixGroup3 = suffixes[2];
                suffixGroup3.AFlag.Should().Be('P');
                suffixGroup3.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup3.Entries.Should().HaveCount(1);
                suffixGroup3.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup3.Entries.Single().Append.Should().Be("en");
                suffixGroup3.Entries.Single().Conditions.GetEncoded().Should().Be(".");

                var suffixGroup4 = suffixes[3];
                suffixGroup4.AFlag.Should().Be('Q');
                suffixGroup4.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup4.Entries.Should().HaveCount(2);
                suffixGroup4.Entries.First().Strip.Should().BeEmpty();
                suffixGroup4.Entries.First().Append.Should().Be("e");
                suffixGroup4.Entries.First().Conditions.GetEncoded().Should().Be(".");
                suffixGroup4.Entries.Last().Strip.Should().BeEmpty();
                suffixGroup4.Entries.Last().Append.Should().Be("en");
                suffixGroup4.Entries.Last().Conditions.GetEncoded().Should().Be(".");

                actual.CompoundEnd.Should().Be('z');
                actual.CompoundPermitFlag.Should().Be('c');
                actual.OnlyInCompound.Should().Be('o');
            }

            [Fact]
            public async Task can_read_1695964_aff()
            {
                var filePath = @"files/1695964.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.TryString.Should().Be("esianrtolcdugmphbyfvkwESIANRTOLCDUGMPHBYFVKW");
                actual.MaxNgramSuggestions.Should().Be(0);
                actual.NeedAffix.Should().Be('h');
                actual.Suffixes.Should().HaveCount(2);
                var suffixGroup1 = actual.Suffixes.First();
                suffixGroup1.AFlag.Should().Be('S');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup1.Entries.Should().HaveCount(1);
                suffixGroup1.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup1.Entries.Single().Append.Should().Be("s");
                suffixGroup1.Entries.Single().Conditions.GetEncoded().Should().Be(".");
                var suffixGroup2 = actual.Suffixes.Last();
                suffixGroup2.AFlag.Should().Be('e');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroup2.Entries.Should().HaveCount(1);
                suffixGroup2.Entries.Single().Strip.Should().BeEmpty();
                suffixGroup2.Entries.Single().Append.Should().Be("e");
                suffixGroup2.Entries.Single().Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_1706659_aff()
            {
                var filePath = @"files/1706659.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-1");
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
                actual.Suffixes.Single().Entries.Should().OnlyContain(e => e.Conditions.GetEncoded() == ".");

                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().Should().ContainInOrder(new[] { 'v', 'w' });
            }

            [Fact]
            public async Task can_read_1975530_aff()
            {
                var filePath = @"files/1975530.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");
                actual.IgnoredChars.Should().ContainInOrder(new[] { 1617, 1614, 1615, 1612, 1618, 1616, 1613, 1600 }.OrderBy(c => c).Select(i => (char)i));
                actual.Prefixes.Should().HaveCount(1);
                var prefixGroup1 = actual.Prefixes.Single();
                prefixGroup1.AFlag.Should().Be('x');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.None);
                prefixGroup1.Entries.Should().HaveCount(1);
                var prefixEntry = prefixGroup1.Entries.Single();
                prefixEntry.Append.Should().Be("ت");
                prefixEntry.Conditions.GetEncoded().Should().Be("أ[^ي]");
                prefixEntry.Strip.Should().Be("أ");
            }

            [Fact]
            public async Task can_read_2970240_aff()
            {
                var filePath = @"files/2970240.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

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

                var actual = await AffixReader.ReadFileAsync(filePath);

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

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().Should().ContainInOrder(new[] { 'a', 'b' });
                actual.CompoundBegin.Should().Be('A');
                actual.CompoundEnd.Should().Be('B');
            }

            [Fact]
            public async Task can_read_affixes_aff()
            {
                var filePath = @"files/affixes.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be('A');
                actual.Prefixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Single().Entries.Should().HaveCount(1);
                var prefixEntry = actual.Prefixes.Single().Entries.Single();
                prefixEntry.Strip.Should().BeEmpty();
                prefixEntry.Append.Should().Be("re");
                prefixEntry.Conditions.GetEncoded().Should().Be(".");

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('B');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.Single().Entries.Should().HaveCount(2);
                var suffixEntry1 = actual.Suffixes.Single().Entries.First();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("ed");
                suffixEntry1.Conditions.GetEncoded().Should().Be("[^y]");
                var suffixEntry2 = actual.Suffixes.Single().Entries.Last();
                suffixEntry2.Strip.Should().Be("y");
                suffixEntry2.Append.Should().Be("ied");
                suffixEntry2.Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_alias_aff()
            {
                var filePath = @"files/alias.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.AliasF.Should().HaveCount(2);
                actual.AliasF.First().Should().ContainInOrder(new int[] { 'A', 'B' });
                actual.AliasF.Last().Should().ContainInOrder(new int[] { 'A' });
                actual.Suffixes.Should().HaveCount(2);
                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                actual.Suffixes.First().Entries.Single().Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries.Single().Append.Should().Be("x");
                actual.Suffixes.First().Entries.Single().Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.Last().AFlag.Should().Be('B');
                actual.Suffixes.Last().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
                actual.Suffixes.Last().Entries.Should().HaveCount(1);
                actual.Suffixes.Last().Entries.Single().Strip.Should().BeEmpty();
                actual.Suffixes.Last().Entries.Single().Append.Should().Be("y");
                actual.Suffixes.Last().Entries.Single().ContClass.Should().ContainInOrder(new[] { 'A' });
                actual.Suffixes.Last().Entries.Single().Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_alias2_aff()
            {
                var filePath = @"files/alias2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.AliasF.Should().HaveCount(2);
                actual.AliasF.First().Should().ContainInOrder(new int[] { 'A', 'B' });
                actual.AliasF.Last().Should().ContainInOrder(new int[] { 'A' });

                actual.AliasM.Should().HaveCount(3);
                actual.AliasM.First().Should().OnlyContain(x => x == "is:affix_x");
                actual.AliasM.Skip(1).First().Should().OnlyContain(x => x == "ds:affix_y");
                actual.AliasM.Last().ShouldAllBeEquivalentTo(new[] { "po:noun", "xx:other_data" });

                actual.Suffixes.Should().HaveCount(2);

                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                var suffixEntry1 = actual.Suffixes.First().Entries.Single();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("x");
                suffixEntry1.Conditions.GetEncoded().Should().Be(".");
                suffixEntry1.MorphCode.Should().OnlyContain(x => x == "is:affix_x");

                actual.Suffixes.Last().AFlag.Should().Be('B');
                actual.Suffixes.Last().Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
                actual.Suffixes.Last().Entries.Should().HaveCount(1);
                var suffixEntry2 = actual.Suffixes.Last().Entries.Single();
                suffixEntry2.Strip.Should().BeEmpty();
                suffixEntry2.Append.Should().Be("y");
                suffixEntry2.ContClass.Should().ContainInOrder(new int[] { 'A' });
                suffixEntry2.Conditions.GetEncoded().Should().Be(".");
                suffixEntry2.MorphCode.Should().OnlyContain(x => x == "ds:affix_y");
            }

            [Fact]
            public async Task can_read_alias3_aff()
            {
                var filePath = @"files/alias3.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.ComplexPrefixes.Should().BeTrue();
                actual.WordChars.Should().BeEquivalentTo(new[] { '_' });
                actual.AliasM.Should().HaveCount(4);
                actual.AliasM.ShouldBeEquivalentTo(new[]
                {
                    new[] { Reversed(@"affix_1/") },
                    new[] { Reversed(@"affix_2/") },
                    new[] { Reversed(@"/suffix_1") },
                    new[] { Reversed(@"[stem_1]") }
                });

                actual.Suffixes.Should().HaveCount(2);
                actual.Prefixes.Should().HaveCount(1);

                var suffixGroup1 = actual.Suffixes.First();
                suffixGroup1.AFlag.Should().Be('A');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                suffixGroup1.Entries.Should().HaveCount(1);
                var suffixEntry1 = suffixGroup1.Entries.Single();
                suffixEntry1.Strip.Should().BeEmpty();
                suffixEntry1.Append.Should().Be("ket");
                suffixEntry1.Conditions.GetEncoded().Should().Be(".");
                suffixEntry1.MorphCode.Should().ContainSingle(Reversed(@"affix_1/"));

                var suffixGroup2 = actual.Suffixes.Last();
                suffixGroup2.AFlag.Should().Be('B');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                suffixGroup2.Entries.Should().HaveCount(1);
                var suffixEntry2 = suffixGroup2.Entries.Single();
                suffixEntry2.Strip.Should().BeEmpty();
                suffixEntry2.Append.Should().Be("tem");
                suffixEntry2.ContClass.Should().ContainInOrder(new int[] { 'A' });
                suffixEntry2.Conditions.GetEncoded().Should().Be(".");
                suffixEntry2.MorphCode.Should().ContainSingle(Reversed(@"affix_2/"));

                var prefixGroup1 = actual.Prefixes.Single();
                prefixGroup1.AFlag.Should().Be('C');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
                prefixGroup1.Entries.Should().HaveCount(1);
                var prefixEntry1 = prefixGroup1.Entries.Single();
                prefixEntry1.Strip.Should().BeEmpty();
                prefixEntry1.Append.Should().Be("_tset_");
                prefixEntry1.Conditions.GetEncoded().Should().Be(".");
                prefixEntry1.MorphCode.Should().ContainSingle(Reversed(@"/suffix_1"));
            }

            [Fact]
            public async Task can_read_allcaps_aff()
            {
                var filePath = @"files/allcaps.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'', '.' });

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('S');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("'s");
                entry1.Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps_utf_aff()
            {
                var filePath = @"files/allcaps_utf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'', '.' });

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('S');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("'s");
                entry1.Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps2_aff()
            {
                var filePath = @"files/allcaps2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.ForbiddenWord.Should().Be('*');

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('s');
                actual.Suffixes.Single().Options.Should().Be(AffixEntryOptions.None);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
                var entry1 = actual.Suffixes.Single().Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("os");
                entry1.Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_allcaps3_aff()
            {
                var filePath = @"files/allcaps3.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '\'' });

                actual.Suffixes.Should().HaveCount(2);
                var suffixGroup1 = actual.Suffixes.First();
                suffixGroup1.AFlag.Should().Be('s');
                suffixGroup1.Options.Should().Be(AffixEntryOptions.None);
                suffixGroup1.Entries.Should().HaveCount(1);
                var entry1 = suffixGroup1.Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("s");
                entry1.Conditions.GetEncoded().Should().Be(".");
                var suffixGroup2 = actual.Suffixes.Last();
                suffixGroup2.AFlag.Should().Be('S');
                suffixGroup2.Options.Should().Be(AffixEntryOptions.None);
                suffixGroup2.Entries.Should().HaveCount(1);
                var entry2 = suffixGroup2.Entries.Single();
                entry2.Strip.Should().BeEmpty();
                entry2.Append.Should().Be("\'s");
                entry2.Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_arabic_aff()
            {
                var filePath = @"files/arabic.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");
                actual.TryString.Should().Be("أ");
                actual.IgnoredChars.ShouldBeEquivalentTo(new[] { 'ّ', 'َ', 'ُ', 'ٌ', 'ْ', 'ِ', 'ٍ' });

                actual.Prefixes.Should().HaveCount(1);
                var group1 = actual.Prefixes.Single();
                group1.AFlag.Should().Be('A');
                group1.Options.Should().Be(AffixEntryOptions.CrossProduct);
                group1.Entries.Should().HaveCount(1);
                var entry1 = group1.Entries.Single();
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().BeEmpty();
                entry1.ContClass.Should().ContainInOrder(new[] { '0', 'X' });
                entry1.Conditions.GetEncoded().Should().Be("أ[^ي]");
            }

            [Fact]
            public async Task can_read_base_aff()
            {
                var filePath = @"files/base.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-1");
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
                entry1.Conditions.GetEncoded().Should().Be(".");

                actual.Suffixes.Should().HaveCount(16);

                actual.Replacements.Should().HaveCount(88);
                var replacements = actual.Replacements.ToList();
                replacements[0].Pattern.Should().Be("a");
                replacements[0].OutString.Should().Be("ei");
                replacements[0].Type.Should().Be(ReplacementValueType.Med);
                replacements[87].Pattern.Should().Be("shun");
                replacements[87].OutString.Should().Be("cion");
                replacements[87].Type.Should().Be(ReplacementValueType.Med);
            }

            [Fact]
            public async Task can_read_base_utf_aff()
            {
                var filePath = @"files/base_utf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

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

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.BreakPoints.ShouldBeEquivalentTo(new[]
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

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.TryString.Should().Be("ot");
            }

            [Fact]
            public async Task can_read_breakoff_aff()
            {
                var filePath = @"files/breakoff.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.TryString.Should().Be("ot");
                actual.BreakPoints.Should().HaveCount(0);
            }

            [Fact]
            public async Task can_read_checkcompoundcase_aff()
            {
                var filePath = @"files/checkcompoundcase.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckCompoundCase.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checkcompounddup_aff()
            {
                var filePath = @"files/checkcompounddup.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckCompoundDup.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checkcompoundpattern_aff()
            {
                var filePath = @"files/checkcompoundpattern.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns.First().Pattern.Should().Be("nny");
                actual.CompoundPatterns.First().Pattern2.Should().Be("ny");
                actual.CompoundPatterns.Last().Pattern.Should().Be("ssz");
                actual.CompoundPatterns.Last().Pattern2.Should().Be("sz");
                actual.SimplifiedCompound.Should().BeFalse();
            }

            [Fact]
            public async Task can_read_checkcompoundpattern2_aff()
            {
                var filePath = @"files/checkcompoundpattern2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns.First().Pattern.Should().Be("o");
                actual.CompoundPatterns.First().Pattern2.Should().Be("b");
                actual.CompoundPatterns.First().Pattern3.Should().Be("z");
                actual.CompoundPatterns.Last().Pattern.Should().Be("oo");
                actual.CompoundPatterns.Last().Pattern2.Should().Be("ba");
                actual.CompoundPatterns.Last().Pattern3.Should().Be("u");
                actual.CompoundMin.Should().Be(1);
                actual.SimplifiedCompound.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_checkcompoundpattern3_aff()
            {
                var filePath = @"files/checkcompoundpattern3.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns.Single().Pattern.Should().Be("o");
                actual.CompoundPatterns.Single().Condition.Should().Be('X');
                actual.CompoundPatterns.Single().Pattern2.Should().Be("b");
                actual.CompoundPatterns.Single().Condition2.Should().Be('Y');
                actual.CompoundPatterns.Single().Pattern3.Should().Be("z");
                actual.CompoundMin.Should().Be(1);
                actual.SimplifiedCompound.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_checkcompoundpattern4_aff()
            {
                var filePath = @"files/checkcompoundpattern4.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('x');
                actual.CompoundMin.Should().Be(1);
                actual.CompoundPatterns.Should().HaveCount(2);
                actual.CompoundPatterns.First().Pattern.Should().Be("a");
                actual.CompoundPatterns.First().Condition.Should().Be('A');
                actual.CompoundPatterns.First().Pattern2.Should().Be("u");
                actual.CompoundPatterns.First().Condition2.Should().Be('A');
                actual.CompoundPatterns.First().Pattern3.Should().Be("O");
                actual.CompoundPatterns.Last().Pattern.Should().Be("u");
                actual.CompoundPatterns.Last().Condition.Should().Be('B');
                actual.CompoundPatterns.Last().Pattern2.Should().Be("u");
                actual.CompoundPatterns.Last().Condition2.Should().Be('B');
                actual.CompoundPatterns.Last().Pattern3.Should().Be("u");
                actual.SimplifiedCompound.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_checkcompoundrep_aff()
            {
                var filePath = @"files/checkcompoundrep.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckCompoundRep.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements.Single().Pattern.Should().NotBeNullOrEmpty();
                actual.Replacements.Single().OutString.Should().Be("i");
                actual.Replacements.Single().Type.Should().Be(ReplacementValueType.Med);
            }

            [Fact]
            public async Task can_read_checkcompoundtriple_aff()
            {
                var filePath = @"files/checkcompoundtriple.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckCompoundTriple.Should().BeTrue();
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_checksharps_aff()
            {
                var filePath = @"files/checksharps.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-1");
                actual.WordChars.Should().ContainInOrder(new[] { '.', 'ß' });
            }

            [Fact]
            public async Task can_read_checksharpsutf_aff()
            {
                var filePath = @"files/checksharpsutf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");
                actual.CheckSharps.Should().BeTrue();
                actual.WordChars.ShouldBeEquivalentTo(new[] { 'ß', '.' });
                actual.KeepCase.Should().Be('k');
            }

            [Fact]
            public async Task can_read_circumfix_aff()
            {
                var filePath = @"files/circumfix.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Circumfix.Should().Be('X');

                actual.Prefixes.Should().HaveCount(2);
                actual.Prefixes.First().AFlag.Should().Be('A');
                actual.Prefixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.First().Entries.Should().HaveCount(1);
                var entry1 = actual.Prefixes.First().Entries[0];
                entry1.Strip.Should().BeEmpty();
                entry1.Append.Should().Be("leg");
                entry1.ContClass.Should().ContainInOrder(new[] { 'X' });
                entry1.Conditions.GetEncoded().Should().Be(".");
                actual.Prefixes.Last().AFlag.Should().Be('B');
                actual.Prefixes.Last().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Last().Entries.Should().HaveCount(1);
                var entry2 = actual.Prefixes.Last().Entries[0];
                entry2.Strip.Should().BeEmpty();
                entry2.Append.Should().Be("legesleg");
                entry2.ContClass.Should().ContainInOrder(new[] { 'X' });
                entry2.Conditions.GetEncoded().Should().Be(".");

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.First().AFlag.Should().Be('C');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.First().Entries.Should().HaveCount(3);
                var entry3 = actual.Suffixes.First().Entries[0];
                entry3.Strip.Should().BeEmpty();
                entry3.Append.Should().Be("obb");
                entry3.Conditions.GetEncoded().Should().Be(".");
                entry3.MorphCode.Should().OnlyContain(x => x == "is:COMPARATIVE");
                var entry4 = actual.Suffixes.First().Entries[1];
                entry4.Strip.Should().BeEmpty();
                entry4.Append.Should().Be("obb");
                entry4.ContClass.Should().ContainInOrder(new[] { 'A', 'X' });
                entry4.Conditions.GetEncoded().Should().Be(".");
                entry4.MorphCode.Should().OnlyContain(x => x == "is:SUPERLATIVE");
                var entry5 = actual.Suffixes.First().Entries[2];
                entry5.Strip.Should().BeEmpty();
                entry5.Append.Should().Be("obb");
                entry5.ContClass.Should().ContainInOrder(new[] { 'B', 'X' });
                entry5.Conditions.GetEncoded().Should().Be(".");
                entry5.MorphCode.Should().OnlyContain(x => x == "is:SUPERSUPERLATIVE");
            }

            [Fact]
            public async Task can_read_colons_in_words_aff()
            {
                var filePath = @"files/colons_in_words.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { ':' });
            }

            [Fact]
            public async Task can_read_compoundaffix2_aff()
            {
                var filePath = @"files/compoundaffix2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('X');
                actual.CompoundPermitFlag.Should().Be('Y');
                actual.Prefixes.Should().HaveCount(1);
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_compoundaffix3_aff()
            {
                var filePath = @"files/compoundaffix3.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundFlag.Should().Be('X');
                actual.CompoundForbidFlag.Should().Be('Z');
                actual.Prefixes.Should().HaveCount(1);
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_compoundrule2_aff()
            {
                var filePath = @"files/compoundrule2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().Should().ContainInOrder(new[] { 'A', '*', 'B', '*', 'C', '*' });
            }

            [Fact]
            public async Task can_read_compoundrule3_aff()
            {
                var filePath = @"files/compoundrule3.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().Should().ContainInOrder(new[] { 'A', '?', 'B', '?', 'C', '?' });
            }

            [Fact]
            public async Task can_read_compoundrule4_aff()
            {
                var filePath = @"files/compoundrule4.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be('c');
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules.First().Should().ContainInOrder(new[] { 'n', '*', '1', 't' });
                actual.CompoundRules.Last().Should().ContainInOrder(new[] { 'n', '*', 'm', 'p' });
            }

            [Fact]
            public async Task can_read_compoundrule5_aff()
            {
                var filePath = @"files/compoundrule5.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules.First().Should().ContainInOrder("N*%?".ToCharArray());
                actual.CompoundRules.Last().Should().ContainInOrder("NN*.NN*%?".ToCharArray());
                actual.WordChars.Should().ContainInOrder("0123456789‰.".ToCharArray().OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_compoundrule6_aff()
            {
                var filePath = @"files/compoundrule6.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules.First().Should().ContainInOrder("A*A".ToCharArray());
                actual.CompoundRules.Last().Should().ContainInOrder("A*AAB*BBBC*C".ToCharArray());
            }

            [Fact]
            public async Task can_read_compoundrule7_aff()
            {
                var filePath = @"files/compoundrule7.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be('c' << 8 | 'c');
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules.First().Should().ContainInOrder(new[] { 'n' << 8 | 'n', '*', '1' << 8 | '1', 't' << 8 | 't' });
                actual.CompoundRules.Last().Should().ContainInOrder(new[] { 'n' << 8 | 'n', '*', 'm' << 8 | 'm', 'p' << 8 | 'p' });
            }

            [Fact]
            public async Task can_read_compoundrule8_aff()
            {
                var filePath = @"files/compoundrule8.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.CompoundMin.Should().Be(1);
                actual.OnlyInCompound.Should().Be(1000);
                actual.CompoundRules.Should().HaveCount(2);
                actual.CompoundRules.First().Should().ContainInOrder(new[] { 1001, '*', 1002, 2001 });
                actual.CompoundRules.Last().Should().ContainInOrder(new[] { 1001, '*', 2002, 2000 });
            }

            [Fact]
            public async Task can_read_condition_aff()
            {
                var filePath = @"files/condition.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.Suffixes.Should().HaveCount(4);
                actual.Prefixes.Should().HaveCount(3);
            }

            [Fact]
            public async Task can_read_condition_utf_aff()
            {
                var filePath = @"files/condition_utf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo("0123456789".ToCharArray());
                actual.Suffixes.Should().HaveCount(1);
                actual.Prefixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_digits_in_words_aff()
            {
                var filePath = @"files/digits_in_words.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(1);
                actual.CompoundRules.Should().HaveCount(1);
                actual.CompoundRules.Single().Should().ContainInOrder(new[] { 'a', '*', 'b' });
                actual.OnlyInCompound.Should().Be('c');
                actual.WordChars.Should().ContainInOrder("0123456789-".ToCharArray().OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_encoding_aff()
            {
                var filePath = @"files/encoding.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-15");
            }

            [Fact]
            public async Task can_read_flag_aff()
            {
                var filePath = @"files/flag.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.FlagMode.Should().Be(FlagMode.Char);
                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.Skip(1).First().AFlag.Should().Be('1');
                actual.Prefixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_flaglong_aff()
            {
                var filePath = @"files/flaglong.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.FlagMode.Should().Be(FlagMode.Long);
                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.Skip(1).First().AFlag.Should().Be('g' << 8 | '?');
                actual.Prefixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_flagnum_aff()
            {
                var filePath = @"files/flagnum.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.First().AFlag.Should().Be((char)999);
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.Skip(1).First().AFlag.Should().Be((char)214);
                actual.Suffixes.Skip(1).First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.Skip(2).First().AFlag.Should().Be((char)216);
                actual.Suffixes.Skip(2).First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be((char)54321);
                actual.Prefixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
            }

            [Fact]
            public async Task can_read_flagutf8_aff()
            {
                var filePath = @"files/flagutf8.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.FlagMode.Should().Be(FlagMode.Uni);
                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                actual.Suffixes.Skip(1).First().AFlag.Should().Be('Ö');
                actual.Suffixes.Skip(1).First().Entries.Should().HaveCount(1);
                actual.Suffixes.Skip(2).First().Entries.Should().HaveCount(1);
                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().Entries.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_fogemorpheme_aff()
            {
                var filePath = @"files/fogemorpheme.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

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

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.ForbiddenWord.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');
                actual.Suffixes.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_forceucase_aff()
            {
                var filePath = @"files/forceucase.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.TryString.Should().Be("F");
                actual.ForceUpperCase.Should().Be('A');
                actual.CompoundFlag.Should().Be('C');
            }

            [Fact]
            public async Task can_read_fullstrip_aff()
            {
                var filePath = @"files/fullstrip.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.FullStrip.Should().BeTrue();
                actual.TryString.Should().Be("aioertnsclmdpgubzfvhÃ q'ACMSkBGPLxEyRTVÃ²IODNwFÃ©Ã¹ÃšÃ¬jUZKHWJYQX");
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                var entry1 = actual.Suffixes.First().Entries[0];
                entry1.Strip.Should().Be("andare");
                entry1.Append.Should().Be("vado");
                entry1.Conditions.GetEncoded().Should().Be(".");
                var entry2 = actual.Suffixes.First().Entries[1];
                entry2.Strip.Should().Be("andare");
                entry2.Append.Should().Be("va");
                entry2.Conditions.GetEncoded().Should().Be(".");
                var entry3 = actual.Suffixes.First().Entries[2];
                entry3.Strip.Should().Be("are");
                entry3.Append.Should().Be("iamo");
                entry3.Conditions.GetEncoded().Should().Be("andare");
            }

            [Fact]
            public async Task can_read_germancompounding_aff()
            {
                var filePath = @"files/germancompounding.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckSharps.Should().BeTrue();
                actual.CompoundBegin.Should().Be('U');
                actual.CompoundMiddle.Should().Be('V');
                actual.CompoundEnd.Should().Be('W');
                actual.CompoundPermitFlag.Should().Be('P');
                actual.OnlyInCompound.Should().Be('X');
                actual.CompoundMin.Should().Be(1);
                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });

                actual.Suffixes.Should().HaveCount(3);

                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.First().Entries.Should().HaveCount(3);
                actual.Suffixes.First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries[0].Append.Should().Be("s");
                actual.Suffixes.First().Entries[0].Key.Should().Be("s");
                actual.Suffixes.First().Entries[0].ContClass.Should().ContainInOrder(new[] { 'U', 'P', 'X' }.OrderBy(x => x));
                actual.Suffixes.First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.First().Entries[1].Append.Should().Be("s");
                actual.Suffixes.First().Entries[1].Key.Should().Be("s");
                actual.Suffixes.First().Entries[2].Append.Should().BeEmpty();
                actual.Suffixes.First().Entries[2].Key.Should().BeEmpty();

                actual.Suffixes.Skip(1).First().AFlag.Should().Be('B');
                actual.Suffixes.Skip(1).First().Entries.Should().HaveCount(2);

                actual.Suffixes.Skip(2).First().AFlag.Should().Be('C');
                actual.Suffixes.Skip(2).First().Entries.Should().HaveCount(1);
                actual.Suffixes.Skip(2).First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.Skip(2).First().Entries[0].Append.Should().Be("n");
                actual.Suffixes.Skip(2).First().Entries[0].ContClass.Should().ContainInOrder(new[] { 'D', 'W' });
                actual.Suffixes.Skip(2).First().Entries[0].Conditions.GetEncoded().Should().Be(".");

                actual.ForbiddenWord.Should().Be('Z');

                actual.Prefixes.Should().HaveCount(2);
                actual.Prefixes.First().AFlag.Should().Be('-');
                actual.Prefixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.First().Entries.Should().HaveCount(1);
                actual.Prefixes.First().Entries[0].Strip.Should().BeEmpty();
                actual.Prefixes.First().Entries[0].Append.Should().Be("-");
                actual.Prefixes.First().Entries[0].ContClass.Should().ContainInOrder(new[] { 'P' });
                actual.Prefixes.First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Prefixes.Last().Entries.Should().HaveCount(29);
            }

            [Fact]
            public async Task can_read_iconv_aff()
            {
                var filePath = @"files/iconv.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.InputConversions.Should().HaveCount(4);
                actual.InputConversions.ContainsKey("ş");
                actual.InputConversions["ş"][0].Should().Be("ș");
                actual.InputConversions.ContainsKey("ţ");
                actual.InputConversions["ţ"][0].Should().Be("ț");
                actual.InputConversions.ContainsKey("Ş");
                actual.InputConversions["Ş"][0].Should().Be("Ș");
                actual.InputConversions.ContainsKey("Ţ");
                actual.InputConversions["Ţ"][0].Should().Be("Ț");
            }

            [Fact]
            public async Task can_read_ignore_aff()
            {
                var filePath = @"files/ignore.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.IgnoredChars.ShouldBeEquivalentTo("aeiou".ToCharArray());
                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be('A');
                actual.Prefixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Single().Entries.Should().HaveCount(1);
                actual.Prefixes.Single().Entries[0].Strip.Should().BeEmpty();
                actual.Prefixes.Single().Entries[0].Append.Should().Be("r");
                actual.Prefixes.Single().Entries[0].Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_ignoreutf_aff()
            {
                var filePath = @"files/ignoreutf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.IgnoredChars.ShouldBeEquivalentTo("ًٌٍَُِّْ".ToCharArray());
                actual.WordChars.ShouldBeEquivalentTo("ًٌٍَُِّْ".ToCharArray());
            }

            [Fact]
            public async Task can_read_maputf_aff()
            {
                var filePath = @"files/maputf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.RelatedCharacterMap.Should().HaveCount(3);
                actual.RelatedCharacterMap.First().ShouldBeEquivalentTo(new[] { "u", "ú", "ü" });
                actual.RelatedCharacterMap.Skip(1).First().ShouldBeEquivalentTo(new[] { "ö", "ó", "o" });
                actual.RelatedCharacterMap.Last().ShouldBeEquivalentTo(new[] { "ß", "ss" });
            }

            [Fact]
            public async Task can_read_morph_aff()
            {
                var filePath = @"files/morph.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be('P');
                actual.Prefixes.Single().Entries[0].MorphCode.ShouldBeEquivalentTo(new[] { "dp:pfx_un", "sp:un" });

                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.First().AFlag.Should().Be('S');
                actual.Suffixes.First().Entries[0].MorphCode.ShouldBeEquivalentTo(new[] { "is:plur" });
                actual.Suffixes.Skip(1).First().AFlag.Should().Be('Q');
                actual.Suffixes.Skip(1).First().Entries[0].MorphCode.ShouldBeEquivalentTo(new[] { "is:sg_3" });
                actual.Suffixes.Last().AFlag.Should().Be('R');
                actual.Suffixes.Last().Entries[0].MorphCode.ShouldBeEquivalentTo(new[] { "ds:der_able" });
            }

            [Fact]
            public async Task can_read_needaffix_aff()
            {
                var filePath = @"files/needaffix.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.NeedAffix.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().Entries.Should().HaveCount(1);
            }

            [Fact]
            public async Task can_read_nepali_aff()
            {
                var filePath = @"files/nepali.aff";
                var key1 = "‌"; // NOTE: this is not the empty string
                var value1_1 = "￰";
                var value1_2 = "‌"; // NOTE: this is not the empty string
                var key2 = "‍"; // NOTE: this is not the empty string
                var value2_2 = "￰";
                key1.Should().NotBeEmpty();
                key2.Should().NotBeEmpty();
                key2.Should().NotBe(key1);
                value1_1.Should().NotBeEmpty();
                value1_2.Should().NotBeEmpty();
                value1_2.Should().NotBe(value1_1);

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.IgnoredChars.Should().BeEquivalentTo(new[] { '￰' });
                actual.WordChars.Should().BeEquivalentTo("ःािीॉॊोौॎॏॕॖॗ‌‍".ToCharArray());

                actual.InputConversions.Should().HaveCount(4);

                actual.InputConversions[key1][0].Equals(value1_1, StringComparison.Ordinal).Should().BeTrue();
                actual.InputConversions[key1][(ReplacementValueType)1].Should().BeNull();
                actual.InputConversions[key1][(ReplacementValueType)2].Equals(value1_2, StringComparison.Ordinal).Should().BeTrue();
                actual.InputConversions[key1][(ReplacementValueType)3].Should().BeNull();

                actual.InputConversions[key2][0].Should().BeNull();
                actual.InputConversions[key2][(ReplacementValueType)1].Should().BeNull();
                actual.InputConversions[key2][(ReplacementValueType)2].Equals(value2_2, StringComparison.Ordinal).Should().BeTrue();
                actual.InputConversions[key2][(ReplacementValueType)3].Should().BeNull();

                actual.InputConversions["र्‌य"][0].Should().Be("र्‌य");

                actual.InputConversions["र्‌व"][0].Should().Be("र्‌व");
            }

            [Fact]
            public async Task can_read_ngram_utf_fix_aff()
            {
                var filePath = @"files/ngram_utf_fix.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Prefixes.Should().HaveCount(1);
                actual.Prefixes.Single().AFlag.Should().Be(101);
                actual.Prefixes.Single().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Prefixes.Single().Entries.Should().HaveCount(1);
                actual.Prefixes.Single().Entries.Single().Strip.Should().BeEmpty();
                actual.Prefixes.Single().Entries.Single().Append.Should().Be("пред");
                actual.Prefixes.Single().Entries.Single().Conditions.GetEncoded().Should().Be(".");

                actual.Suffixes.Should().HaveCount(3);
                actual.Suffixes.Skip(1).First().AFlag.Should().Be(2000);
                actual.Suffixes.Skip(1).First().Entries.Should().HaveCount(3);
                actual.Suffixes.Skip(1).First().Entries[1].Strip.Should().BeEmpty();
                actual.Suffixes.Skip(1).First().Entries[1].Append.Should().Be("ами");
                actual.Suffixes.Skip(1).First().Entries[1].Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_oconv_aff()
            {
                var filePath = @"files/oconv.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.OutputConversions.Should().HaveCount(7);
                actual.OutputConversions["a"][0].Should().Be("A");
                actual.OutputConversions["á"][0].Should().Be("Á");
                actual.OutputConversions["b"][0].Should().Be("B");
                actual.OutputConversions["c"][0].Should().Be("C");
                actual.OutputConversions["d"][0].Should().Be("D");
                actual.OutputConversions["e"][0].Should().Be("E");
                actual.OutputConversions["é"][0].Should().Be("É");
            }

            [Fact]
            public async Task can_read_onlyincompound2_aff()
            {
                var filePath = @"files/onlyincompound2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.OnlyInCompound.Should().Be('O');
                actual.CompoundFlag.Should().Be('A');
                actual.CompoundPermitFlag.Should().Be('P');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.First().AFlag.Should().Be('B');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                actual.Suffixes.First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries[0].Append.Should().Be("s");
                actual.Suffixes.First().Entries[0].ContClass.Should().ContainInOrder(new[] { 'O', 'P' });
                actual.Suffixes.First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns.Single().Pattern.Should().Be("0");
                actual.CompoundPatterns.Single().Condition.Should().Be('B');
                actual.CompoundPatterns.Single().Pattern2.Should().BeEmpty();
                actual.CompoundPatterns.Single().Condition2.Should().Be('A');
            }

            [Fact]
            public async Task can_read_opentaal_cpdpat_aff()
            {
                var filePath = @"files/opentaal_cpdpat.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundBegin.Should().Be('C' << 8 | 'a');
                actual.CompoundMiddle.Should().Be('C' << 8 | 'b');
                actual.CompoundEnd.Should().Be('C' << 8 | 'c');
                actual.CompoundPermitFlag.Should().Be('C' << 8 | 'p');
                actual.OnlyInCompound.Should().Be('C' << 8 | 'x');
                actual.CompoundPatterns.Should().HaveCount(1);
                actual.CompoundPatterns.Single().Pattern.Should().BeEmpty();
                actual.CompoundPatterns.Single().Condition.Should().Be('C' << 8 | 'h');
                actual.CompoundPatterns.Single().Pattern2.Should().BeEmpty();
                actual.CompoundPatterns.Single().Condition2.Should().Be('X' << 8 | 's');
                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.First().AFlag.Should().Be('C' << 8 | 'h');
                actual.Suffixes.First().Options.Should().Be(AffixEntryOptions.CrossProduct);
                actual.Suffixes.First().Entries.Should().HaveCount(2);
                actual.Suffixes.First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries[0].Append.Should().Be("s");
                actual.Suffixes.First().Entries[0].ContClass.Should().ContainInOrder(new[] { 'C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'x', 'C' << 8 | 'p' }.OrderBy(x => x));
                actual.Suffixes.First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.First().Entries[1].Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries[1].Append.Should().Be("s-");
                actual.Suffixes.First().Entries[1].ContClass.Should().ContainInOrder(new[] { 'C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'c', 'C' << 8 | 'p' }.OrderBy(x => x));
                actual.Suffixes.First().Entries[1].Conditions.GetEncoded().Should().Be(".");
            }

            [Fact]
            public async Task can_read_opentaal_cpdpat2_aff()
            {
                var filePath = @"files/opentaal_cpdpat2.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(new[] { '-' });
                actual.NoSplitSuggestions.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_phone_aff()
            {
                var filePath = @"files/phone.aff";
                var expectedPhoneRules = new[]
                {
                     "AH(AEIOUY)-^"
                    ,"AR(AEIOUY)-^"
                    ,"A(HR)^      "
                    ,"A^          "
                    ,"AH(AEIOUY)- "
                    ,"AR(AEIOUY)- "
                    ,"A(HR)       "
                    ,"BB-         "
                    ,"B           "
                    ,"CQ-         "
                    ,"CIA         "
                    ,"CH          "
                    ,"C(EIY)-     "
                    ,"CK          "
                    ,"COUGH^      "
                    ,"CC<         "
                    ,"C           "
                    ,"DG(EIY)     "
                    ,"DD-         "
                    ,"D           "
                    ,"É<          "
                    ,"EH(AEIOUY)-^"
                    ,"ER(AEIOUY)-^"
                    ,"E(HR)^      "
                    ,"ENOUGH^$    "
                    ,"E^          "
                    ,"EH(AEIOUY)- "
                    ,"ER(AEIOUY)- "
                    ,"E(HR)       "
                    ,"FF-         "
                    ,"F           "
                    ,"GN^         "
                    ,"GN$         "
                    ,"GNS$        "
                    ,"GNED$       "
                    ,"GH(AEIOUY)- "
                    ,"GH          "
                    ,"GG9         "
                    ,"G           "
                    ,"H           "
                    ,"IH(AEIOUY)-^"
                    ,"IR(AEIOUY)-^"
                    ,"I(HR)^      "
                    ,"I^          "
                    ,"ING6        "
                    ,"IH(AEIOUY)- "
                    ,"IR(AEIOUY)- "
                    ,"I(HR)       "
                    ,"J           "
                    ,"KN^         "
                    ,"KK-         "
                    ,"K           "
                    ,"LAUGH^      "
                    ,"LL-         "
                    ,"L           "
                    ,"MB$         "
                    ,"MM          "
                    ,"M           "
                    ,"NN-         "
                    ,"N           "
                    ,"OH(AEIOUY)-^"
                    ,"OR(AEIOUY)-^"
                    ,"O(HR)^      "
                    ,"O^          "
                    ,"OH(AEIOUY)- "
                    ,"OR(AEIOUY)- "
                    ,"O(HR)       "
                    ,"PH          "
                    ,"PN^         "
                    ,"PP-         "
                    ,"P           "
                    ,"Q           "
                    ,"RH^         "
                    ,"ROUGH^      "
                    ,"RR-         "
                    ,"R           "
                    ,"SCH(EOU)-   "
                    ,"SC(IEY)-    "
                    ,"SH          "
                    ,"SI(AO)-     "
                    ,"SS-         "
                    ,"S           "
                    ,"TI(AO)-     "
                    ,"TH          "
                    ,"TCH--       "
                    ,"TOUGH^      "
                    ,"TT-         "
                    ,"T           "
                    ,"UH(AEIOUY)-^"
                    ,"UR(AEIOUY)-^"
                    ,"U(HR)^      "
                    ,"U^          "
                    ,"UH(AEIOUY)- "
                    ,"UR(AEIOUY)- "
                    ,"U(HR)       "
                    ,"V^          "
                    ,"V           "
                    ,"WR^         "
                    ,"WH^         "
                    ,"W(AEIOU)-   "
                    ,"X^          "
                    ,"X           "
                    ,"Y(AEIOU)-   "
                    ,"ZZ-         "
                    ,"Z           "
                }
                .Select(p => p.Trim());

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("iso-8859-1");
                actual.Warnings.Should().BeEmpty();
                actual.Phone.Select(p => p.Rule).ShouldAllBeEquivalentTo(expectedPhoneRules);
                actual.Phone.First().Rule.Should().Be("AH(AEIOUY)-^");
                actual.Phone.First().Replace.Should().Be("*H");
                actual.Phone.Last().Rule.Should().Be("Z");
                actual.Phone.Last().Replace.Should().Be("S");
            }

            [Fact]
            public async Task can_read_rep_aff()
            {
                var filePath = @"files/rep.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().HaveCount(8);
                var replacements = actual.Replacements.ToList();
                replacements[0].Pattern.Should().Be("f");
                replacements[0].OutString.Should().Be("ph");
                replacements[0].Med.Should().Be("ph");
                replacements[0].Type.Should().Be(ReplacementValueType.Med);

                replacements[1].Pattern.Should().Be("ph");
                replacements[1].OutString.Should().Be("f");
                replacements[1].Med.Should().Be("f");
                replacements[1].Type.Should().Be(ReplacementValueType.Med);

                replacements[2].Pattern.Should().Be("shun");
                replacements[2].OutString.Should().Be("tion");
                replacements[2].Fin.Should().Be("tion");
                replacements[2].Type.Should().Be(ReplacementValueType.Fin);

                replacements[3].Pattern.Should().Be("alot");
                replacements[3].OutString.Should().Be("a lot");
                replacements[3].Isol.Should().Be("a lot");
                replacements[3].Type.Should().Be(ReplacementValueType.Isol);

                replacements[4].Pattern.Should().Be("foo");
                replacements[4].OutString.Should().Be("bar");
                replacements[4].Isol.Should().Be("bar");
                replacements[4].Type.Should().Be(ReplacementValueType.Isol);

                replacements[5].Pattern.Should().Be("'");
                replacements[5].OutString.Should().Be(" ");
                replacements[5].Med.Should().Be(" ");
                replacements[5].Type.Should().Be(ReplacementValueType.Med);

                replacements[6].Pattern.Should().StartWith("vinte");
                replacements[6].Pattern.Should().EndWith("n");
                replacements[6].OutString.Should().Be("vinte e un");
                replacements[6].Isol.Should().Be("vinte e un");
                replacements[6].Type.Should().Be(ReplacementValueType.Isol);

                replacements[7].Pattern.Should().Be("s");
                replacements[7].OutString.Should().Be("'s");
                replacements[7].Med.Should().Be("'s");
                replacements[7].Type.Should().Be(ReplacementValueType.Med);

                actual.Suffixes.Should().HaveCount(1);
                actual.Suffixes.Single().AFlag.Should().Be('A');
                actual.WordChars.Should().BeEquivalentTo(new[] { '\'' });
            }

            [Fact]
            public async Task can_read_reputf_aff()
            {
                var filePath = @"files/reputf.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements.Single().Pattern.Should().Be("oo");
                actual.Replacements.Single().OutString.Should().Be("őő");
                actual.Replacements.Single().Type.Should().Be(ReplacementValueType.Med);
            }

            [Fact]
            public async Task can_read_simplifiedtriple_aff()
            {
                var filePath = @"files/simplifiedtriple.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CheckCompoundTriple.Should().BeTrue();
                actual.SimplifiedTriple.Should().BeTrue();
                actual.CompoundMin.Should().Be(2);
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_slash_aff()
            {
                var filePath = @"files/slash.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.WordChars.ShouldBeEquivalentTo(@"/:".ToCharArray());
            }

            [Fact]
            public async Task can_read_sug_aff()
            {
                var filePath = @"files/sug.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().NotBeNull();
                var entry = actual.Replacements.Should().HaveCount(1).And.Subject.Single();
                entry.Pattern.Should().Be("alot");
                entry.OutString.Should().Be("a lot");
                entry.Type.Should().Be(ReplacementValueType.Med);
                entry.Med.Should().Be("a lot");
                actual.KeyString.Should().Be("qwertzuiop|asdfghjkl|yxcvbnm|aq");
                actual.WordChars.Should().BeEquivalentTo(new[] { '.' });
                actual.ForbiddenWord.Should().Be('?');
            }

            [Fact]
            public async Task can_read_utf8_bom_aff()
            {
                var filePath = @"files/utf8_bom.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Encoding.WebName.Should().Be("utf-8");
            }

            [Fact]
            public async Task can_read_utfcompound_aff()
            {
                var filePath = @"files/utfcompound.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.CompoundMin.Should().Be(3);
                actual.CompoundFlag.Should().Be('A');
            }

            [Fact]
            public async Task can_read_warn_add()
            {
                var filePath = @"files/warn.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Warn.Should().Be('W');
                actual.Suffixes.Should().HaveCount(1);
                actual.Replacements.Should().HaveCount(1);
                actual.Replacements.Single().Pattern.Should().Be("foo");
                actual.Replacements.Single().OutString.Should().Be("bar");
                actual.Replacements.Single().Type.Should().Be(ReplacementValueType.Med);
            }

            [Fact]
            public async Task can_read_zeroaffix_aff()
            {
                var filePath = @"files/zeroaffix.aff";

                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.NeedAffix.Should().Be('X');
                actual.CompoundFlag.Should().Be('Y');

                actual.Suffixes.Should().HaveCount(3);

                actual.Suffixes.First().AFlag.Should().Be('A');
                actual.Suffixes.First().Entries.Should().HaveCount(1);
                actual.Suffixes.First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.First().Entries[0].Append.Should().BeEmpty();
                actual.Suffixes.First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.First().Entries[0].MorphCode.Should().OnlyContain(x => x == ">");

                actual.Suffixes.Skip(1).First().AFlag.Should().Be('B');
                actual.Suffixes.Skip(1).First().Entries.Should().HaveCount(1);
                actual.Suffixes.Skip(1).First().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.Skip(1).First().Entries[0].Append.Should().BeEmpty();
                actual.Suffixes.Skip(1).First().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.Skip(1).First().Entries[0].MorphCode.Should().OnlyContain(x => x == "<ZERO>>");

                actual.Suffixes.Last().AFlag.Should().Be('C');
                actual.Suffixes.Last().Entries.Should().HaveCount(2);
                actual.Suffixes.Last().Entries[0].Strip.Should().BeEmpty();
                actual.Suffixes.Last().Entries[0].Append.Should().BeEmpty();
                actual.Suffixes.Last().Entries[0].ContClass.Should().ContainInOrder(new[] { 'X', 'A', 'B' }.OrderBy(x => x));
                actual.Suffixes.Last().Entries[0].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.Last().Entries[0].MorphCode.Should().OnlyContain(x => x == "<ZERODERIV>");
                actual.Suffixes.Last().Entries[1].Strip.Should().BeEmpty();
                actual.Suffixes.Last().Entries[1].Append.Should().Be("baz");
                actual.Suffixes.Last().Entries[1].ContClass.Should().ContainInOrder(new[] { 'X', 'A', 'B' }.OrderBy(x => x));
                actual.Suffixes.Last().Entries[1].Conditions.GetEncoded().Should().Be(".");
                actual.Suffixes.Last().Entries[1].MorphCode.Should().OnlyContain(x => x == "<DERIV>");
            }

            public static IEnumerable<object[]> can_read_file_without_exception_args =>
                Directory.GetFiles("files/", "*.aff").Select(filePath => new object[] { filePath });

            public static string[] can_read_file_without_exception_warning_exceptions = new[]
            {
                "base_utf.aff" // this file has some strange morph lines at the bottom, maybe a bug?
            };

            [Theory, MemberData(nameof(can_read_file_without_exception_args))]
            public async Task can_read_file_without_exception(string filePath)
            {
                var actual = await AffixReader.ReadFileAsync(filePath);

                actual.Should().NotBeNull();

                if (!can_read_file_without_exception_warning_exceptions.Contains(Path.GetFileName(filePath)))
                {
                    actual.Warnings.Should().BeEmpty();
                }
            }
        }

        public class ReadAsync : AffixReaderTests
        {
            [Theory]
            [InlineData("de-DE")]
            [InlineData("de")]
            [InlineData("en-US")]
            [InlineData("en_US")]
            [InlineData("tr_TR")]
            [InlineData("en-UK")]
            [InlineData("hu-HU")]
            [InlineData("it")]
            [InlineData("ar")]
            [InlineData("uk")]
            [InlineData("xx")]
            [InlineData("")]
            [InlineData("-")]
            [InlineData("en-XX")]
            [InlineData("en-")]
            public async Task can_read_all_languages(string langCode)
            {
                var textFileContents = "LANG " + langCode;
                var expectedCulture = langCode;
                if (expectedCulture.EndsWith("-"))
                {
                    expectedCulture = expectedCulture.Substring(0, expectedCulture.Length - 1);
                }
                expectedCulture = expectedCulture.Replace('_', '-');

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.Language.Should().Be(langCode);
                actual.Culture.Should().NotBeNull();

                if (string.Equals(expectedCulture, actual.Culture.Name))
                {
                    actual.Culture.Name.Should().Be(expectedCulture);
                }
                else if (!string.IsNullOrEmpty(actual.Culture.Name))
                {
                    expectedCulture.Should().StartWith(actual.Culture.Name, "Not all platforms have the values but they should be similar.");
                }
                else
                {
                    actual.Culture.Name.Should().BeEmpty("Okay well maybe not all platforms can even keep the culture name then?");
                }
            }

            [Theory]
            [InlineData("klmc")]
            [InlineData("ःािीॉॊोौॎॏॕॖॗ‌‍")]
            public async Task can_read_syllablenum(string parameters)
            {
                var textFileContents = "SYLLABLENUM " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundSyllableNum.Should().Be(parameters);
            }

            [Theory]
            [InlineData("0", 0)]
            [InlineData("5", 5)]
            [InlineData("words", null)]
            public async Task can_read_compoundwordmax(string parameters, int? expected)
            {
                var textFileContents = "COMPOUNDWORDMAX " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundWordMax.Should().Be(expected);
            }

            [Theory]
            [InlineData("-1", 1)]
            [InlineData("0", 1)]
            [InlineData("1", 1)]
            [InlineData("2", 2)]
            [InlineData("words", 3)]
            public async Task can_read_compoundmin(string parameters, int expected)
            {
                var textFileContents = "COMPOUNDMIN " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundMin.Should().Be(expected);
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

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundRoot.Should().Be(expected);
            }

            [Theory]
            [InlineData("6 aáeéiíoóöőuúüű", 6, "aáeéiíoóöőuúüű")]
            [InlineData("1 abc", 1, "abc")]
            [InlineData("1", 1, "AEIOUaeiou")]
            [InlineData("abc", 0, "")]
            public async Task can_read_compoundsyllable(string parameters, int expectedNumber, string expectedLettersText)
            {
                var textFileContents = "COMPOUNDSYLLABLE " + parameters;
                var expectedLetters = expectedLettersText.ToCharArray();
                Array.Sort(expectedLetters);

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundMaxSyllable.Should().Be(expectedNumber);
                actual.CompoundVowels.ShouldBeEquivalentTo(expectedLetters);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("=", '=')]
            public async Task can_read_nosuggest(string parameters, int expectedFlag)
            {
                var textFileContents = "NOSUGGEST " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.NoSuggest.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("=", '=')]
            public async Task can_read_nongramsuggest(string parameters, int expectedFlag)
            {
                var textFileContents = "NONGRAMSUGGEST " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.NoNgramSuggest.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData(")", ')')]
            public async Task can_read_lemma_present(string parameters, int expectedFlag)
            {
                var textFileContents = "LEMMA_PRESENT " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.LemmaPresent.Should().Be(expectedFlag);
            }

            [Theory]
            [InlineData("Magyar 1.6", "Magyar 1.6")]
            [InlineData("", null)]
            public async Task can_read_version(string parameters, string expected)
            {
                var textFileContents = "VERSION " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.Version.Should().Be(expected);
            }

            [Theory]
            [InlineData("1", 1)]
            [InlineData("0", 0)]
            [InlineData("", null)]
            public async Task can_read_maxdiff(string parameters, int? expected)
            {
                var textFileContents = "MAXDIFF " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.MaxDifferency.Should().Be(expected);
            }

            [Theory]
            [InlineData("1", 1)]
            [InlineData("0", 0)]
            [InlineData("", 3)]
            public async Task can_read_maxcpdsugs(string parameters, int expected)
            {
                var textFileContents = "MAXCPDSUGS " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.MaxCompoundSuggestions.Should().Be(expected);
            }

            [Theory]
            [InlineData("A", 'A')]
            [InlineData("&", '&')]
            public async Task can_read_substandard(string parameters, int expectedFlag)
            {
                var textFileContents = "SUBSTANDARD " + parameters;

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.SubStandard.Should().Be(expectedFlag);
            }

            [Fact]
            public async Task can_read_compoundmoresuffixes()
            {
                var textFileContents = "COMPOUNDMORESUFFIXES";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CompoundMoreSuffixes.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_checknum()
            {
                var textFileContents = "CHECKNUM";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.CheckNum.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_onlymaxdiff()
            {
                var textFileContents = "ONLYMAXDIFF";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.OnlyMaxDiff.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_sugswithdots()
            {
                var textFileContents = "SUGSWITHDOTS";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.SuggestWithDots.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_forbidwarn()
            {
                var textFileContents = "FORBIDWARN";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.ForbidWarn.Should().BeTrue();
            }

            [Fact]
            public async Task can_read_unknown_command()
            {
                var textFileContents = "UNKNOWN arguments";

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.Should().NotBeNull();
            }

            [Theory]
            [InlineData("abc", "def", "abc", "def", ReplacementValueType.Med)]
            [InlineData("^abc", "d_e_f", "abc", "d e f", ReplacementValueType.Ini)]
            [InlineData("a_b_c$", "d_e_f", "a b c", "d e f", ReplacementValueType.Fin)]
            [InlineData("^a_b_c$", "d_e_f", "a b c", "d e f", ReplacementValueType.Isol)]
            public async Task can_read_all_rep_types(string pattern, string outText, string expectedPattern, string expectedOutString, ReplacementValueType expectedType)
            {
                var textFileContents = $"REP {pattern} {outText}";
                string expectedMed = null;
                string expectedIni = null;
                string expectedFin = null;
                string expectedIsol = null;

                switch (expectedType)
                {
                    case ReplacementValueType.Med:
                        expectedMed = expectedOutString;
                        break;
                    case ReplacementValueType.Ini:
                        expectedIni = expectedOutString;
                        break;
                    case ReplacementValueType.Fin:
                        expectedFin = expectedOutString;
                        break;
                    case ReplacementValueType.Isol:
                        expectedIsol = expectedOutString;
                        break;
                }

                var actual = await AffixReader.ReadAsync(new Utf16StringLineReader(textFileContents));

                actual.Replacements.Should().HaveCount(1);
                var rep = actual.Replacements.Single();
                rep.Pattern.Should().Be(expectedPattern);
                rep.OutString.Should().Be(expectedOutString);
                rep.Type.Should().Be(expectedType);
                rep.Med.Should().Be(expectedMed);
                rep.Ini.Should().Be(expectedIni);
                rep.Fin.Should().Be(expectedFin);
                rep.Isol.Should().Be(expectedIsol);
            }
        }

        protected string Reversed(string text)
        {
            var letters = text.ToCharArray();
            Array.Reverse(letters);
            return new string(letters);
        }
    }
}
