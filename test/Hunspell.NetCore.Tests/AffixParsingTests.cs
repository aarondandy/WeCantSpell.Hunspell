using FluentAssertions;
using System;
using System.Collections.Generic;
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

            Assert.Equal("ISO8859-1", aff.Encoding);
            Assert.Equal("esijanrtolcdugmphbyfvkwqxz", aff.TryString);

            throw new NotImplementedException();
            // TODO: SFX A Y 5
            // TODO: SFX A 0 e.
            // TODO: SFX A 0 er.
            // TODO: SFX A 0 en.
            // TODO: SFX A 0 em.
            // TODO: SFX A 0 es.

            aff.DefCpdTable.Should().NotBeNull();
            aff.DefCpdTable.Should().HaveCount(1);
            aff.DefCpdTable[0].ShouldBeEquivalentTo(new ushort[] { 'v', 'w' });
        }
    }
}
