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
    }
}
