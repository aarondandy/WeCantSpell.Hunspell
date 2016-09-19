using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed partial class HunspellDictionary
    {
        internal const int MaxWordLen = 176;

        public HunspellDictionary(WordList wordList)
        {
            if (wordList == null)
            {
                throw new ArgumentNullException(nameof(wordList));
            }

            WordList = wordList;
        }

        public WordList WordList { get; }

        public AffixConfig Affix
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return WordList.Affix;
            }
        }

        public static HunspellDictionary FromFile(string dictionaryFilePath) =>
            new HunspellDictionary(WordListReader.ReadFile(dictionaryFilePath));

        public static async Task<HunspellDictionary> FromFileAsync(string dictionaryFilePath) =>
            new HunspellDictionary(await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false));

        public static HunspellDictionary FromFile(string dictionaryFilePath, string affixFilePath) =>
            new HunspellDictionary(WordListReader.ReadFile(dictionaryFilePath, affixFilePath));

        public static async Task<HunspellDictionary> FromFileAsync(string dictionaryFilePath, string affixFilePath) =>
            new HunspellDictionary(await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false));

        public bool Check(string word) => new QueryCheck(word, WordList).Check();

        public SpellCheckResult CheckDetails(string word) => new QueryCheck(word, WordList).CheckDetails();

        public IEnumerable<string> Suggest(string word) => new QuerySuggest(word, WordList).Suggest();
    }
}
