using System.Text;

namespace WeCantSpell.Hunspell.TestHarness;

public static class SuggestTest
{
    public static void Run(string dicFilePath, string wordFilePath)
    {
        var wordList = WordListReader.ReadFile(dicFilePath);
        var checkWords = new List<string>();

        using var reader = new StreamReader(new FileStream(wordFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8, true);

        var lineSplitChars = " \t,".ToCharArray();
        string? line;
        while ((line = reader.ReadLine()?.Trim()) is not null)
        {
            if (line.Length == 0 || line.StartsWith('#') || line.StartsWith('['))
            {
                continue;
            }

            checkWords.AddRange(line.Split(lineSplitChars, StringSplitOptions.RemoveEmptyEntries));
        }

        const int wordLimit = 2000;

        if (checkWords.Count > wordLimit)
        {
            checkWords.RemoveRange(wordLimit, checkWords.Count - wordLimit);
        }

        Console.WriteLine($"Suggesting for {checkWords.Count} words");

        foreach (var word in checkWords)
        {
            var suggestions = wordList.Suggest(word);
            Console.WriteLine($"{word}: {string.Join(", ", suggestions)}");
        }
    }
}
