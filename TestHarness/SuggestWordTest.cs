namespace WeCantSpell.Hunspell.TestHarness;

public static class SuggestWordTest
{
    public static void Run(string dicFilePath, string word)
    {
        var wordList = WordListReader.ReadFile(dicFilePath);
        const int wordLimit = 20;

        var results = new List<int>(wordLimit);
        var allSuggestions = new HashSet<string>();

        Console.WriteLine($"Suggesting for word \"{word}\" {wordLimit} times");

        var options = new QueryOptions
        {
            TimeLimitCompoundSuggest = TimeSpan.FromSeconds(10),
            TimeLimitCompoundCheck = TimeSpan.FromSeconds(10),
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(10),
            TimeLimitSuggestStep = TimeSpan.FromSeconds(10),
            MaxSuggestions = 10
        };

        for (var i = 0; i < wordLimit; i++)
        {
            var suggestions = wordList.Suggest(word, options);
            allSuggestions.UnionWith(suggestions);
            results.Add(suggestions.Count());
        }

        Console.WriteLine("Results:");
        foreach (var r in results)
        {
            Console.WriteLine($"{r}");
        }

        Console.WriteLine("Suggestions:");
        foreach (var s in allSuggestions)
        {
            Console.WriteLine(s);
        }
    }
}
