using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

public class CategorizedWordData
{
    public static List<string> GetAssortedEnUsWords() =>
        GetAssortedWords(Path.Combine(DataFilePaths.TestFilesFolderPath, "List_of_common_misspellings.txt"));

    public static List<string> GetAssortedWords(string textFilePath)
    {
        var result = new List<string>();
        using var stram = new FileStream(textFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var reader = new StreamReader(stram, Encoding.UTF8, true);

        string line;
        while ((line = reader.ReadLine()) is not null)
        {
            line = line.Trim();

            if (line.Length == 0 || line.StartsWith("#") || line.StartsWith("["))
            {
                continue;
            }

            result.AddRange(line.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries));
        }

        return result;
    }

    public static CategorizedWordData Create(IEnumerable<string> words, Func<string, bool> isCorrect, Func<string, bool> isRoot)
    {
        var allWords = words.Distinct().OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
        var correctWords = new HashSet<string>(allWords.Where(isCorrect));
        var wrongWords = new HashSet<string>(allWords.Where(word => !correctWords.Contains(word)));
        var rootWords = new HashSet<string>(allWords.Where(isRoot));

        return new CategorizedWordData
        {
            AllWords = allWords,
            CorrectWords = correctWords,
            WrongWords = wrongWords,
            RootWords = rootWords
        };
    }

    private CategorizedWordData()
    {
    }

    public List<string> AllWords { get; private init; }
    public HashSet<string> CorrectWords { get; private init; }
    public HashSet<string> WrongWords { get; private init; }
    public HashSet<string> RootWords { get; private init; }
}
