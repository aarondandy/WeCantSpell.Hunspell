using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
    public static int CountMatchesFromLeft<T>(this ReadOnlySpan<T> a, ReadOnlySpan<T> b) where T : notnull, IEquatable<T>
    {
        var minLength = Math.Min(a.Length, b.Length);
        var count = 0;
        for (; count < minLength && a[count].Equals(b[count]); count++) ;
        return count;
    }

    public static int CountMatchesFromRight<T>(this ReadOnlySpan<T> a, ReadOnlySpan<T> b) where T : notnull, IEquatable<T>
    {
        var minLength = Math.Min(a.Length, b.Length);
        var count = 0;
        for (; count < minLength && a[a.Length - 1 - count].Equals(b[b.Length - 1 - count]); count++) ;
        return count;
    }

    public static int IndexOf<T>(this ReadOnlySpan<T> @this, T value, int startIndex) where T:IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static int IndexOf<T>(this ReadOnlySpan<T> @this, ReadOnlySpan<T> value, int startIndex) where T : IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static ReadOnlySpan<T> Limit<T>(this ReadOnlySpan<T> @this, int maxLength)
    {
#if DEBUG
        if (maxLength < 0) throw new ArgumentOutOfRangeException(nameof(maxLength));
#endif
        return @this.Length > maxLength ? @this.Slice(0, maxLength) : @this;
    }

    public static void Swap<T>(this Span<T> span, int a, int b)
    {
        (span[b], span[a]) = (span[a], span[b]);
    }
}
