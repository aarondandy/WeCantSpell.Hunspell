using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    public struct FastListEnumerator<T>
    {
        private readonly List<T> values;
        private int index;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastListEnumerator(List<T> values)
        {
            this.values = values;
            index = -1;
        }

        public T Current
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return values[index];
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool MoveNext() => ++index < values.Count;
    }
}
