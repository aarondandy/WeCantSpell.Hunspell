using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct FlagSet : IReadOnlyList<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new([]);

    public static bool operator ==(FlagSet left, FlagSet right) => left.Equals(right);

    public static bool operator !=(FlagSet left, FlagSet right) => !(left == right);

    public static FlagSet Create(FlagValue value) => value.IsZero ? Empty : new(value);

    public static FlagSet Create(FlagValue value0, FlagValue value1)
    {
        if (value0.IsZero)
        {
            return Create(value1);
        }

        if (value1.IsZero)
        {
            return Create(value0);
        }

        return CreateUsingMutableBuffer([value0, value1]);
    }

    public static FlagSet Create(FlagValue value0, FlagValue value1, FlagValue value2)
    {
        return CreateUsingMutableBuffer([value0, value1, value2]);
    }

    public static FlagSet Create(FlagValue value0, FlagValue value1, FlagValue value2, FlagValue value3)
    {
        return CreateUsingMutableBuffer([value0, value1, value2, value3]);
    }

    public static FlagSet Create(IEnumerable<FlagValue> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null) throw new ArgumentNullException(nameof(values));
#endif

        var builder = values is ICollection collection ? new Builder(collection.Count) : new Builder();
        builder.AddRange(values);
        return builder.MoveToFlagSet();
    }

    internal static FlagSet ParseAsChars(ReadOnlySpan<char> text)
    {
        return text.IsEmpty ? Empty : CreateUsingBuffer(text);
    }

    internal static FlagSet ParseAsLongs(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var lastIndex = text.Length - 1;
        var builder = new Builder((text.Length + 1) / 2);

        for (var i = 0; i < lastIndex; i += 2)
        {
            builder.Add(FlagValue.CreateAsLong(text[i], text[i + 1]));
        }

        if ((lastIndex & 1) == 0)
        {
            builder.Add(new FlagValue(text[lastIndex]));
        }

        return builder.MoveToFlagSet();
    }

    internal static FlagSet ParseAsNumbers(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var flags = new Builder();

        foreach (var part in text.SplitOnComma(StringSplitOptions.RemoveEmptyEntries))
        {
            if (FlagValue.TryParseAsNumber(part, out var value))
            {
                flags.Add(value);
            }
        }

        return flags.MoveToFlagSet();
    }

    private static void PrepareMutableFlagValuesForUse(ref Span<char> values)
    {
        MemoryEx.RemoveAll(ref values, FlagValue.ZeroValue);

        values.Sort();

        MemoryEx.RemoveAdjacentDuplicates(ref values);
    }

    private static void PrepareMutableFlagValuesForUse(ref char[] values)
    {
        var valuesSpan = values.AsSpan();

        PrepareMutableFlagValuesForUse(ref valuesSpan);

        if (valuesSpan.Length < values.Length)
        {
            if (valuesSpan.IsEmpty)
            {
                values = [];
            }
            else
            {
                Array.Resize(ref values, valuesSpan.Length);
            }
        }
    }

    internal static FlagSet CreateUsingOwnedBuffer(char[] values)
    {
        PrepareMutableFlagValuesForUse(ref values);
        return values.Length == 0 ? Empty : new(values);
    }

    private static FlagSet CreateUsingBuffer(ReadOnlySpan<char> values)
    {
        return CreateUsingOwnedBuffer(values.Trim(FlagValue.Zero).ToArray());
    }

    private static FlagSet CreateUsingMutableBuffer(Span<char> values)
    {
        PrepareMutableFlagValuesForUse(ref values);

        if (values.IsEmpty)
        {
            return Empty;
        }

        if (values.Length == 1)
        {
            return new(values[0]);
        }

        return new(values.ToArray());
    }

    private static char CalculateMask(ReadOnlySpan<char> values)
    {
        char mask = default;
        foreach (var value in values)
        {
            unchecked
            {
                mask |= value;
            }
        }

        return mask;
    }

    private static bool SortedInterectionTest(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
    {
        return (aSet![aSet.Length - 1] >= bSet[0] && bSet[bSet.Length - 1] >= aSet[0])
            &&
            (
                (aSet.Length <= 4 && bSet.Length <= 4)
                ? SortedInterectionTestLinear(aSet, bSet)
                : SortedInterectionTestBinary(aSet, bSet)
            );
    }

    private static bool SortedInterectionTestLinear(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
    {

#if DEBUG
        if (aSet.IsEmpty || bSet.IsEmpty) throw new InvalidOperationException();
#endif

        var aIndex = 0;
        var bIndex = 0;

        do
        {
            var cmp = aSet[aIndex].CompareTo(bSet[bIndex]);
            if (cmp == 0)
            {
                return true;
            }
            else if (cmp < 0)
            {
                aIndex++;

                if (aIndex >= aSet.Length)
                {
                    break; // disjoint or empty can't match
                }
            }
            else
            {
                bIndex++;

                if (bIndex >= bSet.Length)
                {
                    break; // disjoint or empty can't match
                }
            }
        }
        while (true);

        return false;
    }

    private static bool SortedInterectionTestBinary(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
    {

#if DEBUG
        if (aSet.IsEmpty || bSet.IsEmpty) throw new InvalidOperationException();
#endif

        do
        {
            if (aSet.Length > bSet.Length)
            {
                var tmp = aSet;
                aSet = bSet;
                bSet = tmp;
            }

            var flagValuesIndex = bSet.BinarySearch(aSet[0]);
            if (flagValuesIndex >= 0)
            {
                return true;
            }
            else
            {
                aSet = aSet.Slice(1);
                if (aSet.IsEmpty)
                {
                    break;
                }

                bSet = bSet.Slice(~flagValuesIndex);
                if (bSet.IsEmpty)
                {
                    break;
                }
            }
        }
        while (true);

        return false;
    }

    private static bool SortedContains(ReadOnlySpan<char> sorted, char value)
    {
        return sorted.Length <= 8
            ? checkIterative(sorted, value)
            : (sorted.BinarySearch(value) >= 0);

        static bool checkIterative(ReadOnlySpan<char> searchSpace, char target)
        {
            foreach (var value in searchSpace)
            {
                if (value == target)
                {
                    return true;
                }

                if (value > target)
                {
                    break;
                }
            }

            return false;
        }
    }

    private FlagSet(FlagValue value) : this((char)value)
    {
    }

    private FlagSet(char value) : this([value], mask: value)
    {
    }

    private FlagSet(char[] values) : this(values, mask: CalculateMask(values))
    {
    }

    private FlagSet(char[] values, char mask)
    {
        _values = values;
        _mask = mask;
    }

    private readonly char[]? _values;
    private readonly char _mask;

    public int Count => (_values?.Length).GetValueOrDefault();

    public bool IsEmpty => !HasItems;

    public bool HasItems => _values is { Length: > 0 };

    public FlagValue this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            return (FlagValue)_values![index];
        }
    }

    public IEnumerator<FlagValue> GetEnumerator() => GetInternalArray().Select(static v => (FlagValue)v).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString()
    {
        if (IsEmpty)
        {
            return string.Empty;
        }

        if (Array.TrueForAll(_values!, static v => v < 256))
        {
            var builder = StringBuilderPool.Get(_values!.Length);

            foreach (var value in _values)
            {
                builder.Append(value);
            }

            return StringBuilderPool.GetStringAndReturn(builder);
        }
        else
        {
            return string.Join(",", _values!.Select(static v => ((int)v).ToString(CultureInfo.InvariantCulture.NumberFormat)));
        }
    }

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

    public bool Equals(FlagSet other) => GetInternalArray().SequenceEqual(other.GetInternalArray());

    public override bool Equals(object? obj) => obj is FlagSet set && Equals(set);

    public bool Contains(FlagValue value) => Contains((char)value);

    public bool ContainsAny(FlagValue a, FlagValue b)
    {
        if (IsEmpty)
        {
            return false;
        }

        if (b.IsZero || a == b)
        {
            return Contains(a);
        }

        if (a.IsZero)
        {
            return Contains(b);
        }

        if (a > b)
        {
            MemoryEx.Swap(ref a, ref b);
        }

        return ContainsAnyPrepared([a, b]);
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c)
    {
        return HasItems && ContainsAnyUnpreparedMutable([a, b, c]);
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d)
    {
        return HasItems && ContainsAnyUnpreparedMutable([a, b, c, d]);
    }

    public bool ContainsAny(FlagSet other)
    {
        return HasItems
            && other.HasItems
            &&
            (
                other._values!.Length == 1
                ? Contains(other._values[0])
                : (
                    _values!.Length == 1
                    ? other.Contains(_values[0])
                    : ((other._mask & _mask) != default && SortedInterectionTest(other._values, _values))
                )
            );
    }

    public FlagSet Union(FlagSet other)
    {
        if (other.IsEmpty)
        {
            return this;
        }

        if (IsEmpty)
        {
            return other;
        }

        if (other.Count == 1)
        {
            return Union(other[0]);
        }

        if (Count == 1)
        {
            return other.Union(this[0]);
        }

        var builder = new Builder();
        builder.AddRange(this);
        builder.AddRange(other);
        return builder.MoveToFlagSet();
    }

    public FlagSet Union(FlagValue value)
    {
        if (value.IsZero)
        {
            return this;
        }

        if (IsEmpty)
        {
            return new(value);
        }

        var valueIndex = Array.BinarySearch(_values!, value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var newValues = new char[_values!.Length + 1];
        if (valueIndex >= _values.Length)
        {
            _values.CopyTo(newValues.AsSpan());
            newValues[_values.Length] = value;
        }
        else if (valueIndex == 0)
        {
            newValues[0] = value;
            _values.CopyTo(newValues.AsSpan(1));
        }
        else
        {
            _values.AsSpan(0, valueIndex).CopyTo(newValues.AsSpan());
            _values.AsSpan(valueIndex).CopyTo(newValues.AsSpan(valueIndex + 1));
            newValues[valueIndex] = value;
        }

        return new(newValues, unchecked((char)(_mask | value)));
    }

    internal char[] GetInternalArray() => _values ?? [];

    internal bool Contains(char value)
    {
        if (value != FlagValue.ZeroValue || HasItems)
        {
            ReadOnlySpan<char> searchSpace = _values!;

            if (searchSpace.Length == 1)
            {
                return searchSpace[0].Equals(value);
            }

            if (unchecked(value & _mask) == value)
            {
                return SortedContains(searchSpace, value);
            }
        }

        return false;
    }

    private bool ContainsAnyUnpreparedMutable(Span<char> values)
    {
        PrepareMutableFlagValuesForUse(ref values);
        return ContainsAnyPrepared(values);
    }

    private bool ContainsAnyPrepared(ReadOnlySpan<char> other)
    {

#if DEBUG

        if (IsEmpty) throw new InvalidOperationException();

        if (other.IsEmpty) throw new ArgumentOutOfRangeException(nameof(other));

        if (other.Contains(FlagValue.ZeroValue)) throw new ArgumentOutOfRangeException(nameof(other), "Contains zero values");

        for (var i = other.Length - 2; i >= 0; i--)
        {
            if (other[i] > other[i + 1]) throw new InvalidOperationException("Values must be sorted in ascending order");
        }

#endif

        ReadOnlySpan<char> values = _values!;

        if (other.Length == 1)
        {
            return values.Length == 1
                ? values[0] == other[0]
                : SortedContains(values, other[0]);
        }

        if (values.Length == 1)
        {
            return SortedContains(other, values[0]);
        }

        return SortedInterectionTest(values, other);
    }

    public sealed class Builder
    {
        public Builder()
        {
            _builder = ArrayBuilder<char>.Pool.Get();
        }

        public Builder(int capacity)
        {
            _builder = capacity is >= 0 and <= CollectionsEx.CollectionPreallocationLimit
                ? ArrayBuilder<char>.Pool.Get(capacity)
                : ArrayBuilder<char>.Pool.Get();
        }

        private ArrayBuilder<char> _builder;
        private char _mask = default;

        public void Add(FlagValue value)
        {
            if (value.HasValue)
            {
                _builder.AddAsSortedSet(value);
                UnionMask(value);
            }
        }

        public void AddRange(IEnumerable<FlagValue> values)
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

        public void AddRange(FlagSet values)
        {
            if (values.HasItems)
            {
                AddRange(values._values!);
                UnionMask(values._mask);
            }
        }

        public FlagSet Create() => new(_builder.MakeArray(), _mask);

        internal FlagSet MoveToFlagSet()
        {
            var result = new FlagSet(ArrayBuilder<char>.Pool.GetArrayAndReturn(_builder), _mask);
            _builder = null!; // This should operate as a very crude dispose
            return result;
        }

        private void AddRange(ReadOnlySpan<char> values)
        {
            if (_builder.Count == 0)
            {
                _builder.AddRange(values);
                return;
            }

            var lowBoundIndex = 0;
            foreach (var value in values)
            {
                var valueIndex = _builder.BinarySearch(lowBoundIndex, _builder.Count - lowBoundIndex, value);
                if (valueIndex < 0)
                {
                    UnionMask(value);

                    valueIndex = ~valueIndex; // locate the best insertion point

                    if (valueIndex >= _builder.Count)
                    {
                        _builder.Add(value);
                    }
                    else
                    {
                        _builder.Insert(valueIndex, value);
                    }
                }

                lowBoundIndex = valueIndex;
            }
        }

        private void UnionMask(char value)
        {
            unchecked
            {
                _mask |= value;
            }
        }
    }
}
