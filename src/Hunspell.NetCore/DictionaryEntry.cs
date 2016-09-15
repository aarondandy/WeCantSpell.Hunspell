namespace Hunspell
{
    public sealed class DictionaryEntry
    {
        public DictionaryEntry(string word, FlagSet flags, MorphSet morphs, DictionaryEntryOptions options)
        {
            Word = word;
            Flags = flags;
            Morphs = morphs;
            Options = options;
        }

        public string Word { get; }

        public FlagSet Flags { get; }

        public MorphSet Morphs { get; }

        public DictionaryEntryOptions Options { get; }

        public bool HasFlags => Flags.HasFlags;

        public bool ContainsFlag(FlagValue flag) => flag.HasValue && Flags.Contains(flag);

        public bool ContainsAnyFlags(FlagValue a, FlagValue b) => HasFlags && Flags.ContainsAny(a, b);

        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => HasFlags && Flags.ContainsAny(a, b, c);

        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => HasFlags && Flags.ContainsAny(a, b, c, d);
    }
}
