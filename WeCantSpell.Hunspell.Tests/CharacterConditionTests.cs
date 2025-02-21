using System;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class CharacterConditionTests
{
    public class ParseGroup : CharacterConditionTests
    {
        [Fact]
        public void empty_string_produces_no_conditions()
        {
            var actual = CharacterConditionGroup.Parse("");

            actual.ShouldBeEmpty();
        }

        [Fact]
        public void open_condition_group_creates_empty_group()
        {
            var actual = CharacterConditionGroup.Parse("[");

            actual[0].Characters.ShouldBeEmpty();
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void invalid_condition_creates_best_effort_parse_result()
        {
            var actual = CharacterConditionGroup.Parse("[x");

            actual[0].Characters.ShouldBe("x");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void single_letter_creates_single_allwed_char()
        {
            var actual = CharacterConditionGroup.Parse("q");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("q");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.MatchSequence);
        }

        [Fact]
        public void single_dot_allows_anything()
        {
            var actual = CharacterConditionGroup.Parse(".");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBeEmpty();
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void empty_brackets_creates_disallowed_condition()
        {
            var actual = CharacterConditionGroup.Parse("[]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBeEmpty();
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void single_letter_in_brackets_creates_single_char()
        {
            var actual = CharacterConditionGroup.Parse("[b]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("b");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void multiple_letter_in_brackets_creates_allowed_multiple()
        {
            var actual = CharacterConditionGroup.Parse("[qwerty]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("qwerty", ignoreOrder: true);
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void empty_negated_range_allows_any()
        {
            var actual = CharacterConditionGroup.Parse("[^]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBeEmpty();
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void single_in_negated_range_allows_all_but_one()
        {
            var actual = CharacterConditionGroup.Parse("[^t]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("t");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void multiple_in_negated_range_allows_all_but_specified()
        {
            var actual = CharacterConditionGroup.Parse("[^qwerty]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("qwerty", ignoreOrder: true);
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void double_caret_negated_range_allows_all_but_caret()
        {
            var actual = CharacterConditionGroup.Parse("[^^]");

            actual.ShouldHaveSingleItem();
            actual[0].Characters.ShouldBe("^");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void two_character_ranges_makes_two_conditions()
        {
            var actual = CharacterConditionGroup.Parse("[qwerty][asdf]");

            actual.Count.ShouldBe(2);
            actual[0].Characters.ShouldBe("qwerty", ignoreOrder: true);
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
            actual[1].Characters.ShouldBe("asdf", ignoreOrder: true);
            actual[1].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
        }

        [Fact]
        public void two_negated_ranges_makes_two__restricted_conditions()
        {
            var actual = CharacterConditionGroup.Parse("[^qwerty][^asdf]");

            actual.Count.ShouldBe(2);
            actual[0].Characters.ShouldBe("qwerty", ignoreOrder: true);
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
            actual[1].Characters.ShouldBe("asdf", ignoreOrder: true);
            actual[1].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void two_mixed_ranges_makes_two__restricted_conditions()
        {
            var actual = CharacterConditionGroup.Parse("[qwerty][^asdf]");

            actual.Count.ShouldBe(2);
            actual[0].Characters.ShouldBe("qwerty", ignoreOrder: true);
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.PermitChars);
            actual[1].Characters.ShouldBe("asdf", ignoreOrder: true);
            actual[1].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
        }

        [Fact]
        public void documentation_example_creates_correct_conditions()
        {
            var actual = CharacterConditionGroup.Parse("[^aeiou]y");

            actual.Count.ShouldBe(2);
            actual[0].Characters.ShouldBe("aeiou");
            actual[0].Mode.ShouldBe(CharacterCondition.ModeKind.RestrictChars);
            actual[1].Characters.ShouldBe("y");
            actual[1].Mode.ShouldBe(CharacterCondition.ModeKind.MatchSequence);
        }
    }

    public class IsMatch : CharacterConditionTests
    {
        [Fact]
        public void default_allows_nothing()
        {
            CharacterConditionGroup condition = default;

            var actual = condition.IsStartingMatch("h".AsSpan());

            actual.ShouldBeFalse();
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

            actual.ShouldBeTrue();
        }

        [Theory]
        [InlineData('h', 'x', false)]
        [InlineData('g', 'g', true)]
        [InlineData('오', '오', true)]
        [InlineData('오', 'ي', false)]
        public void single_character_allows_exactly_that_character(char allowedLetter, char givenLetter, bool expected)
        {
            var condition = CharacterConditionGroup.Create(CharacterCondition.CreateSequence(allowedLetter));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.ShouldBe(expected);
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
            var condition = CharacterConditionGroup.Create(CharacterCondition.CreateCharSet(range.AsSpan(), false));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.ShouldBe(expected);
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
            var condition = CharacterConditionGroup.Create(CharacterCondition.CreateCharSet(range.AsSpan(), true));

            var actual = condition.IsStartingMatch(givenLetter.ToString().AsSpan());

            actual.ShouldBe(expected);
        }
    }
}
