using System;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class ArrayEx
{

#if NO_SPAN_CONTAINS

    public static bool Contains<T>(this T[] values, T value) where T : IEquatable<T> => Array.IndexOf(values, value) >= 0;

#else

    public static bool Contains<T>(this T[] values, T value) where T : IEquatable<T> => values.AsSpan().Contains(value)!;

#endif

}
