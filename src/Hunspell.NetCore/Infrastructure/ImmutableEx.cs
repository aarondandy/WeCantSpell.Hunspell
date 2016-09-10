using System;
using System.Collections.Immutable;

namespace Hunspell.Infrastructure
{
    internal static class ImmutableEx
    {
        public static ImmutableArray<T> MoveToOrCreateImmutable<T>(this ImmutableArray<T>.Builder builder)
        {
            return builder.Capacity == builder.Count
                ? builder.MoveToImmutable()
                : builder.ToImmutable();
        }

        public static bool ContainsAny<T>(this ImmutableSortedSet<T> a, ImmutableSortedSet<T> b)
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
    }
}
