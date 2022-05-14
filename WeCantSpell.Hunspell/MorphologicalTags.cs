namespace WeCantSpell.Hunspell;

public static class MorphologicalTags
{
    public static string Stem { get; } = "st:";
    public static string AlloMorph { get; } = "al:";
    public static string Pos { get; } = "po:";
    public static string DeriPfx { get; } = "dp:";
    public static string InflPfx { get; } = "ip:";
    public static string TermPfx { get; } = "tp:";
    public static string DeriSfx { get; } = "ds:";
    public static string InflSfx { get; } = "is:";
    public static string TermSfx { get; } = "ts:";
    public static string SurfPfx { get; } = "sp:";
    public static string Freq { get; } = "fr:";
    public static string Phon { get; } = "ph:";
    public static string Hyph { get; } = "hy:";
    public static string Part { get; } = "pa:";
    public static string Flag { get; } = "fl:";
    public static string HashEntry { get; } = "_H:";
}
