﻿using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("AFlag = {AFlag}, Entries = {Entries}")]
public sealed class AffixGroup<TAffixEntry> where TAffixEntry : AffixEntry
{
    private AffixGroup(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<TAffixEntry> entries)
    {
        Entries = entries;
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// All of the entries that make up this group.
    /// </summary>
    public ImmutableArray<TAffixEntry> Entries { get; }

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
        // TODO: Replace this builder with factory methods

        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, ImmutableArray.CreateBuilder<TAffixEntry>())
        {
        }

        private Builder(FlagValue aFlag, AffixEntryOptions options, ImmutableArray<TAffixEntry>.Builder entries)
        {
#if HAS_THROWNULL
            ArgumentNullException.ThrowIfNull(entries);
#else
            ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

            AFlag = aFlag;
            Options = options;
            Entries = entries;
        }

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public ImmutableArray<TAffixEntry>.Builder Entries { get; }

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag { get; set; }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options { get; set; }

        public AffixGroup<TAffixEntry> ToImmutable(bool allowDestructive = false) => new(AFlag, Options, Entries.ToImmutable(allowDestructive: allowDestructive));
    }
}
