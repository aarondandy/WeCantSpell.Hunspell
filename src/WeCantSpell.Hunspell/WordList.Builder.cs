using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public partial class WordList
    {
        public sealed class Builder
        {
            public Builder()
                : this(null, null, null)
            {
            }

            public Builder(AffixConfig affix)
                : this(affix, null, null)
            {
            }

            internal Builder(AffixConfig affix, Deduper<FlagSet> flagSetDeduper, Deduper<MorphSet> morphSet)
            {
                Affix = affix;
                FlagSetDeduper = flagSetDeduper ?? new Deduper<FlagSet>(FlagSet.DefaultComparer);
                FlagSetDeduper.Add(FlagSet.Empty);
                MorphSetDeduper = morphSet ?? new Deduper<MorphSet>(MorphSet.DefaultComparer);
                MorphSetDeduper.Add(MorphSet.Empty);
                WordEntryDetailDeduper = new Deduper<WordEntryDetail>(EqualityComparer<WordEntryDetail>.Default);
                WordEntryDetailDeduper.Add(WordEntryDetail.Default);
            }

            private Dictionary<string, List<WordEntryDetail>> EntryDetailsByRoot;

            public readonly AffixConfig Affix;

            /// <summary>
            /// Spelling replacement suggestions based on phonetics.
            /// </summary>
            public List<SingleReplacement> PhoneticReplacements;

            internal readonly Deduper<FlagSet> FlagSetDeduper;

            internal readonly Deduper<MorphSet> MorphSetDeduper;

            internal readonly Deduper<WordEntryDetail> WordEntryDetailDeduper;

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
                var affix = Affix ?? new AffixConfig.Builder().MoveToImmutable();

                var nGramRestrictedFlags = Dedup(FlagSet.Create(
                    new[]
                    {
                        affix.ForbiddenWord,
                        affix.NoSuggest,
                        affix.NoNgramSuggest,
                        affix.OnlyInCompound,
                        SpecialFlags.OnlyUpcaseFlag
                    }
                    .Where(f => f.HasValue)));

                var result = new WordList(affix)
                {
                    NGramRestrictedFlags = nGramRestrictedFlags,
                };

                if (EntryDetailsByRoot == null)
                {
                    result.EntriesByRoot = new Dictionary<string, WordEntryDetail[]>(0);
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

                result.AllReplacements = affix.Replacements;
                if (PhoneticReplacements != null && PhoneticReplacements.Count != 0)
                {
                    // store ph: field of a morphological description in reptable
                    if (result.AllReplacements.IsEmpty)
                    {
                        result.AllReplacements = destructive
                            ? SingleReplacementSet.TakeList(ReferenceHelpers.Steal(ref PhoneticReplacements))
                            : SingleReplacementSet.Create(PhoneticReplacements);
                    }
                    else
                    {
                        result.AllReplacements = SingleReplacementSet.Create(result.AllReplacements.Concat(PhoneticReplacements));
                    }
                }

                result.NGramRestrictedDetails = new Dictionary<string, WordEntryDetail[]>();

                foreach (var rootSet in result.EntriesByRoot)
                {
                    List<WordEntryDetail> details = null;
                    foreach (var entry in rootSet.Value)
                    {
                        if (nGramRestrictedFlags.ContainsAny(entry.Flags))
                        {
                            if (details == null)
                            {
                                details = new List<WordEntryDetail>();
                            }

                            details.Add(entry);
                        }
                    }

                    if (details != null)
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

            public FlagSet Dedup(FlagSet value) =>
                value == null
                ? value
                : value.Count == 0
                ? FlagSet.Empty
                : FlagSetDeduper.GetEqualOrAdd(value);

            public MorphSet Dedup(MorphSet value) =>
                value == null
                ? value
                : value.Count == 0
                ? MorphSet.Empty
                : MorphSetDeduper.GetEqualOrAdd(value);

            public WordEntryDetail Dedup(WordEntryDetail value) =>
                value == null
                ? value
                : WordEntryDetailDeduper.GetEqualOrAdd(value);
        }
    }
}
