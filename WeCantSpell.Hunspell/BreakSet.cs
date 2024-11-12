﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct BreakSet : IReadOnlyList<string>
{
    public static BreakSet Empty { get; } = new([]);

    public static BreakSet Create(IEnumerable<string> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal BreakSet(string[] entries)
    {
        _entries = entries;
    }

    private readonly string[]? _entries;

    public int Count => _entries is null ? 0 : _entries.Length;

    public bool IsEmpty => _entries is not { Length: > 0 };

    public bool HasItems => _entries is { Length: > 0 };

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

            return _entries![index];
        }
    }

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal string[] GetInternalArray() => _entries ?? [];

    /// <summary>
    /// Calculate break points for recursion limit.
    /// </summary>
    internal int FindRecursionLimit(string scw)
    {
        var nbr = 0;

        if (scw.Length != 0 && HasItems)
        {
            foreach (var breakEntry in _entries!)
            {
                var pos = 0;
                while ((pos = scw.IndexOf(breakEntry, pos, StringComparison.Ordinal)) >= 0)
                {
                    nbr++;
                    pos += breakEntry.Length;
                }
            }
        }

        return nbr;
    }
}
