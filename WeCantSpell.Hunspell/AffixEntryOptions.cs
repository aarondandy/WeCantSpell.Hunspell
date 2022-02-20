using System;

namespace WeCantSpell.Hunspell;

[Flags]
public enum AffixEntryOptions : byte
{
    None = 0,

    /// <summary>
    /// Indicates that both prefixes and suffixes can apply to the same subject.
    /// </summary>
    CrossProduct = 1 << 0,

    Utf8 = 1 << 1,

    AliasF = 1 << 2,

    AliasM = 1 << 3,

    LongCond = 1 << 4
}
