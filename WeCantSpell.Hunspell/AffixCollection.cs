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
/// An <see cref="AffixGroup{TAffixEntry}"/> is set of affix objects
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
/// In the first line has 4 fields which define the <see cref="AffixGroup{TAffixEntry}"/> for this affix that will contain four <see cref="SuffixEntry"/> objects.
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
public abstract class AffixCollection<TAffixEntry> : IEnumerable<AffixGroup<TAffixEntry>>
    where TAffixEntry : AffixEntry
{
    protected AffixCollection()
    {
        _affixesByFlag = null!;
    }

    private Dictionary<FlagValue, AffixGroup<TAffixEntry>> _affixesByFlag;
    private TAffixEntry[] _affixesWithEmptyKeys = Array.Empty<TAffixEntry>();
    private Dictionary<char, EntryTreeNode> _affixTreeRootsByFirstKeyChar = new();

    public FlagSet ContClasses { get; protected set; } = FlagSet.Empty;

    public bool HasAffixes => _affixesByFlag.Count != 0;

    public IEnumerable<FlagValue> FlagValues => _affixesByFlag.Keys;

    public IEnumerator<AffixGroup<TAffixEntry>> GetEnumerator() => _affixesByFlag.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public AffixGroup<TAffixEntry>? GetByFlag(FlagValue flag) => _affixesByFlag.GetValueOrDefault(flag);

    internal TAffixEntry[] GetAffixesWithEmptyKeys() => _affixesWithEmptyKeys;

    internal AffixesByFlagsEnumerator GetByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal GroupsByFlagsEnumerator GetGroupsByFlags(FlagSet flags) => new(flags, _affixesByFlag);

    internal WordEnumerator GetMatchingAffixes(ReadOnlySpan<char> word)
    {
        if (word.IsEmpty || !_affixTreeRootsByFirstKeyChar.TryGetValue(GetKeyIndexValueForWord(word), out var node))
        {
            node = null;
        }

        return new(word, node);
    }

    protected abstract char GetKeyIndexValueForWord(ReadOnlySpan<char> word);

    public abstract class BuilderBase
    {
        protected BuilderBase()
        {
        }

        protected Dictionary<FlagValue, GroupBuilder> _byFlag = new();
        protected ArrayBuilder<TAffixEntry> _emptyKeys = new();
        protected FlagSet.Builder _contClassesBuilder = new();
        protected Dictionary<char, List<TAffixEntry>> _byFirstKeyChar = new();

        public GroupBuilder ForGroup(FlagValue aFlag)
        {
            if (!_byFlag.TryGetValue(aFlag, out var groupBuilder))
            {
                groupBuilder = new(this, aFlag);
                _byFlag.Add(aFlag, groupBuilder);
            }

            return groupBuilder;
        }

        protected void ApplyToCollection(AffixCollection<TAffixEntry> target, bool allowDestructive)
        {
            target.ContClasses = allowDestructive ? _contClassesBuilder.MoveToFlagSet() : _contClassesBuilder.Create();
            target._affixesByFlag = _byFlag.ToDictionary(static x => x.Key, x => x.Value.ToImmutable(allowDestructive: allowDestructive));
            target._affixesWithEmptyKeys = _emptyKeys.MakeOrExtractArray(allowDestructive);
            target._affixTreeRootsByFirstKeyChar = new();

            // loop through each prefix list starting point
            foreach (var (firstChar, affixes) in _byFirstKeyChar)
            {
                if (BuildTree(affixes, allowDestructive: allowDestructive) is { } root)
                {
                    target._affixTreeRootsByFirstKeyChar.Add(firstChar, root);
                }
            }
        }

        private static EntryTreeNode? BuildTree(List<TAffixEntry> affixes, bool allowDestructive)
        {
            if (affixes.Count == 0)
            {
                return null;
            }

            // convert from binary tree to sorted list
            // NOTE: the above comment is a lie, but that is what this sort and initial tree build is for

            int i;
            EntryTreeNode[] allNodes;
            if (allowDestructive)
            {
                affixes.Sort(static (a, b) => StringComparer.Ordinal.Compare(a.Key, b.Key));
                allNodes = new EntryTreeNode[affixes.Count];

                i = allNodes.Length - 1;
                allNodes[i] = new(affixes[affixes.Count - 1]);

                while (--i >= 0)
                {
                    ref var node = ref allNodes[i];
                    node = new(affixes[i]);
                    node.Next = allNodes[i + 1];
                }
            }
            else
            {
                allNodes = new EntryTreeNode[affixes.Count];
                for (i = 0; i < allNodes.Length; i++)
                {
                    allNodes[i] = new(affixes[i]);
                }

                Array.Sort(allNodes, static (a, b) => StringComparer.Ordinal.Compare(a.Affix.Key, b.Affix.Key));

                for (i = 1; i < allNodes.Length; i++)
                {
                    allNodes[i - 1].Next = allNodes[i];
                }
            }

            EntryTreeNode? nptr = null;

            // look through the remainder of the list
            // and find next entry with affix that
            // the current one is not a subset of
            // mark that as destination for NextNE
            // use next in list that you are a subset
            // of as NextEQ

            foreach (var ptr in allNodes)
            {
                nptr = ptr.Next;
                while (nptr is not null)
                {
                    if (!ptr.Affix.IsKeySubset(nptr.Affix.Key))
                    {
                        break;
                    }

                    nptr = nptr.Next;
                }

                ptr.NextNotEqual = nptr;
                // ptr.NextEqual = null;

                if (ptr.Next is not null && ptr.Affix.IsKeySubset(ptr.Next.Affix.Key))
                {
                    ptr.NextEqual = ptr.Next;
                }
            }

            // now clean up by adding smart search termination strings:
            // if you are already a superset of the previous affix
            // but not a subset of the next, search can end here
            // so set NextNE properly

            foreach (var ptr in allNodes)
            {
                nptr = ptr.Next;
                EntryTreeNode? mptr = null;
                while (nptr is not null)
                {
                    if (!ptr.Affix.IsKeySubset(nptr.Affix.Key))
                    {
                        break;
                    }

                    mptr = nptr;
                    nptr = nptr.Next;
                }

                if (mptr is not null)
                {
                    mptr.NextNotEqual = null;
                }
            }

            return allNodes[0];
        }

        public sealed class GroupBuilder
        {
            internal GroupBuilder(BuilderBase parent, FlagValue aFlag)
            {
                _parent = parent;
                Builder = new(aFlag, AffixEntryOptions.None);
            }

            private readonly BuilderBase _parent;

            public AffixGroup<TAffixEntry>.Builder Builder { get; }

            public FlagValue AFlag => Builder.AFlag;

            public AffixEntryOptions Options { get => Builder.Options; set => Builder.Options = value; }

            public bool IsInitialized { get; private set; }

            public void Initialize(AffixEntryOptions options, int expectedCapacity)
            {
                Options = options;

                if (expectedCapacity is > 0 and <= CollectionsEx.CollectionPreallocationLimit)
                {
                    Builder.Entries.Capacity = expectedCapacity;
                }

                IsInitialized = true;
            }

            public void AddEntry(
                string strip,
                string affixText,
                CharacterConditionGroup conditions,
                MorphSet morph,
                FlagSet contClass)
            {
                var entry = CreateEntry(strip, affixText, conditions, morph, contClass);

                Builder.Entries.Add(entry);

                _parent._contClassesBuilder.AddRange(entry.ContClass);

                if (string.IsNullOrEmpty(entry.Key))
                {
                    _parent._emptyKeys.Add(entry);
                }
                else
                {
                    var firstChar = entry.Key[0];
                    if (!_parent._byFirstKeyChar.TryGetValue(firstChar, out var byKeyGroup))
                    {
                        byKeyGroup = new();
                        _parent._byFirstKeyChar.Add(firstChar, byKeyGroup);
                    }

                    byKeyGroup.Add(entry);
                }
            }

            public AffixGroup<TAffixEntry> ToImmutable(bool allowDestructive) =>
                Builder.ToImmutable(allowDestructive: allowDestructive);

            private TAffixEntry CreateEntry(string strip,
                string affixText,
                CharacterConditionGroup conditions,
                MorphSet morph,
                FlagSet contClass)
            {
                if (typeof(TAffixEntry) == typeof(PrefixEntry))
                {
                    return (TAffixEntry)(AffixEntry)new PrefixEntry(
                        strip,
                        affixText,
                        conditions,
                        morph,
                        contClass,
                        AFlag,
                        Options);
                }
                else
                {
                    return (TAffixEntry)(AffixEntry)new SuffixEntry(
                        strip,
                        affixText,
                        conditions,
                        morph,
                        contClass,
                        AFlag,
                        Options);
                }
            }
        }
    }

    internal struct AffixesByFlagsEnumerator
    {
        public AffixesByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, AffixGroup<TAffixEntry>> affixesByFlag)
        {
            _flags = flags.GetInternalArray() ?? Array.Empty<FlagValue>();
            _flagsIndex = 0;
            _byFlag = affixesByFlag;
            _group = null!;
            _groupIndex = 0;
            _current = default!;
        }

        private AffixGroup<TAffixEntry> _group;
        private Dictionary<FlagValue, AffixGroup<TAffixEntry>> _byFlag;
        private FlagValue[] _flags;
        private TAffixEntry _current;
        private int _flagsIndex;
        private int _groupIndex;

        public TAffixEntry Current => _current;

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

            _current = _group!.Entries[_groupIndex++];
            return true;
        }

        private bool MoveNextGroup()
        {
            while (_flagsIndex < _flags.Length)
            {
                if (_byFlag.TryGetValue(_flags[_flagsIndex++], out _group!) && _group.Entries.Length != 0)
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
        public GroupsByFlagsEnumerator(FlagSet flags, Dictionary<FlagValue, AffixGroup<TAffixEntry>> byFlag)
        {
            _flags = flags.GetInternalArray() ?? Array.Empty<FlagValue>();
            _flagsIndex = 0;
            _byFlag = byFlag;
            _current = default!;
        }

        private Dictionary<FlagValue, AffixGroup<TAffixEntry>> _byFlag;
        private AffixGroup<TAffixEntry> _current;
        private FlagValue[] _flags;
        private int _flagsIndex;

        public AffixGroup<TAffixEntry> Current => _current;

        public GroupsByFlagsEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_flagsIndex < _flags.Length)
            {
                if (_byFlag.TryGetValue(_flags[_flagsIndex++], out _current!))
                {
                    return true;
                }
            }

            return false;
        }
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

        public TAffixEntry Current { get; private set; }

        public WordEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_node is not null)
            {
                if (_node.Affix.IsWordSubset(_word))
                {
                    Current = _node.Affix;
                    _node = _node.NextEqual;
                    return true;
                }
                else
                {
                    _node = _node.NextNotEqual;
                }
            }

            return false;
        }
    }

    internal sealed class EntryTreeNode
    {
        public EntryTreeNode(TAffixEntry affix)
        {
            Affix = affix;
        }

        public TAffixEntry Affix { get; }
        public EntryTreeNode? NextEqual { get; set; }
        public EntryTreeNode? NextNotEqual { get; set; }
        public EntryTreeNode? Next { get; set; }
    }
}

public sealed class SuffixCollection : AffixCollection<SuffixEntry>
{
    public static SuffixCollection Empty { get; } = new Builder().BuildCollection(allowDestructive: true);

    private SuffixCollection()
    {
    }

    protected override char GetKeyIndexValueForWord(ReadOnlySpan<char> word) => word[word.Length - 1];

    public sealed class Builder : BuilderBase
    {
        public SuffixCollection BuildCollection(bool allowDestructive)
        {
            var result = new SuffixCollection();
            ApplyToCollection(result, allowDestructive: allowDestructive);
            return result;
        }
    }
}

public sealed class PrefixCollection : AffixCollection<PrefixEntry>
{
    public static PrefixCollection Empty { get; } = new Builder().BuildCollection(allowDestructive: true);

    private PrefixCollection()
    {
    }

    protected override char GetKeyIndexValueForWord(ReadOnlySpan<char> word) => word[0];

    public sealed class Builder : BuilderBase
    {
        public PrefixCollection BuildCollection(bool allowDestructive)
        {
            var result = new PrefixCollection();
            ApplyToCollection(result, allowDestructive: allowDestructive);
            return result;
        }
    }
}
