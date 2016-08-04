using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class DictionaryReaderTests
    {
        public class ReadFileAsync : DictionaryReaderTests
        {
            [Fact]
            public async Task can_read_1463589_utf_dic()
            {
                var filePath = @"files/1463589_utf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Affix.Should().NotBeNull();
                actual.Entries.Should().HaveCount(1);
                var hashEntries = actual.Entries.Values.Single();
                hashEntries.Should().HaveCount(1);
                var entry = hashEntries.Single();
                entry.Flags.Should().BeNullOrEmpty();
                entry.Morphs.Should().BeNullOrEmpty();
                entry.Options.Should().Be(DictionaryEntryOptions.None);
                entry.Word.Should().Be("Kühlschrank");
            }

            [Fact]
            public async Task can_read_1592880_dic()
            {
                var filePath = @"files/1592880.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Keys.ShouldBeEquivalentTo(new[] { "weg", "wege" });
                actual.Entries["weg"][0].Flags.ShouldBeEquivalentTo(new[] { 'Q', 'o', 'z' });
                actual.Entries["weg"][1].Flags.ShouldBeEquivalentTo(new[] { 'P' });
                actual.Entries["wege"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_1695964_dic()
            {
                var filePath = @"files/1695964.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["Mull"].Should().HaveCount(2);
                actual.Entries["Mull"][0].Flags.ShouldBeEquivalentTo(new[] { 'h', 'e' });
                actual.Entries["Mull"][1].Flags.ShouldBeEquivalentTo(new[] { 'S' });
            }

            [Fact]
            public async Task can_read_1706659_dic()
            {
                var filePath = @"files/1706659.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(3);
                actual.Entries["arbeits"][0].Flags.ShouldBeEquivalentTo(new[] { 'v' });
                actual.Entries["scheu"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', 'w' });
                actual.Entries["farbig"][0].Flags.ShouldBeEquivalentTo(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_1975530_dic()
            {
                var filePath = @"files/1975530.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries["أرى"][0].Flags.ShouldBeEquivalentTo(new[] { 'x' });
                actual.Entries["أيار"][0].Flags.ShouldBeEquivalentTo(new[] { 'x' });
            }

            [Fact]
            public async Task can_read_alias_dic()
            {
                var filePath = @"files/alias.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', 'B' });
            }

            [Fact]
            public async Task can_read_alias2_dic()
            {
                var filePath = @"files/alias2.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', 'B' });
                actual.Entries["foo"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:noun", "xx:other_data" });
            }

            [Fact]
            public async Task can_read_alias3_dic()
            {
                var filePath = @"files/alias3.dic";
                var reversedStem = new string("[stem_1]".ToCharArray().Reverse().ToArray());

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["oruo"][0].Flags.ShouldBeEquivalentTo(new[] { 'B', 'C' });
                actual.Entries["oruo"][0].Morphs.Should().OnlyContain(x => x == reversedStem);
            }

            [Fact]
            public async Task can_read_allcaps_dic()
            {
                var filePath = @"files/allcaps.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries["OpenOffice.org"].Should().HaveCount(1);
                actual.Entries["OpenOffice.org"][0].Flags.Should().BeEmpty();
                actual.Entries["Openoffice.org"].Should().HaveCount(1);
                actual.Entries["Openoffice.org"][0].Flags.ShouldBeEquivalentTo(new[] { SpecialFlags.OnlyUpcaseFlag });
                actual.Entries["UNICEF"].Should().HaveCount(1);
                actual.Entries["UNICEF"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["Unicef"].Should().HaveCount(1);
                actual.Entries["Unicef"][0].Flags.ShouldBeEquivalentTo(new[] { 'S', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_allcaps2_dic()
            {
                var filePath = @"files/allcaps2.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries["iPod"][0].Flags.ShouldBeEquivalentTo(new[] { 's' });
                actual.Entries["Ipod"][0].Flags.ShouldBeEquivalentTo(new[] { 's', SpecialFlags.OnlyUpcaseFlag });
                actual.Entries["iPodos"][0].Flags.ShouldBeEquivalentTo(new[] { '*' });
                actual.Entries["ipodos"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_allcaps3_dic()
            {
                var filePath = @"files/allcaps3.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(6);
                actual.Entries["UNESCO"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["Unesco"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["Nasa"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["NASA"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["ACTS"][0].Flags.Should().BeEmpty();
                actual.Entries["act"][0].Flags.ShouldBeEquivalentTo(new[] { 's' });
            }

            [Fact]
            public async Task can_read_allcaps_utf_dic()
            {
                var filePath = @"files/allcaps_utf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries["OpenOffice.org"].Should().HaveCount(1);
                actual.Entries["OpenOffice.org"][0].Flags.Should().BeEmpty();
                actual.Entries["Openoffice.org"].Should().HaveCount(1);
                actual.Entries["Openoffice.org"][0].Flags.ShouldBeEquivalentTo(new[] { SpecialFlags.OnlyUpcaseFlag });
                actual.Entries["UNICEF"].Should().HaveCount(1);
                actual.Entries["UNICEF"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["Unicef"].Should().HaveCount(1);
                actual.Entries["Unicef"][0].Flags.ShouldBeEquivalentTo(new[] { 'S', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_arabic_dic()
            {
                var filePath = @"files/arabic.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["ب"][0].Word.Should().Be("ب");
            }

            [Fact]
            public async Task can_read_base_dic()
            {
                var filePath = @"files/base.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(28);
                actual.Entries.Should().ContainKeys(
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
                    //ext", // duplicate
                    "horrifying",
                    "speech",
                    "suggest",
                    "uncreate",
                    "Hunspell");

                actual.Entries["create"][0].Flags.Should().BeEquivalentTo(new int[] { 'X', 'K', 'V', 'N', 'G', 'A', 'D', 'S' });
                actual.Entries["Hunspell"][0].Flags.Should().BeEmpty();
                actual.Entries["text"][0].Flags.Should().BeEmpty();
                actual.Entries["FAQ"][0].Flags.ShouldBeEquivalentTo(new int[] { 'S', 'M' });
                actual.Entries["Faq"][0].Flags.ShouldBeEquivalentTo(new int[] { 'S', 'M', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_base_utf_dic()
            {
                var filePath = @"files/base_utf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(28);
                actual.Entries.Should().ContainKeys(
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
                    "text", // duplicate
                    "horrifying",
                    "speech",
                    "suggest",
                    "uncreate",
                    "Hunspell");

                actual.Entries["create"][0].Flags.Should().BeEquivalentTo(new int[] { 'X', 'K', 'V', 'N', 'G', 'A', 'D', 'S' });
                actual.Entries["Hunspell"][0].Flags.Should().BeEmpty();
                actual.Entries["text"][0].Flags.Should().BeEmpty();
                actual.Entries["FAQ"][0].Flags.ShouldBeEquivalentTo(new int[] { 'S', 'M' });
                actual.Entries["Faq"][0].Flags.ShouldBeEquivalentTo(new int[] { 'S', 'M', SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_break_dic()
            {
                var filePath = @"files/break.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(3);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "bar",
                    "fox-bax");
            }

            [Fact]
            public async Task can_read_checkcompoundcase_dic()
            {
                var filePath = @"files/checkcompoundcase.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(5);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "Bar",
                    "BAZ",
                    "Baz",
                    "-");

                actual.Entries["BAZ"][0].Flags.ShouldBeEquivalentTo(new[] { 'A' });
                actual.Entries["Baz"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', SpecialFlags.OnlyUpcaseFlag });
                actual.Entries["-"][0].Flags.ShouldBeEquivalentTo(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_checkcompoundcaseutf_dic()
            {
                var filePath = @"files/checkcompoundcaseutf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKey("áoó");
                actual.Entries.Should().ContainKey("Óoá");
            }

            [Fact]
            public async Task can_read_checkcompoundpattern4_dic()
            {
                var filePath = @"files/checkcompoundpattern4.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(7);
                actual.Entries.Should().ContainKeys(
                    "sUrya",
                    "Surya",
                    "udayaM",
                    "Udayam",
                    "pEru",
                    "Peru",
                    "unna");

                actual.Entries["pEru"][0].Flags.ShouldBeEquivalentTo(new[] { 'B', 'x' });
                actual.Entries["Peru"][0].Flags.ShouldBeEquivalentTo(new[] { 'B', 'x', SpecialFlags.OnlyUpcaseFlag });
                actual.Entries["unna"][0].Flags.ShouldBeEquivalentTo(new[] { 'B', 'x' });
            }

            [Fact]
            public async Task can_read_checkcompoundtriple_dic()
            {
                var filePath = @"files/checkcompoundtriple.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "opera",
                    "eel",
                    "bare");
            }

            [Fact]
            public async Task can_read_checksharpsutf_dic()
            {
                var filePath = @"files/checksharpsutf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(6);
                actual.Entries.Should().ContainKeys(
                    "müßig",
                    "Ausstoß",
                    "Abstoß.",
                    "Außenabmessung",
                    "Prozessionsstraße",
                    "Außenmaße");

                actual.Entries["müßig"][0].Flags.ShouldBeEquivalentTo(new[] { 'k' });
            }

            [Fact]
            public async Task can_read_circumfix_dic()
            {
                var filePath = @"files/circumfix.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries.Should().ContainKey("nagy");
                actual.Entries["nagy"][0].Flags.ShouldBeEquivalentTo(new[] { 'C' });
                actual.Entries["nagy"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:adj" });
            }

            [Fact]
            public async Task can_read_colons_in_words_dic()
            {
                var filePath = @"files/colons_in_words.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(3);
                actual.Entries.Should().ContainKeys(
                    "c:a",
                    "S:t",
                    "foo");
            }

            [Fact]
            public async Task can_read_complexprefixes2_dic()
            {
                var filePath = @"files/complexprefixes2.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries.Should().ContainKey("oruo");
                actual.Entries["oruo"][0].Flags.ShouldBeEquivalentTo(new[] { 'B', 'C' });
                actual.Entries["oruo"][0].Morphs.ShouldBeEquivalentTo(new[] { "]1_mets[" });
            }

            [Fact]
            public async Task can_read_compoundaffix_dic()
            {
                var filePath = @"files/compoundaffix.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKey("foo");
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'X', 'P', 'S' });
                actual.Entries.Should().ContainKey("bar");
                actual.Entries["bar"][0].Flags.ShouldBeEquivalentTo(new[] { 'X', 'P', 'S' });
            }

            [Fact]
            public async Task can_read_compoundrule4_dic()
            {
                var filePath = @"files/compoundrule4.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(23);
                actual.Entries.Should().ContainKeys(
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
                    "9th");
            }

            [Fact]
            public async Task can_read_compoundrule5_dic()
            {
                var filePath = @"files/compoundrule5.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(13);
                actual.Entries.Should().ContainKeys(
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
                    "‰");

                actual.Entries["0"][0].Flags.ShouldBeEquivalentTo(new[] { 'N' });
                actual.Entries["0"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:num" });
                actual.Entries["."][0].Flags.ShouldBeEquivalentTo(new[] { '.' });
                actual.Entries["."][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_dot" });
                actual.Entries["%"][0].Flags.ShouldBeEquivalentTo(new[] { '%' });
                actual.Entries["%"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_percent" });
                actual.Entries["‰"][0].Flags.ShouldBeEquivalentTo(new[] { '%' });
                actual.Entries["‰"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:sign_per_mille" });
            }

            [Fact]
            public async Task can_read_compoundrule7_dic()
            {
                var filePath = @"files/compoundrule7.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(23);
                actual.Entries.Should().ContainKeys(
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
                    "9th");
                actual.Entries["0"][0].Flags.ShouldBeEquivalentTo(new[] { 'n' << 8 | 'n', 'm' << 8 | 'm' });
                actual.Entries["3rd"][0].Flags.ShouldBeEquivalentTo(new[] { 'p' << 8 | 'p' });
                actual.Entries["9th"][0].Flags.ShouldBeEquivalentTo(new[] { 'p' << 8 | 'p', 't' << 8 | 't' });
            }

            [Fact]
            public async Task can_read_compoundrule8_dic()
            {
                var filePath = @"files/compoundrule8.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(23);
                actual.Entries.Should().ContainKeys(
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
                    "9th");
                actual.Entries["0"][0].Flags.ShouldBeEquivalentTo(new[] { 1001, 2002 });
                actual.Entries["1st"][0].Flags.ShouldBeEquivalentTo(new[] { 2000 });
                actual.Entries["9th"][0].Flags.ShouldBeEquivalentTo(new[] { 2000, 2001 });
            }

            [Fact]
            public async Task can_read_condition_utf_dic()
            {
                var filePath = @"files/condition_utf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["óőó"][0].Flags.ShouldBeEquivalentTo(new[] { 'S', 'P' });
            }

            [Fact]
            public async Task can_read_conditionalprefix_dic()
            {
                var filePath = @"files/conditionalprefix.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["drink"].Should().HaveCount(2);
                actual.Entries["drink"][0].Flags.ShouldBeEquivalentTo(new[] { 'R', 'Q' });
                actual.Entries["drink"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:verb" });
                actual.Entries["drink"][1].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["drink"][1].Morphs.ShouldBeEquivalentTo(new[] { "po:noun" });
            }

            [Fact]
            public async Task can_read_digits_in_words_dic()
            {
                var filePath = @"files/digits_in_words.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(11);
                actual.Entries.Should().ContainKeys(
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
                    "-jährig");
                actual.Entries["0"][0].Flags.ShouldBeEquivalentTo(new[] { 'a' });
                actual.Entries["-jährig"][0].Flags.ShouldBeEquivalentTo(new[] { 'b', 'c' });
            }

            [Fact]
            public async Task can_read_flag_dic()
            {
                var filePath = @"files/flag.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', '3' });
            }

            [Fact]
            public async Task can_read_flaglong_dic()
            {
                var filePath = @"files/flaglong.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'z' << 8 | 'x', '0' << 8 | '9' });
            }

            [Fact]
            public async Task can_read_flagnum_dic()
            {
                var filePath = @"files/flagnum.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 999, 54321 });
            }

            [Fact]
            public async Task can_read_flagutf8_dic()
            {
                var filePath = @"files/flagutf8.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'A', 'Ü' });
            }

            [Fact]
            public async Task can_read_fogemorpheme_dic()
            {
                var filePath = @"files/fogemorpheme.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys(
                    "gata",
                    "kontoret");
                actual.Entries["gata"][0].Flags.ShouldBeEquivalentTo(new[] { 'A' });
                actual.Entries["kontoret"][0].Flags.ShouldBeEquivalentTo(new object[] { 'X' });
            }

            [Fact]
            public async Task can_read_forbiddenword_dic()
            {
                var filePath = @"files/forbiddenword.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);


                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "bar",
                    "bars",
                    "foos");
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["foo"][0].Morphs.ShouldBeEquivalentTo(new[] { "[1]" });
                actual.Entries["foo"][1].Flags.ShouldBeEquivalentTo(new[] { 'Y', 'X' });
                actual.Entries["foo"][1].Morphs.ShouldBeEquivalentTo(new[] { "[2]" });
                actual.Entries["foo"][2].Flags.ShouldBeEquivalentTo(new[] { 'Y' });
                actual.Entries["foo"][2].Morphs.ShouldBeEquivalentTo(new[] { "[3]" });
                actual.Entries["foo"][3].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["foo"][3].Morphs.ShouldBeEquivalentTo(new[] { "[4]" });
                actual.Entries["bar"][0].Flags.ShouldBeEquivalentTo(new[] { 'Y', 'S' });
                actual.Entries["bar"][0].Morphs.ShouldBeEquivalentTo(new[] { "[5]" });
                actual.Entries["bars"][0].Flags.ShouldBeEquivalentTo(new[] { 'X' });
                actual.Entries["foos"][0].Flags.ShouldBeEquivalentTo(new[] { 'X' });
            }

            [Fact]
            public async Task can_read_i58202_dic()
            {
                var filePath = @"files/i58202.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "bar",
                    "Baz",
                    "Boo");
            }

            [Fact]
            public async Task can_read_iconv_dic()
            {
                var filePath = @"files/iconv.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "Chișinău",
                    "Țepes",
                    "ț",
                    "Ș");
            }

            [Fact]
            public async Task can_read_ignore_dic()
            {
                var filePath = @"files/ignore.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys(
                    "xmpl",
                    "xprssn");
            }

            [Fact]
            public async Task can_read_ignoreutf_dic()
            {
                var filePath = @"files/ignoreutf.dic";
                var ignoreChars = "ًٌٍَُِّْ".ToCharArray();
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

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(9);
                actual.Entries.Should().ContainKeys(expectedWords);
            }

            [Fact]
            public async Task can_read_IJ_dic()
            {
                var filePath = @"files/IJ.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys(
                    "ijs",
                    "Ijs");
                actual.Entries["ijs"].Should().HaveCount(1);
                actual.Entries["ijs"][0].Flags.ShouldBeEquivalentTo(new[] { 'i' });
                actual.Entries["Ijs"].Should().HaveCount(1);
                actual.Entries["Ijs"][0].Flags.ShouldBeEquivalentTo(new[] { '*' });
            }

            [Fact]
            public async Task can_read_keepcase_dic()
            {
                var filePath = @"files/keepcase.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "foo",
                    "Bar",
                    "baz.",
                    "Quux.");
                actual.Entries["baz."][0].Flags.ShouldBeEquivalentTo(new[] { 'A' });
            }

            [Fact]
            public async Task can_read_korean_dic()
            {
                var filePath = @"files/korean.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys(
                    "들어오세요",
                    "안녕하세요");
            }

            [Fact]
            public async Task can_read_maputf_dic()
            {
                var filePath = @"files/maputf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(3);
                actual.Entries.Should().ContainKeys(
                    "Frühstück",
                    "tükörfúró",
                    "groß");
            }

            [Fact]
            public async Task can_read_morph_dic()
            {
                var filePath = @"files/morph.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(8);
                actual.Entries.Should().ContainKeys(
                    "drink",
                    "drank",
                    "drunk",
                    "eat",
                    "ate",
                    "eaten",
                    "phenomenon",
                    "phenomena");

                actual.Entries["drink"].Should().HaveCount(2);
                actual.Entries["drink"][0].Flags.ShouldBeEquivalentTo(new[] { 'S' });
                actual.Entries["drink"][0].Morphs.ShouldBeEquivalentTo(new[] { "po:noun" });
                actual.Entries["drink"][1].Flags.ShouldBeEquivalentTo(new[] { 'R', 'Q' });
                actual.Entries["drink"][1].Morphs.Should().BeEquivalentTo(new[] { "po:verb", "al:drank", "al:drunk", "ts:present" });
                actual.Entries["eaten"][0].Flags.Should().BeEmpty();
                actual.Entries["eaten"][0].Morphs.Should().BeEquivalentTo(new[] { "po:verb", "st:eat", "is:past_2" });
            }

            [Fact]
            public async Task can_read_nepali_dic()
            {
                var filePath = @"files/nepali.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "अलम्",
                    "क्यार",
                    "न्न",
                    "र्‌य");
            }

            [Fact]
            public async Task can_read_ngram_utf_fix_dic()
            {
                var filePath = @"files/ngram_utf_fix.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries.Should().ContainKey("человек");
                actual.Entries["человек"][0].Flags.ShouldBeEquivalentTo(new[] { 2022, 2000, 101 });
            }

            [Fact]
            public async Task can_read_phone_dic()
            {
                var filePath = @"files/phone.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(10);
                actual.Entries.Should().ContainKeys(
                    "Brasilia",
                    "brassily",
                    "Brazilian",
                    "brilliance",
                    "brilliancy",
                    "brilliant",
                    "brain",
                    "brass",
                    "Churchillian",
                    "xxxxxxxxxx");
                actual.Entries["xxxxxxxxxx"][0].Flags.Should().BeEmpty();
                actual.Entries["xxxxxxxxxx"][0].Morphs.ShouldBeEquivalentTo(new[] { "ph:Brasilia" });
            }

            [Fact]
            public async Task can_read_slash_dic()
            {
                var filePath = @"files/slash.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "/",
                    "1/2",
                    "http://",
                    "/usr/share/myspell/");
                actual.Entries["/"][0].Flags.Should().BeEmpty();
                actual.Entries["1/2"][0].Flags.Should().BeEmpty();
                actual.Entries["http://"][0].Flags.Should().BeEmpty();
                actual.Entries["/usr/share/myspell/"][0].Flags.Should().BeEmpty();
            }

            [Fact]
            public async Task can_read_sugutf_dic()
            {
                var filePath = @"files/sugutf.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(11);
                actual.Entries.Should().ContainKeys(
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
                    "Mcdonald");
                actual.Entries["McDonald"][0].Flags.Should().BeEmpty();
                actual.Entries["Mcdonald"][0].Flags.ShouldBeEquivalentTo(new[] { SpecialFlags.OnlyUpcaseFlag });
            }

            [Fact]
            public async Task can_read_utf8_bom_dic()
            {
                var filePath = @"files/utf8_bom.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries.Should().ContainKey("apéritif");
            }

            [Fact]
            public async Task can_read_utf8_bom2_dic()
            {
                var filePath = @"files/utf8_bom2.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(1);
                actual.Entries.Should().ContainKey("apéritif");
            }

            [Fact]
            public async Task can_read_utf8_nonbmp_dic()
            {
                var filePath = @"files/utf8_nonbmp.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(4);
                actual.Entries.Should().ContainKeys(
                    "𐏑",
                    "𐏒",
                    "𐏒𐏑",
                    "𐏒𐏒");
            }

            [Fact]
            public async Task can_read_warn_dic()
            {
                var filePath = @"files/warn.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys("foo", "bar");
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'W', 'A' });
            }

            [Fact]
            public async Task can_read_zeroaffix_dic()
            {
                var filePath = @"files/zeroaffix.dic";

                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Entries.Should().HaveCount(2);
                actual.Entries.Should().ContainKeys("foo", "bar");
                actual.Entries["foo"][0].Flags.ShouldBeEquivalentTo(new[] { 'X', 'A' });
                actual.Entries["foo"][0].Morphs.ShouldBeEquivalentTo(new[] { "<FOO" });
                actual.Entries["bar"][0].Flags.ShouldBeEquivalentTo(new[] { 'X', 'A', 'B', 'C' });
                actual.Entries["bar"][0].Morphs.ShouldBeEquivalentTo(new[] { "<BAR" });
            }

            [Theory, MemberData("AllDicFilePaths")]
            public async Task can_read_file_without_exception(string filePath)
            {
                var actual = await DictionaryReader.ReadFileAsync(filePath);

                actual.Should().NotBeNull();
            }

            public static IEnumerable<object[]> AllDicFilePaths =>
                Array.ConvertAll(Directory.GetFiles("files/", "*.dic"), filePath => new object[] { filePath });
        }
    }
}
