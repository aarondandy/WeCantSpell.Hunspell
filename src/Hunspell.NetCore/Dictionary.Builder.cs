using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hunspell
{
    public partial class Dictionary
    {
        public class Builder
        {
            public Dictionary<string, List<DictionaryEntry>> Entries;

            public Dictionary ToDictionary()
            {
                return new Dictionary
                {
                    Entries = EmptyIfNull(Entries).ToImmutableDictionary(pair => pair.Key, pair => pair.Value.ToImmutableArray())
                };
            }

            private static IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> items)
            {
                return items == null ? Enumerable.Empty<T>() : items;
            }
        }
    }
}
