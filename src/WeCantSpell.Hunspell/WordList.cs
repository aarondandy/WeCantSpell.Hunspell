using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using WeCantSpell.Hunspell.Infrastructure;
using System.Linq;

#if !NO_ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed partial class WordList
    {
        internal const int MaxWordLen = 100;

        public static WordList CreateFromStreams(Stream dictionaryStream, Stream affixStream) =>
            WordListReader.Read(dictionaryStream, affixStream);

#if !NO_IO_FILE
        public static WordList CreateFromFiles(string dictionaryFilePath) =>
            WordListReader.ReadFile(dictionaryFilePath);

        public static WordList CreateFromFiles(string dictionaryFilePath, string affixFilePath) =>
            WordListReader.ReadFile(dictionaryFilePath, affixFilePath);
#endif

#if !NO_ASYNC
        public static async Task<WordList> CreateFromStreamsAsync(Stream dictionaryStream, Stream affixStream) =>
            await WordListReader.ReadAsync(dictionaryStream, affixStream).ConfigureAwait(false);

#if !NO_IO_FILE
        public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath) =>
            await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false);

        public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath, string affixFilePath) =>
            await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false);
#endif

#endif

        public static WordList CreateFromWords(IEnumerable<string> words) =>
            CreateFromWords(words, affix: null);

        public static WordList CreateFromWords(IEnumerable<string> words, AffixConfig affix)
        {
            var wordListBuilder = new Builder(affix ?? new AffixConfig.Builder().MoveToImmutable());

            if (words is IList<string> wordsAsList)
            {
                wordListBuilder.InitializeEntriesByRoot(wordsAsList.Count);
            }
            else
            {
                wordListBuilder.InitializeEntriesByRoot(0);
            }

            foreach (var word in words)
            {
                var wordEntry = new WordEntry(word, FlagSet.Empty, MorphSet.Empty, WordEntryOptions.None);

                WordEntrySet entryList = !wordListBuilder.EntriesByRoot.TryGetValue(word, out entryList)
                    ? WordEntrySet.Create(wordEntry)
                    : WordEntrySet.CopyWithItemAdded(entryList, wordEntry);

                wordListBuilder.EntriesByRoot.Add(word, entryList);
            }

            return wordListBuilder.MoveToImmutable();
        }

        private WordList(AffixConfig affix)
        {
            Affix = affix;
        }

        public AffixConfig Affix { get; private set; }

        private Dictionary<string, WordEntrySet> EntriesByRoot { get; set; }

        private FlagSet NGramRestrictedFlags { get; set; }

        private HashSet<WordEntry> NGramRestrictedEntries { get; set; }

        public IEnumerable<WordEntry> NGramAllowedEntries =>
            NGramRestrictedEntries == null || NGramRestrictedEntries.Count == 0
            ? AllEntries
            : AllEntries.Where(entry => !NGramRestrictedEntries.Contains(entry));

        public IEnumerable<WordEntry> AllEntries => EntriesByRoot.Values.SelectMany(set => set);

        public IEnumerable<string> RootWords => EntriesByRoot.Keys;

        public bool HasEntries => EntriesByRoot.Count != 0;

        public WordEntrySet this[string rootWord] => FindEntriesByRootWord(rootWord);

        public bool Check(string word) => new QueryCheck(word, this).Check();

        public SpellCheckResult CheckDetails(string word) => new QueryCheck(word, this).CheckDetails();

        public IEnumerable<string> Suggest(string word) => new QuerySuggest(word, this).Suggest();

        public WordEntrySet FindEntriesByRootWord(string rootWord)
        {
            WordEntrySet result;
            if (!EntriesByRoot.TryGetValue(rootWord, out result))
            {
                result = WordEntrySet.Empty;
            }

            return result;
        }
    }
}
