using Cocona;

using WeCantSpell.Hunspell.TestHarness;

var app = CoconaApp.Create();

app.AddCommand("load", (string file) => LoadTest.LoadDictionary(file));
app.AddCommand("load-all", (string path) => LoadTest.LoadAllDictionaries(path));

app.Run();
