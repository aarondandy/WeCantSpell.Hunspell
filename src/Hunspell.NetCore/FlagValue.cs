using Hunspell.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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

        public static IEnumerable<FlagValue> ParseFlags(string text, FlagMode mode)
        {
            switch (mode)
            {
                case FlagMode.Char:
                    return text == null
                        ? Enumerable.Empty<FlagValue>()
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

        private static FlagValue[] ConvertCharsToFlags(string text)
        {
            var result = new FlagValue[text.Length];
            for(var i = 0; i < result.Length; i++)
            {
                result[i] = new FlagValue(text[i]);
            }

            return result;
        }

        public static IEnumerable<FlagValue> ParseLongFlags(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<FlagValue>();
            }

            var flags = new List<FlagValue>((text.Length + 1) / 2);
            var lastIndex = text.Length - 1;
            for (var i = 0; i < lastIndex; i += 2)
            {
                flags.Add(new FlagValue(text[i], unchecked((byte)text[i + 1])));
            }

            if (text.Length % 2 == 1)
            {
                flags.Add(new FlagValue(text[text.Length - 1]));
            }

            return flags;
        }

        public static IEnumerable<FlagValue> ParseNumberFlags(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<FlagValue>();
            }

            var textParts = text.SplitOnComma();
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

        public static implicit operator int(FlagValue flag)
        {
            return flag.value;
        }
    }
}
