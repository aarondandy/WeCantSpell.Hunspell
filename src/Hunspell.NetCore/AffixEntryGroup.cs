using System.Collections.Generic;

namespace Hunspell
{
    /// <summary>
    /// Contains a set of <see cref="AffixEntry"/> instances based on the same <see cref="AFlag"/>.
    /// </summary>
    /// <typeparam name="TEntry">The specific entry type.</typeparam>
    public sealed class AffixEntryGroup<TEntry>
        where TEntry : AffixEntry
    {
        public AffixEntryGroup(FlagValue aFlag, AffixEntryOptions options, AffixEntryCollection<TEntry> entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = entries;
        }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public AffixEntryCollection<TEntry> Entries { get; }

        /// <summary>
        /// Indicates if a group has the <see cref="AffixEntryOptions.CrossProduct"/> option enabled.
        /// </summary>
        /// <seealso cref="AffixEntryOptions"/>
        public bool AllowCross => Options.HasFlag(AffixEntryOptions.CrossProduct);
    }

    public static class AffixEntryGroup
    {
        public sealed class Builder<TEntry>
            where TEntry : AffixEntry
        {
            /// <summary>
            /// ID used to represent the affix group.
            /// </summary>
            public FlagValue AFlag { get; set; }

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
                return new AffixEntryGroup<TEntry>(AFlag, Options, AffixEntryCollection<TEntry>.Create(Entries));
            }
        }
    }
}
