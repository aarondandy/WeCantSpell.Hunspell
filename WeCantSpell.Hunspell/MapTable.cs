using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct MapTable : IReadOnlyList<MapEntry>
{
    public static MapTable Empty { get; } = new([]);

    public static MapTable Create(IEnumerable<MapEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal MapTable(MapEntry[] items)
    {
        _entries = items;
    }

    private readonly MapEntry[]? _entries;

    public int Count => _entries is not null ? _entries.Length : 0;

    public bool IsEmpty => _entries is not { Length: > 0 };

    public bool HasItems => _entries is { Length: > 0 };

    public MapEntry this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif

            return _entries![index];
        }
    }

    public IEnumerator<MapEntry> GetEnumerator() => ((IEnumerable<MapEntry>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal MapEntry[] GetInternalArray() => _entries ?? [];
}
