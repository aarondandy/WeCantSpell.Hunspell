using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell
{
    public sealed class AffixEntryWithDetail<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryWithDetail(AffixEntryGroup<TEntry> group, TEntry entry)
        {
            AffixEntry = entry;
            AFlag = group.AFlag;
            Options = group.Options;
        }

        public TEntry AffixEntry { get; }

        public FlagValue AFlag { get; }

        public AffixEntryOptions Options { get; }

        public string Key => AffixEntry.Key;

        public string Append => AffixEntry.Append;

        public string Strip => AffixEntry.Strip;

        public bool HasContClasses => AffixEntry.ContClass.HasItems;

        public CharacterConditionGroup Conditions => AffixEntry.Conditions;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsContClass(FlagValue value) => AffixEntry.ContainsContClass(value);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyContClass(FlagValue a, FlagValue b) => AffixEntry.ContainsAnyContClass(a, b);
    }
}
