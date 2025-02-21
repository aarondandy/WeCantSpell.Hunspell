using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

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
        verification.Count().ShouldBeGreaterThan(2);

        var actual = wordList.Suggest(word, options);

        actual.Count().ShouldBe(2);
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
        verification.ShouldNotBeEmpty();

        var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(fullRunTime.TotalMilliseconds * 0.1));
        stopwatch = Stopwatch.StartNew();
        var actual = wordList.Suggest(word, cts.Token);
        stopwatch.Stop();

        cts.Token.IsCancellationRequested.ShouldBeTrue();
        actual.Count().ShouldBeLessThanOrEqualTo(verification.Count());
        stopwatch.Elapsed.ShouldBeLessThan(fullRunTime);
    }

    protected static Task<WordList> LoadEnUsAsync() => WordList.CreateFromFilesAsync("files/English (American).dic");
}
