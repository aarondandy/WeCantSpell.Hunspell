using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public static class TestTheories
{
    static TestTheories()
    {
        Helpers.EnsureEncodingsReady();
    }

    internal static char[] SpaceOrTab = [' ', '\t'];

    public static string[] GetTestFilePathsByPattern(string pattern)
    {
        var filePaths = Directory.GetFiles("files/", pattern);
        Array.Sort(filePaths, StringComparer.OrdinalIgnoreCase);
        return filePaths;
    }

    public static async ValueTask<List<string>> LoadWordListAsync(string filePath, Encoding encoding, bool sort = false, CancellationToken ct = default)
    {
        var words = new List<string>();

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var reader = new StreamReader(fileStream, encoding, true);

        while ((await reader.ReadLineAsync(ct).ConfigureAwait(false)) is { } rawLine)
        {
            var parts = rawLine.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
            words.AddRange(parts);
        }

        words = words.Distinct().ToList();

        if (sort)
        {
            words.Sort(StringComparer.Ordinal);
        }

        return words;
    }

    public static async ValueTask<List<string>> LoadLinesAsync(string filePath, Encoding encoding, bool allowBlankLines = false, CancellationToken ct = default)
    {
        var lines = new List<string>();

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var reader = new StreamReader(fileStream, encoding, true);

        while ((await reader.ReadLineAsync(ct).ConfigureAwait(false)) is { } rawLine)
        {
            var line = rawLine.Trim(SpaceOrTab);
            if (allowBlankLines || line.Length > 0)
            {
                lines.Add(line);
            }
        }

        return lines;
    }

    public class AffFilePathsData : TheoryData<string>
    {
        public AffFilePathsData() : base(TestTheories.GetTestFilePathsByPattern("*.aff"))
        {
        }
    }

    public class DicFilePathsData : TheoryData<string>
    {
        public DicFilePathsData() : base(GetTestFilePathsByPattern("*.dic"))
        {
        }
    }

    public class GoodWordsData : TheoryData<string, string>
    {
        public static IEnumerable<TheoryDataRow<string, string>> ProduceRows(CancellationToken ct)
        {
            var wordListFilePaths = GetTestFilePathsByPattern("*.good");

            var produceGroups = Task.WhenAll(Array.ConvertAll(wordListFilePaths, produceRowGroup)).GetAwaiter().GetResult();

            return produceGroups.SelectMany(g =>
                g.words
                    // NOTE: These tests are bypassed because capitalization only works when the language is turkish and the UTF8 dic has no language applied
                    .Where(w => (g.dictionaryFilePath.EndsWith("base_utf.dic") && w.Contains('İ')) is false)
                    .Select(w => new TheoryDataRow<string, string>(g.dictionaryFilePath, w)));

            async Task<(string dictionaryFilePath, List<string> words)> produceRowGroup(string wordListFilePath)
            {
                var dictionaryFilePath = Path.ChangeExtension(wordListFilePath, "dic");
                var words = await LoadWordListAsync(wordListFilePath, Encoding.UTF8, sort: true, ct);
                return (dictionaryFilePath, words);
            }
        }

        public GoodWordsData()
        {
            AddRange(ProduceRows(CancellationToken.None));
        }
    }

    public class WrongWordsData : TheoryData<string, string>
    {
        public static IEnumerable<TheoryDataRow<string, string>> ProduceRows(CancellationToken ct)
        {
            var wordListFilePaths = GetTestFilePathsByPattern("*.wrong");

            var produceGroups = Task.WhenAll(Array.ConvertAll(wordListFilePaths, produceRowGroup)).GetAwaiter().GetResult();

            return produceGroups.SelectMany(g =>
                g.words.Select(w =>
                    new TheoryDataRow<string, string>(g.dictionaryFilePath, w)));

            async Task<(string dictionaryFilePath, List<string> words)> produceRowGroup(string wordListFilePath)
            {
                var dictionaryFilePath = Path.ChangeExtension(wordListFilePath, "dic");
                var words = await LoadWordListAsync(wordListFilePath, Encoding.UTF8, sort: true, ct);
                return (dictionaryFilePath, words);
            }
        }

        public WrongWordsData()
        {
            AddRange(ProduceRows(CancellationToken.None));
        }
    }

    public class SuggestionData : TheoryData<string, string, string[]>
    {
        private static HashSet<string> ExcludeFileNames { get; } = new(StringComparer.OrdinalIgnoreCase)
        {
            "nosuggest",
            "onlyincompound",
            "opentaal_forbiddenword1",
            "opentaal_forbiddenword2",
            "rep",
            "ngram_utf_fix",
            "utf8_nonbmp",
            "phone"
        };

        public static IEnumerable<TheoryDataRow<string, string, string[]>[]> ProduceRowGroups(CancellationToken ct)
        {
            var sugFilePaths = GetTestFilePathsByPattern("*.sug")
                .Where(static n => ExcludeFileNames.Contains(Path.GetFileNameWithoutExtension(n)) is false);

            return Task.WhenAll(sugFilePaths.Select(loadSet)).GetAwaiter().GetResult();

            async Task<TheoryDataRow<string, string, string[]>[]> loadSet(string sugFilePath)
            {
                var wrongFilePath = Path.ChangeExtension(sugFilePath, "wrong");
                var dictionaryFilePath = Path.ChangeExtension(sugFilePath, "dic");

                var wrongLinesTask = LoadLinesAsync(wrongFilePath, Encoding.UTF8, ct: ct);
                var suggestionLinesTask = LoadLinesAsync(sugFilePath, Encoding.UTF8, allowBlankLines: true, ct: ct);
                var wrongLines = await wrongLinesTask.ConfigureAwait(false);
                var suggestionLines = await suggestionLinesTask.ConfigureAwait(false);

                if (sugFilePath.EndsWith("ph2.sug"))
                {
                    // NOTE: ph2.wrong does not have a corresponding blank suggestion in the file for rootforbiddenroot
                    suggestionLines.Insert(8, string.Empty);
                }

                if (suggestionLines.Count > wrongLines.Count)
                {
                    throw new InvalidDataException($"File {sugFilePath} has too many suggestions");
                }

                while (suggestionLines.Count < wrongLines.Count)
                {
                    suggestionLines.Add("");
                }

                var set = new TheoryDataRow<string, string, string[]>[wrongLines.Count];
                for (var i = 0; i < wrongLines.Count; i++)
                {
                    var suggestions = suggestionLines[i].Split([','], StringSplitOptions.RemoveEmptyEntries);
                    set[i] = new(dictionaryFilePath, wrongLines[i], Array.ConvertAll(suggestions, static s => s.Trim(SpaceOrTab)));
                }

                return set;
            }
        }

        public SuggestionData()
        {
            foreach (var set in ProduceRowGroups(CancellationToken.None))
            {
                AddRange(set);
            }
        }
    }
}
