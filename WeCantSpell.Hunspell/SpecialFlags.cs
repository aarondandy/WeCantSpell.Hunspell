namespace WeCantSpell.Hunspell;

public static class SpecialFlags
{
    public static FlagValue DefaultFlags { get; } = new FlagValue(65510);

    public static FlagValue ForbiddenWord { get; } = new FlagValue(65510);

    public static FlagValue OnlyUpcaseFlag { get; } = new FlagValue(65511);

    public static FlagValue LetterF { get; } = new FlagValue('F');

    public static FlagValue LetterG { get; } = new FlagValue('G');

    public static FlagValue LetterH { get; } = new FlagValue('H');

    public static FlagValue LetterI { get; } = new FlagValue('I');

    public static FlagValue LetterJ { get; } = new FlagValue('J');

    public static FlagValue LetterXLower { get; } = new FlagValue('x');

    public static FlagValue LetterCLower { get; } = new FlagValue('c');

    public static FlagValue LetterPercent { get; } = new FlagValue('%');

    public static FlagSet SetFGH { get; } = FlagSet.Create(LetterF, LetterG, LetterH);

    public static FlagSet SetXPercent { get; } = FlagSet.Create(LetterXLower, LetterPercent);
}
