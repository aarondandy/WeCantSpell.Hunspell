using System;
using System.Collections.Generic;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    sealed class ArrayComparer<T> : IEqualityComparer<T[]>
    {
        public static readonly ArrayComparer<T> Default = new ArrayComparer<T>(EqualityComparer<T>.Default);

        public ArrayComparer(IEqualityComparer<T> valueComparer) =>
            ValueComparer = valueComparer ?? throw new ArgumentNullException(nameof(valueComparer));

        public IEqualityComparer<T> ValueComparer { get; }

        public bool Equals(T[] x, T[] y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x == null || y == null || x.Length != y.Length)
            {
                return false;
            }

            for (var i = 0; i < x.Length; i++)
            {
                if (!ValueComparer.Equals(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(T[] x, int xOffset, T[] y, int yOffset, int length)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }
            if (xOffset < 0 || xOffset >= x.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(xOffset));
            }
            if (yOffset < 0 || yOffset >= y.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(yOffset));
            }
            if (xOffset + length > x.Length || yOffset + length > y.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            for (var i = 0; i < length; i++)
            {
                if (!ValueComparer.Equals(x[xOffset + i], y[yOffset + i]))
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

            unchecked
            {
                var code = -obj.Length * 449;

                if (obj.Length > 0)
                {
                    code ^= ValueComparer.GetHashCode(obj[0]);
                    if (obj.Length > 1)
                    {
                        code ^= ValueComparer.GetHashCode(obj[obj.Length - 1]);
                        if (obj.Length > 2)
                        {
                            code ^= ValueComparer.GetHashCode(obj[obj.Length / 2]);
                        }
                    }
                }

                return code;
            }
        }
    }
}
