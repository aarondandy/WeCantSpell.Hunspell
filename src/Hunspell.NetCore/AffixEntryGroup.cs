using System;
using System.Collections.Generic;

namespace Hunspell
{
    public class AffixEntryGroup<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryGroup(char aFlag, AffixEntryOptions options, int expectedEntryCount)
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
        public char AFlag { get; }

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
