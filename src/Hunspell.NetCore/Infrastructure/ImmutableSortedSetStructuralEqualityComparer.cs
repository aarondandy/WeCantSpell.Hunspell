using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell.Infrastructure
{
    internal sealed class ImmutableSortedSetStructuralEqualityComparer<T> : IEqualityComparer<ImmutableSortedSet<T>>
        where T : IEquatable<T>
    {
        public static ImmutableSortedSetStructuralEqualityComparer<T> Default = new ImmutableSortedSetStructuralEqualityComparer<T>();

        public ImmutableSortedSetStructuralEqualityComparer()
            : this(EqualityComparer<T>.Default)
        {
        }

        public ImmutableSortedSetStructuralEqualityComparer(IEqualityComparer<T> valueEqualityComparer)
        {
            ValueEqualityComparer = valueEqualityComparer;
        }

        public IEqualityComparer<T> ValueEqualityComparer { get; private set; }

        public bool Equals(ImmutableSortedSet<T> x, ImmutableSortedSet<T> y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return false;
            }

            if (x.Count != y.Count)
            {
                return false;
            }

            for (var i = 0; i < x.Count; i++)
            {
                if (!ValueEqualityComparer.Equals(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(ImmutableSortedSet<T> obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int hashCode = obj.Count;
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
