using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct BreakSet : IReadOnlyList<string>
{
    public static BreakSet Empty { get; } = new(ImmutableArray<string>.Empty);

    internal BreakSet(ImmutableArray<string> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<string> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsDefaultOrEmpty;
    public bool HasItems => !IsEmpty;
    public string this[int index] => _items[index];

    public ImmutableArray<string>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

    /// <summary>
    /// Calculate break points for recursion limit.
    /// </summary>
    internal int FindRecursionLimit(string scw)
    {
        int nbr = 0;

        if (!string.IsNullOrEmpty(scw))
        {
            foreach (var breakEntry in _items)
            {
                int pos = 0;
                while ((pos = scw.IndexOf(breakEntry, pos, StringComparison.Ordinal)) >= 0)
                {
                    nbr++;
                    pos += breakEntry.Length;
                }
            }
        }

        return nbr;
    }
}
