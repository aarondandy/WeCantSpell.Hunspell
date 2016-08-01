using System.Collections.Immutable;

namespace Hunspell
{
    public partial class Dictionary
    {
        private Dictionary()
        {
        }

        public ImmutableArray<DictionaryEntry> Entries { get; private set; }
    }
}
