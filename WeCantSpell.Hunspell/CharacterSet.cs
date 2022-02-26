using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterSet : IReadOnlyList<char>
{
    public static readonly CharacterSet Empty = new(ImmutableArray<char>.Empty);

    public static CharacterSet Create(char value) => new(ImmutableArray.Create(value));

    public static CharacterSet Create(string values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var builder = ImmutableArray.CreateBuilder<char>(values.Length);
        builder.AddRange(values);
        builder.Sort();
        return new(builder.ToImmutable(true));
    }

    internal static CharacterSet Create(ReadOnlySpan<char> values)
    {
        var builder = ImmutableArray.CreateBuilder<char>(values.Length);

        foreach (var value in values)
        {
            builder.Add(value);
        }

        builder.Sort();

        return new(builder.ToImmutable(true));
    }

    private CharacterSet(ImmutableArray<char> values)
    {
#if DEBUG
        for (var i = 1; i < values.Length; i++)
        {
            if (values[i-1] > values[i])
            {
                throw new ArgumentOutOfRangeException(nameof(values));
            }
        }
#endif

        _mask = default;
        _values = values;

        foreach (var c in values)
        {
            unchecked
            {
                _mask |= c;
            }
        }
    }

    private readonly char _mask;
    private readonly ImmutableArray<char> _values;

    public int Count => _values.Length;
    public bool IsEmpty => _values.IsEmpty;
    public bool HasItems => !IsEmpty;
    public char this[int index] => _values[index];

    public ImmutableArray<char>.Enumerator GetEnumerator() => _values.GetEnumerator();
    IEnumerator<char> IEnumerable<char>.GetEnumerator() => ((IEnumerable<char>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    public bool Contains(char value) =>
        unchecked((value & _mask) != default)
        &&
        _values.BinarySearch(value) >= 0;

    public string GetCharactersAsString() => new string(_values.ToArray());

    public sealed class Comparer : IEqualityComparer<CharacterSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(CharacterSet x, CharacterSet y) => x._values.SequenceEqual(y._values);

        public int GetHashCode(CharacterSet obj) =>
            ((IStructuralEquatable)obj._values).GetHashCode(EqualityComparer<char>.Default);
    }
}
