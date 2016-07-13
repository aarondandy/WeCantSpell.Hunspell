using System.Linq;
using FluentAssertions;
using Xunit;
using System;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Tests
{
    public class AffixFileReaderTests
    {
        public class Constructors
        {
            [Fact]
            public void null_line_reader_throws()
            {
                Action act = () => new AffixFileReader(null);

                act.ShouldThrow<ArgumentNullException>();
            }
        }

        public class ReadAsync
        {
            [Fact]
            public async Task can_read_sug_aff()
            {
                AffixFile actual;

                using (var reader = new AffixFileReader(new AffixUtfStreamLineReader(@"files/sug.aff")))
                {
                    actual = await reader.GetOrReadAsync();
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

                using (var reader = new AffixFileReader(new AffixUtfStreamLineReader(@"files/1706659.aff")))
                {
                    actual = await reader.GetOrReadAsync();
                }

                actual.RequestedEncoding.Should().Be("ISO8859-1");
                actual.TryString.Should().Be("esijanrtolcdugmphbyfvkwqxz");

                actual.Suffixes.Should().HaveCount(1);
                var suffixGroupA = actual.Suffixes.Single();
                suffixGroupA.Options.Should().Be(AffixEntryOptions.CrossProduct);
                suffixGroupA.AFlag.Should().Be('A');
                suffixGroupA.Entries.Should().HaveCount(5);
                var suffixes = suffixGroupA.Entries;
                suffixes.Select(s => s.Append).Should().BeEquivalentTo(new[]
                {
                    "e",
                    "er",
                    "en",
                    "em",
                    "es"
                });

                actual.CompoundRules.Should().NotBeNull();
                actual.CompoundRules.Should().HaveCount(1);
                var rule1 = actual.CompoundRules.Single();
                rule1.ShouldBeEquivalentTo(new[] { 'v', 'w' });
            }

            [Fact]
            public async Task can_read_1975530_aff()
            {
                AffixFile actual;

                using (var reader = new AffixFileReader(new AffixUtfStreamLineReader("files/1975530.aff")))
                {
                    actual = await reader.GetOrReadAsync();
                }

                actual.RequestedEncoding.Should().Be("UTF-8");
                actual.IgnoredChars.Should().Be("ٌٍَُِّْـ");
                actual.Prefixes.Should().HaveCount(1);
                var prefixGroup1 = actual.Prefixes.Single();
                prefixGroup1.AFlag.Should().Be('x');
                prefixGroup1.Options.Should().Be(AffixEntryOptions.None);
                prefixGroup1.Entries.Should().HaveCount(1);
                var prefixEntry = prefixGroup1.Entries.Single();
                prefixEntry.Append.Should().Be("ت");
                prefixEntry.ConditionText.Should().Be("أ[^ي]");
                prefixEntry.Strip.Should().Be("أ");
            }
        }
    }
}
