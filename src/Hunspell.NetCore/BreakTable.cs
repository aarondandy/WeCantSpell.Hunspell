using System;
using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class BreakTable : ArrayWrapper<string>
    {
        public static readonly BreakTable Empty = TakeArray(ArrayEx<string>.Empty);

        private BreakTable(string[] breaks)
            : base(breaks)
        {
        }

        internal static BreakTable TakeArray(string[] breaks) => breaks == null ? Empty : new BreakTable(breaks);

        public static BreakTable Create(List<string> breaks) => breaks == null ? Empty : TakeArray(breaks.ToArray());

        public static BreakTable Create(IEnumerable<string> breaks) => breaks == null ? Empty : TakeArray(breaks.ToArray());

        /// <summary>
        /// Calculate break points for recursion limit.
        /// </summary>
        public int FindRecursionLimit(string scw)
        {
            int nbr = 0;
            foreach (var breakEntry in items)
            {
                int pos = 0;
                while ((pos = scw.IndexOf(breakEntry, pos, StringComparison.Ordinal)) >= 0)
                {
                    nbr++;
                    pos += breakEntry.Length;
                }
            }

            return nbr;
        }
    }
}
