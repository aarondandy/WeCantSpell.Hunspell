using FluentAssertions;
using Hunspell.Utilities;
using Xunit;

namespace Hunspell.NetCore.Tests.Utilities
{
    public class StringExtensionsTests
    {
        public class EqualsOffset : StringExtensionsTests
        {
            [Fact]
            public void nulls_are_equal()
            {
                string left = null;
                string right = null;

                var actual = StringExtensions.EqualsOffset(left, 0, right, 0);

                actual.Should().BeTrue();
            }

            [Fact]
            public void null_not_equal_empty()
            {
                string left = "";
                string right = null;

                var actual = StringExtensions.EqualsOffset(left, 0, right, 0);

                actual.Should().BeFalse();
            }

            [Fact]
            public void empty_not_equal_null()
            {
                string left = null;
                string right = "";

                var actual = StringExtensions.EqualsOffset(left, 0, right, 0);

                actual.Should().BeFalse();
            }

            [Fact]
            public void same_text_is_equal()
            {
                string left = new string(new[] { 'A', 'b', 'c' });
                string right = new string(new[] { 'A', 'b', 'c' });

                var actual = StringExtensions.EqualsOffset(left, 0, right, 0);

                actual.Should().BeTrue();
            }

            [Fact]
            public void same_instance_is_equal()
            {
                string left = "Def";
                string right = left;

                var actual = StringExtensions.EqualsOffset(left, 0, right, 0);

                actual.Should().BeTrue();
            }

            [Fact]
            public void same_instance_different_offset_not_equal()
            {
                string left = "Def";
                string right = left;

                var actual = StringExtensions.EqualsOffset(left, 1, right, 0);

                actual.Should().BeFalse();
            }

            [Fact]
            public void diff_strings_of_same_length_but_different_offset_not_equal()
            {
                string left = "abcdefg";
                string right = "hijklmn";

                var actual = StringExtensions.EqualsOffset(left, 1, right, 0);

                actual.Should().BeFalse();
            }

            [Fact]
            public void can_match_ending_of_larger_string()
            {
                string left = "abcdefg";
                string right = "efg";

                var actual = StringExtensions.EqualsOffset(left, 4, right, 0);

                actual.Should().BeTrue();
            }

            [Fact]
            public void can_match_ending_of_larger_string_reversed_arguments()
            {
                string left = "efg";
                string right = "abcdefg";

                var actual = StringExtensions.EqualsOffset(left, 0, right, 4);

                actual.Should().BeTrue();
            }
        }
    }
}
