using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class StringDuplicateRemover
{
    public StringDuplicateRemover(StringComparer comparer)
    {
        _lookup = new(comparer);
    }

    public string GetEqualOrAdd(ReadOnlySpan<char> item) => GetEqualOrAdd(item.ToString());

    public void Reset()
    {
        _lookup.Clear();
    }

#if NO_HASHSET_TRYGET

    private readonly Dictionary<string, string> _lookup;

    public string GetEqualOrAdd(string item)
    {
        if (string.IsNullOrEmpty(item))
        {
            return string.Empty;
        }

        if (!_lookup.TryGetValue(item, out var result))
        {
            _lookup[item] = result = item;
        }

        return result;
    }

    public void Add(string item)
    {
        if (!_lookup.ContainsKey(item))
        {
            _lookup[item] = item;
        }
    }

#else

    private readonly HashSet<string> _lookup;

    public string GetEqualOrAdd(string item)
    {
        if (string.IsNullOrEmpty(item))
        {
            return string.Empty;
        }

        if (!_lookup.TryGetValue(item, out var result))
        {
            _lookup.Add(result = item);
        }

        return result;
    }

    public void Add(string item)
    {
        _lookup.Add(item);
    }

#endif

}
