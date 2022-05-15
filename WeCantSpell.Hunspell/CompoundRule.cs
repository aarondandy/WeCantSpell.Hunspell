using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct CompoundRule : IReadOnlyList<FlagValue>
{
    public static CompoundRule Empty { get; } = new(Array.Empty<FlagValue>());

    public static CompoundRule Create(IEnumerable<FlagValue> values) =>
        new((values ?? throw new ArgumentNullException(nameof(values))).ToArray());

    internal CompoundRule(FlagValue[] items)
    {
#if DEBUG
        if (items is null) throw new ArgumentNullException(nameof(items));
#endif
        _values = items;
    }

    private readonly FlagValue[] _values;

    public int Count => _values.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _values is { Length: > 0 };
    public FlagValue this[int index] => _values[index];
    public IEnumerator<FlagValue> GetEnumerator() => ((IEnumerable<FlagValue>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

    internal bool IsWildcard(int index) => (char)_values[index] is '*' or '?';

    internal bool ContainsRuleFlagForEntry(in WordEntryDetail details)
    {
        foreach (var flag in _values)
        {
            if (!flag.IsWildcard && details.ContainsFlag(flag))
            {
                return true;
            }
        }

        return false;
    }
}
