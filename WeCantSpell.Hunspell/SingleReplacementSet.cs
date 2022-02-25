using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct SingleReplacementSet : IReadOnlyList<SingleReplacement>
{
    internal SingleReplacementSet(ImmutableArray<SingleReplacement> replacements)
    {
#if DEBUG
        if (replacements.IsDefault) throw new ArgumentOutOfRangeException(nameof(replacements));
#endif
        _replacements = replacements;
    }

    private readonly ImmutableArray<SingleReplacement> _replacements;

    public int Count => _replacements.Length;
    public bool IsEmpty => _replacements.IsEmpty;
    public SingleReplacement this[int index] => _replacements[index];

    public ImmutableArray<SingleReplacement>.Enumerator GetEnumerator() => _replacements.GetEnumerator();
    IEnumerator<SingleReplacement> IEnumerable<SingleReplacement>.GetEnumerator() => ((IEnumerable<SingleReplacement>)_replacements).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_replacements).GetEnumerator();
}
