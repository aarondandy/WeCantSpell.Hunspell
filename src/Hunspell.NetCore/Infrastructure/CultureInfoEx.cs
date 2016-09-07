using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class CultureInfoEx
    {
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsHungarianLanguage(this CultureInfo culture)
        {
            return string.Equals(culture?.TwoLetterISOLanguageName, "hu", StringComparison.OrdinalIgnoreCase);
        }
    }
}
