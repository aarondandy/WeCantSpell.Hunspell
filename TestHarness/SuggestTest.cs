using System.Text;

namespace WeCantSpell.Hunspell.TestHarness;

public class SuggestTest
{
    public static void Suggest(string dicFilePath, string wordFilePath)
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

        foreach (var word in checkWords.Take(1000))
        {
            _ = wordList.Suggest(word);
        }
    }
}
