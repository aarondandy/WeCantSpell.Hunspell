using System;

namespace Hunspell.Utilities
{
    internal static class ArrayExtensions
    {
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static sbyte[] ToCString(this sbyte[] @this)
        {
            var result = new sbyte[@this.Length + 1];

            Buffer.BlockCopy(@this, 0, result, 0, @this.Length);
            result[result.Length - 1] = 0;

            return result;
        }
    }
}
