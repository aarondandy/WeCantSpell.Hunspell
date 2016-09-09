using System.Collections.Generic;
using System.Collections.Immutable;

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
                return new Dictionary
                {
                    Entries = Entries == null
                        ? ImmutableDictionary<string, ImmutableArray<DictionaryEntry>>.Empty
                        : Entries.ToImmutableDictionary(pair => pair.Key, pair => pair.Value.ToImmutableArray()),
                    Affix = Affix ?? new AffixConfig.Builder().ToAffixConfig()
                };
            }
        }
    }
}
