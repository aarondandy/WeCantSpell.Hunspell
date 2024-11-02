using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct MorphSet : IReadOnlyList<string>, IEquatable<MorphSet>
{
    public static MorphSet Empty { get; } = new([]);

    public static bool operator ==(MorphSet left, MorphSet right) => left.Equals(right);

    public static bool operator !=(MorphSet left, MorphSet right) => !(left == right);

    public static MorphSet Create(IEnumerable<string> morphs)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(morphs);
#else
        ExceptionEx.ThrowIfArgumentNull(morphs, nameof(morphs));
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

    private readonly string[]? _morphs;

    public int Count => _morphs is null ? 0 : _morphs.Length;

    public bool IsEmpty => _morphs is not { Length: > 0 };

    public bool HasItems => _morphs is { Length: > 0 };

    public string this[int index]
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

            return _morphs![index];
        }
    }

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Equals(MorphSet other) => GetInternalArray().AsSpan().SequenceEqual(GetInternalArray().AsSpan());

    public override bool Equals(object? obj) => obj is MorphSet set && Equals(set);

    public override int GetHashCode() => ((IStructuralEquatable)GetInternalArray()).GetHashCode(StringComparer.Ordinal);

    public override string ToString() => Join(' ');

    internal string Join(string seperator) => string.Join(seperator, GetInternalArray());

    internal string Join(char seperator) =>
#if NO_STATIC_STRINGCHAR_METHODS
        StringEx.Join(seperator, GetInternalArray());
#else
        string.Join(seperator, GetInternalArray());
#endif

    internal string[] GetInternalArray() => _morphs ?? [];
}
