using System;

namespace Hunspell
{
    public class PhoneticEntry
    {
        public PhoneticEntry(string item1, string item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        [Obsolete("Name must change")]
        public string Item1 { get; }

        [Obsolete("Name must change")]
        public string Item2 { get; }
    }
}
