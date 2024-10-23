namespace WeCantSpell.Hunspell.TestHarness;

public static class SimpleIssueScenarios
{
    public static void Issue88()
    {
        string[] inputstringList = ["Seville", "Deville"];
        var dictionary = WordList.CreateFromWords(inputstringList);

        for (var i = 0; i < 10; i++)
        {
            const string query = "Sevill";
            var suggestions = dictionary.Suggest(query);
            Console.WriteLine($"Suggestions for {query}: {string.Join(", ", suggestions)}");
        }
    }

    public static void Issue91()
    {
        string[] inputstringList = ["A100", "P100", "A100 Truck", "D100 Series"];
        var dictionary = WordList.CreateFromWords(inputstringList);

        for (var i = 0; i < 10; i++)
        {
            const string query = "100";
            var suggestions = dictionary.Suggest(query);
            Console.WriteLine($"Suggestions for {query}: {string.Join(", ", suggestions)}");
        }
    }
}
