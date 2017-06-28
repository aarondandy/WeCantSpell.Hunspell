using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class Affix<TEntry>
        where TEntry : AffixEntry
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static Affix<TEntry> Create(TEntry entry, AffixEntryGroup<TEntry> group) =>
            new Affix<TEntry>(entry, group.AFlag, group.Options);

        public Affix(TEntry entry, FlagValue aFlag, AffixEntryOptions options)
        {
            Entry = entry ?? throw new ArgumentNullException(nameof(entry));
            AFlag = aFlag;
            Options = options;
        }

        public TEntry Entry { get; }

        public FlagValue AFlag { get; }

        public AffixEntryOptions Options { get; }
    }
}
