using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class AffixEntryGroupCollection<TEntry> : ArrayWrapper<AffixEntryGroup<TEntry>>
    where TEntry : AffixEntry
{
    public static readonly AffixEntryGroupCollection<TEntry> Empty = TakeArray(ArrayEx<AffixEntryGroup<TEntry>>.Empty);

    private AffixEntryGroupCollection(AffixEntryGroup<TEntry>[] entries) : base(entries)
    {
    }

    internal static AffixEntryGroupCollection<TEntry> TakeArray(AffixEntryGroup<TEntry>[] entries) =>
        entries is null ? Empty : new AffixEntryGroupCollection<TEntry>(entries);

    public static AffixEntryGroupCollection<TEntry> Create(IEnumerable<AffixEntryGroup<TEntry>> entries) =>
        entries is null ? Empty : TakeArray(entries.ToArray());
}
