using System.Globalization;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell
{
    internal static class Utilities
    {
        public static void ApplyCultureHacks()
        {
            // BUG: https://github.com/petabridge/NBench/issues/213
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}
