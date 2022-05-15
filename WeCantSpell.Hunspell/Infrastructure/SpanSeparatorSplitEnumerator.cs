using System;

namespace WeCantSpell.Hunspell.Infrastructure;

internal ref struct SpanSeparatorSplitEnumerator<T> where T : IEquatable<T>
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
