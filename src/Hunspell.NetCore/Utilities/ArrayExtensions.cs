using System;
using System.Collections.Generic;

namespace Hunspell.Utilities
{
    internal static class ArrayExtensions
    {
        public static bool StartsWith<T>(this T[] @this, T[] subset)
            where T : IEquatable<T>
        {
            if (subset.Length > @this.Length)
            {
                return false;
            }

            for (int i = 0; i < subset.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(@this[i], subset[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
