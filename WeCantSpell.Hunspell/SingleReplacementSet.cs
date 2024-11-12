using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct SingleReplacementSet : IReadOnlyList<SingleReplacement>
{
    public static SingleReplacementSet Empty { get; } = new([]);

    public static SingleReplacementSet Create(IEnumerable<SingleReplacement> replacements)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(replacements);
#else
        ExceptionEx.ThrowIfArgumentNull(replacements, nameof(replacements));
#endif

        return new(replacements.ToArray());
    }

    internal SingleReplacementSet(SingleReplacement[] replacements)
    {
        _replacements = replacements;
    }

    private readonly SingleReplacement[]? _replacements;

    public int Count => _replacements is not null ? _replacements.Length : 0;

    public bool IsEmpty => _replacements is not { Length: > 0 };

    public bool HasItems => _replacements is { Length: > 0 };

    public SingleReplacement this[int index]
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

            return _replacements![index];
        }
    }

    public IEnumerator<SingleReplacement> GetEnumerator() => ((IEnumerable<SingleReplacement>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal SingleReplacement[] GetInternalArray() => _replacements ?? [];
}
