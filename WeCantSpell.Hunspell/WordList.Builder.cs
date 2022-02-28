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
        }

        private Dictionary<string, List<WordEntryDetail>> EntryDetailsByRoot;

        public readonly AffixConfig Affix;

        /// <summary>
        /// Spelling replacement suggestions based on phonetics.
        /// </summary>
        public ImmutableArray<SingleReplacement>.Builder PhoneticReplacements { get; } = ImmutableArray.CreateBuilder<SingleReplacement>();

        public void Add(string word, WordEntryDetail detail)
        {
            var details = GetOrCreateDetailList(word);

            details.Add(detail);
        }

        internal List<WordEntryDetail> GetOrCreateDetailList(string word)
        {
            if (!EntryDetailsByRoot.TryGetValue(word, out List<WordEntryDetail> details))
            {
                details = new List<WordEntryDetail>(2);
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
            var result = new WordList(Affix);
            result.NGramRestrictedFlags = FlagSet.Create(new[]
            {
                Affix.ForbiddenWord,
                Affix.NoSuggest,
                Affix.NoNgramSuggest,
                Affix.OnlyInCompound,
                SpecialFlags.OnlyUpcaseFlag
            });

            if (EntryDetailsByRoot is null)
            {
                result.EntriesByRoot = new Dictionary<string, WordEntryDetail[]>();
            }
            else
            {
                result.EntriesByRoot = new Dictionary<string, WordEntryDetail[]>(EntryDetailsByRoot.Count);
                foreach (var pair in EntryDetailsByRoot)
                {
                    result.EntriesByRoot.Add(pair.Key, pair.Value.ToArray());
                }

                if (destructive)
                {
                    EntryDetailsByRoot = null;
                }
            }

            result.AllReplacements = Affix.Replacements;
            if (PhoneticReplacements is { Count: > 0 })
            {
                // store ph: field of a morphological description in reptable
                if (result.AllReplacements.IsEmpty)
                {
                    result.AllReplacements = new(PhoneticReplacements.ToImmutable(destructive));
                }
                else if (destructive)
                {
                    PhoneticReplacements.AddRange(result.AllReplacements);
                    result.AllReplacements = new(PhoneticReplacements.ToImmutable(destructive));
                }
                else
                {
                    result.AllReplacements = new(PhoneticReplacements.Concat(result.AllReplacements).ToImmutableArray());
                }
            }

            result.NGramRestrictedDetails = new Dictionary<string, WordEntryDetail[]>();

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
            if (EntryDetailsByRoot != null)
            {
                return;
            }

            EntryDetailsByRoot = expectedSize <= 0
                ? new Dictionary<string, List<WordEntryDetail>>()
                // PERF: because we add more entries than we are told about, we add a bit more to the expected size
                : new Dictionary<string, List<WordEntryDetail>>((expectedSize / 100) + expectedSize);
        }
    }
}
