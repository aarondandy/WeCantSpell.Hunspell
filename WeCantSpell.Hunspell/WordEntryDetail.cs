using System;
using System.Diagnostics;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Options = {Options}, Flags = {Flags}, Morphs = {Morphs}")]
public readonly struct WordEntryDetail : IEquatable<WordEntryDetail>
{
    public static bool operator ==(WordEntryDetail a, WordEntryDetail b) => a.Equals(b);

    public static bool operator !=(WordEntryDetail a, WordEntryDetail b) => !(a == b);

    public static WordEntryDetail Default { get; } = new WordEntryDetail();

    public WordEntryDetail()
    {
        Morphs = MorphSet.Empty;
        Flags = FlagSet.Empty;
        Options = WordEntryOptions.None;
    }

    public WordEntryDetail(FlagSet flags, MorphSet morphs, WordEntryOptions options)
    {
        Morphs = morphs;
        Flags = flags;
        Options = options;
    }

    public MorphSet Morphs { get; }

    public FlagSet Flags { get; }

    public WordEntryOptions Options { get; }

    public bool ContainsFlag(FlagValue flag) => Flags.Contains(flag);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b) => Flags.ContainsAny(a, b);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => Flags.ContainsAny(a, b, c);

    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => Flags.ContainsAny(a, b, c, d);

    public bool ContainsAnyFlags(FlagSet set) => Flags.ContainsAny(set);

    public bool Equals(WordEntryDetail other) =>
        other.Options == Options
        && other.Flags.Equals(Flags)
        && other.Morphs.Equals(Morphs);

    public override bool Equals(object? obj) => obj is WordEntryDetail entry && Equals(entry);

    public override int GetHashCode() => HashCode.Combine(Options, Flags, Morphs);

    internal WordEntry ToEntry(string word) => new(word, this);
}
