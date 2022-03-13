using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

/// <summary>
/// Contains a set of <see cref="AffixEntry"/> instances based on the same <see cref="AFlag"/>.
/// </summary>
/// <typeparam name="TEntry">The specific entry type.</typeparam>
public sealed class AffixEntryGroup<TEntry> where TEntry : AffixEntry
{
    internal static AffixEntryGroup<TEntry> Invalid { get; } = new(default, AffixEntryOptions.None, ImmutableArray<TEntry>.Empty);

    private AffixEntryGroup(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<TEntry> entries)
    {
        Entries = entries;
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    public ImmutableArray<TEntry> Entries { get; }

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

    internal IEnumerable<Affix<TEntry>> ToAffixes() => Entries.Select(CreateAffix);

    internal Affix<TEntry> CreateAffix(TEntry entry) => new(entry, AFlag, Options);

    public sealed class Builder
    {
        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, ImmutableArray.CreateBuilder<TEntry>())
        {
        }

        public Builder(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<TEntry>.Builder entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public ImmutableArray<TEntry>.Builder Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; set; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; set; }

        public bool HasEntries => Entries.Count > 0;

        public AffixEntryGroup<TEntry> ToImmutable(bool allowDestructive) =>
            new(AFlag, Options, Entries.ToImmutable(allowDestructive: allowDestructive));

        public void Add(TEntry entry)
        {
            Entries.Add(entry);
        }
    }
}
