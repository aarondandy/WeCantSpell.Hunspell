using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public partial class WordList
    {
        public sealed class Builder
        {
            public Builder()
                : this(null, null)
            {
            }

            public Builder(AffixConfig affix)
                : this(affix, null)
            {
            }

            internal Builder(AffixConfig affix, Deduper<FlagSet> flagSetDeduper)
            {
                Affix = affix;
                FlagSetDeduper = flagSetDeduper ?? new Deduper<FlagSet>(new FlagSet.Comparer());
            }

            public Dictionary<string, WordEntrySet> EntriesByRoot;

            public readonly AffixConfig Affix;

            internal readonly Deduper<FlagSet> FlagSetDeduper;

            public WordList ToImmutable()
            {
                return ToImmutable(destructive: false);
            }

            public WordList MoveToImmutable()
            {
                return ToImmutable(destructive: true);
            }

            private WordList ToImmutable(bool destructive)
            {
                var affix = Affix ?? new AffixConfig.Builder().MoveToImmutable();

                var nGramRestrictedFlags = FlagSet.Create(
                    new[]
                    {
                        affix.ForbiddenWord,
                        affix.NoSuggest,
                        affix.NoNgramSuggest,
                        affix.OnlyInCompound,
                        SpecialFlags.OnlyUpcaseFlag
                    }
                    .Where(f => f.HasValue));
                nGramRestrictedFlags = FlagSetDeduper.GetEqualOrAdd(nGramRestrictedFlags);

                var result = new WordList(affix)
                {
                    NGramRestrictedFlags = nGramRestrictedFlags,
                };

                if (destructive)
                {
                    result.EntriesByRoot = EntriesByRoot ?? new Dictionary<string, WordEntrySet>();
                    EntriesByRoot = null;
                }
                else
                {
                    result.EntriesByRoot = EntriesByRoot == null
                        ? new Dictionary<string, WordEntrySet>()
                        : new Dictionary<string, WordEntrySet>(EntriesByRoot);
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
                EntriesByRoot = expectedSize < 0
                    ? new Dictionary<string, WordEntrySet>()
                    // PERF: because we add more entries than we are told about, we add 10% to the expected size
                    : new Dictionary<string, WordEntrySet>((expectedSize / 100) + expectedSize);
            }
        }
    }
}
