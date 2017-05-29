using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell.Tests.Utilities
{
    internal static class EncodingHelpers
    {
        static EncodingHelpers()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void EnsureEncodingsReady()
        {
        }
    }
}
