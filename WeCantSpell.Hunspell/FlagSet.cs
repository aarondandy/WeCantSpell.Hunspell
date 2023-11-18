using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct FlagSet : IReadOnlyList<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new(Array.Empty<FlagValue>(), default);

    public static bool operator ==(FlagSet left, FlagSet right) => left.Equals(right);

    public static bool operator !=(FlagSet left, FlagSet right) => !(left == right);

    public static FlagSet Create(FlagValue value) => new(new[] { value }, value);

    public static FlagSet Create(IEnumerable<FlagValue> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

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
            return new(new FlagValue[] { new FlagValue(text[0]) }, text[0]);
        }

        char mask = default;

        var values = new FlagValue[text.Length];
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];
            values[i] = new FlagValue(c);
            unchecked
            {
                mask |= c;
            }
        }

        Array.Sort(values);
        CollectionsEx.RemoveSortedDuplicates(ref values);
        return new(values, mask);
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

    private FlagSet(FlagValue[] values, char mask)
    {
        _mask = mask;
        _values = values;
    }

    private readonly FlagValue[] _values;
    private readonly char _mask;

    public int Count => _values.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _values is { Length: > 0 };
    public FlagValue this[int index] => _values[index];
    public IEnumerator<FlagValue> GetEnumerator() => ((IEnumerable<FlagValue>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

    internal FlagValue[] GetInternalArray() => _values;

    public FlagSet Union(FlagValue value)
    {
        if (!value.HasValue)
        {
            return this;
        }

        var valueIndex = Array.BinarySearch(_values, value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var newValues = new FlagValue[_values.Length + 1];
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

        if (other._values.Length == 1)
        {
            return Union(other._values[0]);
        }

        if (_values.Length == 1)
        {
            return other.Union(_values[0]);
        }

        var builder = new Builder();
        builder.AddRange(_values);
        builder.AddRange(other._values);
        return builder.MoveToFlagSet();
    }

    public bool Contains(FlagValue value)
    {
        if (_values is not null)
        {
            if (_values.Length == 1)
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

        static bool checkIterative(FlagValue[] searchSpace, FlagValue target)
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

    public bool ContainsAny(FlagSet values)
    {
        if (IsEmpty || values.IsEmpty || (_mask & values._mask) == default)
        {
            return false;
        }

        if (Count == 1)
        {
            return values.Contains(_values[0]);
        }

        if (values.Count == 1)
        {
            return Contains(values[0]);
        }

        return checkIterative(_values, values._values);

        static bool checkIterative(FlagValue[] aSet, FlagValue[] bSet)
        {
            var aIndex = 0;
            var bIndex = 0;

            while (aIndex < aSet.Length && bIndex < bSet.Length)
            {
                var a = aSet[aIndex];
                var b = bSet[bIndex];

                if (a == b)
                {
                    return true;
                }

                if (a < b)
                {
                    aIndex++;
                }
                else
                {
                    bIndex++;
                }
            }

            return false;
        }
    }

    public bool ContainsAny(FlagValue a, FlagValue b)
    {
        if (_values is not null && _values.Length != 0)
        {
            if (_values.Length == 1)
            {
                return _values[0].EqualsAny(a, b);
            }

            if (b < a)
            {
                (b, a) = (a, b);
            }

            var i = 0;
            if (a.HasValue && unchecked((a & _mask) == a))
            {
                i = Array.BinarySearch(_values, i, _values.Length - i, a);
                if (i >= 0)
                {
                    return true;
                }
                else
                {
                    i = ~i;
                }
            }

            if (b.HasValue && unchecked((b & _mask) == b))
            {
                i = Array.BinarySearch(_values, i, _values.Length - i, b);
                if (i >= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c)
    {
        if (_values is not null && _values.Length != 0)
        {
            if (_values.Length == 1)
            {
                return _values[0].EqualsAny(a, b, c);
            }

            if (c < b)
            {
                (c, b) = (b, c);
            }
            if (b < a)
            {
                (b, a) = (a, b);
            }
            if (c < b)
            {
                (c, b) = (b, c);
            }

            var i = 0;
            if (a.HasValue && unchecked((a & _mask) == a))
            {
                i = Array.BinarySearch(_values, i, _values.Length - i, a);
                if (i >= 0)
                {
                    return true;
                }
                else
                {
                    i = ~i;
                }
            }

            if (b.HasValue && unchecked((b & _mask) == b))
            {
                i = Array.BinarySearch(_values, i, _values.Length - i, b);
                if (i >= 0)
                {
                    return true;
                }
                else
                {
                    i = ~i;
                }
            }

            if (c.HasValue && unchecked((c & _mask) == c))
            {
                i = Array.BinarySearch(_values, i, _values.Length - i, c);
                if (i >= 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
        ContainsAny(a, b, c) || Contains(d);

    public bool Equals(FlagSet other) => _values.SequenceEqual(other._values);

    public override bool Equals(object? obj) => obj is FlagSet set && Equals(set);

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

    public override string ToString()
    {
        if (_values?.Length > 0)
        {
            if (Array.TrueForAll(_values, static v => v < 256))
            {
                var builder = StringBuilderPool.Get(_values.Length);

                foreach (var value in _values)
                {
                    builder.Append((char)value);
                }

                return StringBuilderPool.GetStringAndReturn(builder);
            }
            else
            {
                return string.Join(",", _values.Select(static v => ((int)v).ToString(CultureInfo.InvariantCulture.NumberFormat)));
            }
        }

        return string.Empty;
    }

    public sealed class Builder
    {
        public Builder()
        {
            _builder = ArrayBuilder<FlagValue>.Pool.Get();
        }

        public Builder(int capacity)
        {
            _builder = capacity is >= 0 and <= CollectionsEx.CollectionPreallocationLimit
                ? ArrayBuilder<FlagValue>.Pool.Get(capacity)
                : ArrayBuilder<FlagValue>.Pool.Get();
        }

        private ArrayBuilder<FlagValue> _builder;
        private char _mask = default;

        public void AddRange(IEnumerable<FlagValue> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void Add(FlagValue value)
        {
            if (value.HasValue)
            {
                _builder.AddAsSortedSet(value);
                unchecked
                {
                    _mask |= value;
                }
            }
        }

        public void AddRange(FlagSet values)
        {
            if (_builder.Count == 0)
            {
                _builder.AddRange(values._values);
                _mask = values._mask;
                return;
            }

            if (values.IsEmpty)
            {
                return;
            }

            var lowBoundIndex = 0;
            foreach (var value in values._values)
            {
                unchecked
                {
                    _mask |= value;
                }

                var valueIndex = _builder.BinarySearch(lowBoundIndex, _builder.Count - lowBoundIndex, value);
                if (valueIndex < 0)
                {
                    valueIndex = ~valueIndex; // locate the best insertion point

                    if (valueIndex >= _builder.Count)
                    {
#if DEBUG
                        if (valueIndex > _builder.Count)
                        {
                            throw new InvalidOperationException();
                        }
#endif
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

        public FlagSet Create() => new(_builder.MakeArray(), _mask);

        internal FlagSet MoveToFlagSet()
        {
            var result = new FlagSet(ArrayBuilder<FlagValue>.Pool.GetArrayAndReturn(_builder), _mask);
            _builder = null!; // This should operate as a very crude dispose
            return result;
        }
    }
}
