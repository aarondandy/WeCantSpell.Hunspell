using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell
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

        public bool HasValue
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return value != 0;
            }
        }

        public bool IsZero
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return value == 0;
            }
        }

        public static FlagValue Create(char high, char low) => new FlagValue(unchecked((char)((high << 8) | low)));

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool Equals(FlagValue other) => other.value == value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool Equals(int other) => other == value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool Equals(char other) => other == value;

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

        public override int GetHashCode() => value.GetHashCode();

        public int CompareTo(FlagValue other) => value.CompareTo(other.value);

        public int CompareTo(int other) => ((int)value).CompareTo(other);

        public int CompareTo(char other) => value.CompareTo(other);

        public override string ToString() => ((int)value).ToString(CultureInfo.InvariantCulture);

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

        internal static bool TryParseFlag(StringSlice text, FlagMode mode, out FlagValue value)
        {
            if (text.IsNullOrEmpty)
            {
                value = default(FlagValue);
                return false;
            }

            switch (mode)
            {
                case FlagMode.Char:
                    value = new FlagValue(text.Text[text.Offset]);
                    return true;
                case FlagMode.Long:
                    var a = text.Text[text.Offset];
                    value = text.Length >= 2
                        ? Create(a, text.Text[text.Offset + 1])
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

        internal static bool TryParseNumberFlag(StringSlice text, out FlagValue value)
        {
            if (!text.IsNullOrEmpty)
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
                return ParseNumberFlagsInOrder(text).ToArray();
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
                return ParseNumberFlagsInOrder(text, startIndex, length).ToArray();
            }

            throw new NotSupportedException();
        }

        internal static FlagSet ParseFlags(StringSlice text, FlagMode mode) => FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

        internal static FlagValue[] ParseFlagsInOrder(StringSlice text, FlagMode mode)
        {
            if (mode == FlagMode.Char)
            {
                return text.Length == 0 ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text);
            }
            if (mode == FlagMode.Long)
            {
                return ParseLongFlagsInOrder(text);
            }
            if (mode == FlagMode.Num)
            {
                return ParseNumberFlagsInOrder(text).ToArray();
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

        private static FlagSet ConvertCharsToFlags(StringSlice text) => FlagSet.TakeArray(ConvertCharsToFlagsInOrder(text));

        private static FlagValue[] ConvertCharsToFlagsInOrder(StringSlice text)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new FlagValue(text.Text[text.Offset + i]);
            }

            return values;
        }

        public static FlagSet ParseLongFlags(string text) =>
            ParseLongFlags(StringSlice.Create(text));

        public static FlagValue[] ParseLongFlagsInOrder(string text) =>
            ParseLongFlagsInOrder(StringSlice.Create(text));

        public static FlagSet ParseLongFlags(string text, int startIndex, int length) =>
            ParseLongFlags(text.Subslice(startIndex, length));

        public static FlagValue[] ParseLongFlagsInOrder(string text, int startIndex, int length)
            => ParseLongFlagsInOrder(text.Subslice(startIndex, length));

        internal static FlagSet ParseLongFlags(StringSlice text) =>
            FlagSet.TakeArray(ParseLongFlagsInOrder(text));

        internal static FlagValue[] ParseLongFlagsInOrder(StringSlice text)
        {
            if (text.IsNullOrEmpty)
            {
                return ArrayEx<FlagValue>.Empty;
            }

            var flags = new FlagValue[(text.Length + 1) / 2];
            var flagWriteIndex = 0;
            var lastIndex = text.Offset + text.Length - 1;
            for (var i = text.Offset; i < lastIndex; i += 2, flagWriteIndex++)
            {
                flags[flagWriteIndex] = Create(text.Text[i], text.Text[i + 1]);
            }

            if (flagWriteIndex < flags.Length)
            {
                flags[flagWriteIndex] = new FlagValue(text.Text[lastIndex]);
            }

            return flags;
        }

        public static FlagSet ParseNumberFlags(string text) =>
            ParseNumberFlags(StringSlice.Create(text));

        public static List<FlagValue> ParseNumberFlagsInOrder(string text) =>
            ParseNumberFlagsInOrder(StringSlice.Create(text));

        public static FlagSet ParseNumberFlags(string text, int startIndex, int length) =>
            FlagSet.Create(ParseNumberFlagsInOrder(text, startIndex, length));

        public static List<FlagValue> ParseNumberFlagsInOrder(string text, int startIndex, int length) =>
            ParseNumberFlagsInOrder(text.Subslice(startIndex, length));

        internal static FlagSet ParseNumberFlags(StringSlice text) =>
            FlagSet.Create(ParseNumberFlagsInOrder(text));

        internal static List<FlagValue> ParseNumberFlagsInOrder(StringSlice text)
        {
            if (text.IsNullOrEmpty)
            {
                return new List<FlagValue>(0);
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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static implicit operator int(FlagValue flag) => flag.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static implicit operator char(FlagValue flag) => flag.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator !=(FlagValue a, FlagValue b) => a.value != b.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator ==(FlagValue a, FlagValue b) => a.value == b.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator >=(FlagValue a, FlagValue b) => a.value >= b.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator <=(FlagValue a, FlagValue b) => a.value <= b.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator >(FlagValue a, FlagValue b) => a.value > b.value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator <(FlagValue a, FlagValue b) => a.value < b.value;
    }
}
