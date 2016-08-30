using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hunspell.NetCore.Tests
{
    public class HunspellTests
    {
        public class SimpleTests : HunspellTests
        {
            [Theory]
            [InlineData("aardvark")]
            [InlineData("Aardvark")]
            [InlineData("")]
            [InlineData("-")]
            public void cant_find_words_in_empty_dictioanry(string word)
            {
                var dictionary = new Dictionary.Builder().ToDictionary();
                var hunspell = new Hunspell(dictionary);

                var actual = hunspell.Check(word);

                actual.Should().BeFalse();
            }

            [Theory]
            [InlineData("aardvark", "bat")]
            [InlineData("bat", "aardvark")]
            [InlineData("aardvark", "aardvark")]
            [InlineData("", "aardvark")]
            [InlineData("aardvark", "")]
            public void can_find_words_in_single_word_dictioanry(string searchWord, string dictionaryWord)
            {
                var expected = searchWord == dictionaryWord;
                var dictionary = new Dictionary.Builder
                {
                    Entries = new Dictionary<string, List<DictionaryEntry>>
                    {
                        [dictionaryWord] = new List<DictionaryEntry>
                        {
                            new DictionaryEntry(dictionaryWord, Enumerable.Empty<FlagValue>(), Enumerable.Empty<string>(), DictionaryEntryOptions.None)
                        }
                    }
                }.ToDictionary();
                var hunspell = new Hunspell(dictionary);

                var actual = hunspell.Check(searchWord);

                actual.Should().Be(expected);
            }
        }

        public class CheckGoodWords : HunspellTests
        {
            public static IEnumerable<object[]> can_find_good_words_in_dictionary_args =>
                GetAllDataFilePaths("*.good")
                    .SelectMany(ToDictionaryWordTestData);

            [Theory, MemberData(nameof(can_find_good_words_in_dictionary_args))]
            public async Task can_find_good_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var hunspell = await Hunspell.FromFileAsync(dictionaryFilePath);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeTrue();
            }
        }

        public class CheckWrongWords : HunspellTests
        {
            public static IEnumerable<object[]> cant_find_wrong_words_in_dictionary_args =>
                GetAllDataFilePaths("*.wrong")
                    .SelectMany(ToDictionaryWordTestData);

            [Theory, MemberData(nameof(cant_find_wrong_words_in_dictionary_args))]
            public async Task cant_find_wrong_words_in_dictionary(string dictionaryFilePath, string word)
            {
                var hunspell = await Hunspell.FromFileAsync(dictionaryFilePath);

                var checkResult = hunspell.Check(word);

                checkResult.Should().BeFalse();
            }
        }

        public class Suggest : HunspellTests
        {
            public static IEnumerable<object[]> can_find_correct_best_suggestion_args()
            {
                foreach (var testSet in GetSuggestionTestFileSets().Where(s => s.WrongLines.Count == s.SuggestionLines.Count))
                {
                    for (var i = 0; i < testSet.WrongLines.Count; i++)
                    {
                        var suggestions = testSet.SuggestionLines[i]
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(w => w.Trim(SpaceOrTab))
                            .ToArray();

                        yield return new object[]
                        {
                            testSet.DictionaryFilePath,
                            testSet.WrongLines[i],
                            suggestions
                        };
                    }
                }
            }

            [Theory, MemberData(nameof(can_find_correct_best_suggestion_args))]
            public async Task can_find_correct_best_suggestion(string dictionaryFilePath, string givenWord, string[] expectedSuggestions)
            {
                var hunspell = await Hunspell.FromFileAsync(dictionaryFilePath);

                var actual = hunspell.Suggest(givenWord);

                actual.Should().NotBeNullOrEmpty();
                actual.Should().StartWith(expectedSuggestions);
            }

            public static IEnumerable<object[]> misfits() =>
                GetSuggestionTestFileSets()
                    .Where(s => s.WrongLines.Count != s.SuggestionLines.Count)
                    .Select(s => new object[] { s.DictionaryFilePath });

            [Theory, MemberData(nameof(misfits))]
            public async Task expose_misfits(string dictionaryFilePath)
            {
                // maybe keep this here, excluding tests that are hand made
                // just make individual tests for each specific file set that gives problems
                // looks like there are 5 files so far with this problem
                throw new NotImplementedException("Need to find a way to test the suggestions correctly.");
            }

            protected static IEnumerable<SuggestionTestSet> GetSuggestionTestFileSets()
            {
                foreach (var suggestionFilePath in GetAllDataFilePaths("*.sug"))
                {
                    var wrongFilePath = Path.ChangeExtension(suggestionFilePath, "wrong");
                    if (!File.Exists(wrongFilePath))
                    {
                        throw new InvalidOperationException();
                    }

                    var dictionaryFilePath = Path.ChangeExtension(wrongFilePath, "dic");
                    var affix = AffixReader.ReadFile(Path.ChangeExtension(dictionaryFilePath, "aff"));
                    var wrongLines = ExtractLinesFromWordFile(wrongFilePath, affix.Encoding).ToList();
                    var suggestionLines = ExtractLinesFromWordFile(suggestionFilePath, affix.Encoding).ToList();

                    yield return new SuggestionTestSet
                    {
                        DictionaryFilePath = dictionaryFilePath,
                        WrongFilePath = wrongFilePath,
                        SuggestionFilePath = suggestionFilePath,
                        WrongLines = wrongLines,
                        SuggestionLines = suggestionLines
                    };
                }
            }

            protected class SuggestionTestSet
            {
                public string DictionaryFilePath;

                public string WrongFilePath;

                public string SuggestionFilePath;

                public List<string> WrongLines;

                public List<string> SuggestionLines;
            }
        }

        protected static IEnumerable<string> GetAllDataFilePaths(string searchPattern)
        {
            return Directory.GetFiles("files/", searchPattern);
        }

        protected static readonly char[] SpaceOrTab = new[] { ' ', '\t' };

        protected static IEnumerable<object[]> ToDictionaryWordTestData(string wordFilePath)
        {
            var dictionaryPath = Path.ChangeExtension(wordFilePath, "dic");
            var affix = AffixReader.ReadFile(Path.ChangeExtension(wordFilePath, "aff"));

            return ExtractMultipleWordsFromWordFile(wordFilePath, affix.Encoding)
                .Distinct()
                .OrderBy(w => w, StringComparer.Ordinal)
                .Select(line => new object[] { dictionaryPath, line });
        }

        protected static IEnumerable<string> ExtractLinesFromWordFile(string filePath, Encoding encoding)
        {
            return StaticEncodingLineReader.ReadLines(filePath, encoding)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Trim(SpaceOrTab))
                .Where(line => line.Length != 0);
        }

        protected static IEnumerable<string> ExtractMultipleWordsFromWordFile(string filePath, Encoding encoding)
        {
            return ExtractLinesFromWordFile(filePath, encoding)
                .SelectMany(line => line.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
