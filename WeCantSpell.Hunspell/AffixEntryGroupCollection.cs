using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct AffixEntryGroupCollection<TEntry> : IReadOnlyList<AffixEntryGroup<TEntry>> where TEntry : AffixEntry
{
    public static AffixEntryGroupCollection<TEntry> Empty { get; } = new(Array.Empty<AffixEntryGroup<TEntry>>());

    public static AffixEntryGroupCollection<TEntry> Create(IEnumerable<AffixEntryGroup<TEntry>> groups) =>
        new((groups ?? throw new ArgumentNullException(nameof(groups))).ToArray());

    internal AffixEntryGroupCollection(AffixEntryGroup<TEntry>[] groups)
    {
#if DEBUG
        if (groups is null) throw new ArgumentNullException(nameof(groups));
#endif
        Groups = groups;
    }

    internal AffixEntryGroup<TEntry>[] Groups { get; }

    public int Count => Groups.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Groups is { Length: > 0 };
    public AffixEntryGroup<TEntry> this[int index] => Groups[index];
    public IEnumerator<AffixEntryGroup<TEntry>> GetEnumerator() => ((IEnumerable<AffixEntryGroup<TEntry>>)Groups).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Groups.GetEnumerator();
}
