using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class StringEx
    {
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private static readonly char[] CommaArray = new[] { ',' };

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[0] == character;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this)
        {
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnComma(this string @this)
        {
            return @this.Split(CommaArray);
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

        public static string RemoveChars(this string @this, ImmutableSortedSet<char> chars)
        {
            if (string.IsNullOrEmpty(@this) || chars == null || chars.IsEmpty)
            {
                return @this;
            }

            var builder = StringBuilderPool.Get(@this);
            builder.RemoveChars(chars);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool Contains(this string @this, char c)
        {
            return @this.IndexOf(c) >= 0;
        }

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = StringBuilderPool.Get(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
            builder.Replace(index, removeCount, replacement);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset)
        {
            if (ReferenceEquals(a, b) && aOffset == bOffset)
            {
                return true;
            }

            var aRemaining = (a?.Length).GetValueOrDefault() - aOffset;
            var bRemaining = (b?.Length).GetValueOrDefault() - bOffset;

            if (aRemaining != bRemaining)
            {
                return false;
            }

            if (aRemaining < 0)
            {
                return false;
            }

            return string.CompareOrdinal(a, aOffset, b, bOffset, aRemaining) == 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length)
        {
            return (ReferenceEquals(a, b) && aOffset == bOffset && length >= 0)
                || string.CompareOrdinal(a, aOffset, b, bOffset, length) == 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index)
        {
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

                firstCharIndex = firstCharIndex = @this.IndexOf(firstChar, firstCharIndex + 1);
            }

            return false;
        }

        public static string ConcatSubstring(string str0, int startIndex0, int count0, string str1)
        {
            var builder = StringBuilderPool.Get(str1.Length + count0);

            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, string str1, int startIndex1, int count1)
        {
            var builder = StringBuilderPool.Get(str0.Length + count1);

            builder.Append(str0);
            builder.Append(str1, startIndex1, count1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, int startIndex0, int count0, string str1, char c2, string str3, int startIndex3, int count3)
        {
            var builder = StringBuilderPool.Get(count0 + str1.Length + 1 + count3);

            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            builder.Append(c2);
            builder.Append(str3, startIndex3, count3);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, int startIndex0, int count0, string str1, int startIndex1, int count1)
        {
            var builder = StringBuilderPool.Get(count0 + count1);

            builder.Append(str0, startIndex0, count0);
            builder.Append(str1, startIndex1, count1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static bool IsReverseSubset(string s1, string s2)
        {
            var len = s2.Length;
            var index1 = 0;
            var index2 = len - 1;
            while
            (
                len > 0
                &&
                index1 < s1.Length
                &&
                (
                    (s1[index1] == s2[index2])
                    ||
                    (s1[index1] == '.')
                )
            )
            {
                index1++;
                index2--;
                len--;
            }

            return index1 >= s1.Length;
        }

        public static bool IsSubset(string s1, string s2)
        {
            if (s1.Length > s2.Length)
            {
                return false;
            }

            for (var i = 0; i < s1.Length; i++)
            {
                if (s1[i] != '.' && s1[i] != s2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
