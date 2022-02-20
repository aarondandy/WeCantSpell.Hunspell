using System;

namespace WeCantSpell.Hunspell;

[Flags]
public enum WordEntryOptions : byte
{
    /// <summary>
    /// Indicates there is optional morphological data.
    /// </summary>
    None = 0,
    /// <summary>
    /// Using alias compression.
    /// </summary>
    AliasM = 1 << 1,
    /// <summary>
    /// Indicates there is a "ph:" field in the morphological data.
    /// </summary>
    Phon = 1 << 2,
    /// <summary>
    /// Indicates the dictionary word is capitalized.
    /// </summary>
    InitCap = 1 << 3
}
