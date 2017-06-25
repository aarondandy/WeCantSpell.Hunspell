using System;
using System.Collections;
using System.Collections.Generic;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    public class ArrayWrapper<T> :
#if NO_READONLYCOLLECTIONS
        IEnumerable<T>
#else
        IReadOnlyList<T>
#endif
    {
        internal readonly T[] items;

        protected ArrayWrapper(T[] items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));
            IsEmpty = items.Length == 0;
        }

        public bool IsEmpty { get; }

        public T this[int index]
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => items[index];
        }

        public int Count
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => items.Length;
        }

        public bool HasItems
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => !IsEmpty;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Enumerator GetEnumerator() => new Enumerator(items);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public struct Enumerator
        {
            private readonly T[] values;

            private int index;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public Enumerator(T[] values)
            {
                this.values = values;
                index = -1;
            }

            public T Current
            {
#if !NO_INLINE
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
                get => values[index];
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public bool MoveNext() => (++index) < values.Length;
        }
    }

    public class ArrayWrapperComparer<TValue, TCollection> :
        IEqualityComparer<TCollection>
        where TCollection : ArrayWrapper<TValue>
    {
        public ArrayWrapperComparer() =>
            ArrayComparer = ArrayComparer<TValue>.Default;

        public ArrayWrapperComparer(IEqualityComparer<TValue> valueComparer) =>
            ArrayComparer = new ArrayComparer<TValue>(valueComparer);

        private ArrayComparer<TValue> ArrayComparer { get; }

        public bool Equals(TCollection x, TCollection y) =>
            ArrayComparer.Equals(x.items, y.items);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public int GetHashCode(TCollection obj) =>
            ArrayComparer.GetHashCode(obj.items);
    }
}
