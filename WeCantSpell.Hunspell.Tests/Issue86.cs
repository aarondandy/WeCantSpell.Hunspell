using System;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class Issue86 : IAsyncLifetime
{
    private WordList _wordList = null!;

    public async ValueTask InitializeAsync()
    {
        _wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", TestContext.Current.CancellationToken);
    }

    public async ValueTask DisposeAsync() => await Task.Yield();

    [Theory]
    [InlineData("epooied", "epoxied")]
    [InlineData("ewelries", "jewelries")]
    [InlineData("suabbles", "squabbles")]
    public void suggest_correct_word(string query, string expected)
    {
        var suggestions = _wordList.Suggest(
            query,
            new QueryOptions()
            {
                TimeLimitSuggestStep = TimeSpan.FromSeconds(1),
                TimeLimitCompoundCheck = TimeSpan.FromSeconds(1),
                TimeLimitCompoundSuggest = TimeSpan.FromSeconds(1),
                TimeLimitSuggestGlobal = TimeSpan.FromSeconds(1),
            },
            TestContext.Current.CancellationToken);
        suggestions.ShouldContain(expected);
    }

    [Theory]
    [InlineData("poiseed")]
    [InlineData("jewelrys")]
    [InlineData("squabblees")]
    public void wrong_words_are_wrong(string given)
    {
        _wordList.Check(given, TestContext.Current.CancellationToken).ShouldBeFalse();
    }

    [Theory]
    [InlineData("epoxied")]
    [InlineData("jewelries")]
    [InlineData("squabbles")]
    public void correct_words_are_correct(string given)
    {
        _wordList.Check(given, TestContext.Current.CancellationToken).ShouldBeTrue();
    }
}
