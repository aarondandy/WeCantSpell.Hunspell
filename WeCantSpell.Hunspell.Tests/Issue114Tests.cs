using System;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class Issue114Tests : IAsyncLifetime
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
    [InlineData("Teh", "The")]
    [InlineData("Teh", "Tech")]
    [InlineData("quik", "quick")]
    [InlineData("MxDif", "Modify")]
    public void GetSuggestions(string given, string expected)
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
        actual.ShouldContain(expected);
    }
}
