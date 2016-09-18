using System;

namespace Hunspell.Infrastructure
{
    internal static class ArrayEx
    {
        public static TOutput[] ConvertAll<TInput, TOutput>(
            TInput[] array,
#if NET_FULL
            Converter<TInput, TOutput> converter
#else
            Func<TInput, TOutput> converter
#endif
        )
        {
#if NET_FULL
            return Array.ConvertAll(array, converter);
#else
            var result = new TOutput[array.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = converter(array[i]);
            }
            return result;
#endif
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
