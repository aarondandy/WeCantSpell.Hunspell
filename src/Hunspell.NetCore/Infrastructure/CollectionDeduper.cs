using System.Collections.Generic;

namespace Hunspell.Infrastructure
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
            T existing;
            if (lookup.TryGetValue(item, out existing))
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
            T existing;
            if (!lookup.TryGetValue(item, out existing))
            {
                lookup[item] = item;
            }
        }
    }
}
