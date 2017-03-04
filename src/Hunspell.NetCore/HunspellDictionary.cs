using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace Hunspell
{
    public sealed partial class HunspellDictionary
    {
        internal const int MaxWordLen = 100;

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

        public static HunspellDictionary Read(Stream dictionaryStream, Stream affixStream) =>
            new HunspellDictionary(WordListReader.Read(dictionaryStream, affixStream));

#if !NO_IO_FILE
        public static HunspellDictionary FromFile(string dictionaryFilePath) =>
            new HunspellDictionary(WordListReader.ReadFile(dictionaryFilePath));

        public static HunspellDictionary FromFile(string dictionaryFilePath, string affixFilePath) =>
            new HunspellDictionary(WordListReader.ReadFile(dictionaryFilePath, affixFilePath));
#endif

#if !NO_ASYNC
        public static async Task<HunspellDictionary> ReadAsync(Stream dictionaryStream, Stream affixStream) =>
            new HunspellDictionary(await WordListReader.ReadAsync(dictionaryStream, affixStream).ConfigureAwait(false));

#if !NO_IO_FILE
        public static async Task<HunspellDictionary> FromFileAsync(string dictionaryFilePath) =>
            new HunspellDictionary(await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false));

        public static async Task<HunspellDictionary> FromFileAsync(string dictionaryFilePath, string affixFilePath) =>
            new HunspellDictionary(await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false));
#endif

#endif

        public bool Check(string word) => new QueryCheck(word, WordList).Check();

        public SpellCheckResult CheckDetails(string word) => new QueryCheck(word, WordList).CheckDetails();

        public IEnumerable<string> Suggest(string word) => new QuerySuggest(word, WordList).Suggest();
    }
}
