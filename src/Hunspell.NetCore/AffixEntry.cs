using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    /// <summary>
    /// An affix is either a prefix or a suffix attached to root words to make other words.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Basically a Prefix or a Suffix is set of <see cref="AffixEntry"/> objects
    /// which store information about the prefix or suffix along
    /// with supporting routines to check if a word has a particular
    /// prefix or suffix or a combination.
    /// </para>
    /// <para>
    /// Zero stripping or affix are indicated by zero. Zero condition is indicated by dot.
    /// Condition is a simplified, regular expression-like pattern, which must be met
    /// before the affix can be applied. (Dot signs an arbitrary character.Characters in braces
    /// sign an arbitrary character from the character subset.Dash hasn't got special
    /// meaning, but circumflex(^) next the first brace sets the complementer character set.)
    /// </para>
    /// </remarks>
    /// <seealso cref="PrefixEntry"/>
    /// <seealso cref="SuffixEntry"/>
    public abstract class AffixEntry
    {
        protected AffixEntry()
        {
        }

        /// <summary>
        /// The number of conditions that must be met.
        /// </summary>
        [Obsolete("This should probably be moved to a different data structure.")]
        public int ConditionCount { get; private set; }

        /// <summary>
        /// Optional morphological fields separated by spaces or tabulators.
        /// </summary>
        public string MorphCode { get; private set; }

        /// <summary>
        /// Encodes the conditions to be met.
        /// </summary>
        public string ConditionText { get; private set; }

        /// <summary>
        /// The affix string to add.
        /// </summary>
        /// <remarks>
        /// Affix (optionally with flags of continuation classes, separated by a slash).
        /// </remarks>
        public string Append { get; private set; }

        /// <summary>
        /// String to strip before adding affix.
        /// </summary>
        /// <remarks>
        /// Stripping characters from beginning (at prefix rules) or
        /// end(at suffix rules) of the word.
        /// </remarks>
        public string Strip { get; private set; }

        public ImmutableArray<int> ContClass { get; private set; }

        public abstract string Key { get; }

        public static TEntry Create<TEntry>(
            string strip,
            string affixText,
            string conditionText,
            string morph,
            IEnumerable<int> contClass
        )
            where TEntry : AffixEntry, new()
        {
            return new TEntry
            {
                Strip = strip,
                Append = affixText,
                ConditionText = conditionText,
                MorphCode = morph,
                ContClass = contClass.ToImmutableArray()
            };
        }
    }
}
