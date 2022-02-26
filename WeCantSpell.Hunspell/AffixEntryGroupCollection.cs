using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct AffixEntryGroupCollection<TEntry> : IReadOnlyList<AffixEntryGroup<TEntry>> where TEntry : AffixEntry
{
    public static AffixEntryGroupCollection<TEntry> Empty { get; } = new(ImmutableArray<AffixEntryGroup<TEntry>>.Empty);

    internal AffixEntryGroupCollection(ImmutableArray<AffixEntryGroup<TEntry>> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<AffixEntryGroup<TEntry>> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsDefaultOrEmpty;
    public bool HasItems => !IsEmpty;
    public AffixEntryGroup<TEntry> this[int index] => _items[index];

    public ImmutableArray<AffixEntryGroup<TEntry>>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<AffixEntryGroup<TEntry>> IEnumerable<AffixEntryGroup<TEntry>>.GetEnumerator() => ((IEnumerable<AffixEntryGroup<TEntry>>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();
}
