using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class AffixEntryGroupCollection<TEntry> : ArrayWrapper<AffixEntryGroup<TEntry>> where TEntry : AffixEntry
{
    public static readonly AffixEntryGroupCollection<TEntry> Empty = TakeArray(Array.Empty<AffixEntryGroup<TEntry>>());

    internal static AffixEntryGroupCollection<TEntry> TakeArray(AffixEntryGroup<TEntry>[] entries) => new(entries, canStealArray: true);

    private static AffixEntryGroup<TEntry>[] ToCleanArray(IEnumerable<AffixEntryGroup<TEntry>> entries) => entries.ToArray();

    public AffixEntryGroupCollection(IEnumerable<AffixEntryGroup<TEntry>> entries) : this(ToCleanArray(entries ?? throw new ArgumentNullException(nameof(entries))), canStealArray: true)
    {
    }

    private AffixEntryGroupCollection(AffixEntryGroup<TEntry>[] entries, bool canStealArray) : base(entries, canStealArray: canStealArray)
    {
    }

    public sealed class Builder
    {
        public List<AffixEntryGroup<TEntry>> Groups { get; } = new();

        public AffixEntryGroupCollection<TEntry> ToGroupCollection() => new(Groups);

        public void Add(AffixEntryGroup<TEntry> group)
        {
            Groups.Add(group);
        }

        public void AddRange(IEnumerable<AffixEntryGroup<TEntry>> groups)
        {
            Groups.AddRange(groups);
        }
    }
}
