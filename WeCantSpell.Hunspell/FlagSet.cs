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

        char[] values = [value0, value1];
        Array.Sort(values);
        CollectionsEx.RemoveSortedDuplicates(ref values);
        return new(values);
    }

    public static FlagSet Create(FlagValue value0, FlagValue value1, FlagValue value2)
    {
        Span<char> values = [value0, value1, value2];
        values.Sort();
        values = values.TrimStart(FlagValue.ZeroValue);

        if (values.Length == 0)
        {
            return Empty;
        }

        if (values.Length == 1)
        {
            return new(values[0]);
        }

        var flags = values.ToArray();
        CollectionsEx.RemoveSortedDuplicates(ref flags);
        return new(flags);
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
        if (text.IsEmpty)
        {
            return Empty;
        }

        if (text.Length == 1)
        {
            return new(text[0]);
        }

        var values = text.ToArray();
        Array.Sort(values);
        CollectionsEx.RemoveSortedDuplicates(ref values);
        return new(values);
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
        if (aSet.Length <= 4 && bSet.Length <= 4)
        {
            return SortedInterectionTestLinear(aSet, bSet);
        }

        return SortedInterectionTestBinary(aSet, bSet);
    }

    private static bool SortedInterectionTestLinear(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
    {
        if (aSet.IsEmpty || bSet.IsEmpty)
        {
            return false;
        }

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
        if (aSet.IsEmpty || bSet.IsEmpty)
        {
            return false;
        }

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
        if (!HasItems)
        {
            return false;
        }

        if (a > b)
        {
            MemoryEx.Swap(ref a, ref b);
        }

        return ContainsAnySorted([(char)a, (char)b]);
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c)
    {
        return HasItems && ContainsAnyUnsorted([(char)a, (char)b, (char)c]);
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d)
    {
        return HasItems && ContainsAnyUnsorted([(char)a, (char)b, (char)c, (char)d]);
    }

    public bool ContainsAny(FlagSet other)
    {
        if (IsEmpty || other.IsEmpty || (_mask & other._mask) == default)
        {
            return false;
        }

        if (other._values!.Length == 1)
        {
            return Contains(other._values[0]);
        }

        if (_values!.Length == 1)
        {
            return other.Contains(_values[0]);
        }

        return _values!.Length >= other._values!.Length
            ? ContainsAnySorted(other._values)
            : other.ContainsAnySorted(_values);
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
        if (!value.HasValue)
        {
            return this;
        }

        if (!HasItems)
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
        if (HasItems)
        {
            if (_values!.Length == 1)
            {
                return _values[0].Equals(value);
            }

            if (unchecked(value & _mask) == value)
            {
                return _values.Length <= 8
                    ? checkIterative(_values, value)
                    : Array.BinarySearch(_values, value) >= 0;
            }
        }

        return false;

        static bool checkIterative(char[] searchSpace, char target)
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

    private bool ContainsAnyUnsorted(Span<char> other)
    {
        other.Sort();
        return ContainsAnySorted(other);
    }

    private bool ContainsAnySorted(ReadOnlySpan<char> other)
    {
#if DEBUG
        for (var i = other.Length - 2; i >= 0; i--)
        {
            if (other[i] > other[i + 1]) throw new InvalidOperationException("Values must be sorted in ascending order");
        }
#endif

        if (IsEmpty)
        {
            return false;
        }

        other = TrimUnmatchableSorted(other); // Trim ends that won't match

        if (other.IsEmpty)
        {
            return false;
        }

        if (other.Length == 1)
        {
            return Contains(other[0]);
        }

        if (other[0] > _values![_values.Length - 1] || other[other.Length - 1] < _values[0])
        {
            // If the sets are disjoint then a match is impossible
            return false;
        }

        return SortedInterectionTest(other, _values);
    }

    private ReadOnlySpan<char> TrimUnmatchableSorted(ReadOnlySpan<char> span)
    {
        // Only trim zero values from the start as the span should be sorted
        var start = 0;
        for (; start < span.Length && (span[start] == FlagValue.ZeroValue || (span[start] & _mask) == default); start++) ;

        var end = span.Length - 1;
        for (; end >= start && (span[end] & _mask) == default; end--) ;

        return span.Slice(start, end - start + 1);
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
            if (values.IsEmpty)
            {
                return;
            }

            AddRange(values._values!);
            UnionMask(values._mask);
        }

        public FlagSet Create() => new(_builder.MakeArray(), _mask);

        internal FlagSet MoveToFlagSet()
        {
            var result = new FlagSet(ArrayBuilder<char>.Pool.GetArrayAndReturn(_builder), _mask);
            _builder = null!; // This should operate as a very crude dispose
            return result;
        }

        private void AddRange(char[] values)
        {
            if (values is not { Length: > 0 })
            {
                return;
            }

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
