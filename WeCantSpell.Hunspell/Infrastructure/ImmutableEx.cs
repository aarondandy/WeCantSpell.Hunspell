using System.Collections.Immutable;

namespace WeCantSpell.Hunspell.Infrastructure;

internal static class ImmutableEx
{
    public static ImmutableArray<T> ToImmutable<T>(this ImmutableArray<T>.Builder builder, bool allowDestructive)
    {
        if (allowDestructive && builder.Capacity == builder.Count)
        {
            return builder.MoveToImmutable();
        }

        return builder.ToImmutable();
    }
}
