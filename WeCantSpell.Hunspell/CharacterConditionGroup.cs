using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterConditionGroup : IReadOnlyList<CharacterCondition>
{
    public static readonly CharacterConditionGroup Empty = new(ImmutableArray<CharacterCondition>.Empty);

    public static readonly CharacterConditionGroup AllowAnySingleCharacter = new(ImmutableArray.Create(CharacterCondition.AllowAny));

    internal CharacterConditionGroup(ImmutableArray<CharacterCondition> items)
    {
#if DEBUG
        if (items.IsDefault) throw new ArgumentOutOfRangeException(nameof(items));
#endif
        _items = items;
    }

    private readonly ImmutableArray<CharacterCondition> _items;

    public int Count => _items.Length;
    public bool IsEmpty => _items.IsEmpty;
    public bool HasItems => !IsEmpty;
    public CharacterCondition this[int index] => _items[index];

    public ImmutableArray<CharacterCondition>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<CharacterCondition> IEnumerable<CharacterCondition>.GetEnumerator() => ((IEnumerable<CharacterCondition>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

    public bool AllowsAnySingleCharacter => _items.Length == 1 && _items[0].AllowsAny;

    public string GetEncoded()
    {
        return string.Concat(_items.Select(c => c.GetEncoded()));
    }

    public override string ToString() => GetEncoded();

    /// <summary>
    /// Determines if the start of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the start of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsStartingMatch(string text)
    {
        if (string.IsNullOrEmpty(text) || _items.Length > text.Length)
        {
            return false;
        }

        for (var i = 0; i < _items.Length; i++)
        {
            if (!_items[i].IsMatch(text[i]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines if the end of the given <paramref name="text"/> matches the conditions.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True when the end of the <paramref name="text"/> is matched by the conditions.</returns>
    public bool IsEndingMatch(string text)
    {
        if (_items.Length > text.Length)
        {
            return false;
        }

        for (int conditionIndex = _items.Length - 1, textIndex = text.Length - 1; conditionIndex >= 0; conditionIndex--, textIndex--)
        {
            if (!_items[conditionIndex].IsMatch(text[textIndex]))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsOnlyPossibleMatch(string text)
    {
        if (string.IsNullOrEmpty(text) || _items.Length != text.Length)
        {
            return false;
        }

        for (var i = 0; i < text.Length; i++)
        {
            var condition = _items[i];
            if (!condition.PermitsSingleCharacter || condition.Characters[0] != text[i])
            {
                return false;
            }
        }

        return true;
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
