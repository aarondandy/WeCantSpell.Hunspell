﻿using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

public readonly struct FlagValue :
    IEquatable<FlagValue>,
    IEquatable<int>,
    IEquatable<char>,
    IComparable<FlagValue>,
    IComparable<int>,
    IComparable<char>
{
    internal const char ZeroValue = '\0';

    public static FlagValue Zero { get; } = (FlagValue)ZeroValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator char(FlagValue flag) => flag._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator FlagValue(char value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(FlagValue flag) => flag._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator FlagValue(int value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(FlagValue a, FlagValue b) => a._value != b._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(FlagValue a, FlagValue b) => a._value == b._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(FlagValue a, FlagValue b) => a._value >= b._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(FlagValue a, FlagValue b) => a._value <= b._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(FlagValue a, FlagValue b) => a._value > b._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(FlagValue a, FlagValue b) => a._value < b._value;

    internal static FlagValue CreateAsLong(char high, char low) => new(unchecked((char)((high << 8) | low)));

    internal static bool TryParseAsChar(string text, out FlagValue value)
    {
        if (text is { Length: > 0 })
        {
            value = new FlagValue(text[0]);
            return true;
        }

        value = default;
        return false;
    }

    internal static bool TryParseAsChar(ReadOnlySpan<char> text, out FlagValue value)
    {
        if (text.Length > 0)
        {
            value = new FlagValue(text[0]);
            return true;
        }

        value = default;
        return false;
    }

    internal static bool TryParseAsLong(ReadOnlySpan<char> text, out FlagValue value)
    {
        if (text.IsEmpty)
        {
            value = default;
            return false;
        }

        value = text.Length >= 2
            ? CreateAsLong(text[0], text[1])
            : new FlagValue(text[0]);
        return true;
    }

    internal static bool TryParseAsNumber(ReadOnlySpan<char> text, out FlagValue value)
    {
        if (!text.IsEmpty && IntEx.TryParseInvariant(text, out var integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
        {
            value = new(unchecked((char)integerValue));
            return true;
        }

        value = default;
        return false;
    }

    internal static FlagValue[] ParseAsChars(string text)
    {
        if (text.Length > 0)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new FlagValue(text[i]);
            }

            return values;
        }

        return [];
    }

    internal static FlagValue[] ParseAsChars(ReadOnlySpan<char> text)
    {
        if (text.Length > 0)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new FlagValue(text[i]);
            }

            return values;
        }

        return [];
    }

    internal static FlagValue[] ParseAsLongs(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return [];
        }

        var flags = new FlagValue[(text.Length + 1) / 2];
        var flagWriteIndex = 0;
        var lastIndex = text.Length - 1;
        for (var i = 0; i < lastIndex; i += 2, flagWriteIndex++)
        {
            flags[flagWriteIndex] = CreateAsLong(text[i], text[i + 1]);
        }

        if (flagWriteIndex < flags.Length)
        {
            flags[flagWriteIndex] = new FlagValue(text[lastIndex]);
        }

        return flags;
    }

    internal static FlagValue[] ParseAsNumbers(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return [];
        }

        var builder = ArrayBuilder<FlagValue>.Pool.Get();

        foreach (var part in text.SplitOnComma(StringSplitOptions.RemoveEmptyEntries))
        {
            if (TryParseAsNumber(part, out var value))
            {
                builder.Add(value);
            }
        }

        return ArrayBuilder<FlagValue>.Pool.ExtractAndReturn(builder);
    }

    public FlagValue(char value)
    {
        _value = value;
    }

    public FlagValue(int value)
    {
        _value = checked((char)value);
    }

    private readonly char _value;

    public bool HasValue => _value != ZeroValue;

    public bool IsZero => _value == ZeroValue;

    public bool IsWildcard => _value is '*' or '?';

    public bool IsNotWildcard => _value is not '*' or '?';

    internal bool IsPrintable => (int)_value is > 32 and < 127;

    public bool Equals(FlagValue other) => other._value == _value;

    [Obsolete("To be removed")]
    public bool EqualsAny(FlagValue a, FlagValue b) => a._value == _value || b._value == _value;

    [Obsolete("To be removed")]
    public bool EqualsAny(FlagValue a, FlagValue b, FlagValue c) => a._value == _value || b._value == _value || c._value == _value;

    public bool Equals(int other) => other == _value;

    public bool Equals(char other) => other == _value;

    public override bool Equals(object? obj) => obj switch
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

    public override string ToString()
    {
        var result = ((int)_value).ToString(CultureInfo.InvariantCulture);

        if (IsPrintable)
        {
            result += " '" + _value.ToString() + "'";
        }

        return result;
    }
}
