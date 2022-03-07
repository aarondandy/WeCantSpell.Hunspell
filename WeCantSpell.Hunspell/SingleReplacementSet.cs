using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct SingleReplacementSet : IReadOnlyList<SingleReplacement>
{
    public static SingleReplacementSet Empty { get; } = new(Array.Empty<SingleReplacement>());

    public static SingleReplacementSet Create(IEnumerable<SingleReplacement> replacements) =>
        new((replacements ?? throw new ArgumentNullException(nameof(replacements))).ToArray());

    internal SingleReplacementSet(SingleReplacement[] replacements)
    {
#if DEBUG
        if (replacements is null) throw new ArgumentNullException(nameof(replacements));
#endif
        Replacements = replacements;
    }

    internal SingleReplacement[] Replacements { get; }

    public int Count => Replacements.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Replacements is { Length: > 0 };
    public SingleReplacement this[int index] => Replacements[index];
    public IEnumerator<SingleReplacement> GetEnumerator() => ((IEnumerable<SingleReplacement>)Replacements).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Replacements.GetEnumerator();
}
