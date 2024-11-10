using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct FlagSet : IReadOnlyList<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new(string.Empty);

    public static bool operator ==(FlagSet left, FlagSet right) => left.Equals(right);

    public static bool operator !=(FlagSet left, FlagSet right) => !left.Equals(right);

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
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var builder = new StringBuilderSpan(values.GetNonEnumeratedCountOrDefault());

        foreach (var value in values)
        {
            builder.AppendIfNotNull(value);
        }

        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    public static FlagSet Create(ReadOnlySpan<FlagValue> values)
    {
        var builder = new StringBuilderSpan(values.Length);

        foreach (var value in values)
        {
            builder.AppendIfNotNull(value);
        }

        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    internal static FlagSet ParseAsChars(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var builder = new StringBuilderSpan(text);
        builder.RemoveAll('\0');
        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    internal static FlagSet ParseAsLongs(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var lastIndex = text.Length - 1;
        var builder = new StringBuilderSpan((text.Length + 1) / 2);

        for (var i = 0; i < lastIndex; i += 2)
        {
            builder.AppendIfNotNull(FlagValue.CreateAsLong(text[i], text[i + 1]));
        }

        if ((lastIndex & 1) == 0)
        {
            builder.AppendIfNotNull(new FlagValue(text[lastIndex]));
        }

        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    internal static FlagSet ParseAsNumbers(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var builder = new StringBuilderSpan(text.Length);

        foreach (var part in text.SplitOnComma(StringSplitOptions.RemoveEmptyEntries))
        {
            if (FlagValue.TryParseAsNumber(part, out var value))
            {
                builder.AppendIfNotNull(value);
            }
        }

        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    private static void PrepareMutableFlagValuesForUse(ref Span<char> values)
    {
        MemoryEx.RemoveAll(ref values, FlagValue.ZeroValue);

        values.Sort();

        MemoryEx.RemoveAdjacentDuplicates(ref values);
    }

    private static FlagSet CreateUsingMutableBuffer(Span<char> values)
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        PrepareMutableFlagValuesForUse(ref values);

        return new(values.ToString());
    }

#if !HAS_SEARCHVALUES

    private static bool SortedInterectionTest(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
    {

#if DEBUG
        ExceptionEx.ThrowIfArgumentEmpty(aSet, nameof(aSet));
        ExceptionEx.ThrowIfArgumentEmpty(bSet, nameof(bSet));
#endif

        return (aSet[aSet.Length - 1] >= bSet[0] && bSet[bSet.Length - 1] >= aSet[0]) // Check for disjoint sets
            &&
            (
                (aSet.Length <= 4 && bSet.Length <= 4)
                ? sortedInterectionTestLinear(aSet, bSet)
                : sortedInterectionTestBinary(aSet, bSet)
            );

        static bool sortedInterectionTestLinear(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
        {
            var aIndex = 0;
            var bIndex = 0;

            do
            {
                switch (aSet[aIndex].CompareTo(bSet[bIndex]))
                {
                    case 0:
                        return true;

                    case < 0:
                        aIndex++;

                        if (aIndex >= aSet.Length)
                        {
                            goto disjointOrEmptyCantMatch;
                        }

                        break;

                    default:
                        bIndex++;

                        if (bIndex >= bSet.Length)
                        {
                            goto disjointOrEmptyCantMatch;
                        }

                        break;
                }
            }
            while (true);

        disjointOrEmptyCantMatch:

            return false;
        }

        static bool sortedInterectionTestBinary(ReadOnlySpan<char> aSet, ReadOnlySpan<char> bSet)
        {
            do
            {
                if (aSet.Length > bSet.Length)
                {
                    MemoryEx.Swap(ref aSet, ref bSet);
                }

                var flagValuesIndex = bSet.BinarySearch(aSet[0]);
                if (flagValuesIndex >= 0)
                {
                    return true;
                }

                if (aSet.Length <= 1)
                {
                    break;
                }

                flagValuesIndex = ~flagValuesIndex;

                if (bSet.Length <= flagValuesIndex)
                {
                    break;
                }

                aSet = aSet.Slice(1);
                bSet = bSet.Slice(flagValuesIndex);
            }
            while (true);

            return false;
        }
    }

#endif

    private FlagSet(FlagValue value) : this((char)value)
    {
    }

    private FlagSet(char value) : this(value.ToString())
    {
    }

#if HAS_SEARCHVALUES

    private FlagSet(string values)
    {
        _values = values;
        _searchValues = SearchValues.Create(values);
    }

    private readonly string? _values;
    private readonly SearchValues<char>? _searchValues;

#else

    private FlagSet(string values)
    {
        _values = values;
    }

    private readonly string? _values;

#endif

    public int Count => _values is not null ? _values.Length : 0;

    public bool IsEmpty => _values is not { Length: > 0 };

    public bool HasItems => _values is { Length: > 0 };

    public FlagValue this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif
            if (_values is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return (FlagValue)_values![index];
        }
    }

    public IEnumerator<FlagValue> GetEnumerator() => GetInternalText().Select(static v => (FlagValue)v).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString()
    {
        if (IsEmpty)
        {
            return string.Empty;
        }

        if (_values!.All(static v => v < 128))
        {
            return _values!;
        }
        else
        {
            return string.Join(",", _values!.Select(static v => ((int)v).ToString(CultureInfo.InvariantCulture.NumberFormat)));
        }
    }

    public override int GetHashCode() => (int)StringEx.GetStableOrdinalHashCode(GetInternalText());

    public bool Equals(FlagSet other) => GetInternalText().Equals(other.GetInternalText(), StringComparison.Ordinal);

    public override bool Equals(object? obj) => obj is FlagSet set && Equals(set);



#if HAS_SEARCHVALUES

    public bool Contains(FlagValue value) => value != FlagValue.Zero && _searchValues!.Contains(value);

    public bool Contains(char value) => value != FlagValue.ZeroValue && _searchValues!.Contains(value);

    public bool DoesNotContain(FlagValue value) => value == FlagValue.Zero || !_searchValues!.Contains(value);

    private bool DoesNotContain(char value) => value == FlagValue.ZeroValue || !_searchValues!.Contains(value);

    public bool ContainsAny(FlagValue a, FlagValue b) =>
        ((ReadOnlySpan<char>)[a, b]).ContainsAny(_searchValues!);

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
        ((ReadOnlySpan<char>)[a, b, c]).ContainsAny(_searchValues!);

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
        ((ReadOnlySpan<char>)[a, b, c, d]).ContainsAny(_searchValues!);

    public bool ContainsAny(FlagSet other)
    {
        return _values is { Length: > 0 }
            &&
            other._values is { Length: > 0 }
            &&
            (
                _values.Length < other._values.Length
                ? _values.AsSpan().ContainsAny(other._searchValues!)
                : other._values.AsSpan().ContainsAny(_searchValues!)
            );
    }

    public bool DoesNotContainAny(FlagSet other) => !ContainsAny(other);

#else

    public bool Contains(FlagValue value) => Contains((char)value);

    private bool Contains(char value)
    {
        return
            value != FlagValue.ZeroValue
            &&
            MemoryEx.SortedContains(_values.AsSpan(), value);
    }

    public bool DoesNotContain(FlagValue value) => DoesNotContain((char)value);

    private bool DoesNotContain(char value)
    {
        return
            value == FlagValue.ZeroValue
            ||
            !MemoryEx.SortedContains(_values.AsSpan(), value);
    }

    public bool ContainsAny(FlagValue a, FlagValue b)
    {
        return HasItems && ContainsAnyUnpreparedMutable([a, b]);
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
            &&
            other.HasItems
            &&
            (
                other._values!.Length == 1
                ? Contains(other._values[0])
                : (
                    _values!.Length == 1
                        ? other.Contains(_values[0])
                        : SortedInterectionTest(other._values.AsSpan(), _values.AsSpan())
                )
            );
    }

    public bool DoesNotContainAny(FlagSet other)
    {
        return IsEmpty
            ||
            other.IsEmpty
            ||
            (
                other._values!.Length == 1
                ? DoesNotContain(other._values[0])
                : (
                    _values!.Length == 1
                        ? other.DoesNotContain(_values[0])
                        : !SortedInterectionTest(other._values.AsSpan(), _values.AsSpan())
                )
            );
    }

    private bool ContainsAnyUnpreparedMutable(Span<char> other)
    {
        if (_values is not { Length: > 0 })
        {
            return false;
        }

        PrepareMutableFlagValuesForUse(ref other);

        if (_values.Length == 1)
        {
            return other.Length == 1
                ? _values[0] == other[0]
                : MemoryEx.SortedContains(other, _values[0]);
        }

        return other.Length == 1
            ? MemoryEx.SortedContains(_values.AsSpan(), other[0])
            : SortedInterectionTest(_values.AsSpan(), other);
    }

#endif

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

        var oldValues = _values.AsSpan();

        var valueIndex = oldValues.BinarySearch((char)value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var builder = new StringBuilderSpan(oldValues.Length + 1);

        if (valueIndex >= oldValues.Length)
        {
            builder.Append(oldValues);
            builder.Append(value);
        }
        else if (valueIndex == 0)
        {
            builder.Append(value);
            builder.Append(oldValues);
        }
        else
        {
            builder.Append(oldValues.Slice(0, valueIndex));
            builder.Append(value);
            builder.Append(oldValues.Slice(valueIndex));
        }

        return new(builder.GetStringAndDispose());
    }

    internal string GetInternalText() => _values ?? string.Empty;

    public sealed class Builder
    {
        public Builder()
        {
            _builder = ArrayBuilder<char>.Pool.Get();
        }

        public Builder(int capacity)
        {
            _builder = capacity is >= 0 and <= 128
                ? ArrayBuilder<char>.Pool.Get(capacity)
                : ArrayBuilder<char>.Pool.Get();
        }

        private ArrayBuilder<char> _builder;

        public void Add(FlagValue value)
        {
            if (value.HasValue)
            {
                _builder.AddAsSortedSet(value);
            }
        }

        public void AddRange(IEnumerable<FlagValue> values)
        {
#if HAS_THROWNULL
            ArgumentNullException.ThrowIfNull(values);
#else
            ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void AddRange(FlagSet values)
        {
            AddRangePreSorted(values._values.AsSpan());
        }

        public FlagSet Create()
        {
            return new(_builder.AsSpan().ToString());
        }

        internal FlagSet MoveToFlagSet()
        {
            var result = new FlagSet(_builder.AsSpan().ToString());
            ArrayBuilder<char>.Pool.Return(_builder);
            _builder = null!; // This should operate as a very crude dispose
            return result;
        }

        private void AddRangePreSorted(ReadOnlySpan<char> values)
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
    }
}
