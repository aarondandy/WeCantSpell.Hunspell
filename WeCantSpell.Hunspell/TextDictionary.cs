using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

sealed class TextDictionary<TValue> : IEnumerable<KeyValuePair<string, TValue>>
{
    public static TextDictionary<TValue> MapFromDictionary<TSourceValue>(Dictionary<string, TSourceValue> source, Func<TSourceValue, TValue> valueSelector)
    {
        var builder = new Builder(source.Count);
        foreach (var entry in source)
        {
            builder.Write(entry.Key, valueSelector(entry.Value));
        }

        builder.Flush();

        return new TextDictionary<TValue>()
        {
            _entries = builder.Entries,
            _cellarStartIndex = builder.CellarStartIndex,
            _collisionIndex = builder.CollisionIndex,
            Count = source.Count
        };
    }

    private TextDictionary()
    {
        _entries = Array.Empty<Entry>();
        _cellarStartIndex = 0;
        _collisionIndex = 0;
        Count = 0;
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
    private uint _cellarStartIndex;
    private int _collisionIndex;

    public int Count { get; private set; }

    public IEnumerable<string> Keys => FilledEntries.Select(static e => e.Key);

    public IEnumerable<TValue> Values => FilledEntries.Select(static e => e.Value);

    private IEnumerable<Entry> FilledEntries => _entries.Where(static e => e.Key is not null);

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<KeyValuePair<string, TValue>> IEnumerable<KeyValuePair<string, TValue>>.GetEnumerator() => FilledEntries
        .Select(static e => new KeyValuePair<string, TValue>(e.Key, e.Value))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.AsEnumerable().GetEnumerator();

    public bool ContainsKey(string key) => TryGetValue(key, out _);

    public bool ContainsKey(ReadOnlySpan<char> key) => TryGetValue(key, out _);

    public bool TryGetValue(string key, out TValue value)
    {
        var hash = CalculateHash(key);

        ref var entry = ref _entries[hash % _cellarStartIndex];

        if (entry.Key is not null)
        {
            while (true)
            {
                if (entry.HashCode == hash && CheckKeysEqual(entry.Key, key))
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

    public bool TryGetValue(ReadOnlySpan<char> key, out TValue value)
    {
        var hash = CalculateHash(key);

        ref var entry = ref _entries[hash % _cellarStartIndex];

        if (entry.Key is not null)
        {
            while (true)
            {
                if (entry.HashCode == hash && CheckKeysEqual(entry.Key, key))
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

    public bool TryGetValue(ReadOnlySpan<char> key, out string actualKey, out TValue value)
    {
        var hash = CalculateHash(key);

        ref var entry = ref _entries[hash % _cellarStartIndex];

        if (entry.Key is not null)
        {
            while (true)
            {
                if (entry.HashCode == hash && CheckKeysEqual(entry.Key, key))
                {
                    actualKey = entry.Key;
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

        actualKey = null!;
        value = default!;
        return false;
    }

    public void Add(string key, TValue value)
    {
        var hash = CalculateHash(key);
        if (!TryAddWithoutGrowing(hash, key, value))
        {
            RebuildAndInsert(hash, key, value);
        }
    }

    private bool TryAddWithoutGrowing(uint hash, string key, TValue value)
    {
        ref var entry = ref _entries[hash % _cellarStartIndex];

        if (entry.Key is null)
        {
            entry.Set(hash, key, value);
            Count++;
            return true;
        }

        // search through the chain to ensure there are no collisions
        while (true)
        {
            if (entry.HashCode == hash && CheckKeysEqual(entry.Key, key))
            {
                throwDuplicate();
            }

            if (entry.Next < 0)
            {
                break;
            }

            entry = ref _entries[entry.Next];
        }

        if (Count < _entries.Length)
        {
            for (; _collisionIndex >= 0 && _entries[_collisionIndex].Key is not null; _collisionIndex--) ;

            if (_collisionIndex >= 0)
            {
                entry.Next = _collisionIndex;

                entry = ref _entries[_collisionIndex];
                entry.Set(hash, key, value);

                Count++;
                _collisionIndex--;

                return true;
            }
        }

        return false;

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static void throwDuplicate() => throw new InvalidOperationException("Duplicate key");
    }

    private void RebuildAndInsert(uint hash, string key, TValue value)
    {
        var builder = new Builder(Math.Max(_entries.Length * 2, 1));
        foreach (var oldEntry in _entries)
        {
            if (oldEntry.Key is not null)
            {
                builder.Write(oldEntry.HashCode, oldEntry.Key, oldEntry.Value);
            }
        }

        builder.Write(hash, key, value);

        builder.Flush();

        _entries = builder.Entries;
        _cellarStartIndex = builder.CellarStartIndex;
        _collisionIndex = builder.CollisionIndex;
        Count = builder.Count;
    }

#if NO_SPAN_HASHCODE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(string key) => unchecked((uint)StringEx.GetHashCode(key.AsSpan()));
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(ReadOnlySpan<char> key) => unchecked((uint)StringEx.GetHashCode(key));
#else

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(string key) => unchecked((uint)key.GetHashCode());
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(ReadOnlySpan<char> key) => unchecked((uint)string.GetHashCode(key));
#endif

    private static bool CheckKeysEqual(string a, ReadOnlySpan<char> b) => a.AsSpan().Equals(b, StringComparison.Ordinal);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckKeysEqual(string a, string b) => a.Equals(b, StringComparison.Ordinal);

    private static uint CalculateBestCellarIndexForCapacity(int capacity)
    {
        if (capacity < 0)
        {
            return 0;
        }

        if (capacity > 3)
        {
            var index = (int)(((long)capacity * 86) / 100);
            if ((index & 1) == 0)
            {
                index--;
            }

            // gaps between primes don't see too large so this shouldn't have to go through too many iterations
            for (; index < capacity; index += 2)
            {
                if (isOddPrime(index))
                {
                    return (uint)index;
                }
            }
        }

        return (uint)capacity;

        static bool isOddPrime(int n)
        {
            var limit = (int)Math.Sqrt(n);
            for (var i = 3; i <= limit; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
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

                if (entry.Key is not null)
                {
                    Current = new(entry.Key, entry.Value);
                    return true;
                }
            }

            return false;
        }
    }

    private struct Builder
    {
        public Builder(int capacity)
        {
            Entries = new Entry[capacity];
            CellarStartIndex = CalculateBestCellarIndexForCapacity(Entries.Length);
            CollisionIndex = Entries.Length - 1;
            Count = 0;
        }

        private List<(uint hash, string key, TValue value)> _leftovers = new();

        public Entry[] Entries;
        public uint CellarStartIndex;
        public int CollisionIndex;
        public int Count;

        public void Write(string key, TValue value)
        {
            Write(CalculateHash(key), key, value);
        }

        public void Write(uint hash, string key, TValue value)
        {
            ref var entry = ref Entries[hash % CellarStartIndex];
            if (entry.Key is null)
            {
                entry.Set(hash, key, value);
            }
            else if (CollisionIndex >= CellarStartIndex)
            {
                ForceAppendCollisionEntry(hash, key, value);
            }
            else
            {
                _leftovers.Add((hash, key, value));
            }

            Count++;
        }

        public void Flush()
        {
            foreach (var leftover in _leftovers)
            {
                ForceAppendCollisionEntry(leftover.hash, leftover.key, leftover.value);
            }
        }

        private void ForceAppendCollisionEntry(uint hash, string key, TValue value)
        {
            ref var entry = ref Entries[hash % CellarStartIndex];

            while (entry.Next >= 0)
            {
                entry = ref Entries[entry.Next];
            }

            for (; CollisionIndex >= 0 && Entries[CollisionIndex].Key is not null; CollisionIndex--) ;

            if (CollisionIndex < 0)
            {
                throwNoRoomForCollision();
            }

            entry.Next = CollisionIndex;

            entry = ref Entries[CollisionIndex];
            entry.Set(hash, key, value);

            CollisionIndex--;

#if !NO_EXPOSED_NULLANNOTATIONS
            [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
            static void throwNoRoomForCollision() => throw new NotSupportedException();
        }
    }

    private struct Entry
    {
        public uint HashCode;
        public int Next; // -1 is for explicit terminal, >= 0 for the next 0-based index
        public string Key;
        public TValue Value;

        public void Set(uint hashCode, string key, TValue value)
        {
            HashCode = hashCode;
            Next = -1;
            Key = key;
            Value = value;
        }
    }
}
