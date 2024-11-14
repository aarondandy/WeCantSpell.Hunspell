using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WeCantSpell.Hunspell.Benchmarks.Helpers;

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

            if (string.IsNullOrWhiteSpace(line) || line[0] is '#' or '[')
            {
                continue;
            }

            result.AddRange(line.Split([' ', '\t', ','], StringSplitOptions.RemoveEmptyEntries));
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
        var smallSampling = allWords.Where(static (_, i) => i % 100 == 0).ToList();
        smallSampling.TrimExcess();

        return new CategorizedWordData
        {
            AllWords = allWords,
            CorrectWords = correctWords,
            WrongWords = wrongWords,
            RootWords = rootWords,
            SmallSampling = smallSampling
        };
    }

    private CategorizedWordData()
    {
    }

    public List<string> AllWords { get; private set; }
    public List<string> CorrectWords { get; private set; }
    public List<string> WrongWords { get; private set; }
    public List<string> RootWords { get; private set; }
    public List<string> SmallSampling { get; private set; }
}
