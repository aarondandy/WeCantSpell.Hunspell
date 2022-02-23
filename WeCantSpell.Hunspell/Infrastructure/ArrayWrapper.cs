using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell.Infrastructure;

public class ArrayWrapper<T> : IReadOnlyList<T>
{
    protected ArrayWrapper(T[] items) : this(items, canStealArray: false)
    {
    }

    protected ArrayWrapper(T[] items, bool canStealArray)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));

        Items = canStealArray ? items : items.ToArray();
        IsEmpty = Items.Length == 0;
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
            _arrayComparer = ArrayComparer<TValue>.Default;
        }

        private readonly ArrayComparer<TValue> _arrayComparer;

        public bool Equals(TCollection? x, TCollection? y)
        {
            if (x is null) return y is null;
            if (y is null) return false;

            return _arrayComparer.Equals(x.Items, y.Items);
        }

        public int GetHashCode(TCollection obj) => _arrayComparer.GetHashCode(obj.Items);
    }
}
