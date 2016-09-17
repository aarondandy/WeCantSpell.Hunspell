using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MapTable : ListWrapper<MapEntry>
    {
        public static readonly MapTable Empty = TakeList(new List<MapEntry>(0));

        private MapTable(List<MapEntry> entries)
            : base(entries)
        {
        }

        internal static MapTable TakeList(List<MapEntry> entries) => entries == null ? Empty : new MapTable(entries);

        public static MapTable Create(IEnumerable<MapEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());
    }
}
