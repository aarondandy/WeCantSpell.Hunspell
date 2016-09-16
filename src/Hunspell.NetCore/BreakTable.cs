using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public class BreakTable :
        IReadOnlyList<string>
    {
        public static readonly BreakTable Empty = TakeArray(ArrayEx<string>.Empty);

        private readonly string[] breaks;

        private BreakTable(string[] breaks)
        {
            this.breaks = breaks;
        }

        public string this[int index] => breaks[index];

        public int Count => breaks.Length;

        public bool HasBreaks => breaks.Length != 0;

        internal static BreakTable TakeArray(string[] breaks) => new BreakTable(breaks);

        public static BreakTable Create(List<string> breaks) => breaks == null || breaks.Count == 0 ? Empty : TakeArray(breaks.ToArray());

        public static BreakTable Create(IEnumerable<string> breaks) => breaks == null ? Empty : TakeArray(breaks.ToArray());

        /// <summary>
        /// Calculate break points for recursion limit.
        /// </summary>
        public int FindRecursionLimit(string scw)
        {
            int nbr = 0;
            foreach (var breakEntry in breaks)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<string> GetEnumerator() => new FastArrayEnumerator<string>(breaks);

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)breaks).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => breaks.GetEnumerator();
    }
}
