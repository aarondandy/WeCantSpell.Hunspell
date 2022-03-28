using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
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
}
