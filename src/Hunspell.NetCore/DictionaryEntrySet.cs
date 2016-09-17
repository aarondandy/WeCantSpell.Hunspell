using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class DictionaryEntrySet : ArrayWrapper<DictionaryEntry>
    {
        public static readonly DictionaryEntrySet Empty = TakeArray(ArrayEx<DictionaryEntry>.Empty);

        private DictionaryEntrySet(DictionaryEntry[] entries)
            : base(entries)
        {
        }

        internal static DictionaryEntrySet TakeArray(DictionaryEntry[] entries) => entries == null ? Empty : new DictionaryEntrySet(entries);

        public static DictionaryEntrySet Create(IEnumerable<DictionaryEntry> entries) => entries == null ? Empty : TakeArray(entries.ToArray());

        public static DictionaryEntrySet CopyWithItemReplaced(DictionaryEntrySet source, int index, DictionaryEntry replacement)
        {
            var newEntries = new DictionaryEntry[source.items.Length];
            Array.Copy(source.items, newEntries, newEntries.Length);
            newEntries[index] = replacement;
            return TakeArray(newEntries);
        }

        public static DictionaryEntrySet CopyWithItemAdded(DictionaryEntrySet source, DictionaryEntry entry)
        {
            DictionaryEntry[] newEntries;
            if (source.items.Length == 0)
            {
                newEntries = new[] { entry };
            }
            else
            {
                newEntries = new DictionaryEntry[source.items.Length + 1];
                Array.Copy(source.items, newEntries, source.items.Length);
                newEntries[source.items.Length] = entry;
            }

            return TakeArray(newEntries);
        }

        public DictionaryEntry FirstOrDefault()
        {
            return items.Length != 0 ? items[0] : null;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal void DestructiveReplace(int index, DictionaryEntry entry)
        {
            items[index] = entry;
        }
    }
}
