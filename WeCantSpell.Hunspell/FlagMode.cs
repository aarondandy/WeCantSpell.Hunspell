namespace WeCantSpell.Hunspell
{
    /// <summary>
    /// Indicates the method of encoding used for flag values.
    /// </summary>
    public enum FlagMode : byte
    {
        /// <summary>
        /// Ispell's one-character flags (erfg -> e r f g).
        /// </summary>
        Char,
        /// <summary>
        /// Two-character flags (1x2yZz -> 1x 2y Zz).
        /// </summary>
        Long,
        /// <summary>
        /// Decimal numbers separated by comma (4521,23,233 -> 452123 233).
        /// </summary>
        Num,
        /// <summary>
        /// UTF-8 characters.
        /// </summary>
        Uni
    }
}
