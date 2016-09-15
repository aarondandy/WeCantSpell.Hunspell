using System.Collections.Generic;
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

        private Dictionary<string, DictionaryEntrySet> EntriesByRoot { get; set; }

        private FlagSet NGramRestrictedFlags { get; set; }

        private HashSet<DictionaryEntry> NGramRestrictedEntries { get; set; }

        public IEnumerable<DictionaryEntry> NGramAllowedEntries =>
            NGramRestrictedEntries == null || NGramRestrictedEntries.Count == 0
            ? AllEntries
            : AllEntries.Where(entry => !NGramRestrictedEntries.Contains(entry));

        public IEnumerable<DictionaryEntry> AllEntries => EntriesByRoot.Values.SelectMany(set => set);

        public IEnumerable<string> RootWords => EntriesByRoot.Keys;

        public bool HasEntries => EntriesByRoot.Count != 0;

        public DictionaryEntrySet this[string rootWord] => FindEntriesByRootWord(rootWord);

        public DictionaryEntrySet FindEntriesByRootWord(string rootWord)
        {
            DictionaryEntrySet result;
            if (!EntriesByRoot.TryGetValue(rootWord, out result))
            {
                result = DictionaryEntrySet.Empty;
            }

            return result;
        }
    }
}
