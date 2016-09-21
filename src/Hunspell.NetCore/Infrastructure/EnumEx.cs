using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class EnumEx
    {
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag(this AffixConfigOptions value, AffixConfigOptions flag)
        {
            return (value & flag) != 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag(this WordEntryOptions value, WordEntryOptions flag)
        {
            return (value & flag) != 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag(this AffixEntryOptions value, AffixEntryOptions flag)
        {
            return (value & flag) != 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static bool HasFlag(this AffixReader.EntryListType value, AffixReader.EntryListType flag)
        {
            return (value & flag) != 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag(this SpellCheckResultType value, SpellCheckResultType flag)
        {
            return (value & flag) != 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool HasFlag(this NGramOptions value, NGramOptions flag)
        {
            return (value & flag) != 0;
        }
    }
}
