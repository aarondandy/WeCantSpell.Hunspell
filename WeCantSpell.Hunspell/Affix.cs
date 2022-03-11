using System;

namespace WeCantSpell.Hunspell;

sealed class Affix<TEntry> where TEntry : AffixEntry
{
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
