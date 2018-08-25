using System;

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class ReadOnlySpanEx
    {
        public static int IndexOf(this ReadOnlySpan<char> @this, char value, int startIndex)
        {
            var result = @this.Slice(startIndex).IndexOf(value);
            if (result >= 0)
            {
                result += startIndex;
            }

            return result;
        }

        public static bool Contains(this ReadOnlySpan<char> @this, char value) =>
            @this.IndexOf(value) >= 0;
    }
}
