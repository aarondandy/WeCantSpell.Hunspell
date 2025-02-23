using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

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

    public static void ShouldBeValue(this FlagValue actual, char expected)
    {
        actual.AssertAwesomely(v => v.Equals(expected), actual, expected, customMessage: null);
    }

    public static void ShouldBeValue(this FlagValue actual, int expected)
    {
        actual.AssertAwesomely(v => v.Equals(expected), actual, expected, customMessage: null);
    }

    public static void ShouldBeValues(this IEnumerable<FlagValue> actual, IEnumerable<char> expected, bool ignoreOrder = false)
    {
        actual.ShouldBe(expected.Select(static v => (FlagValue)v), ignoreOrder: ignoreOrder);
    }

    public static void ShouldBeValues(this IEnumerable<FlagValue> actual, IEnumerable<int> expected, bool ignoreOrder = false)
    {
        actual.ShouldBe(expected.Select(static v => (FlagValue)v), ignoreOrder: ignoreOrder);
    }

    public static void ShouldBeValues(this IEnumerable<char> actual, IEnumerable<int> expected, bool ignoreOrder = false)
    {
        actual.ShouldBe(expected.Select(static v => (char)v), ignoreOrder: ignoreOrder);
    }

    public static void ShouldHaveCount<T>(this IEnumerable<T> actual, int expected)
    {
        if (actual is ICollection<T> list)
        {
            list.Count.ShouldBe(expected);
        }
        else
        {
            actual.Count().ShouldBe(expected);
        }
    }
}
