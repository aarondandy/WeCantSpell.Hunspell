using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal struct StringSlice :
        IEquatable<string>,
        IEquatable<StringSlice>
    {
        public static readonly StringSlice Empty = new StringSlice(string.Empty, 0, 0);

        public readonly string Text;

        public readonly int Offset;

        public readonly int Length;

        public StringSlice(string text)
        {
#if DEBUG
            Text = text ?? throw new ArgumentNullException(nameof(text));
#else
            Text = text;
#endif
            Length = text.Length;
            Offset = 0;
        }

        public StringSlice(string text, int startIndex, int length)
        {
#if DEBUG
            Text = text ?? throw new ArgumentNullException(nameof(text));
#else
            Text = text;
#endif
            Offset = startIndex;
            Length = length;
        }

        public bool IsEmpty
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Length == 0;
        }

        public bool IsFullString
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Offset == 0 && Text.Length == Length;
        }

        public char this[int index]
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Text[Offset + index];
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public override string ToString() =>
            IsFullString
            ? Text
            : Text.Substring(Offset, Length);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOfOrdinal(string value) =>
            Text.IndexOf(value, Offset, Length, StringComparison.Ordinal) - Offset;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOf(string value, StringComparison comparisonType) =>
            Text.IndexOf(value, Offset, Length, comparisonType) - Offset;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOfOrdinal(string value, int startIndex) =>
            Text.IndexOf(value, Offset + startIndex, Length - startIndex, StringComparison.Ordinal) - Offset;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOf(string value, int startIndex, StringComparison comparisonType) =>
            Text.IndexOf(value, Offset + startIndex, Length - startIndex, comparisonType) - Offset;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOf(char c) =>
            Text.IndexOf(c, Offset, Length) - Offset;

        public string Substring(int startIndex) =>
            Substring(startIndex, Length - startIndex);

        public string Substring(int startIndex, int length)
        {
#if DEBUG
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if (length > Length - startIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
#endif
            return Text.Substring(Offset + startIndex, length);
        }

        public bool Equals(string other) =>
            other != null
            && Length == other.Length
            && StringEx.EqualsOffset(Text, Offset, other, 0, Length);

        public bool Equals(StringSlice other) =>
            Length == other.Length
            && StringEx.EqualsOffset(Text, Offset, other.Text, other.Offset, Length);

        public override bool Equals(object obj)
        {
            if (obj is StringSlice slice)
            {
                return Equals(slice);
            }
            if (obj is string stringValue)
            {
                return Equals(stringValue);
            }

            return false;
        }

        public override int GetHashCode() => Length;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public char[] ToCharArray() => Text.ToCharArray(Offset, Length);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public StringSlice Subslice(int startIndex)
        {
#if DEBUG
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
#endif
            return new StringSlice(Text, Offset + startIndex, Length - startIndex);
        }

        public StringSlice Subslice(int startIndex, int length)
        {
#if DEBUG
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if (length > Length - startIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
#endif
            return new StringSlice(Text, Offset + startIndex, length);
        }

        public string ReplaceString(char oldValue, char newValue)
        {
            if (IsEmpty)
            {
                return string.Empty;
            }
            if (Length == 1)
            {
                var c = this[0];
                return c == oldValue
                    ? newValue.ToString()
                    : c.ToString();
            }

            var builder = StringBuilderPool.Get(this);
            builder.Replace(oldValue, newValue);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }
}
