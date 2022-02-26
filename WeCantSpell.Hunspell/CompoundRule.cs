using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell;

public readonly struct CompoundRule : IReadOnlyList<FlagValue>
{
    internal CompoundRule(ImmutableArray<FlagValue> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _values = items;
    }

    private readonly ImmutableArray<FlagValue> _values;

    public int Count => _values.Length;
    public bool IsEmpty => _values.IsEmpty;
    public bool HasItems => !IsEmpty;
    public FlagValue this[int index] => _values[index];

    public ImmutableArray<FlagValue>.Enumerator GetEnumerator() => _values.GetEnumerator();
    IEnumerator<FlagValue> IEnumerable<FlagValue>.GetEnumerator() => ((IEnumerable<FlagValue>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_values).GetEnumerator();

    internal bool IsWildcard(int index) => (char)_values[index] is '*' or '?';

    internal bool ContainsRuleFlagForEntry(WordEntryDetail details)
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
