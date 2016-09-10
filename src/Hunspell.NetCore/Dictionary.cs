using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hunspell
{
    public sealed partial class Dictionary
    {
        private Dictionary(AffixConfig affix)
        {
            Affix = affix;
        }

        public AffixConfig Affix { get; private set; }

        private ImmutableDictionary<string, ImmutableArray<DictionaryEntry>> EntriesByRoot { get; set; }

        private ImmutableSortedSet<FlagValue> NGramRestrictedFlags { get; set; }

        private ImmutableHashSet<DictionaryEntry> NGramRestrictedEntries { get; set; }

        public IEnumerable<DictionaryEntry> NGramAllowedEntries => AllEntries.Where(entry => !NGramRestrictedEntries.Contains(entry));

        public IEnumerable<DictionaryEntry> AllEntries => EntriesByRoot.Values.SelectMany(set => set);

        public IEnumerable<string> RootWords => EntriesByRoot.Keys;

        public bool HasEntries => !EntriesByRoot.IsEmpty;

        public ImmutableArray<DictionaryEntry> this[string rootWord] => FindEntriesByRootWord(rootWord);

        public ImmutableArray<DictionaryEntry> FindEntriesByRootWord(string rootWord)
        {
            ImmutableArray<DictionaryEntry> result;
            if (!EntriesByRoot.TryGetValue(rootWord, out result))
            {
                result = ImmutableArray<DictionaryEntry>.Empty;
            }

            return result;
        }
    }
}
