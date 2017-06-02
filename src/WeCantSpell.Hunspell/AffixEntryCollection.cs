using System.Collections.Generic;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class AffixEntryCollection<TEntry> : ArrayWrapper<TEntry>
        where TEntry : AffixEntry
    {
        public static readonly AffixEntryCollection<TEntry> Empty = TakeArray(ArrayEx<TEntry>.Empty);

        private AffixEntryCollection(TEntry[] entries) : base(entries)
        {
        }

        internal static AffixEntryCollection<TEntry> TakeArray(TEntry[] entries) =>
            entries == null ? Empty : new AffixEntryCollection<TEntry>(entries);

        public static AffixEntryCollection<TEntry> Create(List<TEntry> entries) =>
            entries == null ? Empty : TakeArray(entries.ToArray());
    }
}
