using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IEquatable<CharacterCondition>
{
    public static readonly CharacterCondition AllowAny = new(CharacterSet.Empty, true);

    public static bool operator ==(CharacterCondition left, CharacterCondition right) => left.Equals(right);

    public static bool operator !=(CharacterCondition left, CharacterCondition right) => !(left == right);

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
