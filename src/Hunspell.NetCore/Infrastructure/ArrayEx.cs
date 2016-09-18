using System;

namespace Hunspell.Infrastructure
{
    internal static class ArrayEx<T>
    {
#if PRE_NETSTANDARD || NET_4_5_1
        public static readonly T[] Empty = new T[0];
#else
        public static readonly T[] Empty = Array.Empty<T>();
#endif
    }
}
