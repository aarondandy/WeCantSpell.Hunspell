using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class IntEx
    {
        private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

        public static bool TryParseInvariant(string text, out int value) =>
            int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);

        public static bool TryParseInvariant(string text, int startIndex, int length, out int value) =>
            int.TryParse(text.Substring(startIndex, length), NumberStyles.Integer, InvariantNumberFormat, out value);

        internal static bool TryParseInvariant(StringSlice text, out int value) =>
            int.TryParse(text.ToString(), NumberStyles.Integer, InvariantNumberFormat, out value);

        public static int? TryParseInvariant(string text) =>
            TryParseInvariant(text, out int value) ? (int?)value : null;
    }
}
