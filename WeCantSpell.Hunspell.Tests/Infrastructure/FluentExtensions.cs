using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Collections;

namespace WeCantSpell.Hunspell.Tests.Infrastructure;

static class FluentExtensions
{
    public static AndConstraint<GenericCollectionAssertions<FlagValue>> ContainInOrder(
        this GenericCollectionAssertions<IEnumerable<FlagValue>, FlagValue, GenericCollectionAssertions<FlagValue>> @this,
        IEnumerable<char> expected)
    {
        return @this.ContainInOrder(expected.Select(static value => (FlagValue)value));
    }

    public static AndConstraint<GenericCollectionAssertions<FlagValue>> ContainInOrder(
        this GenericCollectionAssertions<IEnumerable<FlagValue>, FlagValue, GenericCollectionAssertions<FlagValue>> @this,
        IEnumerable<int> expected)
    {
        return @this.ContainInOrder(expected.Select(static value => (FlagValue)value));
    }
}
