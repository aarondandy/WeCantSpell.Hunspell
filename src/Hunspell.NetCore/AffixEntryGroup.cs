using System;
using System.Collections.Generic;

namespace Hunspell
{
    /// <summary>
    /// Contains a set of <see cref="AffixEntry"/> instances based on the same <see cref="AFlag"/>.
    /// </summary>
    /// <typeparam name="TEntry">The specific entry type.</typeparam>
    public class AffixEntryGroup<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryGroup(int aFlag, AffixEntryOptions options, int expectedEntryCount)
        {
            AFlag = aFlag;
            Options = options;
            Entries = expectedEntryCount > 0 ? new List<TEntry>(expectedEntryCount) : new List<TEntry>();
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public List<TEntry> Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public int AFlag { get; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; }

        [Obsolete("May be unused")]
        public bool AllowCross
        {
            get
            {
                return Options.HasFlag(AffixEntryOptions.CrossProduct);
            }
        }
    }
}
