using System.Diagnostics;

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

        var results = new List<(int, TimeSpan)>(wordLimit);
        var allSuggestions = new HashSet<string>();

        Console.WriteLine($"Suggesting for word \"{word}\" {wordLimit} times");

        var iterationStopwatch = new Stopwatch();
        var fullStopwatch = Stopwatch.StartNew();

        for (var i = 0; i < wordLimit; i++)
        {
            iterationStopwatch.Restart();
            var suggestions = wordList.Suggest(word, options).ToList();
            iterationStopwatch.Stop();

            allSuggestions.UnionWith(suggestions);
            results.Add((suggestions.Count, iterationStopwatch.Elapsed));

            Console.WriteLine($"Iteration {i:00}:\t{suggestions.Count}\t{iterationStopwatch.ElapsedMilliseconds}");
        }

        fullStopwatch.Stop();

        Console.WriteLine("Results:");
        Console.WriteLine($"Average result count: {results.Average(static r => (decimal)r.Item1)}");
        Console.WriteLine($"Average time: {results.Average(static r => r.Item2.TotalMilliseconds)} ms");

        Console.WriteLine("Suggestions:");
        foreach (var s in allSuggestions)
        {
            Console.WriteLine(s);
        }
    }
}
