﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IReadOnlyList<char>, IEquatable<CharacterCondition>
{
    public static readonly CharacterCondition AllowAny = new(string.Empty, ModeKind.RestrictChars);

    public static bool operator ==(CharacterCondition left, CharacterCondition right) => left.Equals(right);

    public static bool operator !=(CharacterCondition left, CharacterCondition right) => !left.Equals(right);

    public static CharacterCondition CreateCharSet(ReadOnlySpan<char> chars, bool restricted) =>
        CreateCharSet(chars, restricted ? ModeKind.RestrictChars : ModeKind.PermitChars);

    public static CharacterCondition CreateCharSet(ReadOnlySpan<char> chars, ModeKind mode)
    {
        return new(buildCharString(chars), mode);

        static string buildCharString(ReadOnlySpan<char> chars)
        {
            if (chars.Length <= 4)
            {
                Span<char> buffer = (stackalloc char[4]).Slice(0, chars.Length);
                chars.CopyTo(buffer);
                buffer.Sort();
                MemoryEx.RemoveAdjacentDuplicates(ref buffer);
                return buffer.ToString();
            }

            return buildCharSetString(chars);
        }

        static string buildCharSetString(ReadOnlySpan<char> chars)
        {
            var builder = new StringBuilderSpan(chars);
            builder.Sort();
            builder.RemoveAdjacentDuplicates();
            return builder.GetStringAndDispose();
        }
    }

    public static CharacterCondition CreateCharSet(string chars, bool restricted) =>
        CreateCharSet(chars, restricted ? ModeKind.RestrictChars : ModeKind.PermitChars);

    public static CharacterCondition CreateCharSet(string chars, ModeKind mode)
    {
        var charsSpan = chars.AsSpan();
        return charsSpan.CheckSortedWithoutDuplicates()
            ? new(chars, mode)
            : CreateCharSet(charsSpan, mode);
    }

    public static CharacterCondition CreateSequence(char c) => CreateSequence(c.ToString());

    public static CharacterCondition CreateSequence(ReadOnlySpan<char> chars) => CreateSequence(chars.ToString());

    public static CharacterCondition CreateSequence(string chars) => new(chars, ModeKind.MatchSequence);

    private CharacterCondition(string characters, ModeKind mode)
    {
        _characters = characters;
        _mode = mode;
    }

    private readonly string? _characters;
    private readonly ModeKind _mode;

    public IReadOnlyList<char> Characters => _characters is not null ? _characters.ToCharArray() : [];

    public ModeKind Mode => _mode;

    public int Count => _characters is not null ? _characters.Length : 0;

    public bool IsEmpty => _characters is not { Length: > 0 };

    public bool HasItems => _characters is { Length: > 0 };

    public char this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif

            if (_characters is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _characters![index];
        }
    }

    public IEnumerator<char> GetEnumerator() => GetValuesAsText().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Contains(char c) =>
        _characters is not null
        &&
        _characters.Length switch
        {
            0 => false,
            1 => _characters[0] == c,
            2 => _characters[0] == c || _characters[1] == c,
            3 => _characters[0] == c || _characters[1] == c || _characters[2] == c,
            > 8 when _mode is not ModeKind.MatchSequence => MemoryEx.SortedLargeSearchSpaceContains(_characters.AsSpan(), c),
            _ => _characters.Contains(c),
        };

    public bool MatchesAnySingleCharacter => _mode == ModeKind.RestrictChars && IsEmpty;

    public string GetEncoded()
    {
        if (IsEmpty && _mode == ModeKind.RestrictChars)
        {
            return ".";
        }

        var stringValue = GetValuesAsText();

        return _mode switch
        {
            ModeKind.RestrictChars => "[^" + stringValue + "]",
            ModeKind.PermitChars => "[" + stringValue + "]",
            _ => stringValue,
        };
    }

    public override string ToString() => GetEncoded();

    public bool Equals(CharacterCondition other) =>
        other._mode == _mode
        && other.GetValuesAsText().Equals(GetValuesAsText(), StringComparison.Ordinal);

    public override bool Equals(object? obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() => HashCode.Combine(StringEx.GetStableOrdinalHashCode(GetValuesAsText()), _mode);

    internal string GetValuesAsText() => _characters ?? string.Empty;

    internal int FullyMatchesFromStart(ReadOnlySpan<char> text)
    {
        if (_mode is ModeKind.MatchSequence)
        {
            if (_characters is not null && text.StartsWith(_characters.AsSpan()))
            {
                return _characters.Length;
            }
        }
        else if (text.Length > 0 && Contains(text[0]) == (_mode is ModeKind.PermitChars))
        {
            return 1;
        }

        return 0;
    }

    internal int FullyMatchesFromEnd(ReadOnlySpan<char> text)
    {
        if (_mode is ModeKind.MatchSequence)
        {
            if (_characters is not null && text.EndsWith(_characters.AsSpan()))
            {
                return _characters.Length;
            }
        }
        else if (text.Length > 0 && Contains(text[text.Length - 1]) == (_mode is ModeKind.PermitChars))
        {
            return 1;
        }

        return 0;
    }

    internal bool IsOnlyPossibleMatch(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.Length > 0)
        {
            switch (_mode)
            {
                // Technically, it could be the only possible match if the set was of size
                // 65536 - 1 as it would only leave space for one specific character. That
                // isn't really feasible or practical so just failing for a reistrct case
                // should work well enough.
                // case ModeKind.RestrictChars: return false;

                case ModeKind.PermitChars:
                    return _characters is { Length: 1 } && _characters[0] == text[0];

                case ModeKind.MatchSequence:
                    if (_characters is { Length: > 0 } && text.Length >= _characters.Length && text.StartsWith(_characters.AsSpan()))
                    {
                        matchLength = _characters.Length;
                        return true;
                    }

                    break;
            }
        }

        return false;
    }

    public enum ModeKind : byte
    {
        PermitChars = 0,
        RestrictChars = 1,
        MatchSequence = 2,
    }
}
