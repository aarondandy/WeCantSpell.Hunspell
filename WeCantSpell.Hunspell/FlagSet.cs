using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct FlagSet : IReadOnlyList<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new(ImmutableArray<FlagValue>.Empty, default);

    public static bool operator ==(FlagSet left, FlagSet right) => left.Equals(right);

    public static bool operator !=(FlagSet left, FlagSet right) => !(left == right);

    public static FlagSet Create(FlagValue value) => new(ImmutableArray.Create(value), value);

    public static FlagSet Create(IEnumerable<FlagValue> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var builder = values is ICollection collection ? new Builder(collection.Count) : new Builder();
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

    internal static FlagSet ParseAsChars(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        var builder = new Builder(text.Length);

        foreach (var @char in text)
        {
            builder.Add(new FlagValue(@char));
        }

        return builder.Create(allowDestructive: true);
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

        if (lastIndex % 2 == 0)
        {
            builder.Add(new FlagValue(text[lastIndex]));
        }

        return builder.Create(allowDestructive: true);
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

        return flags.Create(allowDestructive: true);
    }

    private FlagSet(ImmutableArray<FlagValue> values, char mask)
    {
        _mask = mask;
        _values = values;
    }

    private readonly char _mask;
    private readonly ImmutableArray<FlagValue> _values;

    public int Count => _values.Length;
    public bool IsEmpty => _values.IsDefaultOrEmpty;
    public bool HasItems => !IsEmpty;
    public FlagValue this[int index] => _values[index];

    public ImmutableArray<FlagValue>.Enumerator GetEnumerator() => _values.GetEnumerator();
    IEnumerator<FlagValue> IEnumerable<FlagValue>.GetEnumerator() => ((IEnumerable<FlagValue>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    public FlagSet Union(FlagValue value)
    {
        var valueIndex = _values.BinarySearch(value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var newValues = valueIndex >= _values.Length ? _values.Add(value) : _values.Insert(valueIndex, value);

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
        return builder.Create(allowDestructive: true);
    }

    public bool Contains(FlagValue value)
    {
        if (value.HasValue && HasItems)
        {
            if (_values.Length == 1)
            {
                return _values[0].Equals(value);
            }

            if (unchecked(value & _mask) != default)
            {
                if (_values.Length <= 8)
                {
                    return _values.Contains(value);
                }

                return _values.BinarySearch(value) >= 0;
            }
        }

        return false;
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

        static bool checkIterative(ImmutableArray<FlagValue> aSet, ImmutableArray<FlagValue> bSet)
        {
            int aIndex = 0;
            int bIndex = 0;

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

    public bool ContainsAny(FlagValue a, FlagValue b) =>
        HasItems && (Contains(a) || Contains(b));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c) || Contains(d));

    public bool Equals(FlagSet other) => _values.SequenceEqual(other._values);

    public override bool Equals(object? obj) => obj is FlagSet set && Equals(set);

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

    public class Comparer : IEqualityComparer<FlagSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(FlagSet x, FlagSet y) => x._values.SequenceEqual(y._values);

        public int GetHashCode(FlagSet obj) => HashCode.Combine(obj.Count, obj._mask);
    }

    public sealed class Builder
    {
        public Builder()
        {
            _builder = ImmutableArray.CreateBuilder<FlagValue>();
        }

        public Builder(int capacity)
        {
            _builder = ImmutableArray.CreateBuilder<FlagValue>(capacity);
        }

        private readonly ImmutableArray<FlagValue>.Builder _builder;
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
            if (_builder.Count == 0)
            {
                _builder.Add(value);
                _mask = value;
                return;
            }

            var valueIndex = _builder.BinarySearch(value);
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
            foreach (var value in values)
            {
                unchecked
                {
                    _mask |= value;
                }

                var valueIndex = _builder.BinarySearch(value, lowBoundIndex, _builder.Count - 1);
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

        public FlagSet Create() => Create(allowDestructive: false);

        internal FlagSet Create(bool allowDestructive) => new(_builder.ToImmutable(allowDestructive: allowDestructive), _mask);
    }
}
