using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

/// <summary>
/// An affix is either a prefix or a suffix attached to root words to make other words.
/// </summary>
/// <remarks>
/// 
/// Appendix:  Understanding Affix Code
/// 
/// <para>
/// An affix is either a  prefix or a suffix attached to root words to make 
/// other words.
/// </para>
/// 
/// <para>
/// Basically a <see cref="PrefixGroup"/> or a <see cref="SuffixGroup"/> is set of affix objects
/// which store information about the prefix or suffix along
/// with supporting routines to check if a word has a particular
/// prefix or suffix or a combination.
/// </para>
/// 
/// <para>
/// Zero stripping or affix are indicated by zero. Zero condition is indicated by dot.
/// Condition is a simplified, regular expression-like pattern, which must be met
/// before the affix can be applied. (Dot signs an arbitrary character.Characters in braces
/// sign an arbitrary character from the character subset.Dash hasn't got special
/// meaning, but circumflex(^) next the first brace sets the complementer character set.)
/// </para>
/// 
/// </remarks>
/// 
/// <example>
/// 
/// <para>
/// Here is a suffix borrowed from the en_US.aff file.  This file 
/// is whitespace delimited.
/// </para>
/// 
/// <code>
/// SFX D Y 4 
/// SFX D   0     e          d
/// SFX D   y     ied        [^aeiou]y
/// SFX D   0     ed         [^ey]
/// SFX D   0     ed         [aeiou]y
/// </code>
/// 
/// This information can be interpreted as follows
/// 
/// <para>
/// In the first line has 4 fields which define the <see cref="SuffixGroup"/> for this affix that will contain four <see cref="SuffixEntry"/> objects.
/// </para>
/// 
/// <code>
/// Field
/// -----
/// 1     SFX - indicates this is a suffix
/// 2     D   - is the name of the character flag which represents this suffix
/// 3     Y   - indicates it can be combined with prefixes (cross product)
/// 4     4   - indicates that sequence of 4 affentry structures are needed to
///                properly store the affix information
/// </code>
/// 
/// <para>
/// The remaining lines describe the unique information for the 4 <see cref="SuffixEntry"/> 
/// objects that make up this affix.  Each line can be interpreted
/// as follows: (note fields 1 and 2 are as a check against line 1 info)
/// </para>
/// 
/// <code>
/// Field
/// -----
/// 1     SFX         - indicates this is a suffix
/// 2     D           - is the name of the character flag for this affix
/// 3     y           - the string of chars to strip off before adding affix
///                          (a 0 here indicates the NULL string)
/// 4     ied         - the string of affix characters to add
/// 5     [^aeiou]y   - the conditions which must be met before the affix
///                     can be applied
/// </code>
/// 
/// <para>
/// Field 5 is interesting.  Since this is a suffix, field 5 tells us that
/// there are 2 conditions that must be met.  The first condition is that 
/// the next to the last character in the word must *NOT* be any of the 
/// following "a", "e", "i", "o" or "u".  The second condition is that
/// the last character of the word must end in "y".
/// </para>
/// 
/// </example>
/// 
/// <seealso cref="PrefixCollection"/>
/// <seealso cref="SuffixCollection"/>
public class AffixCollection
{
    public FlagSet ContClasses { get; protected set; } = FlagSet.Empty;
}

public sealed class SuffixCollection : AffixCollection, IEnumerable<SuffixGroup>
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

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public SuffixGroup? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<SuffixGroup> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Suffix[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal GetAffixByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetAffixByFlagsEnumerator
    {
        public GetAffixByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, SuffixGroup> affixesByFlag)
        {
            _flags = flags.GetEnumerator();
            _affixesByFlag = affixesByFlag;
            _group = SuffixGroup.Invalid;
            _groupIndex = 0;
            Current = default!;
        }

        private int _groupIndex;
        private SuffixGroup _group;
        private Dictionary<FlagValue, SuffixGroup> _affixesByFlag;
        private FlagSet.Enumerator _flags;

        public Suffix Current { get; private set; }

        public GetAffixByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_groupIndex >= _group.Entries.Length)
            {
                if (!MoveNextGroup())
                {
                    return false;
                }
            }

            Current = new(_group.Entries[_groupIndex++], _group.AFlag, _group.Options);
            return true;
        }

        private bool MoveNextGroup()
        {
            while (_flags.MoveNext())
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Current) is { } result && result.Entries.Length != 0)
                {
                    _group = result;
                    _groupIndex = 0;
                    return true;
                }
            }

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

public sealed class PrefixCollection : AffixCollection, IEnumerable<PrefixGroup>
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

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public PrefixGroup? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    public IEnumerator<PrefixGroup> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal Prefix[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal GetAffixByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetAffixByFlagsEnumerator
    {
        public GetAffixByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, PrefixGroup> affixesByFlag)
        {
            _flags = flags.GetEnumerator();
            _affixesByFlag = affixesByFlag;
            _group = PrefixGroup.Invalid;
            _groupIndex = 0;
            Current = default!;
        }

        private int _groupIndex;
        private PrefixGroup _group;
        private Dictionary<FlagValue, PrefixGroup> _affixesByFlag;
        private FlagSet.Enumerator _flags;

        public Prefix Current { get; private set; }

        public GetAffixByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_groupIndex >= _group.Entries.Length)
            {
                if (!MoveNextGroup())
                {
                    return false;
                }
            }

            Current = new(_group.Entries[_groupIndex++], _group.AFlag, _group.Options);
            return true;
        }

        private bool MoveNextGroup()
        {
            while (_flags.MoveNext())
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Current) is { } result && result.Entries.Length != 0)
                {
                    _group = result;
                    _groupIndex = 0;
                    return true;
                }
            }

            return false;
        }
    }

    internal GetGroupsByFlagsEnumerator GetGroupsByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal struct GetGroupsByFlagsEnumerator
    {
        public GetGroupsByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, PrefixGroup> affixesByFlag)
        {
            _flags = flags.GetEnumerator();
            _affixesByFlag = affixesByFlag;
            Current = PrefixGroup.Invalid;
        }

        private FlagSet.Enumerator _flags;
        private readonly Dictionary<FlagValue, PrefixGroup> _affixesByFlag;

        public PrefixGroup Current { get; private set; }

        public GetGroupsByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flags.MoveNext())
            {
                if (_affixesByFlag.GetValueOrDefault(_flags.Current) is { } result)
                {
                    Current = result;
                    return true;
                }
            }

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
