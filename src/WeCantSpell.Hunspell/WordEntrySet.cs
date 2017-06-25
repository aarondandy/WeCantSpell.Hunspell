using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class WordEntrySet : ArrayWrapper<WordEntry>
    {
        public static readonly WordEntrySet Empty = TakeArray(ArrayEx<WordEntry>.Empty);

        private WordEntrySet(WordEntry[] entries)
            : base(entries)
        {
        }

        internal static WordEntrySet TakeArray(WordEntry[] entries) =>
            entries == null ? Empty : new WordEntrySet(entries);

        [Obsolete]
        public static WordEntrySet Create(WordEntry entry) => new WordEntrySet(new[] { entry });

        public static WordEntrySet Create(string word, WordEntryDetail[] details)
        {
            var entries = new WordEntry[details.Length];
            for (var i = 0; i < entries.Length; i++)
            {
                entries[i] = new WordEntry(word, details[i]);
            }

            return TakeArray(entries);
        }

        public static WordEntrySet Create(IEnumerable<WordEntry> entries) =>
            entries == null ? Empty : TakeArray(entries.ToArray());

        [Obsolete]
        public static WordEntrySet CopyWithItemReplaced(WordEntrySet source, int index, WordEntry replacement)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (replacement == null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            var newEntries = new WordEntry[source.items.Length];
            Array.Copy(source.items, newEntries, newEntries.Length);
            newEntries[index] = replacement;
            return TakeArray(newEntries);
        }

        [Obsolete]
        public static WordEntrySet CopyWithItemAdded(WordEntrySet source, WordEntry entry)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

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

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        [Obsolete]
        public WordEntry FirstOrDefault() =>
            items.Length != 0 ? items[0] : null;
    }
}
