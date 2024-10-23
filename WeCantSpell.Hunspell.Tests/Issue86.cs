using System;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class Issue86  : IAsyncLifetime
{
    private WordList _wordList = null!;

    public async Task InitializeAsync()
    {
        _wordList = await WordList.CreateFromFilesAsync("files/English (American).dic");
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Theory]
    [InlineData("epooied", "epoxied")]
    [InlineData("ewelries", "jewelries")]
    [InlineData("suabbles", "squabbles")]
    public void suggest_correct_word(string query, string expected)
    {
        var suggestions = _wordList.Suggest(query, new QueryOptions()
        {
            TimeLimitSuggestStep = TimeSpan.FromSeconds(1),
            TimeLimitCompoundCheck = TimeSpan.FromSeconds(1),
            TimeLimitCompoundSuggest = TimeSpan.FromSeconds(1),
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(1),
        });
        suggestions.Should().Contain(expected);
    }

    [Theory]
    [InlineData("poiseed")]
    [InlineData("jewelrys")]
    [InlineData("squabblees")]
    public void wrong_words_are_wrong(string given)
    {
        _wordList.Check(given).Should().BeFalse();
    }

    [Theory]
    [InlineData("epoxied")]
    [InlineData("jewelries")]
    [InlineData("squabbles")]
    public void correct_words_are_correct(string given)
    {
        _wordList.Check(given).Should().BeTrue();
    }
}
