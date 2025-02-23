using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class OptionsTests : IAsyncLifetime
{
    private WordList _wordList = null!;

    public async ValueTask InitializeAsync()
    {
        _wordList = await DictionaryLoader.GetDictionaryAsync("files/English (American).dic", TestContext.Current.CancellationToken);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask DisposeAsync() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    [Fact]
    public void can_limit_word_suggestions()
    {
        var word = "teh";
        var options = new QueryOptions
        {
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(5),
            MaxSuggestions = 2
        };

        var verification = _wordList.Suggest(word, CancellationToken.None);
        verification.Count().ShouldBeGreaterThan(2);

        var actual = _wordList.Suggest(word, options, CancellationToken.None);

        actual.ShouldHaveCount(2);
    }

    [Fact(Skip = "I can't get this timing test to reliably run in a CI environment")]
    public void can_limit_slow_suggestions_with_cancellation_token()
    {
        var word = "lots-ofwords";
        var options = new QueryOptions
        {
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(5),
            MaxSuggestions = 100
        };

        var stopwatch = Stopwatch.StartNew();
        var verification = _wordList.Suggest(word, options, CancellationToken.None);
        stopwatch.Stop();
        var fullRunTime = stopwatch.Elapsed;
        verification.ShouldNotBeEmpty();

        var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(fullRunTime.TotalMilliseconds * 0.1));
        stopwatch = Stopwatch.StartNew();
        var actual = _wordList.Suggest(word, cts.Token);
        stopwatch.Stop();

        cts.Token.IsCancellationRequested.ShouldBeTrue();
        actual.Count().ShouldBeLessThanOrEqualTo(verification.Count());
        stopwatch.Elapsed.ShouldBeLessThan(fullRunTime);
    }
}
