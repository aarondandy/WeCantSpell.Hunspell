using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class BoolEx
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool PostfixIncrement(ref bool b)
        {
            if (b)
            {
                return true;
            }
            else
            {
                b = true;
                return false;
            }
        }
    }
}
