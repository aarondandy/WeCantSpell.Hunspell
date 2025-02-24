using System;
using System.Collections.Generic;
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

    [DebuggerDisplay("AFlag = {AFlag}, Count = {Entries}")]
    [Obsolete("Try to replace this builder with factory methods")]
    public sealed class Builder
    {
        public Builder(FlagValue aFlag, AffixEntryOptions options) : this(aFlag, options, new())
        {
        }

        private Builder(FlagValue aFlag, AffixEntryOptions options, ArrayBuilder<TAffixEntry> entries)
        {
#if HAS_THROWNULL
            ArgumentNullException.ThrowIfNull(entries);
#else
            ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

            AFlag = aFlag;
            Options = options;
            _entries = entries;
        }

        internal readonly ArrayBuilder<TAffixEntry> _entries;
        private FlagValue _aFlag;
        private AffixEntryOptions _options;

        /// <summary>
        /// All of the entries that make up this group.
        /// </summary>
        public IList<TAffixEntry> Entries => _entries;

        /// <summary>
        /// ID used to represent the affix group.
        /// </summary>
        public FlagValue AFlag
        {
            get => _aFlag;
            set => _aFlag = value;
        }

        /// <summary>
        /// Options for this affix group.
        /// </summary>
        public AffixEntryOptions Options
        {
            get => _options;
            set => _options = value;
        }

        [Obsolete]
        public AffixGroup<TAffixEntry> ToImmutable(bool allowDestructive = false) => new(AFlag, Options, Entries.ToImmutable(allowDestructive: allowDestructive));
    }
}
