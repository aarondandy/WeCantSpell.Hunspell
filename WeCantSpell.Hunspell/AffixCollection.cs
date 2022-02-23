using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public abstract class AffixCollection<TEntry> :
    IEnumerable<AffixEntryGroup<TEntry>>
    where TEntry : AffixEntry
{
    internal delegate TResult Constructor<TResult>(
        Dictionary<FlagValue, AffixEntryGroup<TEntry>> affixesByFlag,
        Dictionary<char, AffixEntryGroupCollection<TEntry>> affixesByIndexedByKey,
        AffixEntryGroupCollection<TEntry> affixesWithDots,
        AffixEntryGroupCollection<TEntry> affixesWithEmptyKeys,
        FlagSet contClasses)
        where TResult : AffixCollection<TEntry>;

    internal static TResult Create<TResult>(List<AffixEntryGroup<TEntry>.Builder> builders, Constructor<TResult> constructor)
        where TResult : AffixCollection<TEntry>
    {
        var affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(builders.Count);
        var groupBuildersByKeyAndFlag = new Dictionary<char, Dictionary<FlagValue, AffixEntryGroup<TEntry>.Builder>>();
        var affixesWithEmptyKeys = new AffixEntryGroupCollection<TEntry>.Builder();
        var affixesWithDots = new AffixEntryGroupCollection<TEntry>.Builder();
        var contClasses = new HashSet<FlagValue>();

        foreach (var group in builders.Select(static builder => builder.ToGroup()))
        {
            affixesByFlag.Add(group.AFlag, group);

            var entriesWithNoKey = new AffixEntryGroup<TEntry>.Builder(group.AFlag, group.Options);
            var entriesWithDots = new AffixEntryGroup<TEntry>.Builder(group.AFlag, group.Options);

            foreach (var entry in group.Entries)
            {
                contClasses.UnionWith(entry.ContClass);

                if (string.IsNullOrEmpty(entry.Key))
                {
                    entriesWithNoKey.Add(entry);
                }
                else if (entry.Key.Contains('.'))
                {
                    entriesWithDots.Add(entry);
                }
                else
                {
                    var indexedKey = entry.Key[0];

                    if (!groupBuildersByKeyAndFlag.TryGetValue(indexedKey, out var groupBuildersByFlag))
                    {
                        groupBuildersByFlag = new();
                        groupBuildersByKeyAndFlag.Add(indexedKey, groupBuildersByFlag);
                    }

                    if (!groupBuildersByFlag.TryGetValue(group.AFlag, out var groupBuilder))
                    {
                        groupBuilder = new(group.AFlag, group.Options);
                        groupBuildersByFlag.Add(group.AFlag, groupBuilder);
                    }

                    groupBuilder.Entries.Add(entry);
                }
            }

            if (entriesWithNoKey.HasEntries)
            {
                affixesWithEmptyKeys.Add(entriesWithNoKey.ToGroup());
            }

            if (entriesWithDots.HasEntries)
            {
                affixesWithDots.Add(entriesWithDots.ToGroup());
            }
        }

        var affixesByKey = new Dictionary<char, AffixEntryGroupCollection<TEntry>>(groupBuildersByKeyAndFlag.Count);
        foreach (var keyedBuilder in groupBuildersByKeyAndFlag)
        {
            var indexedAffixGroup = new AffixEntryGroupCollection<TEntry>.Builder();
            indexedAffixGroup.AddRange(keyedBuilder.Value.Values.Select(b => b.ToGroup()));
            affixesByKey.Add(keyedBuilder.Key, indexedAffixGroup.ToGroupCollection());
        }

        return constructor
        (
            affixesByFlag,
            affixesByKey,
            affixesWithDots: affixesWithDots.ToGroupCollection(),
            affixesWithEmptyKeys: affixesWithEmptyKeys.ToGroupCollection(),
            FlagSet.Create(contClasses)
        );
    }

    internal AffixCollection(
        Dictionary<FlagValue, AffixEntryGroup<TEntry>> affixesByFlag,
        Dictionary<char, AffixEntryGroupCollection<TEntry>> affixesByIndexedByKey,
        AffixEntryGroupCollection<TEntry> affixesWithDots,
        AffixEntryGroupCollection<TEntry> affixesWithEmptyKeys,
        FlagSet contClasses)
    {
        AffixesByFlag = affixesByFlag;
        AffixesByIndexedByKey = affixesByIndexedByKey;
        AffixesWithDots = affixesWithDots;
        AffixesWithEmptyKeys = affixesWithEmptyKeys;
        ContClasses = contClasses;
        HasAffixes = affixesByFlag.Count != 0;
    }

    protected Dictionary<FlagValue, AffixEntryGroup<TEntry>> AffixesByFlag { get; }

    protected Dictionary<char, AffixEntryGroupCollection<TEntry>> AffixesByIndexedByKey { get; }

    public AffixEntryGroupCollection<TEntry> AffixesWithDots { get; }

    public AffixEntryGroupCollection<TEntry> AffixesWithEmptyKeys { get; }

    public FlagSet ContClasses { get; }

    public bool HasAffixes { get; }

    public IEnumerable<FlagValue> FlagValues => AffixesByFlag.Keys;

    public AffixEntryGroup<TEntry>? GetByFlag(FlagValue flag) => AffixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<AffixEntryGroup<TEntry>> GetEnumerator() => AffixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal IEnumerable<AffixEntryGroup<TEntry>> GetByFlags(FlagSet flags) =>
        flags.AsEnumerable().Select(flag => AffixesByFlag.GetValueOrDefault(flag)).WhereNotNull();

    internal IEnumerable<AffixEntryGroup<TEntry>> GetAffixesWithEmptyKeysAndFlag(FlagSet flags) =>
        AffixesWithEmptyKeys.AsEnumerable().Where(g => flags.Contains(g.AFlag));

    internal IEnumerable<Affix<TEntry>> GetMatchingWithDotAffixes(string word, Func<string, string, bool> predicate) =>
        AffixesWithDots.SelectMany(group =>
            group.Entries
                .Where(entry => predicate(entry.Key, word))
                .Select(entry => new Affix<TEntry>(entry, group)));
}

public sealed class SuffixCollection : AffixCollection<SuffixEntry>
{
    public static readonly SuffixCollection Empty = new SuffixCollection(
        new(0),
        new(0),
        AffixEntryGroupCollection<SuffixEntry>.Empty,
        AffixEntryGroupCollection<SuffixEntry>.Empty,
        FlagSet.Empty);

    public static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        return Create(
            builders,
            constructor: static (affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) =>
                new SuffixCollection(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses));
    }

    private SuffixCollection(
        Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>> affixesByFlag,
        Dictionary<char, AffixEntryGroupCollection<SuffixEntry>> affixesByIndexedByKey,
        AffixEntryGroupCollection<SuffixEntry> affixesWithDots,
        AffixEntryGroupCollection<SuffixEntry> affixesWithEmptyKeys,
        FlagSet contClasses)
        : base(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) { }

    internal List<Affix<SuffixEntry>> GetMatchingAffixes(string word, FlagSet? groupFlagFilter = null)
    {
        var results = new List<Affix<SuffixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                foreach (var group in indexedGroups)
                {
                    if (groupFlagFilter == null || groupFlagFilter.Contains(group.AFlag))
                    {
                        foreach (var entry in group.Entries)
                        {
                            if (HunspellTextFunctions.IsReverseSubset(entry.Key, word))
                            {
                                results.Add(new(entry, group));
                            }
                        }
                    }
                }
            }

            if (AffixesWithDots.HasItems)
            {
                results.AddRange(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsReverseSubset));
            }
        }

        return results;
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixEntry>
{
    public static readonly PrefixCollection Empty = new PrefixCollection(
        new(0),
        new(0),
        AffixEntryGroupCollection<PrefixEntry>.Empty,
        AffixEntryGroupCollection<PrefixEntry>.Empty,
        FlagSet.Empty);

    public static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        return Create(
            builders,
            constructor: static (affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) =>
                new PrefixCollection(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses));
    }

    private PrefixCollection(
        Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>> affixesByFlag,
        Dictionary<char, AffixEntryGroupCollection<PrefixEntry>> affixesByIndexedByKey,
        AffixEntryGroupCollection<PrefixEntry> affixesWithDots,
        AffixEntryGroupCollection<PrefixEntry> affixesWithEmptyKeys,
        FlagSet contClasses)
        : base(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) { }

    internal List<Affix<PrefixEntry>> GetMatchingAffixes(string word)
    {
        var results = new List<Affix<PrefixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[0], out AffixEntryGroupCollection<PrefixEntry> indexedGroups))
            {
                foreach (var group in indexedGroups)
                {
                    foreach (var entry in group.Entries)
                    {
                        if (HunspellTextFunctions.IsSubset(entry.Key, word))
                        {
                            results.Add(new(entry, group));
                        }
                    }
                }
            }

            if (AffixesWithDots.HasItems)
            {
                results.AddRange(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsSubset));
            }
        }

        return results;
    }
}
