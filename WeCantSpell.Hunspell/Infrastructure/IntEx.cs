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
            if (!TryParseInvariant(text[i], out var digit))
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

    public static int? TryParseInvariant(ReadOnlySpan<char> text) => TryParseInvariant(text, out var value) ? value : null;

    /// <remarks>
    /// This is a strange implementation of a postfix increment to remove the use of an
    /// int in these cases.
    /// </remarks>
    public static bool InversePostfixIncrement(ref bool b)
    {
        if (b)
        {
            return false;
        }

        b = true;
        return true;
    }

    private static bool TryParseInvariant(char character, out int value)
    {
        if (character is >= '0' and <= '9')
        {
            value = character - '0';
            return true;
        }

        value = default;
        return false;
    }
}
