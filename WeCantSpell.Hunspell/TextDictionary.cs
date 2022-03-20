using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

sealed class TextDictionary<TValue> : IEnumerable<KeyValuePair<ReadOnlyMemory<char>, TValue>>
{
    public TextDictionary() : this(5)
    {
    }

    public TextDictionary(int capacity)
    {
        _entries = new Entry[capacity];
        _cellarStartIndex = CalculateCellarStartIndex(capacity);
        _leftoverCursor = _cellarStartIndex;
    }

    private Entry[] _entries;
    private int _cellarStartIndex;
    private int _leftoverCursor;

    public int Count => throw new NotImplementedException();

    public IEnumerable<ReadOnlyMemory<char>> Keys => throw new NotImplementedException();

    public bool ContainsKey(string key) => ContainsKey(key.AsSpan());

    public bool ContainsKey(ReadOnlySpan<char> key)
    {
        return TryGetValue(key, out _);
    }

    public bool TryGetValue(string key, out TValue value) => TryGetValue(key.AsSpan(), out value);

    public bool TryGetValue(ReadOnlySpan<char> key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<ReadOnlyMemory<char>, TValue>> GetEnumerator() => _entries
        .Where(e => e.IsBlank)
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
            return;
        }

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
            // When doing an add, collisions should go into the cellar. If it is full, this will cause a rebuild

            _leftoverCursor = FindEmptyBucketIndex(_entries, _leftoverCursor);
            if (_leftoverCursor < _entries.Length)
            {
                bucket.Next = _leftoverCursor + 1;

                bucket = ref _entries[_leftoverCursor];
                bucket.HashCode = hash;
                bucket.Next = -1;
                bucket.Key = key;
                bucket.Value = value;

                _leftoverCursor++;

                return;
            }
        }

        Rebuild(_entries.Length * 2);

        bucket = ref _entries[hash % _cellarStartIndex];

        if (bucket.IsBlank)
        {
            bucket.HashCode = hash;
            bucket.Next = -1;
            bucket.Key = key;
            bucket.Value = value;
            return;
        }

        FindEndOfChain(_entries, ref bucket);

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

    private uint CalculateHash(ReadOnlyMemory<char> key) =>
#if NO_SPAN_HASHCODE
        unchecked((uint)StringEx.GetHashCode(key.Span));
#else
        unchecked((uint)string.GetHashCode(key.Span));
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

    private static int CalculateCellarStartIndex(int capacity) => (capacity * 86) / 100;

    private static int CalculateBestTableSize(int requiredSize)
    {
        return requiredSize; // TODO: best to use the smallest prime number that fits the required size
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
