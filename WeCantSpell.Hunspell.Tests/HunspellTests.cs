using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class HunspellTests
{
    static CancellationToken TestCancellation => TestContext.Current.CancellationToken;

    static HunspellTests()
    {
        EncodingHelpers.EnsureEncodingsReady();
    }

    public class SimpleTests : HunspellTests
    {
        [Theory]
        [InlineData("aardvark")]
        [InlineData("Aardvark")]
        [InlineData("")]
        [InlineData("-")]
        public void cant_find_words_in_empty_dictioanry(string word)
        {
            var dictionary = new WordList.Builder().ToImmutable();

            var actual = dictionary.Check(word, TestCancellation);

            actual.ShouldBeFalse();
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
            var dictionaryBuilder = new WordList.Builder();
            dictionaryBuilder.InitializeEntriesByRoot(1);
            dictionaryBuilder.Add(dictionaryWord, WordEntryDetail.Default);

            var dictionary = dictionaryBuilder.ToImmutable();

            var actual = dictionary.Check(searchWord, TestCancellation);

            actual.ShouldBe(expected);
        }

        public static IEnumerable<TheoryDataRow<string>> checking_large_word_does_not_cause_errors_args() =>
            GetAllDataFilePaths("*.dic").Select(filePath => new TheoryDataRow<string>(filePath));

        [Theory, MemberData(nameof(checking_large_word_does_not_cause_errors_args))]
        public async Task checking_large_word_does_not_cause_errors(string filePath)
        {
            // attampt to reproduce https://github.com/hunspell/hunspell/issues/446
            var largeInput = new string('X', 102);
            var dictionary = await WordList.CreateFromFilesAsync(filePath, TestCancellation);

            var actual = dictionary.Check(largeInput, TestCancellation);

            actual.ShouldBeFalse();
        }
    }

    public class CheckGoodWords : HunspellTests
    {
        public static IEnumerable<TheoryDataRow<string, string>> can_find_good_words_in_dictionary_args
        {
            get
            {
                var results = GetAllDataFilePaths("*.good")
                    .SelectMany(ToDictionaryWordTestData)
                    // NOTE: These tests are bypassed because capitalization only works when the language is turkish and the UTF8 dic has no language applied
                    .Where(t => (t.dictionaryPath.EndsWith("base_utf.dic") && t.word.Contains('İ')) is false);

                return results.Select(t => new TheoryDataRow<string, string>(t.dictionaryPath, t.word));
            }
        }

        [Theory, MemberData(nameof(can_find_good_words_in_dictionary_args))]
        public async Task can_find_good_words_in_dictionary(string dictionaryFilePath, string word)
        {
            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            QueryOptions options;
            if (dictionaryFilePath.IndexOf("compound", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                options = new QueryOptions()
                {
                    TimeLimitCompoundCheck = TimeSpan.FromSeconds(10)
                };
            }
            else
            {
                options = null;
            }

            var checkResult = dictionary.Check(word, options, TestCancellation);

            checkResult.ShouldBeTrue();
        }

        [Theory, MemberData(nameof(can_find_good_words_in_dictionary_args))]
        public async Task can_find_good_word_spans_in_dictionary(string dictionaryFilePath, string word)
        {
            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var checkResult = dictionary.Check(word.AsSpan(), TestCancellation);

            checkResult.ShouldBeTrue();
        }
    }

    public class CheckWrongWords : HunspellTests
    {
        public static IEnumerable<TheoryDataRow<string, string>> cant_find_wrong_words_in_dictionary_args =>
            GetAllDataFilePaths("*.wrong")
                .SelectMany(ToDictionaryWordTestData)
                .Select(t => new TheoryDataRow<string, string>(t.dictionaryPath, t.word));

        [Theory, MemberData(nameof(cant_find_wrong_words_in_dictionary_args))]
        public async Task cant_find_wrong_words_in_dictionary(string dictionaryFilePath, string word)
        {
            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var checkResult = dictionary.Check(word, TestCancellation);

            checkResult.ShouldBeFalse();
        }

        /// <remarks>
        /// Removed from tests in origin but I wanted to keep it around:
        /// https://github.com/hunspell/hunspell/commit/8d2f85556e7d6712277547cdeea0e424e80527c4 .
        /// The comment on the commit shows why it may have been removed, but I want this test so
        /// I know if changes in behavior ever impact the limit:
        ///   This is an artificial limit, it would be better not to limit the recognition of this kind of compounding.
        /// </remarks>
        [Fact]
        public async Task can_still_find_10_break_pattern_word_wrong()
        {
            var word = "foo-bar-foo-bar-foo-bar-foo-bar-foo-bar-foo";
            var dictionary = await WordList.CreateFromFilesAsync("files/break.dic", TestCancellation);

            var checkResult = dictionary.Check(word, TestCancellation);

            checkResult.ShouldBeFalse();
        }
    }

    public class Suggest : HunspellTests
    {
        [Theory]
        [InlineData("files/nosuggest.dic", "foox")]
        [InlineData("files/nosuggest.dic", "foobarx")]
        [InlineData("files/nosuggest.dic", "barfoox")]
        [InlineData("files/onlyincompound.dic", "pseudo")]
        [InlineData("files/onlyincompound.dic", "pseudos")]
        [InlineData("files/opentaal_forbiddenword1.dic", "foowordbar")]
        [InlineData("files/opentaal_forbiddenword1.dic", "foowordbars")]
        [InlineData("files/opentaal_forbiddenword1.dic", "foowordba")]
        [InlineData("files/opentaal_forbiddenword1.dic", "foowordbas")]
        [InlineData("files/opentaal_forbiddenword2.dic", "foowordbar")]
        [InlineData("files/opentaal_forbiddenword2.dic", "foowordbars")]
        [InlineData("files/opentaal_forbiddenword2.dic", "foowordba")]
        [InlineData("files/opentaal_forbiddenword2.dic", "foowordbas")]
        [InlineData("files/rep.dic", "vacashuns")]
        [InlineData("files/rep.dic", "foobars")]
        [InlineData("files/rep.dic", "barfoos")]
        [InlineData("files/ngram_utf_fix.dic", "времячко")]
        public async Task words_without_suggestions_offer_no_suggestions(string dictionaryFilePath, string word)
        {
            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var actual = dictionary.Suggest(word, TestCancellation);

            actual.ShouldBeEmpty();
        }

        [Theory]
        [InlineData("files/opentaal_forbiddenword1.dic", "barwodfoo", new[] { "barwordfoo" })]
        [InlineData("files/opentaal_forbiddenword2.dic", "barwodfoo", new[] { "barwordfoo" })]
        [InlineData("files/rep.dic", "phorm", new[] { "form" })]
        [InlineData("files/rep.dic", "fantom", new[] { "phantom" })]
        [InlineData("files/rep.dic", "vacashun", new[] { "vacation" })]
        [InlineData("files/rep.dic", "alot", new[] { "a lot", "lot" })]
        [InlineData("files/rep.dic", "un'alunno", new[] { "un alunno" })]
        [InlineData("files/rep.dic", "foo", new[] { "bar" })]
        [InlineData("files/rep.dic", "vinteún", new[] { "vinte e un" })]
        [InlineData("files/rep.dic", "autos", new[] { "auto's" })]
        [InlineData("files/ngram_utf_fix.dic", "человеко", new[] { "человек" })]
        [InlineData("files/utf8_nonbmp.dic", "𐏑𐏒𐏒", new[] { "𐏑 𐏒𐏒", "𐏒𐏑", "𐏒𐏒" })]
        [InlineData("files/ignoresug.dic", "ինչ", new[] { "ինչ" })]
        [InlineData("files/ignoresug.dic", "ի՞նչ", new[] { "ինչ" })]
        [InlineData("files/ignoresug.dic", "մնաս", new[] { "մնաս" })]
        [InlineData("files/ignoresug.dic", "մնա՜ս", new[] { "մնաս" })]
        [InlineData("files/ignoresug.dic", "որտեղ", new[] { "որտեղ" })]
        [InlineData("files/ignoresug.dic", "որտե՞ղ", new[] { "որտեղ" })]
        public async Task words_offer_specific_suggestions(string dictionaryFilePath, string word, string[] expectedSuggestions)
        {
            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var actual = dictionary.Suggest(
                word,
                new QueryOptions()
                {
                    TimeLimitCompoundSuggest = TimeSpan.FromSeconds(10)
                },
                TestCancellation);

            actual.ShouldBe(expectedSuggestions);
        }

        [Fact]
        public async Task can_find_most_phone_suggestions()
        {
            var dictionaryFilePath = "files/phone.dic";
            var word = "Brasillian";
            var minimumExpectedSuggestions = new[] { "Brasilia", "Xxxxxxxxxx", "Brilliant", "Brazilian", "Brassily", "Brilliance" };

            var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var actual = dictionary.Suggest(
                word,
                new QueryOptions
                {
                    TimeLimitSuggestGlobal = TimeSpan.FromSeconds(10), // This test can be a bit slow
                    // Due to different internal dictionary orderings, the expected suggestions may not all appear unless we bring back more results
                    MaxPhoneticSuggestions = 5,
                    MaxSuggestions = 10
                },
                TestCancellation);

            actual.ShouldNotBeNull();
            actual.ShouldNotBeEmpty();

            foreach (var s in minimumExpectedSuggestions)
            {
                actual.ShouldContain(s);
            }
        }

        public static IEnumerable<TheoryDataRow<string, string, string[]>> can_find_correct_best_suggestion_args()
        {
            foreach (var testSet in GetSuggestionTestFileSets().Where(s => s.WrongLines.Count == s.SuggestionLines.Count))
            {
                for (var i = 0; i < testSet.WrongLines.Count; i++)
                {
                    var suggestions = testSet.SuggestionLines[i]
                        .Split([','], StringSplitOptions.RemoveEmptyEntries)
                        .Select(w => w.Trim(SpaceOrTab))
                        .ToArray();

                    yield return new TheoryDataRow<string, string, string[]>
                    (
                        testSet.DictionaryFilePath,
                        testSet.WrongLines[i],
                        suggestions
                    );
                }
            }
        }

        [Theory, MemberData(nameof(can_find_correct_best_suggestion_args))]
        public async Task can_find_correct_best_suggestion(string dictionaryFilePath, string givenWord, string[] expectedSuggestions)
        {
            QueryOptions options;

            if (
                dictionaryFilePath.EndsWith("i35725.dic")
                ||
                dictionaryFilePath.EndsWith("opentaal_keepcase.dic")
            )
            {
                // These tests run a bit slower than others and need some more time to complete
                options = new()
                {
                    TimeLimitSuggestStep = TimeSpan.FromSeconds(20),
                    TimeLimitCompoundSuggest = TimeSpan.FromSeconds(20),
                    TimeLimitCompoundCheck = TimeSpan.FromSeconds(20),
                    TimeLimitSuggestGlobal = TimeSpan.FromSeconds(20)
                };
            }
            else
            {
                options = new()
                {
                    TimeLimitCompoundSuggest = TimeSpan.FromSeconds(5),
                    TimeLimitSuggestGlobal = TimeSpan.FromSeconds(5)
                };
            }

                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath, TestCancellation);

            var actual = dictionary.Suggest(givenWord, options, TestCancellation);

            actual.ShouldNotBeNull();

            // ',' can either be a delimiter in the test data or part of the data
            var actualText = string.Join(", ", actual);
            var expectedText = string.Join(", ", expectedSuggestions);
            actualText.ShouldBe(expectedText);
        }

        [Fact]
        public void untested_suggestion_files_should_not_be_found()
        {
            var untestedSets = GetSuggestionTestFileSets().Where(s => s.WrongLines.Count != s.SuggestionLines.Count);

            untestedSets.ShouldBeEmpty();
        }

        private static readonly HashSet<string> ExcludedSuggestionFiles = new(StringComparer.OrdinalIgnoreCase)
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

        protected static IEnumerable<SuggestionTestSet> GetSuggestionTestFileSets()
        {
            var suggestionFilePaths = GetAllDataFilePaths("*.sug")
                .Where(p => !ExcludedSuggestionFiles.Contains(Path.GetFileNameWithoutExtension(p)));

            foreach (var suggestionFilePath in suggestionFilePaths)
            {
                var wrongFilePath = Path.ChangeExtension(suggestionFilePath, "wrong");
                if (!File.Exists(wrongFilePath))
                {
                    throw new InvalidOperationException($"File {wrongFilePath} not found");
                }

                var dictionaryFilePath = Path.ChangeExtension(wrongFilePath, "dic");
                var encoding = Encoding.UTF8;
                var wrongLines = ExtractLinesFromWordFile(wrongFilePath, encoding).ToList();
                var suggestionLines = ExtractLinesFromWordFile(suggestionFilePath, encoding, allowBlankLines: true).ToList();

                if (suggestionFilePath.EndsWith("ph2.sug"))
                {
                    // NOTE: ph2.wrong does not have a corresponding blank suggestion in the file for rootforbiddenroot
                    suggestionLines.Insert(8, string.Empty);
                }

                if (suggestionFilePath.EndsWith("breakdefault.sug"))
                {
                    // No suggestions were added to compensate for new wrong words
                    suggestionLines.Add(string.Empty);
                }

                if (suggestionFilePath.EndsWith("checksharps.sug"))
                {
                    // No suggestions were added to compensate for new wrong words
                    suggestionLines.Add(string.Empty);
                }

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
        return Directory.GetFiles("files/", searchPattern).OrderBy(n => n);
    }

    protected static readonly char[] SpaceOrTab = [' ', '\t'];

    protected static IEnumerable<(string dictionaryPath, string word)> ToDictionaryWordTestData(string wordFilePath)
    {
        var dictionaryPath = Path.ChangeExtension(wordFilePath, "dic");

        return ExtractMultipleWordsFromWordFile(wordFilePath, Encoding.UTF8)
            .Distinct()
            .OrderBy(w => w, StringComparer.Ordinal)
            .Select(line => (dictionaryPath, line));
    }

    protected static IEnumerable<string> ExtractLinesFromWordFile(string filePath, Encoding encoding, bool allowBlankLines = false)
    {
        var results = File.ReadAllLines(filePath, encoding).Select(line => line.Trim(SpaceOrTab));

        if (!allowBlankLines)
        {
            results = results.Where(line => line.Length != 0);
        }

        return results;
    }

    protected static IEnumerable<string> ExtractMultipleWordsFromWordFile(string filePath, Encoding affixEncoding) =>
        ExtractLinesFromWordFile(filePath, affixEncoding)
            .SelectMany(line => line.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries));
}
