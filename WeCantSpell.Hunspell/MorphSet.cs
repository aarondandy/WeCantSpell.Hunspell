using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct MorphSet : IReadOnlyList<string>, IEquatable<MorphSet>
{
    public static MorphSet Empty { get; } = new(ImmutableArray<string>.Empty);

    public static bool operator ==(MorphSet left, MorphSet right) => left.Equals(right);

    public static bool operator !=(MorphSet left, MorphSet right) => !(left == right);

    public static MorphSet Create(string[] morphs) => new((morphs ?? throw new ArgumentNullException(nameof(morphs))).ToImmutableArray());

    internal static ImmutableArray<string> CreateReversedStrings(ImmutableArray<string> oldMorphs)
    {
        var newMorphs = ImmutableArray.CreateBuilder<string>(oldMorphs.Length);
        var lastIndex = oldMorphs.Length - 1;
        for (var i = oldMorphs.Length - 1; i >= 0; i--)
        {
            newMorphs.Add(oldMorphs[lastIndex - i].GetReversed());
        }

        return newMorphs.ToImmutable(allowDestructive: true);
    }

    internal MorphSet(ImmutableArray<string> morphs)
    {
#if DEBUG
        if (morphs.IsDefault) throw new ArgumentOutOfRangeException(nameof(morphs));
#endif
        _morphs = morphs;
    }

    private readonly ImmutableArray<string> _morphs;

    public int Count => _morphs.Length;
    public bool IsEmpty => _morphs.IsEmpty;
    public bool HasItems => !IsEmpty;
    public string this[int index] => _morphs[index];

    public ImmutableArray<string>.Enumerator GetEnumerator() => _morphs.GetEnumerator();
    IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)_morphs).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_morphs).GetEnumerator();

    internal string Join(string seperator) => string.Join(seperator, _morphs);

    public bool Equals(MorphSet other) => other._morphs.SequenceEqual(_morphs, StringComparer.Ordinal);

    public override bool Equals(object? obj) => obj is MorphSet set && Equals(set);

    public override int GetHashCode() => ((IStructuralEquatable)_morphs).GetHashCode(StringComparer.Ordinal);

    public class Comparer : IEqualityComparer<MorphSet>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(MorphSet x, MorphSet y) => x.Equals(y);

        public int GetHashCode(MorphSet obj) => obj.GetHashCode();
    }
}
