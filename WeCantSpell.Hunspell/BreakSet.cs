using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct BreakSet : IReadOnlyList<string>
{
    public static BreakSet Empty { get; } = new(Array.Empty<string>());

    public static BreakSet Create(IEnumerable<string> entries) =>
        new((entries ?? throw new ArgumentNullException(nameof(entries))).ToArray());

    internal BreakSet(string[] entries)
    {
#if DEBUG
        if (entries is null) throw new ArgumentNullException(nameof(entries));
#endif
        _entries = entries;
    }

    private readonly string[] _entries;

    public int Count => _entries.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _entries is { Length: > 0 };
    public string this[int index] => _entries[index];

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)_entries).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();

    internal string[] GetInternalArray() => _entries;

    /// <summary>
    /// Calculate break points for recursion limit.
    /// </summary>
    internal int FindRecursionLimit(string scw)
    {
        var nbr = 0;

        if (scw.Length != 0)
        {
            foreach (var breakEntry in _entries)
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

    /// <summary>
    /// Calculate break points for recursion limit.
    /// </summary>
    internal int FindRecursionLimit(ReadOnlySpan<char> scw)
    {
        var nbr = 0;

        if (scw is { Length: > 0 })
        {
            foreach (var breakEntry in _entries)
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
