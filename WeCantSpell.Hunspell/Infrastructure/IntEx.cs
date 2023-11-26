using System;
using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure;

static class IntEx
{
    private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

    public static bool TryParseInvariant(string text, out int value) =>
        int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);

    public static bool TryParseInvariant(ReadOnlySpan<char> text, out int value)
    {
#if HAS_SPANCULTUREPARSE_INT
        return int.TryParse(text.Trim(), NumberStyles.Integer, InvariantNumberFormat, out value);
#else
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

        if (!tryParseInvariant(text[text.Length - 1], out value))
        {
            return false;
        }

        for (int i = text.Length - 2, multiplier = 10; i >= 0; i--, multiplier *= 10)
        {
            if (!tryParseInvariant(text[i], out var digit))
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

        static bool tryParseInvariant(char character, out int value)
        {
            if (character is >= '0' and <= '9')
            {
                value = character - '0';
                return true;
            }

            value = default;
            return false;
        }
#endif
    }

    public static int? TryParseInvariant(ReadOnlySpan<char> text) => TryParseInvariant(text, out var value) ? value : null;
}
