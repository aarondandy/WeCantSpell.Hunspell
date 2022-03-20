using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed partial class WordList
{
    internal const int MaxWordLen = 100;
    internal const int MaxWordUtf8Len = MaxWordLen * 3;

    public static WordList CreateFromStreams(Stream dictionaryStream, Stream affixStream) =>
        WordListReader.Read(dictionaryStream, affixStream);

    public static WordList CreateFromFiles(string dictionaryFilePath) =>
        WordListReader.ReadFile(dictionaryFilePath);

    public static WordList CreateFromFiles(string dictionaryFilePath, string affixFilePath) =>
        WordListReader.ReadFile(dictionaryFilePath, affixFilePath);

    public static Task<WordList> CreateFromStreamsAsync(Stream dictionaryStream, Stream affixStream, CancellationToken cancellationToken = default) =>
        WordListReader.ReadAsync(dictionaryStream, affixStream, cancellationToken);

    public static Task<WordList> CreateFromFilesAsync(string dictionaryFilePath, CancellationToken cancellationToken = default) =>
        WordListReader.ReadFileAsync(dictionaryFilePath, cancellationToken);

    public static Task<WordList> CreateFromFilesAsync(string dictionaryFilePath, string affixFilePath, CancellationToken cancellationToken = default) =>
        WordListReader.ReadFileAsync(dictionaryFilePath, affixFilePath, cancellationToken);

    public static WordList CreateFromWords(IEnumerable<string> words) =>
        CreateFromWords(
            words ?? throw new ArgumentNullException(nameof(words)),
            new AffixConfig.Builder().MoveToImmutable());

    public static WordList CreateFromWords(IEnumerable<string> words, AffixConfig affix)
    {
        if (words is null) throw new ArgumentNullException(nameof(words));
        if (affix is null) throw new ArgumentNullException(nameof(affix));

        var wordListBuilder = new Builder(affix);

        wordListBuilder.InitializeEntriesByRoot((words as ICollection<string>)?.Count ?? 0);

        foreach (var word in words)
        {
            wordListBuilder.Add(word, WordEntryDetail.Default);
        }

        return wordListBuilder.MoveToImmutable();
    }

    private WordList(AffixConfig affix, FlagSet nGramRestrictedFlags)
    {
        Affix = affix;
        NGramRestrictedFlags = nGramRestrictedFlags;
        EntriesByRoot = new();
        NGramRestrictedDetails = new();
    }

    public AffixConfig Affix { get; private set; }

    public SingleReplacementSet AllReplacements { get; private set; }

    public IEnumerable<string> RootWords => EntriesByRoot.Keys;

    public bool HasEntries => EntriesByRoot.Count > 0;

    public bool ContainsEntriesForRootWord(string rootWord) => rootWord is not null && EntriesByRoot.ContainsKey(rootWord);

    public WordEntryDetail[] this[string rootWord] =>
        rootWord is not null
            ? FindEntryDetailsByRootWord(rootWord).ToArray()
            : Array.Empty<WordEntryDetail>();

    private TextDictionary<WordEntryDetail[]> EntriesByRoot { get; set; }

    private FlagSet NGramRestrictedFlags { get; set; }

    private NGramAllowedEntriesEnumerator GetNGramAllowedDetailsByKeyLength(int minKeyLength, int maxKeyLength) => new(this, minKeyLength: minKeyLength, maxKeyLength: maxKeyLength);

    private TextDictionary<WordEntryDetail[]> NGramRestrictedDetails { get; set; }

    public bool Check(string word) => Check(word, options: null);

    public bool Check(string word, QueryOptions? options) => new QueryCheck(this, options).Check(word);

    public SpellCheckResult CheckDetails(string word) => CheckDetails(word, options: null);

    public SpellCheckResult CheckDetails(string word, QueryOptions? options) => new QueryCheck(this, options).CheckDetails(word);

    public IEnumerable<string> Suggest(string word) => Suggest(word, options: null);

    public IEnumerable<string> Suggest(string word, QueryOptions? options) => new QuerySuggest(this, options).Suggest(word);

    internal WordEntry? FindFirstEntryByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif
        return EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length > 0
            ? new WordEntry(rootWord, details[0])
            : null;
    }

    internal WordEntryDetail[] FindEntryDetailsByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif

        return EntriesByRoot.TryGetValue(rootWord, out var details)
            ? details
            : Array.Empty<WordEntryDetail>();
    }

    internal WordEntryDetail? FindFirstEntryDetailByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif

        return EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length != 0
            ? details[0]
            : null;
    }

    internal bool TryFindFirstEntryDetailByRootWord(string rootWord, out WordEntryDetail entryDetail)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif

        if (EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length > 0)
        {
            entryDetail = details[0];
            return true;
        }

        entryDetail = default;
        return false;
    }

    private struct NGramAllowedEntriesEnumerator
    {
        public NGramAllowedEntriesEnumerator(WordList wordList, int minKeyLength, int maxKeyLength)
        {
#if DEBUG
            if (minKeyLength > maxKeyLength) throw new ArgumentOutOfRangeException(nameof(maxKeyLength));
#endif

            _coreEnumerator = wordList.EntriesByRoot.GetEnumerator();
            _nGramRestrictedDetails = wordList.NGramRestrictedDetails;
            _requiresNGramFiltering = _nGramRestrictedDetails is { Count: > 0 };
            _minKeyLength = minKeyLength;
            _maxKeyLength = maxKeyLength;
            Current = default;
        }

        private TextDictionary<WordEntryDetail[]>.Enumerator _coreEnumerator;
        private readonly TextDictionary<WordEntryDetail[]> _nGramRestrictedDetails;
        private readonly int _minKeyLength;
        private readonly int _maxKeyLength;
        private readonly bool _requiresNGramFiltering;

        public KeyValuePair<string, WordEntryDetail[]> Current { get; private set; }

        public NGramAllowedEntriesEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            while (_coreEnumerator.MoveNext())
            {
                var rootKey = _coreEnumerator.Current.Key;

                if (rootKey.Length >= _minKeyLength && rootKey.Length <= _maxKeyLength)
                {
                    var rootValue = _coreEnumerator.Current.Value;

                    if (
                        _requiresNGramFiltering
                        && _nGramRestrictedDetails.TryGetValue(rootKey, out var restrictedDetails)
                        && restrictedDetails.Length != 0
                    )
                    {
                        if (restrictedDetails.Length == rootValue.Length)
                        {
                            continue;
                        }
                        else
                        {
                            rootValue = filterNonMatching(rootValue, restrictedDetails);
                            static WordEntryDetail[] filterNonMatching(WordEntryDetail[] source, WordEntryDetail[] check)
                            {
                                var builder = new ArrayBuilder<WordEntryDetail>(source.Length);
                                foreach (var item in source)
                                {
                                    if (!check.Contains(item))
                                    {
                                        builder.Add(item);
                                    }
                                }

                                return builder.Extract();
                            }
                        }
                    }

                    Current = new(rootKey, rootValue);
                    return true;
                }
            }

            Current = default;
            return false;
        }
    }
}
