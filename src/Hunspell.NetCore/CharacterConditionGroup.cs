using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hunspell
{
    public class CharacterConditionGroup : IReadOnlyList<CharacterCondition>
    {
        public static readonly CharacterConditionGroup Empty = new CharacterConditionGroup(ImmutableArray<CharacterCondition>.Empty);

        public static readonly CharacterConditionGroup AllowAnySingleCharacter = new CharacterConditionGroup(ImmutableArray.Create(CharacterCondition.AllowAny));

        public CharacterConditionGroup(ImmutableArray<CharacterCondition> conditions)
        {
            Conditions = conditions;
        }

        public CharacterConditionGroup(IEnumerable<CharacterCondition> conditions)
            : this(conditions.ToImmutableArray())
        {
        }

        private ImmutableArray<CharacterCondition> Conditions { get; }

        public int Count => Conditions.Length;

        public CharacterCondition this[int index] => Conditions[index];

        public bool AllowsAnySingleCharacter => Conditions.Length == 1 && Conditions[0].AllowsAny;

        public IEnumerator<CharacterCondition> GetEnumerator()
        {
            return ((IEnumerable<CharacterCondition>)Conditions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Conditions).GetEnumerator();
        }

        public string GetEncoded()
        {
            return string.Concat(Conditions.Select(c => c.GetEncoded()));
        }

        public override string ToString()
        {
            return GetEncoded();
        }

        /// <summary>
        /// Determines if the start of the given <paramref name="text"/> matches the <see cref="Conditions"/>.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True when the start of the <paramref name="text"/> is matched by the <see cref="Conditions"/>.</returns>
        public bool IsStartingMatch(string text)
        {
            if (string.IsNullOrEmpty(text) || Conditions.Length < text.Length)
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (!Conditions[i].IsMatch(text[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if the end of the given <paramref name="text"/> matches the <see cref="Conditions"/>.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True when the end of the <paramref name="text"/> is matched by the <see cref="Conditions"/>.</returns>
        public bool IsEndingMatch(string text)
        {
            if (string.IsNullOrEmpty(text) || Conditions.Length < text.Length)
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                var textIndex = text.Length - i - 1;
                var conditionIndex = Conditions.Length - i - 1;

                if (!Conditions[conditionIndex].IsMatch(text[textIndex]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOnlyPossibleMatch(string text)
        {
            if (string.IsNullOrEmpty(text) || Conditions.Length != text.Length)
            {
                return false;
            }

            for (var i = 0; i < text.Length; i++)
            {
                var condition = Conditions[i];
                if (!condition.PermitsSingleCharacter || condition.Characters[0] != text[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
