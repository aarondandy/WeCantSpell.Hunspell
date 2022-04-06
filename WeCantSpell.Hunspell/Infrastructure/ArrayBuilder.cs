﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

public class ArrayBuilder<T>
{
    public ArrayBuilder()
    {
        _values = Array.Empty<T>();
    }

    internal ArrayBuilder(T[] values)
    {
        _values = values;
        Count = values.Length;
    }

    public ArrayBuilder(int initialCapacity)
    {
#if DEBUG
        if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));
#endif
        _values = initialCapacity == 0 ? Array.Empty<T>() : new T[initialCapacity];
    }

    private T[] _values;

    public int Count { get; private set; } = 0;

    public int Capacity => _values.Length;

    public void Clear()
    {
        Count = 0;
    }

    public T this[int index]
    {
        get => index >= 0 && index < Count ? _values[index] : throw new ArgumentOutOfRangeException(nameof(index));
        set
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            _values[index] = value;
        }
    }

    public void Add(T value)
    {
        if (Count >= _values.Length)
        {
            EnsureCapacityAtLeast(Count + 1);
        }

        _values[Count++] = value;
    }

    public void AddRange(ICollection<T> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

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
        if (values is null) throw new ArgumentNullException(nameof(values));

        var futureSize = Count + values.Length;
        EnsureCapacityAtLeast(futureSize);
        values.CopyTo(_values, Count);
        Count = futureSize;
    }

    public void GrowToCapacity(int requiredLength)
    {
        if (requiredLength < 0) throw new ArgumentOutOfRangeException(nameof(requiredLength));

        if (_values.Length < requiredLength)
        {
            Array.Resize(ref _values, requiredLength);
        }
    }

    public void EnsureCapacityAtLeast(int requiredLength)
    {
        if (requiredLength < 0) throw new ArgumentOutOfRangeException(nameof(requiredLength));

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

    internal int BinarySearch(int startIndex, int count, T value)
    {
#if DEBUG
        if (startIndex < 0 || startIndex >= Count) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex + count > Count) throw new ArgumentOutOfRangeException(nameof(count));
#endif

        return Array.BinarySearch(_values, startIndex, count, value);
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

    public T[] MakeArray() => Count == 0 ? Array.Empty<T>() : _values.AsSpan(0, Count).ToArray();

    public T[] MakeOrExtractArray(bool extract) => extract ? Extract() : MakeArray();

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
#if DEBUG
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
#endif

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
#if DEBUG
            if (builder is null) throw new ArgumentNullException(nameof(builder));
#endif

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