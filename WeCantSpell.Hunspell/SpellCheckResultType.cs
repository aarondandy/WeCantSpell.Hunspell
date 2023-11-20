using System;

namespace WeCantSpell.Hunspell;

[Flags]
public enum SpellCheckResultType : byte
{
    None = 0,
    /// <summary>
    /// The result is a compound word.
    /// </summary>
    Compound = 1 << 0,
    Forbidden = 1 << 1,
    AllCap = 1 << 2,
    NoCap = 1 << 3,
    InitCap = 1 << 4,
    OrigCap = 1 << 5,
    Warn = 1 << 6,
    /// <summary>
    /// Permit only 2 dictionary words in the compound.
    /// </summary>
    Compound2 = 1 << 7,
}
