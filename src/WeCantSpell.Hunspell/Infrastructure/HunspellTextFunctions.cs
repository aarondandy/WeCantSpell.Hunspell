using System;
using System.Globalization;
using System.Text;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class HunspellTextFunctions
    {
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

        internal static bool IsSubset(string s1, StringSlice s2)
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
        public static bool CharIsNeutral(char c, TextInfo textInfo) =>
            (c > 127 && textInfo.ToUpper(c) == c) || !char.IsLower(c);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool CharIsNotNeutral(char c, TextInfo textInfo) =>
            (c < 127 || textInfo.ToUpper(c) != c) && char.IsLower(c);

        public static string RemoveChars(this string @this, CharacterSet chars)
        {
#if DEBUG
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            if (chars == null)
            {
                throw new ArgumentNullException(nameof(chars));
            }
#endif

            if (@this.Length == 0 || chars.IsEmpty)
            {
                return @this;
            }

            var builder = StringBuilderPool.Get(@this);
            builder.RemoveChars(chars);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string MakeInitCap(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (textInfo == null)
            {
                throw new ArgumentNullException(nameof(textInfo));
            }
#endif
            if (s.Length == 0)
            {
                return s;
            }


            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter == actualFirstLetter)
            {
                return s;
            }

            if (s.Length == 1)
            {
                return expectedFirstLetter.ToString();
            }

            var builder = StringBuilderPool.Get(s);
            builder[0] = expectedFirstLetter;
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string MakeInitCap(StringSlice s, TextInfo textInfo)
        {
#if DEBUG
            if (textInfo == null)
            {
                throw new ArgumentNullException(nameof(textInfo));
            }
#endif
            if (s.Length == 0)
            {
                return string.Empty;
            }

            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter == actualFirstLetter)
            {
                return s.ToString();
            }

            if (s.Length == 1)
            {
                return expectedFirstLetter.ToString();
            }

            var builder = StringBuilderPool.Get(s);
            builder[0] = expectedFirstLetter;
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        /// <summary>
        /// Convert to all little.
        /// </summary>
        public static string MakeAllSmall(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (textInfo == null)
            {
                throw new ArgumentNullException(nameof(textInfo));
            }
#endif
            return textInfo.ToLower(s);
        }

        public static string MakeInitSmall(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (textInfo == null)
            {
                throw new ArgumentNullException(nameof(textInfo));
            }
#endif

            if (s.Length == 0)
            {
                return s;
            }

            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToLower(actualFirstLetter);
            if (expectedFirstLetter == actualFirstLetter)
            {
                return s;
            }

            if (s.Length == 1)
            {
                return expectedFirstLetter.ToString();
            }

            var builder = StringBuilderPool.Get(s, s.Length);
            builder[0] = expectedFirstLetter;
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string MakeAllCap(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (textInfo == null)
            {
                throw new ArgumentNullException(nameof(textInfo));
            }
#endif
            return textInfo.ToUpper(s);
        }

        public static string ReDecodeConvertedStringAsUtf8(string decoded, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                return decoded;
            }

            var encodedBytes = encoding.GetBytes(decoded);
            return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);
        }

        public static string ReDecodeConvertedStringAsUtf8(StringSlice decoded, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                return decoded.ToString();
            }
            if (decoded.IsFullString)
            {
                return ReDecodeConvertedStringAsUtf8(decoded.Text, encoding);
            }

            var encodedBytes = new byte[encoding.GetMaxByteCount(decoded.Length)];
            var byteEncodedCount = encoding.GetBytes(decoded.Text, decoded.Offset, decoded.Length, encodedBytes, 0);
            return Encoding.UTF8.GetString(encodedBytes, 0, byteEncodedCount);
        }
    }
}
