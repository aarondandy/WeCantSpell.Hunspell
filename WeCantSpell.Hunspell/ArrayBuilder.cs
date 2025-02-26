using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}, Capacity = {Capacity}")]
internal sealed class ArrayBuilder<T> : IList<T>
{
    private const int MaxCapacity = 0X7FFFFFC7;

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

    public IEnumerator<T> GetEnumerator() => new ArraySegment<T>(_values, 0, _count).AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T value)
    {
        if (_count >= _values.Length)
        {
            EnsureCapacity(_count + 1);
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
            EnsureCapacity(_count + expectedSize);
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
        EnsureCapacity(futureSize);
        values.CopyTo(_values, _count);
        _count = futureSize;
    }

    public void AddRange(ReadOnlySpan<T> values)
    {
        var futureSize = _count + values.Length;
        EnsureCapacity(futureSize);
        values.CopyTo(_values.AsSpan(_count));
        _count = futureSize;
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
            var newArray = new T[CalculateBestGrowthCapacity(_count + 1)];

            if (insertionIndex > 0)
            {
                _values.AsSpan(0, insertionIndex).CopyTo(newArray);
            }

            _values.AsSpan(insertionIndex).CopyTo(newArray.AsSpan(insertionIndex + 1));

            _values = newArray;
        }
        else
        {
            for (var i = _count; i > insertionIndex; i--)
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

    public void EnsureCapacity(int requiredCapacity)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(requiredCapacity, 0);
#else
        ExceptionEx.ThrowIfArgumentLessThan(requiredCapacity, 0, nameof(requiredCapacity));
#endif

        if (_values.Length < requiredCapacity)
        {
            SetCapacity(CalculateBestGrowthCapacity(requiredCapacity));
        }
    }

    public void Reverse()
    {
        if (_count > 0)
        {
            Array.Reverse(_values, 0, _count);
        }
    }

    public T[] ToArray() => _count == 0 ? [] : _values.AsSpan(0, _count).ToArray();

    public void SetCapacity(int desiredCapacity)
    {
#if HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(desiredCapacity, 0);
#else
        ExceptionEx.ThrowIfArgumentLessThan(desiredCapacity, 0, nameof(desiredCapacity));
#endif

        if (_values.Length == desiredCapacity)
        {
            return;
        }

        if (_count == 0)
        {
            _values = new T[desiredCapacity];
            return;
        }

        var bestCapacity = Math.Max(_count, desiredCapacity);
        if ((uint)bestCapacity > MaxCapacity)
        {
            bestCapacity = MaxCapacity;
        }

        if (_values.Length < bestCapacity)
        {
            Array.Resize(ref _values, desiredCapacity);
        }
    }

    internal T[] Extract()
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
            result = ToArray();
        }

        Clear();

        return result;
    }

    internal T[] MakeOrExtractArray(bool extract) => extract ? Extract() : ToArray();

    private int CalculateBestGrowthCapacity(int minCapacity)
    {
        var result = Math.Max(Math.Max(_values.Length * 2, 4), minCapacity);

        if ((uint)result > MaxCapacity)
        {
            result = MaxCapacity;
        }

        return result;
    }

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
                taken.SetCapacity(capacity);
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
