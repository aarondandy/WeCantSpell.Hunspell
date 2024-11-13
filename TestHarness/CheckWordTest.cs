namespace WeCantSpell.Hunspell.TestHarness;

internal static class CheckWordTest
{
    public static void Run(string dicFilePath, string word)
    {
        var wordList = WordListReader.ReadFile(dicFilePath);

        var correct = wordList.Check(word);

        Console.WriteLine($"{word}: {correct}");
    }
}
