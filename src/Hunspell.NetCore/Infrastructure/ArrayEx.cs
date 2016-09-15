using System;

namespace Hunspell.Infrastructure
{
    internal static class ArrayEx
    {
        public static bool Equals<T>(T[] a, int aOffset, T[] b, int bOffset, int length)
            where T : struct, IEquatable<T>
        {
#if DEBUG
            if (aOffset < 0 || aOffset >= a.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(aOffset));
            }
            if (bOffset < 0 || bOffset >= b.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(bOffset));
            }
            if (aOffset + length > a.Length || bOffset + length > b.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
#endif

            for (var i = 0; i < length; i++)
            {
                if (!a[aOffset + i].Equals(b[bOffset + i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    internal static class ArrayEx<T>
    {

#if PRE_NETSTANDARD || NET_4_5_1
        public static readonly T[] Empty = new T[0];
#else
        public static readonly T[] Empty = Array.Empty<T>();
#endif

    }
}
