using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class TextDictionaryTests
{
    [Fact]
    public void default_is_empty()
    {
        var dic = new TextDictionary<int>();

        dic.ShouldBeEmpty();
        dic.Capacity.ShouldBe(0);
    }

    [Fact]
    public void preallocated_can_be_empty_too()
    {
        var dic = new TextDictionary<int>(10);

        dic.ShouldBeEmpty();
        dic.Capacity.ShouldBe(10);
    }

    [Fact]
    public void try_get_empty_is_false()
    {
        var dic = new TextDictionary<int>();
        dic.Capacity.ShouldBe(0);

        dic.TryGetValue("word", out _).ShouldBeFalse();
    }

    [Fact]
    public void try_get_empty_span_is_false()
    {
        var dic = new TextDictionary<int>();
        dic.Capacity.ShouldBe(0);

        dic.TryGetValue("word".AsSpan(), out _).ShouldBeFalse();
    }

    [Fact]
    public void get_empty_throws_key_not_found_operation()
    {
        var dic = new TextDictionary<int>();
        dic.Capacity.ShouldBe(0);

        Should.Throw<KeyNotFoundException>(() => { var actual = dic["word"]; });
    }

    [Fact]
    public void can_add_to_empty()
    {
        var dic = new TextDictionary<int>();

        dic.Add("word", 3);

        dic.Count.ShouldBe(1);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(1);
        dic.Capacity.ShouldBeLessThanOrEqualTo(4);
        var actual = dic.ShouldHaveSingleItem();
        actual.Key.ShouldBe("word");
        actual.Value.ShouldBe(3);
    }

    [Fact]
    public void can_get_single_from_string_key()
    {
        var dic = new TextDictionary<int>();
        dic.Add("word", 3);

        dic["word"].ShouldBe(3);
    }

    [Fact]
    public void can_try_get_single_from_string_key()
    {
        var dic = new TextDictionary<int>();
        dic.Add("word", 3);

        dic.TryGetValue("word", out var actual).ShouldBeTrue();
        actual.ShouldBe(3);
    }

    [Fact]
    public void can_try_get_single_from_span_key()
    {
        var dic = new TextDictionary<int>();
        dic.Add("word", 3);

        dic.TryGetValue("word".AsSpan(), out var actual).ShouldBeTrue();
        actual.ShouldBe(3);
    }

    [Fact]
    public void can_add_four_sequential_items()
    {
        var dic = new TextDictionary<int>();

        dic.Add("word1", 1);
        dic.Add("word2", 2);
        dic.Add("word3", 3);
        dic.Add("word4", 4);

        dic.Count.ShouldBe(4);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(4);
        dic.Capacity.ShouldBeLessThanOrEqualTo(8);
        dic.ShouldBe(
        [
            new("word1", 1),
            new("word2", 2),
            new("word3", 3),
            new("word4", 4)
        ], ignoreOrder: true);
    }

    [Fact]
    public void can_set_to_add_to_empty()
    {
        var dic = new TextDictionary<int>();

        dic["word"] = 5;

        dic.Count.ShouldBe(1);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(1);
        dic.Capacity.ShouldBeLessThanOrEqualTo(4);
        var actual = dic.ShouldHaveSingleItem();
        actual.Key.ShouldBe("word");
        actual.Value.ShouldBe(5);
    }

    [Fact]
    public void can_set_to_overwrite_single()
    {
        var dic = new TextDictionary<int>();
        dic.Add("word", 3);

        dic["word"] = 5;

        dic.Count.ShouldBe(1);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(1);
        dic.Capacity.ShouldBeLessThanOrEqualTo(4);
        var actual = dic.ShouldHaveSingleItem();
        actual.Key.ShouldBe("word");
        actual.Value.ShouldBe(5);
    }

    [Fact]
    public void can_add_100_items()
    {
        var dic = new TextDictionary<int>();

        for (var i = 1; i <= 100; i++)
        {
            dic[$"word{i}"] = i;
        }

        dic.Count.ShouldBe(100);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(100);
        dic.Capacity.ShouldBeLessThanOrEqualTo(200);

        for (var i = 1; i <= 100; i++)
        {
            dic.ShouldContain(new KeyValuePair<string, int>($"word{i}", i));
        }

        dic.ShouldNotContainKey("word0");
        dic.ShouldNotContainKey("word101");
    }

    [Fact]
    public void double_add_same_key_fails()
    {
        var dic = new TextDictionary<int>();
        dic.Add("word", 2);

        Should.Throw<InvalidOperationException>(() => dic.Add("word", 9));

        dic.Count.ShouldBe(1);
        dic.Capacity.ShouldBeGreaterThanOrEqualTo(1);
        dic.Capacity.ShouldBeLessThanOrEqualTo(4);
        var actual = dic.ShouldHaveSingleItem();
        actual.Key.ShouldBe("word");
        actual.Value.ShouldBe(2);
    }

    [Fact]
    public void clear_empty_does_nothing()
    {
        var dic = new TextDictionary<int>();

        dic.Clear();

        dic.ShouldBeEmpty();
    }

    [Fact]
    public void can_clear_after_add()
    {
        var dic = new TextDictionary<int>
        {
            { "word1", 1 },
            { "word2", 2 },
            { "word3", 3 },
            { "word4", 4 }
        };

        dic.Clear();

        dic.ShouldBeEmpty();
    }

    [Fact]
    public void can_readd_after_clear()
    {
        var dic = new TextDictionary<int>(4)
        {
            { "word1", 1 },
            { "word2", 2 },
            { "word3", 3 },
            { "word4", 4 }
        };
        var originalCapacity = dic.Capacity;
        dic.Clear();

        dic.Add("word1", 1);
        dic.Add("word2", 2);
        dic.Add("word3", 3);
        dic.Add("word4", 4);

        dic.ShouldBe(
        [
            new("word1", 1),
            new("word2", 2),
            new("word3", 3),
            new("word4", 4)
        ], ignoreOrder: true);
        dic.Capacity.ShouldBe(originalCapacity, "The existing storage should be used");
    }

    [Fact]
    public void remove_on_empty_does_nothing()
    {
        var dic = new TextDictionary<int>();

        dic.Remove("not-found");

        dic.ShouldBeEmpty();
    }

    [Fact]
    public void can_remove_one_of_many()
    {
        var dic = new TextDictionary<int>
        {
            { "word1", 1 },
            { "word2", 2 },
            { "word3", 3 },
            { "word4", 4 }
        };

        dic.Remove("word3").ShouldBeTrue();

        dic.ShouldBe(
        [
            new("word1", 1),
            new("word2", 2),
            new("word4", 4)
        ], ignoreOrder: true);
    }

    [Fact]
    public void removing_not_found_does_nothing()
    {
        var dic = new TextDictionary<int>
        {
            { "word1", 1 },
            { "word2", 2 },
            { "word3", 3 },
            { "word4", 4 }
        };

        dic.Remove("not-found").ShouldBeFalse();

        dic.ShouldBe(
        [
            new("word1", 1),
            new("word2", 2),
            new("word3", 3),
            new("word4", 4)
        ], ignoreOrder: true);
    }

    [Fact]
    public void can_readd_after_remove()
    {
        var dic = new TextDictionary<int>(4)
        {
            { "word1", 1 },
            { "word2", 2 },
            { "word3", 3 },
            { "word4", 4 }
        };
        var originalCapacity = dic.Capacity;
        dic.Remove("word1");
        dic.Remove("word2");
        dic.Remove("word3");
        dic.Remove("word4");

        dic.Add("word1", 1);
        dic.Add("word2", 2);
        dic.Add("word3", 3);
        dic.Add("word4", 4);

        dic.ShouldBe(
        [
            new("word1", 1),
            new("word2", 2),
            new("word3", 3),
            new("word4", 4)
        ], ignoreOrder: true);
        dic.Capacity.ShouldBe(originalCapacity, "The existing storage should be used");
    }

    [Fact]
    public void remove_for_missing_with_hash_not_found_does_nothing()
    {
        // This test is important to handle cases where there is not hash match on a missing key

        // arrange
        var dic = new TextDictionary<int>(5);
        dic.HashSpace.ShouldBeGreaterThan(0u);
        const string knownWord = "word";
        var hashHashTarget = StringEx.GetStableOrdinalHashCode(knownWord) % dic.HashSpace;
        dic.Add(knownWord, 0);
        var random = new Random(123);
        string unknownWord;
        do
        {
            unknownWord = random.Next().ToString(CultureInfo.InvariantCulture);
            if (unknownWord != knownWord)
            {
                var wordHash = StringEx.GetStableOrdinalHashCode(unknownWord) % dic.HashSpace;
                if (wordHash != hashHashTarget)
                {
                    break;
                }
            }

            TestContext.Current.CancellationToken.ThrowIfCancellationRequested();
        }
        while (true);

        // act & assert
        dic.Remove(unknownWord).ShouldBeFalse();
    }

    [Fact]
    public void can_remove_all_when_all_hashs_collide()
    {
        // This test was really annoying to create, but is important!
        // It's goal is to provide test coverage for add and remove
        // when there are hash collisions.

        // arrange
        var dic = new TextDictionary<int>(5);
        dic.HashSpace.ShouldBeGreaterThan(0u);
        var givenWords = new List<string>(dic.Capacity);
        const string startWord = "word";
        givenWords.Add("word");
        var hashHashTarget = StringEx.GetStableOrdinalHashCode(startWord) % dic.HashSpace;
        var random = new Random(123);
        givenWords.AddRange(generateCollisionWords(dic.Capacity - givenWords.Count, w => w != startWord));
        givenWords.Count.ShouldBeGreaterThan(2);
        var otherWords = generateCollisionWords(3, w => !givenWords.Contains(w));

        foreach (var toAdd in givenWords)
        {
            dic.Add(toAdd, 0);
            dic.ContainsKey(toAdd).ShouldBeTrue(customMessage: "must be able to add entry with hash collision");
        }

        // act (and some assert)

        foreach (var notFound in otherWords)
        {
            dic.Remove(notFound).ShouldBeFalse(customMessage: "must be able to gracefully handle not found on hash collision");
        }

        dic.Remove(givenWords.First()).ShouldBeTrue(customMessage: "must be able to remove the first entry");
        dic.Remove(givenWords.Last()).ShouldBeTrue(customMessage: "must be able to remove the last entry");

        foreach (var toRemove in givenWords.Skip(1).Take(givenWords.Count - 2))
        {
            dic.Remove(toRemove).ShouldBeTrue(customMessage: "must be able to remove other entries");
        }

        // assert

        dic.Count.ShouldBe(0);
        dic.ShouldBeEmpty();

        // helpers

        List<string> generateCollisionWords(int count, Func<string, bool> predicate)
        {
            var results = new List<string>();
            while (results.Count < count)
            {
                TestContext.Current.CancellationToken.ThrowIfCancellationRequested();
                var word = random.Next().ToString(CultureInfo.InvariantCulture);
                if (predicate(word))
                {
                    var wordHash = StringEx.GetStableOrdinalHashCode(word) % dic.HashSpace;
                    if (wordHash == hashHashTarget)
                    {
                        results.Add(word);
                    }
                }
            }

            return results;
        }
    }
}
