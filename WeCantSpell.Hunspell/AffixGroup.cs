using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("AFlag = {AFlag}, Options = {Options}, Count = {Count}")]
public sealed class AffixGroup<TAffixEntry> : IReadOnlyList<TAffixEntry> where TAffixEntry : AffixEntry
{
    public static AffixGroup<TAffixEntry> Create(FlagValue aFlag, AffixEntryOptions options, IEnumerable<TAffixEntry> entries) =>
        CreateUsingArray(aFlag, options, entries.ToArray());

    internal static AffixGroup<TAffixEntry> CreateUsingArray(FlagValue aFlag, AffixEntryOptions options, TAffixEntry[] entries) =>
        new(aFlag, options, entries);

    private AffixGroup(FlagValue aFlag, AffixEntryOptions options, TAffixEntry[] entries)
    {
        _entries = entries;
        _aFlag = aFlag;
        _options = options;
    }

    private readonly TAffixEntry[] _entries;
    private readonly FlagValue _aFlag;
    private readonly AffixEntryOptions _options;

    /// <summary>
    /// ID used to represent the affix group.
    /// </summary>
    public FlagValue AFlag => _aFlag;

    /// <summary>
    /// Options for this affix group.
    /// </summary>
    public AffixEntryOptions Options => _options;

    public int Count => _entries.Length;

    public bool IsEmpty => _entries.Length <= 0;

    public bool HasItems => _entries.Length > 0;

    internal TAffixEntry[] RawArray => _entries;

    public TAffixEntry this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif

            return _entries[index];
        }
    }

    public IEnumerator<TAffixEntry> GetEnumerator() => ((IEnumerable<TAffixEntry>)RawArray).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
