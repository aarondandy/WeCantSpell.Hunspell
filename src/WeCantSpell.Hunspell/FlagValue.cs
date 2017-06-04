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

        public static FlagValue Create(char high, char low) => new FlagValue(unchecked((char)((high << 8) | low)));

        public static bool TryParseFlag(string text, FlagMode mode, out FlagValue value) =>
            TryParseFlag(StringSlice.Create(text), mode, out value);

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static bool TryParseFlag(string text, int startIndex, int length, FlagMode mode, out FlagValue value) =>
            TryParseFlag(text.Subslice(startIndex, length), mode, out value);

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

        public static bool TryParseNumberFlag(string text, out FlagValue value) =>
            TryParseNumberFlag(StringSlice.Create(text), out value);

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static bool TryParseNumberFlag(string text, int startIndex, int length, out FlagValue value) =>
            TryParseNumberFlag(text.Subslice(startIndex, length), out value);

        internal static bool TryParseNumberFlag(StringSlice text, out FlagValue value)
        {
            if (!text.IsNullOrEmpty)
            {
                if (IntEx.TryParseInvariant(text, out int integerValue) && integerValue >= char.MinValue && integerValue <= char.MaxValue)
                {
                    value = new FlagValue(unchecked((char)integerValue));
                    return true;
                }
            }

            value = default(FlagValue);
            return false;
        }

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static FlagValue[] ParseFlagsInOrder(string text, int startIndex, int length, FlagMode mode) =>
            ParseFlagsInOrder(text.Subslice(startIndex, length), mode);

        public static FlagValue[] ParseFlagsInOrder(string text, FlagMode mode) =>
            ParseFlagsInOrder(StringSlice.Create(text), mode);

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

        public static FlagSet ParseFlags(string text, FlagMode mode) =>
            FlagSet.TakeArray(ParseFlagsInOrder(text, mode));

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static FlagSet ParseFlags(string text, int startIndex, int length, FlagMode mode) =>
            FlagSet.TakeArray(ParseFlagsInOrder(text, startIndex, length, mode));

        public static FlagValue[] ParseLongFlagsInOrder(string text) =>
            ParseLongFlagsInOrder(StringSlice.Create(text));

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static FlagValue[] ParseLongFlagsInOrder(string text, int startIndex, int length) =>
            ParseLongFlagsInOrder(text.Subslice(startIndex, length));

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

        public static FlagSet ParseLongFlags(string text) =>
            ParseLongFlags(StringSlice.Create(text));

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static FlagSet ParseLongFlags(string text, int startIndex, int length) =>
            ParseLongFlags(text.Subslice(startIndex, length));

        internal static FlagSet ParseLongFlags(StringSlice text) =>
            FlagSet.TakeArray(ParseLongFlagsInOrder(text));

        public static List<FlagValue> ParseNumberFlagsInOrder(string text) =>
            ParseNumberFlagsInOrder(StringSlice.Create(text));

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
                if (TryParseNumberFlag(textParts[i], out FlagValue value))
                {
                    flags.Add(value);
                }
            }

            return flags;
        }

        public static FlagSet ParseNumberFlags(string text) =>
            ParseNumberFlags(StringSlice.Create(text));

        [Obsolete("These APIs will be replaced when Span<T> is released.")]
        public static FlagSet ParseNumberFlags(string text, int startIndex, int length) =>
            FlagSet.Create(ParseNumberFlagsInOrder(text.Subslice(startIndex, length)));

        internal static FlagSet ParseNumberFlags(StringSlice text) =>
            FlagSet.Create(ParseNumberFlagsInOrder(text));

        private static FlagValue[] ConvertCharsToFlagsInOrder(string text) =>
            ConvertCharsToFlagsInOrder(StringSlice.Create(text));

        private static FlagValue[] ConvertCharsToFlagsInOrder(StringSlice text)
        {
            var values = new FlagValue[text.Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new FlagValue(text.Text[text.Offset + i]);
            }

            return values;
        }

        private readonly char value;

        public FlagValue(char value) =>
            this.value = value;

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
