using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

sealed class TextDictionary<TValue> : IEnumerable<KeyValuePair<string, TValue>>
{
    // These prime values are taken from the framework internals, so I'm sure there are good reasons to use them
    private static readonly int[] PrimeSizes = new[]
    {
        3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
        1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
        17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
        187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
        1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369
    };

    public TextDictionary() : this(0)
    {
    }

    public TextDictionary(int desiredCapacity)
    {
        if (desiredCapacity < 1)
        {
            desiredCapacity = 1;
        }

        _entries = new Entry[desiredCapacity];
        _cellarStartIndex = CalculateBestCellarIndexForCapacity(_entries.Length);
        _collisionIndex = _entries.Length - 1;
        Count = 0;
    }

    private Entry[] _entries;
    private int _cellarStartIndex;
    private int _collisionIndex;

    public int Count { get; private set; }

    public IEnumerable<string> Keys => FilledEntries.Select(static e => e.Key);

    public IEnumerable<TValue> Values => FilledEntries.Select(static e => e.Value);

    private IEnumerable<Entry> FilledEntries => _entries.Where(static e => !e.IsBlank);

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<KeyValuePair<string, TValue>> IEnumerable<KeyValuePair<string, TValue>>.GetEnumerator() => FilledEntries
        .Select(static e => new KeyValuePair<string, TValue>(e.Key, e.Value))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.AsEnumerable().GetEnumerator();

    public bool ContainsKey(string key) => ContainsKey(key.AsSpan());

    public bool ContainsKey(ReadOnlyMemory<char> key) => ContainsKey(key.Span);

    public bool ContainsKey(ReadOnlySpan<char> key) => TryGetValue(key, out _);

    public bool TryGetValue(string key, out TValue value) => TryGetValue(key.AsSpan(), out value);

    public bool TryGetValue(ReadOnlyMemory<char> key, out TValue value) => TryGetValue(key.Span, out value);

    public bool TryGetValue(ReadOnlySpan<char> key, out TValue value)
    {
        var hash = TextDictionary<TValue>.CalculateHash(key);

        ref var entry = ref _entries[hash % (uint)_cellarStartIndex];

        if (entry.IsOccupied)
        {
            while (true)
            {
                if (entry.HashCode == hash && TextDictionary<TValue>.CheckKeysEqual(entry.Key.AsSpan(), key))
                {
                    value = entry.Value;
                    return true;
                }

                if (entry.Next < 0)
                {
                    break;
                }

                entry = ref _entries[entry.Next];
            }
        }

        value = default!;
        return false;
    }

    public void Add(string key, TValue value)
    {
        var hash = TextDictionary<TValue>.CalculateHash(key);

        ref var bucket = ref _entries[hash % (uint)_cellarStartIndex];
        if (bucket.IsBlank)
        {
            bucket.Set(hash, key, value);

            Count++;

            return;
        }

        // search through the chain to ensure there are no collisions
        while (true)
        {
            if (bucket.HashCode == hash && TextDictionary<TValue>.CheckKeysEqual(bucket.Key.AsSpan(), key.AsSpan()))
            {
                throw new InvalidOperationException("Duplicate key");
            }

            if (bucket.Next < 0)
            {
                break;
            }

            bucket = ref _entries[bucket.Next];
        }

        if (Count < _entries.Length)
        {
            _collisionIndex = FindNextCollisionLocation(_entries, _collisionIndex);

            if (_collisionIndex >= 0)
            {
                bucket.Next = _collisionIndex;

                bucket = ref _entries[_collisionIndex];
                bucket.Set(hash, key, value);

                Count++;
                _collisionIndex--;

                return;
            }
        }

        Rebuild(_entries.Length * 2);

        bucket = ref _entries[hash % (uint)_cellarStartIndex];

        if (bucket.IsBlank)
        {
            bucket.Set(hash, key, value);

            Count++;

            return;
        }

        bucket = ref FindEndOfChain(_entries, ref bucket);

        _collisionIndex = FindNextCollisionLocation(_entries, _collisionIndex);
        if (_collisionIndex < 0)
        {
            throw new NotSupportedException();
        }

        bucket.Next = _collisionIndex;

        bucket = ref _entries[_collisionIndex];
        bucket.Set(hash, key, value);

        Count++;
        _collisionIndex--;
    }

    private void Rebuild(int desiredCapacity)
    {
        var newSize = Math.Max(desiredCapacity, Count);
        var oldEntries = _entries;
        var newEntries = new Entry[newSize];
        var newCellarStartIndex = CalculateBestCellarIndexForCapacity(newEntries.Length);
        var newCollisionIndex = newEntries.Length - 1;

        var leftovers = new List<Entry>();
        foreach (var entry in oldEntries)
        {
            if (entry.IsBlank)
            {
                continue;
            }

            ref var bucket = ref newEntries[entry.HashCode % (uint)newCellarStartIndex];
            if (bucket.IsBlank)
            {
                bucket.Set(entry.HashCode, entry.Key, entry.Value);
            }
            else
            {
                leftovers.Add(entry);
            }
        }

        foreach (var entry in leftovers)
        {
            ref var bucket = ref FindEndOfChain(newEntries, ref newEntries[entry.HashCode % (uint)newCellarStartIndex]);

            newCollisionIndex = FindNextCollisionLocation(newEntries, newCollisionIndex);

            if (newCollisionIndex < 0)
            {
                throw new NotSupportedException();
            }

            bucket.Next = newCollisionIndex;

            bucket = ref newEntries[newCollisionIndex];
            bucket.Set(entry.HashCode, entry.Key, entry.Value);

            newCollisionIndex--;
        }

        _entries = newEntries;
        _cellarStartIndex = newCellarStartIndex;
        _collisionIndex = newCollisionIndex;
    }

    private static uint CalculateHash(string key) => CalculateHash(key.AsSpan());

    private static uint CalculateHash(ReadOnlySpan<char> key) =>
#if NO_SPAN_HASHCODE
        unchecked((uint)StringEx.GetHashCode(key));
#else
        unchecked((uint)string.GetHashCode(key));
#endif

    private static bool CheckKeysEqual(ReadOnlySpan<char> a, ReadOnlySpan<char> b)
    {
        return a.Equals(b, StringComparison.Ordinal);
    }

    private static ref Entry FindEndOfChain(Entry[] entries, ref Entry entry)
    {
        while (entry.Next >= 0)
        {
            entry = ref entries[entry.Next];
        }

        return ref entry;
    }

    private static int FindNextCollisionLocation(Entry[] entries, int startIndex)
    {
        for (; startIndex >= 0 && entries[startIndex].IsOccupied; startIndex--) ;
        return startIndex;
    }

    private static int CalculateBestCellarIndexForCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            return 1;
        }

        if (capacity <= 3)
        {
            return capacity;
        }

        var ratioIndex = (int)(((long)capacity * 86) / 100);

        foreach (var prime in PrimeSizes)
        {
            if (ratioIndex <= prime && prime < capacity)
            {
                return prime;
            }
        }

        return ratioIndex;

    }

    public struct Enumerator
    {
        internal Enumerator(TextDictionary<TValue> dictionary)
        {
            _entries = dictionary._entries;
            _nextPosition = 0;
            Current = default;
        }

        private readonly Entry[] _entries;
        private int _nextPosition;

        public KeyValuePair<string, TValue> Current { get; private set; }

        public bool MoveNext()
        {
            while (_nextPosition < _entries.Length)
            {
                ref var entry = ref _entries[_nextPosition];
                _nextPosition++;

                if (entry.IsBlank)
                {
                    continue;
                }

                Current = new(entry.Key, entry.Value);

                return true;
            }

            return false;
        }
    }

    private struct Entry
    {
        public uint HashCode;
        public int Next; // -1 is for explicit terminal, >= 0 for the next 0-based index
        public string Key;
        public TValue Value;

        public bool IsBlank => Key is null;

        public bool IsOccupied => Key is not null;

        public void Set(uint hashCode, string key, TValue value)
        {
            HashCode = hashCode;
            Next = -1;
            Key = key;
            Value = value;
        }
    }
}
