﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct MorphSet : IReadOnlyList<string>, IEquatable<MorphSet>
{
    public static MorphSet Empty { get; } = new(Array.Empty<string>());

    public static bool operator ==(MorphSet left, MorphSet right) => left.Equals(right);

    public static bool operator !=(MorphSet left, MorphSet right) => !(left == right);

    public static MorphSet Create(IEnumerable<string> morphs)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(morphs);
#else
        if (morphs is null) throw new ArgumentNullException(nameof(morphs));
#endif

        return new(morphs.ToArray());
    }

    internal static string[] CreateReversedStrings(string[] oldMorphs)
    {
        var newMorphs = new string[oldMorphs.Length];
        var lastIndex = oldMorphs.Length - 1;
        var newIndex = 0;
        for (var i = oldMorphs.Length - 1; i >= 0; i--)
        {
            newMorphs[newIndex++] = oldMorphs[lastIndex - i].GetReversed();
        }

        return newMorphs;
    }

    internal MorphSet(string[] morphs)
    {
        _morphs = morphs;
    }

    private readonly string[] _morphs;

    public int Count => _morphs.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _morphs is { Length: > 0 };
    public string this[int index] => _morphs[index];

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)_morphs).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _morphs.GetEnumerator();

    public bool Equals(MorphSet other) => other._morphs.SequenceEqual(_morphs, StringComparer.Ordinal);

    public override bool Equals(object? obj) => obj is MorphSet set && Equals(set);

    public override int GetHashCode() => ((IStructuralEquatable)_morphs).GetHashCode(StringComparer.Ordinal);

    public override string ToString() => Join(' ');

    internal string Join(string seperator) => string.Join(seperator, _morphs);

    internal string Join(char seperator) =>
#if NO_STATIC_STRINGCHAR_METHODS
        StringEx.Join(seperator, _morphs);
#else
        string.Join(seperator, _morphs);
#endif

    internal string[] GetInternalArray() => _morphs;
}
