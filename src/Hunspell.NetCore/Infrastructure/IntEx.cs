using System.Globalization;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class IntEx
    {
        private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

        public static bool TryParseInvariant(string text, out int value) =>
            int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);

        public static bool TryParseInvariant(string text, int startIndex, int length, out int value) =>
            int.TryParse(text.Substring(startIndex, length), NumberStyles.Integer, InvariantNumberFormat, out value);

        public static int? TryParseInvariant(string text)
        {
            int value;
            return TryParseInvariant(text, out value) ? (int?)value : null;
        }
    }
}
