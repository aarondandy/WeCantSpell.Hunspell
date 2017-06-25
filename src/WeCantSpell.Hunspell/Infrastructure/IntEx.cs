using System.Globalization;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class IntEx
    {
        private static readonly NumberFormatInfo InvariantNumberFormat = CultureInfo.InvariantCulture.NumberFormat;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseInvariant(string text, out int value) =>
            int.TryParse(text, NumberStyles.Integer, InvariantNumberFormat, out value);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseInvariant(StringSlice text, out int value) =>
            int.TryParse(text.ToString(), NumberStyles.Integer, InvariantNumberFormat, out value);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int? TryParseInvariant(StringSlice text) =>
            TryParseInvariant(text, out int value) ? (int?)value : null;
    }
}
