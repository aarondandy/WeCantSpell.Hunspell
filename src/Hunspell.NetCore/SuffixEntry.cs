using System;
using Hunspell.Utilities;

namespace Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
        public override string Append
        {
            get
            {
                return base.Append;
            }
            protected set
            {
                base.Append = value;
                RAppend = value.Reverse();
            }
        }

        public string RAppend { get; private set; }

        public override string Key => RAppend;
    }
}
