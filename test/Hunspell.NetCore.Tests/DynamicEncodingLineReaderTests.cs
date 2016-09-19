using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class DynamicEncodingLineReaderTests
    {
        [Fact]
        public void can_read_lines_with_mixed_line_endings()
        {
            var data = "ABC\r\nDEF\n\rGHI\rJKL\nMNO"
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

            List<string> actual;
            using (var readStream = new MemoryStream(data))
            using (var reader = new DynamicEncodingLineReader(readStream, Encoding.UTF8))
            {
                actual = reader.ReadLines()
                    .Where(line => !string.IsNullOrEmpty(line))
                    .ToList();
            }

            actual.ShouldAllBeEquivalentTo(expected);
        }
    }
}
