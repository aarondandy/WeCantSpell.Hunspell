using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
    public static int IndexOf<T>(this ReadOnlySpan<T> @this, T value, int startIndex) where T:IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static int IndexOf<T>(this ReadOnlySpan<T> @this, ReadOnlySpan<T> value, int startIndex) where T : IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
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

    public static bool EndsWith(this ReadOnlyMemory<char> @this, char value) => !@this.IsEmpty && @this.Span[@this.Length - 1] == value;

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

    public static string ReplaceIntoString(this ReadOnlySpan<char> text, char oldChar, char newChar)
    {
        if (text.IsEmpty)
        {
            return string.Empty;
        }

        var replaceIndex = text.IndexOf(oldChar);
        return replaceIndex < 0
            ? text.ToString()
            : buildReplaced(replaceIndex, text, oldChar, newChar);

        static string buildReplaced(int replaceIndex, ReadOnlySpan<char> text, char oldChar, char newChar)
        {
            var builder = StringBuilderPool.Get(text);
            builder.Replace(oldChar, newChar, replaceIndex, builder.Length - replaceIndex);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }

    public static ReadOnlySpan<char> Replace(this ReadOnlySpan<char> text, string oldText, string newText)
    {
        if (text.IsEmpty)
        {
            return ReadOnlySpan<char>.Empty;
        }

        var replaceIndex = text.IndexOf(oldText.AsSpan());
        return replaceIndex < 0 ? text : buildReplaced(replaceIndex, text, oldText, newText);

        static ReadOnlySpan<char> buildReplaced(int replaceIndex, ReadOnlySpan<char> text, string oldText, string newText)
        {
            var builder = StringBuilderPool.Get(text);
            builder.Replace(oldText, newText, replaceIndex, builder.Length - replaceIndex);
            return StringBuilderPool.GetStringAndReturn(builder).AsSpan();
        }
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

    public static int FindNullTerminatedLength(this ReadOnlySpan<char> @this)
    {
        var index = @this.IndexOf('\0');
        return index < 0 ? @this.Length : index;
    }
}
