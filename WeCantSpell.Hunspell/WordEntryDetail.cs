using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public class WordEntryDetail : IEquatable<WordEntryDetail>
{
    public static bool operator ==(WordEntryDetail a, WordEntryDetail b) => a is null ? b is null : a.Equals(b);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool operator !=(WordEntryDetail a, WordEntryDetail b) => !(a == b);

    public static WordEntryDetail Default { get; } = new WordEntryDetail(FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

    public WordEntryDetail(FlagSet flags, MorphSet morphs, WordEntryOptions options)
    {
        Flags = flags ?? FlagSet.Empty;
        Morphs = morphs ?? MorphSet.Empty;
        Options = options;
    }

    public FlagSet Flags { get; }

    public MorphSet Morphs { get; }

    public WordEntryOptions Options { get; }

    public bool HasFlags
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        get => Flags.HasItems;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool ContainsFlag(FlagValue flag) => Flags.Contains(flag);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool ContainsAnyFlags(FlagValue a, FlagValue b) => Flags.ContainsAny(a, b);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => Flags.ContainsAny(a, b, c);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => Flags.ContainsAny(a, b, c, d);

    public bool Equals(WordEntryDetail other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return other.Options == Options
            && other.Flags.Equals(Flags)
            && other.Morphs.Equals(Morphs);
    }

    public override bool Equals(object obj) => Equals(obj as WordEntryDetail);

    public override int GetHashCode() =>
        unchecked(Flags.GetHashCode() ^ Morphs.GetHashCode() ^ Options.GetHashCode());

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    internal WordEntry ToEntry(string word) => new WordEntry(word, this);
}
