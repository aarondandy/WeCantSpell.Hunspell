using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct SingleReplacementSet : IReadOnlyList<SingleReplacement>
{
    public static SingleReplacementSet Empty { get; } = new(Array.Empty<SingleReplacement>());

    public static SingleReplacementSet Create(IEnumerable<SingleReplacement> replacements)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(replacements);
#else
        if (replacements is null) throw new ArgumentNullException(nameof(replacements));
#endif

        return new(replacements.ToArray());
    }

    internal SingleReplacementSet(SingleReplacement[] replacements)
    {
        _replacements = replacements;
    }

    private readonly SingleReplacement[] _replacements;

    public int Count => _replacements.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _replacements is { Length: > 0 };
    public SingleReplacement this[int index] => _replacements[index];

    public IEnumerator<SingleReplacement> GetEnumerator() => ((IEnumerable<SingleReplacement>)_replacements).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _replacements.GetEnumerator();

    internal SingleReplacement[] GetInternalArray() => _replacements ?? Array.Empty<SingleReplacement>();
}
