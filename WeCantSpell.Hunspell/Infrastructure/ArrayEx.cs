using System;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class ArrayEx
{
    public static bool Contains<T>(this T[] values, T value) => Array.IndexOf(values, value) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this T[] array) => array.Length != 0;
}
