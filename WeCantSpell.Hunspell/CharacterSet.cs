using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterSet : IReadOnlyList<char>, IEquatable<CharacterSet>
{
    public static readonly CharacterSet Empty = new(ImmutableArray<char>.Empty, default);

    public static bool operator ==(CharacterSet left, CharacterSet right) => left.Equals(right);

    public static bool operator !=(CharacterSet left, CharacterSet right) => !(left == right);

    public static CharacterSet Create(char value) => new(ImmutableArray.Create(value), value);

    public static CharacterSet Create(IEnumerable<char> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var builder = new Builder();
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

    public static CharacterSet Create(string values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        return Create(values.AsSpan());
    }

    public static CharacterSet Create(ReadOnlySpan<char> values)
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        var builder = new Builder(values.Length);
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

    private CharacterSet(ImmutableArray<char> values, char mask)
    {
        _mask = mask;
        _values = values;
    }

    private readonly char _mask;
    private readonly ImmutableArray<char> _values;

    public int Count => _values.Length;
    public bool IsEmpty => _values.IsDefaultOrEmpty;
    public bool HasItems => !IsEmpty;
    public char this[int index] => _values[index];

    public ImmutableArray<char>.Enumerator GetEnumerator() => _values.GetEnumerator();
    IEnumerator<char> IEnumerable<char>.GetEnumerator() => ((IEnumerable<char>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    public bool Contains(char value)
    {
        if (HasItems)
        {
            if (_values.Length == 1)
            {
                return _values[0] == value;
            }

            if (unchecked((value & _mask) != default))
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

    public string GetCharactersAsString()
    {
        var builder = StringBuilderPool.Get(_values.AsSpan());
        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public bool Equals(CharacterSet obj) => _values.SequenceEqual(obj._values);

    public override bool Equals(object? obj) => obj is CharacterSet set && Equals(set);

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

    public sealed class Comparer : IEqualityComparer<CharacterSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(CharacterSet x, CharacterSet y) => x._values.SequenceEqual(y._values);

        public int GetHashCode(CharacterSet obj) => HashCode.Combine(obj.Count, obj._mask);
    }

    public sealed class Builder
    {
        public Builder()
        {
            _builder = ImmutableArray.CreateBuilder<char>();
        }

        public Builder(int capacity)
        {
            _builder = ImmutableArray.CreateBuilder<char>(capacity);
        }

        private readonly ImmutableArray<char>.Builder _builder;
        private char _mask = default;

        public void AddRange(IEnumerable<char> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void AddRange(ReadOnlySpan<char> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void Add(char value)
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

        public CharacterSet Create() => Create(allowDestructive: false);

        internal CharacterSet Create(bool allowDestructive) => new(_builder.ToImmutable(allowDestructive: allowDestructive), _mask);
    }
}
