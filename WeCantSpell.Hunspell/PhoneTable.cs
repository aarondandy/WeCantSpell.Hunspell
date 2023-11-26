using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct PhoneTable : IReadOnlyList<PhoneticEntry>
{
    public static PhoneTable Empty { get; } = new([]);

    public static PhoneTable Create(IEnumerable<PhoneticEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        if (entries is null) throw new ArgumentNullException(nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal PhoneTable(PhoneticEntry[] items)
    {
        _items = items;
    }

    private readonly PhoneticEntry[]? _items;

    public int Count => (_items?.Length).GetValueOrDefault();

    public bool IsEmpty => !HasItems;

    public bool HasItems => _items is { Length: > 0 };

    public PhoneticEntry this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            return _items![index];
        }
    }

    public IEnumerator<PhoneticEntry> GetEnumerator() => ((IEnumerable<PhoneticEntry>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal PhoneticEntry[] GetInternalArray() => _items ?? [];
}
