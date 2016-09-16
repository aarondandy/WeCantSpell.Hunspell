using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class BoolEx
    {
#if !PRE_NETSTANDARD && !DEBUG
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
