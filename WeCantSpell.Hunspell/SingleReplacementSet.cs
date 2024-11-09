﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct SingleReplacementSet : IReadOnlyList<SingleReplacement>
{
    public static SingleReplacementSet Empty { get; } = new([]);

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

    private readonly SingleReplacement[]? _replacements;

    public int Count => _replacements is null ? 0 : _replacements.Length;

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
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            return _replacements![index];
        }
    }

    public IEnumerator<SingleReplacement> GetEnumerator() => ((IEnumerable<SingleReplacement>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal SingleReplacement[] GetInternalArray() => _replacements ?? [];
}
