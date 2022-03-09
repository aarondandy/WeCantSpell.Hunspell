using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct WordEntryDetail : IEquatable<WordEntryDetail>
{
    public static bool operator ==(WordEntryDetail a, WordEntryDetail b) => a.Equals(b);

    public static bool operator !=(WordEntryDetail a, WordEntryDetail b) => !(a == b);

    public static WordEntryDetail Default { get; } = new WordEntryDetail();

    public WordEntryDetail()
    {
        Flags = FlagSet.Empty;
        Morphs = MorphSet.Empty;
        Options = WordEntryOptions.None;
    }

    public WordEntryDetail(FlagSet flags, MorphSet morphs, WordEntryOptions options)
    {
        Flags = flags;
        Morphs = morphs;
        Options = options;
    }

    public FlagSet Flags { get; }

    public MorphSet Morphs { get; }

    public WordEntryOptions Options { get; }

    public bool HasFlags => Flags.HasItems;

    public bool ContainsFlag(FlagValue flag) => Flags.Contains(flag);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b) => Flags.ContainsAny(a, b);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => Flags.ContainsAny(a, b, c);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => Flags.ContainsAny(a, b, c, d);

    public bool Equals(WordEntryDetail other) =>
        other.Options == Options
        && other.Flags.Equals(Flags)
        && other.Morphs.Equals(Morphs);

    public override bool Equals(object? obj) => obj is WordEntryDetail entry && Equals(entry);

    public override int GetHashCode() => HashCode.Combine(Options, Flags, Morphs);

    internal WordEntry ToEntry(string word) => new(word, this);
}
