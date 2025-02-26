using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class WordAddTests
{
    [Fact]
    public void can_add_to_empty_word_list_and_check()
    {
        string word = "word";
        var wordList = new WordList.Builder().Build();
        wordList.Check(word, CancellationToken.None).ShouldBeFalse();

        wordList.Add(word);

        wordList.Check(word, CancellationToken.None).ShouldBeTrue();
    }

    [Fact]
    public void can_add_to_empty_word_list_and_suggest()
    {
        string given = "ord";
        string word = "word";
        var wordList = new WordList.Builder().Build();
        var suggestions = wordList.Suggest(given, CancellationToken.None);
        suggestions.ShouldNotContain(word);

        wordList.Add(word);

        suggestions = wordList.Suggest(given, CancellationToken.None);
        suggestions.ShouldContain(word);
    }

    [Fact]
    public async Task can_add_to_en_us_word_list_for_check()
    {
        var ct = TestContext.Current.CancellationToken;
        string word = "qwertyuiop";
        var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", ct);
        wordList.Check(word, ct).ShouldBeFalse();

        wordList.Add(word);

        wordList.Check(word, ct).ShouldBeTrue();
    }

    [Fact]
    public async Task can_add_to_en_us_word_list_for_suggest()
    {
        var ct = TestContext.Current.CancellationToken;
        string given = "qwertyuio";
        string word = "qwertyuiop";
        var wordList = await WordList.CreateFromFilesAsync("files/English (American).dic", ct);
        var suggestions = wordList.Suggest(given, ct);
        suggestions.ShouldNotContain(word);
        
        wordList.Add(word);

        suggestions = wordList.Suggest(given, ct);
        suggestions.ShouldContain(word);
    }
}
