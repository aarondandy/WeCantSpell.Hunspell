using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IReadOnlyList<char>, IEquatable<CharacterCondition>
{
    public static readonly CharacterCondition AllowAny = new(ImmutableArray<char>.Empty, ModeKind.RestrictChars);

    public static bool operator ==(CharacterCondition left, CharacterCondition right) => left.Equals(right);

    public static bool operator !=(CharacterCondition left, CharacterCondition right) => !(left == right);

    public static CharacterCondition CreateCharSet(ReadOnlySpan<char> chars, bool restricted)
    {
        var builder = ImmutableArray.CreateBuilder<char>(chars.Length);

        foreach (var c in chars)
        {
            builder.Add(c);
        }

        builder.Sort();

        return new(builder.ToImmutable(allowDestructive: true), restricted ? ModeKind.RestrictChars : ModeKind.PermitChars);
    }

    public static CharacterCondition CreateSequence(char c)
    {
        return new(ImmutableArray.Create(c), ModeKind.MatchSequence);
    }

    public static CharacterCondition CreateSequence(ReadOnlySpan<char> chars)
    {
        var builder = ImmutableArray.CreateBuilder<char>(chars.Length);

        foreach (var c in chars)
        {
            builder.Add(c);
        }

        return new(builder.ToImmutable(allowDestructive: true), ModeKind.MatchSequence);
    }

    private CharacterCondition(ImmutableArray<char> characters, ModeKind mode)
    {
        Characters = characters;
        Mode = mode;
    }

    public ImmutableArray<char> Characters { get; }

    public ModeKind Mode { get; }

    public int Count => Characters.Length;

    public bool IsEmpty => Characters.IsDefaultOrEmpty;

    public bool HasItems => !IsEmpty;

    public char this[int index] => Characters[index];

    public ImmutableArray<char>.Enumerator GetEnumerator() => Characters.GetEnumerator();

    IEnumerator<char> IEnumerable<char>.GetEnumerator() => ((IEnumerable<char>)Characters).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Characters).GetEnumerator();

    public bool Contains(char c)
    {
        if (!HasItems)
        {
            return false;
        }

        if (Characters.Length <= 8 || Mode == ModeKind.MatchSequence)
        {
            return Characters.Contains(c);
        }

        return Characters.BinarySearch(c) >= 0;
    }

    public bool MatchesAnySingleCharacter => Mode == ModeKind.RestrictChars && IsEmpty;

    public string GetEncoded()
    {
        if (Characters.Length == 0 && Mode == ModeKind.RestrictChars)
        {
            return ".";
        }

        var stringValue = Characters.AsSpan().ToString();

        return Mode switch
        {
            ModeKind.MatchSequence => stringValue,
            ModeKind.RestrictChars => "[^" + stringValue + "]",
            ModeKind.PermitChars => "[" + stringValue + "]",
            _ => handleUnsupportedMode(),
        };

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static string handleUnsupportedMode()
        {
            throw new NotSupportedException();
        }
    }

    public override string ToString() => GetEncoded();

    public bool Equals(CharacterCondition other) => Mode == other.Mode && Characters.SequenceEqual(other.Characters);

    public override bool Equals(object? obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() => HashCode.Combine(Characters.Length, Mode);

    internal bool FullyMatchesFromStart(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.IsEmpty)
        {
            return false;
        }

        switch (Mode)
        {
            case ModeKind.PermitChars:
                return Contains(text[0]);
            case ModeKind.RestrictChars:
                return !Contains(text[0]);
            case ModeKind.MatchSequence:
                if (HasItems && text.StartsWith(Characters.AsSpan()))
                {
                    matchLength = Characters.Length;
                    return true;
                }

                break;
        }

        return false;
    }

    internal bool FullyMatchesFromEnd(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.IsEmpty)
        {
            return false;
        }

        switch (Mode)
        {
            case ModeKind.PermitChars:
                return Contains(text[text.Length - 1]);
            case ModeKind.RestrictChars:
                return !Contains(text[text.Length - 1]);
            case ModeKind.MatchSequence:
                if (HasItems && text.EndsWith(Characters.AsSpan()))
                {
                    matchLength = Characters.Length;
                    return true;
                }

                break;
        }

        return false;
    }

    internal bool IsOnlyPossibleMatch(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.IsEmpty)
        {
            return false;
        }

        switch (Mode)
        {
            case ModeKind.RestrictChars:
                return false;
            case ModeKind.PermitChars:
                return HasItems && Characters.Length == 1 && text.StartsWith(Characters[0]);
            case ModeKind.MatchSequence:
                if (HasItems && text.Length >= Characters.Length && text.StartsWith(Characters.AsSpan()))
                {
                    matchLength = Characters.Length;
                    return true;
                }

                break;
        }

        return false;
    }

    public enum ModeKind : byte
    {
        PermitChars,
        RestrictChars,
        MatchSequence
    }
}
