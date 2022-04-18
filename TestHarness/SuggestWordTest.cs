namespace WeCantSpell.Hunspell.TestHarness;

public class SuggestWordTest
{
    public static void Run(string dicFilePath, string word)
    {
        var wordList = WordListReader.ReadFile(dicFilePath);
        const int wordLimit = 20;

        var results = new List<int>(wordLimit);

        Console.WriteLine($"Suggesting for word \"{word}\" {wordLimit} times");

        for (var i = 0; i < wordLimit; i++)
        {
            var suggestions = wordList.Suggest(word);
            results.Add(suggestions.Count());
        }

        Console.WriteLine("Results:");
        foreach (var r in results)
        {
            Console.WriteLine($"{r}");
        }
    }
}
