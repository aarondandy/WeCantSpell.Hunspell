using System.Globalization;
using System.Runtime.CompilerServices;

namespace Hunspell.Infrastructure
{
    internal static class IntEx
    {
        private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseInvariant(string text, out int value)
        {
            return int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int? TryParseInvariant(string text)
        {
            int value;
            return TryParseInvariant(text, out value) ? (int?)value : null;
        }
    }
}
