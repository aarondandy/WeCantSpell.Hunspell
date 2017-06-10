#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class WordEntry
    {
        public WordEntry(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
        {
            Word = word ?? string.Empty;
            Flags = flags ?? FlagSet.Empty;
            Morphs = morphs ?? MorphSet.Empty;
            Options = options;
        }

        public string Word { get; }

        public FlagSet Flags { get; }

        public MorphSet Morphs { get; }

        public WordEntryOptions Options { get; }

        public bool HasFlags
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Flags.HasItems;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsFlag(FlagValue flag) => Flags.Contains(flag);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b) => HasFlags && Flags.ContainsAny(a, b);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => HasFlags && Flags.ContainsAny(a, b, c);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => HasFlags && Flags.ContainsAny(a, b, c, d);
    }
}
