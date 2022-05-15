using System;
using System.Collections.Generic;
using System.Globalization;

using WeCantSpell.Hunspell.Infrastructure;

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

    public static implicit operator char(FlagValue flag) => flag._value;

    public static explicit operator FlagValue(char value) => new(value);

    public static implicit operator int(FlagValue flag) => flag._value;

    public static explicit operator FlagValue(int value) => new(value);

    public static bool operator !=(FlagValue a, FlagValue b) => a._value != b._value;

    public static bool operator ==(FlagValue a, FlagValue b) => a._value == b._value;

    public static bool operator >=(FlagValue a, FlagValue b) => a._value >= b._value;

    public static bool operator <=(FlagValue a, FlagValue b) => a._value <= b._value;

    public static bool operator >(FlagValue a, FlagValue b) => a._value > b._value;

    public static bool operator <(FlagValue a, FlagValue b) => a._value < b._value;

    internal static FlagValue CreateAsLong(char high, char low) => new(unchecked((char)((high << 8) | low)));

    internal static bool TryParseAsChar(ReadOnlySpan<char> text, out FlagValue value)
    {
        if (text.IsEmpty)
        {
            value = default;
            return false;
        }

        value = new FlagValue(text[0]);
        return true;
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

    internal static FlagValue[] ParseAsChars(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Array.Empty<FlagValue>();
        }

        var values = new FlagValue[text.Length];

        for (var i = 0; i < values.Length; i++)
        {
            values[i] = new FlagValue(text[i]);
        }

        return values;
    }

    internal static FlagValue[] ParseAsLongs(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Array.Empty<FlagValue>();
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
            return Array.Empty<FlagValue>();
        }

        var flags = new List<FlagValue>();

        foreach (var part in text.SplitOnComma(StringSplitOptions.RemoveEmptyEntries))
        {
            if (TryParseAsNumber(part, out var value))
            {
                flags.Add(value);
            }
        }

        return flags.ToArray();
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

    public bool Equals(FlagValue other) => other._value == _value;

    public bool EqualsAny(FlagValue a, FlagValue b) => a._value == _value || b._value == _value;

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

    public override string ToString() => ((int)_value).ToString(CultureInfo.InvariantCulture);
}
