using System;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace WeCantSpell.Hunspell.Tests;

public class ArrayBuilderTests
{
    public class RemoveTests
    {
        [Fact]
        public void remove_from_empty_throws()
        {
            var sut = new ArrayBuilder<int>();

            Should.Throw<ArgumentOutOfRangeException>(() => sut.RemoveAt(0));
        }

        [Fact]
        public void can_remove_first()
        {
            var sut = new ArrayBuilder<int> { 1, 2, 3 };

            sut.Remove(1);

            sut.ShouldBe([2, 3]);
        }

        [Fact]
        public void can_remove_middle()
        {
            var sut = new ArrayBuilder<int> { 1, 2, 3 };

            sut.Remove(2);

            sut.ShouldBe([1, 3]);
        }

        [Fact]
        public void can_remove_end()
        {
            var sut = new ArrayBuilder<int> { 1, 2, 3 };

            sut.Remove(3);

            sut.ShouldBe([1, 2]);
        }
    }

    public class AddRange
    {
        [Fact]
        public void can_add_multiple_from_enumerable()
        {
            var sut = new ArrayBuilder<int> { 1, 2 };

            sut.AddRange((IEnumerable<int>)[3, 4, 5]);

            sut.ShouldBe([1, 2, 3, 4, 5]);
        }

        [Fact]
        public void can_add_multiple_from_array()
        {
            var sut = new ArrayBuilder<int> { 1, 2 };

            sut.AddRange((int[])[3, 4, 5]);

            sut.ShouldBe([1, 2, 3, 4, 5]);
        }

        [Fact]
        public void can_add_multiple_from_list()
        {
            var sut = new ArrayBuilder<int> { 1, 2 };

            sut.AddRange(new List<int> { 3, 4, 5 });

            sut.ShouldBe([1, 2, 3, 4, 5]);
        }
    }

    public class Indexer
    {
        [Fact]
        public void can_retrieve_values_from_index()
        {
            var sut = new ArrayBuilder<char> { 'a', 'b', 'c' };

            Enumerable.ToArray(sut).ShouldBe(['a', 'b', 'c']);
        }

        [Fact]
        public void cant_access_deleted_item()
        {
            var sut = new ArrayBuilder<char> { 'a', 'b', 'c', 'd' };
            sut.RemoveAt(3);

            Should.Throw<ArgumentOutOfRangeException>(() => sut[3].ShouldBe('d'));
        }

        [Fact]
        public void can_mutate_value()
        {
            var sut = new ArrayBuilder<char> { 'a', 'b', 'c' };
            sut[1] = 'x';

            Enumerable.ToArray(sut).ShouldBe(['a', 'x', 'c']);
        }
    }
}
