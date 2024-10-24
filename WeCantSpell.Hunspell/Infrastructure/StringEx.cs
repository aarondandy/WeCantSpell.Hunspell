using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class StringEx
{
#if HAS_SEARCHVALUES
    private static readonly System.Buffers.SearchValues<char> TabOrSpace = System.Buffers.SearchValues.Create("\t ");
#endif

#if NO_STATIC_STRINGCHAR_METHODS
    public static bool StartsWith(this string @this, char character) => @this.Length != 0 && @this[0] == character;
#endif

    public static bool StartsWith(this ReadOnlySpan<char> @this, string value, StringComparison comparison) => @this.StartsWith(value.AsSpan(), comparison);

    public static bool StartsWith(this ReadOnlySpan<char> @this, char value) => !@this.IsEmpty && @this[0] == value;

    public static bool EndsWith(this string @this, char character) => @this.Length > 0 && @this[@this.Length - 1] == character;

    public static bool EndsWith(this ReadOnlySpan<char> @this, char value) => !@this.IsEmpty && @this[@this.Length - 1] == value;

    public static bool EndsWith(this ReadOnlyMemory<char> @this, char value) => !@this.IsEmpty && @this.Span[@this.Length - 1] == value;

    public static bool Equals(this ReadOnlySpan<char> @this, string value, StringComparison comparison) => @this.Equals(value.AsSpan(), comparison);

    public static bool EqualsOrdinal(this ReadOnlySpan<char> @this, string value) => @this.Equals(value, StringComparison.Ordinal);

    public static bool EqualsOrdinal(this ReadOnlySpan<char> @this, char value) => @this.Length == 1 && @this[0] == value;

    public static bool EqualsOrdinal(this string @this, ReadOnlySpan<char> value) => value.Equals(@this, StringComparison.Ordinal);

    public static bool EqualsOrdinal(this string @this, char value) => @this.Length == 1 && @this[0] == value;

    public static bool EqualsOrdinal(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value) => @this.Equals(value, StringComparison.Ordinal);

    public static bool IsTabOrSpace(this char c) => c is '\t' or ' ';

#if HAS_SEARCHVALUES
    public static int IndexOfTabOrSpace(this ReadOnlySpan<char> span) => span.IndexOfAny(TabOrSpace);
#else
    public static int IndexOfTabOrSpace(this ReadOnlySpan<char> span) => span.IndexOfAny('\t', ' ');
#endif

    public static int IndexOf(this ReadOnlySpan<char> @this, string value, int startIndex, StringComparison comparisonType)
    {
        return @this.IndexOf(value.AsSpan(), startIndex, comparisonType);
    }

    public static int IndexOf(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value, int startIndex, StringComparison comparisonType)
    {
        var result = @this.Slice(startIndex).IndexOf(value, comparisonType);
        return result < 0 ? result : result + startIndex;
    }

#if NO_STRING_CONTAINS

    public static bool Contains(this string @this, char value) => @this.IndexOf(value) >= 0;

    public static bool Contains(this ReadOnlySpan<char> @this, char value) => @this.IndexOf(value) >= 0;

#endif

    public static bool ContainsAny(this string @this, char value0, char value1) => @this.AsSpan().ContainsAny(value0, value1);

#if NO_SPAN_CONTAINSANY

    public static bool ContainsAny(this ReadOnlySpan<char> @this, char value0, char value1) => @this.IndexOfAny(value0, value1) >= 0;

#endif

    public static bool Contains(this List<string> list, ReadOnlySpan<char> value)
    {
        foreach (var item in list)
        {
            if (item is not null && item.AsSpan().SequenceEqual(value))
            {
                return true;
            }

        }

        return false;
    }

    public static ReadOnlySpan<char> AsSpanRemoveFromEnd(this string @this, int toRemove) => @this.AsSpan(0, @this.Length - toRemove);

    public static string ReplaceIntoString(this ReadOnlySpan<char> @this, int index, int removeCount, string replacement)
    {
        if (index == 0)
        {
            return removeCount >= @this.Length
                ? replacement
                : ConcatString(replacement, @this.Slice(removeCount));
        }

        return ConcatString(@this.Slice(0, index), replacement, @this.Slice(index + removeCount));
    }

    public static string ReplaceIntoString(this ReadOnlySpan<char> text, char oldChar, char newChar)
    {
        if (text.IsEmpty)
        {
            return string.Empty;
        }

        var replaceIndex = text.IndexOf(oldChar);
        if (replaceIndex < 0)
        {
            return text.ToString();
        }

        return buildReplaced(replaceIndex, text, oldChar, newChar);

        static string buildReplaced(int replaceIndex, ReadOnlySpan<char> text, char oldChar, char newChar)
        {
            var builder = StringBuilderPool.Get(text);
            builder.Replace(oldChar, newChar, replaceIndex, builder.Length - replaceIndex);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }

    public static string ReplaceIntoString(this ReadOnlySpan<char> text, string oldText, string newText)
    {
        if (text.IsEmpty)
        {
            return string.Empty;
        }

        var replaceIndex = text.IndexOf(oldText.AsSpan());
        if (replaceIndex < 0)
        {
            return text.ToString();
        }

        return buildReplaced(replaceIndex, text, oldText, newText);

        static string buildReplaced(int replaceIndex, ReadOnlySpan<char> text, string oldText, string newText)
        {
            var builder = StringBuilderPool.Get(text);
            builder.Replace(oldText, newText, replaceIndex, builder.Length - replaceIndex);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }

    public static char GetCharOrTerminator(this string @this, int index)
    {
        return index < @this.Length ? @this[index] : '\0';
    }

    public static char GetCharOrTerminator(this Span<char> @this, int index)
    {
        return index < @this.Length ? @this[index] : '\0';
    }

#if NO_STRING_SPAN
    public static string ConcatString(char c, ReadOnlySpan<char> span)
    {
        var builder = StringBuilderPool.Get(span.Length + 1);
        builder.Append(c);

        if (!span.IsEmpty)
        {
            builder.Append(span);
        }

        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(char c, ReadOnlySpan<char> span) => string.Concat([c], span);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(ReadOnlySpan<char> str0, string str1, char char2, ReadOnlySpan<char> str3)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + str3.Length + 1);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(char2);
        builder.Append(str3);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(ReadOnlySpan<char> str0, string str1, char char2, ReadOnlySpan<char> str3) => string.Concat(str0, str1, [char2], str3);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(string str0, ReadOnlySpan<char> str1, string str2, ReadOnlySpan<char> str3)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + str2.Length + str3.Length);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(str2);
        builder.Append(str3);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(string str0, ReadOnlySpan<char> str1, string str2, ReadOnlySpan<char> str3) => string.Concat(str0, str1, str2, str3);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(string str0, string str1, string str2, ReadOnlySpan<char> str3)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + str2.Length + str3.Length);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(str2);
        builder.Append(str3);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(string str0, string str1, string str2, ReadOnlySpan<char> str3) => string.Concat(str0, str1, str2, str3);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(ReadOnlySpan<char> str0, string str1, ReadOnlySpan<char> str2)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + str2.Length);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(str2);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatString(ReadOnlySpan<char> str0, string str1, ReadOnlySpan<char> str2) => string.Concat(str0, str1, str2);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(ReadOnlySpan<char> str0, char char1, ReadOnlySpan<char> str2)
    {
        var builder = StringBuilderPool.Get(str0.Length + str2.Length + 1);
        builder.Append(str0);
        builder.Append(char1);
        builder.Append(str2);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(ReadOnlySpan<char> str0, char char1, ReadOnlySpan<char> str2) => string.Concat(str0, [char1], str2);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(this ReadOnlySpan<char> @this, char value)
    {
        var builder = StringBuilderPool.Get(@this.Length + 1);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(this ReadOnlySpan<char> @this, char value) => string.Concat(@this, [value]);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(this string @this, char value)
    {
        var builder = StringBuilderPool.Get(@this.Length + 1);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    public static string ConcatString(this string @this, char value) => string.Concat(@this, [value]);
#endif

#if NO_STRING_SPAN
    public static string ConcatString(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value)
    {
        if (@this.IsEmpty)
        {
            return value.ToString();
        }

        if (value.IsEmpty)
        {
            return @this.ToString();
        }

        var builder = StringBuilderPool.Get(@this.Length + value.Length);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatString(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value) => string.Concat(@this, value);
#endif

    public static string ConcatString(this ReadOnlySpan<char> @this, string value) => @this.IsEmpty ? value : ConcatString(@this, value.AsSpan());

    public static string ConcatString(this string @this, ReadOnlySpan<char> value) => value.IsEmpty ? @this : ConcatString(@this.AsSpan(), value);

    public static ReadOnlySpan<char> ConcatSpan(this ReadOnlySpan<char> @this, string value)
    {
        if (@this.IsEmpty)
        {
            return value.AsSpan();
        }

        if (value.Length == 0)
        {
            return @this;
        }

        return ConcatString(@this, value.AsSpan()).AsSpan();
    }

    public static ReadOnlySpan<char> ConcatSpan(this string @this, ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return @this.AsSpan();
        }

        if (@this.Length == 0)
        {
            return value;
        }

        return ConcatString(@this.AsSpan(), value).AsSpan();
    }

#if NO_SPAN_HASHCODE

    public static int GetHashCode(ReadOnlySpan<char> value)
    {
        var hash = 5381;

        for (var i = 1; i < value.Length; i += 2)
        {
            hash = unchecked((hash << 5) ^ ((value[i] << 16) + value[i - 1]));
        }

        if ((value.Length & 1) != 0)
        {
            hash = unchecked((hash << 5) ^ value[value.Length - 1]);
        }

        return hash;
    }

    public static int GetHashCode(string value)
    {
        var hash = 5381;

        for (var i = 1; i < value.Length; i+= 2)
        {
            hash = unchecked((hash << 5) ^ ((value[i] << 16) + value[i - 1]));
        }

        if ((value.Length & 1) != 0)
        {
            hash = unchecked((hash << 5) ^ value[value.Length - 1]);
        }

        return hash;
    }

#endif

    public static string ToStringWithoutChars(this ReadOnlySpan<char> text, char value)
    {
        if (text.IsEmpty)
        {
            return string.Empty;
        }

        var index = text.IndexOf(value);
        if (index < 0)
        {
            return text.ToString();
        }

        if (index == text.Length - 1)
        {
            return text.Slice(0, index).ToString();
        }

        var builder = StringBuilderPool.Get(text.Length - 1);

        do
        {
            builder.Append(text.Slice(0, index));
            text = text.Slice(index + 1);
        }
        while ((index = text.IndexOf(value)) >= 0);

        builder.Append(text);

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ToStringReversed(this ReadOnlySpan<char> @this)
    {
        if (@this.Length <= 1)
        {
            return @this.ToString();
        }

        var builder = StringBuilderPool.Get(@this.Length);
        for (var i = @this.Length - 1; i >= 0; i--)
        {
            builder.Append(@this[i]);
        }

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string GetReversed(this string @this)
    {
        if (@this is not { Length: > 1 })
        {
            return @this;
        }

        var builder = StringBuilderPool.Get(@this.Length);
        for (var i = @this.Length - 1; i >= 0; i--)
        {
            builder.Append(@this[i]);
        }

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static SpanSeparatorSplitEnumerator<char> SplitOnComma(this ReadOnlySpan<char> @this, StringSplitOptions options = StringSplitOptions.None) => new(@this, options, static span => span.IndexOf(','));

    public static SpanSeparatorSplitEnumerator<char> SplitOnTabOrSpace(this ReadOnlySpan<char> @this) => new(@this, StringSplitOptions.RemoveEmptyEntries, static span => span.IndexOfTabOrSpace());

#if NO_STATIC_STRINGCHAR_METHODS
    public static string Join(char seperator, string[] items)
    {
        if (items.Length == 0)
        {
            return string.Empty;
        }

        if (items.Length == 1)
        {
            return items[0];
        }

        return joinUsingBuilder(seperator, items);

        static string joinUsingBuilder(char seperator, string[] items)
        {
            var requiredCapacity = items.Length - 1;
            int i;
            for (i = 0; i < items.Length; i++)
            {
                requiredCapacity += items[i].Length;
            }

            var builder = StringBuilderPool.Get(requiredCapacity);
            builder.Append(items[0]);

            for (i = 1; i < items.Length; i++)
            {
                builder.Append(seperator);
                builder.Append(items[i]);
            }

            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }
#endif

}
