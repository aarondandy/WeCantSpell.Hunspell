using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class AffixEntryCollection<TEntry> : ArrayWrapper<TEntry>
    where TEntry : AffixEntry
{
    public static readonly AffixEntryCollection<TEntry> Empty = TakeArray(Array.Empty<TEntry>());

    internal static AffixEntryCollection<TEntry> TakeArray(TEntry[] entries) => new(entries, canStealArray: true);

    private static TEntry[] ToCleanArray(IEnumerable<TEntry> entries) => entries.ToArray();

    public AffixEntryCollection(IEnumerable<TEntry> entries) : this(ToCleanArray(entries ?? throw new ArgumentNullException(nameof(entries))), canStealArray: true)
    {
    }

    private AffixEntryCollection(TEntry[] entries, bool canStealArray) : base(entries, canStealArray: canStealArray)
    {
    }
}
