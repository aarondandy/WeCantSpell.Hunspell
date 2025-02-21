using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class WordListReaderTests
{
    static CancellationToken TestCancellation => TestContext.Current.CancellationToken;

    public class ReadFileAsync : WordListReaderTests
    {
        [Fact]
        public async Task can_read_1463589_utf_dic()
        {
            var filePath = @"files/1463589_utf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.Affix.ShouldNotBeNull();

            var rw = actual.RootWords.ShouldHaveSingleItem();
            rw.ShouldBe("Kühlschrank");

            var detail = actual[rw].ShouldHaveSingleItem();
            detail.Flags.ShouldBeEmpty();
            detail.Morphs.ShouldBeEmpty();
            detail.Options.ShouldBe(WordEntryOptions.InitCap);
        }

        [Fact]
        public async Task can_read_1592880_dic()
        {
            var filePath = @"files/1592880.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["weg", "wege"]);
            actual["weg"][0].Flags.ShouldBeValues(['Q', 'o', 'z']);
            actual["weg"][1].Flags.ShouldBeValues(['P']);
            actual["wege"][0].Flags.ShouldBeEmpty();
        }

        [Fact]
        public async Task can_read_1695964_dic()
        {
            var filePath = @"files/1695964.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["Mull"].ShouldHaveCount(2);
            actual["Mull"][0].Flags.ShouldBeValues(['e', 'h']);
            actual["Mull"][1].Flags.ShouldBeValues(['S']);
        }

        [Fact]
        public async Task can_read_1706659_dic()
        {
            var filePath = @"files/1706659.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(3);
            actual["arbeits"][0].Flags.ShouldBeValues(['v']);
            actual["scheu"][0].Flags.ShouldBeValues(['A', 'w']);
            actual["farbig"][0].Flags.ShouldBeValues(['A']);
        }

        [Fact]
        public async Task can_read_1975530_dic()
        {
            var filePath = @"files/1975530.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(2);
            actual["أرى"][0].Flags.ShouldBeValues(['x']);
            actual["أيار"][0].Flags.ShouldBeValues(['x']);
        }

        [Fact]
        public async Task can_read_alias_dic()
        {
            var filePath = @"files/alias.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues(['A', 'B']);
        }

        [Fact]
        public async Task can_read_alias2_dic()
        {
            var filePath = @"files/alias2.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues(['A', 'B']);
            actual["foo"][0].Morphs.ShouldBe(["po:noun", "xx:other_data"]);
        }

        [Fact]
        public async Task can_read_alias3_dic()
        {
            var filePath = @"files/alias3.dic";
            var reversedStem = new string("[stem_1]".ToCharArray().Reverse().ToArray());

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["oruo"][0].Flags.ShouldBeValues(['B', 'C']);
            actual["oruo"][0].Morphs.ShouldAllBe(x => x == reversedStem);
        }

        [Fact]
        public async Task can_read_allcaps_dic()
        {
            var filePath = @"files/allcaps.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.Count().ShouldBeGreaterThanOrEqualTo(4);
            actual["OpenOffice.org"].ShouldHaveSingleItem().Flags.ShouldBeEmpty();
            actual["Openoffice.org"].ShouldHaveSingleItem().Flags.ShouldBe([SpecialFlags.OnlyUpcaseFlag]);
            actual["UNICEF"].ShouldHaveSingleItem().Flags.ShouldBeValues(['S']);
            actual["Unicef"].ShouldHaveSingleItem().Flags.ShouldBe([(FlagValue)'S', SpecialFlags.OnlyUpcaseFlag]);
            actual["Afrique"].ShouldHaveSingleItem().Flags.ShouldBeValues(['L']);
        }

        [Fact]
        public async Task can_read_allcaps2_dic()
        {
            var filePath = @"files/allcaps2.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(4);
            actual["iPod"][0].Flags.ShouldBeValues(['s']);
            actual["Ipod"][0].Flags.ShouldBe([(FlagValue)'s', SpecialFlags.OnlyUpcaseFlag]);
            actual["iPodos"][0].Flags.ShouldBeValues(['*']);
            actual["ipodos"][0].Flags.ShouldBeEmpty();
        }

        [Fact]
        public async Task can_read_allcaps3_dic()
        {
            var filePath = @"files/allcaps3.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(6);
            actual["UNESCO"][0].Flags.ShouldBeValues(['S']);
            actual["Unesco"][0].Flags.ShouldBeValues(['S']);
            actual["Nasa"][0].Flags.ShouldBeValues(['S']);
            actual["NASA"][0].Flags.ShouldBeValues(['S']);
            actual["ACTS"][0].Flags.ShouldBeEmpty();
            actual["act"][0].Flags.ShouldBeValues(['s']);
        }

        [Fact]
        public async Task can_read_allcaps_utf_dic()
        {
            var filePath = @"files/allcaps_utf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(4);
            actual["OpenOffice.org"].ShouldHaveSingleItem().Flags.ShouldBeEmpty();
            actual["Openoffice.org"].ShouldHaveSingleItem().Flags.ShouldBe([SpecialFlags.OnlyUpcaseFlag]);
            actual["UNICEF"].ShouldHaveSingleItem().Flags.ShouldBeValues(['S']);
            actual["Unicef"].ShouldHaveSingleItem().Flags.ShouldBe([(FlagValue)'S', SpecialFlags.OnlyUpcaseFlag]);
        }

        [Fact]
        public async Task can_read_arabic_dic()
        {
            var filePath = @"files/arabic.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["ب"].ShouldHaveSingleItem();
        }

        [Fact]
        public async Task can_read_base_dic()
        {
            var filePath = @"files/base.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "created",
                "create",
                "imply",
                "natural",
                "like",
                "convey",
                "look",
                "text",
                "hello",
                "said",
                "sawyer",
                "NASA",
                "rotten",
                "day",
                "tomorrow",
                "seven",
                "FAQ",
                "Faq",
                "can't",
                "doesn't",
                "etc",
                "won't",
                "lip",
                //"text", // duplicate
                "horrifying",
                "speech",
                "suggest",
                "uncreate",
                "Hunspell"
            ], ignoreOrder: true);

            actual["create"][0].Flags.ShouldBeValues(['X', 'K', 'V', 'N', 'G', 'A', 'D', 'S'], ignoreOrder: true);
            actual["Hunspell"][0].Flags.ShouldBeEmpty();
            actual["text"][0].Flags.ShouldBeEmpty();
            actual["FAQ"][0].Flags.ShouldBeValues(['M', 'S']);
            actual["Faq"][0].Flags.ShouldBe([(FlagValue)'M', (FlagValue)'S', SpecialFlags.OnlyUpcaseFlag]);
        }

        [Fact]
        public async Task can_read_base_utf_dic()
        {
            var expectedWords = new[]
            {
                "created",
                "create",
                "imply",
                "natural",
                "like",
                "convey",
                "look",
                "text",
                "hello",
                "said",
                "sawyer",
                "NASA",
                "rotten",
                "day",
                "tomorrow",
                "seven",
                "FAQ",
                "Faq",
                "can’t",
                "doesn’t",
                "etc",
                "won’t",
                "lip",
                //"text", // duplicate
                "horrifying",
                "speech",
                "suggest",
                "uncreate",
                "Hunspell",
                "İzmir"
            };
            var filePath = @"files/base_utf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(expectedWords.Length);
            actual.RootWords.ShouldBe(expectedWords, ignoreOrder: true);

            actual["create"][0].Flags.ShouldBeValues(['X', 'K', 'V', 'N', 'G', 'A', 'D', 'S'], ignoreOrder: true);
            actual["Hunspell"][0].Flags.ShouldBeEmpty();
            actual["text"][0].Flags.ShouldBeEmpty();
            actual["FAQ"][0].Flags.ShouldBeValues(['M', 'S']);
            actual["Faq"][0].Flags.ShouldBe([(FlagValue)'M', (FlagValue)'S', SpecialFlags.OnlyUpcaseFlag]);
        }

        [Fact]
        public async Task can_read_break_dic()
        {
            var filePath = @"files/break.dic";
            var expected = new[]
            {
                "foo",
                "bar",
                "baz",
                "fox-bax",
                "foo-baz",
                "e-mail"
            };

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(expected.Length);
            actual.RootWords.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_checkcompoundcase_dic()
        {
            var filePath = @"files/checkcompoundcase.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "foo",
                "Bar",
                "BAZ",
                "Baz",
                "-"
            ], ignoreOrder: true);

            actual["BAZ"][0].Flags.ShouldBeValues(['A']);
            actual["Baz"][0].Flags.ShouldBe([(FlagValue)'A', SpecialFlags.OnlyUpcaseFlag]);
            actual["-"][0].Flags.ShouldBeValues(['A']);
        }

        [Fact]
        public async Task can_read_checkcompoundcaseutf_dic()
        {
            var filePath = @"files/checkcompoundcaseutf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["áoó", "Óoá"]);
        }

        [Fact]
        public async Task can_read_checkcompoundpattern4_dic()
        {
            var filePath = @"files/checkcompoundpattern4.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "sUrya",
                "Surya",
                "udayaM",
                "Udayam",
                "pEru",
                "Peru",
                "unna"
            ], ignoreOrder: true);

            actual["pEru"][0].Flags.ShouldBeValues(['B', 'x']);
            actual["Peru"][0].Flags.ShouldBe([(FlagValue)'B', (FlagValue)'x', SpecialFlags.OnlyUpcaseFlag]);
            actual["unna"][0].Flags.ShouldBeValues(['B', 'x']);
        }

        [Fact]
        public async Task can_read_checkcompoundtriple_dic()
        {
            var filePath = @"files/checkcompoundtriple.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(4);
            actual.RootWords.ShouldBe(
            [
                "foo",
                "opera",
                "eel",
                "bare"
            ], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_checksharpsutf_dic()
        {
            var filePath = @"files/checksharpsutf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "müßig",
                "Ausstoß",
                "Abstoß.",
                "Außenabmessung",
                "Prozessionsstraße",
                "Außenmaße"
            ], ignoreOrder: true);

            actual["müßig"][0].Flags.ShouldBeValues(['k']);
        }

        [Fact]
        public async Task can_read_circumfix_dic()
        {
            var filePath = @"files/circumfix.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["nagy"][0].Flags.ShouldBeValues(['C']);
            actual["nagy"][0].Morphs.ShouldBe(["po:adj"]);
        }

        [Fact]
        public async Task can_read_colons_in_words_dic()
        {
            var filePath = @"files/colons_in_words.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["c:a", "S:t", "foo"], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_complexprefixes2_dic()
        {
            var filePath = @"files/complexprefixes2.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["oruo"][0].Flags.ShouldBeValues(['B', 'C']);
            actual["oruo"][0].Morphs.ShouldBe(["]1_mets["]);
        }

        [Fact]
        public async Task can_read_compoundaffix_dic()
        {
            var filePath = @"files/compoundaffix.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(2);
            actual["foo"][0].Flags.ShouldBeValues(['X', 'P', 'S'], ignoreOrder: true);
            actual["bar"][0].Flags.ShouldBeValues(['X', 'P', 'S'], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_compoundrule4_dic()
        {
            var filePath = @"files/compoundrule4.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "0th",
                "1st",
                "1th",
                "2nd",
                "2th",
                "3rd",
                "3th",
                "4th",
                "5th",
                "6th",
                "7th",
                "8th",
                "9th"
            ], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_compoundrule5_dic()
        {
            var filePath = @"files/compoundrule5.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                ".",
                "%",
                "‰"
            ], ignoreOrder: true);

            actual["0"][0].Flags.ShouldBeValues(['N']);
            actual["0"][0].Morphs.ShouldBe(["po:num"]);
            actual["."][0].Flags.ShouldBeValues(['.']);
            actual["."][0].Morphs.ShouldBe(["po:sign_dot"]);
            actual["%"][0].Flags.ShouldBeValues(['%']);
            actual["%"][0].Morphs.ShouldBe(["po:sign_percent"]);
            actual["‰"][0].Flags.ShouldBeValues(['%']);
            actual["‰"][0].Morphs.ShouldBe(["po:sign_per_mille"]);
        }

        [Fact]
        public async Task can_read_compoundrule7_dic()
        {
            var filePath = @"files/compoundrule7.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "0th",
                "1st",
                "1th",
                "2nd",
                "2th",
                "3rd",
                "3th",
                "4th",
                "5th",
                "6th",
                "7th",
                "8th",
                "9th"
            ], ignoreOrder: true);
            actual["0"][0].Flags.ShouldBeValues(['n' << 8 | 'n', 'm' << 8 | 'm'], ignoreOrder: true);
            actual["3rd"][0].Flags.ShouldBeValues(['p' << 8 | 'p'], ignoreOrder: true);
            actual["9th"][0].Flags.ShouldBeValues(['p' << 8 | 'p', 't' << 8 | 't'], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_compoundrule8_dic()
        {
            var filePath = @"files/compoundrule8.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "0th",
                "1st",
                "1th",
                "2nd",
                "2th",
                "3rd",
                "3th",
                "4th",
                "5th",
                "6th",
                "7th",
                "8th",
                "9th"
            ], ignoreOrder: true);
            actual["0"][0].Flags.ShouldBeValues([1001, 2002]);
            actual["1st"][0].Flags.ShouldBeValues([2000]);
            actual["9th"][0].Flags.ShouldBeValues([2000, 2001]);
        }

        [Fact]
        public async Task can_read_condition_utf_dic()
        {
            var filePath = @"files/condition_utf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["óőó"][0].Flags.ShouldBeValues(['P', 'S']);
        }

        [Fact]
        public async Task can_read_conditionalprefix_dic()
        {
            var filePath = @"files/conditionalprefix.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["drink"].ShouldHaveCount(2);
            actual["drink"][0].Flags.ShouldBeValues(['Q', 'R']);
            actual["drink"][0].Morphs.ShouldBe(["po:verb"]);
            actual["drink"][1].Flags.ShouldBeValues(['S']);
            actual["drink"][1].Morphs.ShouldBe(["po:noun"]);
        }

        [Fact]
        public async Task can_read_digits_in_words_dic()
        {
            var filePath = @"files/digits_in_words.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "-jährig"
            ], ignoreOrder: true);
            actual["0"][0].Flags.ShouldBeValues(['a']);
            actual["-jährig"][0].Flags.ShouldBeValues(['b', 'c']);
        }

        [Fact]
        public async Task can_read_flag_dic()
        {
            var filePath = @"files/flag.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues(['3', 'A']);
        }

        [Fact]
        public async Task can_read_flaglong_dic()
        {
            var filePath = @"files/flaglong.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues(['z' << 8 | 'x', '0' << 8 | '9'], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_flagnum_dic()
        {
            var filePath = @"files/flagnum.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues([999, 54321]);
        }

        [Fact]
        public async Task can_read_flagutf8_dic()
        {
            var filePath = @"files/flagutf8.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["foo"][0].Flags.ShouldBeValues(['A', 'Ü']);
        }

        [Fact]
        public async Task can_read_fogemorpheme_dic()
        {
            var filePath = @"files/fogemorpheme.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(2);
            actual.RootWords.ShouldBe(["gata", "kontoret"], ignoreOrder: true);
            actual["gata"][0].Flags.ShouldBeValues(['A']);
            actual["kontoret"][0].Flags.ShouldBeValues(['X']);
        }

        [Fact]
        public async Task can_read_forbiddenword_dic()
        {
            var filePath = @"files/forbiddenword.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "foo",
                "bar",
                "bars",
                "foos",
                "kg",
                "Kg",
                "KG",
                "cm",
                "Cm"
            ], ignoreOrder: true);
            actual["foo"][0].Flags.ShouldBeValues(['S']);
            actual["foo"][0].Morphs.ShouldBeEmpty();
            actual["foo"][1].Flags.ShouldBeValues(['X', 'Y']);
            actual["foo"][1].Morphs.ShouldBeEmpty();
            actual["bar"][0].Flags.ShouldBeValues(['S', 'Y']);
            actual["bar"][0].Morphs.ShouldBeEmpty();
            actual["bars"][0].Flags.ShouldBeValues(['X']);
            actual["foos"][0].Flags.ShouldBeValues(['X']);
            actual["kg"][0].Flags.ShouldBeEmpty();
            actual["Kg"][0].Flags.ShouldBeValues(['X']);
            actual["KG"][0].Flags.ShouldBeValues(['X']);
            actual["cm"][0].Flags.ShouldBeEmpty();
            actual["Cm"][0].Flags.ShouldBeValues(['X']);
        }

        [Fact]
        public async Task can_read_i58202_dic()
        {
            var filePath = @"files/i58202.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "foo",
                "bar",
                "Baz",
                "Boo"
            ], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_iconv_dic()
        {
            var filePath = @"files/iconv.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "Chișinău",
                "Țepes",
                "ț",
                "Ș"
            ], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_ignore_dic()
        {
            var filePath = @"files/ignore.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["xmpl", "xprssn"]);
        }

        [Fact]
        public async Task can_read_ignoreutf_dic()
        {
            var filePath = @"files/ignoreutf.dic";
            string[] expectedWords =
            [
                "طِير",
                "فَتحة",
                "ضُمة",
                "كِسرة",
                "فتحًتان",
                "ضمتانٌ",
                "كسرتاٍن",
                "شدّة",
                "سكوْن"
            ];

            foreach (var ignoreChar in new[] { 1618, 1617, 1616, 1615, 1614, 1613, 1612, 1611 })
            {
                var ignoreString = ((char)ignoreChar).ToString();

                for (var i = 0; i < expectedWords.Length; i++)
                {
                    expectedWords[i] = expectedWords[i].Replace(ignoreString, "");
                }
            }

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(expectedWords, ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_IJ_dic()
        {
            var filePath = @"files/IJ.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["ijs", "Ijs"], ignoreOrder: true);
            actual["ijs"].ShouldHaveSingleItem().Flags.ShouldBeValues(['i']);
            actual["Ijs"].ShouldHaveSingleItem().Flags.ShouldBeValues(['*']);
        }

        [Fact]
        public async Task can_read_keepcase_dic()
        {
            var filePath = @"files/keepcase.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "foo",
                "Bar",
                "baz.",
                "Quux."
            ], ignoreOrder: true);
            actual["baz."][0].Flags.ShouldBeValues(['A']);
        }

        [Fact]
        public async Task can_read_korean_dic()
        {
            var filePath = @"files/korean.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.Count().ShouldBeGreaterThanOrEqualTo(2);
            actual.RootWords.ShouldContain("들어오세요");
            actual.RootWords.ShouldContain("안녕하세요");
        }

        [Fact]
        public async Task can_read_maputf_dic()
        {
            var filePath = @"files/maputf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["Frühstück", "tükörfúró", "groß"], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_morph_dic()
        {
            var filePath = @"files/morph.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "drink",
                "drank",
                "drunk",
                "eat",
                "ate",
                "eaten",
                "phenomenon",
                "phenomena"
            ], ignoreOrder: true);

            actual["drink"].ShouldHaveCount(2);
            actual["drink"][0].Flags.ShouldBeValues(['S']);
            actual["drink"][0].Morphs.ShouldBe(["po:noun"]);
            actual["drink"][1].Flags.ShouldBeValues(['Q', 'R']);
            actual["drink"][1].Morphs.ShouldBe(["po:verb", "al:drank", "al:drunk", "ts:present"]);
            actual["eaten"][0].Flags.ShouldBeEmpty();
            actual["eaten"][0].Morphs.ShouldBe(["po:verb", "st:eat", "is:past_2"]);
        }

        [Fact]
        public async Task can_read_nepali_dic()
        {
            var filePath = @"files/nepali.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "अलम्",
                "क्यार",
                "न्न",
                "र्‌य"
            ], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_ngram_utf_fix_dic()
        {
            var filePath = @"files/ngram_utf_fix.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem();
            actual["человек"][0].Flags.ShouldBeValues([2022, 2000, 101], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_phone_dic()
        {
            var filePath = @"files/phone.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "Brasilia",
                "brassily",
                "Brazilian",
                "brilliance",
                "brilliancy",
                "brilliant",
                "brain",
                "brass",
                "Churchillian",
                "xxxxxxxxxx"
            ], ignoreOrder: true);
            actual["xxxxxxxxxx"][0].Flags.ShouldBeEmpty();
            actual["xxxxxxxxxx"][0].Morphs.ShouldBe(["ph:Brasilia"]);
        }

        [Fact]
        public async Task can_read_slash_dic()
        {
            var filePath = @"files/slash.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "/",
                "1/2",
                "http://",
                "/usr/share/myspell/"
            ], ignoreOrder: true);
            actual["/"][0].Flags.ShouldBeEmpty();
            actual["1/2"][0].Flags.ShouldBeEmpty();
            actual["http://"][0].Flags.ShouldBeEmpty();
            actual["/usr/share/myspell/"][0].Flags.ShouldBeEmpty();
        }

        [Fact]
        public async Task can_read_sugutf_dic()
        {
            var filePath = @"files/sugutf.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(
            [
                "NASA",
                "Gandhi",
                "grateful",
                "permanent",
                "vacation",
                "a",
                "lot",
                "have",
                "which",
                "McDonald",
                "Mcdonald"
            ], ignoreOrder: true);
            actual["McDonald"][0].Flags.ShouldBeEmpty();
            actual["Mcdonald"][0].Flags.ShouldBe([SpecialFlags.OnlyUpcaseFlag]);
        }

        [Fact]
        public async Task can_read_utf8_bom_dic()
        {
            var filePath = @"files/utf8_bom.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem().ShouldBe("apéritif");
        }

        [Fact]
        public async Task can_read_utf8_bom2_dic()
        {
            var filePath = @"files/utf8_bom2.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveSingleItem().ShouldBe("apéritif");
        }

        [Fact]
        public async Task can_read_utf8_nonbmp_dic()
        {
            var filePath = @"files/utf8_nonbmp.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldBe(["𐏑", "𐏒", "𐏒𐏑", "𐏒𐏒"], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_warn_dic()
        {
            var filePath = @"files/warn.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(2);
            actual.RootWords.ShouldBe(["foo", "bar"], ignoreOrder: true);
            actual["foo"][0].Flags.ShouldBeValues(['A', 'W'], ignoreOrder: true);
        }

        [Fact]
        public async Task can_read_zeroaffix_dic()
        {
            var filePath = @"files/zeroaffix.dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldHaveCount(2);
            actual.RootWords.ShouldBe(["foo", "bar"], ignoreOrder: true);
            actual["foo"][0].Flags.ShouldBeValues(['X', 'A'], ignoreOrder: true);
            actual["foo"][0].Morphs.ShouldBe(["<FOO"]);
            actual["bar"][0].Flags.ShouldBeValues(['X', 'A', 'B', 'C'], ignoreOrder: true);
            actual["bar"][0].Morphs.ShouldBe(["<BAR"]);
        }

        [Fact]
        public async Task can_read_english_dic()
        {
            var filePath = @"files/English (American).dic";

            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.RootWords.ShouldNotBeEmpty();
            actual.Affix.Warnings.ShouldBeEmpty();
        }

        [Theory(Skip = "Not performant enough yet")]
        [MemberData(nameof(large_assortment_of_dic_files))]
        public async Task can_read_file_without_exception(string filePath)
        {
            var actual = await WordListReader.ReadFileAsync(filePath, TestCancellation);

            actual.ShouldNotBeNull();
        }
    }

    public class ReadFile : WordListReaderTests
    {
        [Fact]
        public void can_read_english_dic()
        {
            var filePath = @"files/English (American).dic";

            var actual = WordListReader.ReadFile(filePath);

            actual.RootWords.ShouldNotBeEmpty();
            actual.Affix.Warnings.ShouldBeEmpty();
        }

        [Theory(Skip = "Not performant enough yet")]
        [MemberData(nameof(large_assortment_of_dic_files))]
        public void can_read_file_without_exception(string filePath)
        {
            var actual = WordListReader.ReadFile(filePath);

            actual.ShouldNotBeNull();
        }
    }

    public static IEnumerable<object[]> large_assortment_of_dic_files
    {
        get
        {
            IEnumerable<string> files = Directory.GetFiles("files/", "*.dic");
            if (Directory.Exists("samples"))
            {
                files = files.Concat(Directory.GetFiles("samples/", "*.dic"));
            }

            return files
                .OrderBy(x => x)
                .Select(filePath => new object[] { filePath });
        }
    }
}
