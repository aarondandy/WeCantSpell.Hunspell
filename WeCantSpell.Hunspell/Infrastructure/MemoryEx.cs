using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
    public delegate bool SplitPartHandler(ReadOnlySpan<char> part, int index);

    public static int IndexOf(this ReadOnlySpan<char> @this, char value, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        if (result >= 0)
        {
            result += startIndex;
        }

        return result;
    }

    public static int IndexOfAny(this ReadOnlySpan<char> @this, char value0, char value1, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOfAny(value0, value1);
        if (result >= 0)
        {
            result += startIndex;
        }

        return result;
    }

    public static int IndexOfAny(this ReadOnlySpan<char> @this, CharacterSet chars)
    {
        if (chars.HasItems)
        {
            if (chars.Count == 1)
            {
                return @this.IndexOf(chars[0]);
            }

            for (var searchLocation = 0; searchLocation < @this.Length; searchLocation++)
            {
                if (chars.Contains(@this[searchLocation]))
                {
                    return searchLocation;
                }
            }
        }

        return -1;
    }

    public static bool Equals(this ReadOnlySpan<char> @this, string value, StringComparison comparison) => @this.Equals(value.AsSpan(), comparison);

    public static bool EqualsOrdinal(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value) => @this.Equals(value, StringComparison.Ordinal);

    public static bool EqualsLimited(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value, int lengthLimit) =>
        @this.Limit(lengthLimit).EqualsOrdinal(value.Limit(lengthLimit));

    public static bool ContainsAny(this ReadOnlySpan<char> @this, char value0, char value1) => @this.IndexOfAny(value0, value1) >= 0;

    public static bool Contains(this ReadOnlySpan<char> @this, char value) => @this.IndexOf(value) >= 0;

    public static bool Contains(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value) => @this.IndexOf(value) >= 0;

    public static bool StartsWith(this ReadOnlySpan<char> @this, string value, StringComparison comparison) => @this.StartsWith(value.AsSpan(), comparison);

    public static bool StartsWith(this ReadOnlySpan<char> @this, char value) => !@this.IsEmpty && @this[0] == value;

    public static bool EndsWith(this ReadOnlySpan<char> @this, char value) => !@this.IsEmpty && @this[@this.Length - 1] == value;

    public static bool SplitOnComma(this ReadOnlySpan<char> @this, SplitPartHandler partHandler)
    {
        int partIndex = 0;
        int startIndex = 0;
        int partLength;
        while (@this.IndexOf(',', startIndex) is { } commaIndex and >= 0)
        {
            partLength = commaIndex - startIndex;
            if (partLength > 0)
            {
                if (!partHandler(@this.Slice(startIndex, partLength), partIndex))
                {
                    return false;
                }

                partIndex++;
            }

            startIndex = commaIndex + 1;
        }

        partLength = @this.Length - startIndex;
        return partLength > 0
            && partHandler(@this.Slice(startIndex, partLength), partIndex);
    }

    public static bool Split(this ReadOnlySpan<char> @this, char value0, char value1, SplitPartHandler partHandler)
    {
        int partIndex = 0;
        int startIndex = 0;
        int partLength;
        while (@this.IndexOfAny(value0, value1, startIndex) is { } commaIndex and >= 0)
        {
            partLength = commaIndex - startIndex;
            if (partLength > 0)
            {
                if (!partHandler(@this.Slice(startIndex, partLength), partIndex++))
                {
                    return false;
                }
            }

            startIndex = commaIndex + 1;
        }

        partLength = @this.Length - startIndex;
        return partLength > 0
            && partHandler(@this.Slice(startIndex, partLength), partIndex);
    }

    public static bool SplitOnTabOrSpace(this ReadOnlySpan<char> @this, SplitPartHandler partHandler) => @this.Split(' ', '\t', partHandler);

    public static string Without(this ReadOnlySpan<char> @this, char value)
    {
        var removeIndex = @this.IndexOf(value);
        if (removeIndex < 0)
        {
            return @this.ToString();
        }

        if (removeIndex == @this.Length - 1)
        {
            return @this.Slice(0, removeIndex).ToString();
        }

        var builder = StringBuilderPool.Get(@this.Length - 1);
        builder.Append(@this.Slice(0, removeIndex));

        for (var i = removeIndex; i < @this.Length; i++)
        {
            ref readonly var c = ref @this[i];
            if (value != c)
            {
                builder.Append(c);
            }
        }

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static ReadOnlySpan<char> Replace(this ReadOnlySpan<char> @this, char oldChar, char newChar)
    {
        var replaceIndex = @this.IndexOf(oldChar);
        if (replaceIndex < 0)
        {
            return @this;
        }
        
        var builder = StringBuilderPool.Get(@this.Length);
        builder.Append(@this.Slice(0, replaceIndex));
        builder.Append(newChar);
        for (var i = replaceIndex + 1; i < @this.Length; i++)
        {
            ref readonly var c = ref @this[i];
            builder.Append((c == oldChar) ? newChar : c);
        }

        return StringBuilderPool.GetStringAndReturn(builder).AsSpan();
    }

    public static ReadOnlySpan<char> Replace(this ReadOnlySpan<char> @this, string oldText, string newText)
    {
        var replaceIndex = @this.IndexOf(oldText.AsSpan());
        if (replaceIndex < 0)
        {
            return @this;
        }

        // TODO: use replaceIndex to optimize

        return @this.ToString().Replace(oldText, newText).AsSpan();
    }

    public static ReadOnlySpan<char> Reversed(this ReadOnlySpan<char> @this)
    {
        if (@this.Length <= 1)
        {
            return @this;
        }

        var chars = new char[@this.Length];
        var lastIndex = @this.Length - 1;
        for (var i = 0; i < chars.Length; i++)
        {
            chars[i] = @this[lastIndex - i];
        }

        return new ReadOnlySpan<char>(chars);
    }

    public static ReadOnlySpan<char> Limit(this ReadOnlySpan<char> @this, int maxLength)
    {
#if DEBUG
        if (maxLength < 0) throw new ArgumentOutOfRangeException(nameof(maxLength));
#endif
        return @this.Length > maxLength ? @this.Slice(0, maxLength) : @this;
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

    public static string ConcatString(this ReadOnlySpan<char> @this, char value) => @this.ConcatString(value.ToString());
}
