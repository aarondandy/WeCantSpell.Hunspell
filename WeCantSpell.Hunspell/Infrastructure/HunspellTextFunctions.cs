using System;
using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure;

static class HunspellTextFunctions
{
    public static bool IsReverseSubset(string s1, string s2) => IsReverseSubset(s1.AsSpan(), s2.AsSpan());

    public static bool IsReverseSubset(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
    {
        if (s1.Length > s2.Length)
        {
            return false;
        }

        for (int index1 = 0, index2 = s2.Length - 1; index1 < s1.Length; index1++, index2--)
        {
            ref readonly var s1c = ref s1[index1];
            if (s1c != '.' && s1c != s2[index2])
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsSubset(string s1, string s2) => IsSubset(s1.AsSpan(), s2.AsSpan());

    public static bool IsSubset(string s1, ReadOnlySpan<char> s2) => IsSubset(s1.AsSpan(), s2);

    public static bool IsSubset(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
    {
        if (s1.Length > s2.Length)
        {
            return false;
        }

        for (var i = 0; i < s1.Length; i++)
        {
            ref readonly var s1c = ref s1[i];
            if (s1c != '.' && s1c != s2[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsNumericWord(string word)
    {
        byte state = 0; // 0 = begin, 1 = number, 2 = separator
        var wordSpan = word.AsSpan();
        for (var i = 0; i < wordSpan.Length; i++)
        {
            ref readonly var c = ref wordSpan[i];
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

    public static int CountMatchingFromRight(string text, char character)
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


    public static string WithoutChars(this string @this, CharacterSet chars)
    {
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

        return ReplaceFirstLetter(expectedFirstLetter, s.AsSpan());
    }

    public static string MakeInitCap(ReadOnlySpan<char> s, TextInfo textInfo)
    {
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

        return ReplaceFirstLetter(expectedFirstLetter, s);
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

        return ReplaceFirstLetter(expectedFirstLetter, s.AsSpan());
    }

    public static string MakeAllCap(string s, TextInfo textInfo) => textInfo.ToUpper(s);

    public static string MakeTitleCase(string s, CultureInfo cultureInfo)
    {
        if (s.Length == 0)
        {
            return s;
        }

        var builder = StringBuilderPool.Get(cultureInfo.TextInfo.ToLower(s));
        builder[0] = cultureInfo.TextInfo.ToUpper(s[0]);
        return StringBuilderPool.GetStringAndReturn(builder);
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
