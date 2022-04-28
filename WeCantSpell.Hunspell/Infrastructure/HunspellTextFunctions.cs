using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class HunspellTextFunctions
{
    public static bool IsReverseSubset(string s1, ReadOnlySpan<char> s2)
    {
        return s1.Length <= s2.Length && isReverseSubset(s1.AsSpan(), s2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool isReverseSubset(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
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

    public static bool IsReverseSubsetIgnoringWildcards(string s1, ReadOnlySpan<char> s2)
    {
        return s1.Length <= s2.Length && isReverseSubset(s1.AsSpan(), s2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool isReverseSubset(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
            var s2LastIndex = s2.Length - 1;

            for (var i = 0; i < s1.Length; i++)
            {
                if (s2[s2LastIndex - i] != s1[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static bool IsSubset(string s1, ReadOnlySpan<char> s2)
    {
        return s1.Length <= s2.Length && isSubset(s1.AsSpan(), s2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool isSubset(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
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

    public static bool IsSubsetIgnoringWildcards(string s1, ReadOnlySpan<char> s2) =>
        s1.Length <= s2.Length && s1.AsSpan().Equals(s2.Slice(0, s1.Length), StringComparison.Ordinal);

    public static bool IsNumericWord(ReadOnlySpan<char> word)
    {
        byte state = 0; // 0 = begin, 1 = number, 2 = separator
        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i];
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
        var lastIndex = text.Length - 1;
        var searchIndex = lastIndex;
        for (; searchIndex >= 0 && text[searchIndex] == character; searchIndex--) ;

        return lastIndex - searchIndex;
    }

    public static int CountMatchingFromRight(ReadOnlySpan<char> text, char character)
    {
        var lastIndex = text.Length - 1;
        var searchIndex = lastIndex;
        for (; searchIndex >= 0 && text[searchIndex] == character; searchIndex--) ;

        return lastIndex - searchIndex;
    }

    /// <summary>
    /// This is a character class function used within Hunspell to determine if a character is an ASCII letter.
    /// </summary>
    /// <param name="ch">The character value to check.</param>
    /// <returns><c>true</c> is a given character is an ASCII letter.</returns>
    public static bool MyIsAlpha(char ch) => ch >= 128 || char.IsLetter(ch);

    public static string MakeInitCap(string s, TextInfo textInfo)
    {
        if (s.Length > 0)
        {
            var actualFirstLetter = s[0];
            var expectedFirstLetter = textInfo.ToUpper(actualFirstLetter);
            if (expectedFirstLetter != actualFirstLetter)
            {
                return ReplaceFirstLetter(expectedFirstLetter, s.AsSpan());
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
                return ReplaceFirstLetter(expectedFirstLetter, s).AsSpan();
            }
        }

        return s;
    }

    private static string ReplaceFirstLetter(char firstLetter, ReadOnlySpan<char> baseText)
    {
        var builder = StringBuilderPool.Get(baseText.Length);
        builder.Append(firstLetter);

        if (baseText.Length > 1)
        {
            builder.Append(baseText.Slice(1));
        }

        return StringBuilderPool.GetStringAndReturn(builder);
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
                return ReplaceFirstLetter(expectedFirstLetter, s.AsSpan());
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

        static bool charIsNotNeutral(char c, TextInfo textInfo) => (c < 127 || textInfo.ToUpper(c) != c) && char.IsLower(c);
    }
}
