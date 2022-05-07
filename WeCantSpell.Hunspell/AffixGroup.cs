using System;
using System.Collections.Immutable;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public interface IAffixGroup<TAffixEntry>
    where TAffixEntry : IAffixEntry
{
    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    ImmutableArray<TAffixEntry> Entries { get; }

    /// <summary>
    /// ID used to represent the affix group.
    /// </summary>
    FlagValue AFlag { get; }

    /// <summary>
    /// Options for this affix group.
    /// </summary>
    AffixEntryOptions Options { get; }
}

/// <summary>
/// Contains a set of <see cref="PrefixEntry"/> instances based on the same <see cref="AFlag"/>.
/// </summary>
public sealed class PrefixGroup : IAffixGroup<PrefixEntry>
{
    internal static PrefixGroup Invalid { get; } = new(default, AffixEntryOptions.None, ImmutableArray<PrefixEntry>.Empty);

    private PrefixGroup(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<PrefixEntry> entries)
    {
        Entries = entries;
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    public ImmutableArray<PrefixEntry> Entries { get; }

    /// <summary>
    /// ID used to represent the affix group.
    /// </summary>
    public FlagValue AFlag { get; }

    /// <summary>
    /// Options for this affix group.
    /// </summary>
    public AffixEntryOptions Options { get; }

    public sealed class Builder
    {
        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, ImmutableArray.CreateBuilder<PrefixEntry>())
        {
        }

        public Builder(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<PrefixEntry>.Builder entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public ImmutableArray<PrefixEntry>.Builder Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; set; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; set; }

        public PrefixGroup ToImmutable(bool allowDestructive = false) => new(AFlag, Options, Entries.ToImmutable(allowDestructive: allowDestructive));
    }
}

/// <summary>
/// Contains a set of <see cref="SuffixEntry"/> instances based on the same <see cref="AFlag"/>.
/// </summary>
public sealed class SuffixGroup : IAffixGroup<SuffixEntry>
{
    internal static SuffixGroup Invalid { get; } = new(default, AffixEntryOptions.None, ImmutableArray<SuffixEntry>.Empty);

    private SuffixGroup(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<SuffixEntry> entries)
    {
        Entries = entries;
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    public ImmutableArray<SuffixEntry> Entries { get; }

    /// <summary>
    /// ID used to represent the affix group.
    /// </summary>
    public FlagValue AFlag { get; }

    /// <summary>
    /// Options for this affix group.
    /// </summary>
    public AffixEntryOptions Options { get; }

    public sealed class Builder
    {
        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, ImmutableArray.CreateBuilder<SuffixEntry>())
        {
        }

        public Builder(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<SuffixEntry>.Builder entries)
        {
            AFlag = aFlag;
            Options = options;
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public ImmutableArray<SuffixEntry>.Builder Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; set; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; set; }

        public SuffixGroup ToImmutable(bool allowDestructive = false) => new(AFlag, Options, Entries.ToImmutable(allowDestructive: allowDestructive));
    }
}
