#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure;

static class ReferenceHelpers
{
#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static void Swap<T>(ref T a, ref T b)
    {
        var tmp = a;
        a = b;
        b = tmp;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static T Steal<T>(ref T item) where T : class
    {
        var value = item;
        item = null;
        return value;
    }
}
