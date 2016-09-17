using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MapTable :
        IReadOnlyList<MapEntry>
    {
        public static readonly MapTable Empty = TakeList(new List<MapEntry>(0));

        private List<MapEntry> entries;

        private MapTable(List<MapEntry> entries)
        {
            this.entries = entries;
        }

        public MapEntry this[int index]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return entries[index];
            }
        }

        public int Count
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return entries.Count;
            }
        }

        public bool HasEntries => entries.Count != 0;

        public bool IsEmpty => entries.Count == 0;

        internal static MapTable TakeList(List<MapEntry> entries) => entries == null ? Empty : new MapTable(entries);

        public static MapTable Create(IEnumerable<MapEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastListEnumerator<MapEntry> GetEnumerator() => new FastListEnumerator<MapEntry>(entries);

        IEnumerator<MapEntry> IEnumerable<MapEntry>.GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();
    }
}
