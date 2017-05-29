namespace WeCantSpell.Hunspell
{
    public static class SpecialFlags
    {
        public static readonly FlagValue DefaultFlags = new FlagValue(65510);

        public static readonly FlagValue ForbiddenWord = new FlagValue(65510);

        public static readonly FlagValue OnlyUpcaseFlag = new FlagValue(65511);

        public static readonly FlagValue LetterF = new FlagValue('F');

        public static readonly FlagValue LetterG = new FlagValue('G');

        public static readonly FlagValue LetterH = new FlagValue('H');

        public static readonly FlagValue LetterI = new FlagValue('I');

        public static readonly FlagValue LetterJ = new FlagValue('J');

        public static readonly FlagValue LetterXLower = new FlagValue('x');

        public static readonly FlagValue LetterCLower = new FlagValue('c');

        public static readonly FlagValue LetterPercent = new FlagValue('%');
    }
}
