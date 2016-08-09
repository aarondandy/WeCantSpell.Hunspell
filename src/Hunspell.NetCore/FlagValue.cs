using Hunspell.Utilities;
using System;
using System.Globalization;

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

        public static bool TryParse(string text, out FlagValue value)
        {
            int integerValue;
            var parsedOk = IntExtensions.TryParseInvariant(text, out integerValue);
            value = new FlagValue(integerValue);
            return parsedOk;
        }

        public static implicit operator int(FlagValue flag)
        {
            return flag.value;
        }
    }
}
