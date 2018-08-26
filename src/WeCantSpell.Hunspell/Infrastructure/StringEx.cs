using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class StringEx
    {
        private static readonly char[] SpaceOrTab = { ' ', '\t' };

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Length != 0 && @this[0] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsTabOrSpace(this char c) => c == ' ' || c == '\t';

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

        public static bool Contains(this string @this, char value) => @this.IndexOf(value) >= 0;

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = StringBuilderPool.Get(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
            builder.Replace(index, removeCount, replacement);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index)
        {
#if DEBUG
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            return index < @this.Length ? @this[index] : '\0';
        }

        public static bool ContainsSubstringOrdinal(this string @this, string value, int startIndex, int length)
        {
            var firstChar = value[startIndex];
            var firstCharIndex = @this.IndexOf(firstChar);
            while (firstCharIndex >= 0)
            {
                if (@this.AsSpan(firstCharIndex).Limit(length).Equals(value.AsSpan(startIndex).Limit(length), StringComparison.Ordinal))
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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsLineBreakChar(this char c) => c == '\n' || c == '\r';

        public static string Concat(this string text0, ReadOnlySpan<char> text1)
        {
#if DEBUG
            if (text0 == null) throw new ArgumentNullException(nameof(text0));
#endif

            if (text0.Length == 0)
            {
                return text1.ToString();
            }

            if (text1.IsEmpty)
            {
                return text0;
            }

            var builder = StringBuilderPool.Get(text0.Length + text1.Length);
            builder.Append(text0);
            builder.Append(text1);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }
}
