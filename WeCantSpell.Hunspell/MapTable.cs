using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct MapTable : IReadOnlyList<MapEntry>
{
    public static MapTable Empty { get; } = new(ImmutableArray<MapEntry>.Empty);

    internal MapTable(ImmutableArray<MapEntry> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<MapEntry> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsDefaultOrEmpty;
    public bool HasItems => !IsEmpty;
    public MapEntry this[int index] => _items[index];

    public ImmutableArray<MapEntry>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<MapEntry> IEnumerable<MapEntry>.GetEnumerator() => ((IEnumerable<MapEntry>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();
}
