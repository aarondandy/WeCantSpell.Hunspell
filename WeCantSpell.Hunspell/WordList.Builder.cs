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
            _entriesByRoot = [];
        }

        internal TextDictionary<WordEntryDetail[]> _entriesByRoot;

        public readonly AffixConfig Affix;

        /// <summary>
        /// Spelling replacement suggestions based on phonetics.
        /// </summary>
        public IList<SingleReplacement> PhoneticReplacements => _phoneticReplacements;

        internal ArrayBuilder<SingleReplacement> _phoneticReplacements { get; } = [];

        /// <summary>
        /// Adds a root word to this builder.
        /// </summary>
        /// <param name="word">The root word to add.</param>
        /// <returns><c>true</c> when a root is added, <c>false</c> otherwise.</returns>
        public bool Add(string word)
        {
            return Add(word, FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);
        }

        /// <summary>
        /// Adds a root word to this builder.
        /// </summary>
        /// <param name="word">The root word to add.</param>
        /// <param name="flags">The flags associated with the root <paramref name="word"/> detail entry.</param>
        /// <param name="morphs">The morphs associated with the root <paramref name="word"/> detail entry.</param>
        /// <param name="options">The options associated with the root <paramref name="word"/> detail entry.</param>
        /// <returns><c>true</c> when a root is added, <c>false</c> otherwise.</returns>
        public bool Add(string word, FlagSet flags, IEnumerable<string> morphs, WordEntryOptions options)
        {
            return Add(word, new WordEntryDetail(flags, MorphSet.Create(morphs), options));
        }

        /// <summary>
        /// Adds a root word to this builder.
        /// </summary>
        /// <param name="word">The root word to add.</param>
        /// <param name="flags">The flags associated with the root <paramref name="word"/> detail entry.</param>
        /// <param name="morphs">The morphs associated with the root <paramref name="word"/> detail entry.</param>
        /// <param name="options">The options associated with the root <paramref name="word"/> detail entry.</param>
        /// <returns><c>true</c> when a root is added, <c>false</c> otherwise.</returns>
        public bool Add(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
        {
            return Add(word, new WordEntryDetail(flags, morphs, options));
        }

        /// <summary>
        /// Adds a root word to this builder.
        /// </summary>
        /// <param name="word">The root word to add details for.</param>
        /// <param name="detail">The details to associate with the root <paramref name="word"/>.</param>
        /// <returns><c>true</c> when a root is added, <c>false</c> otherwise.</returns>
        public bool Add(string word, WordEntryDetail detail)
        {
            return WordList.Add(_entriesByRoot, Affix, word, detail);
        }

        /// <summary>
        /// Removes all detail entries for the given root <paramref name="word"/>.
        /// </summary>
        /// <param name="word">The root to delete all entries for.</param>
        /// <returns>The count of entries removed.</returns>
        public int Remove(string word)
        {
            return WordList.Remove(_entriesByRoot, Affix, word);
        }

        /// <summary>
        /// Removes a specific detail entry for the given root <paramref name="word"/> and detail arguments.
        /// </summary>
        /// <param name="word">The root word to delete a specific entry for.</param>
        /// <param name="flags">The flags to match on an entry.</param>
        /// <param name="morphs">The morphs to match on an entry.</param>
        /// <param name="options">The options to match on an entry.</param>
        /// <returns><c>true</c> when an entry is remove, otherwise <c>false</c>.</returns>
        public bool Remove(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
        {
            return Remove(word, new WordEntryDetail(flags, morphs, options));
        }

        /// <summary>
        /// Removes a specific <paramref name="detail"/> entry for the given root <paramref name="word"/>.
        /// </summary>
        /// <param name="word">The root word to delete a specific entry for.</param>
        /// <param name="detail">The detail to delete for a specific root.</param>
        /// <returns><c>true</c> when an entry is remove, otherwise <c>false</c>.</returns>
        public bool Remove(string word, WordEntryDetail detail)
        {
            return WordList.Remove(_entriesByRoot, Affix, word, detail);
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
                entriesByRoot = _entriesByRoot;
                _entriesByRoot = [];
            }
            else
            {
                entriesByRoot = TextDictionary<WordEntryDetail[]>.Clone(_entriesByRoot, static v => [.. v]);
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
                _entriesByRoot.EnsureCapacity(expectedCapacity);
            }
        }
    }
}
