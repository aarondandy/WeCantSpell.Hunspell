namespace WeCantSpell.Hunspell.TestHarness;

public class LoadTest
{
    public static void LoadDictionary(string filePath)
    {
        var wordList = WordListReader.ReadFile(filePath);
        Console.WriteLine($"Loaded {wordList.RootWords.Count()} roots");
    }

    public static void LoadAllDictionaries(string path)
    {
        var paths = Directory.GetFiles(path, "*.dic");

        foreach (var filePath in paths)
        {
            var wordList = WordListReader.ReadFile(filePath);
            Console.WriteLine($"Loaded {wordList.RootWords.Count()} roots from {filePath}");
        }
    }
}
