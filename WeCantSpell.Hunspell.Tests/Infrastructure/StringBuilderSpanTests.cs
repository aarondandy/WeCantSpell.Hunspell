using FluentAssertions;

using WeCantSpell.Hunspell.Infrastructure;

using Xunit;

namespace WeCantSpell.Hunspell.Tests.Infrastructure;
public class StringBuilderSpanTests
{
    [Fact]
    public void can_replace_string_equal_size()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "cc");
        builder.ToString().Should().Be("cccccc");
    }

    [Fact]
    public void can_replace_string_smaller()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "c");
        builder.ToString().Should().Be("ccc");
    }

    [Fact]
    public void can_replace_string_larger()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "ccc");
        builder.ToString().Should().Be("ccccccccc");
    }

    [Fact]
    public void can_replace_string_equal_size_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "cc", 2, 2);
        builder.ToString().Should().Be("abccab");
    }

    [Fact]
    public void can_replace_string_smaller_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "c", 2, 2);
        builder.ToString().Should().Be("abcab");
    }

    [Fact]
    public void can_replace_string_larger_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "ccc", 2, 2);
        builder.ToString().Should().Be("abcccab");
    }

    [Fact]
    public void can_replace_string_many_equal_size_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "cc", 2, 6);
        builder.ToString().Should().Be("abccccccab");
    }

    [Fact]
    public void can_replace_string_many_smaller_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "c", 2, 6);
        builder.ToString().Should().Be("abcccab");
    }

    [Fact]
    public void can_replace_string_many_larger_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "ccc", 2, 6);
        builder.ToString().Should().Be("abcccccccccab");
    }

    [Fact]
    public void can_replace_string_many_equal_size_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "cc", 2, 10);
        builder.ToString().Should().Be("abxccxccxccxab");
    }

    [Fact]
    public void can_replace_string_many_smaller_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "c", 2, 10);
        builder.ToString().Should().Be("abxcxcxcxab");
    }

    [Fact]
    public void can_replace_string_many_larger_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "ccc", 2, 10);
        builder.ToString().Should().Be("abxcccxcccxcccxab");
    }
}
