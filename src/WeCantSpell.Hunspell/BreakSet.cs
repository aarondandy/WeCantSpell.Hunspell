using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class BreakSet : ArrayWrapper<string>
    {
        public static readonly BreakSet Empty = TakeArray(ArrayEx<string>.Empty);

        internal static BreakSet TakeArray(string[] breaks) => breaks == null ? Empty : new BreakSet(breaks);

        public static BreakSet Create(List<string> breaks) => breaks == null ? Empty : TakeArray(breaks.ToArray());

        public static BreakSet Create(IEnumerable<string> breaks) => breaks == null ? Empty : TakeArray(breaks.ToArray());

        private BreakSet(string[] breaks)
            : base(breaks)
        {
        }

        /// <summary>
        /// Calculate break points for recursion limit.
        /// </summary>
        internal int FindRecursionLimit(string scw)
        {
            int nbr = 0;

            if (!string.IsNullOrEmpty(scw))
            {
                foreach (var breakEntry in items)
                {
                    int pos = 0;
                    while ((pos = scw.IndexOf(breakEntry, pos, StringComparison.Ordinal)) >= 0)
                    {
                        nbr++;
                        pos += breakEntry.Length;
                    }
                }
            }

            return nbr;
        }
    }
}
