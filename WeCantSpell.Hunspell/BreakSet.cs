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
        Entries = entries;
    }

    internal string[] Entries { get; }

    public int Count => Entries.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Entries is { Length: > 0 };
    public string this[int index] => Entries[index];

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)Entries).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Entries.GetEnumerator();

    /// <summary>
    /// Calculate break points for recursion limit.
    /// </summary>
    internal int FindRecursionLimit(string scw)
    {
        var nbr = 0;

        if (scw.Length != 0)
        {
            foreach (var breakEntry in Entries)
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
            foreach (var breakEntry in Entries)
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
