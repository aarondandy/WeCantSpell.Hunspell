using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public partial class Dictionary
    {
        public class Builder
        {
            public Builder()
            {
            }

            public Builder(AffixConfig affix)
            {
                Affix = affix;
            }

            public Dictionary<string, List<DictionaryEntry>> Entries;

            public AffixConfig Affix;

            public Dictionary ToDictionary()
            {
                var affix = Affix ?? new AffixConfig.Builder().ToAffixConfig();

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
                    NGramRestrictedFlags = nGramRestrictedFlags
                };

                var lookupRootBuilder = ImmutableDictionary.CreateBuilder<string, ImmutableArray<DictionaryEntry>>();
                var nGramRestrictedEntriesBuilder = ImmutableHashSet.CreateBuilder<DictionaryEntry>();

                if (Entries != null)
                {
                    foreach (var rootSet in Entries)
                    {
                        var rootWord = rootSet.Key;
                        var rootEntries = rootSet.Value;

                        lookupRootBuilder.Add(rootWord, rootEntries.ToImmutableArray());

                        foreach (var entry in rootEntries)
                        {
                            if (nGramRestrictedFlags.ContainsAny(entry.Flags))
                            {
                                nGramRestrictedEntriesBuilder.Add(entry);
                            }
                        }
                    }
                }

                result.EntriesByRoot = lookupRootBuilder.ToImmutable();
                result.NGramRestrictedEntries = nGramRestrictedEntriesBuilder.ToImmutable();

                return result;
            }
        }
    }
}
