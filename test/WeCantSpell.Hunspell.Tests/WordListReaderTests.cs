using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WeCantSpell.Hunspell.Tests
{
    public class WordListReaderTests
    {
        public class ReadFileAsync : WordListReaderTests
        {
            [Fact]
            public async Task can_read_1463589_utf_dic()
            {
                var filePath = @"files/1463589_utf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.Affix.Should().NotBeNull();
                actual.AllEntries.Should().HaveCount(1);
                var entry = actual.AllEntries.Single();
                entry.Flags.Should().BeNullOrEmpty();
                entry.Morphs.Should().BeNullOrEmpty();
                entry.Options.Should().Be(WordEntryOptions.None);
                entry.Word.Should().Be("Kühlschrank");
            }

            [Fact]
            public async Task can_read_1592880_dic()
            {
                var filePath = @"files/1592880.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.ShouldBeEquivalentTo(new[] { "weg", "wege" });
                actual["weg"][0].Flags.Should().ContainInOrder(new[] { 'Q', 'o', 'z' });
                actual["weg"][1].Flags.Should().ContainInOrder(new[] { 'P' });
                actual["wege"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_1695964_dic()
            {
                var filePath = @"files/1695964.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["Mull"].Should().HaveCount(2);
                actual["Mull"][0].Flags.Should().ContainInOrder(new[] { 'e', 'h' });
                actual["Mull"][1].Flags.Should().ContainInOrder(new[] { 'S' });
            }

            [Fact]
            public async Task can_read_1706659_dic()
            {
                var filePath = @"files/1706659.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(3);
                actual["arbeits"][0].Flags.Should().ContainInOrder(new[] { 'v' });
                actual["scheu"][0].Flags.Should().ContainInOrder(new[] { 'A', 'w' });
                actual["farbig"][0].Flags.Should().ContainInOrder(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_1975530_dic()
            {
                var filePath = @"files/1975530.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual["أرى"][0].Flags.Should().ContainInOrder(new[] { 'x' });
                actual["أيار"][0].Flags.Should().ContainInOrder(new[] { 'x' });
            }

            [Fact]
            public async Task can_read_alias_dic()
            {
                var filePath = @"files/alias.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'A', 'B' });
            }

            [Fact]
            public async Task can_read_alias2_dic()
            {
                var filePath = @"files/alias2.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'A', 'B' });
                actual["foo"][0].Morphs.Should().ContainInOrder(new[] { "po:noun", "xx:other_data" });
            }

            [Fact]
            public async Task can_read_alias3_dic()
            {
                var filePath = @"files/alias3.dic";
                var reversedStem = new string("[stem_1]".ToCharArray().Reverse().ToArray());

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["oruo"][0].Flags.Should().ContainInOrder(new[] { 'B', 'C' });
                actual["oruo"][0].Morphs.Should().OnlyContain(x => x == reversedStem);
            }

            [Fact]
            public async Task can_read_allcaps_dic()
            {
                var filePath = @"files/allcaps.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual["OpenOffice.org"].Should().HaveCount(1);
                actual["OpenOffice.org"][0].Flags.Should().BeEmpty();
                actual["Openoffice.org"].Should().HaveCount(1);
                actual["Openoffice.org"][0].Flags.Should().ContainInOrder(new[] { SpecialFlags.OnlyUpcaseFlag });
                actual["UNICEF"].Should().HaveCount(1);
                actual["UNICEF"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["Unicef"].Should().HaveCount(1);
                actual["Unicef"][0].Flags.Should().ContainInOrder(new[] { 'S', (char)SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_allcaps2_dic()
            {
                var filePath = @"files/allcaps2.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual["iPod"][0].Flags.Should().ContainInOrder(new[] { 's' });
                actual["Ipod"][0].Flags.Should().ContainInOrder(new[] { 's', (char)SpecialFlags.OnlyUpcaseFlag });
                actual["iPodos"][0].Flags.Should().ContainInOrder(new[] { '*' });
                actual["ipodos"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_allcaps3_dic()
            {
                var filePath = @"files/allcaps3.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(6);
                actual["UNESCO"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["Unesco"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["Nasa"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["NASA"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["ACTS"][0].Flags.Should().BeEmpty();
                actual["act"][0].Flags.Should().ContainInOrder(new[] { 's' });
            }

            [Fact]
            public async Task can_read_allcaps_utf_dic()
            {
                var filePath = @"files/allcaps_utf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual["OpenOffice.org"].Should().HaveCount(1);
                actual["OpenOffice.org"][0].Flags.Should().BeEmpty();
                actual["Openoffice.org"].Should().HaveCount(1);
                actual["Openoffice.org"][0].Flags.Should().ContainInOrder(new[] { SpecialFlags.OnlyUpcaseFlag });
                actual["UNICEF"].Should().HaveCount(1);
                actual["UNICEF"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["Unicef"].Should().HaveCount(1);
                actual["Unicef"][0].Flags.Should().ContainInOrder(new[] { 'S', (char)SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_arabic_dic()
            {
                var filePath = @"files/arabic.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["ب"][0].Word.Should().Be("ب");
            }

            [Fact]
            public async Task can_read_base_dic()
            {
                var filePath = @"files/base.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(28);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "Hunspell" });

                actual["create"][0].Flags.Should().ContainInOrder(new int[] { 'X', 'K', 'V', 'N', 'G', 'A', 'D', 'S' }.OrderBy(x => x));
                actual["Hunspell"][0].Flags.Should().BeEmpty();
                actual["text"][0].Flags.Should().BeEmpty();
                actual["FAQ"][0].Flags.Should().ContainInOrder(new int[] { 'M', 'S' });
                actual["Faq"][0].Flags.Should().ContainInOrder(new int[] { 'M', 'S', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_base_utf_dic()
            {
                var filePath = @"files/base_utf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(28);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "Hunspell" });

                actual["create"][0].Flags.Should().ContainInOrder(new int[] { 'X', 'K', 'V', 'N', 'G', 'A', 'D', 'S' }.OrderBy(x => x));
                actual["Hunspell"][0].Flags.Should().BeEmpty();
                actual["text"][0].Flags.Should().BeEmpty();
                actual["FAQ"][0].Flags.Should().ContainInOrder(new int[] { 'M', 'S' });
                actual["Faq"][0].Flags.Should().ContainInOrder(new int[] { 'M', 'S', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_break_dic()
            {
                var filePath = @"files/break.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(3);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "bar",
                    "fox-bax" });
            }

            [Fact]
            public async Task can_read_checkcompoundcase_dic()
            {
                var filePath = @"files/checkcompoundcase.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(5);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "Bar",
                    "BAZ",
                    "Baz",
                    "-" });

                actual["BAZ"][0].Flags.Should().ContainInOrder(new[] { 'A' });
                actual["Baz"][0].Flags.Should().ContainInOrder(new[] { 'A', (char)SpecialFlags.OnlyUpcaseFlag });
                actual["-"][0].Flags.Should().ContainInOrder(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_checkcompoundcaseutf_dic()
            {
                var filePath = @"files/checkcompoundcaseutf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] { "áoó", "Óoá" });
            }

            [Fact]
            public async Task can_read_checkcompoundpattern4_dic()
            {
                var filePath = @"files/checkcompoundpattern4.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(7);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "sUrya",
                    "Surya",
                    "udayaM",
                    "Udayam",
                    "pEru",
                    "Peru",
                    "unna" });

                actual["pEru"][0].Flags.Should().ContainInOrder(new[] { 'B', 'x' });
                actual["Peru"][0].Flags.Should().ContainInOrder(new[] { 'B', 'x', (char)SpecialFlags.OnlyUpcaseFlag });
                actual["unna"][0].Flags.Should().ContainInOrder(new[] { 'B', 'x' });
            }

            [Fact]
            public async Task can_read_checkcompoundtriple_dic()
            {
                var filePath = @"files/checkcompoundtriple.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "opera",
                    "eel",
                    "bare" });
            }

            [Fact]
            public async Task can_read_checksharpsutf_dic()
            {
                var filePath = @"files/checksharpsutf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(6);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "müßig",
                    "Ausstoß",
                    "Abstoß.",
                    "Außenabmessung",
                    "Prozessionsstraße",
                    "Außenmaße" });

                actual["müßig"][0].Flags.Should().ContainInOrder(new[] { 'k' });
            }

            [Fact]
            public async Task can_read_circumfix_dic()
            {
                var filePath = @"files/circumfix.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["nagy"][0].Flags.Should().ContainInOrder(new[] { 'C' });
                actual["nagy"][0].Morphs.Should().ContainInOrder(new[] { "po:adj" });
            }

            [Fact]
            public async Task can_read_colons_in_words_dic()
            {
                var filePath = @"files/colons_in_words.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(3);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "c:a",
                    "S:t",
                    "foo" });
            }

            [Fact]
            public async Task can_read_complexprefixes2_dic()
            {
                var filePath = @"files/complexprefixes2.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["oruo"][0].Flags.Should().ContainInOrder(new[] { 'B', 'C' });
                actual["oruo"][0].Morphs.Should().ContainInOrder(new[] { "]1_mets[" });
            }

            [Fact]
            public async Task can_read_compoundaffix_dic()
            {
                var filePath = @"files/compoundaffix.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'X', 'P', 'S' }.OrderBy(x => x));
                actual["bar"][0].Flags.Should().ContainInOrder(new[] { 'X', 'P', 'S' }.OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_compoundrule4_dic()
            {
                var filePath = @"files/compoundrule4.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(23);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "9th" });
            }

            [Fact]
            public async Task can_read_compoundrule5_dic()
            {
                var filePath = @"files/compoundrule5.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(13);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "‰" });

                actual["0"][0].Flags.Should().ContainInOrder(new[] { 'N' });
                actual["0"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:num" });
                actual["."][0].Flags.Should().ContainInOrder(new[] { '.' });
                actual["."][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_dot" });
                actual["%"][0].Flags.Should().ContainInOrder(new[] { '%' });
                actual["%"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_percent" });
                actual["‰"][0].Flags.Should().ContainInOrder(new[] { '%' });
                actual["‰"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_per_mille" });
            }

            [Fact]
            public async Task can_read_compoundrule7_dic()
            {
                var filePath = @"files/compoundrule7.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(23);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "9th" });
                actual["0"][0].Flags.Should().ContainInOrder(new[] { 'n' << 8 | 'n', 'm' << 8 | 'm' }.OrderBy(x => x));
                actual["3rd"][0].Flags.Should().ContainInOrder(new[] { 'p' << 8 | 'p' });
                actual["9th"][0].Flags.Should().ContainInOrder(new[] { 'p' << 8 | 'p', 't' << 8 | 't' }.OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_compoundrule8_dic()
            {
                var filePath = @"files/compoundrule8.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(23);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "9th" });
                actual["0"][0].Flags.Should().ContainInOrder(new[] { 1001, 2002 });
                actual["1st"][0].Flags.Should().ContainInOrder(new[] { 2000 });
                actual["9th"][0].Flags.Should().ContainInOrder(new[] { 2000, 2001 });
            }

            [Fact]
            public async Task can_read_condition_utf_dic()
            {
                var filePath = @"files/condition_utf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["óőó"][0].Flags.Should().ContainInOrder(new[] { 'P', 'S' });
            }

            [Fact]
            public async Task can_read_conditionalprefix_dic()
            {
                var filePath = @"files/conditionalprefix.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["drink"].Should().HaveCount(2);
                actual["drink"][0].Flags.Should().ContainInOrder(new[] { 'Q', 'R' });
                actual["drink"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:verb" });
                actual["drink"][1].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["drink"][1].Morphs.ShouldBeEquivalentTo(new[] { "po:noun" });
            }

            [Fact]
            public async Task can_read_digits_in_words_dic()
            {
                var filePath = @"files/digits_in_words.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(11);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "-jährig" });
                actual["0"][0].Flags.Should().ContainInOrder(new[] { 'a' });
                actual["-jährig"][0].Flags.Should().ContainInOrder(new[] { 'b', 'c' });
            }

            [Fact]
            public async Task can_read_flag_dic()
            {
                var filePath = @"files/flag.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { '3', 'A', });
            }

            [Fact]
            public async Task can_read_flaglong_dic()
            {
                var filePath = @"files/flaglong.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'z' << 8 | 'x', '0' << 8 | '9' }.OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_flagnum_dic()
            {
                var filePath = @"files/flagnum.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 999, 54321 });
            }

            [Fact]
            public async Task can_read_flagutf8_dic()
            {
                var filePath = @"files/flagutf8.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'A', 'Ü' });
            }

            [Fact]
            public async Task can_read_fogemorpheme_dic()
            {
                var filePath = @"files/fogemorpheme.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "gata",
                    "kontoret" });
                actual["gata"][0].Flags.Should().ContainInOrder(new[] { 'A' });
                actual["kontoret"][0].Flags.Should().ContainInOrder(new object[] { 'X' });
            }

            [Fact]
            public async Task can_read_forbiddenword_dic()
            {
                var filePath = @"files/forbiddenword.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);


                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "bar",
                    "bars",
                    "foos" });
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["foo"][0].Morphs.ShouldBeEquivalentTo(new[] { "[1]" });
                actual["foo"][1].Flags.Should().ContainInOrder(new[] { 'X', 'Y' });
                actual["foo"][1].Morphs.ShouldBeEquivalentTo(new[] { "[2]" });
                actual["foo"][2].Flags.Should().ContainInOrder(new[] { 'Y' });
                actual["foo"][2].Morphs.ShouldBeEquivalentTo(new[] { "[3]" });
                actual["foo"][3].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["foo"][3].Morphs.ShouldBeEquivalentTo(new[] { "[4]" });
                actual["bar"][0].Flags.Should().ContainInOrder(new[] { 'S', 'Y' });
                actual["bar"][0].Morphs.ShouldBeEquivalentTo(new[] { "[5]" });
                actual["bars"][0].Flags.Should().ContainInOrder(new[] { 'X' });
                actual["foos"][0].Flags.Should().ContainInOrder(new[] { 'X' });
            }

            [Fact]
            public async Task can_read_i58202_dic()
            {
                var filePath = @"files/i58202.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "bar",
                    "Baz",
                    "Boo" });
            }

            [Fact]
            public async Task can_read_iconv_dic()
            {
                var filePath = @"files/iconv.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "Chișinău",
                    "Țepes",
                    "ț",
                    "Ș" });
            }

            [Fact]
            public async Task can_read_ignore_dic()
            {
                var filePath = @"files/ignore.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "xmpl",
                    "xprssn" });
            }

            [Fact]
            public async Task can_read_ignoreutf_dic()
            {
                var filePath = @"files/ignoreutf.dic";
                var ignoreChars = new[] { 1618, 1617, 1616, 1615, 1614, 1613, 1612, 1611 }.Select(i => (char)i).ToArray();
                IEnumerable<string> expectedWords = new[]
                {
                    "طِير",
                    "فَتحة",
                    "ضُمة",
                    "كِسرة",
                    "فتحًتان",
                    "ضمتانٌ",
                    "كسرتاٍن",
                    "شدّة",
                    "سكوْن"
                };
                foreach (var ignoreChar in ignoreChars)
                {
                    expectedWords = expectedWords.Select(w => w.Replace(ignoreChar.ToString(), ""));
                }

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(9);
                actual.RootWords.ShouldBeEquivalentTo(expectedWords);
            }

            [Fact]
            public async Task can_read_IJ_dic()
            {
                var filePath = @"files/IJ.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "ijs",
                    "Ijs" });
                actual["ijs"].Should().HaveCount(1);
                actual["ijs"][0].Flags.Should().ContainInOrder(new[] { 'i' });
                actual["Ijs"].Should().HaveCount(1);
                actual["Ijs"][0].Flags.Should().ContainInOrder(new[] { '*' });
            }

            [Fact]
            public async Task can_read_keepcase_dic()
            {
                var filePath = @"files/keepcase.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "foo",
                    "Bar",
                    "baz.",
                    "Quux." });
                actual["baz."][0].Flags.Should().ContainInOrder(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_korean_dic()
            {
                var filePath = @"files/korean.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "들어오세요",
                    "안녕하세요" });
            }

            [Fact]
            public async Task can_read_maputf_dic()
            {
                var filePath = @"files/maputf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(3);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "Frühstück",
                    "tükörfúró",
                    "groß" });
            }

            [Fact]
            public async Task can_read_morph_dic()
            {
                var filePath = @"files/morph.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(8);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "drink",
                    "drank",
                    "drunk",
                    "eat",
                    "ate",
                    "eaten",
                    "phenomenon",
                    "phenomena" });

                actual["drink"].Should().HaveCount(2);
                actual["drink"][0].Flags.Should().ContainInOrder(new[] { 'S' });
                actual["drink"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:noun" });
                actual["drink"][1].Flags.Should().ContainInOrder(new[] { 'Q', 'R' });
                actual["drink"][1].Morphs.Should().BeEquivalentTo(new[] { "po:verb", "al:drank", "al:drunk", "ts:present" });
                actual["eaten"][0].Flags.Should().BeEmpty();
                actual["eaten"][0].Morphs.Should().BeEquivalentTo(new[] { "po:verb", "st:eat", "is:past_2" });
            }

            [Fact]
            public async Task can_read_nepali_dic()
            {
                var filePath = @"files/nepali.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "अलम्",
                    "क्यार",
                    "न्न",
                    "र्‌य" });
            }

            [Fact]
            public async Task can_read_ngram_utf_fix_dic()
            {
                var filePath = @"files/ngram_utf_fix.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual["человек"][0].Flags.Should().ContainInOrder(new[] { 2022, 2000, 101 }.OrderBy(x => x));
            }

            [Fact]
            public async Task can_read_phone_dic()
            {
                var filePath = @"files/phone.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(10);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "Brasilia",
                    "brassily",
                    "Brazilian",
                    "brilliance",
                    "brilliancy",
                    "brilliant",
                    "brain",
                    "brass",
                    "Churchillian",
                    "xxxxxxxxxx" });
                actual["xxxxxxxxxx"][0].Flags.Should().BeEmpty();
                actual["xxxxxxxxxx"][0].Morphs.ShouldBeEquivalentTo(new[] { "ph:Brasilia" });
            }

            [Fact]
            public async Task can_read_slash_dic()
            {
                var filePath = @"files/slash.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "/",
                    "1/2",
                    "http://",
                    "/usr/share/myspell/" });
                actual["/"][0].Flags.Should().BeEmpty();
                actual["1/2"][0].Flags.Should().BeEmpty();
                actual["http://"][0].Flags.Should().BeEmpty();
                actual["/usr/share/myspell/"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_sugutf_dic()
            {
                var filePath = @"files/sugutf.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(11);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
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
                    "Mcdonald" });
                actual["McDonald"][0].Flags.Should().BeEmpty();
                actual["Mcdonald"][0].Flags.ShouldBeEquivalentTo(new[] { SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_utf8_bom_dic()
            {
                var filePath = @"files/utf8_bom.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual.RootWords.Should().ContainSingle("apéritif");
            }

            [Fact]
            public async Task can_read_utf8_bom2_dic()
            {
                var filePath = @"files/utf8_bom2.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(1);
                actual.RootWords.Should().ContainSingle("apéritif");
            }

            [Fact]
            public async Task can_read_utf8_nonbmp_dic()
            {
                var filePath = @"files/utf8_nonbmp.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(4);
                actual.RootWords.ShouldBeEquivalentTo(new[] {
                    "𐏑",
                    "𐏒",
                    "𐏒𐏑",
                    "𐏒𐏒" });
            }

            [Fact]
            public async Task can_read_warn_dic()
            {
                var filePath = @"files/warn.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] { "foo", "bar" });
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'A', 'W' });
            }

            [Fact]
            public async Task can_read_zeroaffix_dic()
            {
                var filePath = @"files/zeroaffix.dic";

                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.RootWords.Should().HaveCount(2);
                actual.RootWords.ShouldBeEquivalentTo(new[] { "foo", "bar" });
                actual["foo"][0].Flags.Should().ContainInOrder(new[] { 'X', 'A' }.OrderBy(x => x));
                actual["foo"][0].Morphs.ShouldBeEquivalentTo(new[] { "<FOO" });
                actual["bar"][0].Flags.Should().ContainInOrder(new[] { 'X', 'A', 'B', 'C' }.OrderBy(x => x));
                actual["bar"][0].Morphs.ShouldBeEquivalentTo(new[] { "<BAR" });
            }

            public static IEnumerable<object[]> can_read_file_without_exception_args =>
                Directory.GetFiles("files/", "*.dic").Select(filePath => new object[] { filePath });

            [Theory, MemberData(nameof(can_read_file_without_exception_args))]
            public async Task can_read_file_without_exception(string filePath)
            {
                var actual = await WordListReader.ReadFileAsync(filePath);

                actual.Should().NotBeNull();
            }
        }
    }
}
