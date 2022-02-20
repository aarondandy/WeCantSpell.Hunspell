﻿using System;
using System.Collections.Generic;
using System.Globalization;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public readonly struct FlagValue :
    IEquatable<FlagValue>,
    IEquatable<int>,
    IEquatable<char>,
    IComparable<FlagValue>,
    IComparable<int>,
    IComparable<char>
{
    private const char ZeroValue = '\0';

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static implicit operator int(FlagValue flag) => flag._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static implicit operator FlagValue(int value) => new FlagValue(value);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static implicit operator char(FlagValue flag) => flag._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static implicit operator FlagValue(char value) => new FlagValue(value);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator !=(FlagValue a, FlagValue b) => a._value != b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator ==(FlagValue a, FlagValue b) => a._value == b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator >=(FlagValue a, FlagValue b) => a._value >= b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator <=(FlagValue a, FlagValue b) => a._value <= b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator >(FlagValue a, FlagValue b) => a._value > b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator <(FlagValue a, FlagValue b) => a._value < b._value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    internal static FlagValue Create(char high, char low) => new FlagValue(unchecked((char)((high << 8) | low)));

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool TryParseFlag(string text, FlagMode mode, out FlagValue value) =>
        TryParseFlag(text.AsSpan(), mode, out value);

    internal static bool TryParseFlag(ReadOnlySpan<char> text, FlagMode mode, out FlagValue value)
    {
        if (text.IsEmpty)
        {
            value = default;
            return false;
        }

        switch (mode)
        {
            case FlagMode.Char:
                value = new FlagValue(text[0]);
                return true;
            case FlagMode.Long:
                var a = text[0];
                value = text.Length >= 2
                    ? Create(a, text[1])
                    : new FlagValue(a);
                return true;
            case FlagMode.Num:
                return TryParseNumberFlag(text, out value);
            case FlagMode.Uni:
            default:
                throw new NotSupportedException();
        }
    }

    private static bool TryParseNumberFlag(ReadOnlySpan<char> text, out FlagValue value)
    {
        if (!text.IsEmpty && IntEx.TryParseInvariant(text, out int integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
        {
            value = new FlagValue(unchecked((char)integerValue));
            return true;
        }

        value = default;
        return false;
    }

    internal static FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text, FlagMode mode) =>
        mode switch
        {
            FlagMode.Char => text.IsEmpty ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text),
            FlagMode.Long => ParseLongFlagsInOrder(text),
            FlagMode.Num => ParseNumberFlagsInOrder(text).ToArray(),
            _ => throw new NotSupportedException(),
        };

    public static FlagSet ParseFlags(string text, FlagMode mode) => ParseFlags(text.AsSpan(), mode);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    internal static FlagSet ParseFlags(ReadOnlySpan<char> text, FlagMode mode) =>
        FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

    private static FlagValue[] ParseLongFlagsInOrder(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return ArrayEx<FlagValue>.Empty;
        }

        var flags = new FlagValue[(text.Length + 1) / 2];
        var flagWriteIndex = 0;
        var lastIndex = text.Length - 1;
        for (var i = 0; i < lastIndex; i += 2, flagWriteIndex++)
        {
            flags[flagWriteIndex] = Create(text[i], text[i + 1]);
        }

        if (flagWriteIndex < flags.Length)
        {
            flags[flagWriteIndex] = new FlagValue(text[lastIndex]);
        }

        return flags;
    }

    private static List<FlagValue> ParseNumberFlagsInOrder(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return new List<FlagValue>(0);
        }

        var flags = new List<FlagValue>();
        text.Split(',', (part, _) =>
        {
            if (TryParseNumberFlag(part, out var value))
            {
                flags.Add(value);
            }

            return true;
        });

        return flags;
    }

    private static FlagValue[] ConvertCharsToFlagsInOrder(ReadOnlySpan<char> text)
    {
        var values = new FlagValue[text.Length];
        for (var i = 0; i < values.Length; i++)
        {
            values[i] = new FlagValue(text[i]);
        }

        return values;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public FlagValue(char value)
    {
        _value = value;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public FlagValue(int value)
    {
        _value = checked((char)value);
    }

    private readonly char _value;

    public bool HasValue
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        get => _value != ZeroValue;
    }

    public bool IsZero
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        get => _value == ZeroValue;
    }

    public bool IsWildcard
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        get => _value == '*' || _value == '?';
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool Equals(FlagValue other) => other._value == _value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool Equals(int other) => other == _value;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool Equals(char other) => other == _value;

    public override bool Equals(object obj) =>
        obj switch
        {
            FlagValue value => Equals(value),
            int value => Equals(value),
            char value => Equals(value),
            _ => false,
        };

    public override int GetHashCode() => _value.GetHashCode();

    public int CompareTo(FlagValue other) => _value.CompareTo(other._value);

    public int CompareTo(int other) => ((int)_value).CompareTo(other);

    public int CompareTo(char other) => _value.CompareTo(other);

    public override string ToString() => ((int)_value).ToString(CultureInfo.InvariantCulture);
}
