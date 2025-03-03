using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        Helpers.EnsureEncodingsReady();
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
            var dictionary = new WordList.Builder().Build();

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

            var dictionary = dictionaryBuilder.Build();

            var actual = dictionary.Check(searchWord, TestCancellation);

            actual.ShouldBe(expected);
        }

        [Theory, ClassData(typeof(TestTheories.DicFilePathsData))]
        public async Task checking_large_word_does_not_cause_errors(string filePath)
        {
            // attampt to reproduce https://github.com/hunspell/hunspell/issues/446
            var largeInput = new string('X', 102);
            var dictionary = await DictionaryLoader.GetDictionaryAsync(filePath, TestCancellation);

            var actual = dictionary.Check(largeInput, TestCancellation);

            actual.ShouldBeFalse();
        }
    }

    public class CheckGoodWords : HunspellTests
    {
        [Theory, ClassData(typeof(TestTheories.GoodWordsData))]
        public async Task can_find_good_words_in_dictionary(string filePath, string word)
        {
            var dictionary = await DictionaryLoader.GetDictionaryAsync(filePath, TestCancellation);

            QueryOptions options;
            if (filePath.IndexOf("compound", StringComparison.OrdinalIgnoreCase) >= 0)
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

        [Theory, ClassData(typeof(TestTheories.GoodWordsData))]
        public async Task can_find_good_word_spans_in_dictionary(string filePath, string word)
        {
            var dictionary = await DictionaryLoader.GetDictionaryAsync(filePath, TestCancellation);

            QueryOptions options;
            if (filePath.IndexOf("compound", StringComparison.OrdinalIgnoreCase) >= 0)
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

            var checkResult = dictionary.Check(word.AsSpan(), TestCancellation);

            checkResult.ShouldBeTrue();
        }
    }

    public class CheckWrongWords : HunspellTests
    {
        [Theory, ClassData(typeof(TestTheories.WrongWordsData))]
        public async Task cant_find_wrong_words_in_dictionary(string filePath, string word)
        {
            var dictionary = await DictionaryLoader.GetDictionaryAsync(filePath, TestCancellation);

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
            var dictionary = await DictionaryLoader.GetDictionaryAsync("files/break.dic", TestCancellation);

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
            var dictionary = await DictionaryLoader.GetDictionaryAsync(dictionaryFilePath, TestCancellation);

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
            var dictionary = await DictionaryLoader.GetDictionaryAsync(dictionaryFilePath, TestCancellation);

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

            var dictionary = await DictionaryLoader.GetDictionaryAsync(dictionaryFilePath, TestCancellation);

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

        [Theory, ClassData(typeof(TestTheories.SuggestionData))]
        public async Task can_find_correct_best_suggestion(string name, string given, string[] expected)
        {
            var dictionaryFilePath = Path.ChangeExtension(name, "dic");

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

            var dictionary = await DictionaryLoader.GetDictionaryAsync(dictionaryFilePath, TestCancellation);

            var actual = dictionary.Suggest(given, options, TestCancellation);

            actual.ShouldNotBeNull();

            // ',' can either be a delimiter in the test data or part of the data
            var actualText = string.Join(", ", actual);
            var expectedText = string.Join(", ", expected);
            actualText.ShouldBe(expectedText);
        }
    }
}
