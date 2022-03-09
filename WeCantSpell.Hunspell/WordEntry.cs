using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

sealed class WordEntry : IEquatable<WordEntry>
{
    public static bool operator ==(WordEntry? a, WordEntry? b) => a is null ? b is null : a.Equals(b);

    public static bool operator !=(WordEntry? a, WordEntry? b) => !(a == b);

    public WordEntry(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
        : this(word, new WordEntryDetail(flags, morphs, options)) { }

    public WordEntry(string word, WordEntryDetail detail)
    {
        Word = word ?? string.Empty;
        Detail = detail;
    }

    public string Word { get; }

    public WordEntryDetail Detail { get; }

    public bool ContainsFlag(FlagValue flag) => Detail.ContainsFlag(flag);

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
