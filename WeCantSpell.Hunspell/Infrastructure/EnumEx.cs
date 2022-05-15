namespace WeCantSpell.Hunspell.Infrastructure;

static class EnumEx
{
    public static bool HasFlag(this AffixConfigOptions value, AffixConfigOptions flag) => (value & flag) == flag;

    public static bool AllowCross(this AffixEntryOptions value) => HasFlag(value, AffixEntryOptions.CrossProduct);

    public static bool HasFlag(this WordEntryOptions value, WordEntryOptions flag) => (value & flag) == flag;

    public static bool HasFlag(this AffixEntryOptions value, AffixEntryOptions flag) => (value & flag) == flag;

    public static bool HasFlag(this SpellCheckResultType value, SpellCheckResultType flag) => (value & flag) == flag;
}
