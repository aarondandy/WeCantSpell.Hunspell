using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IEquatable<CharacterCondition>
{
    private static Regex ConditionParsingRegex = new Regex(
        @"^(\[[^\]]*\]|\.|[^\[\]\.])*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static readonly CharacterCondition AllowAny = new(CharacterSet.Empty, true);

    public static CharacterConditionGroup Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return CharacterConditionGroup.Empty;
        }

        var match = ConditionParsingRegex.Match(text);
        if (!match.Success || match.Groups.Count < 2)
        {
            return CharacterConditionGroup.Empty;
        }

        var captures = match.Groups[1].Captures;
        var conditions = ImmutableArray.CreateBuilder<CharacterCondition>(captures.Count);
        foreach (Capture capture in captures)
        {
            conditions.Add(ParseSingle(capture.Value.AsSpan()));
        }

        return new(conditions.ToImmutable(true));
    }

    private static CharacterCondition ParseSingle(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty || text.Length == 0)
        {
            return AllowAny;
        }
        if (text.Length == 1)
        {
            var singleChar = text[0];
            if (singleChar == '.')
            {
                return AllowAny;
            }

            return new(CharacterSet.Create(singleChar), false);
        }

        if (!text.StartsWith('[') || !text.EndsWith(']'))
        {
            throw new InvalidOperationException();
        }

        var restricted = text[1] == '^';
        text = restricted ? text.Slice(2, text.Length - 3) : text.Slice(1, text.Length - 2);
        return new(CharacterSet.Create(text), restricted);
    }

    public CharacterCondition(CharacterSet characters, bool restricted)
    {
        Characters = characters;
        Restricted = restricted;
    }

    public CharacterSet Characters { get; }

    /// <summary>
    /// Indicates that the <see cref="Characters"/> are restricted when <c>true</c>.
    /// </summary>
    public bool Restricted { get; }

    public bool IsMatch(char c) => Characters.Contains(c) ^ Restricted;

    public bool AllowsAny => Restricted && Characters is not { Count: > 0 };

    public bool PermitsSingleCharacter => !Restricted && Characters is { Count: 1 };

    public string GetEncoded()
    {
        if (AllowsAny)
        {
            return ".";
        }

        if (PermitsSingleCharacter)
        {
            return Characters[0].ToString();
        }

        var result = Restricted ? "[^" : "[";
        if (Characters is { Count: > 0 })
        {
            result += Characters.GetCharactersAsString() + "]";
        }
        else
        {
            result += "]";
        }

        return result;
    }

    public override string ToString() => GetEncoded();

    public bool Equals(CharacterCondition other) =>
        Restricted == other.Restricted
        && CharacterSet.Comparer.Instance.Equals(Characters, other.Characters);

    public override bool Equals(object? obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() => HashCode.Combine(CharacterSet.Comparer.Instance.GetHashCode(Characters), Restricted);
}
