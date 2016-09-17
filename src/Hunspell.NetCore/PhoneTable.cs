using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class PhoneTable : ListWrapper<PhoneticEntry>
    {
        public static readonly PhoneTable Empty = TakeList(new List<PhoneticEntry>(0));

        private PhoneTable(List<PhoneticEntry> entries)
            : base(entries)
        {
        }

        internal static PhoneTable TakeList(List<PhoneticEntry> entries) => entries == null ? Empty : new PhoneTable(entries);

        public static PhoneTable Create(IEnumerable<PhoneticEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());
    }
}
