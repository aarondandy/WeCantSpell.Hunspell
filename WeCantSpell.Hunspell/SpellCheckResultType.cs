using System;

namespace WeCantSpell.Hunspell
{
    [Flags]
    public enum SpellCheckResultType : byte
    {
        None = 0,
        Compound = 1 << 0,
        Forbidden = 1 << 1,
        AllCap = 1 << 2,
        NoCap = 1 << 3,
        InitCap = 1 << 4,
        OrigCap = 1 << 5,
        Warn = 1 << 6
    }
}
