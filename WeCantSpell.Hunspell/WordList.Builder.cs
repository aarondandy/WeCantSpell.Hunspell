using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    public sealed class Builder
    {
        public Builder() : this(new AffixConfig.Builder().MoveToImmutable())
        {
        }

        public Builder(AffixConfig affix)
        {
            Affix = affix;
            EntryDetailsByRoot = new();
        }

        private Dictionary<string, List<WordEntryDetail>> EntryDetailsByRoot;

        public readonly AffixConfig Affix;

        /// <summary>
        /// Spelling replacement suggestions based on phonetics.
        /// </summary>
        public List<SingleReplacement> PhoneticReplacements { get; } = new();

        public void Add(string word, WordEntryDetail detail)
        {
            var details = GetOrCreateDetailList(word);

            details.Add(detail);
        }

        internal List<WordEntryDetail> GetOrCreateDetailList(string word)
        {
            if (!EntryDetailsByRoot.TryGetValue(word, out var details))
            {
                details = new List<WordEntryDetail>(2); // TODO: maybe 1 is a better starting capacity
                EntryDetailsByRoot.Add(word, details);
            }

            return details;
        }

        public WordList ToImmutable() =>
            ToImmutable(destructive: false);

        public WordList MoveToImmutable() =>
            ToImmutable(destructive: true);

        private WordList ToImmutable(bool destructive)
        {
            var result = new WordList(Affix, FlagSet.Create(new[]
            {
                Affix.ForbiddenWord,
                Affix.NoSuggest,
                Affix.NoNgramSuggest,
                Affix.OnlyInCompound,
                SpecialFlags.OnlyUpcaseFlag
            }));

#if NO_HASHSET_CAPACITY
            result.EntriesByRoot = new(EntryDetailsByRoot.Count);
#else
            result.EntriesByRoot.Clear();
            result.EntriesByRoot.EnsureCapacity(EntryDetailsByRoot.Count);
#endif
            foreach (var pair in EntryDetailsByRoot)
            {
                result.EntriesByRoot.Add(pair.Key, pair.Value.ToArray());
            }

            result.AllReplacements = Affix.Replacements;
            if (PhoneticReplacements is { Count: > 0 })
            {
                // store ph: field of a morphological description in reptable
                if (result.AllReplacements.IsEmpty)
                {
                    result.AllReplacements = new(PhoneticReplacements.ToArray());
                }
                else if (destructive)
                {
                    PhoneticReplacements.AddRange(result.AllReplacements);
                    result.AllReplacements = new(PhoneticReplacements.ToArray());
                }
                else
                {
                    result.AllReplacements = SingleReplacementSet.Create(PhoneticReplacements.Concat(result.AllReplacements));
                }
            }

            var details = new List<WordEntryDetail>();
            foreach (var rootSet in result.EntriesByRoot)
            {
                details.Clear();
                foreach (var entry in rootSet.Value)
                {
                    if (result.NGramRestrictedFlags.ContainsAny(entry.Flags))
                    {
                        details.Add(entry);
                    }
                }

                if (details.Count != 0)
                {
                    result.NGramRestrictedDetails.Add(rootSet.Key, details.ToArray());
                }
            }

            return result;
        }

        public void InitializeEntriesByRoot(int expectedSize)
        {
            // PERF: because we add more entries than we are told about, we add a bit more to the expected size
            var expectedCapacity = (expectedSize / 100) + expectedSize;

#if NO_HASHSET_CAPACITY
            if (EntryDetailsByRoot.Count == 0)
            {
                EntryDetailsByRoot = new Dictionary<string, List<WordEntryDetail>>((expectedSize / 100) + expectedSize);
            }
#else
            EntryDetailsByRoot.EnsureCapacity(expectedCapacity);
#endif
        }
    }
}
