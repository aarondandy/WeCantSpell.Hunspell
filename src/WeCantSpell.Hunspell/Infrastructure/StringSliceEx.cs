using System;
using System.Collections.Generic;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class StringSliceEx
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringSlice Subslice(this string text, int startIndex) =>
            new StringSlice(text, startIndex, text.Length - startIndex);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringSlice Subslice(this string text, int startIndex, int length) =>
            new StringSlice(text, startIndex, length);

        public static List<StringSlice> SliceOnTabOrSpace(this StringSlice @this)
        {
            var parts = new List<StringSlice>();

            int startIndex = 0;
            int splitIndex;
            int partLength;
            while ((splitIndex = @this.IndexOfSpaceOrTab(startIndex)) >= 0)
            {
                partLength = splitIndex - startIndex;
                if (partLength > 0)
                {
                    parts.Add(@this.Subslice(startIndex, partLength));
                }

                startIndex = splitIndex + 1;
            }

            partLength = @this.Length - startIndex;
            if (partLength > 0)
            {
                parts.Add(@this.Subslice(startIndex, partLength));
            }

            return parts;
        }

        public static List<StringSlice> SplitOnComma(this StringSlice @this)
        {
            var parts = new List<StringSlice>();

            var finalIndex = @this.Offset + @this.Length;
            var startIndex = @this.Offset;
            int commaIndex;
            int partLength;
            while ((commaIndex = @this.Text.IndexOf(',', startIndex, finalIndex - startIndex)) >= 0)
            {
                partLength = commaIndex - startIndex;
                if (partLength > 0)
                {
                    parts.Add(new StringSlice(@this.Text, startIndex, partLength));
                }

                startIndex = commaIndex + 1;
            }

            commaIndex = finalIndex;
            partLength = commaIndex - startIndex;
            if (partLength > 0)
            {
                parts.Add(new StringSlice(@this.Text, startIndex, partLength));
            }

            return parts;
        }
    }
}
