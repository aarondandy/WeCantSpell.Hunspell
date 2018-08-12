using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    struct StringSlice :
        IEquatable<string>,
        IEquatable<StringSlice>
    {
        public static readonly StringSlice Empty = new StringSlice(string.Empty, 0, 0);

        public static implicit operator StringSlice(string text) => string.IsNullOrEmpty(text) ? Empty : new StringSlice(text);

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

#if DEBUG
            if (text.Length < startIndex + length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
#endif
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
        public int IndexOf(string value, StringComparison comparisonType) =>
            Text.IndexOf(value, Offset, Length, comparisonType) - Offset;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int IndexOf(char c) =>
            Text.IndexOf(c, Offset, Length) - Offset;

        public int IndexOf(char c, int startIndex) =>
            Text.IndexOf(c, Offset + startIndex, Length - startIndex) - Offset;

        public bool Contains(char c) =>
            Text.IndexOf(c, Offset, Length) >= 0;

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

        public bool Equals(string other, StringComparison comparisonType) =>
            other != null
            &&
            (
                IsFullString
                    ? Text.Equals(other, comparisonType)
                    : (
                        Length == other.Length
                        && StringEx.EqualsOffset(Text, Offset, other, 0, Length, comparisonType)
                    )
            );

        public bool Equals(string other) => Equals(other, StringComparison.Ordinal);

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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

        public string ReplaceString(string oldValue, string newValue)
        {
            var builder = StringBuilderPool.Get(this);
            builder.Replace(oldValue, newValue);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public string ReplaceString(char oldValue, char newValue)
        {
            if (IsEmpty)
            {
                return string.Empty;
            }
            if (Length == 1)
            {
                var c = First();
                return c == oldValue
                    ? newValue.ToString()
                    : c.ToString();
            }

            var builder = StringBuilderPool.Get(this);
            builder.Replace(oldValue, newValue);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public int IndexOfSpaceOrTab(int searchOffset)
        {
            var lastIndex = Offset + Length;
            for (searchOffset = Offset + searchOffset; searchOffset < lastIndex; searchOffset++)
            {
                if (Text[searchOffset].IsTabOrSpace())
                {
                    return searchOffset - Offset;
                }
            }

            return -1;
        }

        public string ReverseString()
        {
            var builder = StringBuilderPool.Get(this);
            builder.Reverse();
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public char First()
        {
#if DEBUG
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }
#endif
            return Text[Offset];
        }
    }
}
