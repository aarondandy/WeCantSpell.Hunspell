using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
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
            var affixesByIndexedByKeyBuilders = new Dictionary<char, Dictionary<FlagValue, AffixEntryGroup<TEntry>.Builder>>();
            var affixesWithEmptyKeys = new List<AffixEntryGroup<TEntry>>();
            var affixesWithDots = new List<AffixEntryGroup<TEntry>>();
            var contClasses = new HashSet<FlagValue>();

            if (builders != null)
            {
                foreach (var builder in builders)
                {
                    var group = builder.ToGroup();
                    affixesByFlag.Add(group.AFlag, group);

                    var entriesWithNoKey = new List<TEntry>();
                    var entriesWithDots = new List<TEntry>();

                    foreach (var entry in group.Entries)
                    {
                        var key = entry.Key;

                        contClasses.UnionWith(entry.ContClass);

                        if (string.IsNullOrEmpty(key))
                        {
                            entriesWithNoKey.Add(entry);
                        }
                        else if (key.Contains('.'))
                        {
                            entriesWithDots.Add(entry);
                        }
                        else
                        {
                            var indexedKey = key[0];
                            if (!affixesByIndexedByKeyBuilders.TryGetValue(indexedKey, out var keyedAffixes))
                            {
                                keyedAffixes = new Dictionary<FlagValue, AffixEntryGroup<TEntry>.Builder>();
                                affixesByIndexedByKeyBuilders.Add(indexedKey, keyedAffixes);
                            }

                            if (!keyedAffixes.TryGetValue(group.AFlag, out var groupBuilder))
                            {
                                groupBuilder = new AffixEntryGroup<TEntry>.Builder
                                {
                                    AFlag = group.AFlag,
                                    Options = group.Options,
                                    Entries = new List<TEntry>()
                                };
                                keyedAffixes.Add(group.AFlag, groupBuilder);
                            }

                            groupBuilder.Entries.Add(entry);
                        }
                    }

                    if (entriesWithNoKey.Count > 0)
                    {
                        affixesWithEmptyKeys.Add(new AffixEntryGroup<TEntry>(group.AFlag, group.Options, AffixEntryCollection<TEntry>.Create(entriesWithNoKey)));
                    }
                    if (entriesWithDots.Count > 0)
                    {
                        affixesWithDots.Add(new AffixEntryGroup<TEntry>(group.AFlag, group.Options, AffixEntryCollection<TEntry>.Create(entriesWithDots)));
                    }
                }
            }

            var affixesByIndexedByKey = new Dictionary<char, AffixEntryGroupCollection<TEntry>>(affixesByIndexedByKeyBuilders.Count);
            foreach (var keyedBuilder in affixesByIndexedByKeyBuilders)
            {
                var indexedAffixGroup = new List<AffixEntryGroup<TEntry>>(keyedBuilder.Value.Count);
                foreach(var builderPair in keyedBuilder.Value)
                {
                    indexedAffixGroup.Add(builderPair.Value.ToGroup());
                }

                affixesByIndexedByKey.Add(keyedBuilder.Key, AffixEntryGroupCollection<TEntry>.TakeList(indexedAffixGroup));
            }

            return constructor
            (
                affixesByFlag,
                affixesByIndexedByKey,
                AffixEntryGroupCollection<TEntry>.TakeList(affixesWithDots),
                AffixEntryGroupCollection<TEntry>.TakeList(affixesWithEmptyKeys),
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

        public AffixEntryGroup<TEntry> GetByFlag(FlagValue flag)
        {
            AffixesByFlag.TryGetValue(flag, out AffixEntryGroup<TEntry> result);
            return result;
        }

        public IEnumerator<AffixEntryGroup<TEntry>> GetEnumerator() => AffixesByFlag.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => AffixesByFlag.Values.GetEnumerator();

        internal List<AffixEntryGroup<TEntry>> GetByFlags(FlagSet flags)
        {
#if DEBUG
            if (flags == null)
            {
                throw new ArgumentNullException(nameof(flags));
            }
#endif

            var results = new List<AffixEntryGroup<TEntry>>(flags.Count);
            foreach(var flag in flags)
            {
                if (AffixesByFlag.TryGetValue(flag, out AffixEntryGroup<TEntry> result))
                {
                    results.Add(result);
                }
            }

            return results;
        }

        internal List<AffixEntryGroup<TEntry>> GetAffixesWithEmptyKeysAndFlag(FlagSet flags)
        {
#if DEBUG
            if (flags == null)
            {
                throw new ArgumentNullException(nameof(flags));
            }
#endif

            var results = new List<AffixEntryGroup<TEntry>>(flags.Count);
            foreach (var group in AffixesWithEmptyKeys)
            {
                if (flags.Contains(group.AFlag))
                {
                    results.Add(group);
                }
            }

            return results;
        }

        internal IEnumerable<Affix<TEntry>> GetMatchingWithDotAffixes(string word, Func<string, string, bool> predicate) =>
            AffixesWithDots.SelectMany(group =>
                group.Entries
                    .Where(entry => predicate(entry.Key, word))
                    .Select(entry => Affix<TEntry>.Create(entry, group)));
    }

    public sealed class SuffixCollection : AffixCollection<SuffixEntry>
    {
        public static readonly SuffixCollection Empty = new SuffixCollection(
            new Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>>(0),
            new Dictionary<char, AffixEntryGroupCollection<SuffixEntry>>(0),
            AffixEntryGroupCollection<SuffixEntry>.Empty,
            AffixEntryGroupCollection<SuffixEntry>.Empty,
            FlagSet.Empty);

        public static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder> builders)
        {
            if (builders == null || builders.Count == 0)
            {
                return Empty;
            }

            return Create(
                builders,
                (affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) =>
                    new SuffixCollection(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses));
        }

        private SuffixCollection(
            Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>> affixesByFlag,
            Dictionary<char, AffixEntryGroupCollection<SuffixEntry>> affixesByIndexedByKey,
            AffixEntryGroupCollection<SuffixEntry> affixesWithDots,
            AffixEntryGroupCollection<SuffixEntry> affixesWithEmptyKeys,
            FlagSet contClasses)
            : base(affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) { }

        internal List<Affix<SuffixEntry>> GetMatchingAffixes(string word, FlagSet groupFlagFilter = null)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new List<Affix<SuffixEntry>>(0);
            }

            var results = new List<Affix<SuffixEntry>>();

            if (AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out AffixEntryGroupCollection<SuffixEntry> indexedGroups))
            {
                foreach (var group in indexedGroups)
                {
                    if (groupFlagFilter == null || groupFlagFilter.Contains(group.AFlag))
                    {
                        foreach (var entry in group.Entries)
                        {
                            if (HunspellTextFunctions.IsReverseSubset(entry.Key, word))
                            {
                                results.Add(Affix<SuffixEntry>.Create(entry, group));
                            }
                        }
                    }
                }
            }

            if (AffixesWithDots.HasItems)
            {
                results.AddRange(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsReverseSubset));
            }

            return results;
        }
    }

    public sealed class PrefixCollection : AffixCollection<PrefixEntry>
    {
        public static readonly PrefixCollection Empty = new PrefixCollection(
            new Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>>(0),
            new Dictionary<char, AffixEntryGroupCollection<PrefixEntry>>(0),
            AffixEntryGroupCollection<PrefixEntry>.Empty,
            AffixEntryGroupCollection<PrefixEntry>.Empty,
            FlagSet.Empty);

        public static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder> builders)
        {
            if (builders == null || builders.Count == 0)
            {
                return Empty;
            }

            return Create(
                builders,
                (affixesByFlag, affixesByIndexedByKey, affixesWithDots, affixesWithEmptyKeys, contClasses) =>
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
            if (string.IsNullOrEmpty(word))
            {
                return new List<Affix<PrefixEntry>>(0);
            }

            var results = new List<Affix<PrefixEntry>>();

            if (AffixesByIndexedByKey.TryGetValue(word[0], out AffixEntryGroupCollection<PrefixEntry> indexedGroups))
            {
                foreach (var group in indexedGroups)
                {
                    foreach (var entry in group.Entries)
                    {
                        if (HunspellTextFunctions.IsSubset(entry.Key, word))
                        {
                            results.Add(Affix<PrefixEntry>.Create(entry, group));
                        }
                    }
                }
            }

            if (AffixesWithDots.HasItems)
            {
                results.AddRange(GetMatchingWithDotAffixes(word, HunspellTextFunctions.IsSubset));
            }

            return results;
        }
    }
}
