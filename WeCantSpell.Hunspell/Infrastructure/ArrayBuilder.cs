using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class ArrayBuilder<T> : IList<T>
{
    public ArrayBuilder()
    {
        _values = [];
    }

    public ArrayBuilder(int initialCapacity)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(initialCapacity, 0);
#else
        ExceptionEx.ThrowIfArgumentLessThan(initialCapacity, 0, nameof(initialCapacity));
#endif

        _values = initialCapacity == 0 ? [] : new T[initialCapacity];
    }

    private T[] _values;
    private int _count;

    public int Count => _count;

    public int Capacity => _values.Length;

    bool ICollection<T>.IsReadOnly => false;

    public void Clear()
    {
        _count = 0;
    }

    public T this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, _count, nameof(index));
#endif
            return _values[index];
        }
        set
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, _count, nameof(index));
#endif
            _values[index] = value;
        }
    }

    public int IndexOf(T item) => Array.IndexOf(_values, item, 0, _count);

    public bool Contains(T item) => IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex) => Array.Copy(_values, 0, array, arrayIndex, _count);

    public IEnumerator<T> GetEnumerator() => _values.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T value)
    {
        if (_count >= _values.Length)
        {
            EnsureCapacityAtLeast(_count + 1);
        }

        _values[_count++] = value;
    }

    public void AddRange(IEnumerable<T> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var expectedSize = values.GetNonEnumeratedCountOrDefault();
        if (expectedSize > 0)
        {
            EnsureCapacityAtLeast(_count + expectedSize);
        }

        foreach (var value in values)
        {
            Add(value);
        }
    }

    public void AddRange(T[] values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var futureSize = _count + values.Length;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values, _count);
        _count = futureSize;
    }

    public void AddRange(ReadOnlySpan<T> values)
    {
        var futureSize = _count + values.Length;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values.AsSpan(_count));
        _count = futureSize;
    }

    public void GrowToCapacity(int requiredLength)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(requiredLength, 0);
#else
        ExceptionEx.ThrowIfArgumentLessThan(requiredLength, 0, nameof(requiredLength));
#endif

        if (_values.Length < requiredLength)
        {
            if (_count == 0)
            {
                _values = new T[requiredLength];
            }
            else
            {
                Array.Resize(ref _values, requiredLength);
            }
        }
    }

    public void EnsureCapacityAtLeast(int requiredLength)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(requiredLength, 0);
#else
        ExceptionEx.ThrowIfArgumentLessThan(requiredLength, 0, nameof(requiredLength));
#endif

        if (_values.Length < requiredLength)
        {
            GrowToCapacity(CalculateBestCapacity(requiredLength));
        }
    }

    public bool AddAsSortedSet(T value)
    {
        if (_count == 0)
        {
            Add(value);
            return true;
        }

        var insertLocation = Array.BinarySearch(_values, 0, _count, value);
        if (insertLocation >= 0)
        {
            return false;
        }

        insertLocation = ~insertLocation;

        Insert(insertLocation, value);
        return true;
    }

    public void AddAsSortedSet(IEnumerable<T> values)
    {
        var expectedCount = values.GetNonEnumeratedCountOrDefault();
        if (expectedCount > 0)
        {
            EnsureCapacityAtLeast(_count + expectedCount);
        }

        AddRange(values);
        Array.Sort(_values, 0, _count);
        RemoveAdjacentDuplicates();
    }

    public void AddAsSortedSet(ReadOnlySpan<T> values)
    {
        AddRange(values);
        Array.Sort(_values, 0, _count);
        RemoveAdjacentDuplicates();
    }

    public void Insert(int insertionIndex, T value)
    {
        if (insertionIndex >= _count)
        {
            Add(value);
            return;
        }

        if (_count >= _values.Length)
        {
            var newArray = new T[CalculateBestCapacity(_count + 1)];

            if (insertionIndex > 0)
            {
                _values.AsSpan(0, insertionIndex).CopyTo(newArray);
            }

            _values.AsSpan(insertionIndex).CopyTo(newArray.AsSpan(insertionIndex + 1));

            _values = newArray;
        }
        else
        {
            for(var i = _count; i > insertionIndex; i--)
            {
                _values[i] = _values[i - 1];
            }
        }

        _values[insertionIndex] = value;

        _count++;
    }

    public void RemoveAt(int index)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _count);
#else
        ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
        ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, _count, nameof(index));
#endif

        if (_count > 0)
        {
            var newSize = _count - 1;

            if (index < newSize)
            {
                Array.Copy(_values, index + 1, _values, index, newSize - index);
            }

            _values[newSize] = default!;

            _count = newSize;
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

    private void RemoveAdjacentDuplicates()
    {
        if (_count < 2)
        {
            return;
        }

        var eq = EqualityComparer<T>.Default;
        var i = 1;
        do
        {
            if (eq.Equals(_values[i - 1], _values[i]))
            {
                _values.AsSpan(i + 1, _count - i - 1).CopyTo(_values.AsSpan(i));
                _count--;
            }
            else
            {
                i++;
            }
        }
        while (i < _count);
    }

    public T[] Extract()
    {
#if DEBUG
        if (_count > _values.Length) ExceptionEx.ThrowInvalidOperation();
#endif

        T[] result;
        if (_count == 0)
        {
            result = [];
        }
        else if (_count == _values.Length)
        {
            result = Interlocked.Exchange(ref _values, []);
        }
        else
        {
            result = MakeArray();
        }

        Clear();

        return result;
    }

    public void Reverse()
    {
        if (_count > 0)
        {
            Array.Reverse(_values, 0, _count);
        }
    }

    public T[] MakeArray() => _count == 0 ? [] : AsSpan().ToArray();

    public T[] MakeOrExtractArray(bool extract) => extract ? Extract() : MakeArray();

    internal int BinarySearch(int startIndex, int count, T value) => Array.BinarySearch(_values, startIndex, count, value);

    internal Span<T> AsSpan() => _values.AsSpan(0, _count);

    private int CalculateBestCapacity(int minCapacity) => Math.Max(CalculateNextCapacity(), minCapacity);

    private int CalculateNextCapacity() => Math.Max(_values.Length * 2, 4);

    internal static class Pool
    {
        private const int MaxCapacity = 32; // NOTE: Because of the growth values, this should be a power of two

        private static ArrayBuilder<T>? Cache;

        public static ArrayBuilder<T> Get()
        {
            if (Interlocked.Exchange(ref Cache, null) is { } taken)
            {
                taken.Clear();
            }
            else
            {
                taken = [];
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
            if (builder is { Capacity: <= MaxCapacity })
            {
                Volatile.Write(ref Cache, builder);
            }
        }

        public static T[] ExtractAndReturn(ArrayBuilder<T> builder)
        {
            var result = builder.Extract();
            Return(builder);
            return result;
        }
    }
}
