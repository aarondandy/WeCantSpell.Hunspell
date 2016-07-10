using System;

namespace Hunspell
{
    [Flags]
    public enum AffixEntryOptions
    {
        None = 0,

        /// <summary>
        /// Indicates that both prefixes and suffixes can apply to the same subject.
        /// </summary>
        CrossProduct = 1,

        [Obsolete("This flag should be unused as UTF16 will be used internally.")]
        Utf8 = 2,

        AliasF = 4,

        AliasM = 8,

        [Obsolete("This flag should be unused because this library should not preallocate arrays.")]
        LongCond = 16
    }
}
