using System.Runtime.CompilerServices;

namespace Hunspell
{
    public class AffixEntryWithDetail<TEntry>
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

        public bool HasContClasses => AffixEntry.HasContClasses;

        public CharacterConditionGroup Conditions => AffixEntry.Conditions;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsContClass(FlagValue value) => AffixEntry.ContainsContClass(value);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyContClass(FlagValue a, FlagValue b) => AffixEntry.ContainsAnyContClass(a, b);
    }
}
