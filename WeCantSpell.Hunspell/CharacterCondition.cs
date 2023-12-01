using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IReadOnlyList<char>, IEquatable<CharacterCondition>
{
    public static readonly CharacterCondition AllowAny = new([], ModeKind.RestrictChars);

    public static bool operator ==(CharacterCondition left, CharacterCondition right) => left.Equals(right);

    public static bool operator !=(CharacterCondition left, CharacterCondition right) => !(left == right);

    public static CharacterCondition CreateCharSet(ReadOnlySpan<char> chars, bool restricted)
    {
        var builder = ArrayBuilderPool<char>.Get(chars.Length);

        foreach (var c in chars)
        {
            builder.AddAsSortedSet(c);
        }

        return new(ArrayBuilderPool<char>.ExtractAndReturn(builder), restricted ? ModeKind.RestrictChars : ModeKind.PermitChars);
    }

    public static CharacterCondition CreateSequence(char c) => new([c], ModeKind.MatchSequence);

    public static CharacterCondition CreateSequence(ReadOnlySpan<char> chars) => new(chars.ToArray(), ModeKind.MatchSequence);

    private CharacterCondition(char[] characters, ModeKind mode)
    {
        _characters = characters;
        Mode = mode;
    }

    public char[]? _characters { get; }

    public IReadOnlyList<char> Characters => GetInternalArray();

    public ModeKind Mode { get; }

    public int Count => (_characters?.Length).GetValueOrDefault();

    public bool IsEmpty => !HasItems;

    public bool HasItems => _characters is { Length: > 0 };

    public char this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            return _characters![index];
        }
    }

    public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Contains(char c)
    {
        if (!HasItems)
        {
            return false;
        }

        if (_characters!.Length <= 8 || Mode == ModeKind.MatchSequence)
        {
            return _characters.Contains(c);
        }

        return Array.BinarySearch(_characters, c) >= 0;
    }

    public bool MatchesAnySingleCharacter => Mode == ModeKind.RestrictChars && IsEmpty;

    public string GetEncoded()
    {
        if (IsEmpty && Mode == ModeKind.RestrictChars)
        {
            return ".";
        }

        var stringValue = GetInternalArray().AsSpan().ToString();

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

    public bool Equals(CharacterCondition other) =>
        Mode == other.Mode
        && GetInternalArray().SequenceEqual(other.GetInternalArray());

    public override bool Equals(object? obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() => HashCode.Combine((_characters?.Length).GetValueOrDefault(), Mode);

    internal char[] GetInternalArray() => _characters ?? [];

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
                if (HasItems && text.StartsWith(_characters.AsSpan()))
                {
                    matchLength = _characters!.Length;
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
                if (HasItems && text.EndsWith(_characters.AsSpan()))
                {
                    matchLength = _characters!.Length;
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
                return HasItems && _characters!.Length == 1 && text.StartsWith(_characters[0]);
            case ModeKind.MatchSequence:
                if (HasItems && text.Length >= _characters!.Length && text.StartsWith(_characters.AsSpan()))
                {
                    matchLength = _characters.Length;
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
