using System;
using System.Diagnostics;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Word = {Word}, {Detail}")]
sealed class WordEntry : IEquatable<WordEntry>
{
    public static bool operator ==(WordEntry? a, WordEntry? b) => a is null ? b is null : a.Equals(b);

    public static bool operator !=(WordEntry? a, WordEntry? b) => !(a == b);

    public WordEntry(string word, in WordEntryDetail detail)
    {
        Word = word ?? string.Empty;
        Detail = detail;
    }

    public string Word { get; }

    public WordEntryDetail Detail { get; }

    public MorphSet Morphs => Detail.Morphs;

    public FlagSet Flags => Detail.Flags;

    public WordEntryOptions Options => Detail.Options;

    public bool ContainsFlag(FlagValue flag) => Flags.Contains(flag);

    public bool ContainsAnyFlags(FlagSet flags) => Flags.ContainsAny(flags);

    public bool Equals(WordEntry? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(other.Word, Word, StringComparison.Ordinal)
            && other.Detail.Equals(Detail);
    }

    public override bool Equals(object? obj) => obj is WordEntry entry && Equals(entry);

    public override int GetHashCode() => HashCode.Combine(Word, Detail);
}
