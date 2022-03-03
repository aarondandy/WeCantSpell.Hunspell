using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
    public static int IndexOf(this ReadOnlySpan<char> @this, char value, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOf(value);
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

    public static SpanSeparatorSplitEnumerator<char> SplitOnComma(this ReadOnlySpan<char> @this, StringSplitOptions options = StringSplitOptions.None) => new(@this, options, static span => span.IndexOf(','));

    public static SpanSeparatorSplitEnumerator<char> SplitOnTabOrSpace(this ReadOnlySpan<char> @this) => new(@this, StringSplitOptions.RemoveEmptyEntries, static span => span.IndexOfAny(' ', '\t'));

    public ref struct SpanSeparatorSplitEnumerator<T> where T : IEquatable<T>
    {
        public delegate int FindNextSeparator(ReadOnlySpan<T> text);

        public SpanSeparatorSplitEnumerator(ReadOnlySpan<T> span, StringSplitOptions options, FindNextSeparator findNextSeparator)
        {
#if DEBUG
            if (options != StringSplitOptions.None && options != StringSplitOptions.RemoveEmptyEntries)
            {
                throw new ArgumentOutOfRangeException(nameof(options));
            }
#endif

            _span = span;
            _options = options;
            _findNextSeparator = findNextSeparator;
        }

        private ReadOnlySpan<T> _span;
        private readonly StringSplitOptions _options;
        private readonly FindNextSeparator _findNextSeparator;
        private bool _done = false;

        public ReadOnlySpan<T> Current { get; private set; } = ReadOnlySpan<T>.Empty;

        public SpanSeparatorSplitEnumerator<T> GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_options == StringSplitOptions.RemoveEmptyEntries)
            {
                while (MoveNextPart())
                {
                    if (!Current.IsEmpty)
                    {
                        return true;
                    }
                }

                return false;
            }

            return MoveNextPart();
        }

        private bool MoveNextPart()
        {
            if (_done)
            {
                return false;
            }

            var separatorIndex = _findNextSeparator(_span);
            if (separatorIndex >= 0)
            {
                Current = _span.Slice(0, separatorIndex);

                var nextStartIndex = separatorIndex + 1;
                _span = _span.Length > nextStartIndex
                    ? _span.Slice(nextStartIndex)
                    : ReadOnlySpan<T>.Empty;
            }
            else
            {
                Current = _span;
                _done = true;
            }

            return true;
        }
    }

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

        for (var i = removeIndex + 1; i < @this.Length; i++)
        {
            ref readonly var c = ref @this[i];
            if (value != c)
            {
                builder.Append(c);
            }
        }

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public static string ReplaceIntoString(this ReadOnlySpan<char> @this, char oldChar, char newChar)
    {
        if (@this.IsEmpty)
        {
            return string.Empty;
        }

        var replaceIndex = @this.IndexOf(oldChar);
        if (replaceIndex < 0)
        {
            return @this.ToString();
        }

        var builder = StringBuilderPool.Get(@this);

        do
        {
            builder[replaceIndex] = newChar;
        }
        while ((replaceIndex = builder.IndexOf(oldChar, replaceIndex + 1)) >= 0);

        return StringBuilderPool.GetStringAndReturn(builder);
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
