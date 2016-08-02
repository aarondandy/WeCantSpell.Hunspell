using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public class DictionaryEntry
    {
        public DictionaryEntry(string word, IEnumerable<int> flags, IEnumerable<string> morphs, DictionaryEntryOptions options)
        {
            Word = word;
            Flags = flags == null ? ImmutableArray<int>.Empty : flags.ToImmutableArray();
            Morphs = morphs == null ? ImmutableArray<string>.Empty : morphs.ToImmutableArray();
            Options = options;
        }

        public string Word { get; }

        public ImmutableArray<int> Flags { get; }

        public ImmutableArray<string> Morphs { get; }

        public DictionaryEntryOptions Options { get; }
    }
}
