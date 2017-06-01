using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class StringEx
    {
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character) => @this.Length != 0 && @this[0] == character;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character) => @this.Length != 0 && @this[@this.Length - 1] == character;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this) => @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsNullOrWhiteSpace(string value)
        {
#if NO_STRINGISNULLORWHITESPACE
            if (value != null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
#else
            return string.IsNullOrWhiteSpace(value);
#endif
        }

        public static StringSlice[] SliceOnTabOrSpace(this string @this)
        {
            var parts = new List<StringSlice>();

            int startIndex = 0;
            int splitIndex;
            int partLength;
            while ((splitIndex = IndexOfTabOrSpace(@this, startIndex)) >= 0)
            {
                partLength = splitIndex - startIndex;
                if (partLength > 0)
                {
                    parts.Add(new StringSlice
                    {
                        Text = @this,
                        Offset = startIndex,
                        Length = partLength
                    });
                }

                startIndex = splitIndex + 1;
            }

            partLength = @this.Length - startIndex;
            if (partLength > 0)
            {
                parts.Add(new StringSlice
                {
                    Text = @this,
                    Offset = startIndex,
                    Length = partLength
                });
            }

            return parts.ToArray();
        }

        private static int IndexOfTabOrSpace(string text, int startIndex)
        {
            for (var i = startIndex; i < text.Length; i++)
            {
                var c = text[i];
                if (c == ' ' || c == '\t')
                {
                    return i;
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

        public static string RemoveChars(this string @this, CharacterSet chars)
        {
            if (string.IsNullOrEmpty(@this) || chars == null || chars.IsEmpty)
            {
                return @this;
            }

            var builder = StringBuilderPool.Get(@this);
            builder.RemoveChars(chars);
            return StringBuilderPool.GetStringAndReturn(builder);
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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length) =>
            (
                aOffset == bOffset
                &&
                ReferenceEquals(a, b)
                &&
                length >= 0
            )
            ||
            string.CompareOrdinal(a, aOffset, b, bOffset, length) == 0;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index) => index < @this.Length ? @this[index] : '\0';

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

        public static string ConcatSubstring(string str0, int startIndex0, int length0, string str1, char char2, string str3, int startIndex3)
        {
            var length3 = str3.Length - startIndex3;
            var builder = StringBuilderPool.Get(length0 + str1.Length + 1 + length3);

            builder.Append(str0, startIndex0, length0);
            builder.Append(str1);
            builder.Append(char2);
            builder.Append(str3, startIndex3, length3);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, string str1, int startIndex1)
        {
            var length1 = str1.Length - startIndex1;
            var builder = StringBuilderPool.Get(str0.Length + length1);

            builder.Append(str0);
            builder.Append(str1, startIndex1, length1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, int startIndex0, int length0, string str1, int startIndex1)
        {
            var length1 = str1.Length - startIndex1;
            var builder = StringBuilderPool.Get(length0 + length1);

            builder.Append(str0, startIndex0, length0);
            builder.Append(str1, startIndex1, length1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, int startIndex0, int length0, string str1, string str2, int startIndex2)
        {
            var length2 = str2.Length - startIndex2;
            var builder = StringBuilderPool.Get(length0 + str1.Length + length2);

            builder.Append(str0, startIndex0, length0);
            builder.Append(str1);
            builder.Append(str2, startIndex2, length2);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatSubstring(string str0, int startIndex0, int length0, char char1)
        {
            var builder = StringBuilderPool.Get(length0 + 1);

            builder.Append(str0, startIndex0, length0);
            builder.Append(char1);

            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static bool IsReverseSubset(string s1, string s2)
        {
            if (s2.Length < s1.Length)
            {
                return false;
            }

            for (int index1 = 0, index2 = s2.Length - 1; index1 < s1.Length; index1++, index2--)
            {
                if (s1[index1] != '.' && s1[index1] != s2[index2])
                {
                    return false;
                }
            }

            return true;
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

        public static bool IsNumericWord(string word)
        {
            int i;
            byte state = 0; // 0 = begin, 1 = number, 2 = separator
            for (i = 0; i < word.Length; i++)
            {
                var c = word[i];
                if (char.IsNumber(c))
                {
                    state = 1;
                }
                else if (c == ',' || c == '.' || c == '-')
                {
                    if (state == 2 || i == 0)
                    {
                        break;
                    }

                    state = 2;
                }
                else
                {
                    break;
                }
            }

            return i == word.Length && state == 1;
        }

        public static int CountMatchingFromLeft(string text, char character)
        {
            var count = 0;
            while (count < text.Length && text[count] == character)
            {
                count++;
            }

            return count;
        }

        public static int CountMatchingFromRight(string text, char character)
        {
            var lastIndex = text.Length - 1;
            var searchIndex = lastIndex;
            while (searchIndex >= 0 && text[searchIndex] == character)
            {
                searchIndex--;
            }

            return lastIndex - searchIndex;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool MyIsAlpha(char ch) => ch < 128 || char.IsLetter(ch);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static StringSlice Subslice(this string text, int offset, int length) =>
            new StringSlice
            {
                Text = text,
                Offset = offset,
                Length = length
            };

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static StringSlice Subslice(this string text, int offset) =>
            new StringSlice
            {
                Text = text,
                Offset = offset,
                Length = text.Length - offset
            };

        internal static bool Contains(this List<string> values, StringSlice test)
        {
            foreach (var value in values)
            {
                if (test.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
