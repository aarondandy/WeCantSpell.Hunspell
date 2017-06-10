#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class BoolEx
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool InversePostfixIncrement(ref bool b)
        {
            if (b)
            {
                return false;
            }
            else
            {
                b = true;
                return true;
            }
        }
    }
}
