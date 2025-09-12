using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        int index;

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
                    index = span.IndexOf(']');
                    if (index >= 0)
                    {
                        span = span.Slice(0, index);
                        text = text.Slice(index + 2);
                    }
                    else
                    {
                        text = [];
                    }

                    if (span.Length > 0 && span[0] == '^')
                    {
                        conditions.Add(CharacterCondition.CreateCharSet(span.Slice(1), CharacterCondition.ModeKind.RestrictChars));
                    }
                    else
                    {
                        conditions.Add(CharacterCondition.CreateCharSet(span, CharacterCondition.ModeKind.PermitChars));
                    }

                    break;

                default:
#if HAS_SEARCHVALUES
                    index = text.IndexOfAny(ConditionParseStopCharacters);
#else
                    index = text.IndexOfAny('.', '[');
#endif

                    if (index >= 0)
                    {
                        span = text.Slice(0, index);
                        text = text.Slice(index);
                    }
                    else
                    {
                        span = text;
                        text = [];
                    }

                    conditions.Add(CharacterCondition.CreateSequence(span));

                    break;
            }
        }
        while (text.Length > 0);

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

    internal CharacterCondition[] RawArray => _items ?? [];

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

    public IEnumerator<CharacterCondition> GetEnumerator() => ((IEnumerable<CharacterCondition>)RawArray).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MatchesAnySingleCharacter => _items is { Length: 1 } && _items[0].MatchesAnySingleCharacter;

    public string GetEncoded() => string.Concat(RawArray.Select(static c => c.GetEncoded()));

    public override string ToString() => GetEncoded();

    /// <summary>
    /// Determines if the start of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the start of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsStartingMatch(ReadOnlySpan<char> text)
    {
        if (_items is not null)
        {
            foreach (var condition in _items)
            {
                var matchLength = condition.FullyMatchesFromStart(text);
                if (matchLength > 0)
                {
                    text = text.Slice(matchLength);
                }
                else
                {
                    goto exit;
                }

            }

            return true;
        }

    exit:
        return false;
    }

    /// <summary>
    /// Determines if the end of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the end of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsEndingMatch(ReadOnlySpan<char> text)
    {
        if (_items is not null)
        {
            for (var conditionIndex = _items.Length - 1; conditionIndex >= 0; conditionIndex--)
            {
                var matchLength = _items[conditionIndex].FullyMatchesFromEnd(text);
                if (matchLength > 0)
                {
                    text = text.Slice(0, text.Length - matchLength);
                }
                else
                {
                    goto exit;
                }
            }

            return true;
        }

    exit:
        return false;
    }

    public bool IsOnlyPossibleMatch(ReadOnlySpan<char> text)
    {
        if (_items is not null)
        {
            foreach (var condition in _items)
            {
                if (!condition.IsOnlyPossibleMatch(text, out var matchLength))
                {
                    return false;
                }

                text = text.Slice(matchLength);
            }
        }

        return text.IsEmpty;
    }
}
