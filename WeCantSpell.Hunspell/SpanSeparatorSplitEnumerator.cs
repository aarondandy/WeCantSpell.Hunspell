using System;

namespace WeCantSpell.Hunspell;

internal ref struct SpanSeparatorSplitEnumerator<T> where T : IEquatable<T>
{
    public delegate int FindNextSeparator(ReadOnlySpan<T> text);

    public SpanSeparatorSplitEnumerator(ReadOnlySpan<T> span, StringSplitOptions options, FindNextSeparator findNextSeparator)
    {
#if DEBUG
        if (options is not (StringSplitOptions.None or StringSplitOptions.RemoveEmptyEntries))
        {
            ExceptionEx.ThrowArgumentOutOfRange(nameof(options));
        }
#endif

        _findNextSeparator = findNextSeparator;
        _options = options;
        _span = span;
        _done = false;
    }

    private readonly FindNextSeparator _findNextSeparator;
    private readonly StringSplitOptions _options;
    private ReadOnlySpan<T> _span;
    private ReadOnlySpan<T> _current;
    private bool _done;

    public readonly ReadOnlySpan<T> Current => _current;

    public readonly SpanSeparatorSplitEnumerator<T> GetEnumerator() => this;

    public bool MoveNext()
    {
        if (_options == StringSplitOptions.RemoveEmptyEntries)
        {
            return MoveNextPartSkippingEmpty();
        }

        return MoveNextPart();
    }

    private bool MoveNextPartSkippingEmpty()
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

    private bool MoveNextPart()
    {
        if (_done)
        {
            return false;
        }

        var separatorIndex = _findNextSeparator(_span);
        if (separatorIndex >= 0)
        {
            _current = _span.Slice(0, separatorIndex);

            var nextStartIndex = separatorIndex + 1;
            _span = _span.Length > nextStartIndex ? _span.Slice(nextStartIndex) : [];
        }
        else
        {
            _current = _span;
            _done = true;
        }

        return true;
    }
}
