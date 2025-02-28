using System;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    public sealed class Builder
    {
        public Builder() : this(new AffixConfig.Builder().Extract())
        {
        }

        public Builder(AffixConfig affix)
        {
            Affix = affix;
            _entryDetailsByRoot = new();
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

        /// <summary>
        /// Builds a new <see cref="WordList"/> based on the values in this builder.
        /// </summary>
        /// <returns>A new <see cref="WordList"/>.</returns>
        public WordList Build() => BuildOrExtract(extract: false);

        /// <summary>
        /// Builds a new <see cref="WordList"/> based on the values in this builder.
        /// </summary>
        /// <remarks>
        /// This method can leave the builder in an invalid state
        /// but provides better performance for file reads.
        /// </remarks>
        /// <returns>A new <see cref="WordList"/>.</returns>
        public WordList Extract() => BuildOrExtract(extract: true);

        private WordList BuildOrExtract(bool extract)
        {
            TextDictionary<WordEntryDetail[]> entriesByRoot;
            if (extract)
            {
                entriesByRoot = _entryDetailsByRoot;
                _entryDetailsByRoot = new();
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
                    allReplacements = new(_phoneticReplacements.MakeOrExtractArray(extract));
                }
                else if (extract)
                {
                    _phoneticReplacements.AddRange(allReplacements.RawArray);
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

            return new WordList(
                Affix,
                entriesByRoot: entriesByRoot,
                nGramRestrictedFlags: nGramRestrictedFlags,
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
