using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class AffixEntryWithDetailCollection<TEntry> : ListWrapper<AffixEntryWithDetail<TEntry>>
        where TEntry : AffixEntry
    {
        public static readonly AffixEntryWithDetailCollection<TEntry> Empty = TakeList(new List<AffixEntryWithDetail<TEntry>>(0));

        private AffixEntryWithDetailCollection(List<AffixEntryWithDetail<TEntry>> entries)
            : base(entries)
        {
        }

        internal static AffixEntryWithDetailCollection<TEntry> TakeList(List<AffixEntryWithDetail<TEntry>> entries) => entries == null ? Empty : new AffixEntryWithDetailCollection<TEntry>(entries);

        public static AffixEntryWithDetailCollection<TEntry> Create(IEnumerable<AffixEntryWithDetail<TEntry>> entries) => entries == null ? Empty : new AffixEntryWithDetailCollection<TEntry>(entries.ToList());
    }
}
