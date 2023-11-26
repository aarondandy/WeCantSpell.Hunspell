using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class ArrayBuilder<T> : IList<T>
{
    public ArrayBuilder()
    {
        _values = [];
    }

    internal ArrayBuilder(T[] values)
    {
        _values = values;
        Count = values.Length;
    }

    public ArrayBuilder(int initialCapacity)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(initialCapacity, 0);
#else
        if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));
#endif

        _values = initialCapacity == 0 ? [] : new T[initialCapacity];
    }

    private T[] _values;

    public int Count { get; private set; } = 0;

    public int Capacity => _values.Length;

    public bool IsReadOnly => false;

    public void Clear()
    {
        Count = 0;
    }

    public T this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            return _values[index];
        }
        set
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            _values[index] = value;
        }
    }

    public int IndexOf(T item) => Array.IndexOf(_values, item, 0, Count);

    public bool Contains(T item) => IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex) => Array.Copy(_values, 0, array, arrayIndex, Count);

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return _values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T value)
    {
        if (Count >= _values.Length)
        {
            EnsureCapacityAtLeast(Count + 1);
        }

        _values[Count++] = value;
    }

    public void AddRange(IEnumerable<T> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null) throw new ArgumentNullException(nameof(values));
#endif

        foreach (var value in values)
        {
            Add(value);
        }
    }

    public void AddRange(ICollection<T> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null) throw new ArgumentNullException(nameof(values));
#endif

        var futureSize = Count + values.Count;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values, Count);
        Count = futureSize;
    }

    public void AddRange(System.Collections.Immutable.ImmutableArray<T> values)
    {
        var futureSize = Count + values.Length;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values, Count);
        Count = futureSize;
    }

    public void AddRange(T[] values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null) throw new ArgumentNullException(nameof(values));
#endif

        var futureSize = Count + values.Length;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values, Count);
        Count = futureSize;
    }

    public void GrowToCapacity(int requiredLength)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(requiredLength, 0);
#else
        if (requiredLength < 0) throw new ArgumentOutOfRangeException(nameof(requiredLength));
#endif

        if (_values.Length < requiredLength)
        {
            Array.Resize(ref _values, requiredLength);
        }
    }

    public void EnsureCapacityAtLeast(int requiredLength)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(requiredLength, 0);
#else
        if (requiredLength < 0) throw new ArgumentOutOfRangeException(nameof(requiredLength));
#endif

        if (_values.Length < requiredLength)
        {
            Array.Resize(ref _values, CalculateBestCapacity(requiredLength));
        }
    }

    public bool AddAsSortedSet(T value)
    {
        if (Count == 0)
        {
            Add(value);
            return true;
        }

        var insertLocation = Array.BinarySearch(_values, 0, Count, value);
        if (insertLocation >= 0)
        {
            return false;
        }

        insertLocation = ~insertLocation;

        Insert(insertLocation, value);
        return true;
    }

    public void Insert(int insertionIndex, T value)
    {
        if (insertionIndex >= Count)
        {
            Add(value);
            return;
        }

        if (Count >= _values.Length)
        {
            var newArray = new T[CalculateBestCapacity(Count + 1)];

            if (insertionIndex > 0)
            {
                _values.AsSpan(0, insertionIndex).CopyTo(newArray);
            }

            _values.AsSpan(insertionIndex).CopyTo(newArray.AsSpan(insertionIndex + 1));

            _values = newArray;
        }
        else
        {
            for(var i = Count; i > insertionIndex; i--)
            {
                _values[i] = _values[i - 1];
            }
        }

        _values[insertionIndex] = value;

        Count++;
    }

    public void RemoveAt(int index)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
        if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if (Count > 0)
        {
            var newSize = Count - 1;

            if (index < newSize)
            {
                Array.Copy(_values, index + 1, _values, index, newSize - index);
            }

            _values[newSize] = default!;

            Count = newSize;
        }
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public T[] Extract()
    {
#if DEBUG
        if (Count > _values.Length) throw new InvalidOperationException();
#endif

        var result = Count == _values.Length
            ? Interlocked.Exchange(ref _values, Array.Empty<T>())
            : MakeArray();

        Clear();

        return result;
    }

    public void Reverse()
    {
        if (Count > 0)
        {
            Array.Reverse(_values, 0, Count);
        }
    }

    public T[] MakeArray() => Count == 0 ? [] : _values.AsSpan(0, Count).ToArray();

    public T[] MakeOrExtractArray(bool extract) => extract ? Extract() : MakeArray();

    internal int BinarySearch(int startIndex, int count, T value) => Array.BinarySearch(_values, startIndex, count, value);

    private int CalculateBestCapacity(int minCapacity) => Math.Max(CalculateNextCapacity(), minCapacity);

    private int CalculateNextCapacity() => Math.Max(_values.Length * 2, 4);

    internal static class Pool
    {
        private const int MaxCapacity = 20;

        private static ArrayBuilder<T>? Cache;

        public static ArrayBuilder<T> Get()
        {
            if (Interlocked.Exchange(ref Cache, null) is { } taken)
            {
                taken.Clear();
            }
            else
            {
                taken = new();
            }

            return taken;
        }

        public static ArrayBuilder<T> Get(int capacity)
        {
            if (Interlocked.Exchange(ref Cache, null) is { } taken)
            {
                taken.Clear();
                taken.GrowToCapacity(capacity);
            }
            else
            {
                taken = new(capacity);
            }

            return taken;
        }

        public static void Return(ArrayBuilder<T> builder)
        {
            if (builder.Capacity > 0 && builder.Capacity <= MaxCapacity)
            {
                Volatile.Write(ref Cache, builder);
            }
        }

        public static T[] GetArrayAndReturn(ArrayBuilder<T> builder)
        {
            var result = builder.MakeArray();
            Return(builder);
            return result;
        }
    }
}
