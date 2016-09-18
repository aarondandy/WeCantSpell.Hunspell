using System.Collections.Generic;
using System.Linq;

namespace Hunspell
{
    public sealed partial class WordList
    {
        private WordList(AffixConfig affix)
        {
            Affix = affix;
        }

        public AffixConfig Affix { get; private set; }

        private Dictionary<string, WordEntrySet> EntriesByRoot { get; set; }

        private FlagSet NGramRestrictedFlags { get; set; }

        private HashSet<WordEntry> NGramRestrictedEntries { get; set; }

        public IEnumerable<WordEntry> NGramAllowedEntries =>
            NGramRestrictedEntries == null || NGramRestrictedEntries.Count == 0
            ? AllEntries
            : AllEntries.Where(entry => !NGramRestrictedEntries.Contains(entry));

        public IEnumerable<WordEntry> AllEntries => EntriesByRoot.Values.SelectMany(set => set);

        public IEnumerable<string> RootWords => EntriesByRoot.Keys;

        public bool HasEntries => EntriesByRoot.Count != 0;

        public WordEntrySet this[string rootWord] => FindEntriesByRootWord(rootWord);

        public WordEntrySet FindEntriesByRootWord(string rootWord)
        {
            WordEntrySet result;
            if (!EntriesByRoot.TryGetValue(rootWord, out result))
            {
                result = WordEntrySet.Empty;
            }

            return result;
        }
    }
}
