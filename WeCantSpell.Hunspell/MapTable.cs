using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct MapTable : IReadOnlyList<MapEntry>
{
    public static MapTable Empty { get; } = new([]);

    public static MapTable Create(IEnumerable<MapEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        if (entries is null) throw new ArgumentNullException(nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal MapTable(MapEntry[] items)
    {
        _entries = items;
    }

    private readonly MapEntry[]? _entries;

    public int Count => (_entries?.Length).GetValueOrDefault();

    public bool IsEmpty => !HasItems;

    public bool HasItems => _entries is { Length: > 0 };

    public MapEntry this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            return _entries![index];
        }
    }

    public IEnumerator<MapEntry> GetEnumerator() => ((IEnumerable<MapEntry>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal MapEntry[] GetInternalArray() => _entries ?? [];
}
