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
        }
    }
}
