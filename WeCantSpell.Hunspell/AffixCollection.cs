using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class SuffixCollection : IEnumerable<SuffixGroup>
{
    public static SuffixCollection Empty { get; } = new();

    public static SuffixCollection Create(List<SuffixGroup.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static SuffixCollection Create(List<SuffixGroup.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var result = new SuffixCollection();
        result._affixesByFlag = new Dictionary<FlagValue, SuffixGroup>(builders.Count);

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<Suffix>();
        var affixesWithDots = new ArrayBuilder<Suffix>();
        var affixesByKeyPrefix = new Dictionary<char, ArrayBuilder<Suffix>>();

        foreach (var sourceBuilder in builders)
        {
            var group = sourceBuilder.ToImmutable(allowDestructive: allowDestructive);

            result._affixesByFlag.Add(group.AFlag, group);

            foreach (var entry in group.Entries)
            {
                contClassesBuilder.AddRange(entry.ContClass);

                var affix = new Suffix(entry, group.AFlag, group.Options);

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

                    if (!affixesByKeyPrefix.TryGetValue(indexedKey, out var indexGroup))
                    {
                        indexGroup = new();
                        affixesByKeyPrefix.Add(indexedKey, indexGroup);
                    }

                    indexGroup.Add(affix);
                }
            }
        }

        result.ContClasses = contClassesBuilder.MoveToFlagSet();
        result._affixesWithEmptyKeys = affixesWithEmptyKeys.Extract();
        result._affixesWithDots = affixesWithDots.Extract();
        result._affixesByKeyPrefix = affixesByKeyPrefix.ToDictionary(static pair => pair.Key, static pair => pair.Value.Extract());

        return result;
    }

    private SuffixCollection()
    {
    }

    private Dictionary<FlagValue, SuffixGroup> _affixesByFlag = new();
    private Dictionary<char, Suffix[]> _affixesByKeyPrefix = new();
    private Suffix[] _affixesWithDots = Array.Empty<Suffix>();
    private Suffix[] _affixesWithEmptyKeys = Array.Empty<Suffix>();

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public SuffixGroup? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<SuffixGroup> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Suffix[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal GetByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetByFlagsEnumerator
    {
        public GetByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, SuffixGroup> affixesByFlag)
        {
            _flags = flags;
            _affixesByFlag = affixesByFlag;
            _flagsIndex = 0;
            Current = SuffixGroup.Invalid;
        }

        private readonly FlagSet _flags;
        private readonly Dictionary<FlagValue, SuffixGroup> _affixesByFlag;
        private int _flagsIndex;

        public SuffixGroup Current { get; private set; }

        public GetByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flagsIndex < _flags.Count)
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Values[_flagsIndex++]) is { } result)
                {
                    Current = result;
                    return true;
                }
            }

            Current = SuffixGroup.Invalid;
            return false;
        }
    }

    internal AffixWordEnumerator GetMatchingAffixGroups(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<Suffix>();
        var second = Array.Empty<Suffix>();

        if (!word.IsEmpty)
        {
            if (_affixesByKeyPrefix.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                first = indexedAffixes;
            }

            second = _affixesWithDots;
        }

        return new(first, second);
    }

    internal struct AffixWordEnumerator
    {
        public AffixWordEnumerator(Suffix[] first, Suffix[] second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private Suffix[] _first;
        private Suffix[] _second;

        public Suffix Current { get; private set; }

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
        var first = Array.Empty<Suffix>();
        var second = Array.Empty<Suffix>();

        if (!word.IsEmpty)
        {
            if (groupFlagFilter.HasItems && _affixesByKeyPrefix.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                first = indexedAffixes;
            }

            second = _affixesWithDots;
        }

        return new (first, second, groupFlagFilter);
    }

    internal struct AffixWordFlagEnumerator
    {
        public AffixWordFlagEnumerator(Suffix[] first, Suffix[] second, FlagSet groupFlagFilter)
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
        private Suffix[] _first;
        private Suffix[] _second;
        private FlagSet _firstFlagFilter;

        public Suffix Current { get; private set; }

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

public sealed class PrefixCollection : IEnumerable<PrefixGroup>
{
    public static PrefixCollection Empty { get; } = new();

    public static PrefixCollection Create(List<PrefixGroup.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static PrefixCollection Create(List<PrefixGroup.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var result = new PrefixCollection();
        result._affixesByFlag = new Dictionary<FlagValue, PrefixGroup>(builders.Count);

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<Prefix>();
        var affixesWithDots = new ArrayBuilder<Prefix>();
        var affixesByKeyPrefix = new Dictionary<char, ArrayBuilder<Prefix>>();

        foreach (var sourceBuilder in builders)
        {
            var group = sourceBuilder.ToImmutable(allowDestructive: allowDestructive);

            result._affixesByFlag.Add(group.AFlag, group);

            foreach (var entry in group.Entries)
            {
                contClassesBuilder.AddRange(entry.ContClass);

                var affix = new Prefix(entry, group.AFlag, group.Options);

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

                    if (!affixesByKeyPrefix.TryGetValue(indexedKey, out var indexGroup))
                    {
                        indexGroup = new();
                        affixesByKeyPrefix.Add(indexedKey, indexGroup);
                    }

                    indexGroup.Add(affix);
                }
            }
        }

        result.ContClasses = contClassesBuilder.MoveToFlagSet();
        result._affixesWithEmptyKeys = affixesWithEmptyKeys.Extract();
        result._affixesWithDots = affixesWithDots.Extract();
        result._affixesByKeyPrefix = affixesByKeyPrefix.ToDictionary(static pair => pair.Key, static pair => pair.Value.Extract());

        return result;
    }

    private PrefixCollection()
    {
    }

    private Dictionary<FlagValue, PrefixGroup> _affixesByFlag = new();
    private Dictionary<char, Prefix[]> _affixesByKeyPrefix = new();
    private Prefix[] _affixesWithDots = Array.Empty<Prefix>();
    private Prefix[] _affixesWithEmptyKeys = Array.Empty<Prefix>();

    public FlagSet ContClasses { get; private set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public PrefixGroup? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<PrefixGroup> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Prefix[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal GetByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetByFlagsEnumerator
    {
        public GetByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, PrefixGroup> affixesByFlag)
        {
            _flags = flags;
            _affixesByFlag = affixesByFlag;
            _flagsIndex = 0;
            Current = PrefixGroup.Invalid;
        }

        private readonly FlagSet _flags;
        private readonly Dictionary<FlagValue, PrefixGroup> _affixesByFlag;
        private int _flagsIndex;

        public PrefixGroup Current { get; private set; }

        public GetByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flagsIndex < _flags.Count)
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Values[_flagsIndex++]) is { } result)
                {
                    Current = result;
                    return true;
                }
            }

            Current = PrefixGroup.Invalid;
            return false;
        }
    }

    internal AffixWordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        var first = Array.Empty<Prefix>();
        var second = Array.Empty<Prefix>();

        if (!word.IsEmpty)
        {
            if (_affixesByKeyPrefix.TryGetValue(word[0], out var indexedAffixes))
            {
                first = indexedAffixes;
            }

            second = _affixesWithDots;
        }

        return new(first, second);
    }

    internal struct AffixWordEnumerator
    {
        public AffixWordEnumerator(Prefix[] first, Prefix[] second)
        {
            _first = first;
            _firstIndex = 0;
            _second = second;
            _secondIndex = 0;
            Current = default!;
        }

        private int _firstIndex = 0;
        private int _secondIndex = 0;
        private Prefix[] _first;
        private Prefix[] _second;

        public Prefix Current { get; private set; }

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
