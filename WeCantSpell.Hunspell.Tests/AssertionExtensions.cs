using System.Collections.Generic;
using System.Linq;

using Shouldly;

namespace WeCantSpell.Hunspell.Tests;

internal static class AssertionExtensions
{
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
