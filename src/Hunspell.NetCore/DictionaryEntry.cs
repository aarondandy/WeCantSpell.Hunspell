using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public class DictionaryEntry
    {
        public DictionaryEntry(string word, IEnumerable<FlagValue> flags, IEnumerable<string> morphs, DictionaryEntryOptions options)
        {
            Word = word;
            Flags = flags == null ? ImmutableSortedSet<FlagValue>.Empty : flags.ToImmutableSortedSet();
            Morphs = morphs == null ? ImmutableArray<string>.Empty : morphs.ToImmutableArray();
            Options = options;
        }

        public string Word { get; }

        public ImmutableSortedSet<FlagValue> Flags { get; }

        public ImmutableArray<string> Morphs { get; }

        public DictionaryEntryOptions Options { get; }

        public bool HasFlags => !Flags.IsEmpty;

        public bool ContainsFlag(FlagValue flag) => flag.HasValue && Flags.Contains(flag);

        public bool ContainsAnyFlags(FlagValue a, FlagValue b)
        {
            return HasFlags
                &&
                (
                    (a.HasValue && Flags.Contains(a))
                    ||
                    (b.HasValue && Flags.Contains(b))
                );
        }

        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c)
        {
            return HasFlags
                &&
                (
                    (a.HasValue && Flags.Contains(a))
                    ||
                    (b.HasValue && Flags.Contains(b))
                    ||
                    (c.HasValue && Flags.Contains(c))
                );
        }

        public bool ContainsAnyFlags(params FlagValue[] flags)
        {
            if (!HasFlags || flags == null || flags.Length == 0)
            {
                return false;
            }

            for (var i = 0; i < flags.Length; i++)
            {
                var flag = flags[i];
                if (flag.HasValue && Flags.Contains(flag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
