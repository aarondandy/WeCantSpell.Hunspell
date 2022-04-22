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

    internal AffixEnumerable ToAffixes() => new(Entries, AFlag, Options);

    internal Affix<TEntry> CreateAffix(TEntry entry) => new(entry, AFlag, Options);

    internal struct AffixEnumerable
    {
        public AffixEnumerable(ImmutableArray<TEntry> entries, FlagValue aFlag, AffixEntryOptions options)
        {
            _core = entries.GetEnumerator();
            _aFlag = aFlag;
            _options = options;
            Current = default!;
        }

        private ImmutableArray<TEntry>.Enumerator _core;
        private FlagValue _aFlag;
        private AffixEntryOptions _options;

        public Affix<TEntry> Current { get; private set; }

        public AffixEnumerable GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_core.MoveNext())
            {
                Current = new(_core.Current, _aFlag, _options);
                return true;
            }

            return false;
        }
    }

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
