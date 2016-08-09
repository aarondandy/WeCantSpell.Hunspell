using System;

namespace Hunspell
{
    [Flags]
    public enum AffixEntryOptions : short
    {
        None = 0,

        /// <summary>
        /// Indicates that both prefixes and suffixes can apply to the same subject.
        /// </summary>
        CrossProduct = 1 << 0,

        [Obsolete("This flag should not be unused as UTF16 will be used internally.")]
        Utf8 = 1 << 1,

        AliasF = 1 << 2,

        AliasM = 1 << 3,

        [Obsolete("This flag should be unused because this port should not preallocate arrays of a fixed size.")]
        LongCond = 1 << 4
    }
}
