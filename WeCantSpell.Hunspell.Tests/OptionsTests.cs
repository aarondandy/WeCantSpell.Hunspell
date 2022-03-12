using System;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class OptionsTests
{
    [Fact]
    public async Task can_limit_word_suggestions()
    {
        var word = "teh";
        var wordList = await LoadEnUsAsync();
        var options = new QueryOptions
        {
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(5),
            MaxSuggestions = 2
        };

        var verification = wordList.Suggest(word, null);
        verification.Should().HaveCountGreaterThan(2);

        var actual = wordList.Suggest(word, options);

        actual.Should().HaveCount(2);
    }

    protected Task<WordList> LoadEnUsAsync() =>
        WordList.CreateFromFilesAsync("files/English (American).dic");
}
