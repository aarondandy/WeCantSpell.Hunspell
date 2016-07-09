using System.Globalization;

namespace Hunspell.Utilities
{
    internal static class IntExtensions
    {
        public static bool TryParseInvariant(string text, out int value)
        {
            return int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out value);
        }
    }
}
