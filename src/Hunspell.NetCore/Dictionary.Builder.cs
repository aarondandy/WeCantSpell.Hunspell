using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public partial class Dictionary
    {
        public sealed class Builder
        {
            public Builder()
            {
            }

            public Builder(AffixConfig affix)
            {
                Affix = affix;
            }

            public Dictionary<string, ImmutableArray<DictionaryEntry>> EntriesByRoot;

            public AffixConfig Affix;

            public Dictionary ToImmutable()
            {
                return ToImmutable(destructive: false);
            }

            public Dictionary MoveToImmutable()
            {
                return ToImmutable(destructive: true);
            }

            private Dictionary ToImmutable(bool destructive)
            {
                var affix = Affix ?? new AffixConfig.Builder().MoveToImmutable();

                var nGramRestrictedFlags =
                    new[]
                    {
                        affix.ForbiddenWord,
                        affix.NoSuggest,
                        affix.NoNgramSuggest,
                        affix.OnlyInCompound,
                        SpecialFlags.OnlyUpcaseFlag
                    }
                    .Where(f => f.HasValue)
                    .ToImmutableSortedSet();

                var result = new Dictionary(affix)
                {
                    NGramRestrictedFlags = nGramRestrictedFlags,
                };

                if (destructive)
                {
                    result.EntriesByRoot = EntriesByRoot ?? new Dictionary<string, ImmutableArray<DictionaryEntry>>();
                    EntriesByRoot = null;
                }
                else
                {
                    result.EntriesByRoot = EntriesByRoot == null
                        ? new Dictionary<string, ImmutableArray<DictionaryEntry>>()
                        : new Dictionary<string, ImmutableArray<DictionaryEntry>>(EntriesByRoot);
                }

                var nGramRestrictedEntries = new HashSet<DictionaryEntry>();

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
                    ? new Dictionary<string, ImmutableArray<DictionaryEntry>>()
                    // PERF: because we add more entries than we are told about, we add 10% to the expected size
                    : new Dictionary<string, ImmutableArray<DictionaryEntry>>((expectedSize / 100) + expectedSize);
            }
        }
    }
}
