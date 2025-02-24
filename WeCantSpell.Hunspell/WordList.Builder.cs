using System;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    public sealed class Builder
    {
        public Builder() : this(new AffixConfig.Builder().MoveToImmutable())
        {
        }

        public Builder(AffixConfig affix)
        {
            Affix = affix;
            _entryDetailsByRoot = new(1);
        }

        internal TextDictionary<WordEntryDetail[]> _entryDetailsByRoot;

        public readonly AffixConfig Affix;

        /// <summary>
        /// Spelling replacement suggestions based on phonetics.
        /// </summary>
        public IList<SingleReplacement> PhoneticReplacements => _phoneticReplacements;

        internal ArrayBuilder<SingleReplacement> _phoneticReplacements { get; } = [];

        public void Add(string word)
        {
            Add(word, WordEntryDetail.Default);
        }

        public void Add(string word, WordEntryDetail detail)
        {
            ref var details = ref _entryDetailsByRoot.GetOrAdd(word);
            if (details is null)
            {
                details = [detail];
            }
            else
            {
                Array.Resize(ref details, details.Length + 1);
                details[details.Length - 1] = detail;
            }
        }

        [Obsolete]
        public WordList ToImmutable() => ToImmutable(allowDestructive: false);

        [Obsolete]
        public WordList MoveToImmutable() => ToImmutable(allowDestructive: true);

        [Obsolete]
        public WordList ToImmutable(bool allowDestructive)
        {
            TextDictionary<WordEntryDetail[]> entriesByRoot;
            if (allowDestructive)
            {
                entriesByRoot = _entryDetailsByRoot;
                _entryDetailsByRoot = new(1);
            }
            else
            {
                entriesByRoot = TextDictionary<WordEntryDetail[]>.Clone(_entryDetailsByRoot, static v => [.. v]);
            }

            var allReplacements = Affix.Replacements;
            if (_phoneticReplacements is { Count: > 0 })
            {
                // store ph: field of a morphological description in reptable
                if (allReplacements.IsEmpty)
                {
                    allReplacements = new(_phoneticReplacements.MakeOrExtractArray(allowDestructive));
                }
                else if (allowDestructive)
                {
                    _phoneticReplacements.AddRange(allReplacements);
                    allReplacements = new(_phoneticReplacements.Extract());
                }
                else
                {
                    allReplacements = SingleReplacementSet.Create(_phoneticReplacements.Concat(allReplacements));
                }
            }

            var nGramRestrictedFlags = FlagSet.Create(
            [
                Affix.ForbiddenWord,
                Affix.NoSuggest,
                Affix.NoNgramSuggest,
                Affix.OnlyInCompound,
                SpecialFlags.OnlyUpcaseFlag
            ]);
            TextDictionary<WordEntryDetail[]> nGramRestrictedDetails = new(1);
            var restrictedRootSetDetails = new ArrayBuilder<WordEntryDetail>();
            foreach (var rootSet in entriesByRoot)
            {
                restrictedRootSetDetails.Clear();

                foreach (var entry in rootSet.Value)
                {
                    if (nGramRestrictedFlags.ContainsAny(entry.Flags))
                    {
                        restrictedRootSetDetails.Add(entry);
                    }
                }

                if (restrictedRootSetDetails.Count > 0)
                {
                    nGramRestrictedDetails.Add(rootSet.Key, restrictedRootSetDetails.Extract());
                }
            }

            return new WordList(
                Affix,
                entriesByRoot: entriesByRoot,
                nGramRestrictedDetails: nGramRestrictedDetails,
                allReplacements);
        }

        public void InitializeEntriesByRoot(int expectedSize)
        {
            if (expectedSize > 0)
            {
                // PERF: because we add more entries than we are told about, we add a bit more to the expected size
                var expectedCapacity = (expectedSize / 100) + expectedSize;
                _entryDetailsByRoot.EnsureCapacity(expectedCapacity);
            }
        }
    }
}
