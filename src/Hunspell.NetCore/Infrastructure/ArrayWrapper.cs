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
            HasItems = items.Length != 0;
            IsEmpty = !HasItems;
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

        public bool HasItems { get; }

        public bool IsEmpty { get; }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Enumerator GetEnumerator() => new Enumerator(items);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public struct Enumerator
        {
            private readonly T[] values;
            private int index;

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public Enumerator(T[] values)
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
            public bool MoveNext() => ++index < values.Length;
        }

        public class Comparer : IEqualityComparer<ArrayWrapper<T>>
        {
            public Comparer()
            {
                ArrayComparer = ArrayComparer<T>.Default;
            }

            public Comparer(IEqualityComparer<T> valueComparer)
            {
                ArrayComparer = new ArrayComparer<T>(valueComparer);
            }

            private ArrayComparer<T> ArrayComparer { get; }

            public bool Equals(ArrayWrapper<T> x, ArrayWrapper<T> y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                if (x == null || y == null)
                {
                    return false;
                }

                return ArrayComparer.Equals(x.items, y.items);
            }

            public int GetHashCode(ArrayWrapper<T> obj)
            {
                return ArrayComparer.GetHashCode(obj.items);
            }
        }
    }
}
