using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class StringDeduper
{
    public StringDeduper() : this(StringComparer.Ordinal)
    {
    }

    public StringDeduper(IEqualityComparer<string> comparer)
    {
        _lookup = new Dictionary<string, string>(comparer);
        Add(string.Empty);
    }

    private readonly Dictionary<string, string> _lookup;

    public string GetEqualOrAdd(string item)
    {
        if (_lookup.TryGetValue(item, out string existing))
        {
            return existing;
        }
        else
        {
            _lookup[item] = item;
            return item;
        }
    }

    public void Add(string item)
    {
        if (!_lookup.ContainsKey(item))
        {
            _lookup[item] = item;
        }
    }
}
