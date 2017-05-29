using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class Swapper
    {
#if !NO_METHODIMPL && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
