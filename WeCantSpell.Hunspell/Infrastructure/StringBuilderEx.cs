using System;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure;

static class StringBuilderEx
{
    public static bool EndsWith(this StringBuilder builder, char c) => builder.Length != 0 && builder[builder.Length - 1] == c;

#if NO_SB_SPANS
    public static StringBuilder Append(this StringBuilder builder, ReadOnlySpan<char> value)
    {
        if (!value.IsEmpty)
        {
            unsafe
            {
                fixed (char* start = &System.Runtime.InteropServices.MemoryMarshal.GetReference(value))
                {
                    builder.Append(start, value.Length);
                }
            }
        }

        return builder;
    }
#endif

}
