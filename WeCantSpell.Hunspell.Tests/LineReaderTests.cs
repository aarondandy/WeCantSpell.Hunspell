using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class LineReaderTests
{
    public class ReadLinesTests
    {
        [Fact]
        public void can_read_lines_with_mixed_line_endings()
        {
            var data = "ABC\r\nDEF\r\nGHI\nJKL\nMNO"
                .ToCharArray()
                .Select(c => (byte)c)
                .ToArray();
            var expected = new List<string>
            {
                "ABC",
                "DEF",
                "GHI",
                "JKL",
                "MNO"
            };

            using var readStream = new MemoryStream(data);
            using var reader = new LineReader(readStream, Encoding.UTF8);

            var actual = new List<string>();
            while (reader.ReadNext())
            {
                actual.Add(reader.Current.ToString());
            }

            actual.Should().BeEquivalentTo(expected);
        }
    }

    public class ReadLinesAsyncTests
    {
        [Fact]
        public async Task can_read_lines_with_mixed_line_endings()
        {
            var data = "ABC\r\nDEF\r\nGHI\nJKL\nMNO"
                .ToCharArray()
                .Select(c => (byte)c)
                .ToArray();
            var expected = new List<string>
            {
                "ABC",
                "DEF",
                "GHI",
                "JKL",
                "MNO"
            };

            using var readStream = new MemoryStream(data);
            using var reader = new LineReader(readStream, Encoding.UTF8);

            var actual = new List<string>();
            while (await reader.ReadNextAsync(CancellationToken.None))
            {
                actual.Add(reader.Current.ToString());
            }

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
