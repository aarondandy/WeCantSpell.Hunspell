using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using System;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Tests
{
    public class AffixFileTests
    {
        public class Read
        {
            [Fact]
            public async Task can_read_sug_aff()
            {
                AffixFile actual;
                using (var reader = new AffixUtfStreamLineReader(@"files/sug.aff"))
                {
                    actual = await AffixFile.ReadAsync(reader);
                }

                actual.MaxNgramSuggestions.Should().Be(0);
                actual.Replacements.Should().NotBeNull();
                var entry = actual.Replacements.Should().HaveCount(1).And.Subject.Single();
                entry.Pattern.Should().Be("alot");
                entry.Med.Should().Be("a lot");
                actual.KeyString.Should().Be("qwertzuiop|asdfghjkl|yxcvbnm|aq");
                actual.WordChars.Should().Be(".");
                actual.ForbiddenWord.Should().Be((ushort)'?');
            }

            [Fact]
            public async Task can_read_1706659_aff()
            {
                AffixFile actual;
                using (var reader = new AffixUtfStreamLineReader(@"files/1706659.aff"))
                {
                    actual = await AffixFile.ReadAsync(reader);
                }

                actual.RequestedEncoding.Should().Be("ISO8859-1");
                actual.TryString.Should().Be("esijanrtolcdugmphbyfvkwqxz");

                /*actual.SStart
                    .Where(a => a != null)
                    .Select(a => a.Affix)
                    .ShouldBeEquivalentTo(new[]
                    {
                    "e",
                    "er",
                    "en",
                    "em",
                    "es"
                    });*/

                //actual.DefCpdTable.Should().NotBeNull();
                //actual.DefCpdTable.Should().HaveCount(1);
                //actual.DefCpdTable[0].ShouldBeEquivalentTo(new ushort[] { 'v', 'w' });

                throw new NotImplementedException();
            }
        }
    }
}
