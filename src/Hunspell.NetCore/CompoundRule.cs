using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hunspell
{
    public sealed class CompoundRule :
        IReadOnlyList<FlagValue>
    {
        private FlagValue[] values;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private CompoundRule(FlagValue[] values)
        {
            this.values = values;
        }

        public FlagValue this[int index] => values[index];

        public int Count => values.Length;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static CompoundRule TakeArray(FlagValue[] values) => new CompoundRule(values);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static CompoundRule Create(List<FlagValue> values) => TakeArray(values.ToArray());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public IEnumerator<FlagValue> GetEnumerator() => ((IEnumerable<FlagValue>)values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
