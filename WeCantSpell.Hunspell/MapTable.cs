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
        Entries = items;
    }

    internal MapEntry[] Entries { get; }

    public int Count => Entries.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Entries is { Length: > 0 };
    public MapEntry this[int index] => Entries[index];
    public IEnumerator<MapEntry> GetEnumerator() => ((IEnumerable<MapEntry>)Entries).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Entries.GetEnumerator();
}
