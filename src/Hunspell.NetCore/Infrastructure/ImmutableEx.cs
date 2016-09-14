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
    }
}
