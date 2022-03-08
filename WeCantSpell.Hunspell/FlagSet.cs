using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        return builder.Create(allowDestructive: true);
    }

    internal static FlagSet ParseAsChars(ReadOnlySpan<char> text) =>
        text.IsEmpty ? Empty : new Builder(text).Create(allowDestructive: true);

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

    private FlagSet(FlagValue[] values, char mask)
    {
        _mask = mask;
        Values = values;
    }

    private readonly char _mask;
    internal FlagValue[] Values { get; }

    public int Count => Values.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Values is { Length: > 0 };
    public FlagValue this[int index] => Values[index];
public IEnumerator<FlagValue> GetEnumerator() => ((IEnumerable<FlagValue>)Values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();

    public FlagSet Union(FlagValue value)
    {
        var valueIndex = Array.BinarySearch(Values, value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var newValues = new FlagValue[Values.Length + 1];
        if (valueIndex >= Values.Length)
        {
            Values.CopyTo(newValues.AsSpan());
            newValues[Values.Length] = value;
        }
        else if (valueIndex == 0)
        {
            newValues[0] = value;
            Values.CopyTo(newValues.AsSpan(1));
        }
        else
        {
            Values.AsSpan(0, valueIndex).CopyTo(newValues.AsSpan());
            Values.AsSpan(valueIndex).CopyTo(newValues.AsSpan(valueIndex + 1));
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

        if (other.Values.Length == 1)
        {
            return Union(other.Values[0]);
        }

        if (Values.Length == 1)
        {
            return other.Union(Values[0]);
        }

        var builder = new Builder();
        builder.AddRange(Values);
        builder.AddRange(other.Values);
        return builder.Create(allowDestructive: true);
    }

    public bool Contains(FlagValue value)
    {
        if (value.HasValue && HasItems)
        {
            if (Values.Length == 1)
            {
                return Values[0].Equals(value);
            }

            if (unchecked(value & _mask) != default)
            {
                if (Values.Length <= 8)
                {
                    return Values.Contains(value);
                }

                return Array.BinarySearch(Values, value) >= 0;
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
            return values.Contains(Values[0]);
        }

        if (values.Count == 1)
        {
            return Contains(values[0]);
        }

        return checkIterative(Values, values.Values);

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

    public bool ContainsAny(FlagValue a, FlagValue b) =>
        HasItems && (Contains(a) || Contains(b));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c) || Contains(d));

    public bool Equals(FlagSet other) => Values.SequenceEqual(other.Values);

    public override bool Equals(object? obj) => obj is FlagSet set && Equals(set);

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

    public class Comparer : IEqualityComparer<FlagSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(FlagSet x, FlagSet y) => x.Values.SequenceEqual(y.Values);

        public int GetHashCode(FlagSet obj) => HashCode.Combine(obj.Count, obj._mask);
    }

    public sealed class Builder
    {
        public Builder()
        {
            _builder = new();
        }

        public Builder(int capacity)
        {
            _builder = new(capacity);
        }

        public Builder(ReadOnlySpan<char> text)
        {
            _mask = default;

            var values = new FlagValue[text.Length];
            for (var i = 0; i < text.Length; i++)
            {
                ref readonly var c = ref text[i];
                values[i] = (FlagValue)c;
                unchecked
                {
                    _mask |= c;
                }
            }

            Array.Sort(values);
            CollectionsEx.RemoveSortedDuplicates(ref values);
            _builder = new(values);
        }

        private readonly ArrayBuilder<FlagValue> _builder;
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
            _builder.AddAsSortedSet(value);
            unchecked
            {
                _mask |= value;
            }
        }

        public void AddRange(FlagSet values)
        {
            if (_builder.Count == 0)
            {
                _builder.AddRange(values.Values);
                _mask = values._mask;
                return;
            }

            if (values.IsEmpty)
            {
                return;
            }

            var lowBoundIndex = 0;
            foreach (var value in values.Values)
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

        public FlagSet Create() => Create(allowDestructive: false);

        internal FlagSet Create(bool allowDestructive) => new(_builder.MakeOrExtractArray(allowDestructive), _mask);
    }
}
