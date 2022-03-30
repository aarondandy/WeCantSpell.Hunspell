using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure;

static class StringBuilderEx
{
    public static void Swap(this StringBuilder @this, int indexA, int indexB)
    {
#if DEBUG
        if (indexA < 0 || indexA > @this.Length) throw new ArgumentOutOfRangeException(nameof(indexA));
        if (indexB < 0 || indexB > @this.Length) throw new ArgumentOutOfRangeException(nameof(indexB));
#endif

        (@this[indexB], @this[indexA]) = (@this[indexA], @this[indexB]);
    }

    public static void RemoveChars(this StringBuilder @this, CharacterSet chars)
    {
        var nextWriteLocation = 0;
        for (var searchLocation = 0; searchLocation < @this.Length; searchLocation++)
        {
            var c = @this[searchLocation];
            if (!chars.Contains(c))
            {
                @this[nextWriteLocation] = c;
                nextWriteLocation++;
            }
        }

        @this.Remove(nextWriteLocation, @this.Length - nextWriteLocation);
    }

    public static void Reverse(this StringBuilder @this)
    {
        if (@this is { Length: > 1 })
        {
            var swapOtherIndexOffset = @this.Length - 1;
            var stopIndex = @this.Length / 2;
            for (var i = 0; i < stopIndex; i++)
            {
                @this.Swap(i, swapOtherIndexOffset - i);
            }
        }
    }

    public static bool EndsWith(this StringBuilder builder, char c) => builder.Length > 0 && builder[builder.Length - 1] == c;

#if NO_SB_SPANS
    public static StringBuilder Append(this StringBuilder builder, ReadOnlySpan<char> value)
    {
#if DEBUG
        if (builder is null) throw new ArgumentNullException(nameof(builder));
#endif

        if (!value.IsEmpty)
        {
            unsafe
            {
                fixed (char* start = &MemoryMarshal.GetReference(value))
                {
                    builder.Append(start, value.Length);
                }
            }
        }

        return builder;
    }
#endif

}
