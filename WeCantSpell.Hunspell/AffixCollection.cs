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
public abstract class AffixCollection<TAffixGroup, TAffixEntry> : IEnumerable<TAffixGroup>
    where TAffixGroup : class, IAffixGroup<TAffixEntry>
    where TAffixEntry : IAffixEntry
{
    internal AffixCollection()
    {
    }

    protected AffixCollection(Dictionary<FlagValue, TAffixGroup> affixesByFlag)
    {
        AffixesByFlag = affixesByFlag;

        var contClassesBuilder = new FlagSet.Builder();
        var affixesWithEmptyKeys = new ArrayBuilder<TAffixEntry>();
        var affixesWithDots = new ArrayBuilder<TAffixEntry>();
        var affixesByKeyPrefix = new Dictionary<char, ArrayBuilder<TAffixEntry>>();
        var affixesByFirstKeyChar = new Dictionary<char, List<TAffixEntry>>();

        foreach (var group in AffixesByFlag.Values)
        {
            foreach (var affix in group.Entries)
            {
                contClassesBuilder.AddRange(affix.ContClass);

                if (string.IsNullOrEmpty(affix.Key))
                {
                    affixesWithEmptyKeys.Add(affix);
                }
                else
                {
                    var indexedKey = affix.Key[0];

                    if (!affixesByFirstKeyChar.TryGetValue(indexedKey, out var firstKeyList))
                    {
                        firstKeyList = new();
                        affixesByFirstKeyChar.Add(indexedKey, firstKeyList);
                    }

                    firstKeyList.Add(affix);

                    if (affix.Key.Contains('.'))
                    {
                        affixesWithDots.Add(affix);
                    }
                    else
                    {
                        if (!affixesByKeyPrefix.TryGetValue(indexedKey, out var indexGroup))
                        {
                            indexGroup = new();
                            affixesByKeyPrefix.Add(indexedKey, indexGroup);
                        }

                        indexGroup.Add(affix);
                    }

                }
            }
        }

        ContClasses = contClassesBuilder.MoveToFlagSet();
        AffixesWithEmptyKeys = affixesWithEmptyKeys.Extract();
        AffixesWithDots = affixesWithDots.Extract();
        AffixesByKeyPrefix = affixesByKeyPrefix.ToDictionary(static pair => pair.Key, static pair => pair.Value.Extract());

        foreach (var (firstChar, affixes) in affixesByFirstKeyChar)
        {
            EntryTreeNode? root = null;

            foreach (var affix in affixes)
            {
                var newNode = new EntryTreeNode(affix);
                if (root is null)
                {
                    root = newNode;
                    continue;
                }

                var search = root;

                do
                {
                    var parent = search;
                    if (affix.Key.CompareTo(search.Affix.Key) <= 0)
                    {
                        search = search.EqualOrSomething;
                        if (search is null)
                        {
                            parent.EqualOrSomething = newNode;
                            break;
                        }
                    }
                    else
                    {
                        search = search.NotEqual;
                        if (search is null)
                        {
                            parent.NotEqual = newNode;
                            break;
                        }
                    }
                }
                while (true);
            }

            if (root is not null)
            {
                AffixTreeRootsByFirstKeyChar.Add(firstChar, root);
            }
        }


    }

    internal sealed class EntryTreeNode
    {
        public EntryTreeNode(TAffixEntry affix)
        {
            Affix = affix;
        }

        public TAffixEntry Affix { get; }
        public EntryTreeNode? EqualOrSomething { get; set; }
        public EntryTreeNode? NotEqual { get; set; }
        public EntryTreeNode? Next { get; set; }
    }

    protected Dictionary<FlagValue, TAffixGroup> AffixesByFlag;
    protected Dictionary<char, TAffixEntry[]> AffixesByKeyPrefix;
    internal Dictionary<char, EntryTreeNode> AffixTreeRootsByFirstKeyChar = new();
    protected TAffixEntry[] AffixesWithDots = Array.Empty<TAffixEntry>();
    protected TAffixEntry[] AffixesWithEmptyKeys = Array.Empty<TAffixEntry>();

    public FlagSet ContClasses { get; protected set; } = FlagSet.Empty;

    public bool HasAffixes => AffixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => AffixesByFlag.Keys;

    public IEnumerator<TAffixGroup> GetEnumerator() => AffixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public TAffixGroup? GetByFlag(FlagValue flag) => AffixesByFlag.GetValueOrDefault(flag);

    internal TAffixEntry[] GetAffixesWithEmptyKeys() => AffixesWithEmptyKeys;

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
            Current = default!;
        }

        private int _groupIndex;
        private TAffixGroup _group;
        private Dictionary<FlagValue, TAffixGroup> _byFlag;
        private FlagSet.Enumerator _flags;

        public TAffixEntry Current { get; private set; }

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

            Current = _group!.Entries[_groupIndex++];
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

        private Dictionary<FlagValue, TAffixGroup> _byFlag;
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

public sealed class SuffixCollection : AffixCollection<SuffixGroup, SuffixEntry>
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

    internal WordEnumerator GetMatchingAffixesX(ReadOnlySpan<char> word)
    {
        if (!word.IsEmpty && AffixTreeRootsByFirstKeyChar.TryGetValue(word[word.Length - 1], out var root))
        {
            return new(word, root);
        }

        return new(word, null);
    }

    internal ref struct WordEnumerator
    {
        internal WordEnumerator(ReadOnlySpan<char> word, EntryTreeNode? node)
        {
            _word = word;
            _node = node;
            Current = default!;
        }

        private EntryTreeNode? _node;
        private ReadOnlySpan<char> _word;

        public SuffixEntry Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_node is not null)
            {
                if (_node.Affix.IsReverseSubset(_word))
                {
                    Current = _node.Affix;
                    _node = _node.EqualOrSomething;
                    return true;
                }
                else
                {
                    _node = _node.NotEqual;
                }
            }

            return false;
        }
    }

    internal WordEnumerator2 GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        var simpleText = Array.Empty<SuffixEntry>();
        var withDots = Array.Empty<SuffixEntry>();

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

    internal ref struct WordEnumerator2
    {
        public WordEnumerator2(ReadOnlySpan<char> word, SuffixEntry[] simpleText, SuffixEntry[] withDots)
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
        private SuffixEntry[] _simpleText;
        private SuffixEntry[] _withDots;
        private ReadOnlySpan<char> _word;

        public SuffixEntry Current { get; private set; }

        public WordEnumerator2 GetEnumerator() => this;

        public bool MoveNext()
        {
            return MoveSimple() || MoveWithDots();
        }

        private bool MoveSimple()
        {
            while (_simpleTextIndex < _simpleText.Length)
            {
                ref var candidate = ref _simpleText[_simpleTextIndex++];
                if (candidate.IsExactReverseSubset(_word))
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
                if (candidate.IsReverseSubset(_word))
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
        var simpleText = Array.Empty<SuffixEntry>();
        var withDots = Array.Empty<SuffixEntry>();

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
        public WordFlagEnumerator(ReadOnlySpan<char> word, SuffixEntry[] simpleText, SuffixEntry[] withDots, FlagSet groupFlagFilter)
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
        private SuffixEntry[] _simpleText;
        private SuffixEntry[] _withDots;
        private FlagSet _firstFlagFilter;
        private ReadOnlySpan<char> _word;

        public SuffixEntry Current { get; private set; }

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
                if (_firstFlagFilter.Contains(candidate.AFlag) && candidate.IsExactReverseSubset(_word))
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
                if (candidate.IsReverseSubset(_word))
                {
                    Current = candidate;
                    return true;
                }
            }

            return false;
        }
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixGroup, PrefixEntry>
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

    private PrefixCollection() : base()
    {
    }

    private PrefixCollection(Dictionary<FlagValue, PrefixGroup> byFlag) : base(byFlag)
    {
    }

    internal WordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        if (word.IsEmpty || !AffixTreeRootsByFirstKeyChar.TryGetValue(word[0], out var node))
        {
            node = null;
        }

        return new(word, node);
    }

    internal ref struct WordEnumerator
    {
        public WordEnumerator(ReadOnlySpan<char> word, EntryTreeNode? node)
        {
            _word = word;
            _node = node;
            Current = default!;
        }

        private EntryTreeNode? _node;
        private ReadOnlySpan<char> _word;

        public PrefixEntry Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_node is not null)
            {
                if (_node.Affix.IsSubset(_word))
                {
                    Current = _node.Affix;
                    _node = _node.EqualOrSomething;
                    return true;
                }
                else
                {
                    _node = _node.NotEqual;
                }
            }

            return false;
        }
    }

    public class Builder
    {
        private Dictionary<FlagValue, PrefixGroup.Builder> _byFlag = new();
        private ArrayBuilder<PrefixEntry> _emptyKeys = new();
        private FlagSet.Builder _contClassesBuilder = new();
        private Dictionary<char, List<PrefixEntry>> _byFirstKeyChar = new();

        public PrefixCollection BuildCollection(bool allowDestructive)
        {
            var result = new PrefixCollection();
            result.ContClasses = allowDestructive ? _contClassesBuilder.MoveToFlagSet() : _contClassesBuilder.Create();
            result.AffixesByFlag = _byFlag.ToDictionary(static x => x.Key, x => x.Value.ToImmutable(allowDestructive: allowDestructive));
            result.AffixesWithEmptyKeys = _emptyKeys.MakeOrExtractArray(allowDestructive);
            result.AffixTreeRootsByFirstKeyChar = new();

            // loop through each prefix list starting point
            foreach (var (firstChar, affixes) in _byFirstKeyChar)
            {
                if (affixes.Count == 0)
                {
                    continue;
                }

                // convert from binary tree to sorted list
                // NOTE: the above comment is a lie, but that is what this sort and initial tree build is for
                affixes.Sort(static (a, b) => StringComparer.Ordinal.Compare(a.Key, b.Key));
                var allNodes = affixes.ConvertAll(static affix => new EntryTreeNode(affix));
                for (var i = 1; i < allNodes.Count; i++)
                {
                    allNodes[i - 1].Next = allNodes[i];
                }

                // look through the remainder of the list
                // and find next entry with affix that
                // the current one is not a subset of
                // mark that as destination for NextNE
                // use next in list that you are a subset
                // of as NextEQ

                foreach (var ptr in allNodes)
                {
                    var nptr = ptr.Next;
                    for (; nptr is not null; nptr = nptr.Next)
                    {
                        if (!ptr.Affix.IsSubset(nptr.Affix.Key))
                        {
                            break;
                        }
                    }

                    ptr.NotEqual = nptr;
                    ptr.EqualOrSomething = null;

                    if (ptr.Next is not null && ptr.Affix.IsSubset(ptr.Next.Affix.Key))
                    {
                        ptr.EqualOrSomething = ptr.Next;
                    }
                }

                // now clean up by adding smart search termination strings:
                // if you are already a superset of the previous prefix
                // but not a subset of the next, search can end here
                // so set NextNE properly

                foreach (var ptr in allNodes)
                {
                    var nptr = ptr.Next;
                    EntryTreeNode? mptr = null;
                    for (; nptr is not null; nptr = nptr.Next)
                    {
                        if (!ptr.Affix.IsSubset(nptr.Affix.Key))
                        {
                            break;
                        }

                        mptr = nptr;
                    }

                    if (mptr is not null)
                    {
                        mptr.NotEqual = null;
                    }
                }

                result.AffixTreeRootsByFirstKeyChar.Add(firstChar, allNodes[0]);
            }

            return result;
        }

        public bool HasEncounteredAFlag(FlagValue aFlag)
        {
            return _byFlag.ContainsKey(aFlag);
        }

        public void PrepareGroup(FlagValue aFlag, AffixEntryOptions options, int expectedEntryCount)
        {
            if (!_byFlag.TryGetValue(aFlag, out var group))
            {
                group = new PrefixGroup.Builder(aFlag, options);
                _byFlag.Add(aFlag, group);
            }

            if (expectedEntryCount is > 0 and <= 1000)
            {
                group.Entries.Capacity = expectedEntryCount;
            }
        }

        public void AddEntry(
            FlagValue aFlag,
            string strip,
            string affixText,
            CharacterConditionGroup conditions,
            MorphSet morph,
            FlagSet contClass)
        {
            if (!_byFlag.TryGetValue(aFlag, out var group))
            {
                return;
            }

            var entry = new PrefixEntry(
                strip,
                affixText,
                conditions,
                morph,
                contClass,
                aFlag,
                group.Options);

            group.Entries.Add(entry);

            _contClassesBuilder.AddRange(entry.ContClass);

            if (string.IsNullOrEmpty(entry.Key))
            {
                _emptyKeys.Add(entry);
            }
            else
            {
                var firstChar = entry.Key[0];
                if (!_byFirstKeyChar.TryGetValue(firstChar, out var byKeyGroup))
                {
                    byKeyGroup = new();
                    _byFirstKeyChar.Add(firstChar, byKeyGroup);
                }

                byKeyGroup.Add(entry);
            }
        }
    }
}
