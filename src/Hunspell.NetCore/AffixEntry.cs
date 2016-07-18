using System;
using System.Collections.Immutable;

namespace Hunspell
{
    /// <summary>
    /// An affix is either a prefix or a suffix attached to root words to make other words.
    /// </summary>
    /// <remarks>
    /// Basically a Prefix or a Suffix is set of AffEntry objects
    /// which store information about the prefix or suffix along
    /// with supporting routines to check if a word has a particular
    /// prefix or suffix or a combination.
    /// </remarks>
    public abstract class AffixEntry
    {
        protected AffixEntry()
        {
        }

        /// <summary>
        /// The number of conditions that must be met.
        /// </summary>
        [Obsolete("This should probably be moved to a different data structure.")]
        public int ConditionCount { get; set; }

        public string MorphCode { get; set; }

        /// <summary>
        /// Encodes the conditions to be met.
        /// </summary>
        public string ConditionText { get; set; }

        /// <summary>
        /// The affix string to add.
        /// </summary>
        public string Append { get; set; }

        /// <summary>
        /// String to strip before adding affix.
        /// </summary>
        public string Strip { get; set; }

        public abstract string Key { get; }

        public ImmutableList<int> ContClass { get; set; }
    }
}
