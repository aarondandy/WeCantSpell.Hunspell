namespace WeCantSpell.Hunspell.TestHarness;

public static class SimpleIssueScenarios
{
    public static void Issue88()
    {
        Console.WriteLine("Issue88 Reproduction");

        var inputstringList = new List<string> { "Seville", "Deville" };
        var dictionary = WordList.CreateFromWords(inputstringList);

        for (var i = 0; i < 10; i++)
        {
            var query = "Sevill";
            var suggestions = dictionary.Suggest(query);
            Console.WriteLine($"Suggestions for {query}: {string.Join(", ", suggestions)}");
        }
    }
}
