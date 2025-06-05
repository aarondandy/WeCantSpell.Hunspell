using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class PhonetAuTests : IAsyncLifetime
{
    private WordList _wordList = null!;

    public async ValueTask InitializeAsync()
    {
        _wordList = await DictionaryLoader.GetDictionaryAsync("files/English (Australian).dic", TestContext.Current.CancellationToken);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask DisposeAsync() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    [Theory]
    [InlineData("behafiour", "behaviour")]
    [InlineData("katalogue", "catalogue")]
    [InlineData("color", "colour")]
    [InlineData("coluor", "colour")]
    [InlineData("didgeridu", "didgeridoo")]
    [InlineData("dincum", "dinkum")]
    [InlineData("duny", "dunny")]
    [InlineData("imu", "emu")]
    [InlineData("flafour", "flavour")]
    [InlineData("gangaroo", "kangaroo")]
    [InlineData("gnow", "now")]
    [InlineData("kangaru", "kangaroo")]
    [InlineData("know", "now")]
    [InlineData("Si'an", "Xi'an")]
    [InlineData("xinge", "singe")]
    [InlineData("yaka", "yakka")]
    public void can_suggest_based_on_phonetics(string given, string expected)
    {
        var actual = _wordList.Suggest(given, TestContext.Current.CancellationToken);

        actual.ShouldContain(expected);
    }

    [Fact]
    public void suggest_doesnt_crash_on_empty_phonetics()
    {
        // number is truncated to zero-length string by the phonetics algorithm
        var suggestions = _wordList.Suggest(
            "001",
            new QueryOptions() { MaxWords = 5 },
            TestContext.Current.CancellationToken);
        suggestions.ShouldBeEmpty();
    }
}
