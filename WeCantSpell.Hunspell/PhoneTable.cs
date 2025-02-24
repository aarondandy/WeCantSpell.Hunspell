using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#if HAS_FROZENDICTIONARY
using System.Collections.Frozen;
#endif

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct PhoneTable : IReadOnlyList<PhoneticEntry>
{
    public static PhoneTable Empty { get; } = new([]);

    public static PhoneTable Create(IEnumerable<PhoneticEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

        return new(entries.ToArray());
    }

#if HAS_FROZENDICTIONARY
    private static FrozenDictionary<char, PhoneticEntry[]> BuildByFirstRuleCharLookup(PhoneticEntry[] entries)
#else
    private static Dictionary<char, PhoneticEntry[]> BuildByFirstRuleCharLookup(PhoneticEntry[] entries)
#endif
    {
        var lookupBuilder = new Dictionary<char, ArrayBuilder<PhoneticEntry>>();

        foreach (var entry in entries)
        {
            if (entry.Rule is { Length: > 0 })
            {
                var key = entry.Rule[0];

                if (!lookupBuilder.TryGetValue(key, out var entriesBuilder))
                {
                    entriesBuilder = new(1);
                    lookupBuilder.Add(key, entriesBuilder);
                }

                entriesBuilder.Add(entry);
            }
        }

#if HAS_FROZENDICTIONARY
        return lookupBuilder.ToFrozenDictionary(static group => group.Key, static group => group.Value.Extract());
#else
        return lookupBuilder.ToDictionary(static entry => entry.Key, static entry => entry.Value.Extract());
#endif
    }

    internal PhoneTable(PhoneticEntry[] items)
    {
        _items = items;
        _byFirstRuleChar = BuildByFirstRuleCharLookup(items);
    }

    private readonly PhoneticEntry[]? _items;

#if HAS_FROZENDICTIONARY
    private readonly FrozenDictionary<char, PhoneticEntry[]>? _byFirstRuleChar;
#else
    private readonly Dictionary<char, PhoneticEntry[]>? _byFirstRuleChar;
#endif

    public int Count => _items is not null ? _items.Length : 0;

    public bool IsEmpty => _items is not { Length: > 0 };

    public bool HasItems => _items is { Length: > 0 };

    public PhoneticEntry this[int index]
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

            if (_items is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _items![index];
        }
    }

    public IEnumerator<PhoneticEntry> GetEnumerator() => ((IEnumerable<PhoneticEntry>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal PhoneticEntry[] GetInternalArray() => _items ?? [];

    internal PhoneticEntry[] GetInternalArrayByFirstRuleChar(char ruleKey) => _byFirstRuleChar?.GetValueOrDefault(ruleKey) ?? [];
}
