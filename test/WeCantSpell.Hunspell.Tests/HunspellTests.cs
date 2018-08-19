using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WeCantSpell.Hunspell.Tests.Infrastructure;
using Xunit;

namespace WeCantSpell.Hunspell.Tests
{
    public class HunspellTests
    {
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

                var actual = dictionary.Check(word);

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
                var dictionaryBuilder = new WordList.Builder();
                dictionaryBuilder.InitializeEntriesByRoot(1);
                dictionaryBuilder.Add(
                    dictionaryWord,
                    new WordEntryDetail(
                        FlagSet.Empty,
                        MorphSet.Empty,
                        WordEntryOptions.None));

                var dictionary = dictionaryBuilder.ToImmutable();

                var actual = dictionary.Check(searchWord);

                actual.Should().Be(expected);
            }

            public static IEnumerable<object[]> checking_large_word_does_not_cause_errors_args() =>
                GetAllDataFilePaths("*.dic").Select(filePath => new object[] { filePath });

            [Theory, MemberData(nameof(checking_large_word_does_not_cause_errors_args))]
            public async Task checking_large_word_does_not_cause_errors(string filePath)
            {
                // attampt to reproduce https://github.com/hunspell/hunspell/issues/446
                var largeInput = new string('X', 102);
                var dictionary = await WordList.CreateFromFilesAsync(filePath);

                var actual = dictionary.Check(largeInput);

                actual.Should().BeFalse();
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
                if (dictionaryFilePath.EndsWith("base_utf.dic") && word.Contains("İ"))
                {
                    // NOTE: These tests are bypassed because capitalization only works when the language is turkish and the UTF8 dic has no language applied
                    return;
                }

                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var checkResult = dictionary.Check(word);

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
                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var checkResult = dictionary.Check(word);

                checkResult.Should().BeFalse();
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
                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var actual = dictionary.Suggest(word);

                actual.Should().BeEmpty();
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
            [InlineData("files/rep.dic", "autos", new[] { "auto's", "auto" })]
            [InlineData("files/ngram_utf_fix.dic", "человеко", new[] { "человек" })]
            [InlineData("files/utf8_nonbmp.dic", "𐏑𐏒𐏒", new[] { "𐏑 𐏒𐏒", "𐏒𐏑", "𐏒𐏒" })]
            public async Task words_offer_specific_suggestions(string dictionaryFilePath, string word, string[] expectedSuggestions)
            {
                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var actual = dictionary.Suggest(word);

                actual.Should().NotBeNullOrEmpty();
                actual.ShouldBeEquivalentTo(expectedSuggestions);
            }

            [Theory]
            [InlineData("files/phone.dic", "Brasillian", new[] { "Brasilia", "Xxxxxxxxxx", "Brilliant", "Brazilian", "Brassily", "Brilliance" })]
            public async Task words_offer_at_least_suggestions_in_any_order(string dictionaryFilePath, string word, string[] expectedSuggestions)
            {
                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var actual = dictionary.Suggest(word);

                actual.Should().NotBeNullOrEmpty();
                actual.Should().Contain(expectedSuggestions);
            }

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
                var dictionary = await WordList.CreateFromFilesAsync(dictionaryFilePath);

                var actual = dictionary.Suggest(givenWord);

                actual.Should().NotBeNull();

                // ',' can either be a delimiter in the test data or part of the data
                var actualText = string.Join(", ", actual);
                var expectedText = string.Join(", ", expectedSuggestions);
                actualText.Should().Be(expectedText);
            }



            [Fact]
            public void untested_suggestion_files_should_not_be_found()
            {
                var untestedSets = GetSuggestionTestFileSets();

                untestedSets.Should().NotContain(s => s.WrongLines.Count != s.SuggestionLines.Count);
            }

            private static readonly HashSet<string> ExcludedSuggestionFiles = new HashSet<string>
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
                        throw new InvalidOperationException();
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

        protected static readonly char[] SpaceOrTab = { ' ', '\t' };

        protected static IEnumerable<object[]> ToDictionaryWordTestData(string wordFilePath)
        {
            var dictionaryPath = Path.ChangeExtension(wordFilePath, "dic");

            return ExtractMultipleWordsFromWordFile(wordFilePath, Encoding.UTF8)
                .Distinct()
                .OrderBy(w => w, StringComparer.Ordinal)
                .Select(line => new object[] { dictionaryPath, line });
        }

        protected static IEnumerable<string> ExtractLinesFromWordFile(string filePath, Encoding encoding, bool allowBlankLines = false)
        {
            var results = StaticEncodingLineReader.ReadLines(filePath, encoding)
                .Where(line => line != null)
                .Select(line => line.Trim(SpaceOrTab));

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
}
