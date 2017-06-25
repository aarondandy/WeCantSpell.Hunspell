using System;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class WordEntry : IEquatable<WordEntry>
    {
        public static bool operator ==(WordEntry a, WordEntry b) =>
            ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool operator !=(WordEntry a, WordEntry b) => !(a == b);

        public WordEntry(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
            : this(word, new WordEntryDetail(flags, morphs, options)) { }

        public WordEntry(string word, WordEntryDetail detail)
        {
            Word = word ?? string.Empty;
            Detail = detail ?? WordEntryDetail.Default;
        }

        public string Word { get; }

        public WordEntryDetail Detail { get; }

        public FlagSet Flags
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Detail.Flags;
        }

        public MorphSet Morphs
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Detail.Morphs;
        }

        public WordEntryOptions Options
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Detail.Options;
        }

        public bool HasFlags
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Detail.HasFlags;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsFlag(FlagValue flag) => Detail.ContainsFlag(flag);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b) => Detail.ContainsAnyFlags(a, b);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c) => Detail.ContainsAnyFlags(a, b, c);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyFlags(FlagValue a, FlagValue b, FlagValue c, FlagValue d) => Detail.ContainsAnyFlags(a, b, c, d);

        public bool Equals(WordEntry other)
        {
            if (ReferenceEquals(other, null))
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

        public override bool Equals(object obj) =>
            obj is WordEntry entry && Equals(entry);

        public override int GetHashCode() =>
            unchecked(Word.GetHashCode() ^ Detail.GetHashCode());
    }
}
