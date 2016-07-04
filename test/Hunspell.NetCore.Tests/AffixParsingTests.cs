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
    }
}
