using System;

namespace Hunspell
{
    [Flags]
    public enum NGramOptions
    {
        LongerWorse = 1 << 0,
        AnyMismatch = 1 << 1,
        Lowering = 1 << 2,
        Weighted = 1 << 3
    }
}
