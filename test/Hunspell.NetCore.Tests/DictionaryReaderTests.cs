using FluentAssertions;
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
        }
    }
}
