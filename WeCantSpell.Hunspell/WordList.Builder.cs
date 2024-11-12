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

        public WordList ToImmutable() => ToImmutable(allowDestructive: false);

        public WordList MoveToImmutable() => ToImmutable(allowDestructive: true);

        public WordList ToImmutable(bool allowDestructive)
        {
            var result = new WordList(Affix, nGramRestrictedFlags: FlagSet.Create(
            [
                Affix.ForbiddenWord,
                Affix.NoSuggest,
                Affix.NoNgramSuggest,
                Affix.OnlyInCompound,
                SpecialFlags.OnlyUpcaseFlag
            ]));

            if (allowDestructive)
            {
                result.EntriesByRoot = _entryDetailsByRoot;
                _entryDetailsByRoot = new(1);
            }
            else
            {
                result.EntriesByRoot = TextDictionary<WordEntryDetail[]>.Clone(_entryDetailsByRoot, static v => [.. v]);
            }

            result.AllReplacements = Affix.Replacements;
            if (_phoneticReplacements is { Count: > 0 })
            {
                // store ph: field of a morphological description in reptable
                if (result.AllReplacements.IsEmpty)
                {
                    result.AllReplacements = new(_phoneticReplacements.MakeOrExtractArray(allowDestructive));
                }
                else if (allowDestructive)
                {
                    _phoneticReplacements.AddRange(result.AllReplacements);
                    result.AllReplacements = new(_phoneticReplacements.Extract());
                }
                else
                {
                    result.AllReplacements = SingleReplacementSet.Create(_phoneticReplacements.MakeArray().Concat(result.AllReplacements));
                }
            }

            var details = new ArrayBuilder<WordEntryDetail>();
            foreach (var rootSet in result.EntriesByRoot)
            {
                details.Clear();
                details.GrowToCapacity(1);

                foreach (var entry in rootSet.Value)
                {
                    if (result.NGramRestrictedFlags.ContainsAny(entry.Flags))
                    {
                        details.Add(entry);
                    }
                }

                if (details.Count != 0)
                {
                    result.NGramRestrictedDetails.Add(rootSet.Key, details.Extract());
                }
            }

            return result;
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
