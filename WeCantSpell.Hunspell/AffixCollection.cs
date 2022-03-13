using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        var affixesWithEmptyKeys = new List<AffixEntryGroup<TEntry>>();
        var affixesWithDots = new List<AffixEntryGroup<TEntry>>();
        var contClassesBuilder = new FlagSet.Builder();

        foreach (var group in builders.Select(static builder => builder.ToImmutable(false))) // TODO: refactor this to allow for destructive ToImmutable
        {
            affixesByFlag.Add(group.AFlag, group);

            var entriesWithNoKey = new AffixEntryGroup<TEntry>.Builder(group.AFlag, group.Options);
            var entriesWithDots = new AffixEntryGroup<TEntry>.Builder(group.AFlag, group.Options);

            foreach (var entry in group.Entries)
            {
                contClassesBuilder.AddRange(entry.ContClass);

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
                affixesWithEmptyKeys.Add(entriesWithNoKey.ToImmutable(true));
            }

            if (entriesWithDots.HasEntries)
            {
                affixesWithDots.Add(entriesWithDots.ToImmutable(true));
            }
        }

        var affixesByKey = new Dictionary<char, AffixEntryGroupCollection<TEntry>>(groupBuildersByKeyAndFlag.Count);
        foreach (var keyedBuilder in groupBuildersByKeyAndFlag)
        {
            var indexedAffixEntryGroups = new AffixEntryGroup<TEntry>[keyedBuilder.Value.Count];
            var writeIndex = 0;
            foreach (var b in keyedBuilder.Value.Values)
            {
                indexedAffixEntryGroups[writeIndex++] = b.ToImmutable(allowDestructive: true);
            }
            affixesByKey.Add(keyedBuilder.Key, new(indexedAffixEntryGroups));
        }

        result.AffixesByFlag = affixesByFlag;
        result.AffixesByIndexedByKey = affixesByKey;
        result.AffixesWithDots = new(affixesWithDots.ToArray());
        result.AffixesWithEmptyKeys = new(affixesWithEmptyKeys.ToArray());
        result.ContClasses = contClassesBuilder.MoveToFlagSet();
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

    internal GetByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, AffixesByFlag);

    internal IEnumerable<AffixEntryGroup<TEntry>> GetAffixesWithEmptyKeysAndFlag(FlagSet flags) =>
        AffixesWithEmptyKeys.AsEnumerable().Where(g => flags.Contains(g.AFlag));

    internal IEnumerable<Affix<TEntry>> GetMatchingWithDotAffixes(string word, Func<string, string, bool> predicate) =>
        AffixesWithDots.SelectMany(group =>
            group.Entries
                .Where(entry => predicate(entry.Key, word))
                .Select(group.CreateAffix));

    internal struct GetByFlagsEnumerator
    {
        public GetByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, AffixEntryGroup<TEntry>> affixesByFlag)
        {
            _flags = flags;
            _affixesByFlag = affixesByFlag;
            _flagsIndex = -1;
            Current = AffixEntryGroup<TEntry>.Invalid;
        }

        private readonly FlagSet _flags;
        private readonly Dictionary<FlagValue, AffixEntryGroup<TEntry>> _affixesByFlag;
        private int _flagsIndex;

        public AffixEntryGroup<TEntry> Current { get; private set; }

        public GetByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flagsIndex + 1 < _flags.Count)
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Values[++_flagsIndex]) is { } result)
                {
                    Current = result;
                    return true;
                }
            }

            Current = AffixEntryGroup<TEntry>.Invalid;
            return false;
        }
    }
}

public sealed class SuffixCollection : AffixCollection<SuffixEntry>
{
    public static SuffixCollection Empty { get; } = new();

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

    internal IEnumerable<Affix<SuffixEntry>> GetMatchingAffixes(string word)
    {
        var results = Enumerable.Empty<Affix<SuffixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                results = getGroupAffixes(word, indexedGroups);
            }

            if (AffixesWithDots.HasItems)
            {
                results = results.Concat(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsReverseSubset));
            }
        }

        return results;

        static IEnumerable<Affix<SuffixEntry>> getGroupAffixes(string word, AffixEntryGroupCollection<SuffixEntry> indexedGroups)
        {
            foreach (var group in indexedGroups.Groups)
            {
                foreach (var entry in group.Entries)
                {
                    if (HunspellTextFunctions.IsReverseSubset(entry.Key, word))
                    {
                        yield return group.CreateAffix(entry);
                    }
                }
            }
        }
    }

    internal IEnumerable<Affix<SuffixEntry>> GetMatchingAffixes(string word, FlagSet groupFlagFilter)
    {
        var results = Enumerable.Empty<Affix<SuffixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                if (groupFlagFilter.HasItems)
                {
                    results = getFilteredGroupAffixes(word, indexedGroups, groupFlagFilter);
                }
            }

            if (AffixesWithDots.HasItems)
            {
                results = results.Concat(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsReverseSubset));
            }
        }

        return results;

        static IEnumerable<Affix<SuffixEntry>> getFilteredGroupAffixes(string word, AffixEntryGroupCollection<SuffixEntry> indexedGroups, FlagSet groupFlagFilter)
        {
            foreach (var group in indexedGroups.Groups)
            {
                if (groupFlagFilter.Contains(group.AFlag))
                {
                    foreach (var entry in group.Entries)
                    {
                        if (HunspellTextFunctions.IsReverseSubset(entry.Key, word))
                        {
                            yield return group.CreateAffix(entry);
                        }
                    }
                }
            }
        }
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixEntry>
{
    public static PrefixCollection Empty { get; } = new();

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

    internal IEnumerable<Affix<PrefixEntry>> GetMatchingAffixes(string word)
    {
        var results = Enumerable.Empty<Affix<PrefixEntry>>();

        if (!string.IsNullOrEmpty(word))
        {
            if (AffixesByIndexedByKey.TryGetValue(word[0], out var indexedGroups))
            {
                results = getGroupAffixes(word, indexedGroups);
            }

            if (AffixesWithDots.HasItems)
            {
                results = results.Concat(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsSubset));
            }
        }

        return results;

        static IEnumerable<Affix<PrefixEntry>> getGroupAffixes(string word, AffixEntryGroupCollection<PrefixEntry> indexedGroups)
        {
            foreach (var group in indexedGroups.Groups)
            {
                foreach (var entry in group.Entries)
                {
                    if (HunspellTextFunctions.IsSubset(entry.Key, word))
                    {
                        yield return group.CreateAffix(entry);
                    }
                }
            }
        }
    }
}
