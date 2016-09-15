using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Hunspell
{
    public struct FlagValue :
        IEquatable<FlagValue>,
        IEquatable<int>,
        IEquatable<char>,
        IComparable<FlagValue>,
        IComparable<int>,
        IComparable<char>
    {
        private char value;

        public FlagValue(char value)
        {
            this.value = value;
        }

        public FlagValue(int value)
        {
            this.value = checked((char)value);
        }

        public bool HasValue => value != 0;

        public static FlagValue Create(char high, char low)
        {
            return new FlagValue(unchecked((char)((high << 8) | low)));
        }

        public bool Equals(FlagValue other)
        {
            return other.value == value;
        }

        public bool Equals(int other)
        {
            return other == value;
        }

        public bool Equals(char other)
        {
            return other == value;
        }

        public override bool Equals(object obj)
        {
            if (obj is FlagValue)
            {
                return Equals((FlagValue)obj);
            }
            if (obj is int)
            {
                return Equals((int)obj);
            }
            if (obj is char)
            {
                return Equals((char)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public int CompareTo(FlagValue other)
        {
            return value.CompareTo(other.value);
        }

        public int CompareTo(int other)
        {
            return ((int)value).CompareTo(other);
        }

        public int CompareTo(char other)
        {
            return value.CompareTo(other);
        }

        public override string ToString()
        {
            return ((int)value).ToString(CultureInfo.InvariantCulture);
        }

        public static bool TryParseFlag(string text, FlagMode mode, out FlagValue value)
        {
            if (string.IsNullOrEmpty(text))
            {
                value = default(FlagValue);
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

        public static bool TryParseFlag(string text, int startIndex, int length, FlagMode mode, out FlagValue value)
        {
            if (string.IsNullOrEmpty(text))
            {
                value = default(FlagValue);
                return false;
            }

            switch (mode)
            {
                case FlagMode.Char:
                    value = new FlagValue(text[startIndex]);
                    return true;
                case FlagMode.Long:
                    var a = text[startIndex];
                    value = length >= 2
                        ? Create(a, text[startIndex + 1])
                        : new FlagValue(a);
                    return true;
                case FlagMode.Num:
                    return TryParseNumberFlag(text, startIndex, length, out value);
                case FlagMode.Uni:
                default:
                    throw new NotSupportedException();
            }
        }

        public static bool TryParseNumberFlag(string text, out FlagValue value)
        {
            if (text != null)
            {
                int integerValue;
                if (IntEx.TryParseInvariant(text, out integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
                {
                    value = new FlagValue(unchecked((char)integerValue));
                    return true;
                }
            }

            value = default(FlagValue);
            return false;
        }

        public static bool TryParseNumberFlag(string text, int startIndex, int length, out FlagValue value)
        {
            if (text != null)
            {
                int integerValue;
                if (IntEx.TryParseInvariant(text, startIndex, length, out integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
                {
                    value = new FlagValue(unchecked((char)integerValue));
                    return true;
                }
            }

            value = default(FlagValue);
            return false;
        }

        public static FlagSet ParseFlags(string text, FlagMode mode) => FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

        public static FlagValue[] ParseFlagsInOrder(string text, FlagMode mode)
        {
            if (mode == FlagMode.Char)
            {
                return text == null ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text);
            }
            if (mode == FlagMode.Long)
            {
                return ParseLongFlagsInOrder(text);
            }
            if (mode == FlagMode.Num)
            {
                return ParseNumberFlagsInOrder(text);
            }

            throw new NotSupportedException();
        }

        public static FlagSet ParseFlags(string text, int startIndex, int length, FlagMode mode) => FlagSet.TakeArray(ParseFlagsInOrder(text, startIndex, length, mode));

        public static FlagValue[] ParseFlagsInOrder(string text, int startIndex, int length, FlagMode mode)
        {
            if (mode == FlagMode.Char)
            {
                return text == null ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text, startIndex, length);
            }
            if (mode == FlagMode.Long)
            {
                return ParseLongFlagsInOrder(text, startIndex, length);
            }
            if (mode == FlagMode.Num)
            {
                return ParseNumberFlagsInOrder(text, startIndex, length);
            }

            throw new NotSupportedException();
        }

        private static FlagSet ConvertCharsToFlags(string text) => FlagSet.TakeArray(ConvertCharsToFlagsInOrder(text));

        private static FlagValue[] ConvertCharsToFlagsInOrder(string text)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < text.Length; i++)
            {
                values[i] = new FlagValue(text[i]);
            }

            return values;
        }

        private static FlagSet ConvertCharsToFlags(string text, int startIndex, int length) => FlagSet.TakeArray(ConvertCharsToFlagsInOrder(text, startIndex, length));

        private static FlagValue[] ConvertCharsToFlagsInOrder(string text, int startIndex, int length)
        {
            var values = new FlagValue[length];
            for (var i = 0; i < length; i++)
            {
                values[i] = new FlagValue(text[startIndex + i]);
            }

            return values;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static FlagSet ParseLongFlags(string text) => ParseLongFlags(text, 0, text.Length);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static FlagValue[] ParseLongFlagsInOrder(string text) => ParseLongFlagsInOrder(text, 0, text.Length);

        public static FlagSet ParseLongFlags(string text, int startIndex, int length) => FlagSet.TakeArray(ParseLongFlagsInOrder(text, startIndex, length));

        public static FlagValue[] ParseLongFlagsInOrder(string text, int startIndex, int length)
        {
            if (length == 0 || string.IsNullOrEmpty(text))
            {
                return ArrayEx<FlagValue>.Empty;
            }

            var flags = new FlagValue[(length + 1) / 2];
            var flagWriteIndex = 0;
            var lastIndex = startIndex + length - 1;
            for (var i = startIndex; i < lastIndex; i += 2, flagWriteIndex++)
            {
                flags[flagWriteIndex] = Create(text[i], text[i + 1]);
            }

            if (flagWriteIndex < flags.Length)
            {
                flags[flagWriteIndex] = new FlagValue(text[lastIndex]);
            }

            return flags;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static FlagSet ParseNumberFlags(string text) => ParseNumberFlags(text, 0, text.Length);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static FlagValue[] ParseNumberFlagsInOrder(string text) => ParseNumberFlagsInOrder(text, 0, text.Length);

        public static FlagSet ParseNumberFlags(string text, int startIndex, int length) => FlagSet.TakeArray(ParseNumberFlagsInOrder(text, startIndex, length));

        public static FlagValue[] ParseNumberFlagsInOrder(string text, int startIndex, int length)
        {
            if (length == 0 || string.IsNullOrEmpty(text))
            {
                return ArrayEx<FlagValue>.Empty;
            }

            var textParts = text.Substring(startIndex, length).SplitOnComma();

            var flags = new List<FlagValue>(textParts.Length);
            for (var i = 0; i < textParts.Length; i++)
            {
                FlagValue value;
                if (TryParseNumberFlag(textParts[i], out value))
                {
                    flags.Add(value);
                }
            }

            return flags.ToArray();
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static implicit operator int(FlagValue flag)
        {
            return flag.value;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static implicit operator char(FlagValue flag)
        {
            return flag.value;
        }
    }
}
