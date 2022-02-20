using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class Deduper<T>
{
    public Deduper(IEqualityComparer<T> comparer)
    {
        _lookup = new Dictionary<T, T>(comparer);
    }

    private readonly Dictionary<T, T> _lookup;

    public T GetEqualOrAdd(T item)
    {
        if (_lookup.TryGetValue(item, out T existing))
        {
            return existing;
        }
        else
        {
            _lookup[item] = item;
            return item;
        }
    }

    public void Add(T item)
    {
        if (!_lookup.ContainsKey(item))
        {
            _lookup[item] = item;
        }
    }
}
