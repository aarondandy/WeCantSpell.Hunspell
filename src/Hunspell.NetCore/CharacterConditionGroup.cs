using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CharacterConditionGroup : IReadOnlyList<CharacterCondition>
    {
        public static readonly CharacterConditionGroup Empty = CharacterConditionGroup.TakeArray(ArrayEx<CharacterCondition>.Empty);

        public static readonly CharacterConditionGroup AllowAnySingleCharacter = CharacterConditionGroup.Create(CharacterCondition.AllowAny);

        private CharacterConditionGroup(CharacterCondition[] conditions)
        {
            this.conditions = conditions;
        }

        private readonly CharacterCondition[] conditions;

        public int Count => conditions.Length;

        public CharacterCondition this[int index] => conditions[index];

        public bool AllowsAnySingleCharacter => conditions.Length == 1 && conditions[0].AllowsAny;

        internal static CharacterConditionGroup TakeArray(CharacterCondition[] conditions) => new CharacterConditionGroup(conditions);

        public static CharacterConditionGroup Create(CharacterCondition condition) => TakeArray(new[] { condition });

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<CharacterCondition> GetEnumerator() => new FastArrayEnumerator<CharacterCondition>(conditions);

        IEnumerator<CharacterCondition> IEnumerable<CharacterCondition>.GetEnumerator() => ((IEnumerable<CharacterCondition>)conditions).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => conditions.GetEnumerator();

        public string GetEncoded() => string.Concat(conditions.Select(c => c.GetEncoded()));

        public override string ToString() => GetEncoded();

        /// <summary>
        /// Determines if the start of the given <paramref name="text"/> matches the conditions.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True when the start of the <paramref name="text"/> is matched by the conditions.</returns>
        public bool IsStartingMatch(string text)
        {
            if (string.IsNullOrEmpty(text) || conditions.Length > text.Length)
            {
                return false;
            }

            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].IsMatch(text[i]))
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
            if (conditions.Length > text.Length)
            {
                return false;
            }

            for (int conditionIndex = conditions.Length - 1, textIndex = text.Length - 1; conditionIndex >= 0; conditionIndex--, textIndex--)
            {
                if (!conditions[conditionIndex].IsMatch(text[textIndex]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOnlyPossibleMatch(string text)
        {
            if (string.IsNullOrEmpty(text) || conditions.Length != text.Length)
            {
                return false;
            }

            for (var i = 0; i < text.Length; i++)
            {
                var condition = conditions[i];
                if (!condition.PermitsSingleCharacter || condition.Characters[0] != text[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
