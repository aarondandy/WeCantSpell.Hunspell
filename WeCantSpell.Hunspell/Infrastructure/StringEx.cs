using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class StringEx
{
    private static readonly char[] SpaceOrTab = { ' ', '\t' };

    public static bool StartsWith(this string @this, char character) => @this.Length != 0 && @this[0] == character;

    public static bool EndsWith(this string @this, char character) => @this.Length > 0 && @this[@this.Length - 1] == character;

    public static bool IsTabOrSpace(this char c) => c is ' ' or '\t';

    public static int IndexOfTabOrSpace(this ReadOnlySpan<char> span) => span.IndexOfAny(' ', '\t');

    public static ReadOnlySpan<char> GetReversed(this ReadOnlySpan<char> @this)
    {
        if (@this is not { Length: > 1 })
        {
            return @this;
        }

        var buffer = new char[@this.Length];
        var lastIndex = @this.Length - 1;
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] = @this[lastIndex - i];
        }

        return buffer.AsSpan();
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

#if NO_STRING_CONTAINS
    public static bool Contains(this string @this, char value) => @this.IndexOf(value) >= 0;
#endif

    public static string ReplaceIntoString(this ReadOnlySpan<char> @this, int index, int removeCount, string replacement)
    {
        var builder = StringBuilderPool.Get(Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
        builder.Append(@this);
        builder.Replace(index, removeCount, replacement);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static char GetCharOrTerminator(this string @this, int index)
    {
#if DEBUG
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
#endif
        return index < @this.Length ? @this[index] : '\0';
    }

    public static string ConcatString(ReadOnlySpan<char> str0, string str1, char char2, ReadOnlySpan<char> str3)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + 1 + str3.Length);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(char2);
        builder.Append(str3);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ConcatString(ReadOnlySpan<char> str0, string str1, ReadOnlySpan<char> str2)
    {
        var builder = StringBuilderPool.Get(str0.Length + str1.Length + str2.Length);
        builder.Append(str0);
        builder.Append(str1);
        builder.Append(str2);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ConcatString(ReadOnlySpan<char> str0, char char1, ReadOnlySpan<char> str2)
    {
        var builder = StringBuilderPool.Get(str0.Length + 1 + str2.Length);
        builder.Append(str0);
        builder.Append(char1);
        builder.Append(str2);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ConcatString(this ReadOnlySpan<char> @this, char value)
    {
        var builder = StringBuilderPool.Get(@this.Length + 1);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ConcatString(this ReadOnlySpan<char> @this, string value)
    {
#if DEBUG
        if (value is null) throw new ArgumentNullException(nameof(value));
#endif
        if (@this.IsEmpty)
        {
            return value;
        }
        if (value.Length == 0)
        {
            return @this.ToString();
        }

        var builder = StringBuilderPool.Get(@this.Length + value.Length);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ConcatString(this string @this, ReadOnlySpan<char> value)
    {
        if (@this.Length == 0)
        {
            return value.ToString();
        }

        if (value.IsEmpty)
        {
            return @this;
        }

        var builder = StringBuilderPool.Get(@this.Length + value.Length);
        builder.Append(@this);
        builder.Append(value);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string WithoutIndex(this string @this, int index)
    {
#if DEBUG
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
        if (index >= @this.Length) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if (index == 0)
        {
            return @this.Substring(1);
        }

        var lastIndex = @this.Length - 1;
        if (index == lastIndex)
        {
            return @this.Substring(0, lastIndex);
        }

        var builder = StringBuilderPool.Get(lastIndex);
        builder.Append(@this, 0, index);
        builder.Append(@this, index + 1, lastIndex - index);
        return StringBuilderPool.GetStringAndReturn(builder);
    }

#if NO_SPAN_HASHCODE
    public static int GetHashCode(ReadOnlySpan<char> value)
    {
        int hash = 5381;
        while (value.Length >= 2)
        {
            hash = unchecked((hash << 5) ^ ((value[1] << 16) + value[0]));
            value = value.Slice(2);
        }

        if (!value.IsEmpty)
        {
            hash = unchecked((hash << 5) ^ value[0]);
        }

        return hash;
    }
#endif
}
