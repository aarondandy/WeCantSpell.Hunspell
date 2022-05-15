using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct MapTable : IReadOnlyList<MapEntry>
{
    public static MapTable Empty { get; } = new(Array.Empty<MapEntry>());

    public static MapTable Create(IEnumerable<MapEntry> entries) =>
        new((entries ?? throw new ArgumentNullException(nameof(entries))).ToArray());

    internal MapTable(MapEntry[] items)
    {
#if DEBUG
        if (items is null) throw new ArgumentNullException(nameof(items));
#endif
        _entries = items;
    }

    private readonly MapEntry[] _entries;

    public int Count => _entries.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _entries is { Length: > 0 };
    public MapEntry this[int index] => _entries[index];
    public IEnumerator<MapEntry> GetEnumerator() => ((IEnumerable<MapEntry>)_entries).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();
    internal MapEntry[] GetInternalArray() => _entries;
}
