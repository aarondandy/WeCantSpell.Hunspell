using System;
using System.Globalization;
using System.Text;
using System.Runtime.InteropServices;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class HunspellTextFunctions
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

        public static bool IsSubset(string s1, ReadOnlySpan<char> s2)
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
            byte state = 0; // 0 = begin, 1 = number, 2 = separator
            for (var i = 0; i < word.Length; i++)
            {
                var c = word[i];
                if (char.IsNumber(c))
                {
                    state = 1;
                }
                else if (c == ',' || c == '.' || c == '-')
                {
                    if (state != 1)
                    {
                        return false;
                    }

                    state = 2;
                }
                else
                {
                    return false;
                }
            }

            return state == 1;
        }

        public static int CountMatchingFromLeft(string text, char character)
        {
            var count = 0;
            for (; count < text.Length && text[count] == character; count++) ;

            return count;
        }

        public static int CountMatchingFromRight(string text, char character)
        {
            var lastIndex = text.Length - 1;
            var searchIndex = lastIndex;
            for (; searchIndex >= 0 && text[searchIndex] == character; searchIndex--) ;

            return lastIndex - searchIndex;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool MyIsAlpha(char ch) => ch < 128 || char.IsLetter(ch);

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

        public static ReadOnlySpan<char> RemoveChars(this ReadOnlySpan<char> @this, CharacterSet chars)
        {
#if DEBUG
            if (chars == null)
            {
                throw new ArgumentNullException(nameof(chars));
            }
#endif

            if (@this.IsEmpty || chars.IsEmpty)
            {
                return @this;
            }

            var removeIndex = @this.IndexOfAny(chars);
            if (removeIndex < 0)
            {
                return @this;
            }

            if (removeIndex == @this.Length - 1)
            {
                return @this.Slice(0, removeIndex);
            }

            var buffer = new char[@this.Length - 1];
            @this.Slice(0, removeIndex).CopyTo(buffer.AsSpan());
            var writeIndex = removeIndex;
            for (var i = removeIndex; i < @this.Length; i++)
            {
                ref readonly var c = ref @this[i];
                if (!chars.Contains(c))
                {
                    buffer[writeIndex++] = c;
                }
            }

            return buffer.AsSpan(0, writeIndex);
        }

        public static string MakeInitCap(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (textInfo == null) throw new ArgumentNullException(nameof(textInfo));
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

        public static string MakeInitCap(ReadOnlySpan<char> s, TextInfo textInfo)
        {
#if DEBUG
            if (textInfo == null) throw new ArgumentNullException(nameof(textInfo));
#endif
            if (s.IsEmpty)
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

            var builder = StringBuilderPool.Get(s);
            builder[0] = expectedFirstLetter;
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string MakeAllCap(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (textInfo == null) throw new ArgumentNullException(nameof(textInfo));
#endif
            return textInfo.ToUpper(s);
        }

        public static ReadOnlySpan<char> MakeTitleCase(ReadOnlySpan<char> s, CultureInfo cultureInfo)
        {
#if DEBUG
            if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));
#endif

            if (s.IsEmpty)
            {
                return s;
            }

            var buffer = new char[s.Length];
            s.Slice(0, 1).ToUpper(buffer.AsSpan(0, 1), cultureInfo);
            if (s.Length > 1)
            {
                s.Slice(1).ToLower(buffer.AsSpan(1), cultureInfo);
            }
            return new ReadOnlySpan<char>(buffer);
        }

        public static ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded, Encoding encoding)
        {
            if (Encoding.UTF8.Equals(encoding))
            {
                return decoded;
            }

            var encodedBytes = encoding.GetBytes(decoded.ToArray());
            return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length).AsSpan();
        }

        public static CapitalizationType GetCapitalizationType(ReadOnlySpan<char> word, TextInfo textInfo)
        {
            if (word.IsEmpty)
            {
                return CapitalizationType.None;
            }

            var hasFoundMoreCaps = false;
            var firstIsUpper = false;
            var hasLower = false;

            for (int i = 0; i < word.Length; i++)
            {
                ref readonly var c = ref word[i];

                if (!hasFoundMoreCaps && char.IsUpper(c))
                {
                    if (i == 0)
                    {
                        firstIsUpper = true;
                    }
                    else
                    {
                        hasFoundMoreCaps = true;
                    }

                    if (hasLower)
                    {
                        break;
                    }
                }
                else if (!hasLower && CharIsNotNeutral(c, textInfo))
                {
                    hasLower = true;
                    if (hasFoundMoreCaps)
                    {
                        break;
                    }
                }
            }

            if (firstIsUpper)
            {
                if (!hasFoundMoreCaps)
                {
                    return CapitalizationType.Init;
                }
                if (!hasLower)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.HuhInit;
            }
            else
            {
                if (!hasFoundMoreCaps)
                {
                    return CapitalizationType.None;
                }
                if (!hasLower)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.Huh;
            }
        }
    }
}
