using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

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
        private const char ZeroValue = '\0';

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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static FlagValue Create(char high, char low) => new FlagValue(unchecked((char)((high << 8) | low)));

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

        internal static bool TryParseFlag(StringSlice text, FlagMode mode, out FlagValue value)
        {
            if (text.IsEmpty)
            {
                value = default(FlagValue);
                return false;
            }

            switch (mode)
            {
                case FlagMode.Char:
                    value = new FlagValue(text.First());
                    return true;
                case FlagMode.Long:
                    var a = text.First();
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

        private static bool TryParseNumberFlag(string text, out FlagValue value)
        {
            if (!string.IsNullOrEmpty(text) && IntEx.TryParseInvariant(text, out int integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
            {
                value = new FlagValue(unchecked((char)integerValue));
                return true;
            }

            value = default(FlagValue);
            return false;
        }

        private static bool TryParseNumberFlag(StringSlice text, out FlagValue value)
        {
            if (!text.IsEmpty && IntEx.TryParseInvariant(text.ToString(), out int integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
            {
                value = new FlagValue(unchecked((char)integerValue));
                return true;
            }

            value = default(FlagValue);
            return false;
        }

        internal static FlagValue[] ParseFlagsInOrder(string text, FlagMode mode)
        {
            switch (mode)
            {
                case FlagMode.Char: return string.IsNullOrEmpty(text) ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text);
                case FlagMode.Long: return ParseLongFlagsInOrder(text);
                case FlagMode.Num: return ParseNumberFlagsInOrder(text).ToArray();
                default: throw new NotSupportedException();
            }
        }

        internal static FlagValue[] ParseFlagsInOrder(StringSlice text, FlagMode mode)
        {
            switch (mode)
            {
                case FlagMode.Char: return text.IsEmpty ? ArrayEx<FlagValue>.Empty : ConvertCharsToFlagsInOrder(text);
                case FlagMode.Long: return ParseLongFlagsInOrder(text);
                case FlagMode.Num: return ParseNumberFlagsInOrder(text).ToArray();
                default: throw new NotSupportedException();
            }
        }

        public static FlagSet ParseFlags(string text, FlagMode mode) =>
            string.IsNullOrEmpty(text)
                ? FlagSet.Empty
                : FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static FlagSet ParseFlags(StringSlice text, FlagMode mode) =>
            FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

        private static FlagValue[] ParseLongFlagsInOrder(string text) =>
            string.IsNullOrEmpty(text)
                ? ArrayEx<FlagValue>.Empty
                : ParseLongFlagsInOrder(new StringSlice(text));

        private static FlagValue[] ParseLongFlagsInOrder(StringSlice text)
        {
            if (text.IsEmpty)
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

        private static List<FlagValue> ParseNumberFlagsInOrder(string text) =>
            string.IsNullOrEmpty(text)
                ? new List<FlagValue>()
                : ParseNumberFlagsInOrder(new StringSlice(text));

        private static List<FlagValue> ParseNumberFlagsInOrder(StringSlice text)
        {
            if (text.IsEmpty)
            {
                return new List<FlagValue>(0);
            }

            var textParts = text.SplitOnComma();

            var flags = new List<FlagValue>(textParts.Count);
            for (var i = 0; i < textParts.Count; i++)
            {
                if (TryParseNumberFlag(textParts[i], out FlagValue value))
                {
                    flags.Add(value);
                }
            }

            return flags;
        }

        private static FlagValue[] ConvertCharsToFlagsInOrder(string text)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new FlagValue(text[i]);
            }

            return values;
        }

        private static FlagValue[] ConvertCharsToFlagsInOrder(StringSlice text)
        {
            var values = new FlagValue[text.Length];
            for (int valueIndex = 0, textIndex = text.Offset; valueIndex < values.Length; valueIndex++, textIndex++)
            {
                values[valueIndex] = new FlagValue(text.Text[textIndex]);
            }

            return values;
        }

        private readonly char value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FlagValue(char value) =>
            this.value = value;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FlagValue(int value) =>
            this.value = checked((char)value);

        public bool HasValue
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => value != ZeroValue;
        }

        public bool IsZero
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => value == ZeroValue;
        }

        internal bool IsWildcard
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => value == '*' || value == '?';
        }

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
            if (obj is FlagValue flagValue)
            {
                return Equals(flagValue);
            }
            if (obj is int intValue)
            {
                return Equals(intValue);
            }
            if (obj is char charValue)
            {
                return Equals(charValue);
            }

            return false;
        }

        public override int GetHashCode() => value.GetHashCode();

        public int CompareTo(FlagValue other) => value.CompareTo(other.value);

        public int CompareTo(int other) => ((int)value).CompareTo(other);

        public int CompareTo(char other) => value.CompareTo(other);

        public override string ToString() => ((int)value).ToString(CultureInfo.InvariantCulture);
    }
}
