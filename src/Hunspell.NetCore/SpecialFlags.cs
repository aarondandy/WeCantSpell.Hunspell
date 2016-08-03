using System;

namespace Hunspell
{
    public static class SpecialFlags
    {
        public static readonly int DefaultFlags = 65510;

        [Obsolete("This value appears to be unused")]
        public static readonly int ForbiddenWord = 65510;

        public static readonly int OnlyUpcaseFlag = 65511;
    }
}
