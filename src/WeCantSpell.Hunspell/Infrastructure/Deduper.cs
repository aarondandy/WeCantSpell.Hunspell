using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal sealed class Deduper<T>
    {
        public Deduper(IEqualityComparer<T> comparer)
        {
            lookup = new Dictionary<T, T>(comparer);
        }

        private readonly Dictionary<T, T> lookup;

        public T GetEqualOrAdd(T item)
        {
            if (lookup.TryGetValue(item, out T existing))
            {
                return existing;
            }
            else
            {
                lookup[item] = item;
                return item;
            }
        }

        public void Add(T item)
        {
            if (!lookup.ContainsKey(item))
            {
                lookup[item] = item;
            }
        }
    }
}
