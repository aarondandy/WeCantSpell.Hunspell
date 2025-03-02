using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class AffixReaderTests
{
    static CancellationToken TestCancellation => TestContext.Current.CancellationToken;

    static AffixReaderTests()
    {
        Helpers.EnsureEncodingsReady();
    }

    public class ReadFileAsync : AffixReaderTests
    {
        [Fact]
        public async Task can_read_1463589_aff()
        {
            var filePath = @"files/1463589.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(1);
        }

        [Fact]
        public async Task can_read_1463589_utf_aff()
        {
            var filePath = @"files/1463589_utf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");
            actual.MaxNgramSuggestions.ShouldBe(1);
        }

        [Fact]
        public async Task can_read_1592880_aff()
        {
            var filePath = @"files/1592880.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-1");

            actual.Suffixes.ShouldHaveCount(4);
            var suffixes = actual.Suffixes.ToList();

            var suffixGroup1 = suffixes[0];
            suffixGroup1.AFlag.ShouldBeValue('N');
            suffixGroup1.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var entry = suffixGroup1.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("n");
            entry.Conditions.GetEncoded().ShouldBe(".");

            var suffixGroup2 = suffixes[1];
            suffixGroup2.AFlag.ShouldBeValue('S');
            suffixGroup2.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            entry = suffixGroup2.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("s");
            entry.Conditions.GetEncoded().ShouldBe(".");

            var suffixGroup3 = suffixes[2];
            suffixGroup3.AFlag.ShouldBeValue('P');
            suffixGroup3.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            entry = suffixGroup3.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("en");
            entry.Conditions.GetEncoded().ShouldBe(".");

            var suffixGroup4 = suffixes[3];
            suffixGroup4.AFlag.ShouldBeValue('Q');
            suffixGroup4.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            suffixGroup4.ShouldHaveCount(2);
            entry = suffixGroup4[0];
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("e");
            entry.Conditions.GetEncoded().ShouldBe(".");
            entry = suffixGroup4[1];
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("en");
            entry.Conditions.GetEncoded().ShouldBe(".");

            actual.CompoundEnd.ShouldBeValue('z');
            actual.CompoundPermitFlag.ShouldBeValue('c');
            actual.OnlyInCompound.ShouldBeValue('o');
        }

        [Fact]
        public async Task can_read_1695964_aff()
        {
            var filePath = @"files/1695964.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.TryString.ShouldBe("esianrtolcdugmphbyfvkwESIANRTOLCDUGMPHBYFVKW");
            actual.MaxNgramSuggestions.ShouldBe(0);
            actual.NeedAffix.ShouldBeValue('h');
            actual.Suffixes.ShouldHaveCount(2);

            var suffixGroup1 = actual.Suffixes.ElementAt(0);
            suffixGroup1.AFlag.ShouldBeValue('S');
            suffixGroup1.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var entry = suffixGroup1.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("s");
            entry.Conditions.GetEncoded().ShouldBe(".");

            var suffixGroup2 = actual.Suffixes.ElementAt(1);
            suffixGroup2.AFlag.ShouldBeValue('e');
            suffixGroup2.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            entry = suffixGroup2.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("e");
            entry.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_1706659_aff()
        {
            var filePath = @"files/1706659.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-1");
            actual.TryString.ShouldBe("esijanrtolcdugmphbyfvkwqxz");
            var group = actual.Suffixes.ShouldHaveSingleItem();
            group.AFlag.ShouldBeValue('A');
            group.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            group.ShouldHaveCount(5);
            group.Select(e => e.Append).ShouldBe(
            [
                "e",
                "er",
                "en",
                "em",
                "es"
            ]);
            group.ShouldAllBe(e => e.Strip == string.Empty);
            group.ShouldAllBe(e => e.Conditions.GetEncoded() == ".");

            actual.CompoundRules.ShouldHaveSingleItem().ShouldBeValues(['v', 'w']);
        }

        [Fact]
        public async Task can_read_1975530_aff()
        {
            var filePath = @"files/1975530.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");
            actual.IgnoredChars.ShouldBeValues([1600, 1612, 1613, 1614, 1615, 1616, 1617, 1618], ignoreOrder: true);
            var prefixGroup1 = actual.Prefixes.ShouldHaveSingleItem();
            prefixGroup1.AFlag.ShouldBeValue('x');
            prefixGroup1.Options.ShouldBe(AffixEntryOptions.None);
            var prefixEntry = prefixGroup1.ShouldHaveSingleItem();
            prefixEntry.Append.ShouldBe("ت");
            prefixEntry.Conditions.GetEncoded().ShouldBe("أ[^ي]");
            prefixEntry.Strip.ShouldBe("أ");
        }

        [Fact]
        public async Task can_read_2970240_aff()
        {
            var filePath = @"files/2970240.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('c');
            var pattern = actual.CompoundPatterns.ShouldHaveSingleItem();
            pattern.Pattern.ShouldBe("le");
            pattern.Pattern2.ShouldBe("fi");
        }

        [Fact]
        public async Task can_read_2970242_aff()
        {
            var filePath = @"files/2970242.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('c');
            var pattern = actual.CompoundPatterns.ShouldHaveSingleItem();
            pattern.Pattern.ShouldBeEmpty();
            pattern.Condition.ShouldBeValue('a');
            pattern.Pattern2.ShouldBeEmpty();
            pattern.Condition2.ShouldBeValue('b');
            pattern.Pattern3.ShouldBeNullOrEmpty();
        }

        [Fact]
        public async Task can_read_2999225_aff()
        {
            var filePath = @"files/2999225.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundRules.ShouldHaveSingleItem().ShouldBeValues(['a', 'b']);
            actual.CompoundBegin.ShouldBeValue('A');
            actual.CompoundEnd.ShouldBeValue('B');
        }

        [Fact]
        public async Task can_read_affixes_aff()
        {
            var filePath = @"files/affixes.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            var prefix = actual.Prefixes.ShouldHaveSingleItem();
            prefix.AFlag.ShouldBeValue('A');
            prefix.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var prefixEntry = prefix.ShouldHaveSingleItem();
            prefixEntry.Strip.ShouldBeEmpty();
            prefixEntry.Append.ShouldBe("re");
            prefixEntry.Conditions.GetEncoded().ShouldBe(".");

            var suffix = actual.Suffixes.ShouldHaveSingleItem();
            suffix.AFlag.ShouldBeValue('B');
            suffix.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            suffix.ShouldHaveCount(2);
            var suffixEntry1 = suffix[0];
            suffixEntry1.Strip.ShouldBeEmpty();
            suffixEntry1.Append.ShouldBe("ed");
            suffixEntry1.Conditions.GetEncoded().ShouldBe("[^y]");
            var suffixEntry2 = suffix[1];
            suffixEntry2.Strip.ShouldBe("y");
            suffixEntry2.Append.ShouldBe("ied");
            suffixEntry2.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_alias_aff()
        {
            var filePath = @"files/alias.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.AliasF.ShouldHaveCount(2);
            actual.AliasF[0].ShouldBeValues(['A', 'B']);
            actual.AliasF[1].ShouldBeValues(['A']);
            actual.Suffixes.ShouldHaveCount(2);
            var suffix = actual.Suffixes.ElementAt(0);
            suffix.AFlag.ShouldBeValue('A');
            suffix.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
            var entry = suffix.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("x");
            entry.Conditions.GetEncoded().ShouldBe(".");
            suffix = actual.Suffixes.ElementAt(1);
            suffix.AFlag.ShouldBeValue('B');
            suffix.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF);
            entry = suffix.ShouldHaveSingleItem();
            entry.Strip.ShouldBeEmpty();
            entry.Append.ShouldBe("y");
            entry.ContClass.ShouldBeValues(['A']);
            entry.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_alias2_aff()
        {
            var filePath = @"files/alias2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.AliasF.ShouldHaveCount(2);
            actual.AliasF[0].ShouldBeValues(['A', 'B']);
            actual.AliasF[1].ShouldBeValues(['A']);

            actual.AliasM.ShouldHaveCount(3);
            actual.AliasM[0].ShouldAllBe(x => x == "is:affix_x");
            actual.AliasM[1].ShouldAllBe(x => x == "ds:affix_y");
            actual.AliasM[2].ShouldBe(["po:noun", "xx:other_data"]);

            actual.Suffixes.ShouldHaveCount(2);

            var suffixGroup = actual.Suffixes.ElementAt(0);
            suffixGroup.AFlag.ShouldBeValue('A');
            suffixGroup.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
            var suffixEntry1 = suffixGroup.ShouldHaveSingleItem();
            suffixEntry1.Strip.ShouldBeEmpty();
            suffixEntry1.Append.ShouldBe("x");
            suffixEntry1.Conditions.GetEncoded().ShouldBe(".");
            suffixEntry1.MorphCode.ShouldAllBe(x => x == "is:affix_x");

            suffixGroup = actual.Suffixes.ElementAt(1);
            suffixGroup.AFlag.ShouldBeValue('B');
            suffixGroup.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasF | AffixEntryOptions.AliasM);
            var suffixEntry2 = suffixGroup.ShouldHaveSingleItem();
            suffixEntry2.Strip.ShouldBeEmpty();
            suffixEntry2.Append.ShouldBe("y");
            suffixEntry2.ContClass.ShouldBeValues(['A']);
            suffixEntry2.Conditions.GetEncoded().ShouldBe(".");
            suffixEntry2.MorphCode.ShouldAllBe(x => x == "ds:affix_y");
        }

        [Fact]
        public async Task can_read_alias3_aff()
        {
            var filePath = @"files/alias3.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.ComplexPrefixes.ShouldBeTrue();
            actual.WordChars.ShouldBeValues(['_']);
            actual.AliasM.ShouldBe(
            [
                new MorphSet([@"affix_1/".Reversed()]),
                new MorphSet([@"affix_2/".Reversed()]),
                new MorphSet([@"/suffix_1".Reversed()]),
                new MorphSet([@"[stem_1]".Reversed()])
            ]);

            actual.Suffixes.ShouldHaveCount(2);

            var suffixGroup1 = actual.Suffixes.ElementAt(0);
            suffixGroup1.AFlag.ShouldBeValue('A');
            suffixGroup1.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
            var suffixEntry1 = suffixGroup1.ShouldHaveSingleItem();
            suffixEntry1.Strip.ShouldBeEmpty();
            suffixEntry1.Append.ShouldBe("ket");
            suffixEntry1.Conditions.GetEncoded().ShouldBe(".");
            suffixEntry1.MorphCode.ShouldHaveSingleItem().ShouldBe(@"affix_1/".Reversed());

            var suffixGroup2 = actual.Suffixes.ElementAt(1);
            suffixGroup2.AFlag.ShouldBeValue('B');
            suffixGroup2.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
            var suffixEntry2 = suffixGroup2.ShouldHaveSingleItem();
            suffixEntry2.Strip.ShouldBeEmpty();
            suffixEntry2.Append.ShouldBe("tem");
            suffixEntry2.ContClass.ShouldBeValues(['A']);
            suffixEntry2.Conditions.GetEncoded().ShouldBe(".");
            suffixEntry2.MorphCode.ShouldHaveSingleItem().ShouldBe(@"affix_2/".Reversed());

            var prefixGroup1 = actual.Prefixes.ShouldHaveSingleItem();
            prefixGroup1.AFlag.ShouldBeValue('C');
            prefixGroup1.Options.ShouldBe(AffixEntryOptions.CrossProduct | AffixEntryOptions.AliasM);
            var prefixEntry1 = prefixGroup1.ShouldHaveSingleItem();
            prefixEntry1.Strip.ShouldBeEmpty();
            prefixEntry1.Append.ShouldBe("_tset_");
            prefixEntry1.Conditions.GetEncoded().ShouldBe(".");
            prefixEntry1.MorphCode.ShouldHaveSingleItem().ShouldBe(@"/suffix_1".Reversed());
        }

        [Fact]
        public async Task can_read_allcaps_aff()
        {
            var filePath = @"files/allcaps.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe(['\'', '.']);

            var group = actual.Suffixes.ShouldHaveSingleItem();
            group.AFlag.ShouldBeValue('S');
            group.Options.ShouldBe(AffixEntryOptions.None);
            var entry1 = group.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("'s");
            entry1.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_allcaps_utf_aff()
        {
            var filePath = @"files/allcaps_utf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");

            actual.WordChars.ShouldBe(['\'', '.'], ignoreOrder: true);

            var group = actual.Suffixes.ShouldHaveSingleItem();
            group.AFlag.ShouldBeValue('S');
            group.Options.ShouldBe(AffixEntryOptions.None);

            var entry1 = group.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("'s");
            entry1.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_allcaps2_aff()
        {
            var filePath = @"files/allcaps2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.ForbiddenWord.ShouldBeValue('*');

            var group = actual.Suffixes.ShouldHaveSingleItem();
            group.AFlag.ShouldBeValue('s');
            group.Options.ShouldBe(AffixEntryOptions.None);

            var entry1 = group.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("os");
            entry1.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_allcaps3_aff()
        {
            var filePath = @"files/allcaps3.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe(['\'']);

            actual.Suffixes.ShouldHaveCount(2);

            var suffixGroup1 = actual.Suffixes.ElementAt(0);
            suffixGroup1.AFlag.ShouldBeValue('s');
            suffixGroup1.Options.ShouldBe(AffixEntryOptions.None);
            var entry1 = suffixGroup1.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("s");
            entry1.Conditions.GetEncoded().ShouldBe(".");

            var suffixGroup2 = actual.Suffixes.ElementAt(1);
            suffixGroup2.AFlag.ShouldBeValue('S');
            suffixGroup2.Options.ShouldBe(AffixEntryOptions.None);
            var entry2 = suffixGroup2.ShouldHaveSingleItem();
            entry2.Strip.ShouldBeEmpty();
            entry2.Append.ShouldBe("\'s");
            entry2.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_arabic_aff()
        {
            var filePath = @"files/arabic.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");
            actual.TryString.ShouldBe("أ");
            actual.IgnoredChars.ShouldBe(['ّ', 'َ', 'ُ', 'ٌ', 'ْ', 'ِ', 'ٍ'], ignoreOrder: true);

            var group1 = actual.Prefixes.ShouldHaveSingleItem();
            group1.AFlag.ShouldBeValue('A');
            group1.Options.ShouldBe(AffixEntryOptions.CrossProduct);

            var entry1 = group1.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBeEmpty();
            entry1.ContClass.ShouldBeValues(['0', 'X']);
            entry1.Conditions.GetEncoded().ShouldBe("أ[^ي]");
        }

        [Fact]
        public async Task can_read_base_aff()
        {
            var filePath = @"files/base.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-1");
            actual.WordChars.ShouldBe(['\'', '.']);
            actual.TryString.ShouldBe("esianrtolcdugmphbyfvkwz'");

            actual.Prefixes.ShouldHaveCount(7);
            var prefixGroup1 = actual.Prefixes.ElementAt(0);
            prefixGroup1.AFlag.ShouldBeValue('A');
            prefixGroup1.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var entry1 = prefixGroup1.ShouldHaveSingleItem();
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("re");
            entry1.Conditions.GetEncoded().ShouldBe(".");

            actual.Suffixes.ShouldHaveCount(16);

            actual.Replacements.ShouldHaveCount(88);
            var replacements = actual.Replacements.ToList();
            replacements[0].Pattern.ShouldBe("a");
            replacements[0].OutString.ShouldBe("ei");
            replacements[0].Type.ShouldBe(ReplacementValueType.Med);
            replacements[87].Pattern.ShouldBe("shun");
            replacements[87].OutString.ShouldBe("cion");
            replacements[87].Type.ShouldBe(ReplacementValueType.Med);
        }

        [Fact]
        public async Task can_read_base_utf_aff()
        {
            var filePath = @"files/base_utf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.TryString.ShouldBe("esianrtolcdugmphbyfvkwzESIANRTOLCDUGMPHBYFVKWZ'");
            actual.MaxNgramSuggestions.ShouldBe(1);
            actual.WordChars.ShouldBe(['.', '\'', '’' ], ignoreOrder: true);
            actual.Prefixes.ShouldHaveCount(7);
            actual.Suffixes.ShouldHaveCount(16);
            actual.Replacements.ShouldHaveCount(88);
        }

        [Fact]
        public async Task can_read_break_aff()
        {
            var filePath = @"files/break.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.BreakPoints.ShouldBe(["-", "–"]);

            actual.WordChars.ShouldBe(['-', '–']);
        }

        [Fact]
        public async Task can_read_breakdefault_aff()
        {
            var filePath = @"files/breakdefault.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);
            actual.WordChars.ShouldBe(['-']);
            actual.TryString.ShouldBe("ot");
        }

        [Fact]
        public async Task can_read_breakoff_aff()
        {
            var filePath = @"files/breakoff.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);
            actual.WordChars.ShouldBe(['-']);
            actual.TryString.ShouldBe("ot");
            actual.BreakPoints.ShouldBeEmpty();
        }

        [Fact]
        public async Task can_read_checkcompoundcase_aff()
        {
            var filePath = @"files/checkcompoundcase.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckCompoundCase.ShouldBeTrue();
            actual.CompoundFlag.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_checkcompounddup_aff()
        {
            var filePath = @"files/checkcompounddup.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckCompoundDup.ShouldBeTrue();
            actual.CompoundFlag.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_checkcompoundpattern_aff()
        {
            var filePath = @"files/checkcompoundpattern.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('A');
            actual.CompoundPatterns.ShouldHaveCount(2);
            actual.CompoundPatterns[0].Pattern.ShouldBe("nny");
            actual.CompoundPatterns[0].Pattern2.ShouldBe("ny");
            actual.CompoundPatterns[1].Pattern.ShouldBe("ssz");
            actual.CompoundPatterns[1].Pattern2.ShouldBe("sz");
            actual.SimplifiedCompound.ShouldBeFalse();
        }

        [Fact]
        public async Task can_read_checkcompoundpattern2_aff()
        {
            var filePath = @"files/checkcompoundpattern2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('A');
            actual.CompoundPatterns.ShouldHaveCount(2);
            actual.CompoundPatterns[0].Pattern.ShouldBe("o");
            actual.CompoundPatterns[0].Pattern2.ShouldBe("b");
            actual.CompoundPatterns[0].Pattern3.ShouldBe("z");
            actual.CompoundPatterns[1].Pattern.ShouldBe("oo");
            actual.CompoundPatterns[1].Pattern2.ShouldBe("ba");
            actual.CompoundPatterns[1].Pattern3.ShouldBe("u");
            actual.CompoundMin.ShouldBe(1);
            actual.SimplifiedCompound.ShouldBeTrue();
        }

        [Fact]
        public async Task can_read_checkcompoundpattern3_aff()
        {
            var filePath = @"files/checkcompoundpattern3.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('A');
            var compoundPattern = actual.CompoundPatterns.ShouldHaveSingleItem();
            compoundPattern.Pattern.ShouldBe("o");
            compoundPattern.Condition.ShouldBeValue('X');
            compoundPattern.Pattern2.ShouldBe("b");
            compoundPattern.Condition2.ShouldBeValue('Y');
            compoundPattern.Pattern3.ShouldBe("z");
            actual.CompoundMin.ShouldBe(1);
            actual.SimplifiedCompound.ShouldBeTrue();
        }

        [Fact]
        public async Task can_read_checkcompoundpattern4_aff()
        {
            var filePath = @"files/checkcompoundpattern4.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('x');
            actual.CompoundMin.ShouldBe(1);
            actual.CompoundPatterns.ShouldHaveCount(2);
            actual.CompoundPatterns[0].Pattern.ShouldBe("a");
            actual.CompoundPatterns[0].Condition.ShouldBeValue('A');
            actual.CompoundPatterns[0].Pattern2.ShouldBe("u");
            actual.CompoundPatterns[0].Condition2.ShouldBeValue('A');
            actual.CompoundPatterns[0].Pattern3.ShouldBe("O");
            actual.CompoundPatterns[1].Pattern.ShouldBe("u");
            actual.CompoundPatterns[1].Condition.ShouldBeValue('B');
            actual.CompoundPatterns[1].Pattern2.ShouldBe("u");
            actual.CompoundPatterns[1].Condition2.ShouldBeValue('B');
            actual.CompoundPatterns[1].Pattern3.ShouldBe("u");
            actual.SimplifiedCompound.ShouldBeTrue();
        }

        [Fact]
        public async Task can_read_checkcompoundrep_aff()
        {
            var filePath = @"files/checkcompoundrep.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckCompoundRep.ShouldBeTrue();
            actual.CompoundFlag.ShouldBeValue('A');
            var r = actual.Replacements.ShouldHaveSingleItem();
            r.Pattern.ShouldNotBeNullOrEmpty();
            r.OutString.ShouldBe("i");
            r.Type.ShouldBe(ReplacementValueType.Med);
        }

        [Fact]
        public async Task can_read_checkcompoundtriple_aff()
        {
            var filePath = @"files/checkcompoundtriple.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckCompoundTriple.ShouldBeTrue();
            actual.CompoundFlag.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_checksharps_aff()
        {
            var filePath = @"files/checksharps.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-1");
            actual.WordChars.ShouldBe(['.', 'ß'], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_checksharpsutf_aff()
        {
            var filePath = @"files/checksharpsutf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");
            actual.CheckSharps.ShouldBeTrue();
            actual.WordChars.ShouldBe(['ß', '.'], ignoreOrder: true);
            actual.KeepCase.ShouldBeValue('k');
        }

        [Fact]
        public async Task can_read_circumfix_aff()
        {
            var filePath = @"files/circumfix.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Circumfix.ShouldBeValue('X');

            actual.Prefixes.ShouldHaveCount(2);

            var pg = actual.Prefixes.ElementAt(0);
            pg.AFlag.ShouldBeValue('A');
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            pg.ShouldHaveSingleItem();

            var entry1 = pg[0];
            entry1.Strip.ShouldBeEmpty();
            entry1.Append.ShouldBe("leg");
            entry1.ContClass.ShouldBeValues(['X']);
            entry1.Conditions.GetEncoded().ShouldBe(".");

            pg = actual.Prefixes.ElementAt(1);
            pg.AFlag.ShouldBeValue('B');
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            pg.ShouldHaveSingleItem();

            var entry2 = actual.Prefixes.ElementAt(1)[0];
            entry2.Strip.ShouldBeEmpty();
            entry2.Append.ShouldBe("legesleg");
            entry2.ContClass.ShouldBeValues(['X']);
            entry2.Conditions.GetEncoded().ShouldBe(".");

            var sg = actual.Suffixes.ShouldHaveSingleItem();
            sg.AFlag.ShouldBeValue('C');
            sg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            sg.ShouldHaveCount(3);

            var entry3 = sg[0];
            entry3.Strip.ShouldBeEmpty();
            entry3.Append.ShouldBe("obb");
            entry3.Conditions.GetEncoded().ShouldBe(".");
            entry3.MorphCode.ShouldAllBe(x => x == "is:COMPARATIVE");

            var entry4 = sg[1];
            entry4.Strip.ShouldBeEmpty();
            entry4.Append.ShouldBe("obb");
            entry4.ContClass.ShouldBeValues(['A', 'X']);
            entry4.Conditions.GetEncoded().ShouldBe(".");
            entry4.MorphCode.ShouldAllBe(x => x == "is:SUPERLATIVE");

            var entry5 = sg[2];
            entry5.Strip.ShouldBeEmpty();
            entry5.Append.ShouldBe("obb");
            entry5.ContClass.ShouldBeValues(['B', 'X']);
            entry5.Conditions.GetEncoded().ShouldBe(".");
            entry5.MorphCode.ShouldAllBe(x => x == "is:SUPERSUPERLATIVE");
        }

        [Fact]
        public async Task can_read_colons_in_words_aff()
        {
            var filePath = @"files/colons_in_words.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe(":");
        }

        [Fact]
        public async Task can_read_compoundaffix2_aff()
        {
            var filePath = @"files/compoundaffix2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('X');
            actual.CompoundPermitFlag.ShouldBeValue('Y');
            actual.Prefixes.ShouldHaveSingleItem();
            actual.Suffixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_compoundaffix3_aff()
        {
            var filePath = @"files/compoundaffix3.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('X');
            actual.CompoundForbidFlag.ShouldBeValue('Z');
            actual.Prefixes.ShouldHaveSingleItem();
            actual.Suffixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_compoundrule2_aff()
        {
            var filePath = @"files/compoundrule2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(1);
            actual.CompoundRules.ShouldHaveSingleItem().ShouldBeValues(['A', '*', 'B', '*', 'C', '*']);
        }

        [Fact]
        public async Task can_read_compoundrule3_aff()
        {
            var filePath = @"files/compoundrule3.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(1);
            actual.CompoundRules.ShouldHaveSingleItem().ShouldBeValues(['A', '?', 'B', '?', 'C', '?']);
        }

        [Fact]
        public async Task can_read_compoundrule4_aff()
        {
            var filePath = @"files/compoundrule4.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("0123456789");
            actual.CompoundMin.ShouldBe(1);
            actual.OnlyInCompound.ShouldBeValue('c');
            actual.CompoundRules.ShouldHaveCount(2);
            actual.CompoundRules[0].ShouldBeValues(['n', '*', '1', 't']);
            actual.CompoundRules[1].ShouldBeValues(['n', '*', 'm', 'p']);
        }

        [Fact]
        public async Task can_read_compoundrule5_aff()
        {
            var filePath = @"files/compoundrule5.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(1);
            actual.CompoundRules.ShouldHaveCount(2);
            actual.CompoundRules[0].ShouldBeValues("N*%?");
            actual.CompoundRules[1].ShouldBeValues("NN*.NN*%?");
            actual.WordChars.ShouldBe("0123456789‰.", ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_compoundrule6_aff()
        {
            var filePath = @"files/compoundrule6.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(1);
            actual.CompoundRules.ShouldHaveCount(2);
            actual.CompoundRules[0].ShouldBeValues("A*A");
            actual.CompoundRules[1].ShouldBeValues("A*AAB*BBBC*C");
        }

        [Fact]
        public async Task can_read_compoundrule7_aff()
        {
            var filePath = @"files/compoundrule7.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("0123456789");
            actual.CompoundMin.ShouldBe(1);
            actual.OnlyInCompound.ShouldBeValue('c' << 8 | 'c');
            actual.CompoundRules.ShouldHaveCount(2);
            actual.CompoundRules[0].ShouldBeValues(['n' << 8 | 'n', '*', '1' << 8 | '1', 't' << 8 | 't']);
            actual.CompoundRules[1].ShouldBeValues(['n' << 8 | 'n', '*', 'm' << 8 | 'm', 'p' << 8 | 'p']);
        }

        [Fact]
        public async Task can_read_compoundrule8_aff()
        {
            var filePath = @"files/compoundrule8.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("0123456789");
            actual.CompoundMin.ShouldBe(1);
            actual.OnlyInCompound.ShouldBeValue(1000);
            actual.CompoundRules.ShouldHaveCount(2);
            actual.CompoundRules[0].ShouldBeValues([1001, '*', 1002, 2001]);
            actual.CompoundRules[1].ShouldBeValues([1001, '*', 2002, 2000]);
        }

        [Fact]
        public async Task can_read_condition_aff()
        {
            var filePath = @"files/condition.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("0123456789");
            actual.Suffixes.ShouldHaveCount(4);
            actual.Prefixes.ShouldHaveCount(3);
        }

        [Fact]
        public async Task can_read_condition_utf_aff()
        {
            var filePath = @"files/condition_utf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("0123456789");
            actual.Suffixes.ShouldHaveSingleItem();
            actual.Prefixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_digits_in_words_aff()
        {
            var filePath = @"files/digits_in_words.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(1);
            actual.CompoundRules.ShouldHaveSingleItem();
            actual.CompoundRules.Single().ShouldBeValues(['a', '*', 'b']);
            actual.OnlyInCompound.ShouldBeValue('c');
            actual.WordChars.ShouldBe("0123456789-", ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_encoding_aff()
        {
            var filePath = @"files/encoding.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-15");
        }

        [Fact]
        public async Task can_read_flag_aff()
        {
            var filePath = @"files/flag.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.FlagMode.ShouldBe(FlagParsingMode.Char);
            actual.Suffixes.ShouldHaveCount(3);
            actual.Suffixes.ElementAt(1).AFlag.ShouldBeValue('1');
            actual.Prefixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_flaglong_aff()
        {
            var filePath = @"files/flaglong.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.FlagMode.ShouldBe(FlagParsingMode.Long);
            actual.Suffixes.ShouldHaveCount(3);
            actual.Suffixes.ElementAt(1).AFlag.ShouldBeValue('g' << 8 | '?');
            actual.Prefixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_flagnum_aff()
        {
            var filePath = @"files/flagnum.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Suffixes.ShouldHaveCount(3);
            actual.Suffixes.ElementAt(0).AFlag.ShouldBeValue(999);
            actual.Suffixes.ElementAt(0).Options.ShouldBe(AffixEntryOptions.CrossProduct);
            actual.Suffixes.ElementAt(1).AFlag.ShouldBeValue(214);
            actual.Suffixes.ElementAt(1).Options.ShouldBe(AffixEntryOptions.CrossProduct);
            actual.Suffixes.ElementAt(2).AFlag.ShouldBeValue(216);
            actual.Suffixes.ElementAt(2).Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var pg = actual.Prefixes.ShouldHaveSingleItem();
            pg.AFlag.ShouldBeValue(54321);
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
        }

        [Fact]
        public async Task can_read_flagutf8_aff()
        {
            var filePath = @"files/flagutf8.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.FlagMode.ShouldBe(FlagParsingMode.Uni);
            actual.Suffixes.ShouldHaveCount(3);
            actual.Suffixes.ElementAt(0).ShouldHaveSingleItem();
            actual.Suffixes.ElementAt(1).AFlag.ShouldBeValue('Ö');
            actual.Suffixes.ElementAt(1).ShouldHaveSingleItem();
            actual.Suffixes.ElementAt(2).ShouldHaveSingleItem();
            var pg = actual.Prefixes.ShouldHaveSingleItem();
            pg.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_fogemorpheme_aff()
        {
            var filePath = @"files/fogemorpheme.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundFlag.ShouldBeValue('X');
            actual.CompoundBegin.ShouldBeValue('Y');
            actual.OnlyInCompound.ShouldBeValue('Z');
            actual.CompoundPermitFlag.ShouldBeValue('P');
            actual.Suffixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_forbiddenword_aff()
        {
            var filePath = @"files/forbiddenword.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.ForbiddenWord.ShouldBeValue('X');
            actual.CompoundFlag.ShouldBeValue('Y');
            actual.Suffixes.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_forceucase_aff()
        {
            var filePath = @"files/forceucase.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.TryString.ShouldBe("F");
            actual.ForceUpperCase.ShouldBeValue('A');
            actual.CompoundFlag.ShouldBeValue('C');
        }

        [Fact]
        public async Task can_read_fullstrip_aff()
        {
            var filePath = @"files/fullstrip.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.FullStrip.ShouldBeTrue();
            actual.TryString.ShouldBe("aioertnsclmdpgubzfvhÃ q'ACMSkBGPLxEyRTVÃ²IODNwFÃ©Ã¹ÃšÃ¬jUZKHWJYQX");

            var sg = actual.Suffixes.ShouldHaveSingleItem();
            sg.AFlag.ShouldBeValue('A');
            sg.Options.ShouldBe(AffixEntryOptions.CrossProduct);

            var entry1 = sg[0];
            entry1.Strip.ShouldBe("andare");
            entry1.Append.ShouldBe("vado");
            entry1.Conditions.GetEncoded().ShouldBe(".");

            var entry2 = sg[1];
            entry2.Strip.ShouldBe("andare");
            entry2.Append.ShouldBe("va");
            entry2.Conditions.GetEncoded().ShouldBe(".")
                ;
            var entry3 = sg[2];
            entry3.Strip.ShouldBe("are");
            entry3.Append.ShouldBe("iamo");
            entry3.Conditions.GetEncoded().ShouldBe("andare");
        }

        [Fact]
        public async Task can_read_germancompounding_aff()
        {
            var filePath = @"files/germancompounding.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckSharps.ShouldBeTrue();
            actual.CompoundBegin.ShouldBeValue('U');
            actual.CompoundMiddle.ShouldBeValue('V');
            actual.CompoundEnd.ShouldBeValue('W');
            actual.CompoundPermitFlag.ShouldBeValue('P');
            actual.OnlyInCompound.ShouldBeValue('X');
            actual.CompoundMin.ShouldBe(1);
            actual.WordChars.ShouldBe(['-']);

            actual.Suffixes.ShouldHaveCount(3);

            var sg = actual.Suffixes.ElementAt(0);
            sg.AFlag.ShouldBeValue('A');
            sg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            sg.ShouldHaveCount(3);
            sg[0].Strip.ShouldBeEmpty();
            sg[0].Append.ShouldBe("s");
            sg[0].Key.ShouldBe("s");
            sg[0].ContClass.ShouldBeValues(['U', 'P', 'X'], ignoreOrder: true);
            sg[0].Conditions.GetEncoded().ShouldBe(".");
            sg[1].Append.ShouldBe("s");
            sg[1].Key.ShouldBe("s");
            sg[2].Append.ShouldBeEmpty();
            sg[2].Key.ShouldBeEmpty();

            sg = actual.Suffixes.ElementAt(1);
            sg.AFlag.ShouldBeValue('B');
            sg.ShouldHaveCount(2);

            sg = actual.Suffixes.ElementAt(2);
            sg.AFlag.ShouldBeValue('C');
            var se = sg.ShouldHaveSingleItem();
            se.Strip.ShouldBeEmpty();
            se.Append.ShouldBe("n");
            se.ContClass.ShouldBeValues(['D', 'W']);
            se.Conditions.GetEncoded().ShouldBe(".");

            actual.ForbiddenWord.ShouldBeValue('Z');

            actual.Prefixes.ShouldHaveCount(2);

            var pg = actual.Prefixes.ElementAt(0);
            pg.AFlag.ShouldBeValue('-');
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var pe = pg.ShouldHaveSingleItem();
            pe.Strip.ShouldBeEmpty();
            pe.Append.ShouldBe("-");
            pe.ContClass.ShouldBeValues(['P']);
            pe.Conditions.GetEncoded().ShouldBe(".");

            actual.Prefixes.ElementAt(1).ShouldHaveCount(29);
        }

        [Fact]
        public async Task can_read_iconv_aff()
        {
            var filePath = @"files/iconv.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.InputConversions.ShouldHaveCount(4);
            actual.InputConversions.ContainsKey("ş");
            actual.InputConversions["ş"][0].ShouldBe("ș");
            actual.InputConversions.ContainsKey("ţ");
            actual.InputConversions["ţ"][0].ShouldBe("ț");
            actual.InputConversions.ContainsKey("Ş");
            actual.InputConversions["Ş"][0].ShouldBe("Ș");
            actual.InputConversions.ContainsKey("Ţ");
            actual.InputConversions["Ţ"][0].ShouldBe("Ț");
        }

        [Fact]
        public async Task can_read_ignore_aff()
        {
            var filePath = @"files/ignore.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.IgnoredChars.ShouldBe("aeiou");
            var pg = actual.Prefixes.ShouldHaveSingleItem();
            pg.AFlag.ShouldBeValue('A');
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var e = pg.ShouldHaveSingleItem();
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBe("r");
            e.Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_ignoreutf_aff()
        {
            var filePath = @"files/ignoreutf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.IgnoredChars.ShouldBe("ًٌٍَُِّْ", ignoreOrder: true);
            actual.WordChars.ShouldBe("ًٌٍَُِّْ", ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_maputf_aff()
        {
            var filePath = @"files/maputf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);
            actual.RelatedCharacterMap.ShouldHaveCount(3);
            actual.RelatedCharacterMap[0].ShouldBe(["u", "ú", "ü"]);
            actual.RelatedCharacterMap[1].ShouldBe(["ö", "ó", "o"]);
            actual.RelatedCharacterMap[2].ShouldBe(["ß", "ss"]);
        }

        [Fact]
        public async Task can_read_morph_aff()
        {
            var filePath = @"files/morph.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            var pg = actual.Prefixes.ShouldHaveSingleItem();
            pg.AFlag.ShouldBeValue('P');
            pg[0].MorphCode.ShouldBe(["dp:pfx_un", "sp:un"]);

            actual.Suffixes.ShouldHaveCount(3);

            var sg = actual.Suffixes.ElementAt(0);
            sg.AFlag.ShouldBeValue('S');
            sg[0].MorphCode.ShouldBe(["is:plur"]);

            sg = actual.Suffixes.ElementAt(1);
            sg.AFlag.ShouldBeValue('Q');
            sg[0].MorphCode.ShouldBe(["is:sg_3"]);

            sg = actual.Suffixes.ElementAt(2);
            sg.AFlag.ShouldBeValue('R');
            sg[0].MorphCode.ShouldBe(["ds:der_able"]);
        }

        [Fact]
        public async Task can_read_needaffix_aff()
        {
            var filePath = @"files/needaffix.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.NeedAffix.ShouldBeValue('X');
            actual.CompoundFlag.ShouldBeValue('Y');
            var g = actual.Suffixes.ShouldHaveSingleItem();
            g.ShouldHaveSingleItem();
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
            key1.ShouldNotBeEmpty();
            key2.ShouldNotBeEmpty();
            key2.ShouldNotBe(key1);
            value1_1.ShouldNotBeEmpty();
            value1_2.ShouldNotBeEmpty();
            value1_2.ShouldNotBe(value1_1);

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.IgnoredChars.ShouldBe("￰");
            actual.WordChars.ShouldBe("ःािीॉॊोौॎॏॕॖॗ‌‍");

            actual.InputConversions.ShouldHaveCount(4);

            actual.InputConversions[key1][0].Equals(value1_1, StringComparison.Ordinal).ShouldBeTrue();
            actual.InputConversions[key1][(ReplacementValueType)1].ShouldBeNull();
            actual.InputConversions[key1][(ReplacementValueType)2].Equals(value1_2, StringComparison.Ordinal).ShouldBeTrue();
            actual.InputConversions[key1][(ReplacementValueType)3].ShouldBeNull();

            actual.InputConversions[key2][0].ShouldBeNull();
            actual.InputConversions[key2][(ReplacementValueType)1].ShouldBeNull();
            actual.InputConversions[key2][(ReplacementValueType)2].Equals(value2_2, StringComparison.Ordinal).ShouldBeTrue();
            actual.InputConversions[key2][(ReplacementValueType)3].ShouldBeNull();

            actual.InputConversions["र्‌य"][0].ShouldBe("र्‌य");

            actual.InputConversions["र्‌व"][0].ShouldBe("र्‌व");
        }

        [Fact]
        public async Task can_read_ngram_utf_fix_aff()
        {
            var filePath = @"files/ngram_utf_fix.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            var pg = actual.Prefixes.ShouldHaveSingleItem();
            pg.AFlag.ShouldBeValue(101);
            pg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var e = pg.ShouldHaveSingleItem();
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBe("пред");
            e.Conditions.GetEncoded().ShouldBe(".");

            actual.Suffixes.ShouldHaveCount(3);
            var sg = actual.Suffixes.ElementAt(1);
            sg.AFlag.ShouldBeValue(2000);
            sg.ShouldHaveCount(3);
            sg[1].Strip.ShouldBeEmpty();
            sg[1].Append.ShouldBe("ами");
            sg[1].Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_oconv_aff()
        {
            var filePath = @"files/oconv.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.OutputConversions.ShouldHaveCount(7);
            actual.OutputConversions["a"][0].ShouldBe("A");
            actual.OutputConversions["á"][0].ShouldBe("Á");
            actual.OutputConversions["b"][0].ShouldBe("B");
            actual.OutputConversions["c"][0].ShouldBe("C");
            actual.OutputConversions["d"][0].ShouldBe("D");
            actual.OutputConversions["e"][0].ShouldBe("E");
            actual.OutputConversions["é"][0].ShouldBe("É");
        }

        [Fact]
        public async Task can_read_onlyincompound2_aff()
        {
            var filePath = @"files/onlyincompound2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.OnlyInCompound.ShouldBeValue('O');
            actual.CompoundFlag.ShouldBeValue('A');
            actual.CompoundPermitFlag.ShouldBeValue('P');
            var sg = actual.Suffixes.ShouldHaveSingleItem();
            sg.AFlag.ShouldBeValue('B');
            sg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            var e = sg.ShouldHaveSingleItem();
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBe("s");
            e.ContClass.ShouldBeValues(['O', 'P']);
            e.Conditions.GetEncoded().ShouldBe(".");
            var pe = actual.CompoundPatterns.ShouldHaveSingleItem();
            pe.Pattern.ShouldBe("0");
            pe.Condition.ShouldBeValue('B');
            pe.Pattern2.ShouldBeEmpty();
            pe.Condition2.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_opentaal_cpdpat_aff()
        {
            var filePath = @"files/opentaal_cpdpat.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundBegin.ShouldBeValue('C' << 8 | 'a');
            actual.CompoundMiddle.ShouldBeValue('C' << 8 | 'b');
            actual.CompoundEnd.ShouldBeValue('C' << 8 | 'c');
            actual.CompoundPermitFlag.ShouldBeValue('C' << 8 | 'p');
            actual.OnlyInCompound.ShouldBeValue('C' << 8 | 'x');
            var pe = actual.CompoundPatterns.ShouldHaveSingleItem();
            pe.Pattern.ShouldBeEmpty();
            pe.Condition.ShouldBeValue('C' << 8 | 'h');
            pe.Pattern2.ShouldBeEmpty();
            pe.Condition2.ShouldBeValue('X' << 8 | 's');
            var sg = actual.Suffixes.ShouldHaveSingleItem();
            sg.AFlag.ShouldBeValue('C' << 8 | 'h');
            sg.Options.ShouldBe(AffixEntryOptions.CrossProduct);
            sg.ShouldHaveCount(2);
            sg[0].Strip.ShouldBeEmpty();
            sg[0].Append.ShouldBe("s");
            sg[0].ContClass.ShouldBeValues(['C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'x', 'C' << 8 | 'p'], ignoreOrder: true);
            sg[0].Conditions.GetEncoded().ShouldBe(".");
            sg[1].Strip.ShouldBeEmpty();
            sg[1].Append.ShouldBe("s-");
            sg[1].ContClass.ShouldBeValues(['C' << 8 | 'a', 'C' << 8 | 'b', 'C' << 8 | 'c', 'C' << 8 | 'p'], ignoreOrder: true);
            sg[1].Conditions.GetEncoded().ShouldBe(".");
        }

        [Fact]
        public async Task can_read_opentaal_cpdpat2_aff()
        {
            var filePath = @"files/opentaal_cpdpat2.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe("-");
            actual.NoSplitSuggestions.ShouldBeTrue();
        }

        [Fact]
        public async Task can_read_phone_aff()
        {
            var filePath = @"files/phone.aff";
            var expectedPhoneRulesSection =
            """
            AH(AEIOUY)-^
            AR(AEIOUY)-^
            A(HR)^      
            A^          
            AH(AEIOUY)- 
            AR(AEIOUY)- 
            A(HR)       
            BB-         
            B           
            CQ-         
            CIA         
            CH          
            C(EIY)-     
            CK          
            COUGH^      
            CC<         
            C           
            DG(EIY)     
            DD-         
            D           
            É<          
            EH(AEIOUY)-^
            ER(AEIOUY)-^
            E(HR)^      
            ENOUGH^$    
            E^          
            EH(AEIOUY)- 
            ER(AEIOUY)- 
            E(HR)       
            FF-         
            F           
            GN^         
            GN$         
            GNS$        
            GNED$       
            GH(AEIOUY)- 
            GH          
            GG9         
            G           
            H           
            IH(AEIOUY)-^
            IR(AEIOUY)-^
            I(HR)^      
            I^          
            ING6        
            IH(AEIOUY)- 
            IR(AEIOUY)- 
            I(HR)       
            J           
            KN^         
            KK-         
            K           
            LAUGH^      
            LL-         
            L           
            MB$         
            MM          
            M           
            NN-         
            N           
            OH(AEIOUY)-^
            OR(AEIOUY)-^
            O(HR)^      
            O^          
            OH(AEIOUY)- 
            OR(AEIOUY)- 
            O(HR)       
            PH          
            PN^         
            PP-         
            P           
            Q           
            RH^         
            ROUGH^      
            RR-         
            R           
            SCH(EOU)-   
            SC(IEY)-    
            SH          
            SI(AO)-     
            SS-         
            S           
            TI(AO)-     
            TH          
            TCH--       
            TOUGH^      
            TT-         
            T           
            UH(AEIOUY)-^
            UR(AEIOUY)-^
            U(HR)^      
            U^          
            UH(AEIOUY)- 
            UR(AEIOUY)- 
            U(HR)       
            V^          
            V           
            WR^         
            WH^         
            W(AEIOU)-   
            X^          
            X           
            Y(AEIOU)-   
            ZZ-         
            Z           
            """;
            var expectedPhoneRules = expectedPhoneRulesSection
                .Split(separator: (char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .ToArray();
            
            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("iso-8859-1");
            actual.Warnings.ShouldBeEmpty();
            actual.Phone.Select(p => p.Rule).ShouldBe(expectedPhoneRules);
            actual.Phone[0].Rule.ShouldBe("AH(AEIOUY)-^");
            actual.Phone[0].Replace.ShouldBe("*H");
            actual.Phone.Single(p => p.Rule == "Z").Replace.ShouldBe("S");
        }

        [Fact]
        public async Task can_read_rep_aff()
        {
            var filePath = @"files/rep.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);

            var replacements = actual.Replacements;
            replacements.ShouldHaveCount(8);

            replacements[0].Pattern.ShouldBe("f");
            replacements[0].OutString.ShouldBe("ph");
            replacements[0].Med.ShouldBe("ph");
            replacements[0].Type.ShouldBe(ReplacementValueType.Med);

            replacements[1].Pattern.ShouldBe("ph");
            replacements[1].OutString.ShouldBe("f");
            replacements[1].Med.ShouldBe("f");
            replacements[1].Type.ShouldBe(ReplacementValueType.Med);

            replacements[2].Pattern.ShouldBe("shun");
            replacements[2].OutString.ShouldBe("tion");
            replacements[2].Fin.ShouldBe("tion");
            replacements[2].Type.ShouldBe(ReplacementValueType.Fin);

            replacements[3].Pattern.ShouldBe("alot");
            replacements[3].OutString.ShouldBe("a lot");
            replacements[3].Isol.ShouldBe("a lot");
            replacements[3].Type.ShouldBe(ReplacementValueType.Isol);

            replacements[4].Pattern.ShouldBe("foo");
            replacements[4].OutString.ShouldBe("bar");
            replacements[4].Isol.ShouldBe("bar");
            replacements[4].Type.ShouldBe(ReplacementValueType.Isol);

            replacements[5].Pattern.ShouldBe("'");
            replacements[5].OutString.ShouldBe(" ");
            replacements[5].Med.ShouldBe(" ");
            replacements[5].Type.ShouldBe(ReplacementValueType.Med);

            replacements[6].Pattern.ShouldStartWith("vinte");
            replacements[6].Pattern.ShouldEndWith("n");
            replacements[6].OutString.ShouldBe("vinte e un");
            replacements[6].Isol.ShouldBe("vinte e un");
            replacements[6].Type.ShouldBe(ReplacementValueType.Isol);

            replacements[7].Pattern.ShouldBe("s");
            replacements[7].OutString.ShouldBe("'s");
            replacements[7].Med.ShouldBe("'s");
            replacements[7].Type.ShouldBe(ReplacementValueType.Med);

            var sg = actual.Suffixes.ShouldHaveSingleItem();
            sg.AFlag.ShouldBeValue('A');

            actual.WordChars.ShouldBe("\'");
        }

        [Fact]
        public async Task can_read_reputf_aff()
        {
            var filePath = @"files/reputf.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);
            var r = actual.Replacements.ShouldHaveSingleItem();
            r.Pattern.ShouldBe("oo");
            r.OutString.ShouldBe("őő");
            r.Type.ShouldBe(ReplacementValueType.Med);
        }

        [Fact]
        public async Task can_read_simplifiedtriple_aff()
        {
            var filePath = @"files/simplifiedtriple.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CheckCompoundTriple.ShouldBeTrue();
            actual.SimplifiedTriple.ShouldBeTrue();
            actual.CompoundMin.ShouldBe(2);
            actual.CompoundFlag.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_slash_aff()
        {
            var filePath = @"files/slash.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.WordChars.ShouldBe(@"/:");
        }

        [Fact]
        public async Task can_read_sug_aff()
        {
            var filePath = @"files/sug.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.MaxNgramSuggestions.ShouldBe(0);
            actual.Replacements.ShouldHaveCount(2);
            var entry = actual.Replacements[0];
            entry.Pattern.ShouldBe("alot");
            entry.OutString.ShouldBe("a lot");
            entry.Type.ShouldBe(ReplacementValueType.Med);
            entry.Med.ShouldBe("a lot");
            actual.KeyString.ShouldBe("qwertzuiop|asdfghjkl|yxcvbnm|aq");
            actual.WordChars.ShouldBe(['.', '-'], ignoreOrder: true);
            actual.ForbiddenWord.ShouldBeValue('?');
            var entry2 = actual.Replacements[1];
            entry2.Pattern.ShouldBe("inspite");
            entry2.OutString.ShouldBe("in spite");
        }

        [Fact]
        public async Task can_read_utf8_bom_aff()
        {
            var filePath = @"files/utf8_bom.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Encoding.WebName.ShouldBe("utf-8");
        }

        [Fact]
        public async Task can_read_utfcompound_aff()
        {
            var filePath = @"files/utfcompound.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.CompoundMin.ShouldBe(3);
            actual.CompoundFlag.ShouldBeValue('A');
        }

        [Fact]
        public async Task can_read_warn_add()
        {
            var filePath = @"files/warn.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.Warn.ShouldBeValue('W');
            actual.Suffixes.ShouldHaveSingleItem();
            var r = actual.Replacements.ShouldHaveSingleItem();
            r.Pattern.ShouldBe("foo");
            r.OutString.ShouldBe("bar");
            r.Type.ShouldBe(ReplacementValueType.Med);
        }

        [Fact]
        public async Task can_read_zeroaffix_aff()
        {
            var filePath = @"files/zeroaffix.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.NeedAffix.ShouldBeValue('X');
            actual.CompoundFlag.ShouldBeValue('Y');

            actual.Suffixes.ShouldHaveCount(3);

            var sg = actual.Suffixes.ElementAt(0);
            sg.AFlag.ShouldBeValue('A');

            var e = sg.ShouldHaveSingleItem();
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBeEmpty();
            e.Conditions.GetEncoded().ShouldBe(".");
            e.MorphCode.ShouldAllBe(x => x == ">");

            sg = actual.Suffixes.ElementAt(1);
            sg.AFlag.ShouldBeValue('B');

            e = sg.ShouldHaveSingleItem();
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBeEmpty();
            e.Conditions.GetEncoded().ShouldBe(".");
            e.MorphCode.ShouldAllBe(x => x == "<ZERO>>");

            sg = actual.Suffixes.ElementAt(2);
            sg.AFlag.ShouldBeValue('C');
            sg.ShouldHaveCount(2);

            e = sg[0];
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBeEmpty();
            e.ContClass.ShouldBeValues(['X', 'A', 'B'], ignoreOrder: true);
            e.Conditions.GetEncoded().ShouldBe(".");
            e.MorphCode.ShouldAllBe(x => x == "<ZERODERIV>");

            e = sg[1];
            e.Strip.ShouldBeEmpty();
            e.Append.ShouldBe("baz");
            e.ContClass.ShouldBeValues(['X', 'A', 'B'], ignoreOrder: true);
            e.Conditions.GetEncoded().ShouldBe(".");
            e.MorphCode.ShouldAllBe(x => x == "<DERIV>");
        }

        [Fact]
        public async Task can_read_flag_mode_from_russian_english_bilingual()
        {
            var filePath = @"files/Russian-English Bilingual.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.FlagMode.ShouldBe(FlagParsingMode.Num);
        }

        [Theory, ClassData(typeof(TestTheories.AffFilePathsData))]
        public async Task can_read_file_without_exception(string filePath)
        {
            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.ShouldNotBeNull();

            var fileName = Path.GetFileName(filePath);

            if (fileName == "base_utf.aff")
            {
                actual.Warnings.ShouldNotBeEmpty(customMessage: "this file has some strange morph lines at the bottom, maybe a bug?");
            }
            else if (fileName is "1748408-2.aff" or "1748408-4.aff")
            {
                actual.Warnings.ShouldNotBeEmpty();
            }
            else if (fileName == "English (Australian).aff")
            {
                actual.Warnings.ShouldBe(
                [
                    "Failed to parse line: MIDWORD '-",
                    "Failed to parse line: BAD ~"
                ], "I'm not sure what to make of this");
            }
            else
            {
                actual.Warnings.ShouldBeEmpty();
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
        [InlineData("-")]
        [InlineData("en-XX")]
        [InlineData("en-")]
        public void can_read_all_languages(string langCode)
        {
            var textFileContents = $"LANG {langCode}";
            var expectedCulture = langCode;

            if (expectedCulture.EndsWith("-"))
            {
                expectedCulture = expectedCulture.Substring(0, expectedCulture.Length - 1);
            }

            expectedCulture = expectedCulture.Replace('_', '-');

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.Language.ShouldBe(langCode);
            actual.Culture.ShouldNotBeNull();

            if (expectedCulture.Equals(actual.Culture.Name, StringComparison.OrdinalIgnoreCase))
            {
                actual.Culture.Name.ShouldBe(expectedCulture);
            }
            else
            {
                // On linux net48 runs unknown cultures may behave differently
                expectedCulture.ShouldStartWith(actual.Culture.Name, customMessage: "en-XX may change to en, or worse");
                actual.Language.ShouldBe(expectedCulture);
            }
        }

        [Fact]
        public void reading_empty_lang_code_leaves_language_unset_and_culture_invariant()
        {
            var textFileContents = "LANG ";

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.Language.ShouldBeNullOrEmpty();
            actual.Culture.ShouldBe(System.Globalization.CultureInfo.InvariantCulture);
        }

        [Theory]
        [InlineData("klmc")]
        [InlineData("ःािीॉॊोौॎॏॕॖॗ‌‍")]
        public void can_read_syllablenum(string parameters)
        {
            var textFileContents = "SYLLABLENUM " + parameters;

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.CompoundSyllableNum.ShouldBe(parameters);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("5", 5)]
        [InlineData("words", null)]
        public void can_read_compoundwordmax(string parameters, int? expected)
        {
            var textFileContents = "COMPOUNDWORDMAX " + parameters;

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.CompoundWordMax.ShouldBe(expected);
        }

        [Theory]
        [InlineData("-1", 1)]
        [InlineData("0", 1)]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("words", 3)]
        public void can_read_compoundmin(string parameters, int expected)
        {
            var textFileContents = "COMPOUNDMIN " + parameters;

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.CompoundMin.ShouldBe(expected);
        }

        [Theory]
        [InlineData("A", 'A')]
        [InlineData("Z", 'Z')]
        [InlineData("AB", 'A' << 8 | 'B')]
        [InlineData("y", 'y')]
        public void can_read_compoundroot(string parameters, int expected)
        {
            var textFileContents = string.Empty;

            if (parameters.Length > 1)
            {
                textFileContents += "FLAG LONG\n";
            }

            textFileContents += "COMPOUNDROOT " + parameters;

            var actual = AffixReader.ReadFromString(textFileContents);

            actual.CompoundRoot.ShouldBeValue(expected);
        }

        [Theory]
        [InlineData("6 aáeéiíoóöőuúüű", 6, "aáeéiíoóöőuúüű")]
        [InlineData("1 abc", 1, "abc")]
        [InlineData("1", 1, "AEIOUaeiou")]
        [InlineData("abc", 0, "")]
        public void can_read_compoundsyllable(string parameters, int expectedNumber, string expectedLettersText)
        {
            var actual = AffixReader.ReadFromString($"COMPOUNDSYLLABLE {parameters}");

            actual.CompoundMaxSyllable.ShouldBe(expectedNumber);
            actual.CompoundVowels.ShouldBe(expectedLettersText, ignoreOrder: true);
        }

        [Theory]
        [InlineData("A", 'A')]
        [InlineData("=", '=')]
        public void can_read_nosuggest(string parameters, char expectedFlag)
        {
            var actual = AffixReader.ReadFromString($"NOSUGGEST {parameters}");

            actual.NoSuggest.ShouldBeValue(expectedFlag);
        }

        [Theory]
        [InlineData("A", 'A')]
        [InlineData("=", '=')]
        public void can_read_nongramsuggest(string parameters, char expectedFlag)
        {
            var actual = AffixReader.ReadFromString($"NONGRAMSUGGEST {parameters}");

            actual.NoNgramSuggest.ShouldBeValue(expectedFlag);
        }

        [Theory]
        [InlineData("A", 'A')]
        [InlineData(")", ')')]
        public void can_read_lemma_present(string parameters, char expectedFlag)
        {
            var actual = AffixReader.ReadFromString($"LEMMA_PRESENT {parameters}");

            actual.LemmaPresent.ShouldBeValue(expectedFlag);
        }

        [Theory]
        [InlineData("Magyar 1.6", "Magyar 1.6")]
        [InlineData("", null)]
        public void can_read_version(string parameters, string expected)
        {
            var actual = AffixReader.ReadFromString($"VERSION {parameters}");

            actual.Version.ShouldBe(expected);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("", null)]
        public void can_read_maxdiff(string parameters, int? expected)
        {
            var actual = AffixReader.ReadFromString($"MAXDIFF {parameters}");

            actual.MaxDifferency.ShouldBe(expected);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("", 3)]
        public void can_read_maxcpdsugs(string parameters, int expected)
        {
            var actual = AffixReader.ReadFromString($"MAXCPDSUGS {parameters}");

            actual.MaxCompoundSuggestions.ShouldBe(expected);
        }

        [Theory]
        [InlineData("A", 'A')]
        [InlineData("&", '&')]
        public void can_read_substandard(string parameters, char expectedFlag)
        {
            var actual = AffixReader.ReadFromString($"SUBSTANDARD {parameters}");

            actual.SubStandard.ShouldBeValue(expectedFlag);
        }

        [Fact]
        public void can_read_compoundmoresuffixes()
        {
            var actual = AffixReader.ReadFromString("COMPOUNDMORESUFFIXES");

            actual.CompoundMoreSuffixes.ShouldBeTrue();
        }

        [Fact]
        public void can_read_checknum()
        {
            var actual = AffixReader.ReadFromString("CHECKNUM");

            actual.CheckNum.ShouldBeTrue();
        }

        [Fact]
        public void can_read_onlymaxdiff()
        {
            var actual = AffixReader.ReadFromString("ONLYMAXDIFF");

            actual.OnlyMaxDiff.ShouldBeTrue();
        }

        [Fact]
        public void can_read_sugswithdots()
        {
            var actual = AffixReader.ReadFromString("SUGSWITHDOTS");

            actual.SuggestWithDots.ShouldBeTrue();
        }

        [Fact]
        public void can_read_forbidwarn()
        {
            var actual = AffixReader.ReadFromString("FORBIDWARN");

            actual.ForbidWarn.ShouldBeTrue();
        }

        [Fact]
        public void can_read_unknown_command()
        {
            var actual = AffixReader.ReadFromString("UNKNOWN arguments");

            actual.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("LONG", FlagParsingMode.Long)]
        [InlineData("CHAR", FlagParsingMode.Char)]
        [InlineData("NUM", FlagParsingMode.Num)]
        [InlineData("NUMBER", FlagParsingMode.Num)]
        [InlineData("UTF", FlagParsingMode.Uni)]
        [InlineData("UNI", FlagParsingMode.Uni)]
        [InlineData("UNICODE", FlagParsingMode.Uni)]
        [InlineData("UTF-8", FlagParsingMode.Uni)]
        public void can_read_flag_mode(string given, FlagParsingMode expected)
        {
            var actual = AffixReader.ReadFromString($"FLAG {given}");

            actual.FlagMode.ShouldBe(expected);
        }

        [Theory]
        [InlineData("abc", "def", "abc", "def", ReplacementValueType.Med)]
        [InlineData("^abc", "d_e_f", "abc", "d e f", ReplacementValueType.Ini)]
        [InlineData("a_b_c$", "d_e_f", "a b c", "d e f", ReplacementValueType.Fin)]
        [InlineData("^a_b_c$", "d_e_f", "a b c", "d e f", ReplacementValueType.Isol)]
        public void can_read_all_rep_types(string pattern, string outText, string expectedPattern, string expectedOutString, ReplacementValueType expectedType)
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

            var actual = AffixReader.ReadFromString(textFileContents);

            var rep = actual.Replacements.ShouldHaveSingleItem();
            rep.Pattern.ShouldBe(expectedPattern);
            rep.OutString.ShouldBe(expectedOutString);
            rep.Type.ShouldBe(expectedType);
            rep.Med.ShouldBe(expectedMed);
            rep.Ini.ShouldBe(expectedIni);
            rep.Fin.ShouldBe(expectedFin);
            rep.Isol.ShouldBe(expectedIsol);
        }

        [Fact]
        public async Task can_ignore_alias_comments()
        {
            var filePath = @"files/af_am_comments.aff";

            var actual = await AffixReader.ReadFileAsync(filePath, TestCancellation);

            actual.AliasF.ShouldHaveSingleItem().ShouldBeValues([2, 3]);
            actual.AliasM.ShouldHaveSingleItem().ShouldBe(["ts:0"]);
        }
    }
}
