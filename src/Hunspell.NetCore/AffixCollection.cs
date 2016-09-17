using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class AffixCollection<TEntry> :
        IEnumerable<AffixEntryGroup<TEntry>>
        where TEntry : AffixEntry
    {
        public static readonly AffixCollection<TEntry> Empty = new AffixCollection<TEntry>
        (
            new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(0),
            new Dictionary<char, AffixEntryWithDetailCollection<TEntry>>(0),
            AffixEntryWithDetailCollection<TEntry>.Empty,
            FlagSet.Empty
        );

        private readonly Dictionary<FlagValue, AffixEntryGroup<TEntry>> affixesByFlag;

        private readonly Dictionary<char, AffixEntryWithDetailCollection<TEntry>> affixesByIndexedKeyCharacter;

        private AffixCollection
        (
            Dictionary<FlagValue, AffixEntryGroup<TEntry>> affixesByFlag,
            Dictionary<char, AffixEntryWithDetailCollection<TEntry>> affixesByIndexedKeyCharacter,
            AffixEntryWithDetailCollection<TEntry> affixesWithEmptyKeys,
            FlagSet contClasses
        )
        {
            this.affixesByFlag = affixesByFlag;
            this.affixesByIndexedKeyCharacter = affixesByIndexedKeyCharacter;
            AffixesWithEmptyKeys = affixesWithEmptyKeys;
            ContClasses = contClasses;
            HasAffixes = affixesByFlag.Count != 0;
            IsEmpty = !HasAffixes;
        }

        public AffixEntryWithDetailCollection<TEntry> AffixesWithEmptyKeys { get; }

        public FlagSet ContClasses { get; }

        public bool HasAffixes { get; }

        public bool IsEmpty { get; }

        public static AffixCollection<TEntry> Create(List<AffixEntryGroup.Builder<TEntry>> builders)
        {
            if (builders == null || builders.Count == 0)
            {
                return Empty;
            }

            var affixesByFlag = new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(builders.Count);
            var affixesByIndexedKeyCharacterBuilders = new Dictionary<char, List<AffixEntryWithDetail<TEntry>>>();
            var affixesWithEmptyKeys = new List<AffixEntryWithDetail<TEntry>>();
            var contClasses = new HashSet<FlagValue>();

            foreach(var builder in builders)
            {
                var group = builder.ToGroup();
                affixesByFlag.Add(group.AFlag, group);

                foreach(var entry in group.Entries)
                {
                    var key = entry.Key;
                    contClasses.UnionWith(entry.ContClass);
                    var entryWithDetail = new AffixEntryWithDetail<TEntry>(group, entry);
                    if (string.IsNullOrEmpty(key))
                    {
                        affixesWithEmptyKeys.Add(entryWithDetail);
                    }
                    else
                    {
                        var indexedChar = key[0];
                        List<AffixEntryWithDetail<TEntry>> keyedAffixes;
                        if (affixesByIndexedKeyCharacterBuilders.TryGetValue(indexedChar, out keyedAffixes))
                        {
                            keyedAffixes.Add(entryWithDetail);
                        }
                        else
                        {
                            affixesByIndexedKeyCharacterBuilders.Add(indexedChar, new List<AffixEntryWithDetail<TEntry>>
                            {
                                entryWithDetail
                            });
                        }
                    }
                }
            }

            var affixesByIndexedKeyCharacter = new Dictionary<char, AffixEntryWithDetailCollection<TEntry>>(
                affixesByIndexedKeyCharacterBuilders.Count);
            foreach(var keyedBuilder in affixesByIndexedKeyCharacterBuilders)
            {
                affixesByIndexedKeyCharacter.Add(keyedBuilder.Key, AffixEntryWithDetailCollection<TEntry>.TakeList(keyedBuilder.Value));
            }

            return new AffixCollection<TEntry>
            (
                affixesByFlag,
                affixesByIndexedKeyCharacter,
                AffixEntryWithDetailCollection<TEntry>.TakeList(affixesWithEmptyKeys),
                FlagSet.Create(contClasses)
            );
        }

        public AffixEntryGroup<TEntry> GetByFlag(FlagValue flag)
        {
            AffixEntryGroup<TEntry> result;
            affixesByFlag.TryGetValue(flag, out result);
            return result;
        }

        public IEnumerable<AffixEntryWithDetail<TEntry>> GetMatchingAffixes(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return Enumerable.Empty<AffixEntryWithDetail<TEntry>>();
            }

            AffixEntryWithDetailCollection<TEntry> dotAffixes;
            affixesByIndexedKeyCharacter.TryGetValue('.', out dotAffixes);

            if (typeof(TEntry) == typeof(PrefixEntry))
            {
                AffixEntryWithDetailCollection<TEntry> characterAffixes;
                if (!word.StartsWith('.'))
                {
                    affixesByIndexedKeyCharacter.TryGetValue(word[0], out characterAffixes);
                }
                else
                {
                    characterAffixes = null;
                }

                return FastUnion(dotAffixes, characterAffixes)
                    .Where(e => StringEx.IsSubset(e.Key, word));
            }
            else if (typeof(TEntry) == typeof(SuffixEntry))
            {
                AffixEntryWithDetailCollection<TEntry> characterAffixes;
                if (!word.EndsWith('.'))
                {
                    affixesByIndexedKeyCharacter.TryGetValue(word[word.Length - 1], out characterAffixes);
                }
                else
                {
                    characterAffixes = null;
                }

                return FastUnion(dotAffixes, characterAffixes)
                    .Where(e => StringEx.IsReverseSubset(e.Key, word));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static IEnumerable<AffixEntryWithDetail<TEntry>> FastUnion(AffixEntryWithDetailCollection<TEntry> a, AffixEntryWithDetailCollection<TEntry> b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return Enumerable.Empty<AffixEntryWithDetail<TEntry>>();
                }
                else
                {
                    return b;
                }
            }
            else
            {
                if (b == null)
                {
                    return a;
                }
                else
                {
                    if (ReferenceEquals(a, b))
                    {
                        return a;
                    }

                    return Enumerable.Union(a, b);
                }
            }
        }

        public IEnumerator<AffixEntryGroup<TEntry>> GetEnumerator()
        {
            return affixesByFlag.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return affixesByFlag.Values.GetEnumerator();
        }
    }
}
