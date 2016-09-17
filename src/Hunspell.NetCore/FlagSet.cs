using System;
using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class FlagSet : ArrayWrapper<FlagValue>
    {
        public static readonly FlagSet Empty = new FlagSet(ArrayEx<FlagValue>.Empty);

        private FlagSet(FlagValue[] values)
            : base(values)
        {
        }

        internal static FlagSet TakeArray(FlagValue[] values)
        {
            Array.Sort(values);
            return new FlagSet(values);
        }

        public static FlagSet Create(IEnumerable<FlagValue> given)
        {
            var values = given.Distinct().ToArray();
            Array.Sort(values);
            return TakeArray(values);
        }

        public static FlagSet Combine(FlagSet set, FlagValue value)
        {
            var values = set.items.Concat(new[] { value }).Distinct().ToArray();
            Array.Sort(values);
            return TakeArray(values);
        }

        public static bool ContainsAny(FlagSet a, FlagSet b)
        {
            if (a == null || a.IsEmpty || b == null || b.IsEmpty)
            {
                return false;
            }
            if (a.Count == 1)
            {
                return b.Contains(a[0]);
            }
            if (b.Count == 1)
            {
                return a.Contains(b[0]);
            }

            if (a.Count > b.Count)
            {
                Swapper.Swap(ref a, ref b);
            }

            foreach (var item in a)
            {
                if (b.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(FlagValue value) => value.HasValue && Array.BinarySearch(items, value) >= 0;

        public bool ContainsAny(FlagSet values) => ContainsAny(this, values);

        public bool ContainsAny(FlagValue a, FlagValue b) =>
                (a.HasValue && Contains(a))
                ||
                (b.HasValue && Contains(b));

        public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
                (a.HasValue && Contains(a))
                ||
                (b.HasValue && Contains(b))
                ||
                (c.HasValue && Contains(c));

        public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
                (a.HasValue && Contains(a))
                ||
                (b.HasValue && Contains(b))
                ||
                (c.HasValue && Contains(c))
                ||
                (d.HasValue && Contains(d));
    }
}
