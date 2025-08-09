using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class EnUsWordsTests : IAsyncLifetime
{
    private WordList _spell = null!;
    private ImmutableArray<CommonSpellingMistake> _words = ImmutableArray<CommonSpellingMistake>.Empty;

    public async ValueTask InitializeAsync()
    {
        var ct = TestContext.Current.CancellationToken;

        var spellTask = DictionaryLoader.GetDictionaryAsync("files/English (American).dic", ct);

        // NOTE: not all of these words may be wrong with respect to the dictionary used
        var results = new List<CommonSpellingMistake>();
        using var fileStream = new FileStream("files/List_of_common_misspellings.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var fileReader = new StreamReader(fileStream, Encoding.UTF8, true);

        string line;
        while ((line = await fileReader.ReadLineAsync(ct).ConfigureAwait(false)) is not null)
        {
            if (string.IsNullOrWhiteSpace(line) || line[0] is '#' or '[')
            {
                continue;
            }

            var parts = line.Split(null);
            if (parts.Length != 2)
            {
                continue;
            }

            results.Add(new CommonSpellingMistake
            {
                Wrong = parts[0],
                Correct = parts[1]
            });
        }

        _words = results.ToImmutableArray();

        _spell = await spellTask.ConfigureAwait(false);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask DisposeAsync() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously


    [Fact]
    public void most_wrong_words_are_not_found()
    {
        var wrongCount = 0;

        Parallel.ForEach(
            _words,
            new() { CancellationToken = TestContext.Current.CancellationToken },
            word =>
            {
                if (_spell.Check(word.Wrong, TestContext.Current.CancellationToken))
                {
                    Interlocked.Increment(ref wrongCount);
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(_words.Length / 10);
    }

    [Fact]
    public void most_correct_words_are_found()
    {
        var wrongCount = 0;

        Parallel.ForEach(
            _words,
            new() { CancellationToken = TestContext.Current.CancellationToken },
            word =>
            {
                if (!_spell.Check(word.Correct, TestContext.Current.CancellationToken))
                {
                    Interlocked.Increment(ref wrongCount);
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(_words.Length / 10);
    }

    [Fact]
    public void most_correct_words_are_suggested_for_wrong_words()
    {
        var ct = TestContext.Current.CancellationToken;

        var words = _words
            .Where(static (_,i) => i % 11 == 0)
            .Where(word => _spell.Check(word.Correct, ct) && !_spell.Check(word.Wrong, ct))
            .Take(10)
            .ToArray();
        var wrongCount = 0;

        Parallel.ForEach(
            words,
            new() { CancellationToken = ct },
            word =>
            {
                var suggestions = _spell.Suggest(word.Wrong, new QueryOptions()
                {
                    TimeLimitSuggestGlobal = TimeSpan.FromSeconds(2),
                    TimeLimitCompoundSuggest = TimeSpan.FromSeconds(1),
                    TimeLimitSuggestStep = TimeSpan.FromSeconds(1),
                }, ct);

                if (!suggestions.Contains(word.Correct))
                {
                    Interlocked.Increment(ref wrongCount);
                }
            });

        wrongCount.ShouldBeLessThanOrEqualTo(words.Length / 5);
    }

    protected struct CommonSpellingMistake
    {
        public string Wrong;
        public string Correct;
    }
}
