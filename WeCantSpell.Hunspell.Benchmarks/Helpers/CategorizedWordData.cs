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
        var allWords = words.Distinct().OrderBy(static x => x, StringComparer.OrdinalIgnoreCase).ToArray();
        var correctWords = allWords.Where(isCorrect).ToArray();
        var wrongWords = allWords.Except(correctWords).ToArray();

        // Suggest sampling should be a few good words mixed in with mostly wrong words
        var suggestGroup = wrongWords.Where(static (_, i) => i % 15 == 0) // start with some wrong words, about 266
            .Concat(correctWords.Where(static (_, i) => i % 100 == 0)) // finish off with some correct words, about 40
            .OrderBy(static x => x, StringComparer.OrdinalIgnoreCase); // mix it all up

        return new CategorizedWordData
        {
            // Who doesn't like nice round numbers?
            MostWords = allWords.Take(7000).ToArray(),
            CorrectWords = correctWords.Take(3000).ToArray(),
            WrongWords = wrongWords.Take(4000).ToArray(),
            //RootWords = allWords.Where(isRoot).ToArray(),
            SmallSuggestSampling = suggestGroup.Take(300).ToArray()
        };
    }

    private CategorizedWordData()
    {
    }

    public string[] MostWords { get; private set; }
    public string[] CorrectWords { get; private set; }
    public string[] WrongWords { get; private set; }
    //public string[] RootWords { get; private set; }
    public string[] SmallSuggestSampling { get; private set; }
}
