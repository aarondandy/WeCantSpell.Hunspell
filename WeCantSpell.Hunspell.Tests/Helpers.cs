using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeCantSpell.Hunspell.Tests;

static class Helpers
{
    static Helpers()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void EnsureEncodingsReady()
    {
    }

#if NO_CANCELLABLE_READLINE
    public static Task<string> ReadLineAsync(this StreamReader reader, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return reader.ReadLineAsync();
    }
#endif

}
