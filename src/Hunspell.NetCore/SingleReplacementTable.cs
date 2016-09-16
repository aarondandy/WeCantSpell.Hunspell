using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public class SingleReplacementTable :
        IReadOnlyList<SingleReplacementEntry>
    {
        public static readonly SingleReplacementTable Empty = TakeList(new List<SingleReplacementEntry>(0));

        private List<SingleReplacementEntry> replacements;

        private SingleReplacementTable(List<SingleReplacementEntry> replacements)
        {
            this.replacements = replacements;
        }

        public bool HasReplacements => replacements.Count != 0;

        public SingleReplacementEntry this[int index]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return replacements[index];
            }
        }

        public int Count
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return replacements.Count;
            }
        }

        internal static SingleReplacementTable TakeList(List<SingleReplacementEntry> replacements) => new SingleReplacementTable(replacements);

        public static SingleReplacementTable Create(IEnumerable<SingleReplacementEntry> replacements) => TakeList(replacements.ToList());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastListEnumerator<SingleReplacementEntry> GetEnumerator() => new FastListEnumerator<SingleReplacementEntry>(replacements);

        IEnumerator<SingleReplacementEntry> IEnumerable<SingleReplacementEntry>.GetEnumerator() => replacements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => replacements.GetEnumerator();
    }
}
