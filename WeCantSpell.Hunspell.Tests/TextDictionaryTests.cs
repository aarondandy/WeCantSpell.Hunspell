using System;
using System.Collections.Generic;

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
}
