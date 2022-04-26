using System;

namespace WeCantSpell.Hunspell;

readonly struct Affix<TEntry> where TEntry : AffixEntry
{
    public Affix(TEntry entry, FlagValue aFlag, AffixEntryOptions options)
    {
#if DEBUG
        if (entry is null) throw new ArgumentNullException(nameof(entry));
#endif
        Entry = entry;
        AFlag = aFlag;
        Options = options;
    }

    public TEntry Entry { get; }
    public FlagValue AFlag { get; }
    public AffixEntryOptions Options { get; }
    public string Key => Entry.Key;

    public bool ContainsContClass(FlagValue flag) => Entry.ContainsContClass(flag);
}

readonly struct Prefix
{
    public Prefix(PrefixEntry entry, FlagValue aFlag, AffixEntryOptions options)
    {
#if DEBUG
        if (entry is null) throw new ArgumentNullException(nameof(entry));
#endif
        Entry = entry;
        AFlag = aFlag;
        Options = options;
    }

    public PrefixEntry Entry { get; }
    public FlagValue AFlag { get; }
    public AffixEntryOptions Options { get; }
    public string Key => Entry.Key;

    public bool ContainsContClass(FlagValue flag) => Entry.ContainsContClass(flag);
}

readonly struct Suffix
{
    public Suffix(SuffixEntry entry, FlagValue aFlag, AffixEntryOptions options)
    {
#if DEBUG
        if (entry is null) throw new ArgumentNullException(nameof(entry));
#endif
        Entry = entry;
        AFlag = aFlag;
        Options = options;
    }

    public SuffixEntry Entry { get; }
    public FlagValue AFlag { get; }
    public AffixEntryOptions Options { get; }
    public string Key => Entry.Key;

    public bool ContainsContClass(FlagValue flag) => Entry.ContainsContClass(flag);
}
