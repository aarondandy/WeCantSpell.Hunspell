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
public abstract class AffixCollection<TAffixGroup, TAffixEntry, TAffix> : IEnumerable<TAffixGroup>
    where TAffixGroup : class, IAffixGroup<TAffixEntry, TAffix>
    where TAffixEntry : IAffixEntry
    where TAffix : IAffix
{
    protected AffixCollection(Dictionary<FlagValue, TAffixGroup> affixesByFlag)
    {
        AffixesByFlag = affixesByFlag;

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<TAffix>();
        var affixesWithDots = new ArrayBuilder<TAffix>();
        var affixesByKeyPrefix = new Dictionary<char, ArrayBuilder<TAffix>>();

        foreach (var group in AffixesByFlag.Values)
        {
            foreach (var entry in group.Entries)
            {
                contClassesBuilder.AddRange(entry.ContClass);

                var affix = group.ToAffix(entry);

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

        ContClasses = contClassesBuilder.MoveToFlagSet();
        AffixesWithEmptyKeys = affixesWithEmptyKeys.Extract();
        AffixesWithDots = affixesWithDots.Extract();
        AffixesByKeyPrefix = affixesByKeyPrefix.ToDictionary(static pair => pair.Key, static pair => pair.Value.Extract());
    }

    protected readonly Dictionary<FlagValue, TAffixGroup> AffixesByFlag;
    protected readonly Dictionary<char, TAffix[]> AffixesByKeyPrefix;
    protected readonly TAffix[] AffixesWithDots = Array.Empty<TAffix>();
    protected readonly TAffix[] AffixesWithEmptyKeys = Array.Empty<TAffix>();

    public FlagSet ContClasses { get; protected set; } = FlagSet.Empty;

    public bool HasAffixes => AffixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => AffixesByFlag.Keys;

    public IEnumerator<TAffixGroup> GetEnumerator() => AffixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public TAffixGroup? GetByFlag(FlagValue flag) => AffixesByFlag.GetValueOrDefault(flag);

    internal TAffix[] GetAffixesWithEmptyKeys() => AffixesWithEmptyKeys;

    internal AffixesByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, AffixesByFlag);

    internal GroupsByFlagsEnumerator GetGroupsByFlags(FlagSet flags) => new(flags, AffixesByFlag);

    internal struct AffixesByFlagsEnumerator
    {
        public AffixesByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, TAffixGroup> affixesByFlag)
        {
            _flags = flags.GetEnumerator();
            _byFlag = affixesByFlag;
            _group = null!;
            _groupIndex = 0;
        }

        private int _groupIndex;
        private TAffixGroup _group;
        private readonly Dictionary<FlagValue, TAffixGroup> _byFlag;
        private FlagSet.Enumerator _flags;

        public TAffix Current => _group!.GetAffix(_groupIndex++);

        public AffixesByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (!(_groupIndex < _group?.Entries.Length))
            {
                if (!MoveNextGroup())
                {
                    return false;
                }
            }

            return true;
        }

        private bool MoveNextGroup()
        {
            while (_flags.MoveNext())
            {
                if (_byFlag.TryGetValue(_flags.Current, out _group!) && _group.Entries.Length != 0)
                {
                    _groupIndex = 0;
                    return true;
                }
            }

            return false;
        }
    }

    internal struct GroupsByFlagsEnumerator
    {
        public GroupsByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, TAffixGroup> byFlag)
        {
            _flags = flags.GetEnumerator();
            _byFlag = byFlag;
            _current = default!;
        }

        private readonly Dictionary<FlagValue, TAffixGroup> _byFlag;
        private FlagSet.Enumerator _flags;
        private TAffixGroup _current;

        public TAffixGroup Current => _current;

        public GroupsByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flags.MoveNext())
            {
                if (_byFlag.TryGetValue(_flags.Current, out _current!))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

public sealed class SuffixCollection : AffixCollection<SuffixGroup, SuffixEntry, Suffix>
{
    public static SuffixCollection Empty { get; } = new(new());

    public static SuffixCollection Create(List<SuffixGroup.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static SuffixCollection Create(List<SuffixGroup.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var byFlag = new Dictionary<FlagValue, SuffixGroup>(builders.Count);
        foreach (var builder in builders)
        {
            var group = builder.ToImmutable(allowDestructive: allowDestructive);
            byFlag.Add(group.AFlag, group);
        }

        return new(byFlag);
    }

    private SuffixCollection(Dictionary<FlagValue, SuffixGroup> byFlag) : base(byFlag)
    {
    }

    internal WordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        var simpleText = Array.Empty<Suffix>();
        var withDots = Array.Empty<Suffix>();

        if (!word.IsEmpty)
        {
            if (AffixesByKeyPrefix.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                simpleText = indexedAffixes;
            }

            withDots = AffixesWithDots;
        }

        return new(word, simpleText, withDots);
    }

    internal ref struct WordEnumerator
    {
        public WordEnumerator(ReadOnlySpan<char> word, Suffix[] simpleText, Suffix[] withDots)
        {
            _word = word;
            _simpleText = simpleText;
            _simpleTextIndex = 0;
            _withDots = withDots;
            _withDotsIndex = 0;
            Current = default!;
        }

        private int _simpleTextIndex = 0;
        private int _withDotsIndex = 0;
        private readonly Suffix[] _simpleText;
        private readonly Suffix[] _withDots;
        private readonly ReadOnlySpan<char> _word;

        public Suffix Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            return MoveSimple() || MoveWithDots();
        }

        private bool MoveSimple()
        {
            while (_simpleTextIndex < _simpleText.Length)
            {
                ref var candidate = ref _simpleText[_simpleTextIndex++];
                if (candidate.Entry.IsExactReverseSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }

        private bool MoveWithDots()
        {
            while (_withDotsIndex < _withDots.Length)
            {
                ref var candidate = ref _withDots[_withDotsIndex++];
                if (candidate.Entry.IsReverseSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }
    }

    internal WordFlagEnumerator GetMatchingAffixes(ReadOnlySpan<char> word, FlagSet groupFlagFilter)
    {
        var simpleText = Array.Empty<Suffix>();
        var withDots = Array.Empty<Suffix>();

        if (!word.IsEmpty)
        {
            if (groupFlagFilter.HasItems && AffixesByKeyPrefix.TryGetValue(word[word.Length - 1], out var indexedAffixes))
            {
                simpleText = indexedAffixes;
            }

            withDots = AffixesWithDots;
        }

        return new(word, simpleText, withDots, groupFlagFilter);
    }

    internal ref struct WordFlagEnumerator
    {
        public WordFlagEnumerator(ReadOnlySpan<char> word, Suffix[] simpleText, Suffix[] withDots, FlagSet groupFlagFilter)
        {
            _word = word;
            _simpleText = simpleText;
            _simpleTextIndex = 0;
            _firstFlagFilter = groupFlagFilter;
            _withDots = withDots;
            _withDotsIndex = 0;
            Current = default!;
        }

        private int _simpleTextIndex = 0;
        private int _withDotsIndex = 0;
        private readonly Suffix[] _simpleText;
        private readonly Suffix[] _withDots;
        private readonly FlagSet _firstFlagFilter;
        private readonly ReadOnlySpan<char> _word;

        public Suffix Current { get; private set; }

        public WordFlagEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            return MoveSimple() || MoveWithDots();
        }

        private bool MoveSimple()
        {
            while (_simpleTextIndex < _simpleText.Length)
            {
                ref var candidate = ref _simpleText[_simpleTextIndex++];
                if (_firstFlagFilter.Contains(candidate.AFlag) && candidate.Entry.IsExactReverseSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }

        private bool MoveWithDots()
        {
            while (_withDotsIndex < _withDots.Length)
            {
                ref var candidate = ref _withDots[_withDotsIndex++];
                if (candidate.Entry.IsReverseSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixGroup, PrefixEntry, Prefix>
{
    public static PrefixCollection Empty { get; } = new(new());

    public static PrefixCollection Create(List<PrefixGroup.Builder>? builders) => Create(builders, allowDestructive: false);

    internal static PrefixCollection Create(List<PrefixGroup.Builder>? builders, bool allowDestructive)
    {
        if (builders is not { Count: > 0 })
        {
            return Empty;
        }

        var byFlag = new Dictionary<FlagValue, PrefixGroup>(builders.Count);
        foreach (var builder in builders)
        {
            var group = builder.ToImmutable(allowDestructive: allowDestructive);
            byFlag.Add(group.AFlag, group);
        }

        return new(byFlag);
    }

    private PrefixCollection(Dictionary<FlagValue, PrefixGroup> byFlag) : base(byFlag)
    {
    }

    internal WordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        var simpleText = Array.Empty<Prefix>();
        var withDots = Array.Empty<Prefix>();

        if (!word.IsEmpty)
        {
            if (AffixesByKeyPrefix.TryGetValue(word[0], out var indexedAffixes))
            {
                simpleText = indexedAffixes;
            }

            withDots = AffixesWithDots;
        }

        return new(word, simpleText, withDots);
    }

    internal ref struct WordEnumerator
    {
        public WordEnumerator(ReadOnlySpan<char> word, Prefix[] simpleText, Prefix[] withDots)
        {
            _word = word;
            _simpleText = simpleText;
            _simpleTextIndex = 0;
            _withDots = withDots;
            _withDotsIndex = 0;
            Current = default!;
        }

        private int _simpleTextIndex = 0;
        private int _withDotsIndex = 0;
        private readonly Prefix[] _simpleText;
        private readonly Prefix[] _withDots;
        private readonly ReadOnlySpan<char> _word;

        public Prefix Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            return MoveSimple() || MoveWithDots();
        }

        private bool MoveSimple()
        {
            while (_simpleTextIndex < _simpleText.Length)
            {
                ref var candidate = ref _simpleText[_simpleTextIndex++];
                if (candidate.Entry.IsExactSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }

        private bool MoveWithDots()
        {
            while (_withDotsIndex < _withDots.Length)
            {
                ref var candidate = ref _withDots[_withDotsIndex++];
                if (candidate.Entry.IsSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }
    }
}
