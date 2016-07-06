using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class AffixParsingTests
    {
        [Fact]
        public void can_parse_sug_aff()
        {
            var ptr = new List<HashMgr>
            {
                new HashMgr()
            };

            var aff = new AffixMgr("files/sug.aff", ptr);

            Assert.Equal(0, aff.MaxNgramSugs);
            Assert.NotNull(aff.RepTable);
            Assert.Equal(1, aff.RepTable.Count);
            Assert.Equal("alot", aff.RepTable[0].Pattern);
            Assert.Equal("a lot", aff.RepTable[0].Med);
            Assert.Equal("qwertzuiop|asdfghjkl|yxcvbnm|aq", aff.KeyString);
            Assert.Equal(".", aff.WordChars);
            Assert.Equal(".", aff.WordCharsUtf16);
            Assert.Equal((ushort)'?', aff.ForbiddenWord);
        }

        [Fact]
        public void can_parse_1706659_aff()
        {
            var ptr = new List<HashMgr>
            {
                new HashMgr()
            };

            var aff = new AffixMgr("files/1706659.aff", ptr);

            aff.Encoding.Should().Be("ISO8859-1");
            aff.TryString.Should().Be("esijanrtolcdugmphbyfvkwqxz");

            aff.SStart
                .Where(a => a != null)
                .Select(a => a.Affix)
                .ShouldBeEquivalentTo(new[]
                {
                    "e",
                    "er",
                    "en",
                    "em",
                    "es"
                });

            aff.DefCpdTable.Should().NotBeNull();
            aff.DefCpdTable.Should().HaveCount(1);
            aff.DefCpdTable[0].ShouldBeEquivalentTo(new ushort[] { 'v', 'w' });
        }
    }
}
