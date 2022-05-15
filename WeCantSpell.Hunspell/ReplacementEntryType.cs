using System;

namespace WeCantSpell.Hunspell;

/// <remarks>
/// The numeric values assigned to these labels may be used for arithetic so they should not be changed.
/// </remarks>
[Flags]
public enum ReplacementValueType : byte
{
    /// <summary>
    /// Indicates that text can contain the pattern.
    /// </summary>
    Med = 0,
    /// <summary>
    /// Indicates that text can start with the pattern.
    /// </summary>
    Ini = 1,
    /// <summary>
    /// Indicates that text can end with the pattern.
    /// </summary>
    Fin = 2,
    /// <summary>
    /// Indicates that text must match the pattern exactly.
    /// </summary>
    Isol = Ini | Fin
}
