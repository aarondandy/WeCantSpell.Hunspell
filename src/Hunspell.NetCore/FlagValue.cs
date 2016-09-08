using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public FlagValue(int value)
        {
            this.value = value;
        }

        public FlagValue(char value)
        {
            this.value = value;
        }

        public FlagValue(int a, byte b)
        {
            this.value = unchecked(a << 8 | b);
        }

        private int value;

        public bool HasValue => value != 0;

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
            return value.CompareTo(other);
        }

        public int CompareTo(char other)
        {
            return value.CompareTo(other);
        }

        public override string ToString()
        {
            return value.ToString(CultureInfo.InvariantCulture);
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
                        ? new FlagValue(a, unchecked((byte)text[1]))
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
                        ? new FlagValue(a, unchecked((byte)text[startIndex + 1]))
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
            if (text == null)
            {
                value = default(FlagValue);
                return false;
            }

            int integerValue;
            var parsedOk = IntEx.TryParseInvariant(text, out integerValue);
            value = new FlagValue(integerValue);
            return parsedOk;
        }

        public static bool TryParseNumberFlag(string text, int startIndex, int length, out FlagValue value)
        {
            if (text == null)
            {
                value = default(FlagValue);
                return false;
            }

            int integerValue;
            var parsedOk = IntEx.TryParseInvariant(text, startIndex, length, out integerValue);
            value = new FlagValue(integerValue);
            return parsedOk;
        }

        public static IEnumerable<FlagValue> ParseFlags(string text, FlagMode mode)
        {
            switch (mode)
            {
                case FlagMode.Char:
                    return text == null
                        ? ArrayEx<FlagValue>.Empty
                        : ConvertCharsToFlags(text);
                case FlagMode.Long:
                    return ParseLongFlags(text);
                case FlagMode.Num:
                    return ParseNumberFlags(text);
                case FlagMode.Uni:
                default:
                    throw new NotSupportedException();
            }
        }

        public static IEnumerable<FlagValue> ParseFlags(string text, int startIndex, int length, FlagMode mode)
        {
            switch (mode)
            {
                case FlagMode.Char:
                    return text == null
                        ? ArrayEx<FlagValue>.Empty
                        : ConvertCharsToFlags(text, startIndex, length);
                case FlagMode.Long:
                    return ParseLongFlags(text, startIndex, length);
                case FlagMode.Num:
                    return ParseNumberFlags(text, startIndex, length);
                case FlagMode.Uni:
                default:
                    throw new NotSupportedException();
            }
        }

        private static FlagValue[] ConvertCharsToFlags(string text)
        {
            var result = new FlagValue[text.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FlagValue(text[i]);
            }

            return result;
        }

        private static FlagValue[] ConvertCharsToFlags(string text, int startIndex, int length)
        {
            var result = new FlagValue[length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FlagValue(text[i + startIndex]);
            }

            return result;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static List<FlagValue> ParseLongFlags(string text)
        {
            return ParseLongFlags(text, 0, text.Length);
        }

        public static List<FlagValue> ParseLongFlags(string text, int startIndex, int length)
        {
            if (length == 0 || string.IsNullOrEmpty(text))
            {
                return new List<FlagValue>();
            }

            var flags = new List<FlagValue>((length + 1) / 2);
            var lastIndex = startIndex + length - 1;
            for (var i = startIndex; i < lastIndex; i += 2)
            {
                flags.Add(new FlagValue(text[i], unchecked((byte)text[i + 1])));
            }

            if (length % 2 == 1)
            {
                flags.Add(new FlagValue(text[lastIndex]));
            }

            return flags;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static List<FlagValue> ParseNumberFlags(string text)
        {
            return ParseNumberFlags(text, 0, text.Length);
        }

        public static List<FlagValue> ParseNumberFlags(string text, int startIndex, int length)
        {
            if (length == 0 || string.IsNullOrEmpty(text))
            {
                return new List<FlagValue>();
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

            return flags;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static implicit operator int(FlagValue flag)
        {
            return flag.value;
        }
    }
}
