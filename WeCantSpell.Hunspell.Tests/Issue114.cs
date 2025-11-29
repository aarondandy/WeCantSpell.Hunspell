using System;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class Issue114 : IAsyncLifetime
{
    private WordList _wordList = null!;

    public async ValueTask InitializeAsync()
    {
        _wordList = await DictionaryLoader.GetDictionaryAsync("files/English (American).dic", TestContext.Current.CancellationToken);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask DisposeAsync() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    [Theory]
    [InlineData("Teh", "Te|Th|Eh|Tet|Ted|Meh|T eh|Te h|Te-h|The|Tech|Tee|Tea|Ten|Tel")]
    [InlineData("quik", "quirk|quick|quin|quit|quid|quip|quiz")]
    [InlineData("MxDif", "Modify")]
    public void GetSuggestions(string given, string expectedString)
    {
        var actual = _wordList.Suggest(
            given,
            new QueryOptions()
            {
                TimeLimitSuggestStep = TimeSpan.FromSeconds(1),
                TimeLimitCompoundCheck = TimeSpan.FromSeconds(1),
                TimeLimitCompoundSuggest = TimeSpan.FromSeconds(1),
                TimeLimitSuggestGlobal = TimeSpan.FromSeconds(1),
            },
            TestContext.Current.CancellationToken);

        var expected = expectedString.Split('|');
        actual.ShouldBe(expected, comparer: StringComparer.Ordinal, ignoreOrder: true);
    }
}
