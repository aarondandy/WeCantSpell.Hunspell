using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public class DictionaryEntry
    {
        public DictionaryEntry(string word, IEnumerable<FlagValue> flags, IEnumerable<string> morphs, DictionaryEntryOptions options)
        {
            Word = word;
            Flags = flags == null ? ImmutableArray<FlagValue>.Empty : flags.ToImmutableArray();
            Morphs = morphs == null ? ImmutableArray<string>.Empty : morphs.ToImmutableArray();
            Options = options;
        }

        public string Word { get; }

        public ImmutableArray<FlagValue> Flags { get; }

        public ImmutableArray<string> Morphs { get; }

        public DictionaryEntryOptions Options { get; }

        public bool HasFlags => !Flags.IsDefaultOrEmpty;
    }
}
