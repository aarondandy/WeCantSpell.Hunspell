using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class OutputConversionTests
{
    [Fact]
    public void can_transform_outputs_for_oconv2()
    {
        var filePath = @"files/oconv2.dic";
        var dictionary = WordList.CreateFromFiles(filePath);

        var actual = dictionary.CheckDetails("aas");
        actual.Correct.ShouldBeTrue();
        actual.Root.ShouldBe("aa");
    }
}
