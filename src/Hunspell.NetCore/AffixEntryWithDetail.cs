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

        public bool ContainsContClass(FlagValue value) => AffixEntry.ContainsContClass(value);

        public bool ContainsAnyContClass(FlagValue a, FlagValue b) => AffixEntry.ContainsAnyContClass(a, b);
    }
}
