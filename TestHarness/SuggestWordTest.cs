namespace WeCantSpell.Hunspell.TestHarness;

public static class SuggestWordTest
{
    public static void RunIterationsSlow(string dicFilePath, string word)
    {
        var options = new QueryOptions
        {
            TimeLimitCompoundSuggest = TimeSpan.FromSeconds(10),
            TimeLimitCompoundCheck = TimeSpan.FromSeconds(10),
            TimeLimitSuggestGlobal = TimeSpan.FromSeconds(10),
            TimeLimitSuggestStep = TimeSpan.FromSeconds(10),
            MaxSuggestions = 10
        };

        Run(dicFilePath, word, options, wordLimit: 20);
    }

    public static void RunIterationsFast(string dicFilePath, string word)
    {
        Run(dicFilePath, word, options: null, wordLimit: 50);
    }

    public static void Run(string dicFilePath, string word, QueryOptions? options, int wordLimit)
    {
        var wordList = WordListReader.ReadFile(dicFilePath);

        var results = new List<int>(wordLimit);
        var allSuggestions = new HashSet<string>();

        Console.WriteLine($"Suggesting for word \"{word}\" {wordLimit} times");

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

        Console.WriteLine($"Average: {results.Average(static r => (decimal)r)}");

        Console.WriteLine("Suggestions:");
        foreach (var s in allSuggestions)
        {
            Console.WriteLine(s);
        }
    }
}
