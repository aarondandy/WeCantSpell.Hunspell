using System;
using System.Collections.Generic;

namespace Hunspell.Infrastructure
{
    internal sealed class ListComparer<T> : IEqualityComparer<List<T>>
    {
        public static readonly ListComparer<T> Default = new ListComparer<T>(EqualityComparer<T>.Default);

        public ListComparer(IEqualityComparer<T> valueComparer)
        {
            if (valueComparer == null)
            {
                throw new ArgumentNullException(nameof(valueComparer));
            }

            ValueComparer = valueComparer;
        }

        public IEqualityComparer<T> ValueComparer { get; }

        public bool Equals(List<T> x, List<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x == null || y == null || x.Count != y.Count)
            {
                return false;
            }

            for (var i = 0; i < x.Count; i++)
            {
                if (!ValueComparer.Equals(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(List<T> obj)
        {
            if (obj == null)
            {
                return 0;
            }

            unchecked
            {
                var code = -obj.Count * 997;

                if (obj.Count > 0)
                {
                    code ^= ValueComparer.GetHashCode(obj[0]);
                    if (obj.Count > 1)
                    {
                        code ^= ValueComparer.GetHashCode(obj[obj.Count - 1]);
                        if (obj.Count > 2)
                        {
                            code ^= ValueComparer.GetHashCode(obj[obj.Count / 2]);
                        }
                    }
                }

                return code;
            }
        }
    }
}
