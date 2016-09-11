using System.Collections.Immutable;

namespace Hunspell
{
    public sealed class DictionaryEntry
    {
        public DictionaryEntry(string word, ImmutableSortedSet<FlagValue> flags, ImmutableArray<string> morphs, DictionaryEntryOptions options)
        {
            Word = word;
            Flags = flags;
            Morphs = morphs;
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

        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d)
        {
            return HasFlags
                &&
                (
                    (a.HasValue && Flags.Contains(a))
                    ||
                    (b.HasValue && Flags.Contains(b))
                    ||
                    (c.HasValue && Flags.Contains(c))
                    ||
                    (d.HasValue && Flags.Contains(d))
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
