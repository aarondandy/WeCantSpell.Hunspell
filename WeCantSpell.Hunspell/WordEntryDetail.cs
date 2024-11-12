﻿using System;
using System.Diagnostics;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Options = {Options}, Flags = {Flags}, Morphs = {Morphs}")]
public readonly struct WordEntryDetail : IEquatable<WordEntryDetail>
{
    public static bool operator ==(WordEntryDetail a, WordEntryDetail b) => a.Equals(b);

    public static bool operator !=(WordEntryDetail a, WordEntryDetail b) => !a.Equals(b);

    public static WordEntryDetail Default { get; } = new WordEntryDetail(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

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

    public bool ContainsAnyFlags(FlagSet flags) => Flags.ContainsAny(flags);

    public bool DoesNotContainFlag(FlagValue flag) => Flags.DoesNotContain(flag);

    public bool DoesNotContainAnyFlags(FlagSet flags) => Flags.DoesNotContainAny(flags);

    public bool Equals(WordEntryDetail other) =>
        other.Options == Options
        && other.Flags.Equals(Flags)
        && other.Morphs.Equals(Morphs);

    public override bool Equals(object? obj) => obj is WordEntryDetail entry && Equals(entry);

    public override int GetHashCode() => HashCode.Combine(Options, Flags, Morphs);

    internal WordEntry ToEntry(string word) => new(word, this);
}
