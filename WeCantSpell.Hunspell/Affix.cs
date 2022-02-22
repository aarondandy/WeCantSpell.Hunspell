using System;

namespace WeCantSpell.Hunspell;

sealed class Affix<TEntry> where TEntry : AffixEntry
{
    internal Affix(TEntry entry, AffixEntryGroup<TEntry> group) : this(entry, group.AFlag, group.Options)
    {
    }

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
