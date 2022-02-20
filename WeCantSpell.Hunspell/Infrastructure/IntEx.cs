using System;
using System.Globalization;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure;

static class IntEx
{
    private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

    public static bool TryParseInvariant(string text, out int value) =>
        int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);

    public static bool TryParseInvariant(ReadOnlySpan<char> text, out int value)
    {
        text = text.Trim();
        if (text.IsEmpty)
        {
            value = default;
            return false;
        }

        var isNegative = false;
        if (text[0] == '-')
        {
            isNegative = true;
            text = text.Slice(1);
        }

        if (text.IsEmpty)
        {
            value = default;
            return false;
        }

        if (!TryParseInvariant(text[text.Length - 1], out value))
        {
            return false;
        }

        for (int i = text.Length - 2, multiplier = 10; i >= 0; i--, multiplier *= 10)
        {
            if (!TryParseInvariant(text[i], out int digit))
            {
                return false;
            }

            value += (multiplier * digit);
        }

        if (isNegative)
        {
            value = -value;
        }

        return true;
    }

    public static int? TryParseInvariant(ReadOnlySpan<char> text) =>
        TryParseInvariant(text, out int value) ? value : default(int?);

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public static bool InversePostfixIncrement(ref bool b)
    {
        if (b)
        {
            return false;
        }
        else
        {
            b = true;
            return true;
        }
    }

    private static bool TryParseInvariant(char character, out int value)
    {
        if (character >= '0' && character <= '9')
        {
            value = character - '0';
            return true;
        }

        value = default;
        return false;
    }
}
