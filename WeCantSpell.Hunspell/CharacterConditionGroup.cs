using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterConditionGroup : IReadOnlyList<CharacterCondition>
{
#if HAS_SEARCHVALUES
    private static readonly System.Buffers.SearchValues<char> ConditionParseStopCharacters = System.Buffers.SearchValues.Create(".[");
#endif

    public static readonly CharacterConditionGroup Empty = new([]);

    public static readonly CharacterConditionGroup AllowAnySingleCharacter = Create(CharacterCondition.AllowAny);

    public static CharacterConditionGroup Create(CharacterCondition condition) => new([condition]);

    public static CharacterConditionGroup Create(IEnumerable<CharacterCondition> conditions)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(conditions);
#else
        ExceptionEx.ThrowIfArgumentNull(conditions, nameof(conditions));
#endif

        return new(conditions.ToArray());
    }

    public static CharacterConditionGroup Parse(string text)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(text);
#else
        ExceptionEx.ThrowIfArgumentNull(text, nameof(text));
#endif

        return Parse(text.AsSpan());
    }

    public static CharacterConditionGroup Parse(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
        {
            return Empty;
        }

        ReadOnlySpan<char> span;
        var conditions = ArrayBuilder<CharacterCondition>.Pool.Get();

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
                        text = [];
                    }

                    var restricted = span.Length > 0 && span[0] == '^';
                    if (restricted)
                    {
                        span = span.Slice(1);
                    }
                    conditions.Add(CharacterCondition.CreateCharSet(span, restricted: restricted));

                    break;

                default:
#if HAS_SEARCHVALUES
                    var stopIndex = text.IndexOfAny(ConditionParseStopCharacters);
#else
                    var stopIndex = text.IndexOfAny('.', '[');
#endif
                    span = stopIndex < 0 ? text : text.Slice(0, stopIndex);
                    text = text.Slice(span.Length);

                    conditions.Add(CharacterCondition.CreateSequence(span));

                    break;
            }
        }
        while (!text.IsEmpty);

        return new(ArrayBuilder<CharacterCondition>.Pool.ExtractAndReturn(conditions));
    }

    internal CharacterConditionGroup(CharacterCondition[] items)
    {
        _items = items;
    }

    private readonly CharacterCondition[]? _items;

    public int Count => _items is not null ? _items.Length : 0;
    public bool IsEmpty => _items is not { Length: > 0 };
    public bool HasItems => _items is { Length: > 0 };
    public CharacterCondition this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif
            return _items![index];
        }
    }

    public IEnumerator<CharacterCondition> GetEnumerator() => ((IEnumerable<CharacterCondition>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal CharacterCondition[] GetInternalArray() => _items ?? [];

    public bool MatchesAnySingleCharacter => _items is { Length: 1 } && _items[0].MatchesAnySingleCharacter;

    public string GetEncoded() => string.Concat(GetInternalArray().Select(c => c.GetEncoded()));

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

        foreach (var condition in _items!)
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

        for (var conditionIndex = _items!.Length - 1; conditionIndex >= 0; conditionIndex--)
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
        foreach (var condition in GetInternalArray())
        {
            if (!condition.IsOnlyPossibleMatch(text, out var matchLength))
            {
                return false;
            }

            text = text.Slice(matchLength);
        }

        return text.IsEmpty;
    }
}
