using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterCondition : IReadOnlyList<char>, IEquatable<CharacterCondition>
{
    public static readonly CharacterCondition AllowAny = new(string.Empty, ModeKind.RestrictChars);

    public static bool operator ==(CharacterCondition left, CharacterCondition right) => left.Equals(right);

    public static bool operator !=(CharacterCondition left, CharacterCondition right) => !left.Equals(right);

    public static CharacterCondition CreateCharSet(ReadOnlySpan<char> chars, bool restricted)
    {
        var builder = new StringBuilderSpan(chars);
        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose(), restricted ? ModeKind.RestrictChars : ModeKind.PermitChars);
    }

    public static CharacterCondition CreateCharSet(string chars, bool restricted)
    {
        var charsSpan = chars.AsSpan();
        return charsSpan.CheckSortedWithoutDuplicates()
            ? new(chars, restricted ? ModeKind.RestrictChars : ModeKind.PermitChars)
            : CreateCharSet(charsSpan, restricted);
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
                ExceptionEx.ThrowInvalidOperation("Condition is not initialized");
            }

            return _characters![index];
        }
    }

    public IEnumerator<char> GetEnumerator() => GetValuesAsText().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Contains(char c) =>
        _characters is not null
        &&
        (
            (_mode is ModeKind.MatchSequence || _characters.Length <= 8)
            ? _characters.Contains(c)
            : MemoryEx.SortedContains(_characters.AsSpan(), c)
        );

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
            ModeKind.MatchSequence => stringValue,
            ModeKind.RestrictChars => "[^" + stringValue + "]",
            ModeKind.PermitChars => "[" + stringValue + "]",
            _ => ThrowUnsupportedMode(_mode),
        };
    }

    public override string ToString() => GetEncoded();

    public bool Equals(CharacterCondition other) =>
        other._mode == _mode
        && other.GetValuesAsText().SequenceEqual(GetValuesAsText());

    public override bool Equals(object? obj) => obj is CharacterCondition cc && Equals(cc);

    public override int GetHashCode() => HashCode.Combine(StringEx.GetStableOrdinalHashCode(GetValuesAsText()), _mode);

    internal string GetValuesAsText() => _characters ?? string.Empty;

    internal bool FullyMatchesFromStart(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.Length > 0)
        {
            switch (_mode)
            {
                case ModeKind.PermitChars:
                    return Contains(text[0]);

                case ModeKind.RestrictChars:
                    return !Contains(text[0]);

                case ModeKind.MatchSequence:
                    if (_characters is { Length: > 0 } && text.StartsWith(_characters.AsSpan()))
                    {
                        matchLength = _characters.Length;
                        return true;
                    }

                    break;
            }
        }

        return false;
    }

    internal bool FullyMatchesFromEnd(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.Length > 0)
        {
            switch (_mode)
            {
                case ModeKind.PermitChars:
                    return Contains(text[text.Length - 1]);

                case ModeKind.RestrictChars:
                    return !Contains(text[text.Length - 1]);

                case ModeKind.MatchSequence:
                    if (_characters is { Length: > 0 } && text.EndsWith(_characters.AsSpan()))
                    {
                        matchLength = _characters.Length;
                        return true;
                    }

                    break;
            }
        }

        return false;
    }

    internal bool IsOnlyPossibleMatch(ReadOnlySpan<char> text, out int matchLength)
    {
        matchLength = 1;

        if (text.Length > 0)
        {
            switch (_mode)
            {
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

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    [MethodImpl(MethodImplOptions.NoInlining)]
    static string ThrowUnsupportedMode(ModeKind mode) => throw new NotSupportedException("Character condition mode " + mode.ToString() + " not supported");

    public enum ModeKind : byte
    {
        PermitChars = 0,
        RestrictChars = 1,
        MatchSequence = 2,
    }
}
