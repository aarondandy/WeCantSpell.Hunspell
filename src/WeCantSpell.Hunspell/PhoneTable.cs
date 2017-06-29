using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class PhoneTable : ListWrapper<PhoneticEntry>
    {
        public static readonly PhoneTable Empty = TakeList(new List<PhoneticEntry>(0));

        public static PhoneTable Create(IEnumerable<PhoneticEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());

        internal static PhoneTable TakeList(List<PhoneticEntry> entries) => entries == null ? Empty : new PhoneTable(entries);

        private PhoneTable(List<PhoneticEntry> entries)
            : base(entries)
        {
        }
    }
}
