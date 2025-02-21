using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class StringBuilderSpanTests
{
    [Fact]
    public void can_replace_string_equal_size()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "cc");
        builder.ToString().ShouldBe("cccccc");
    }

    [Fact]
    public void can_replace_string_smaller()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "c");
        builder.ToString().ShouldBe("ccc");
    }

    [Fact]
    public void can_replace_string_larger()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "ccc");
        builder.ToString().ShouldBe("ccccccccc");
    }

    [Fact]
    public void can_replace_string_equal_size_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "cc", 2, 2);
        builder.ToString().ShouldBe("abccab");
    }

    [Fact]
    public void can_replace_string_smaller_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "c", 2, 2);
        builder.ToString().ShouldBe("abcab");
    }

    [Fact]
    public void can_replace_string_larger_constrained()
    {
        var builder = new StringBuilderSpan("ababab");
        builder.Replace("ab", "ccc", 2, 2);
        builder.ToString().ShouldBe("abcccab");
    }

    [Fact]
    public void can_replace_string_many_equal_size_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "cc", 2, 6);
        builder.ToString().ShouldBe("abccccccab");
    }

    [Fact]
    public void can_replace_string_many_smaller_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "c", 2, 6);
        builder.ToString().ShouldBe("abcccab");
    }

    [Fact]
    public void can_replace_string_many_larger_constrained()
    {
        var builder = new StringBuilderSpan("ababababab");
        builder.Replace("ab", "ccc", 2, 6);
        builder.ToString().ShouldBe("abcccccccccab");
    }

    [Fact]
    public void can_replace_string_many_equal_size_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "cc", 2, 10);
        builder.ToString().ShouldBe("abxccxccxccxab");
    }

    [Fact]
    public void can_replace_string_many_smaller_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "c", 2, 10);
        builder.ToString().ShouldBe("abxcxcxcxab");
    }

    [Fact]
    public void can_replace_string_many_larger_constrained_with_junk()
    {
        var builder = new StringBuilderSpan("abxabxabxabxab");
        builder.Replace("ab", "ccc", 2, 10);
        builder.ToString().ShouldBe("abxcccxcccxcccxab");
    }
}
