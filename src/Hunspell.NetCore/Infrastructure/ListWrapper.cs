using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    public class ListWrapper<T> :
        IReadOnlyList<T>
    {
        protected readonly List<T> items;

        protected ListWrapper(List<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items;
            HasItems = items.Count != 0;
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
                return items.Count;
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
            private readonly List<T> values;
            private int index;

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public Enumerator(List<T> values)
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

        public class Comparer : IEqualityComparer<ListWrapper<T>>
        {
            public Comparer()
            {
                ListComparer = ListComparer<T>.Default;
            }

            public Comparer(IEqualityComparer<T> valueComparer)
            {
                ListComparer = new ListComparer<T>(valueComparer);
            }

            private ListComparer<T> ListComparer { get; }

            public bool Equals(ListWrapper<T> x, ListWrapper<T> y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                if (x == null || y == null)
                {
                    return false;
                }

                return ListComparer.Equals(x.items, y.items);
            }

            public int GetHashCode(ListWrapper<T> obj)
            {
                return ListComparer.GetHashCode(obj.items);
            }
        }
    }
}
