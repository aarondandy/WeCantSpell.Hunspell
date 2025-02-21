using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell.Tests;

static class EncodingHelpers
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
