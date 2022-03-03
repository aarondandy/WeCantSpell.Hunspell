using System;

using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class CharacterConditionTests
{
    public class ParseGroup : CharacterConditionTests
    {
        [Fact]
        public void empty_string_produces_no_conditions()
        {
            var text = string.Empty;

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().BeEmpty();
        }

        [Fact]
        public void open_condition_group_creates_empty_group()
        {
            var text = "[";

            var actual = CharacterConditionGroup.Parse(text);

            actual[0].Characters.Should().BeEmpty();
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void invalid_condition_creates_best_effort_parse_result()
        {
            var text = "[x";

            var actual = CharacterConditionGroup.Parse(text);

            actual[0].Characters.Should().BeEquivalentTo(new[] { 'x' });
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void single_letter_creates_single_allwed_char()
        {
            var text = "q";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo(new[] { 'q' });
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.MatchSequence);
        }

        [Fact]
        public void single_dot_allows_anything()
        {
            var text = ".";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEmpty();
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void empty_brackets_creates_disallowed_condition()
        {
            var text = "[]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEmpty();
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void single_letter_in_brackets_creates_single_char()
        {
            var text = "[b]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo(new[] { 'b' });
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void multiple_letter_in_brackets_creates_allowed_multiple()
        {
            var text = "[qwerty]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void empty_negated_range_allows_any()
        {
            var text = "[^]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEmpty();
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void single_in_negated_range_allows_all_but_one()
        {
            var text = "[^t]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo(new[] { 't' });
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void multiple_in_negated_range_allows_all_but_specified()
        {
            var text = "[^qwerty]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void double_caret_negated_range_allows_all_but_caret()
        {
            var text = "[^^]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(1);
            actual[0].Characters.Should().BeEquivalentTo(new[] { '^' });
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void two_character_ranges_makes_two_conditions()
        {
            var text = "[qwerty][asdf]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(2);
            actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
            actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
            actual[1].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void two_negated_ranges_makes_two__restricted_conditions()
        {
            var text = "[^qwerty][^asdf]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(2);
            actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
            actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
            actual[1].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void two_mixed_ranges_makes_two__restricted_conditions()
        {
            var text = "[qwerty][^asdf]";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(2);
            actual[0].Characters.Should().BeEquivalentTo("qwerty".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.PermitChars);
            actual[1].Characters.Should().BeEquivalentTo("asdf".ToCharArray());
            actual[1].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void documentation_example_creates_correct_conditions()
        {
            var text = "[^aeiou]y";

            var actual = CharacterConditionGroup.Parse(text);

            actual.Should().HaveCount(2);
            actual[0].Characters.Should().BeEquivalentTo("aeiou".ToCharArray());
            actual[0].Mode.Should().Be(CharacterCondition.ModeKind.RestrictChars);
            actual[1].Characters.Should().BeEquivalentTo(new[] { 'y' });
            actual[1].Mode.Should().Be(CharacterCondition.ModeKind.MatchSequence);
        }
    }

    public class IsMatch : CharacterConditionTests
    {
        [Fact]
        public void default_allows_nothing()
        {
            var condition = default(CharacterConditionGroup);

            var actual = condition.IsStartingMatch("h".AsSpan());

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
            var condition = CharacterConditionGroup.AllowAnySingleCharacter;

            var actual = condition.IsStartingMatch(character.ToString().AsSpan());

            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData('h', 'x', false)]
        [InlineData('g', 'g', true)]
        [InlineData('오', '오', true)]
        [InlineData('오', 'ي', false)]
        public void single_character_allows_exactly_that_character(char allowedLetter, char givenLetter, bool expected)
        {
            var condition = new CharacterConditionGroup(CharacterCondition.CreateSequence(allowedLetter));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("", 'x', false)]
        [InlineData("a", 'x', false)]
        [InlineData("ab", 'x', false)]
        [InlineData("xyz", 'a', false)]
        [InlineData("x", 'x', true)]
        [InlineData("xy", 'x', true)]
        [InlineData("xyz", 'x', true)]
        [InlineData("xyz", 'y', true)]
        [InlineData("xyz", 'z', true)]
        public void range_allows_only_specified_characters(string range, char givenLetter, bool expected)
        {
            var condition = new CharacterConditionGroup(CharacterCondition.CreateCharSet(range.AsSpan(), false));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("", 'x', true)]
        [InlineData("a", 'x', true)]
        [InlineData("ab", 'x', true)]
        [InlineData("xyz", 'a', true)]
        [InlineData("x", 'x', false)]
        [InlineData("xy", 'x', false)]
        [InlineData("xyz", 'x', false)]
        [InlineData("xyz", 'y', false)]
        [InlineData("xyz", 'z', false)]
        public void range_resricts_specified_characters(string range, char givenLetter, bool expected)
        {
            var condition = new CharacterConditionGroup(CharacterCondition.CreateCharSet(range.AsSpan(), true));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.Should().Be(expected);
        }
    }
}
