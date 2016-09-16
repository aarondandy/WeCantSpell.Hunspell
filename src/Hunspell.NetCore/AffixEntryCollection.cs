using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class AffixEntryCollection<TEntry> :
        IReadOnlyCollection<TEntry>
        where TEntry : AffixEntry
    {
        public static readonly AffixEntryCollection<TEntry> Empty = TakeArray(ArrayEx<TEntry>.Empty);

        private readonly TEntry[] entries;

        private AffixEntryCollection(TEntry[] entries)
        {
            this.entries = entries;
        }

        public TEntry this[int index] => entries[index];

        public int Count => entries.Length;

        internal static AffixEntryCollection<TEntry> TakeArray(TEntry[] entries) => new AffixEntryCollection<TEntry>(entries);

        public static AffixEntryCollection<TEntry> Create(List<TEntry> entries) => TakeArray(entries.ToArray());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<TEntry> GetEnumerator() => new FastArrayEnumerator<TEntry>(entries);

        IEnumerator<TEntry> IEnumerable<TEntry>.GetEnumerator() => ((IEnumerable<TEntry>)entries).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();
    }
}
