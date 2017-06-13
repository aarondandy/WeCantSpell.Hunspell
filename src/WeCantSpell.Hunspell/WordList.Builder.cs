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
            }

            public Dictionary<string, List<WordEntry>> EntriesByRoot;

            public readonly AffixConfig Affix;

            internal readonly Deduper<FlagSet> FlagSetDeduper;

            internal readonly Deduper<MorphSet> MorphSetDeduper;

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

                if (EntriesByRoot == null)
                {
                    result.EntriesByRoot = new Dictionary<string, WordEntrySet>(0);
                }
                else
                {
                    result.EntriesByRoot = new Dictionary<string, WordEntrySet>(EntriesByRoot.Count);
                    foreach(var pair in EntriesByRoot)
                    {
                        result.EntriesByRoot.Add(pair.Key, WordEntrySet.TakeArray(pair.Value.ToArray()));
                    }

                    if (destructive)
                    {
                        EntriesByRoot.Clear();
                    }
                }

                var nGramRestrictedEntries = new HashSet<WordEntry>();

                foreach (var rootSet in result.EntriesByRoot)
                {
                    foreach (var entry in rootSet.Value)
                    {
                        if (nGramRestrictedFlags.ContainsAny(entry.Flags))
                        {
                            nGramRestrictedEntries.Add(entry);
                        }
                    }
                }

                result.NGramRestrictedEntries = nGramRestrictedEntries;

                return result;
            }

            public void InitializeEntriesByRoot(int expectedSize)
            {
                EntriesByRoot = expectedSize <= 0
                    ? new Dictionary<string, List<WordEntry>>()
                    // PERF: because we add more entries than we are told about, we add a bit more to the expected size
                    : new Dictionary<string, List<WordEntry>>((expectedSize / 100) + expectedSize);
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
        }
    }
}
