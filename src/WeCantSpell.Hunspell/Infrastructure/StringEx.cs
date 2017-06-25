using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class StringEx
    {
        private static readonly char[] SpaceOrTab = { ' ', '\t' };

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
#endif
            return @this.Length != 0 && @this[0] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
#endif
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsCommentPrefix(char c) => c == '#' || c == '/';

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this)
        {
#if DEBUG
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
#endif
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsTabOrSpace(char c) => c == ' ' || c == '\t';

        public static int IndexOfNonTabOrSpace(string text, int offset)
        {
#if DEBUG
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
#endif
            for (; offset < text.Length; offset++)
            {
                if (!IsTabOrSpace(text[offset]))
                {
                    return offset;
                }
            }

            return -1;
        }

        public static string Reverse(this string @this)
        {
            if (@this == null || @this.Length <= 1)
            {
                return @this;
            }

            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool Contains(this string @this, char c) => @this.IndexOf(c) >= 0;

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = StringBuilderPool.Get(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
            builder.Replace(index, removeCount, replacement);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset)
        {
#if DEBUG
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (aOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aOffset));
            }
            if (bOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bOffset));
            }
#endif

            var aRemaining = a.Length - aOffset;
            return aRemaining >= 0
                && (aRemaining == b.Length - bOffset)
                && string.CompareOrdinal(a, aOffset, b, bOffset, aRemaining) == 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsLimited(string a, string b, int length)
        {
#if DEBUG
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
#endif
            return length <= 0 || string.CompareOrdinal(a, 0, b, 0, length) == 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length)
        {
#if DEBUG
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (aOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aOffset));
            }
            if (bOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bOffset));
            }
#endif
            return length <= 0 || string.CompareOrdinal(a, aOffset, b, bOffset, length) == 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length, StringComparison comparisonType)
        {
#if DEBUG
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (aOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aOffset));
            }
            if (bOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bOffset));
            }
#endif
            return length <= 0 || string.Compare(a, aOffset, b, bOffset, length, comparisonType) == 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index)
        {
#if DEBUG
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
#endif
            return index < @this.Length ? @this[index] : '\0';
        }

        public static bool ContainsSubstringOrdinal(this string @this, string value, int startIndex, int length)
        {
            var firstChar = value[startIndex];
            var firstCharIndex = @this.IndexOf(firstChar);
            while (firstCharIndex >= 0)
            {
                if (EqualsOffset(@this, firstCharIndex, value, startIndex, length))
                {
                    return true;
                }

                firstCharIndex = @this.IndexOf(firstChar, firstCharIndex + 1);
            }

            return false;
        }

        public static string ConcatString(string str0, int startIndex0, int count0, string str1)
        {
            if (count0 == 0)
            {
                return str1 ?? string.Empty;
            }

            var builder = StringBuilderPool.Get(str1.Length + count0);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, string str1, int startIndex1, int count1)
        {
            if (string.IsNullOrEmpty(str0))
            {
                return str1.Substring(startIndex1, count1);
            }

            var builder = StringBuilderPool.Get(str0.Length + count1);
            builder.Append(str0);
            builder.Append(str1, startIndex1, count1);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, char char2, string str3, int startIndex3)
        {
            var count3 = str3.Length - startIndex3;
            var builder = StringBuilderPool.Get(count0 + str1.Length + 1 + count3);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            builder.Append(char2);
            builder.Append(str3, startIndex3, count3);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, StringSlice str1) =>
            ConcatString(str0, str1.Text, str1.Offset, str1.Length);

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, int startIndex1) =>
            ConcatString(str0, startIndex0, count0, str1, startIndex1, str1.Length - startIndex1);

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, int startIndex1, int count1)
        {
            var builder = StringBuilderPool.Get(count0 + count1);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1, startIndex1, count1);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, string str2, int startIndex2)
        {
            var count2 = str2.Length - startIndex2;
            var builder = StringBuilderPool.Get(count0 + str1.Length + count2);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            builder.Append(str2, startIndex2, count2);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, int startIndex0, int count0, char char1) =>
            ConcatString(str0, startIndex0, count0, char1.ToString());

        public static string ConcatString(string str0, int startIndex0, int count0, char char1, string str2, int startIndex2) =>
            ConcatString(str0, startIndex0, count0, char1.ToString(), str2, startIndex2);

        public static int FirstIndexOfLineBreakChar(string text, int offset)
        {
#if DEBUG
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
#endif

            for (; offset < text.Length; offset++)
            {
                if (IsLineBreakChar(text[offset]))
                {
                    return offset;
                }
            }

            return -1;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsLineBreakChar(char c) => c == '\n' || c == '\r';

        public static bool ContainsAny(this string text, char a, char b)
        {
#if DEBUG
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
#endif

            return text.Length != 0
                && text.IndexOfAny(new[] { a, b }) >= 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int IndexOfOrdinal(this string text, string value) =>
            text.IndexOf(value, StringComparison.Ordinal);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int IndexOfOrdinal(this string text, string value, int startIndex) =>
            text.IndexOf(value, startIndex, StringComparison.Ordinal);
    }
}
