using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class MapTable : ListWrapper<MapEntry>
    {
        public static readonly MapTable Empty = TakeList(new List<MapEntry>(0));

        public static MapTable Create(IEnumerable<MapEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());

        internal static MapTable TakeList(List<MapEntry> entries) => entries == null ? Empty : new MapTable(entries);

        private MapTable(List<MapEntry> entries)
            : base(entries)
        {
        }
    }
}
