using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct PhoneTable : IReadOnlyList<PhoneticEntry>
{
    public static PhoneTable Empty { get; } = new(Array.Empty<PhoneticEntry>());

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

    private readonly PhoneticEntry[] _items;

    public int Count => _items.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _items is { Length: > 0 };
    public PhoneticEntry this[int index] => _items[index];
    public IEnumerator<PhoneticEntry> GetEnumerator() => ((IEnumerable<PhoneticEntry>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
}
