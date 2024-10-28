using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

/// <remarks>
/// This probably does not need to exist if https://github.com/dotnet/runtime/issues/27229 works out.
/// </remarks>
sealed class TextDictionary<TValue> : IEnumerable<KeyValuePair<string, TValue>>, IDictionary<string, TValue>
{
    public static TextDictionary<TValue> Clone(TextDictionary<TValue> source)
    {
        var builder = new Builder(source.Count);

        var sourceEntries = source._entries;
        for (var i = 0; i < sourceEntries.Length; i++)
        {
            ref var entry = ref sourceEntries[i];
            if (entry.Key is not null)
            {
                builder.Write(entry.HashCode, entry.Key, entry.Value);
            }
        }

        builder.Flush();

        return new TextDictionary<TValue>(ref builder);
    }

    public static TextDictionary<TValue> Clone<TSourceValue>(TextDictionary<TSourceValue> source, Func<TSourceValue, TValue> valueSelector)
    {
        var builder = new Builder(source.Count);

        var sourceEntries = source._entries;
        for (var i = 0; i < sourceEntries.Length; i++)
        {
            ref var entry = ref sourceEntries[i];
            if (entry.Key is not null)
            {
                builder.Write(entry.HashCode, entry.Key, valueSelector(entry.Value));
            }
        }

        builder.Flush();

        return new TextDictionary<TValue>(ref builder);
    }

    public static TextDictionary<TValue> MapFromDictionary(Dictionary<string, TValue> source)
    {
        var builder = new Builder(source.Count);
        foreach (var entry in source)
        {
            builder.Write(entry.Key, entry.Value);
        }

        builder.Flush();

        return new TextDictionary<TValue>(ref builder);
    }

    public static TextDictionary<TValue> MapFromDictionary<TSourceValue>(Dictionary<string, TSourceValue> source, Func<TSourceValue, TValue> valueSelector)
    {
        var builder = new Builder(source.Count);
        foreach (var entry in source)
        {
            builder.Write(entry.Key, valueSelector(entry.Value));
        }

        builder.Flush();

        return new TextDictionary<TValue>(ref builder);
    }

    internal static TextDictionary<TValue> MapFromPairs(KeyValuePair<string, TValue>[] source)
    {
        var builder = new Builder(source.Length);
        foreach (var entry in source)
        {
            builder.Write(entry.Key, entry.Value);
        }

        builder.Flush();

        return new TextDictionary<TValue>(ref builder);
    }

    private TextDictionary()
    {
        _entries = [];
        _cellarStartIndex = 0;
        _fastmodMul = 0;
        _collisionIndex = 0;
        Count = 0;
    }

    private TextDictionary(ref Builder builder)
    {
        _entries = builder.Entries;
        _cellarStartIndex = builder.CellarStartIndex;
        _fastmodMul = builder.FastmodMultiplier;
        _collisionIndex = builder.CollisionIndex;
        Count = builder.Count;
    }

    public TextDictionary(int desiredCapacity)
    {
        if (desiredCapacity < 1)
        {
            desiredCapacity = 1;
        }

        _entries = new Entry[desiredCapacity];
        _cellarStartIndex = CalculateBestCellarIndexForCapacity(_entries.Length);
        _fastmodMul = CalculateFastmodMultiplier(_cellarStartIndex);
        _collisionIndex = _entries.Length - 1;
        Count = 0;
    }

    private Entry[] _entries;
    private ulong _fastmodMul;
    private uint _cellarStartIndex;
    private int _collisionIndex;

    public int Count { get; private set; }

    public bool HasItems => Count != 0;

    public bool IsEmpty => Count == 0;

    public TValue this[string key]
    {
        get
        {
            if (!TryGetValue(key, out var result))
            {
                throwNotFound();
            }

            return result;

#if !NO_EXPOSED_NULLANNOTATIONS
            [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
            static void throwNotFound() => throw new KeyNotFoundException("Given key was not found");
        }
        set
        {
            ref var entryValue = ref GetOrAdd(key);
            entryValue = value;
        }
    }

    public KeysCollection Keys => new(this);

    public ValuesCollection Values => new(this);

    private IEnumerable<Entry> FilledEntries => _entries.Where(static e => e.Key is not null);

    ICollection<string> IDictionary<string, TValue>.Keys => Keys;

    ICollection<TValue> IDictionary<string, TValue>.Values => Values;

    public bool IsReadOnly => false;

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<KeyValuePair<string, TValue>> IEnumerable<KeyValuePair<string, TValue>>.GetEnumerator() => FilledEntries
        .Select(static e => new KeyValuePair<string, TValue>(e.Key, e.Value))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.AsEnumerable().GetEnumerator();

    public bool ContainsKey(string key) => !Unsafe.IsNullRef(ref GetRefByKey(key));

    public bool ContainsKey(ReadOnlySpan<char> key) => !Unsafe.IsNullRef(ref GetRefByKey(key));

    public bool Contains(KeyValuePair<string, TValue> item) =>
        TryGetValue(item.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);

    public bool TryGetValue(
        string key,
#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
#endif
        out TValue value)
    {
        ref var entry = ref GetRefByKey(key);
        if (Unsafe.IsNullRef(ref entry))
        {
            value = default!;
            return false;
        }

        value = entry.Value;
        return true;
    }

    public bool TryGetValue(
        ReadOnlySpan<char> key,
#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
#endif
        out TValue value)
    {
        ref var entry = ref GetRefByKey(key);
        if (Unsafe.IsNullRef(ref entry))
        {
            value = default!;
            return false;
        }

        value = entry.Value;
        return true;
    }

    public bool TryGetValue(
        ReadOnlySpan<char> key,
#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
#endif
        out string actualKey,
#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
#endif
        out TValue value)
    {
        ref var entry = ref GetRefByKey(key);
        if (Unsafe.IsNullRef(ref entry))
        {
            actualKey = null!;
            value = default!;
            return false;
        }

        actualKey = entry.Key;
        value = entry.Value;
        return true;
    }

    public void EnsureCapacity(int capacity)
    {
        if (_entries.Length >= capacity)
        {
            return;
        }

        var builder = new Builder(capacity);
        foreach (var oldEntry in _entries)
        {
            if (oldEntry.Key is not null)
            {
                builder.Write(oldEntry.HashCode, oldEntry.Key, oldEntry.Value);
            }
        }

        builder.Flush();

#if DEBUG
        if (Count != builder.Count) throw new InvalidOperationException();
#endif

        _entries = builder.Entries;
        _cellarStartIndex = builder.CellarStartIndex;
        _fastmodMul = builder.FastmodMultiplier;
        _collisionIndex = builder.CollisionIndex;
    }

    public void Add(KeyValuePair<string, TValue> item) => Add(item.Key, item.Value);

    public void Add(string key, TValue value)
    {
        var hash = CalculateHash(key);
        ref var entry = ref GetOrAddWithoutGrowth(hash, key, isAdd: true);
        if (Unsafe.IsNullRef(ref entry))
        {
            EnsureCapacity(Math.Max(_entries.Length * 2, 1));
            entry = ref GetOrAddWithoutGrowth(hash, key, isAdd: true);
            if (Unsafe.IsNullRef(ref entry))
            {
                throwFailed();
            }
        }

        entry.Value = value;

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static void throwFailed() => throw new InvalidOperationException("Failed to add");
    }

    public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
    {
        foreach (var pair in this)
        {
            array[arrayIndex++] = pair;
        }
    }

    public void Clear() => throw new NotImplementedException();

    public bool Remove(string key) => throw new NotImplementedException();

    public bool Remove(KeyValuePair<string, TValue> item) => throw new NotImplementedException();

    internal ref TValue GetOrAdd(string key)
    {
        var hash = CalculateHash(key);
        ref var entry = ref GetOrAddWithoutGrowth(hash, key, isAdd: false);
        if (Unsafe.IsNullRef(ref entry))
        {
            EnsureCapacity(Math.Max(_entries.Length * 2, 1));
            entry = ref GetOrAddWithoutGrowth(hash, key, isAdd: false);
            if (Unsafe.IsNullRef(ref entry))
            {
                return ref throwFailed();
            }
        }

        return ref entry.Value;

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static ref TValue throwFailed() => throw new InvalidOperationException("Failed to add");
    }

    private ref Entry GetOrAddWithoutGrowth(uint hash, string key, bool isAdd)
    {
        ref var entry = ref GetRefByHash(hash);

        if (entry.Key is null)
        {
            entry = new(hash, key);
            Count++;
            return ref entry;
        }

        while (true)
        {
            if (entry.HashCode == hash && key.Equals(entry.Key))
            {
                if (isAdd)
                {
                    throwDuplicate();
                }

                return ref entry;
            }

            if (entry.Next < 0)
            {
                break;
            }

            entry = ref _entries[entry.Next];
        }

        if (Count < _entries.Length && (_collisionIndex = FindNextEmptyCollisionIndex()) >= 0)
        {
            entry.Next = _collisionIndex;

            entry = ref _entries[_collisionIndex--];
            entry = new(hash, key);
            Count++;

            return ref entry;
        }

        return ref Unsafe.NullRef<Entry>();

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static void throwDuplicate() => throw new InvalidOperationException("Duplicate key");
    }

    private int FindNextEmptyCollisionIndex()
    {
        var i = _collisionIndex;
        for (; i >= 0 && _entries[i].Key is not null; i--) ;
        return i;
    }

    private ref Entry GetRefByKey(string key) => ref GetRefByKey(CalculateHash(key), key);

    private ref Entry GetRefByKey(ReadOnlySpan<char> key) => ref GetRefByKey(CalculateHash(key), key);

    private ref Entry GetRefByKey(uint hash, string key)
    {
        ref var entry = ref GetRefByHash(hash);

        if (entry.Key is not null)
        {
            while (true)
            {
                if (entry.HashCode == hash && key.Equals(entry.Key))
                {
                    return ref entry;
                }

                if (entry.Next < 0)
                {
                    break;
                }

                entry = ref _entries[entry.Next];
            }
        }

        return ref Unsafe.NullRef<Entry>();
    }

    private ref Entry GetRefByKey(uint hash, ReadOnlySpan<char> key)
    {
        ref var entry = ref GetRefByHash(hash);

        if (entry.Key is not null)
        {
            while (true)
            {
                if (entry.HashCode == hash && key.SequenceEqual(entry.Key.AsSpan()))
                {
                    return ref entry;
                }

                if (entry.Next < 0)
                {
                    break;
                }

                entry = ref _entries[entry.Next];
            }
        }

        return ref Unsafe.NullRef<Entry>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref Entry GetRefByHash(uint hash) =>
        ref _entries[GetIndexByHash(hash, _cellarStartIndex, _fastmodMul)];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetIndexByHash(uint hash, uint divisor, ulong multiplier) =>
        // I barely understand this algorithm, but it's how the .NET dictionary works on 64-bit platforms. Sources:
        // - https://lemire.me/blog/2019/02/08/faster-remainders-when-the-divisor-is-a-constant-beating-compilers-and-libdivide/
        // - https://github.com/dotnet/runtime/pull/406
        (uint)(((((multiplier * hash) >> 32) + 1) * divisor) >> 32);

    private static ulong CalculateFastmodMultiplier(uint divisor) =>
        divisor == 0 ? 0 : (ulong.MaxValue / divisor) + 1;

#if NO_SPAN_HASHCODE

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(string key) => unchecked((uint)StringEx.GetHashCode(key));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(ReadOnlySpan<char> key) => unchecked((uint)StringEx.GetHashCode(key));

#else

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(string key) => unchecked((uint)key.GetHashCode());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint CalculateHash(ReadOnlySpan<char> key) => unchecked((uint)string.GetHashCode(key));

#endif

    private static uint CalculateBestCellarIndexForCapacity(int capacity)
    {
        if (capacity < 0)
        {
            return 0;
        }

        if (capacity > 3)
        {
            // this seems to be a good ratio to start with, based on something from wikipedia
            var index = (int)(((long)capacity * 86) / 100);
            if ((index & 1) == 0)
            {
                index--;
            }

            // gaps between primes don't seem too large so this shouldn't have to go through too many iterations
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

    public sealed class KeysCollection : ICollection<string>
    {
        internal KeysCollection(TextDictionary<TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        private readonly TextDictionary<TValue> _dictionary;

        public int Count => _dictionary.Count;
        public bool IsReadOnly => true;

        public void Add(string item) => throw new NotSupportedException();
        public void Clear() => throw new NotSupportedException();
        public bool Contains(string item) => _dictionary.ContainsKey(item);
        public void CopyTo(string[] array, int arrayIndex) => throw new NotImplementedException();
        public bool Remove(string item) => throw new NotSupportedException();
        public IEnumerator<string> GetEnumerator() => _dictionary.FilledEntries.Select(static e => e.Key).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public sealed class ValuesCollection : ICollection<TValue>
    {
        internal ValuesCollection(TextDictionary<TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        private readonly TextDictionary<TValue> _dictionary;

        public int Count => _dictionary.Count;
        public bool IsReadOnly => true;

        public void Add(TValue item) => throw new NotSupportedException();
        public void Clear() => throw new NotSupportedException();
        public bool Contains(TValue item) => GetValues().Contains(item);
        public void CopyTo(TValue[] array, int arrayIndex) => throw new NotImplementedException();
        public bool Remove(TValue item) => throw new NotSupportedException();
        public IEnumerator<TValue> GetEnumerator() => GetValues().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private IEnumerable<TValue> GetValues() => _dictionary.FilledEntries.Select(static e => e.Value);
    }

    public struct Enumerator
    {
        internal Enumerator(TextDictionary<TValue> dictionary)
        {
            _entries = dictionary._entries;
            _index = 0;
            _current = default;
        }

        private readonly Entry[] _entries;
        private int _index;
        private KeyValuePair<string, TValue> _current;

        public readonly KeyValuePair<string, TValue> Current => _current;

        public bool MoveNext()
        {
            while (_index < _entries.Length)
            {
                ref var entry = ref _entries[_index++];
                if (entry.Key is not null)
                {
                    _current = new(entry.Key, entry.Value);
                    return true;
                }
            }

            return false;
        }
    }

    internal struct KeyLengthEnumerator
    {
        internal KeyLengthEnumerator(TextDictionary<TValue> dictionary, int minKeyLength, int maxKeyLength)
        {
#if DEBUG
            if (minKeyLength > maxKeyLength) throw new ArgumentOutOfRangeException(nameof(maxKeyLength));
#endif

            _entries = dictionary._entries;
            _minKeyLength = minKeyLength;
            _maxKeyLength = maxKeyLength;
            _index = 0;
            _current = default;
        }

        private readonly Entry[] _entries;
        private readonly int _minKeyLength;
        private readonly int _maxKeyLength;
        private int _index;
        private KeyValuePair<string, TValue> _current;

        public readonly KeyValuePair<string, TValue> Current => _current;

        public bool MoveNext()
        {
            while (_index < _entries.Length)
            {
                ref var entry = ref _entries[_index++];

                if (entry.Key?.Length is { } keyLength && keyLength <= _maxKeyLength && keyLength >= _minKeyLength)
                {
                    _current = new(entry.Key, entry.Value);
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
            FastmodMultiplier = CalculateFastmodMultiplier(CellarStartIndex);
            CollisionIndex = Entries.Length - 1;
            Count = 0;

            _leftovers = new((int)(Entries.Length - CellarStartIndex));
        }

        private readonly List<(uint hash, string key, TValue value)> _leftovers;

        public Entry[] Entries;
        public ulong FastmodMultiplier;
        public uint CellarStartIndex;
        public int CollisionIndex;
        public int Count;

        public void Write(string key, TValue value)
        {
            Write(CalculateHash(key), key, value);
        }

        public void Write(uint hash, string key, TValue value)
        {
            ref var entry = ref GetRefByHash(hash);
            if (entry.Key is null)
            {
                entry = new(hash, key, value);
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
            foreach (var (hash, key, value) in _leftovers)
            {
                ForceAppendCollisionEntry(hash, key, value);
            }
        }

        private void ForceAppendCollisionEntry(uint hash, string key, TValue value)
        {
            for (; CollisionIndex >= 0 && Entries[CollisionIndex].Key is not null; CollisionIndex--) ;

            if (CollisionIndex < 0)
            {
                throwNoRoomForCollision();
            }

            ref var entry = ref GetRefByHash(hash);

            while (entry.Next >= 0)
            {
                entry = ref Entries[entry.Next];
            }

            entry.Next = CollisionIndex;

            Entries[CollisionIndex--] = new(hash, key, value);

#if !NO_EXPOSED_NULLANNOTATIONS
            [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
            static void throwNoRoomForCollision() => throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly ref Entry GetRefByHash(uint hash) =>
            ref Entries[GetIndexByHash(hash, CellarStartIndex, FastmodMultiplier)];
    }

    private struct Entry
    {
        public uint HashCode;
        public int Next; // -1 is for explicit terminal, >= 0 for the next 0-based index
        public string Key;
        public TValue Value;

        public Entry(uint hashCode, string key)
        {
            HashCode = hashCode;
            Next = -1;
            Key = key;
            Value = default!;
        }

        public Entry(uint hashCode, string key, TValue value)
        {
            HashCode = hashCode;
            Next = -1;
            Key = key;
            Value = value;
        }
    }
}
