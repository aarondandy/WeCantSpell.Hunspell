using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public partial class WordList
    {
        public sealed class Builder
        {
            public Builder()
                : this(null, null, null, null)
            {
            }

            public Builder(AffixConfig affix)
                : this(affix, null, null, null)
            {
            }

            internal Builder(AffixConfig affix, Deduper<FlagSet> flagSetDeduper, Deduper<MorphSet> morphSet, StringDeduper stringDeduper)
            {
                Affix = affix;
                FlagSetDeduper = flagSetDeduper ?? new Deduper<FlagSet>(FlagSet.DefaultComparer);
                FlagSetDeduper.Add(FlagSet.Empty);
                StringDeduper = stringDeduper ?? new StringDeduper();
                StringDeduper.Add(MorphologicalTags.Phon);
                MorphSetDeduper = morphSet ?? new Deduper<MorphSet>(MorphSet.DefaultComparer);
                MorphSetDeduper.Add(MorphSet.Empty);
            }

            public Dictionary<string, WordEntrySet> EntriesByRoot;

            public readonly AffixConfig Affix;

            internal readonly Deduper<FlagSet> FlagSetDeduper;

            internal readonly Deduper<MorphSet> MorphSetDeduper;

            internal readonly StringDeduper StringDeduper;

            public WordList ToImmutable() =>
                ToImmutable(destructive: false);

            public WordList MoveToImmutable() =>
                ToImmutable(destructive: true);

            private WordList ToImmutable(bool destructive)
            {
                var affix = Affix ?? new AffixConfig.Builder().MoveToImmutable();

                var nGramRestrictedFlags = Dedup(FlagSet.Create(
                    new[]
                    {
                        affix.ForbiddenWord,
                        affix.NoSuggest,
                        affix.NoNgramSuggest,
                        affix.OnlyInCompound,
                        SpecialFlags.OnlyUpcaseFlag
                    }
                    .Where(f => f.HasValue)));

                var result = new WordList(affix)
                {
                    NGramRestrictedFlags = nGramRestrictedFlags,
                };

                if (destructive)
                {
                    result.EntriesByRoot = EntriesByRoot ?? new Dictionary<string, WordEntrySet>();
                    EntriesByRoot = null;
                }
                else
                {
                    result.EntriesByRoot = EntriesByRoot == null
                        ? new Dictionary<string, WordEntrySet>()
                        : new Dictionary<string, WordEntrySet>(EntriesByRoot);
                }

                var nGramRestrictedEntries = new HashSet<WordEntry>();

                foreach (var rootSet in result.EntriesByRoot)
                {
                    foreach (var entry in rootSet.Value)
                    {
                        if (nGramRestrictedFlags.ContainsAny(entry.Flags))
                        {
                            nGramRestrictedEntries.Add(entry);
                        }
                    }
                }

                result.NGramRestrictedEntries = nGramRestrictedEntries;

                return result;
            }

            public void InitializeEntriesByRoot(int expectedSize)
            {
                EntriesByRoot = expectedSize < 0
                    ? new Dictionary<string, WordEntrySet>()
                    // PERF: because we add more entries than we are told about, we add a bit more to the expected size
                    : new Dictionary<string, WordEntrySet>((expectedSize / 100) + expectedSize);
            }

            public FlagSet Dedup(FlagSet value) =>
                value == null ? null : FlagSetDeduper.GetEqualOrAdd(value);

            public string Dedup(string value) =>
                value == null ? null : StringDeduper.GetEqualOrAdd(value);

            public MorphSet Dedup(MorphSet value) =>
                value == null ? null : MorphSetDeduper.GetEqualOrAdd(value);
        }
    }
}
