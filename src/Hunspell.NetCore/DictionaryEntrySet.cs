using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class DictionaryEntrySet :
        IReadOnlyList<DictionaryEntry>
    {
        public static readonly DictionaryEntrySet Empty = TakeArray(ArrayEx<DictionaryEntry>.Empty);

        private DictionaryEntry[] entries;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private DictionaryEntrySet(DictionaryEntry[] entries)
        {
            this.entries = entries;
        }

        public DictionaryEntry this[int index] => entries[index];

        public int Count => entries.Length;

        public bool IsEmpty => entries.Length == 0;

        public bool HasEntries => entries.Length != 0;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static DictionaryEntrySet TakeArray(DictionaryEntry[] entries) => new DictionaryEntrySet(entries);

        public static DictionaryEntrySet CopyWithItemReplaced(DictionaryEntrySet source, int index, DictionaryEntry replacement)
        {
            var newEntries = new DictionaryEntry[source.entries.Length];
            Array.Copy(source.entries, newEntries, newEntries.Length);
            newEntries[index] = replacement;
            return TakeArray(newEntries);
        }

        public static DictionaryEntrySet CopyWithItemAdded(DictionaryEntrySet source, DictionaryEntry entry)
        {
            DictionaryEntry[] newEntries;
            if (source.entries.Length == 0)
            {
                newEntries = new[] { entry };
            }
            else
            {
                newEntries = new DictionaryEntry[source.entries.Length + 1];
                Array.Copy(source.entries, newEntries, source.entries.Length);
                newEntries[source.entries.Length] = entry;
            }

            return TakeArray(newEntries);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public IEnumerator<DictionaryEntry> GetEnumerator() => ((IEnumerable<DictionaryEntry>)entries).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => entries.GetEnumerator();

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal void DestructiveReplace(int index, DictionaryEntry entry)
        {
            entries[index] = entry;
        }
    }
}
