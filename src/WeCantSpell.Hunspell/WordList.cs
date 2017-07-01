using System;
using System.Collections.Generic;
using System.IO;
using WeCantSpell.Hunspell.Infrastructure;
using System.Linq;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

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

        public static WordList CreateFromFiles(string dictionaryFilePath) =>
            WordListReader.ReadFile(dictionaryFilePath);

        public static WordList CreateFromFiles(string dictionaryFilePath, string affixFilePath) =>
            WordListReader.ReadFile(dictionaryFilePath, affixFilePath);

#if !NO_ASYNC

        public static async Task<WordList> CreateFromStreamsAsync(Stream dictionaryStream, Stream affixStream) =>
            await WordListReader.ReadAsync(dictionaryStream, affixStream).ConfigureAwait(false);

        public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath) =>
            await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false);

        public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath, string affixFilePath) =>
            await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false);

#endif

        public static WordList CreateFromWords(IEnumerable<string> words) =>
            CreateFromWords(words, affix: null);

        public static WordList CreateFromWords(IEnumerable<string> words, AffixConfig affix)
        {
            if (words == null)
            {
                words = Enumerable.Empty<string>();
            }

            var wordListBuilder = new Builder(affix ?? new AffixConfig.Builder().MoveToImmutable());

            if (words is IList<string> wordsAsList)
            {
                wordListBuilder.InitializeEntriesByRoot(wordsAsList.Count);
            }
            else
            {
                wordListBuilder.InitializeEntriesByRoot(-1);
            }

            var entryDetail = WordEntryDetail.Default;

            foreach (var word in words)
            {
                wordListBuilder.Add(word, entryDetail);
            }

            return wordListBuilder.MoveToImmutable();
        }

        private WordList(AffixConfig affix)
        {
            Affix = affix;
        }

        public AffixConfig Affix { get; private set; }

        public IEnumerable<string> RootWords => EntriesByRoot.Keys;

        public bool HasEntries => EntriesByRoot.Count != 0;

        public bool ContainsEntriesForRootWord(string rootWord) =>
            rootWord != null && EntriesByRoot.ContainsKey(rootWord);

        public WordEntryDetail[] this[string rootWord] =>
            (rootWord != null)
                ? (WordEntryDetail[])FindEntryDetailsByRootWord(rootWord).Clone()
                : ArrayEx<WordEntryDetail>.Empty;

        private Dictionary<string, WordEntryDetail[]> EntriesByRoot { get; set; }

        private FlagSet NGramRestrictedFlags { get; set; }

        private IEnumerable<KeyValuePair<string, WordEntryDetail[]>> NGramAllowedDetails =>
            (NGramRestrictedDetails == null || NGramRestrictedDetails.Count == 0)
            ? EntriesByRoot
            : GetAllNGramAllowedEntries();

        private Dictionary<string, WordEntryDetail[]> NGramRestrictedDetails { get; set; }

        public bool Check(string word) => new QueryCheck(word, this).Check();

        public SpellCheckResult CheckDetails(string word) => new QueryCheck(word, this).CheckDetails();

        public IEnumerable<string> Suggest(string word) => new QuerySuggest(word, this).Suggest();

        internal WordEntry FindFirstEntryByRootWord(string rootWord)
        {
#if DEBUG
            if (rootWord == null)
            {
                throw new ArgumentNullException(nameof(rootWord));
            }
#endif
            var details = FindEntryDetailsByRootWord(rootWord);
            return details.Length == 0
                ? null
                : new WordEntry(rootWord, details[0]);
        }

        internal WordEntryDetail[] FindEntryDetailsByRootWord(string rootWord)
        {
#if DEBUG
            if (rootWord == null)
            {
                throw new ArgumentNullException(nameof(rootWord));
            }
#endif
            return (rootWord == null || !EntriesByRoot.TryGetValue(rootWord, out WordEntryDetail[] details))
                ? ArrayEx<WordEntryDetail>.Empty
                : details;
        }

        internal WordEntryDetail FindFirstEntryDetailByRootWord(string rootWord)
        {
#if DEBUG
            if (rootWord == null)
            {
                throw new ArgumentNullException(nameof(rootWord));
            }
#endif

            return EntriesByRoot.TryGetValue(rootWord, out WordEntryDetail[] details) && details.Length != 0
                ? details[0]
                : null;
        }

        private IEnumerable<KeyValuePair<string, WordEntryDetail[]>> GetAllNGramAllowedEntries() =>
            EntriesByRoot.Select(rootPair =>
            {
                if (NGramRestrictedDetails.TryGetValue(rootPair.Key, out WordEntryDetail[] restrictedDetails))
                {
                    return new KeyValuePair<string, WordEntryDetail[]>(
                        rootPair.Key,
                        rootPair.Value
                            .Where(d => !restrictedDetails.Contains(d))
                            .ToArray());
                }
                else
                {
                    return rootPair;
                }
            });
    }
}
