using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class PhoneTable :
        IReadOnlyList<PhoneticEntry>
    {
        public static readonly PhoneTable Empty = TakeList(new List<PhoneticEntry>(0));

        private List<PhoneticEntry> entries;

        private PhoneTable(List<PhoneticEntry> entries)
        {
            this.entries = entries;
        }

        public PhoneticEntry this[int index]
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

        internal static PhoneTable TakeList(List<PhoneticEntry> entries) => entries == null ? Empty : new PhoneTable(entries);

        public static PhoneTable Create(IEnumerable<PhoneticEntry> entries) => entries == null ? Empty : TakeList(entries.ToList());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastListEnumerator<PhoneticEntry> GetEnumerator() => new FastListEnumerator<PhoneticEntry>(entries);

        IEnumerator<PhoneticEntry> IEnumerable<PhoneticEntry>.GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();
    }
}
