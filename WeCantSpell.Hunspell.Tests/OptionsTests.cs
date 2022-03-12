using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

        var verification = wordList.Suggest(word);
        verification.Should().HaveCountGreaterThan(2);

        var actual = wordList.Suggest(word, options);

        actual.Should().HaveCount(2);
    }

    [Fact(Skip = "I can't get this timing test to reliably run in a CI environment")]
    public async Task can_limit_slow_suggestions_with_cancellation_token()
    {
        var word = "lots-ofwords";
        var wordList = await LoadEnUsAsync();
        var options = new QueryOptions
        {
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(5),
            MaxSuggestions = 100
        };

        var stopwatch = Stopwatch.StartNew();
        var verification = wordList.Suggest(word, options);
        stopwatch.Stop();
        var fullRunTime = stopwatch.Elapsed;
        verification.Should().NotBeEmpty();

        var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(fullRunTime.TotalMilliseconds * 0.1));
        options.CancellationToken = cts.Token;
        stopwatch = Stopwatch.StartNew();
        var actual = wordList.Suggest(word, options);
        stopwatch.Stop();

        options.CancellationToken.IsCancellationRequested.Should().BeTrue();
        actual.Should().HaveCountLessThanOrEqualTo(verification.Count());
        stopwatch.Elapsed.Should().BeLessThan(fullRunTime);
    }

    protected Task<WordList> LoadEnUsAsync() =>
        WordList.CreateFromFilesAsync("files/English (American).dic");
}
