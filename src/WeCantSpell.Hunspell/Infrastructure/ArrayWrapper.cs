using System;
using System.Collections;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    public class ArrayWrapper<T> : IReadOnlyList<T>
    {
        protected ArrayWrapper(T[] items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));
            IsEmpty = items.Length == 0;
        }

        internal readonly T[] items;

        public bool IsEmpty { get; }

        public bool HasItems => !IsEmpty;

        public ref readonly T this[int index] => ref items[index];

        public int Count => items.Length;

        T IReadOnlyList<T>.this[int index] => items[index];

        public ReadOnlySpan<T>.Enumerator GetEnumerator() => new ReadOnlySpan<T>(items).GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public class ArrayWrapperComparer<TValue, TCollection> :
            IEqualityComparer<TCollection>
            where TCollection : ArrayWrapper<TValue>
            where TValue : IEquatable<TValue>
        {
            public ArrayWrapperComparer()
            {
                arrayComparer = ArrayComparer<TValue>.Default;
            }

            private readonly ArrayComparer<TValue> arrayComparer;

            public bool Equals(TCollection x, TCollection y)
            {
                if (x == null)
                {
                    return y == null;
                }
                if (y == null)
                {
                    return false;
                }

                return arrayComparer.Equals(x.items, y.items);
            }

            public int GetHashCode(TCollection obj) => obj == null ? 0 : arrayComparer.GetHashCode(obj.items);
        }
    }
}
