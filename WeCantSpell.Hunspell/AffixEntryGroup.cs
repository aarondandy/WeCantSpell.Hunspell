using System;
using System.Collections.Generic;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

/// <summary>
/// Contains a set of <see cref="AffixEntry"/> instances based on the same <see cref="AFlag"/>.
/// </summary>
/// <typeparam name="TEntry">The specific entry type.</typeparam>
public sealed class AffixEntryGroup<TEntry> where TEntry : AffixEntry
{
    public AffixEntryGroup(FlagValue aFlag, AffixEntryOptions options, AffixEntryCollection<TEntry> entries)
    {
        Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    public AffixEntryCollection<TEntry> Entries { get; }

    /// <summary>
    /// ID used to represent the affix group.
    /// </summary>
    public FlagValue AFlag { get; }

    /// <summary>
    /// Options for this affix group.
    /// </summary>
    public AffixEntryOptions Options { get; }

    /// <summary>
    /// Indicates if a group has the <see cref="AffixEntryOptions.CrossProduct"/> option enabled.
    /// </summary>
    /// <seealso cref="AffixEntryOptions"/>
    public bool AllowCross => EnumEx.HasFlag(Options, AffixEntryOptions.CrossProduct);

    internal Affix<TEntry>[] ToAffixes() => Array.ConvertAll(Entries.Items, CreateAffix);

    internal Affix<TEntry> CreateAffix(TEntry entry) => new(entry, AFlag, Options);

    public sealed class Builder
    {
        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, new())
        {
        }

        public Builder(FlagValue aFlag, AffixEntryOptions options, List<TEntry> entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public List<TEntry> Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; set; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; set; }

        public bool HasEntries => Entries.Count > 0;

        public AffixEntryGroup<TEntry> ToGroup() => new(AFlag, Options, new(Entries));

        public void Add(TEntry entry)
        {
            Entries.Add(entry);
        }
    }
}
