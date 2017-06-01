using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal sealed class StringDeduper
    {
        public StringDeduper()
            : this(StringComparer.Ordinal)
        {
        }

        public StringDeduper(IEqualityComparer<string> comparer)
        {
            lookup = new Dictionary<string, string>(comparer);
            Add(string.Empty);
        }

        private readonly Dictionary<string, string> lookup;

        public string GetEqualOrAdd(string item)
        {
            if (lookup.TryGetValue(item, out string existing))
            {
                return existing;
            }
            else
            {
                lookup[item] = item;
                return item;
            }
        }

        public void Add(string item)
        {
            if (!lookup.ContainsKey(item))
            {
                lookup[item] = item;
            }
        }
    }
}
