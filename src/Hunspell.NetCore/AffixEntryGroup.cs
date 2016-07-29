using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    /// <summary>
    /// Contains a set of <see cref="AffixEntry"/> instances based on the same <see cref="AFlag"/>.
    /// </summary>
    /// <typeparam name="TEntry">The specific entry type.</typeparam>
    public class AffixEntryGroup<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryGroup(int aFlag, AffixEntryOptions options, IEnumerable<TEntry> entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = ImmutableArray.CreateRange(entries);
        }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public int AFlag { get; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public ImmutableArray<TEntry> Entries { get; }
    }

    public static class AffixEntryGroup
    {
        public class Builder<TEntry>
            where TEntry: AffixEntry
        {
            /// <summary>
            /// ID used to represent the affix group.
            /// </summary>
            public int AFlag { get; set; }

            /// <summary>
            /// Options for this affix group.
            /// </summary>
            public AffixEntryOptions Options { get; set; }

            /// <summary>
            /// All of the entries that make up this group.
            /// </summary>
            public List<TEntry> Entries { get; set; }

            public AffixEntryGroup<TEntry> ToGroup()
            {
                return new AffixEntryGroup<TEntry>(AFlag, Options, Entries);
            }
        }
    }
}
