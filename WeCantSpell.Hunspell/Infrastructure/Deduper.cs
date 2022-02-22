using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class Deduper<T>
{
    public Deduper(IEqualityComparer<T> comparer)
    {
        _lookup = new(comparer);
    }

#if NO_HASHSET_TRYGET

    private readonly Dictionary<T, T> _lookup;

    public T GetEqualOrAdd(T item)
    {
        if (!_lookup.TryGetValue(item, out var result))
        {
            _lookup[item] = result = item;
        }

        return result;
    }

    public void Add(T item)
    {
        if (!_lookup.ContainsKey(item))
        {
            _lookup[item] = item;
        }
    }

#else

    private readonly HashSet<T> _lookup;

    public T GetEqualOrAdd(T item)
    {
        if (!_lookup.TryGetValue(item, out var result))
        {
            _lookup.Add(result = item);
        }

        return result;
    }

    public void Add(T item)
    {
        _lookup.Add(item);
    }

#endif
}
