using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class WordListReader
{
    private WordListReader(WordList.Builder? builder, AffixConfig affix)
    {
        if (builder is null)
        {
            _ownsBuilder = true;
            builder = new WordList.Builder(affix);
        }

        Builder = builder;
        Affix = affix;
        FlagParser = new FlagParser(Affix.FlagMode, Affix.Encoding);
    }

    private readonly bool _ownsBuilder;
    private bool _hasInitialized;

    private WordList.Builder Builder { get; }

    private AffixConfig Affix { get; }

    private FlagParser FlagParser { get; }

    private TextInfo TextInfo => Affix.Culture.TextInfo;

    public static Task<WordList> ReadFileAsync(string dictionaryFilePath, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
#endif

        var affixFilePath = FindAffixFilePath(dictionaryFilePath);
        return ReadFileAsync(dictionaryFilePath, affixFilePath, cancellationToken);
    }

    public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, string affixFilePath, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
        ArgumentNullException.ThrowIfNull(affixFilePath);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affixFilePath is null) throw new ArgumentNullException(nameof(affixFilePath));
#endif

        var affix = await AffixReader.ReadFileAsync(affixFilePath, cancellationToken).ConfigureAwait(false);
        return await ReadFileAsync(dictionaryFilePath, affix, cancellationToken);
    }

    public static Task<WordList> ReadFileAsync(string dictionaryFilePath, AffixConfig affix, CancellationToken cancellationToken = default) =>
        ReadFileAsync(dictionaryFilePath, affix, builder: null, cancellationToken);

    public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, AffixConfig affix, WordList.Builder? builder, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
        ArgumentNullException.ThrowIfNull(affix);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affix is null) throw new ArgumentNullException(nameof(affix));
#endif

        using var stream = StreamEx.OpenAsyncReadFileStream(dictionaryFilePath);
        return await ReadAsync(stream, affix, builder, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<WordList> ReadAsync(Stream dictionaryStream, Stream affixStream, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryStream);
        ArgumentNullException.ThrowIfNull(affixStream);
#else
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affixStream is null) throw new ArgumentNullException(nameof(affixStream));
#endif

        var affix = await AffixReader.ReadAsync(affixStream, cancellationToken).ConfigureAwait(false);
        return await ReadAsync(dictionaryStream, affix, cancellationToken);
    }

    public static Task<WordList> ReadAsync(Stream dictionaryStream, AffixConfig affix, CancellationToken cancellationToken = default) =>
        ReadAsync(dictionaryStream, affix, builder: null, cancellationToken);

    public static async Task<WordList> ReadAsync(Stream dictionaryStream, AffixConfig affix, WordList.Builder? builder, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryStream);
        ArgumentNullException.ThrowIfNull(affix);
#else
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affix is null) throw new ArgumentNullException(nameof(affix));
#endif

        var readerInstance = new WordListReader(builder, affix);

        using (var lineReader = new LineReader(dictionaryStream, affix.Encoding))
        {
            while (await lineReader.ReadNextAsync(cancellationToken))
            {
                readerInstance.ParseLine(lineReader.Current.Span);
            }
        }

        return readerInstance.BuildWordList(allowDestructive: true);
    }

    public static WordList ReadFile(string dictionaryFilePath)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
#endif

        var affixFilePath = FindAffixFilePath(dictionaryFilePath);
        return ReadFile(dictionaryFilePath, AffixReader.ReadFile(affixFilePath));
    }

    public static WordList ReadFile(string dictionaryFilePath, string affixFilePath)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
        ArgumentNullException.ThrowIfNull(affixFilePath);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affixFilePath is null) throw new ArgumentNullException(nameof(affixFilePath));
#endif

        var affix = AffixReader.ReadFile(affixFilePath);
        return ReadFile(dictionaryFilePath, affix);
    }

    public static WordList ReadFile(string dictionaryFilePath, AffixConfig affix) =>
        ReadFile(dictionaryFilePath, affix, builder: null);

    public static WordList ReadFile(string dictionaryFilePath, AffixConfig affix, WordList.Builder? builder)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
        ArgumentNullException.ThrowIfNull(affix);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affix is null) throw new ArgumentNullException(nameof(affix));
#endif

        using var stream = StreamEx.OpenReadFileStream(dictionaryFilePath);
        return Read(stream, affix, builder);
    }

    public static WordList Read(Stream dictionaryStream, Stream affixStream)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryStream);
        ArgumentNullException.ThrowIfNull(affixStream);
#else
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affixStream is null) throw new ArgumentNullException(nameof(affixStream));
#endif

        var affix = AffixReader.Read(affixStream);
        return Read(dictionaryStream, affix);
    }

    public static WordList Read(Stream dictionaryStream, AffixConfig affix) =>
        Read(dictionaryStream, affix, builder: null);

    public static WordList Read(Stream dictionaryStream, AffixConfig affix, WordList.Builder? builder)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryStream);
        ArgumentNullException.ThrowIfNull(affix);
#else
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affix is null) throw new ArgumentNullException(nameof(affix));
#endif

        var readerInstance = new WordListReader(builder, affix);

        using var lineReader = new LineReader(dictionaryStream, affix.Encoding);
        while (lineReader.ReadNext())
        {
            readerInstance.ParseLine(lineReader.Current.Span);
        }

        return readerInstance.Builder.MoveToImmutable();
    }

    private WordList BuildWordList(bool allowDestructive)
    {
        return Builder.ToImmutable(allowDestructive: _ownsBuilder && allowDestructive);
    }

    private static string FindAffixFilePath(string dictionaryFilePath)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(dictionaryFilePath);
#else
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
#endif

        var directoryName = Path.GetDirectoryName(dictionaryFilePath);
        if (!string.IsNullOrEmpty(directoryName))
        {
            var locatedAffFile = Directory.GetFiles(directoryName, Path.GetFileNameWithoutExtension(dictionaryFilePath) + ".*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault(affFilePath => ".AFF".Equals(Path.GetExtension(affFilePath), StringComparison.OrdinalIgnoreCase));

            if (locatedAffFile is not null)
            {
                return locatedAffFile;
            }
        }

        return Path.ChangeExtension(dictionaryFilePath, "aff");
    }

    private void ParseLine(ReadOnlySpan<char> line)
    {
        if (line.Length == 0)
        {
            return;
        }

        if (!_hasInitialized)
        {
            if (AttemptToProcessInitializationLine(line))
            {
                return;
            }

            Builder.InitializeEntriesByRoot(-1);
        }

        var parsed = ParsedWordLine.Parse(line);
        if (parsed.Word.IsEmpty)
        {
            return;
        }

        FlagSet flags;
        if (!parsed.Flags.IsEmpty)
        {
            if (Affix.IsAliasF)
            {
                if (IntEx.TryParseInvariant(parsed.Flags, out var flagAliasNumber) && Affix.TryGetAliasF(flagAliasNumber, out var aliasedFlags))
                {
                    flags = aliasedFlags;
                }
                else
                {
                    // TODO: warn
                    return;
                }
            }
            else
            {
                flags = FlagParser.ParseFlagSet(parsed.Flags);
            }
        }
        else
        {
            flags = FlagSet.Empty;
        }

        AddWord(parsed.Word.ReplaceIntoString(@"\/", @"/"), flags, parsed.Morphs);
    }

    private bool AttemptToProcessInitializationLine(ReadOnlySpan<char> text)
    {
        _hasInitialized = true;

        // read through any leading spaces
        int i;
        for (i = 0; i < text.Length && char.IsWhiteSpace(text[i]); i++) ;
        text = text.Slice(i);

        // find the possible value
        for (i = 0; i < text.Length && !char.IsWhiteSpace(text[i]); i++) ;
        if (i < text.Length)
        {
            text = text.Slice(0, i);
        }

        if (!text.IsEmpty && IntEx.TryParseInvariant(text, out var expectedSize))
        {
            Builder.InitializeEntriesByRoot(expectedSize);

            return true;
        }

        return false;
    }

    private void AddWord(string word, FlagSet flags, string[] morphs)
    {
        if (Affix.IgnoredChars.HasItems)
        {
            word = Affix.IgnoredChars.RemoveChars(word);
        }

        if (Affix.ComplexPrefixes)
        {
            word = word.GetReversed();

            if (morphs.Length != 0 && !Affix.IsAliasM)
            {
                morphs = MorphSet.CreateReversedStrings(morphs);
            }
        }

        var capType = HunspellTextFunctions.GetCapitalizationType(word, TextInfo);
        AddWord(word, flags, morphs, false, capType);
        AddWordCapitalized(word, flags, morphs, capType);
    }

    private string[] AddWord_HandleMorph(string[] morphs, string word, CapitalizationType capType, ref WordEntryOptions options)
    {
        if (Affix.IsAliasM)
        {
            options |= WordEntryOptions.AliasM;
            var morphBuilder = ArrayBuilder<string>.Pool.Get();
            foreach (var originalValue in morphs)
            {
                if (IntEx.TryParseInvariant(originalValue, out var morphNumber) && Affix.TryGetAliasM(morphNumber, out var aliasedMorph))
                {
                    morphBuilder.AddRange(aliasedMorph.GetInternalArray());
                }
                else
                {
                    morphBuilder.Add(originalValue);
                }
            }

            morphs = ArrayBuilder<string>.Pool.ExtractAndReturn(morphBuilder);
        }

        var morphPhonEnumerator = new AddWordMorphFilterEnumerator(morphs);
        if (morphPhonEnumerator.MoveNext())
        {
            options |= WordEntryOptions.Phon;

            // store ph: fields (pronounciation, misspellings, old orthography etc.)
            // of a morphological description in reptable to use in REP replacements.

            do
            {
                var ph = morphPhonEnumerator.Current.AsSpan(MorphologicalTags.Phon.Length);
                if (ph.Length == 0)
                {
                    continue;
                }

                ReadOnlySpan<char> wordpart;
                // dictionary based REP replacement, separated by "->"
                // for example "pretty ph:prity ph:priti->pretti" to handle
                // both prity -> pretty and pritier -> prettiest suggestions.
                var strippatt = ph.IndexOf("->".AsSpan());
                if (strippatt > 0 && strippatt < (ph.Length - 2))
                {
                    wordpart = ph.Slice(strippatt + 2);
                    ph = ph.Slice(0, strippatt);
                }
                else
                {
                    wordpart = word.AsSpan();
                }

                // when the ph: field ends with the character *,
                // strip last character of the pattern and the replacement
                // to match in REP suggestions also at character changes,
                // for example, "pretty ph:prity*" results "prit->prett"
                // REP replacement instead of "prity->pretty", to get
                // prity->pretty and pritiest->prettiest suggestions.
                if (ph.EndsWith('*'))
                {
                    if (ph.Length > 2 && wordpart.Length > 1)
                    {
                        ph = ph.Slice(0, ph.Length - 2);
                        wordpart = wordpart.Slice(0, word.Length - 1);
                    }
                }

                var phString = ph.ToString();
                var wordpartString = wordpart.ToString();

                // capitalize lowercase pattern for capitalized words to support
                // good suggestions also for capitalized misspellings, eg.
                // Wednesday ph:wendsay
                // results wendsay -> Wednesday and Wendsay -> Wednesday, too.
                if (capType == CapitalizationType.Init)
                {
                    var phCapitalized = HunspellTextFunctions.MakeInitCap(phString, Affix.Culture.TextInfo);
                    if (phCapitalized.Length != 0)
                    {
                        // add also lowercase word in the case of German or
                        // Hungarian to support lowercase suggestions lowercased by
                        // compound word generation or derivational suffixes
                        // (for example by adjectival suffix "-i" of geographical
                        // names in Hungarian:
                        // Massachusetts ph:messzecsuzec
                        // messzecsuzeci -> massachusettsi (adjective)
                        // For lowercasing by conditional PFX rules, see
                        // tests/germancompounding test example or the
                        // Hungarian dictionary.)
                        if (Affix.IsGerman || Affix.IsHungarian)
                        {
                            var wordpartLower = HunspellTextFunctions.MakeAllSmall(wordpartString, Affix.Culture.TextInfo);
                            Builder._phoneticReplacements.Add(new SingleReplacement(phString, wordpartLower, ReplacementValueType.Med));
                        }

                        Builder._phoneticReplacements.Add(new SingleReplacement(phCapitalized, wordpartString, ReplacementValueType.Med));
                    }
                }

                Builder._phoneticReplacements.Add(new SingleReplacement(phString, wordpartString, ReplacementValueType.Med));
            }
            while (morphPhonEnumerator.MoveNext());
        }

        return morphs;
    }

    private struct AddWordMorphFilterEnumerator
    {
        public AddWordMorphFilterEnumerator(string[] morphs)
        {
            _morphs = morphs;
            _index = 0;
            Current = default!;
        }

        private readonly string[] _morphs;
        public string Current;
        private int _index;

        public bool MoveNext()
        {
            var i = _index;
            while (i < _morphs.Length)
            {
                var morph = _morphs[i++];
                if (morph.StartsWith(MorphologicalTags.Phon))
                {
                    Current = morph;
                    _index = i;
                    return true;
                }
            }

            Current = default!;
            _index = i;
            return false;
        }
    }

    private void AddWord(string word, FlagSet flags, string[] morphs, bool onlyUpperCase, CapitalizationType capType)
    {
        // store the description string or its pointer
        var options = capType == CapitalizationType.Init ? WordEntryOptions.InitCap : WordEntryOptions.None;
        if (morphs.Length != 0)
        {
            morphs = AddWord_HandleMorph(morphs, word, capType, ref options);
        }

        ref var details = ref Builder._entryDetailsByRoot.GetOrAdd(word);

        if (details is not null)
        {
            if (onlyUpperCase)
            {
                if (details.Length != 0)
                {
                    return;
                }
            }
            else
            {
                for (var i = 0; i < details.Length; i++)
                {
                    ref var entry = ref details[i];
                    if (entry.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                    {
                        entry = new(flags, entry.Morphs, entry.Options);
                        return;
                    }
                }
            }

            Array.Resize(ref details, details.Length + 1);
            details[details.Length - 1] = new(
                flags,
                new MorphSet(morphs),
                options);
        }
        else
        {
            details =
            [
                new(flags, new MorphSet(morphs), options)
            ];
        }
    }

    private void AddWordCapitalized(string word, FlagSet flags, string[] morphs, CapitalizationType capType)
    {
        // add inner capitalized forms to handle the following allcap forms:
        // Mixed caps: OpenOffice.org -> OPENOFFICE.ORG
        // Allcaps with suffixes: CIA's -> CIA'S

        if (
            (
                capType is (CapitalizationType.Huh or CapitalizationType.HuhInit)
                ||
                (capType == CapitalizationType.All && flags.HasItems)
            )
            &&
            flags.DoesNotContain(Affix.ForbiddenWord)
        )
        {
            flags = flags.Union(SpecialFlags.OnlyUpcaseFlag);
            word = HunspellTextFunctions.MakeTitleCase(word, Affix.Culture);
            AddWord(word, flags, morphs, true, CapitalizationType.Init);
        }
    }

    private readonly ref struct ParsedWordLine
    {
        private ParsedWordLine(ReadOnlySpan<char> word, ReadOnlySpan<char> flags, string[] morphs)
        {
            Morphs = morphs;
            Word = word;
            Flags = flags;
        }

        public readonly string[] Morphs;
        public readonly ReadOnlySpan<char> Word;
        public readonly ReadOnlySpan<char> Flags;

        public static ParsedWordLine Parse(ReadOnlySpan<char> line)
        {
            int i;

            // read past any leading tabs or spaces
            for (i = 0; i < line.Length && line[i].IsTabOrSpace(); ++i) ;

            if (i > 0)
            {
                line = line.Slice(i);
            }

            if (line.IsEmpty)
            {
                return default;
            }

            // Try to locate the end of the word part of a line, taking morphs into consideration
            var endOfWordAndFlagsPosition = findIndexOfFirstMorphByColonCharAndSpacingHints(line);
            if (endOfWordAndFlagsPosition <= 0)
            {
                endOfWordAndFlagsPosition = line.IndexOf('\t');
                if (endOfWordAndFlagsPosition < 0)
                {
                    endOfWordAndFlagsPosition = line.Length;
                }
            }

            for(; endOfWordAndFlagsPosition > 0 && line[endOfWordAndFlagsPosition - 1].IsTabOrSpace(); --endOfWordAndFlagsPosition) ;

            var wordPart = line.Slice(0, endOfWordAndFlagsPosition);
            var morphPart = line.Slice(endOfWordAndFlagsPosition);

            ReadOnlySpan<char> flagsPart;
            var flagsDelimiterPosition = indexOfFlagsDelimiter(wordPart);
            if (flagsDelimiterPosition >= 0)
            {
                flagsPart = wordPart.Slice(flagsDelimiterPosition + 1);
                wordPart = wordPart.Slice(0, flagsDelimiterPosition);
            }
            else
            {
                flagsPart = [];
            }

            if (wordPart.IsEmpty)
            {
                return default;
            }

            string[] morphs;
            if (morphPart.IsEmpty)
            {
                morphs = [];
            }
            else
            {
                var morphsBuilder = ArrayBuilder<string>.Pool.Get();

                foreach (var morph in morphPart.SplitOnTabOrSpace())
                {
                    morphsBuilder.Add(morph.ToString());
                }

                morphs = ArrayBuilder<string>.Pool.ExtractAndReturn(morphsBuilder);
            }

            return new ParsedWordLine(
                word: wordPart,
                flags: flagsPart,
                morphs: morphs);

            static int findIndexOfFirstMorphByColonCharAndSpacingHints(ReadOnlySpan<char> text)
            {
                var index = 0;
                while ((index = text.IndexOf(':', index)) >= 0)
                {
                    var checkLocation = index - 3;
                    if (checkLocation >= 0 && text[checkLocation].IsTabOrSpace())
                    {
                        return checkLocation;
                    }

                    index++;
                }

                return -1;
            }

            static int indexOfFlagsDelimiter(ReadOnlySpan<char> text)
            {
                // NOTE: the first character is ignored as a single slash should be treated as a word
                for (var i = 1; i < text.Length; i++)
                {
                    if (text[i] == '/' && text[i - 1] != '\\')
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }
}
