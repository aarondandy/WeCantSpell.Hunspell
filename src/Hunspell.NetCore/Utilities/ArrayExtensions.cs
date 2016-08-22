using System;

namespace Hunspell.Utilities
{
    internal static class ArrayExtensions
    {
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] input, Func<TInput, TOutput> converter)
        {
#if NETCORE
            var output = new TOutput[input.Length];
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = converter(input[i]);
            }

            return output;
#else
            return Array.ConvertAll(input, value => converter(value));
#endif
        }

        public static bool Equals<T>(T[] a, int aOffset, T[] b, int bOffset, int length)
            where T : struct, IEquatable<T>
        {
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
}
