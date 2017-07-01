using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class AffixEntryGroupCollection<TEntry> : ListWrapper<AffixEntryGroup<TEntry>>
        where TEntry : AffixEntry
    {
        public static readonly AffixEntryGroupCollection<TEntry> Empty = TakeList(new List<AffixEntryGroup<TEntry>>(0));

        private AffixEntryGroupCollection(List<AffixEntryGroup<TEntry>> entries) : base(entries)
        {
        }

        internal static AffixEntryGroupCollection<TEntry> TakeList(List<AffixEntryGroup<TEntry>> entries) =>
            entries == null ? Empty : new AffixEntryGroupCollection<TEntry>(entries);

        public static AffixEntryGroupCollection<TEntry> Create(IEnumerable<AffixEntryGroup<TEntry>> entries) =>
            entries == null ? Empty : TakeList(entries.ToList());
    }
}
