using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterConditionGroup : IReadOnlyList<CharacterCondition>
{
    public static readonly CharacterConditionGroup Empty = new(Array.Empty<CharacterCondition>());

    public static readonly CharacterConditionGroup AllowAnySingleCharacter = Create(CharacterCondition.AllowAny);

    public static CharacterConditionGroup Create(CharacterCondition condition) => new(new[] { condition });
    public static CharacterConditionGroup Create(IEnumerable<CharacterCondition> conditions) =>
        new((conditions ?? throw new ArgumentNullException(nameof(conditions))).ToArray());

    public static CharacterConditionGroup Parse(string text)
    {
        if (text is null) throw new ArgumentNullException(nameof(text));

        return Parse(text.AsSpan());
    }

    public static CharacterConditionGroup Parse(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        ReadOnlySpan<char> span;
        var conditions = new List<CharacterCondition>();

        do
        {
            switch (text[0])
            {
                case '.':
                    conditions.Add(CharacterCondition.AllowAny);
                    text = text.Slice(1);

                    break;

                case '[':
                    span = text.Slice(1);
                    var closeIndex = span.IndexOf(']');
                    if (closeIndex >= 0)
                    {
                        span = span.Slice(0, closeIndex);
                        text = text.Slice(closeIndex + 2);
                    }
                    else
                    {
                        text = ReadOnlySpan<char>.Empty;
                    }

                    var restricted = span.Length > 0 && span[0] == '^';
                    if (restricted)
                    {
                        span = span.Slice(1);
                    }
                    conditions.Add(CharacterCondition.CreateCharSet(span, restricted: restricted));

                    break;

                default:
                    var stopIndex = text.IndexOfAny('.', '[');
                    span = stopIndex < 0 ? text : text.Slice(0, stopIndex);
                    text = text.Slice(span.Length);

                    conditions.Add(CharacterCondition.CreateSequence(span));

                    break;
            }
        }
        while (!text.IsEmpty);

        return Create(conditions);
    }

    internal CharacterConditionGroup(CharacterCondition[] items)
    {
#if DEBUG
        if (items is null) throw new ArgumentNullException(nameof(items));
#endif
        _items = items;
    }

    private readonly CharacterCondition[] _items;

    public int Count => _items.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _items is { Length: > 0 };
    public CharacterCondition this[int index] => _items[index];
    public IEnumerator<CharacterCondition> GetEnumerator() => ((IEnumerable<CharacterCondition>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

    public bool MatchesAnySingleCharacter => HasItems && _items.Length == 1 && _items[0].MatchesAnySingleCharacter;

    public string GetEncoded() => string.Concat(_items.Select(c => c.GetEncoded()));

    public override string ToString() => GetEncoded();

    /// <summary>
    /// Determines if the start of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the start of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsStartingMatch(ReadOnlySpan<char> text)
    {
        if (IsEmpty)
        {
            return false;
        }

        foreach (var condition in _items)
        {
            if (!condition.FullyMatchesFromStart(text, out var matchLength))
            {
                return false;
            }

            text = text.Slice(matchLength);
        }

        return true;
    }

    /// <summary>
    /// Determines if the end of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the end of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsEndingMatch(ReadOnlySpan<char> text)
    {
        if (IsEmpty)
        {
            return false;
        }

        for (var conditionIndex = _items.Length - 1; conditionIndex >= 0; conditionIndex--)
        {
            if (!_items[conditionIndex].FullyMatchesFromEnd(text, out var matchLength))
            {
                return false;
            }

            text = text.Slice(0, text.Length - matchLength);
        }

        return true;
    }

    public bool IsOnlyPossibleMatch(ReadOnlySpan<char> text)
    {
        foreach (var condition in _items)
        {
            if (!condition.IsOnlyPossibleMatch(text, out var matchLength))
            {
                return false;
            }

            text = text.Slice(matchLength);
        }

        return text.IsEmpty;
    }

    public sealed class Comparer : IEqualityComparer<CharacterConditionGroup>
    {
        public static Comparer Instance { get; } = new();

        private Comparer()
        {
        }

        public bool Equals(CharacterConditionGroup x, CharacterConditionGroup y) =>
            x._items.SequenceEqual(y._items);

        public int GetHashCode(CharacterConditionGroup obj) => 
            ((IStructuralEquatable)obj._items).GetHashCode(EqualityComparer<CharacterCondition>.Default);
    }
}
