using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class OutputConversionTests
{
    [Fact]
    public async Task can_transform_outputs_for_oconv2()
    {
        var dictionary = await DictionaryLoader.GetDictionaryAsync("files/oconv2.dic", TestContext.Current.CancellationToken);

        var actual = dictionary.CheckDetails("aas", TestContext.Current.CancellationToken);
        actual.Correct.ShouldBeTrue();
        actual.Root.ShouldBe("aa");
    }
}
