using FluentAssertions;
using Xunit;

namespace WeCantSpell.Hunspell.Tests
{
    public class CharacterConditionTests
    {
        public class Parse : CharacterConditionTests
        {
            [Fact]
            public void empty_string_produces_no_conditions()
            {
                var text = string.Empty;

                var actual = CharacterCondition.Parse(text);

                actual.Should().BeEmpty();
            }

            [Fact]
            public void invalid_condition_creates_empty_result()
            {
                var text = "[x";

                var actual = CharacterCondition.Parse(text);

                actual.Should().BeEmpty();
            }

            [Fact]
            public void single_letter_creates_single_allwed_char()
            {
                var text = "q";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo(new[] { 'q' });
                actual[0].Restricted.Should().BeFalse();
            }

            [Fact]
            public void single_dot_allows_anything()
            {
                var text = ".";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEmpty();
                actual[0].Restricted.Should().BeTrue();
            }

            [Fact]
            public void empty_brackets_creates_disallowed_condition()
            {
                var text = "[]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEmpty();
                actual[0].Restricted.Should().BeFalse();
            }

            [Fact]
            public void single_letter_in_brackets_creates_single_char()
            {
                var text = "[b]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo(new[] { 'b' });
                actual[0].Restricted.Should().BeFalse();
            }

            [Fact]
            public void multiple_letter_in_brackets_creates_allowed_multiple()
            {
                var text = "[qwerty]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
                actual[0].Restricted.Should().BeFalse();
            }

            [Fact]
            public void empty_negated_range_allows_any()
            {
                var text = "[^]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEmpty();
                actual[0].Restricted.Should().BeTrue();
            }

            [Fact]
            public void single_in_negated_range_allows_all_but_one()
            {
                var text = "[^t]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo(new[] { 't' });
                actual[0].Restricted.Should().BeTrue();
            }

            [Fact]
            public void multiple_in_negated_range_allows_all_but_specified()
            {
                var text = "[^qwerty]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
                actual[0].Restricted.Should().BeTrue();
            }

            [Fact]
            public void double_caret_negated_range_allows_all_but_caret()
            {
                var text = "[^^]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(1);
                actual[0].Characters.Should().BeEquivalentTo(new[] { '^' });
                actual[0].Restricted.Should().BeTrue();
            }

            [Fact]
            public void two_character_ranges_makes_two_conditions()
            {
                var text = "[qwerty][asdf]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(2);
                actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
                actual[0].Restricted.Should().BeFalse();
                actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
                actual[1].Restricted.Should().BeFalse();
            }

            [Fact]
            public void two_negated_ranges_makes_two__restricted_conditions()
            {
                var text = "[^qwerty][^asdf]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(2);
                actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
                actual[0].Restricted.Should().BeTrue();
                actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
                actual[1].Restricted.Should().BeTrue();
            }

            [Fact]
            public void two_mixed_ranges_makes_two__restricted_conditions()
            {
                var text = "[qwerty][^asdf]";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(2);
                actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
                actual[0].Restricted.Should().BeFalse();
                actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
                actual[1].Restricted.Should().BeTrue();
            }

            [Fact]
            public void documentation_example_creates_correct_conditions()
            {
                var text = "[^aeiou]y";

                var actual = CharacterCondition.Parse(text);

                actual.Should().HaveCount(2);
                actual[0].Characters.Should().BeEquivalentTo("aeiou".ToCharArray());
                actual[0].Restricted.Should().BeTrue();
                actual[1].Characters.Should().BeEquivalentTo(new[] { 'y' });
                actual[1].Restricted.Should().BeFalse();
            }
        }

        public class IsMatch : CharacterConditionTests
        {
            [Fact]
            public void default_allows_nothing()
            {
                var condition = new CharacterCondition();

                var actual = condition.IsMatch('h');

                actual.Should().BeFalse();
            }

            [Theory]
            [InlineData('w')]
            [InlineData('.')]
            [InlineData('[')]
            [InlineData(']')]
            [InlineData('^')]
            [InlineData(' ')]
            [InlineData('\0')]
            [InlineData('오')]
            public void dot_creates_permissive_condition(char character)
            {
                var condition = CharacterCondition.AllowAny;

                var actual = condition.IsMatch(character);

                actual.Should().BeTrue();
            }

            [Theory]
            [InlineData('h', 'x', false)]
            [InlineData('g', 'g', true)]
            [InlineData('오', '오', true)]
            [InlineData('오', 'ي', false)]
            public void single_character_allows_exactly_that_character(char allowedLetter, char givenLetter, bool expected)
            {
                var condition = CharacterCondition.Create(allowedLetter, false);

                var actual = condition.IsMatch(givenLetter);

                actual.Should().Be(expected);
            }

            [Theory]
            [InlineData(new char[0], 'x', false)]
            [InlineData(new[] { 'a' }, 'x', false)]
            [InlineData(new[] { 'a', 'b' }, 'x', false)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'a', false)]
            [InlineData(new[] { 'x' }, 'x', true)]
            [InlineData(new[] { 'x', 'y' }, 'x', true)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'x', true)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'y', true)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'z', true)]
            public void range_allows_only_specified_characters(char[] range, char givenLetter, bool expected)
            {
                var condition = CharacterCondition.Create(range, false);

                var actual = condition.IsMatch(givenLetter);

                actual.Should().Be(expected);
            }

            [Theory]
            [InlineData(new char[0], 'x', true)]
            [InlineData(new[] { 'a' }, 'x', true)]
            [InlineData(new[] { 'a', 'b' }, 'x', true)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'a', true)]
            [InlineData(new[] { 'x' }, 'x', false)]
            [InlineData(new[] { 'x', 'y' }, 'x', false)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'x', false)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'y', false)]
            [InlineData(new[] { 'x', 'y', 'z' }, 'z', false)]
            public void range_resricts_specified_characters(char[] range, char givenLetter, bool expected)
            {
                var condition = CharacterCondition.Create(range, true);

                var actual = condition.IsMatch(givenLetter);

                actual.Should().Be(expected);
            }
        }
    }
}
