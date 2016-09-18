using System;

namespace Hunspell
{
    [Flags]
    public enum WordEntryOptions : byte
    {
        None = 0,

        AliasM = 1 << 1,

        Phon = 1 << 2
    }
}
