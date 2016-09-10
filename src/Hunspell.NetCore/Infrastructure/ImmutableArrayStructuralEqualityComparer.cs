using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell.Infrastructure
{
    internal class ImmutableArrayStructuralEqualityComparer<T> : IEqualityComparer<ImmutableArray<T>>
        where T : IEquatable<T>
    {
        public ImmutableArrayStructuralEqualityComparer()
            : this(EqualityComparer<T>.Default)
        {
        }

        public ImmutableArrayStructuralEqualityComparer(IEqualityComparer<T> valueEqualityComparer)
        {
            ValueEqualityComparer = valueEqualityComparer;
        }

        public IEqualityComparer<T> ValueEqualityComparer { get; private set; }

        public bool Equals(ImmutableArray<T> x, ImmutableArray<T> y)
        {
            if (x.IsDefault)
            {
                return y.IsDefault;
            }
            if (y.IsDefault)
            {
                return false;
            }

            if (x.Length != y.Length)
            {
                return false;
            }

            for (var i = 0; i < x.Length; i++)
            {
                if (!ValueEqualityComparer.Equals(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(ImmutableArray<T> obj)
        {
            if (obj.IsDefaultOrEmpty)
            {
                return 0;
            }

            int hashCode = obj.Length;
            var maxSearchLength = Math.Min(hashCode, 5);
            for (var i = 0; i < maxSearchLength; i++)
            {
                unchecked
                {
                    hashCode ^= ValueEqualityComparer.GetHashCode(obj[i]);
                }
            }

            return hashCode;
        }
    }
}
