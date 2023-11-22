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

    public static CategorizedWordData Create(IEnumerable<string> words, WordList wordList) => Create(
        words,
        isCorrect: wordList.Check,
        isRoot: wordList.ContainsEntriesForRootWord);

    public static CategorizedWordData Create(IEnumerable<string> words, Func<string, bool> isCorrect, Func<string, bool> isRoot)
    {
        var allWords = words.Distinct().OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
        allWords.TrimExcess();
        var correctWords = allWords.FindAll(word => isCorrect(word));
        correctWords.TrimExcess();
        var wrongWords = allWords.Except(correctWords).ToList();
        wrongWords.TrimExcess();
        var rootWords = allWords.FindAll(word => isRoot(word));
        rootWords.TrimExcess();

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

    public List<string> AllWords { get; private set; }
    public List<string> CorrectWords { get; private set; }
    public List<string> WrongWords { get; private set; }
    public List<string> RootWords { get; private set; }
}
