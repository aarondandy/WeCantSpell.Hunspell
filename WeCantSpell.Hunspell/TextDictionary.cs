using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

sealed class TextDictionary<TValue> : IEnumerable<KeyValuePair<ReadOnlyMemory<char>, TValue>>
{
    const int MinimumCapacity = 3;

    // These prime values are taken from the framework internals, so I'm sure there are good reasons to use them
    private static readonly int[] PrimeSizes = new[]
    {
        3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
        1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
        17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
        187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
        1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369
    };

    public TextDictionary() : this(MinimumCapacity)
    {
    }

    public TextDictionary(int capacity)
    {
        if (capacity < MinimumCapacity)
        {
            capacity = MinimumCapacity;
        }

        _entries = new Entry[capacity];
        _cellarStartIndex = CalculateCellarStartIndex(capacity);
        _leftoverCursor = _cellarStartIndex;
        Count = 0;
    }

    private Entry[] _entries;
    private int _cellarStartIndex;
    private int _leftoverCursor;

    public int Count { get; private set; }

    public IEnumerable<ReadOnlyMemory<char>> Keys => _entries
        .Where(e => !e.IsBlank)
        .Select(e => e.Key);

    public bool ContainsKey(string key) => ContainsKey(key.AsSpan());

    public bool ContainsKey(ReadOnlyMemory<char> key) => ContainsKey(key.Span);

    public bool ContainsKey(ReadOnlySpan<char> key) => TryGetValue(key, out _);

    public bool TryGetValue(string key, out TValue value) => TryGetValue(key.AsSpan(), out value);

    public bool TryGetValue(ReadOnlyMemory<char> key, out TValue value) => TryGetValue(key.Span, out value);

    public bool TryGetValue(ReadOnlySpan<char> key, out TValue value)
    {
        var hash = CalculateHash(key);

        ref var bucket = ref _entries[hash % _cellarStartIndex];
        while (true)
        {
            if (bucket.HashCode == hash && CheckKeysEqual(bucket.Key.Span, key))
            {
                value = bucket.Value;
                return true;
            }

            if (bucket.Next <= 0)
            {
                value = default!;
                return false;
            }

            bucket = ref _entries[bucket.Next - 1];
        }
    }

    public IEnumerator<KeyValuePair<ReadOnlyMemory<char>, TValue>> GetEnumerator() => _entries
        .Where(e => !e.IsBlank)
        .Select(e => new KeyValuePair<ReadOnlyMemory<char>, TValue>(e.Key, e.Value))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(string key, TValue value)
    {
        Add(key.AsMemory(), value);
    }

    public void Add(ReadOnlyMemory<char> key, TValue value)
    {
        var hash = CalculateHash(key);

        ref var bucket = ref _entries[hash % _cellarStartIndex];
        if (bucket.IsBlank)
        {
            bucket.HashCode = hash;
            bucket.Next = -1;
            bucket.Key = key;
            bucket.Value = value;

            Count++;

            return;
        }

        // search through the chain to ensure there are no collisions
        while (true)
        {
            if (bucket.HashCode == hash && CheckKeysEqual(bucket.Key.Span, key.Span))
            {
                throw new InvalidOperationException("Duplicate key");
            }

            if (bucket.Next <= 0)
            {
                break;
            }

            bucket = ref _entries[bucket.Next - 1];
        }

        if (_leftoverCursor >= _cellarStartIndex)
        {
            // When doing an add, collisions should go into the cellar. If it is full, this will require a rebuild

            _leftoverCursor = FindEmptyBucketIndex(_entries, _leftoverCursor);
            if (_leftoverCursor < _entries.Length)
            {
                bucket.Next = _leftoverCursor + 1;

                bucket = ref _entries[_leftoverCursor];
                bucket.HashCode = hash;
                bucket.Next = -1;
                bucket.Key = key;
                bucket.Value = value;

                Count++;
                _leftoverCursor++;

                return;
            }
        }

        Rebuild(Count * 2);

        bucket = ref _entries[hash % _cellarStartIndex];

        if (bucket.IsBlank)
        {
            bucket.HashCode = hash;
            bucket.Next = -1;
            bucket.Key = key;
            bucket.Value = value;

            Count++;

            return;
        }

        bucket = ref FindEndOfChain(_entries, ref bucket);

        _leftoverCursor = FindEmptyBucketIndex(_entries, _leftoverCursor);
        if (_leftoverCursor >= _entries.Length)
        {
            _leftoverCursor = 0;

            // because a rebuild has already happened, try to fit the entry in the normal area
            _leftoverCursor = FindEmptyBucketIndex(_entries.AsSpan(0, _cellarStartIndex), _leftoverCursor);
            if (_leftoverCursor >= _cellarStartIndex)
            {
                throw new NotSupportedException();
            }
        }

        bucket.Next = _leftoverCursor + 1;

        bucket = ref _entries[_leftoverCursor];
        bucket.HashCode = hash;
        bucket.Next = -1;
        bucket.Key = key;
        bucket.Value = value;

        Count++;
        _leftoverCursor++;
    }

    private void Rebuild(int minCapacity)
    {
        var newSize = CalculateBestTableSize(Math.Max(_entries.Length, minCapacity));
        var newEntries = new Entry[newSize];
        var newCellarStartIndex = CalculateCellarStartIndex(newEntries.Length);

        var leftovers = new List<Entry>();
        foreach (var entry in _entries)
        {
            if (entry.IsBlank)
            {
                continue;
            }

            ref var bucket = ref newEntries[entry.HashCode % newCellarStartIndex];
            if (bucket.IsBlank)
            {
                bucket.HashCode = entry.HashCode;
                bucket.Next = -1;
                bucket.Key = entry.Key;
                bucket.Value = entry.Value;
            }
            else
            {
                leftovers.Add(entry);
            }
        }

        var newLeftoverCursor = newCellarStartIndex;

        foreach (var entry in leftovers)
        {
            ref var bucket = ref FindEndOfChain(newEntries, ref newEntries[entry.HashCode % newCellarStartIndex]);

            if (newLeftoverCursor >= newCellarStartIndex)
            {
                newLeftoverCursor = FindEmptyBucketIndex(newEntries, newLeftoverCursor);

                if (newLeftoverCursor >= newEntries.Length)
                {
                    newLeftoverCursor = FindEmptyBucketIndex(newEntries.AsSpan(0, newCellarStartIndex), 0);
                    if (newLeftoverCursor >= newCellarStartIndex)
                    {
                        throw new NotSupportedException();
                    }
                }
            }
            else
            {
                newLeftoverCursor = FindEmptyBucketIndex(newEntries.AsSpan(0, newCellarStartIndex), newLeftoverCursor);
                if (newLeftoverCursor >= newCellarStartIndex)
                {
                    throw new NotSupportedException();
                }
            }

            bucket.Next = newLeftoverCursor + 1;

            bucket = ref newEntries[newLeftoverCursor];
            bucket.HashCode = entry.HashCode;
            bucket.Next = -1;
            bucket.Key = entry.Key;
            bucket.Value = entry.Value;

            newLeftoverCursor++;
        }

        _entries = newEntries;
        _cellarStartIndex = newCellarStartIndex;
        _leftoverCursor = newLeftoverCursor >= newCellarStartIndex ? newLeftoverCursor : newEntries.Length;
    }

    private uint CalculateHash(ReadOnlyMemory<char> key) => CalculateHash(key.Span);

    private uint CalculateHash(ReadOnlySpan<char> key) =>
#if NO_SPAN_HASHCODE
        unchecked((uint)StringEx.GetHashCode(key));
#else
        unchecked((uint)string.GetHashCode(key));
#endif

    private bool CheckKeysEqual(ReadOnlySpan<char> a, ReadOnlySpan<char> b)
    {
        return a.Equals(b, StringComparison.Ordinal);
    }

    private static ref Entry FindEndOfChain(Entry[] entries, ref Entry entry)
    {
        while (entry.Next > 0)
        {
            entry = ref entries[entry.Next - 1];
        }

        return ref entry;
    }

    private static int FindEmptyBucketIndex(Entry[] entries, int startIndex)
    {
        for (; startIndex < entries.Length && !entries[startIndex].IsBlank; startIndex++) ;
        return startIndex;
    }

    private static int FindEmptyBucketIndex(Span<Entry> entries, int startIndex)
    {
        for (; startIndex < entries.Length && !entries[startIndex].IsBlank; startIndex++) ;
        return startIndex;
    }

    private static int CalculateCellarStartIndex(int capacity) =>
        // The ratio 0.86 has been found to be a good one, I guess
        (capacity * 86) / 100;

    private static int CalculateBestTableSize(int requiredSize)
    {
        foreach (var value in PrimeSizes)
        {
            if (value >= requiredSize)
            {
                return value;
            }
        }

        return requiredSize;
    }

    private struct Entry
    {
        public uint HashCode;
        public int Next; // 0 is for uninitialized, -1 is for explicit terminal, > 0 for the next 1-based index
        public ReadOnlyMemory<char> Key;
        public TValue Value;

        public bool IsBlank =>
            // The idea with this logic is that the condition for a blank entry and a default (cleared) entry should be the same
            HashCode == default && Next == default;
    }
}
