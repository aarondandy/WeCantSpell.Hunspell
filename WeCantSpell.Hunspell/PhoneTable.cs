using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct PhoneTable : IReadOnlyList<PhoneticEntry>
{
    internal PhoneTable(ImmutableArray<PhoneticEntry> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<PhoneticEntry> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsEmpty;
    public bool HasItems => !IsEmpty;
    public PhoneticEntry this[int index] => _items[index];

    public ImmutableArray<PhoneticEntry>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<PhoneticEntry> IEnumerable<PhoneticEntry>.GetEnumerator() => ((IEnumerable<PhoneticEntry>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();
}
