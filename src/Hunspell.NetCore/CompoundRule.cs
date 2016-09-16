using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CompoundRule :
        IReadOnlyList<FlagValue>
    {
        private FlagValue[] values;

        private CompoundRule(FlagValue[] values)
        {
            this.values = values;
        }

        public FlagValue this[int index] => values[index];

        public int Count => values.Length;

        internal static CompoundRule TakeArray(FlagValue[] values) => new CompoundRule(values);

        public static CompoundRule Create(List<FlagValue> values) => TakeArray(values.ToArray());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<FlagValue> GetEnumerator() => new FastArrayEnumerator<FlagValue>(values);

        IEnumerator<FlagValue> IEnumerable<FlagValue>.GetEnumerator() => ((IEnumerable<FlagValue>)values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
