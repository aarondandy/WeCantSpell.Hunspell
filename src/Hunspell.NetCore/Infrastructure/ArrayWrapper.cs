using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    public class ArrayWrapper<T> :
        IReadOnlyList<T>
    {

        protected readonly T[] items;

        protected ArrayWrapper(T[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items;
        }

        public T this[int index]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return items[index];
            }
        }

        public int Count
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return items.Length;
            }
        }

        public bool HasItems
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return items.Length != 0;
            }
        }

        public bool IsEmpty
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return items.Length == 0;
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<T> GetEnumerator() => new FastArrayEnumerator<T>(items);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}
