using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class WordEntrySet : ArrayWrapper<WordEntry>
    {
        public static readonly WordEntrySet Empty = TakeArray(ArrayEx<WordEntry>.Empty);

        private WordEntrySet(WordEntry[] entries)
            : base(entries)
        {
        }

        internal static WordEntrySet TakeArray(WordEntry[] entries) => entries == null ? Empty : new WordEntrySet(entries);

        public static WordEntrySet Create(WordEntry entry) => TakeArray(new[] { entry });

        public static WordEntrySet Create(IEnumerable<WordEntry> entries) => entries == null ? Empty : TakeArray(entries.ToArray());

        public static WordEntrySet CopyWithItemReplaced(WordEntrySet source, int index, WordEntry replacement)
        {
            var newEntries = new WordEntry[source.items.Length];
            Array.Copy(source.items, newEntries, newEntries.Length);
            newEntries[index] = replacement;
            return TakeArray(newEntries);
        }

        public static WordEntrySet CopyWithItemAdded(WordEntrySet source, WordEntry entry)
        {
            WordEntry[] newEntries;
            if (source.items.Length == 0)
            {
                newEntries = new[] { entry };
            }
            else
            {
                newEntries = new WordEntry[source.items.Length + 1];
                Array.Copy(source.items, newEntries, source.items.Length);
                newEntries[source.items.Length] = entry;
            }

            return TakeArray(newEntries);
        }

        public WordEntry FirstOrDefault()
        {
            return items.Length != 0 ? items[0] : null;
        }

#if !NO_METHODIMPL && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal void DestructiveReplace(int index, WordEntry entry)
        {
            items[index] = entry;
        }
    }
}
