using System;
using System.Globalization;

namespace Hunspell.Utilities
{
    internal static class CultureInfoExtensions
    {
        public static bool IsHungarianLanguage(this CultureInfo culture)
        {
            return StringComparer.OrdinalIgnoreCase.Equals(culture?.TwoLetterISOLanguageName, "hu");
        }
    }
}
