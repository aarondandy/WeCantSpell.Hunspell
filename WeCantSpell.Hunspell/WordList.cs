using System;
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
            wordListBuilder.Add(word);
        }

        return wordListBuilder.MoveToImmutable();
    }

    private WordList(AffixConfig affix, FlagSet nGramRestrictedFlags)
    {
        Affix = affix;
        NGramRestrictedFlags = nGramRestrictedFlags;
        EntriesByRoot = new(0);
        NGramRestrictedDetails = new(0);
    }

    public AffixConfig Affix { get; private set; }

    public SingleReplacementSet AllReplacements { get; private set; }

    public IEnumerable<string> RootWords => EntriesByRoot.Keys;

    public bool HasEntries => EntriesByRoot.Count > 0;

    public bool ContainsEntriesForRootWord(string rootWord) => rootWord is not null && EntriesByRoot.ContainsKey(rootWord);

    public bool ContainsEntriesForRootWord(ReadOnlySpan<char> rootWord) => EntriesByRoot.ContainsKey(rootWord);

    public WordEntryDetail[] this[string rootWord] =>
        rootWord is not null
            ? FindEntryDetailsByRootWord(rootWord).ToArray()
            : Array.Empty<WordEntryDetail>();

    private TextDictionary<WordEntryDetail[]> EntriesByRoot { get; set; }

    private FlagSet NGramRestrictedFlags { get; set; }

    private TextDictionary<WordEntryDetail[]> NGramRestrictedDetails { get; set; }

    public bool Check(string word) => Check(word, options: null, CancellationToken.None);

    public bool Check(ReadOnlySpan<char> word) => Check(word, options: null, CancellationToken.None);

    public bool Check(string word, QueryOptions? options) => Check(word, options, CancellationToken.None);

    public bool Check(ReadOnlySpan<char> word, QueryOptions? options) => Check(word, options, CancellationToken.None);

    public bool Check(string word, CancellationToken cancellationToken) => Check(word, options: null, cancellationToken);

    public bool Check(ReadOnlySpan<char> word, CancellationToken cancellationToken) => Check(word, options: null, cancellationToken);

    public bool Check(string word, QueryOptions? options, CancellationToken cancellationToken) => new QueryCheck(this, options, cancellationToken).Check(word);

    public bool Check(ReadOnlySpan<char> word, QueryOptions? options, CancellationToken cancellationToken) => new QueryCheck(this, options, cancellationToken).Check(word);

    public SpellCheckResult CheckDetails(string word) => CheckDetails(word, options: null, CancellationToken.None);

    public SpellCheckResult CheckDetails(ReadOnlySpan<char> word) => CheckDetails(word, options: null, CancellationToken.None);

    public SpellCheckResult CheckDetails(string word, QueryOptions? options) => CheckDetails(word, options, CancellationToken.None);

    public SpellCheckResult CheckDetails(ReadOnlySpan<char> word, QueryOptions? options) => CheckDetails(word, options, CancellationToken.None);

    public SpellCheckResult CheckDetails(string word, CancellationToken cancellationToken) => CheckDetails(word, options: null, cancellationToken);

    public SpellCheckResult CheckDetails(ReadOnlySpan<char> word, CancellationToken cancellationToken) => CheckDetails(word, options: null, cancellationToken);

    public SpellCheckResult CheckDetails(string word, QueryOptions? options, CancellationToken cancellationToken) => new QueryCheck(this, options, cancellationToken).CheckDetails(word);

    public SpellCheckResult CheckDetails(ReadOnlySpan<char> word, QueryOptions? options, CancellationToken cancellationToken) => new QueryCheck(this, options, cancellationToken).CheckDetails(word);

    public IEnumerable<string> Suggest(string word) => Suggest(word, options: null, CancellationToken.None);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word) => Suggest(word, options: null, CancellationToken.None);

    public IEnumerable<string> Suggest(string word, QueryOptions? options) => Suggest(word, options, CancellationToken.None);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, QueryOptions? options) => Suggest(word, options, CancellationToken.None);

    public IEnumerable<string> Suggest(string word, CancellationToken cancellationToken) => Suggest(word, options: null, cancellationToken);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, CancellationToken cancellationToken) => Suggest(word, options: null, cancellationToken);

    public IEnumerable<string> Suggest(string word, QueryOptions? options, CancellationToken cancellationToken) => new QuerySuggest(this, options, cancellationToken).Suggest(word);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, QueryOptions? options, CancellationToken cancellationToken) => new QuerySuggest(this, options, cancellationToken).Suggest(word);

    private WordEntry? FindFirstEntryByRootWord(ReadOnlySpan<char> rootWord)
    {
        return EntriesByRoot.TryGetValue(rootWord, out var key, out var details) && details.Length > 0
            ? new WordEntry(key, details[0])
            : null;
    }

    private WordEntry? FindFirstEntryByRootWord(string rootWord)
    {
        return EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length > 0
            ? new WordEntry(rootWord, details[0])
            : null;
    }

    private WordEntryDetail[] FindEntryDetailsByRootWord(string rootWord)
    {
#if DEBUG
        if (rootWord is null) throw new ArgumentNullException(nameof(rootWord));
#endif

        return EntriesByRoot.TryGetValue(rootWord, out var details)
            ? details
            : Array.Empty<WordEntryDetail>();
    }

    private WordEntryDetail[] FindEntryDetailsByRootWord(ReadOnlySpan<char> rootWord)
    {
        return EntriesByRoot.TryGetValue(rootWord, out var details)
            ? details
            : Array.Empty<WordEntryDetail>();
    }

    private WordEntryDetail? FindFirstEntryDetailByRootWord(ReadOnlySpan<char> rootWord)
    {
        return EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length != 0
            ? details[0]
            : null;
    }

    private bool TryFindFirstEntryDetailByRootWord(ReadOnlySpan<char> rootWord, out WordEntryDetail entryDetail)
    {
        if (EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length > 0)
        {
            entryDetail = details[0];
            return true;
        }

        entryDetail = default;
        return false;
    }

    private bool TryFindFirstEntryDetailByRootWord(string rootWord, out WordEntryDetail entryDetail)
    {
        if (EntriesByRoot.TryGetValue(rootWord, out var details) && details.Length > 0)
        {
            entryDetail = details[0];
            return true;
        }

        entryDetail = default;
        return false;
    }

    private NGramAllowedEntriesEnumerator GetNGramAllowedDetailsByKeyLength(int minKeyLength, int maxKeyLength) => new(this, minKeyLength: minKeyLength, maxKeyLength: maxKeyLength);

    private struct NGramAllowedEntriesEnumerator
    {
        public NGramAllowedEntriesEnumerator(WordList wordList, int minKeyLength, int maxKeyLength)
        {
#if DEBUG
            if (minKeyLength > maxKeyLength) throw new ArgumentOutOfRangeException(nameof(maxKeyLength));
#endif

            _coreEnumerator = new(wordList.EntriesByRoot, minKeyLength, maxKeyLength);
            _nGramRestrictedDetails = wordList.NGramRestrictedDetails;
            _checkRestrictedDetails = _nGramRestrictedDetails.Count != 0;
            _current = default;
        }

        private TextDictionary<WordEntryDetail[]>.KeyLengthEnumerator _coreEnumerator;
        private TextDictionary<WordEntryDetail[]> _nGramRestrictedDetails;
        private KeyValuePair<string, WordEntryDetail[]> _current;
        private bool _checkRestrictedDetails;

        public KeyValuePair<string, WordEntryDetail[]> Current => _current;

        public NGramAllowedEntriesEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_checkRestrictedDetails)
            {
                return MoveNextWithRestrictedDetails();
            }

            if (_coreEnumerator.MoveNext())
            {
                _current = _coreEnumerator.Current;
                return true;
            }

            _current = default;
            return false;
        }

        private bool MoveNextWithRestrictedDetails()
        {
            while (_coreEnumerator.MoveNext())
            {
                _current = _coreEnumerator.Current;

                if (_nGramRestrictedDetails.TryGetValue(_current.Key, out var restrictedDetails) && restrictedDetails.Length != 0)
                {
                    if (restrictedDetails.Length == _current.Value.Length)
                    {
                        continue;
                    }

                    _current = new(_current.Key, filterNonMatching(_current.Value, restrictedDetails));
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

                return true;
            }

            _current = default;
            return false;
        }
    }
}
