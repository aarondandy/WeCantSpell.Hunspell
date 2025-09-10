using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class Issue106Tests : IAsyncLifetime
{
    private WordList _wordListFr = null!;
    private WordList _wordListEs = null!;

    public async ValueTask InitializeAsync()
    {
        _wordListFr = await DictionaryLoader.GetDictionaryAsync("files/French.dic", TestContext.Current.CancellationToken);
        _wordListEs = await DictionaryLoader.GetDictionaryAsync("files/Spanish.dic", TestContext.Current.CancellationToken);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask DisposeAsync() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    [Fact]
    public void can_suggest_fr_designation()
    {
        var given = "Designation";
        var actual = _wordListFr.Suggest(given, TestContext.Current.CancellationToken);
        actual.ShouldContain("Désignation");
    }

    [Fact]
    public void can_suggest_es_descripcion()
    {
        var given = "Descripcion";
        var actual = _wordListEs.Suggest(given, TestContext.Current.CancellationToken);
        actual.ShouldContain("Descripción");
    }
}
