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
    protected static void Apply<TResult>(TResult result, List<AffixEntryGroup<TEntry>.Builder> builders)
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

        result.AffixesByFlag = affixesByFlag;
        result.AffixesByIndexedByKey = affixesByKey;
        result.AffixesWithDots = affixesWithDots.ToGroupCollection();
        result.AffixesWithEmptyKeys = affixesWithEmptyKeys.ToGroupCollection();
        result.ContClasses = FlagSet.Create(contClasses);
    }

    private protected AffixCollection()
    {
    }

    protected Dictionary<FlagValue, AffixEntryGroup<TEntry>> AffixesByFlag { get; private set; } = new();

    protected Dictionary<char, AffixEntryGroupCollection<TEntry>> AffixesByIndexedByKey { get; private set; } = new();

    public AffixEntryGroupCollection<TEntry> AffixesWithDots { get; private set; } = AffixEntryGroupCollection<TEntry>.Empty;

    public AffixEntryGroupCollection<TEntry> AffixesWithEmptyKeys { get; private set; } = AffixEntryGroupCollection<TEntry>.Empty;

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => AffixesByFlag.Count != 0;

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
                .Select(group.CreateAffix));
}

public sealed class SuffixCollection : AffixCollection<SuffixEntry>
{
    public static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders)
    {
        var result = new SuffixCollection();

        if (builders is { Count: > 0 })
        {
            Apply(result, builders);
        }

        return result;
    }

    private SuffixCollection()
    {
    }

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
                                results.Add(group.CreateAffix(entry));
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
    public static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders)
    {
        var result = new PrefixCollection();

        if (builders is { Count: > 0 })
        {
            Apply(result, builders);
        }

        return result;
    }

    private PrefixCollection()
    {
    }

    internal List<Affix<PrefixEntry>> GetMatchingAffixes(string word)
    {
        var results = new List<Affix<PrefixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[0], out var indexedGroups))
            {
                foreach (var group in indexedGroups)
                {
                    foreach (var entry in group.Entries)
                    {
                        if (HunspellTextFunctions.IsSubset(entry.Key, word))
                        {
                            results.Add(group.CreateAffix(entry));
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
