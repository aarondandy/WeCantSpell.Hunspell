using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public interface IAffix
{
    FlagValue AFlag { get; }
    AffixEntryOptions Options { get; }
    string Append { get; }
    string Key { get; }
}

public readonly struct Prefix : IAffix
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
    public string Append => Entry.Append;
    public string Key => Entry.Key;
    public bool AllowCross => EnumEx.HasFlag(Options, AffixEntryOptions.CrossProduct);

    public bool ContainsContClass(FlagValue flag) => Entry.ContainsContClass(flag);
    public bool ContainsAnyContClass(FlagValue a, FlagValue b) => Entry.ContainsAnyContClass(a, b);
    public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => Entry.ContainsAnyContClass(a, b, c);
}

public readonly struct Suffix : IAffix
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
    public string Append => Entry.Append;
    public string Key => Entry.Key;
    public bool AllowCross => EnumEx.HasFlag(Options, AffixEntryOptions.CrossProduct);

    public bool ContainsContClass(FlagValue flag) => Entry.ContainsContClass(flag);
    public bool ContainsAnyContClass(FlagValue a, FlagValue b) => Entry.ContainsAnyContClass(a, b);
    public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => Entry.ContainsAnyContClass(a, b, c);
}
