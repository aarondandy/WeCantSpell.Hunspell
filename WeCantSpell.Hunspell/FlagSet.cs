using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct FlagSet : IReadOnlyList<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new(ImmutableArray<FlagValue>.Empty);

    public static bool operator ==(FlagSet left, FlagSet right) => left.Equals(right);

    public static bool operator !=(FlagSet left, FlagSet right) => !(left == right);

    public static FlagSet Create(FlagValue value) => new(ImmutableArray.Create(value));

    public static FlagSet Create(IEnumerable<FlagValue> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var hashSet = new HashSet<FlagValue>();
        var builder = ImmutableArray.CreateBuilder<FlagValue>();

        foreach (var value in values)
        {
            if (hashSet.Add(value))
            {
                builder.Add(value);
            }
        }

        builder.Sort();

        return new(builder.ToImmutable(allowDestructive: true));
    }

    [Obsolete]
    internal static FlagSet Create(FlagValue[] values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var builder = ImmutableArray.CreateBuilder<FlagValue>(values.Length);
        builder.AddRange(values);
        return CreateDestructuve(builder);
    }

    internal static FlagSet CreateDestructuve(ImmutableArray<FlagValue>.Builder builder)
    {
        var hashSet = new HashSet<FlagValue>();

        var i = 0;
        while (i < builder.Count)
        {
            if (hashSet.Add(builder[i]))
            {
                i++;
            }
            else
            {
                builder.RemoveAt(i);
            }
        }

        builder.Sort();
        return new(builder.ToImmutable(allowDestructive: true));
    }

    private static char CalculateMask(ImmutableArray<FlagValue> values)
    {
#if DEBUG
        var previous = values.FirstOrDefault();
#endif

        char mask = default;
        foreach (var c in values)
        {
#if DEBUG
            if (previous > c)
            {
                throw new ArgumentOutOfRangeException(nameof(values));
            }

            previous = c;
#endif

            unchecked
            {
                mask |= c;
            }
        }

        return mask;
    }


    private FlagSet(ImmutableArray<FlagValue> values) : this(values, CalculateMask(values))
    {
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

    internal FlagSet Union(FlagValue value)
    {
        var valueIndex = _values.BinarySearch(value);
        if (valueIndex >= 0)
        {
            return this;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        return new(valueIndex >= _values.Length ? _values.Add(value) : _values.Insert(valueIndex, value));
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

        return Create(_values.Concat(other._values));
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

        if (values.Count == 1)
        {
            return Contains(values[0]);
        }

        if (Count == 1)
        {
            return values.Contains(_values[0]);
        }

        // TODO: use the sorted nature of the data as an advantage when detecting an intersection

        return Count <= values.Count
            ? checkIterative(this, values)
            : checkIterative(values, this);

        static bool checkIterative(FlagSet a, FlagSet b)
        {
            foreach (var value in a)
            {
                if (b.Contains(value))
                {
                    return true;
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

    public override int GetHashCode() => ((IStructuralEquatable)_values).GetHashCode(EqualityComparer<FlagValue>.Default);

    public class Comparer : IEqualityComparer<FlagSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(FlagSet x, FlagSet y) => x._values.SequenceEqual(y._values);
        public int GetHashCode(FlagSet obj) => ((IStructuralEquatable)obj._values).GetHashCode(EqualityComparer<FlagValue>.Default);
    }
}
