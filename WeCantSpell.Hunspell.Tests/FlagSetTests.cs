using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class FlagSetTests
{
    [Fact]
    public void can_union_flag_to_end()
    {
        var set = FlagSet.Create((FlagValue)'a');

        var actual = set.Union((FlagValue)'z');

        actual.Should().Equal(new FlagValue[] { (FlagValue)'a', (FlagValue)'z' });
    }

    [Fact]
    public void can_union_flag_to_beginning()
    {
        var set = FlagSet.Create((FlagValue)'z');

        var actual = set.Union((FlagValue)'a');

        actual.Should().Equal(new FlagValue[] { (FlagValue)'a', (FlagValue)'z' });
    }

    [Fact]
    public void can_union_flag_to_middle()
    {
        var set = FlagSet.Create(new[] { (FlagValue)'a', (FlagValue)'z' });

        var actual = set.Union((FlagValue)'x');

        actual.Should().Equal(new FlagValue[] { (FlagValue)'a', (FlagValue)'x', (FlagValue)'z' });
    }
}
