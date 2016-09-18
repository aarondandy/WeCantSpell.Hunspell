using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed partial class Hunspell
    {
        internal const int MaxWordLen = 176;

        public Hunspell(WordList wordList)
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

        public static Hunspell FromFile(string dictionaryFilePath) =>
            new Hunspell(WordListReader.ReadFile(dictionaryFilePath));

        public static async Task<Hunspell> FromFileAsync(string dictionaryFilePath) =>
            new Hunspell(await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false));

        public static Hunspell FromFile(string dictionaryFilePath, string affixFilePath) =>
            new Hunspell(WordListReader.ReadFile(dictionaryFilePath, affixFilePath));

        public static async Task<Hunspell> FromFileAsync(string dictionaryFilePath, string affixFilePath) =>
            new Hunspell(await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false));

        public bool Check(string word) => new QueryCheck(word, WordList).Check();

        public SpellCheckResult CheckDetails(string word) => new QueryCheck(word, WordList).CheckDetails();

        public List<string> Suggest(string word) => new QuerySuggest(word, WordList).Suggest();
    }
}
