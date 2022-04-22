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
    protected static void Apply<TResult>(TResult result, List<AffixEntryGroup<TEntry>.Builder> builders, bool allowDestructive)
        where TResult : AffixCollection<TEntry>
    {
        var affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(builders.Count);
        var groupBuildersByKeyAndFlag = new Dictionary<char, Dictionary<FlagValue, AffixEntryGroup<TEntry>.Builder>>();
        var affixesWithEmptyKeys = new List<AffixEntryGroup<TEntry>>();
        var affixesWithDots = new List<AffixEntryGroup<TEntry>>();
        var contClassesBuilder = new FlagSet.Builder();

        foreach (var sourceBuilder in builders)
        {
            var group = sourceBuilder.ToImmutable(allowDestructive: allowDestructive);

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

                    if (!groupBuildersByFlag.TryGetValue(group.AFlag, out var groupBuilderByFlag))
                    {
                        groupBuilderByFlag = new(group.AFlag, group.Options);
                        groupBuildersByFlag.Add(group.AFlag, groupBuilderByFlag);
                    }

                    groupBuilderByFlag.Entries.Add(entry);
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
            while (++_flagsIndex < _flags.Count)
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Values[_flagsIndex]) is { } result)
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

    public static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders, bool allowDestructive)
    {
        var result = new SuffixCollection();

        if (builders is { Count: > 0 })
        {
            Apply(result, builders, allowDestructive: allowDestructive);
        }

        return result;
    }

    private SuffixCollection()
    {
    }

    internal AffixGroupWordEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<AffixEntryGroup<SuffixEntry>>();
        var second = AffixEntryGroupCollection<SuffixEntry>.Empty;

        if (!word.IsEmpty)
        {
            if (AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                first = indexedGroups.Groups;
            }

            second = AffixesWithDots;
        }

        return new(first, second);
    }

    internal struct AffixGroupWordEnumerator
    {
        public AffixGroupWordEnumerator(AffixEntryGroup<SuffixEntry>[] first, AffixEntryGroupCollection<SuffixEntry> second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private AffixEntryGroup<SuffixEntry>[] _first;
        private AffixEntryGroupCollection<SuffixEntry> _second;

        public AffixEntryGroup<SuffixEntry> Current { get; private set; }

        public AffixGroupWordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_firstIndex < _first.Length)
            {
                Current = _first[_firstIndex++];
                return true;
            }

            if (_secondIndex < _second.Count)
            {
                Current = _second[_secondIndex++];
                return true;
            }

            return false;
        }
    }

    internal AffixGroupWordFlagEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word, FlagSet groupFlagFilter)
    {
        var first = Array.Empty<AffixEntryGroup<SuffixEntry>>();
        var second = AffixEntryGroupCollection<SuffixEntry>.Empty;

        if (!word.IsEmpty)
        {
            if (groupFlagFilter.HasItems && AffixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                first = indexedGroups.Groups;
            }

            second = AffixesWithDots;
        }

        return new (first, second, groupFlagFilter);
    }

    internal struct AffixGroupWordFlagEnumerator
    {
        public AffixGroupWordFlagEnumerator(AffixEntryGroup<SuffixEntry>[] first, AffixEntryGroupCollection<SuffixEntry> second, FlagSet groupFlagFilter)
        {
            _first = first;
            _firstIndex = 0;
            _firstFlagFilter = groupFlagFilter;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private AffixEntryGroup<SuffixEntry>[] _first;
        private AffixEntryGroupCollection<SuffixEntry> _second;
        private FlagSet _firstFlagFilter;

        public AffixEntryGroup<SuffixEntry> Current { get; private set; }

        public AffixGroupWordFlagEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_firstIndex < _first.Length)
            {
                Current = _first[_firstIndex++];
                if (_firstFlagFilter.Contains(Current.AFlag))
                {
                    return true;
                }
            }

            if (_secondIndex < _second.Count)
            {
                Current = _second[_secondIndex++];
                return true;
            }

            return false;
        }
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixEntry>
{
    public static PrefixCollection Empty { get; } = new();

    public static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders, bool allowDestructive)
    {
        var result = new PrefixCollection();

        if (builders is { Count: > 0 })
        {
            Apply(result, builders, allowDestructive: allowDestructive);
        }

        return result;
    }

    private PrefixCollection()
    {
    }

    internal AffixGroupWordEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<AffixEntryGroup<PrefixEntry>>();
        var second = AffixEntryGroupCollection<PrefixEntry>.Empty;

        if (!word.IsEmpty)
        {
            if (AffixesByIndexedByKey.TryGetValue(word[0], out var indexedGroups))
            {
                first = indexedGroups.Groups;
            }

            if (AffixesWithDots.HasItems)
            {
                second = AffixesWithDots;
            }
        }

        return new(first, second);
    }

    internal struct AffixGroupWordEnumerator
    {
        public AffixGroupWordEnumerator(AffixEntryGroup<PrefixEntry>[] first, AffixEntryGroupCollection<PrefixEntry> second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private AffixEntryGroup<PrefixEntry>[] _first;
        private AffixEntryGroupCollection<PrefixEntry> _second;

        public AffixEntryGroup<PrefixEntry> Current { get; private set; }

        public AffixGroupWordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_firstIndex < _first.Length)
            {
                Current = _first[_firstIndex++];
                return true;
            }

            if (_secondIndex < _second.Count)
            {
                Current = _second[_secondIndex++];
                return true;
            }

            return false;
        }
    }
}
