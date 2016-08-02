using System.Collections.Immutable;

namespace Hunspell
{
    public partial class Dictionary
    {
        private Dictionary()
        {
        }

        public ImmutableDictionary<string, ImmutableArray<DictionaryEntry>> Entries { get; private set; }

        public AffixConfig Affix { get; private set; }
    }
}
