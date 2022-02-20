﻿using System;
using System.Globalization;
using System.Text;
using System.Buffers;
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
#if DEBUG
            if (s1 == null) throw new ArgumentNullException(nameof(s1));
            if (s2 == null) throw new ArgumentNullException(nameof(s2));
#endif
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
#if DEBUG
            if (s1 == null) throw new ArgumentNullException(nameof(s1));
            if (s2 == null) throw new ArgumentNullException(nameof(s2));
#endif
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
#if DEBUG
            if (s1 == null) throw new ArgumentNullException(nameof(s1));
#endif
            if (s1.Length > s2.Length)
            {
                return false;
            }

            var s1Span = s1.AsSpan();
            for (var i = 0; i < s1Span.Length; i++)
            {
                ref readonly var s1c = ref s1Span[i];
                if (s1c != '.' && s1c != s2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsNumericWord(string word)
        {
#if DEBUG
            if (word == null) throw new ArgumentNullException(nameof(word));
#endif
            byte state = 0; // 0 = begin, 1 = number, 2 = separator
            var wordSpan = word.AsSpan();
            for (var i = 0; i < wordSpan.Length; i++)
            {
                ref readonly var c = ref wordSpan[i];
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

        public static string WithoutChars(this string @this, CharacterSet chars)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (chars == null) throw new ArgumentNullException(nameof(chars));
#endif

            if (@this.Length == 0 || chars.IsEmpty)
            {
                return @this;
            }

            var thisSpan = @this.AsSpan();
            var index = thisSpan.IndexOfAny(chars);
            if (index < 0)
            {
                return @this;
            }

            var lastIndex = thisSpan.Length - 1;
            if (index == lastIndex)
            {
                return @this.Substring(0, lastIndex);
            }

            var builder = StringBuilderPool.Get(lastIndex);
            builder.Append(thisSpan.Slice(0, index));
            index++;
            for (; index < thisSpan.Length; index++)
            {
                ref readonly var c = ref thisSpan[index];
                if (!chars.Contains(c))
                {
                    builder.Append(c);
                }
            }

            return StringBuilderPool.GetStringAndReturn(builder);
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

            var builder = StringBuilderPool.Get(s.Length);
            builder.Append(expectedFirstLetter);
            if (s.Length > 1)
            {
                builder.Append(s, 1, s.Length - 1);
            }
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

            ref readonly var actualFirstLetter = ref s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter == actualFirstLetter)
            {
                return s.ToString();
            }

            var builder = StringBuilderPool.Get(s.Length);
            builder.Append(expectedFirstLetter);
            if (s.Length > 1)
            {
                builder.Append(s.Slice(1));
            }
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        /// <summary>
        /// Convert to all little.
        /// </summary>
        public static string MakeAllSmall(string s, TextInfo textInfo)
        {
#if DEBUG
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (textInfo == null) throw new ArgumentNullException(nameof(textInfo));
#endif
            return textInfo.ToLower(s);
        }

        public static string MakeInitSmall(string s, TextInfo textInfo)
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
            var expectedFirstLetter = textInfo.ToLower(actualFirstLetter);
            if (expectedFirstLetter == actualFirstLetter)
            {
                return s;
            }

            var builder = StringBuilderPool.Get(s.Length);
            builder.Append(expectedFirstLetter);
            if (s.Length > 1)
            {
                builder.Append(s, 1, s.Length - 1);
            }
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

        public static string MakeTitleCase(string s, CultureInfo cultureInfo)
        {
#if DEBUG
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));
#endif

            if (s.Length == 0)
            {
                return s;
            }

            using (var mo = MemoryPool<char>.Shared.Rent(s.Length))
            {
                var sSpan = s.AsSpan();
                var buffer = mo.Memory.Span.Slice(0, sSpan.Length);
                sSpan.Slice(0, 1).ToUpper(buffer.Slice(0, 1), cultureInfo);
                if (sSpan.Length > 1)
                {
                    sSpan.Slice(1).ToLower(buffer.Slice(1), cultureInfo);
                }

                return buffer.ToString();
            }
        }

        public static ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded, Encoding encoding)
        {
            if (Encoding.UTF8.Equals(encoding))
            {
                return decoded;
            }

            byte[] encodedBytes;
            int encodedBytesCount;

            unsafe
            {
                fixed (char* decodedPointer = &MemoryMarshal.GetReference(decoded))
                {
                    encodedBytes = new byte[Encoding.UTF8.GetByteCount(decodedPointer, decoded.Length)];
                    fixed (byte* encodedBytesPointer = &encodedBytes[0])
                    {
                        encodedBytesCount = encoding.GetBytes(decodedPointer, decoded.Length, encodedBytesPointer, encodedBytes.Length);
                    }
                }
            }

            return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytesCount).AsSpan();
        }

        public static CapitalizationType GetCapitalizationType(string word, TextInfo textInfo) =>
            GetCapitalizationType(word.AsSpan(), textInfo);

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
