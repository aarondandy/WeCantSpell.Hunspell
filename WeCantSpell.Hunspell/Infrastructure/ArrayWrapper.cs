using System;
using System.Collections;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

public class ArrayWrapper<T> : IReadOnlyList<T>
{
    protected ArrayWrapper(T[] items)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        IsEmpty = items.Length == 0;
    }

    internal readonly T[] Items;

    public bool IsEmpty { get; }

    public bool HasItems => !IsEmpty;

    public ref readonly T this[int index] => ref Items[index];

    public int Count => Items.Length;

    T IReadOnlyList<T>.this[int index] => Items[index];

    public ReadOnlySpan<T>.Enumerator GetEnumerator() => new ReadOnlySpan<T>(Items).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

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
            if (x is null) return y is null;
            if (y is null) return false;

            return arrayComparer.Equals(x.Items, y.Items);
        }

        public int GetHashCode(TCollection obj) => obj is null ? 0 : arrayComparer.GetHashCode(obj.Items);
    }
}
