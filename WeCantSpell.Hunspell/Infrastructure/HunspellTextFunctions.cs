﻿using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class HunspellTextFunctions
{
    public static bool IsReverseSubset(string s1, ReadOnlySpan<char> s2)
    {
        return s1.Length <= s2.Length && check(s1.AsSpan(), s2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool check(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
            var s2LastIndex = s2.Length - 1;

            for (var i = 0; i < s1.Length; i++)
            {
                var c = s1[i];
                if (c != '.' && s2[s2LastIndex - i] != c)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static bool IsSubset(string s1, ReadOnlySpan<char> s2)
    {
        return s1.Length <= s2.Length && check(s1.AsSpan(), s2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool check(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
            for (var i = 0; i < s1.Length; i++)
            {
                var c = s1[i];
                if (c != '.' && s2[i] != c)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static bool IsNumericWord(ReadOnlySpan<char> word)
    {
        byte state = 0; // 0 = begin, 1 = number, 2 = separator
        foreach (var c in word)
        {
            if (char.IsNumber(c))
            {
                state = 1;
            }
            else if (c is ',' or '.' or '-')
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

    public static int CountMatchingFromLeft(ReadOnlySpan<char> text, char character)
    {
        var count = 0;
        for (; count < text.Length && text[count] == character; count++) ;

        return count;
    }

    public static int CountMatchingFromRight(string text, char character)
    {
        var searchIndex = text.Length - 1;
        for (; searchIndex >= 0 && text[searchIndex] == character; searchIndex--) ;

        return text.Length - searchIndex - 1;
    }

    public static int CountMatchingFromRight(ReadOnlySpan<char> text, char character)
    {
        var searchIndex = text.Length - 1;
        for (; searchIndex >= 0 && text[searchIndex] == character; searchIndex--) ;

        return text.Length - searchIndex - 1;
    }

    /// <summary>
    /// This is a character class function used within Hunspell to determine if a character is an ASCII letter or something else.
    /// </summary>
    /// <param name="ch">The character value to check.</param>
    /// <returns><c>true</c> is a given character is an ASCII letter or something else.</returns>
    public static bool MyIsAlpha(char ch) => ch is (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or >= (char)128;

    public static string MakeInitCap(string s, TextInfo textInfo)
    {
        if (s.Length > 0)
        {
            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter != actualFirstLetter)
            {
                return StringEx.ConcatString(expectedFirstLetter, s.AsSpan(1));
            }
        }

        return s;
    }

    public static ReadOnlySpan<char> MakeInitCap(ReadOnlySpan<char> s, TextInfo textInfo)
    {
        if (!s.IsEmpty)
        {
            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter != actualFirstLetter)
            {
                return StringEx.ConcatString(expectedFirstLetter, s.Slice(1)).AsSpan();
            }
        }

        return s;
    }

    /// <summary>
    /// Convert to all little.
    /// </summary>
    public static string MakeAllSmall(string s, TextInfo textInfo) => textInfo.ToLower(s);

    public static string MakeInitSmall(string s, TextInfo textInfo)
    {
        if (s.Length != 0)
        {
            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToLower(actualFirstLetter);
            if (expectedFirstLetter != actualFirstLetter)
            {
                return StringEx.ConcatString(expectedFirstLetter, s.AsSpan(1));
            }
        }

        return s;
    }

    public static string MakeAllCap(string s, TextInfo textInfo) => textInfo.ToUpper(s);

    public static string MakeTitleCase(string s, CultureInfo cultureInfo)
    {
        if (s.Length != 0)
        {
            var builder = StringBuilderPool.Get(cultureInfo.TextInfo.ToLower(s));
            builder[0] = cultureInfo.TextInfo.ToUpper(s[0]);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        return s;
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

        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i];

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
            else if (!hasLower && charIsNotNeutral(c, textInfo))
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

        static bool charIsNotNeutral(char c, TextInfo textInfo) => c < 128
            ? c is >= 'a' and <= 'z' // For ASCII, only the a-z range needs to be checked
            : (char.IsLower(c) && textInfo.ToUpper(c) != c); // Outside ASCII, use the framework combined with the uppercase thing
    }
}
