using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct MapEntry : IReadOnlyList<string>
{
    internal MapEntry(ImmutableArray<string> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<string> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsEmpty;
    public bool HasItems => !IsEmpty;
    public string this[int index] => _items[index];

    public ImmutableArray<string>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();
}
