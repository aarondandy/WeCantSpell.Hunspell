using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    sealed class ArrayComparer<T> : IEqualityComparer<T[]>
        where T : IEquatable<T>
    {
        public static readonly ArrayComparer<T> Default = new ArrayComparer<T>();

        private static readonly StringComparer StringComparer = typeof(T) == typeof(string) ? StringComparer.Ordinal : null;

        private static readonly EqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

        public ArrayComparer()
        {
        }

        public bool Equals(T[] x, T[] y)
        {
            if (x == null)
            {
                return y == null;
            }

            if (y == null)
            {
                return false;
            }

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
                return x.AsSpan<T>().SequenceEqual(y.AsSpan<T>());
            }

            if (typeof(T) == typeof(string))
            {
                return CompareStrings((string[])(object)x, (string[])(object)y);
            }

            return CompareAnything(x, y);
        }

        private bool CompareStrings(string[] x, string[] y)
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

        private bool CompareAnything(T[] x, T[] y)
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

        public int GetHashCode(T[] obj)
        {
            if (obj == null)
            {
                return 0;
            }

            if (obj.Length == 0)
            {
                return 17;
            }

            unchecked
            {
                int hash = (17 * 31) + obj.Length.GetHashCode();

                hash = (hash * 31) + obj[0].GetHashCode();

                if (obj.Length > 1)
                {
                    if (obj.Length > 2)
                    {
                        hash = (hash * 31) + obj[obj.Length / 2].GetHashCode();
                    }

                    hash = (hash * 31) + obj[obj.Length - 1].GetHashCode();
                }

                return hash;
            }
        }
    }
}
