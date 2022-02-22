using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class ArrayComparer<T> : IEqualityComparer<T[]>
    where T : IEquatable<T>
{
    public static readonly ArrayComparer<T> Default = new();

    private static readonly StringComparer StringComparer = typeof(T) == typeof(string) ? StringComparer.Ordinal : null;

    private static readonly EqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

    public ArrayComparer()
    {
    }

    public bool Equals(T[]? x, T[]? y)
    {
        if (x is null) return y == null;

        if (y is null) return false;

        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x.Length != y.Length)
        {
            return false;
        }

        if (typeof(T) == typeof(char) || typeof(T) == typeof(FlagValue))
        {
            return x.AsSpan().SequenceEqual(y.AsSpan());
        }

        if (typeof(T) == typeof(string))
        {
#if NO_SPAN_SEQUENCEEQUAL_COMPARER
            sequenceEqualsString((string[])(object)x, (string[])(object)y);
            static bool sequenceEqualsString(string[] x, string[] y)
            {
                for (var i = 0; i < x.Length; i++)
                {
                    if (!StringComparer.Equals(x[i], y[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
#else
            return ((string[])(object)x).AsSpan().SequenceEqual((string[])(object)y, StringComparer);
#endif
        }

#if NO_SPAN_SEQUENCEEQUAL_COMPARER
        return compareAnything(x, y);
        static bool compareAnything(T[] x, T[] y)
        {
            for (var i = 0; i < x.Length; i++)
            {
                if (!EqualityComparer.Equals(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }
#else
        return x.AsSpan().SequenceEqual(y, EqualityComparer);
#endif
    }

    public int GetHashCode(T[] obj)
    {
        if (obj is not { Length: > 0 })
        {
            return 0;
        }

        if (obj.Length == 1)
        {
            return HashCode.Combine(obj.Length, obj[0]);
        }
        else if (obj.Length == 2)
        {
            return HashCode.Combine(obj.Length, obj[0], obj[1]);
        }

        return HashCode.Combine(obj.Length, obj[0], obj[obj.Length / 2], obj[obj.Length - 1]);
    }
}
