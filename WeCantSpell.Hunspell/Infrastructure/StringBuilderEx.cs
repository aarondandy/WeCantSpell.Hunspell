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

    public static string ToStringTerminated(this StringBuilder @this)
    {
        var terminatedIndex = @this.IndexOfNullChar();
        return terminatedIndex >= 0
            ? @this.ToString(0, terminatedIndex)
            : @this.ToString();
    }

    public static string ToStringTerminated(this StringBuilder @this, int startIndex)
    {
        var terminatedIndex = @this.IndexOfNullChar(startIndex);
        if (terminatedIndex < 0)
        {
            terminatedIndex = @this.Length;
        }

        return @this.ToString(startIndex, terminatedIndex - startIndex);
    }

    public static int IndexOfNullChar(this StringBuilder @this) => IndexOfNullChar(@this, 0);

    public static int IndexOfNullChar(this StringBuilder @this, int offset)
    {
        for (; offset < @this.Length; offset++)
        {
            if (@this[offset] == '\0')
            {
                return offset;
            }
        }

        return -1;
    }

    public static char GetCharOrTerminator(this StringBuilder @this, int index) => index < @this.Length ? @this[index] : '\0';

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

    public static bool StartsWith(this StringBuilder builder, char c) => builder.Length > 0 && builder[0] == c;

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
