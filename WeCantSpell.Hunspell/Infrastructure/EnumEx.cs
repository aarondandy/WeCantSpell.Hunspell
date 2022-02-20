#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
static class EnumEx
{
#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool HasFlag(this AffixConfigOptions value, AffixConfigOptions flag) => (value & flag) == flag;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool HasFlag(this WordEntryOptions value, WordEntryOptions flag) => (value & flag) == flag;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool HasFlag(this AffixEntryOptions value, AffixEntryOptions flag) => (value & flag) == flag;

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool HasFlag(this SpellCheckResultType value, SpellCheckResultType flag) => (value & flag) == flag;
}
}
