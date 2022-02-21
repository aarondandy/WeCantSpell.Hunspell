using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public struct CharacterCondition :
    IEquatable<CharacterCondition>
{
    private static Regex ConditionParsingRegex = new Regex(
        @"^(\[[^\]]*\]|\.|[^\[\]\.])*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static readonly CharacterCondition AllowAny = new CharacterCondition(CharacterSet.Empty, true);

    internal static CharacterCondition TakeArray(char[] characters, bool restricted) =>
        new CharacterCondition(characters, restricted);

    public static CharacterCondition Create(char character, bool restricted) =>
        new CharacterCondition(character, restricted);

    public static CharacterCondition Create(IEnumerable<char> characters, bool restricted) =>
        TakeArray(characters is null ? Array.Empty<char>() : characters.ToArray(), restricted);

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
        var conditions = new CharacterCondition[captures.Count];
        for (var captureIndex = 0; captureIndex < captures.Count; captureIndex++)
        {
            conditions[captureIndex] = ParseSingle(captures[captureIndex].Value.AsSpan());
        }

        return CharacterConditionGroup.TakeArray(conditions);
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

            return Create(singleChar, false);
        }

        if (!text.StartsWith('[') || !text.EndsWith(']'))
        {
            throw new InvalidOperationException();
        }

        var restricted = text[1] == '^';
        text = restricted ? text.Slice(2, text.Length - 3) : text.Slice(1, text.Length - 2);
        return TakeArray(text.ToArray(), restricted);
    }

    public CharacterCondition(CharacterSet characters, bool restricted)
    {
        Characters = characters;
        Restricted = restricted;
    }

    private CharacterCondition(char character, bool restricted)
        : this(CharacterSet.Create(character), restricted)
    {
    }

    private CharacterCondition(char[] characters, bool restricted)
        : this(CharacterSet.TakeArray(characters), restricted)
    {
    }

    public CharacterSet Characters { get; }

    /// <summary>
    /// Indicates that the <see cref="Characters"/> are restricted when <c>true</c>.
    /// </summary>
    public bool Restricted { get; }

    public bool IsMatch(char c) => (Characters is not null && Characters.Contains(c)) ^ Restricted;

    public bool AllowsAny => Restricted && (Characters is null || Characters.Count == 0);

    public bool PermitsSingleCharacter => !Restricted && Characters is not null && Characters.Count == 1;

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

        var lettersText = (Characters is null || Characters.Count == 0)
            ? string.Empty
            : Characters.GetCharactersAsString();

        return (Restricted ? "[^" : "[") + lettersText + "]";
    }

    public override string ToString() => GetEncoded();

    public bool Equals(CharacterCondition other) =>
        Restricted == other.Restricted && CharacterSet.DefaultComparer.Equals(Characters, other.Characters);

    public override bool Equals(object obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() =>
        unchecked((Restricted.GetHashCode() * 149) ^ CharacterSet.DefaultComparer.GetHashCode(Characters));
}
