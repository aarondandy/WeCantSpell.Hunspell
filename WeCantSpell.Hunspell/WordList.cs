using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public sealed partial class WordList
{
    internal const int MaxWordLen = 100;

    public static WordList CreateFromStreams(Stream dictionaryStream, Stream affixStream) =>
        WordListReader.Read(dictionaryStream, affixStream);

    public static WordList CreateFromFiles(string dictionaryFilePath) =>
        WordListReader.ReadFile(dictionaryFilePath);

    public static WordList CreateFromFiles(string dictionaryFilePath, string affixFilePath) =>
        WordListReader.ReadFile(dictionaryFilePath, affixFilePath);

    public static async Task<WordList> CreateFromStreamsAsync(Stream dictionaryStream, Stream affixStream) =>
        await WordListReader.ReadAsync(dictionaryStream, affixStream).ConfigureAwait(false);

    public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath) =>
        await WordListReader.ReadFileAsync(dictionaryFilePath).ConfigureAwait(false);

    public static async Task<WordList> CreateFromFilesAsync(string dictionaryFilePath, string affixFilePath) =>
        await WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false);

    public static WordList CreateFromWords(IEnumerable<string> words) =>
        CreateFromWords(words, affix: null);

    public static WordList CreateFromWords(IEnumerable<string> words, AffixConfig affix)
    {
        words ??= Enumerable.Empty<string>();

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

    public SingleReplacementSet AllReplacements { get; private set; }

    public IEnumerable<string> RootWords => EntriesByRoot.Keys;

    public bool HasEntries => EntriesByRoot.Count != 0;

    public bool ContainsEntriesForRootWord(string rootWord) => rootWord is not null && EntriesByRoot.ContainsKey(rootWord);

    public WordEntryDetail[] this[string rootWord] =>
        rootWord is not null
            ? (WordEntryDetail[])FindEntryDetailsByRootWord(rootWord).Clone()
            : ArrayEx<WordEntryDetail>.Empty;

    private Dictionary<string, WordEntryDetail[]> EntriesByRoot { get; set; }

    private FlagSet NGramRestrictedFlags { get; set; }

    private NGramAllowedEntries GetNGramAllowedDetails(Func<string, bool> rootKeyFilter) => new NGramAllowedEntries(this, rootKeyFilter);

    private Dictionary<string, WordEntryDetail[]> NGramRestrictedDetails { get; set; }

    public bool Check(string word) => new QueryCheck(this).Check(word);

    public SpellCheckResult CheckDetails(string word) => new QueryCheck(this).CheckDetails(word);

    public IEnumerable<string> Suggest(string word) => new QuerySuggest(this).Suggest(word);

    internal WordEntry FindFirstEntryByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif
        var details = FindEntryDetailsByRootWord(rootWord);
        return details.Length == 0
            ? null
            : new WordEntry(rootWord, details[0]);
    }

    internal WordEntryDetail[] FindEntryDetailsByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif
        return (rootWord is null || !EntriesByRoot.TryGetValue(rootWord, out WordEntryDetail[] details))
            ? ArrayEx<WordEntryDetail>.Empty
            : details;
    }

    internal WordEntryDetail FindFirstEntryDetailByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif

        return EntriesByRoot.TryGetValue(rootWord, out WordEntryDetail[] details) && details.Length != 0
            ? details[0]
            : null;
    }

    private class NGramAllowedEntries : IEnumerable<KeyValuePair<string, WordEntryDetail[]>>
    {
        public NGramAllowedEntries(WordList wordList, Func<string, bool> rootKeyFilter)
        {
            _wordList = wordList;
            _rootKeyFilter = rootKeyFilter;
        }

        private readonly WordList _wordList;

        private readonly Func<string, bool> _rootKeyFilter;

        public Enumerator GetEnumerator() => new Enumerator(_wordList.EntriesByRoot, _wordList.NGramRestrictedDetails, _rootKeyFilter);

        IEnumerator<KeyValuePair<string, WordEntryDetail[]>> IEnumerable<KeyValuePair<string, WordEntryDetail[]>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Enumerator : IEnumerator<KeyValuePair<string, WordEntryDetail[]>>
        {
            public Enumerator(Dictionary<string, WordEntryDetail[]> entriesByRoot, Dictionary<string, WordEntryDetail[]> nGramRestrictedDetails, Func<string, bool> rootKeyFilter)
            {
                _coreEnumerator = entriesByRoot.GetEnumerator();
                _entriesByRoot = entriesByRoot;
                _nGramRestrictedDetails = nGramRestrictedDetails;
                _rootKeyFilter = rootKeyFilter;
                _requiresNGramFiltering = nGramRestrictedDetails != null && nGramRestrictedDetails.Count != 0;
            }

            Dictionary<string, WordEntryDetail[]>.Enumerator _coreEnumerator;
            readonly Dictionary<string, WordEntryDetail[]> _entriesByRoot;
            readonly Dictionary<string, WordEntryDetail[]> _nGramRestrictedDetails;
            readonly Func<string, bool> _rootKeyFilter;
            readonly bool _requiresNGramFiltering;

            public KeyValuePair<string, WordEntryDetail[]> Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                while (_coreEnumerator.MoveNext())
                {
                    var rootPair = _coreEnumerator.Current;
                    if (!_rootKeyFilter(rootPair.Key))
                    {
                        continue;
                    }

                    if (_requiresNGramFiltering)
                    {
                        if (_nGramRestrictedDetails.TryGetValue(rootPair.Key.ToString(), out WordEntryDetail[] restrictedDetails))
                        {
                            if (restrictedDetails.Length != 0)
                            {
                                WordEntryDetail[] filteredValues = rootPair.Value;
                                if (restrictedDetails.Length == rootPair.Value.Length)
                                {
                                    continue;
                                }
                                else
                                {
                                    filteredValues = filteredValues.Where(d => !restrictedDetails.Contains(d)).ToArray();
                                }

                                rootPair = new KeyValuePair<string, WordEntryDetail[]>(rootPair.Key, filteredValues);
                            }
                        }
                    }

                    Current = rootPair;
                    return true;
                }

                Current = default;
                return false;
            }

            public void Reset()
            {
                ((IEnumerator)_coreEnumerator).Reset();
            }

            public void Dispose()
            {
                _coreEnumerator.Dispose();
            }
        }
    }
}
