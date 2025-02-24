using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace WeCantSpell.Hunspell;

public sealed partial class WordList
{
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

    public static WordList CreateFromWords(IEnumerable<string> words)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(words);
#else
        ExceptionEx.ThrowIfArgumentNull(words, nameof(words));
#endif

        return CreateFromWords(words, new AffixConfig.Builder().MoveToImmutable());
    }

    public static WordList CreateFromWords(IEnumerable<string> words, AffixConfig affix)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(words);
        ArgumentNullException.ThrowIfNull(affix);
#else
        ExceptionEx.ThrowIfArgumentNull(words, nameof(words));
        ExceptionEx.ThrowIfArgumentNull(affix, nameof(affix));
#endif

        var wordListBuilder = new Builder(affix);

        wordListBuilder.InitializeEntriesByRoot(words.GetNonEnumeratedCountOrDefault());

        foreach (var word in words)
        {
            wordListBuilder.Add(word);
        }

        return wordListBuilder.MoveToImmutable();
    }

    private WordList(
        AffixConfig affix,
        TextDictionary<WordEntryDetail[]> entriesByRoot,
        TextDictionary<WordEntryDetail[]> nGramRestrictedDetails,
        SingleReplacementSet allReplacements
    )
    {
        _affix = affix;
        _entriesByRoot = entriesByRoot;
        _nGramRestrictedDetails = nGramRestrictedDetails;
        _allReplacements = allReplacements;
    }

    private readonly AffixConfig _affix;
    private readonly TextDictionary<WordEntryDetail[]> _entriesByRoot;
    private readonly TextDictionary<WordEntryDetail[]> _nGramRestrictedDetails;
    private readonly SingleReplacementSet _allReplacements;

    public AffixConfig Affix => _affix;

    public SingleReplacementSet AllReplacements => _allReplacements;

    public IEnumerable<string> RootWords => _entriesByRoot.Keys;

    public bool HasEntries => _entriesByRoot.HasItems;

    public bool IsEmpty => _entriesByRoot.IsEmpty;

    public WordEntryDetail[] this[string rootWord] => TryGetEntryDetailsByRootWord(rootWord, out var details) ? details : [];

    public bool ContainsEntriesForRootWord(string rootWord) => rootWord is not null && _entriesByRoot.ContainsKey(rootWord);

    public bool ContainsEntriesForRootWord(ReadOnlySpan<char> rootWord) => _entriesByRoot.ContainsKey(rootWord);

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

    public SpellCheckResult CheckDetails(string word, QueryOptions? options, CancellationToken cancellationToken)
    {
        var result = new QueryCheck(this, options, cancellationToken).CheckDetails(word);
        ApplyRootOutputConversions(ref result);
        return result;
    }

    public SpellCheckResult CheckDetails(ReadOnlySpan<char> word, QueryOptions? options, CancellationToken cancellationToken)
    {
        var result = new QueryCheck(this, options, cancellationToken).CheckDetails(word);
        ApplyRootOutputConversions(ref result);
        return result;
    }

    private void ApplyRootOutputConversions(ref SpellCheckResult result)
    {
        // output conversion
        if (result.Correct && _affix.OutputConversions.TryConvert(result.Root, out var converted) && !string.Equals(result.Root, converted, StringComparison.Ordinal))
        {
            result = SpellCheckResult.Success(converted, result.Info);
        }
    }

    public IEnumerable<string> Suggest(string word) => Suggest(word, options: null, CancellationToken.None);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word) => Suggest(word, options: null, CancellationToken.None);

    public IEnumerable<string> Suggest(string word, QueryOptions? options) => Suggest(word, options, CancellationToken.None);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, QueryOptions? options) => Suggest(word, options, CancellationToken.None);

    public IEnumerable<string> Suggest(string word, CancellationToken cancellationToken) => Suggest(word, options: null, cancellationToken);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, CancellationToken cancellationToken) => Suggest(word, options: null, cancellationToken);

    public IEnumerable<string> Suggest(string word, QueryOptions? options, CancellationToken cancellationToken) => new QuerySuggest(this, options, cancellationToken).Suggest(word);

    public IEnumerable<string> Suggest(ReadOnlySpan<char> word, QueryOptions? options, CancellationToken cancellationToken) => new QuerySuggest(this, options, cancellationToken).Suggest(word);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryGetEntryDetailsByRootWord(
        string rootWord,
#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)]
#endif
        out WordEntryDetail[] details)
    {
        return _entriesByRoot.TryGetValue(rootWord, out details);
    }

    private bool TryGetFirstEntryDetailByRootWord(ReadOnlySpan<char> rootWord, out WordEntryDetail entryDetail)
    {
        if (_entriesByRoot.TryGetValue(rootWord, out var details))
        {
#if DEBUG
            if (details.Length == 0) ExceptionEx.ThrowInvalidOperation();
#endif

            entryDetail = details[0];
            return true;
        }

        entryDetail = default;
        return false;
    }

    private bool TryFindFirstEntryDetailByRootWord(string rootWord, out WordEntryDetail entryDetail)
    {
        if (_entriesByRoot.TryGetValue(rootWord, out var details))
        {
#if DEBUG
            if (details.Length == 0) ExceptionEx.ThrowInvalidOperation();
#endif

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
            _nGramRestrictedDetails = wordList._nGramRestrictedDetails;
            _coreEnumerator = new(wordList._entriesByRoot, minKeyLength, maxKeyLength);
            _current = default;
        }

        private readonly TextDictionary<WordEntryDetail[]> _nGramRestrictedDetails;
        private TextDictionary<WordEntryDetail[]>.KeyLengthEnumerator _coreEnumerator;
        private KeyValuePair<string, WordEntryDetail[]> _current;

        public readonly KeyValuePair<string, WordEntryDetail[]> Current => _current;

        public readonly NGramAllowedEntriesEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_nGramRestrictedDetails.HasItems)
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
                        var builder = ArrayBuilder<WordEntryDetail>.Pool.Get(source.Length);

                        foreach (var item in source)
                        {
                            if (!check.Contains(item))
                            {
                                builder.Add(item);
                            }
                        }

                        return ArrayBuilder<WordEntryDetail>.Pool.ExtractAndReturn(builder);
                    }
                }

                return true;
            }

            _current = default;
            return false;
        }
    }
}
