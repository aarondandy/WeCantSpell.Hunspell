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

        var verification = wordList.Suggest(word, null);
        verification.Should().HaveCountGreaterThan(2);

        var actual = wordList.Suggest(word, options);

        actual.Should().HaveCount(2);
    }

    [Fact]
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
        verification.Should().NotBeEmpty();
        stopwatch.Stop();
        var fullRunTime = stopwatch.Elapsed;

        var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(fullRunTime.TotalMilliseconds * 0.25));
        options.CancellationToken = cts.Token;
        stopwatch = Stopwatch.StartNew();
        var actual = wordList.Suggest(word, options);
        stopwatch.Stop();

        actual.Should().HaveCountLessThanOrEqualTo(verification.Count());
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromMilliseconds(fullRunTime.TotalMilliseconds * 0.75));
    }

    protected Task<WordList> LoadEnUsAsync() =>
        WordList.CreateFromFilesAsync("files/English (American).dic");
}
