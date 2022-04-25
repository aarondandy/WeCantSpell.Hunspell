using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class SuffixCollection : IEnumerable<AffixEntryGroup<SuffixEntry>>
{
    public static SuffixCollection Empty { get; } = new();

    public static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static SuffixCollection Create(List<AffixEntryGroup<SuffixEntry>.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var result = new SuffixCollection();
        result._affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>>(builders.Count);

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<Affix<SuffixEntry>>();
        var affixesWithDots = new ArrayBuilder<Affix<SuffixEntry>>();
        var affixesByKey = new Dictionary<char, ArrayBuilder<Affix<SuffixEntry>>>();

        foreach (var sourceBuilder in builders)
        {
            var group = sourceBuilder.ToImmutable(allowDestructive: allowDestructive);

            result._affixesByFlag.Add(group.AFlag, group);

            foreach (var entry in group.Entries)
            {
                contClassesBuilder.AddRange(entry.ContClass);

                var affix = group.CreateAffix(entry);

                if (string.IsNullOrEmpty(entry.Key))
                {
                    affixesWithEmptyKeys.Add(affix);
                }
                else if (entry.Key.Contains('.'))
                {
                    affixesWithDots.Add(affix);
                }
                else
                {
                    var indexedKey = entry.Key[0];

                    if (!affixesByKey.TryGetValue(indexedKey, out var indexGroup))
                    {
                        indexGroup = new();
                        affixesByKey.Add(indexedKey, indexGroup);
                    }

                    indexGroup.Add(affix);
                }
            }
        }

        result.ContClasses = contClassesBuilder.MoveToFlagSet();
        result._affixesWithEmptyKeys = affixesWithEmptyKeys.Extract();
        result._affixesWithDots = affixesWithDots.Extract();
        result._affixesByIndexedByKey = affixesByKey.ToDictionary(static pair => pair.Key, static pair => pair.Value.Extract());

        return result;
    }

    private SuffixCollection()
    {
    }

    private Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>> _affixesByFlag = new();
    private Dictionary<char, Affix<SuffixEntry>[]> _affixesByIndexedByKey = new();
    private Affix<SuffixEntry>[] _affixesWithDots = Array.Empty<Affix<SuffixEntry>>();
    private Affix<SuffixEntry>[] _affixesWithEmptyKeys = Array.Empty<Affix<SuffixEntry>>();

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public AffixEntryGroup<SuffixEntry>? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<AffixEntryGroup<SuffixEntry>> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Affix<SuffixEntry>[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal GetByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetByFlagsEnumerator
    {
        public GetByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>> affixesByFlag)
        {
            _flags = flags;
            _affixesByFlag = affixesByFlag;
            _flagsIndex = -1;
            Current = AffixEntryGroup<SuffixEntry>.Invalid;
        }

        private readonly FlagSet _flags;
        private readonly Dictionary<FlagValue, AffixEntryGroup<SuffixEntry>> _affixesByFlag;
        private int _flagsIndex;

        public AffixEntryGroup<SuffixEntry> Current { get; private set; }

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

            Current = AffixEntryGroup<SuffixEntry>.Invalid;
            return false;
        }
    }

    internal AffixWordEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<Affix<SuffixEntry>>();
        var second = Array.Empty<Affix<SuffixEntry>>();

        if (!word.IsEmpty)
        {
            if (_affixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                first = indexedGroups;
            }

            second = _affixesWithDots;
        }

        return new(first, second);
    }

    internal struct AffixWordEnumerator
    {
        public AffixWordEnumerator(Affix<SuffixEntry>[] first, Affix<SuffixEntry>[] second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private Affix<SuffixEntry>[] _first;
        private Affix<SuffixEntry>[] _second;

        public Affix<SuffixEntry> Current { get; private set; }

        public AffixWordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_firstIndex < _first.Length)
            {
                Current = _first[_firstIndex++];
                return true;
            }

            if (_secondIndex < _second.Length)
            {
                Current = _second[_secondIndex++];
                return true;
            }

            return false;
        }
    }

    internal AffixWordFlagEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word, FlagSet groupFlagFilter)
    {
        var first = Array.Empty<Affix<SuffixEntry>>();
        var second = Array.Empty<Affix<SuffixEntry>>();

        if (!word.IsEmpty)
        {
            if (groupFlagFilter.HasItems && _affixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedGroups))
            {
                first = indexedGroups;
            }

            second = _affixesWithDots;
        }

        return new (first, second, groupFlagFilter);
    }

    internal struct AffixWordFlagEnumerator
    {
        public AffixWordFlagEnumerator(Affix<SuffixEntry>[] first, Affix<SuffixEntry>[] second, FlagSet groupFlagFilter)
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
        private Affix<SuffixEntry>[] _first;
        private Affix<SuffixEntry>[] _second;
        private FlagSet _firstFlagFilter;

        public Affix<SuffixEntry> Current { get; private set; }

        public AffixWordFlagEnumerator GetEnumerator() => this;

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

            if (_secondIndex < _second.Length)
            {
                Current = _second[_secondIndex++];
                return true;
            }

            return false;
        }
    }
}

public sealed class PrefixCollection : IEnumerable<AffixEntryGroup<PrefixEntry>>
{
    public static PrefixCollection Empty { get; } = new();

    public static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders) => Create(builders, allowDestructive: false);

    private static void Apply(PrefixCollection result, List<AffixEntryGroup<PrefixEntry>.Builder> builders, bool allowDestructive)
    {
        var affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>>(builders.Count);
        var groupBuildersByKeyAndFlag = new Dictionary<char, Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>.Builder>>();
        var affixesWithEmptyKeys = new List<AffixEntryGroup<PrefixEntry>>();
        var affixesWithDots = new List<AffixEntryGroup<PrefixEntry>>();
        var contClassesBuilder = new FlagSet.Builder();

        foreach (var sourceBuilder in builders)
        {
            var group = sourceBuilder.ToImmutable(allowDestructive: allowDestructive);

            affixesByFlag.Add(group.AFlag, group);

            var entriesWithNoKey = new AffixEntryGroup<PrefixEntry>.Builder(group.AFlag, group.Options);
            var entriesWithDots = new AffixEntryGroup<PrefixEntry>.Builder(group.AFlag, group.Options);

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

        var affixesByKey = new Dictionary<char, AffixEntryGroupCollection<PrefixEntry>>(groupBuildersByKeyAndFlag.Count);
        foreach (var keyedBuilder in groupBuildersByKeyAndFlag)
        {
            var indexedAffixEntryGroups = new AffixEntryGroup<PrefixEntry>[keyedBuilder.Value.Count];
            var writeIndex = 0;
            foreach (var b in keyedBuilder.Value.Values)
            {
                indexedAffixEntryGroups[writeIndex++] = b.ToImmutable(allowDestructive: true);
            }
            affixesByKey.Add(keyedBuilder.Key, new(indexedAffixEntryGroups));
        }

        result._affixesByFlag = affixesByFlag;
        result._affixesByIndexedByKey = affixesByKey;
        result.AffixesWithDots = new(affixesWithDots.ToArray());
        result.AffixesWithEmptyKeys = new(affixesWithEmptyKeys.ToArray());
        result.ContClasses = contClassesBuilder.MoveToFlagSet();
    }

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

    private Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>> _affixesByFlag = new();

    private Dictionary<char, AffixEntryGroupCollection<PrefixEntry>> _affixesByIndexedByKey = new();

    public AffixEntryGroupCollection<PrefixEntry> AffixesWithDots { get; private set; } = AffixEntryGroupCollection<PrefixEntry>.Empty;

    public AffixEntryGroupCollection<PrefixEntry> AffixesWithEmptyKeys { get; private set; } = AffixEntryGroupCollection<PrefixEntry>.Empty;

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public AffixEntryGroup<PrefixEntry>? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<AffixEntryGroup<PrefixEntry>> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal GetByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetByFlagsEnumerator
    {
        public GetByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>> affixesByFlag)
        {
            _flags = flags;
            _affixesByFlag = affixesByFlag;
            _flagsIndex = -1;
            Current = AffixEntryGroup<PrefixEntry>.Invalid;
        }

        private readonly FlagSet _flags;
        private readonly Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>> _affixesByFlag;
        private int _flagsIndex;

        public AffixEntryGroup<PrefixEntry> Current { get; private set; }

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

            Current = AffixEntryGroup<PrefixEntry>.Invalid;
            return false;
        }
    }

    internal AffixGroupWordEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<AffixEntryGroup<PrefixEntry>>();
        var second = AffixEntryGroupCollection<PrefixEntry>.Empty;

        if (!word.IsEmpty)
        {
            if (_affixesByIndexedByKey.TryGetValue(word[0], out var indexedGroups))
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
