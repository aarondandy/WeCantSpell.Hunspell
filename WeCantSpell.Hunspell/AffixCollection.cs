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
            if (_affixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                first = indexedAffixes;
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
            if (groupFlagFilter.HasItems && _affixesByIndexedByKey.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                first = indexedAffixes;
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

    internal static PrefixCollection Create(List<AffixEntryGroup<PrefixEntry>.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var result = new PrefixCollection();
        result._affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>>(builders.Count);

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<Affix<PrefixEntry>>();
        var affixesWithDots = new ArrayBuilder<Affix<PrefixEntry>>();
        var affixesByKey = new Dictionary<char, ArrayBuilder<Affix<PrefixEntry>>>();

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

    private PrefixCollection()
    {
    }

    private Dictionary<FlagValue, AffixEntryGroup<PrefixEntry>> _affixesByFlag = new();
    private Dictionary<char, Affix<PrefixEntry>[]> _affixesByIndexedByKey = new();
    private Affix<PrefixEntry>[] _affixesWithDots = Array.Empty<Affix<PrefixEntry>>();
    private Affix<PrefixEntry>[] _affixesWithEmptyKeys = Array.Empty<Affix<PrefixEntry>>();

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public AffixEntryGroup<PrefixEntry>? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<AffixEntryGroup<PrefixEntry>> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Affix<PrefixEntry>[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

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

    internal AffixWordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<Affix<PrefixEntry>>();
        var second = Array.Empty<Affix<PrefixEntry>>();

        if (!word.IsEmpty)
        {
            if (_affixesByIndexedByKey.TryGetValue(word[0], out var indexedAffixes))
            {
                first = indexedAffixes;
            }

            second = _affixesWithDots;
        }

        return new(first, second);
    }

    internal struct AffixWordEnumerator
    {
        public AffixWordEnumerator(Affix<PrefixEntry>[] first, Affix<PrefixEntry>[] second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private Affix<PrefixEntry>[] _first;
        private Affix<PrefixEntry>[] _second;

        public Affix<PrefixEntry> Current { get; private set; }

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
}
