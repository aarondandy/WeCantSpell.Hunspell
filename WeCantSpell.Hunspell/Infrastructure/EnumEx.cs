using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class EnumEx
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlagEx(this AffixConfigOptions value, AffixConfigOptions flag) => (value & flag) == flag;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlagEx(this WordEntryOptions value, WordEntryOptions flag) => (value & flag) == flag;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlagEx(this AffixEntryOptions value, AffixEntryOptions flag) => (value & flag) == flag;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlagEx(this SpellCheckResultType value, SpellCheckResultType flag) => (value & flag) == flag;
}
