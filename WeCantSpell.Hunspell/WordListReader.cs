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
        Builder = builder ?? new WordList.Builder(affix);
        Affix = affix;
        FlagParser = new FlagParser(Affix.FlagMode, Affix.Encoding);
    }

    private bool _hasInitialized;

    private WordList.Builder Builder { get; }

    private AffixConfig Affix { get; }

    private FlagParser FlagParser { get; }

    private TextInfo TextInfo => Affix.Culture.TextInfo;

    public static async Task<WordList> ReadFileAsync(string dictionaryFilePath)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));

        var affixFilePath = FindAffixFilePath(dictionaryFilePath);
        return await ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false);
    }

    public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, string affixFilePath)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affixFilePath is null) throw new ArgumentNullException(nameof(affixFilePath));

        var affixBuilder = new AffixConfig.Builder();
        var affix = await AffixReader.ReadFileAsync(affixFilePath, affixBuilder).ConfigureAwait(false);
        var wordListBuilder = new WordList.Builder(affix);
        return await ReadFileAsync(dictionaryFilePath, affix, wordListBuilder);
    }

    public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, AffixConfig affix, WordList.Builder? builder = null)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affix is null) throw new ArgumentNullException(nameof(affix));

        using var stream = StreamEx.OpenAsyncReadFileStream(dictionaryFilePath);
        return await ReadAsync(stream, affix, builder).ConfigureAwait(false);
    }

    public static async Task<WordList> ReadAsync(Stream dictionaryStream, Stream affixStream)
    {
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affixStream is null) throw new ArgumentNullException(nameof(affixStream));

        var affixBuilder = new AffixConfig.Builder();
        var affix = await AffixReader.ReadAsync(affixStream, affixBuilder).ConfigureAwait(false);
        var wordListBuilder = new WordList.Builder(affix);
        return await ReadAsync(dictionaryStream, affix, wordListBuilder);
    }

    public static async Task<WordList> ReadAsync(Stream dictionaryStream, AffixConfig affix, WordList.Builder? builder = null)
    {
        var ct = CancellationToken.None; // TODO

        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affix is null) throw new ArgumentNullException(nameof(affix));

        var readerInstance = new WordListReader(builder, affix);

        await performRead().ConfigureAwait(false);

        return readerInstance.Builder.MoveToImmutable();

        async Task performRead()
        {
            using var lineReader = LineReader.Create(dictionaryStream, affix.Encoding);
            while (await lineReader.MoveNextAsync(ct))
            {
                readerInstance.ParseLine(lineReader.Current.Span);
            }
        }
    }

    public static WordList ReadFile(string dictionaryFilePath)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));

        var affixFilePath = FindAffixFilePath(dictionaryFilePath);
        return ReadFile(dictionaryFilePath, AffixReader.ReadFile(affixFilePath));
    }

    public static WordList ReadFile(string dictionaryFilePath, string affixFilePath)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affixFilePath is null) throw new ArgumentNullException(nameof(affixFilePath));

        var affixBuilder = new AffixConfig.Builder();
        var affix = AffixReader.ReadFile(affixFilePath, affixBuilder);
        var wordListBuilder = new WordList.Builder(affix);
        return ReadFile(dictionaryFilePath, affix, wordListBuilder);
    }

    public static WordList ReadFile(string dictionaryFilePath, AffixConfig affix, WordList.Builder? builder = null)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));
        if (affix is null) throw new ArgumentNullException(nameof(affix));

        using var stream = StreamEx.OpenReadFileStream(dictionaryFilePath);
        return Read(stream, affix, builder);
    }

    public static WordList Read(Stream dictionaryStream, Stream affixStream)
    {
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(dictionaryStream));
        if (affixStream is null) throw new ArgumentNullException(nameof(affixStream));

        var affixBuilder = new AffixConfig.Builder();
        var affix = AffixReader.Read(affixStream, affixBuilder);
        var wordListBuilder = new WordList.Builder(affix);
        return Read(dictionaryStream, affix, wordListBuilder);
    }

    public static WordList Read(Stream dictionaryStream, AffixConfig affix, WordList.Builder? builder = null)
    {
        if (dictionaryStream is null) throw new ArgumentNullException(nameof(affix));
        if (affix is null) throw new ArgumentNullException(nameof(affix));

        var readerInstance = new WordListReader(builder, affix);

        using var lineReader = LineReader.Create(dictionaryStream, affix.Encoding);
        while (lineReader.MoveNext())
        {
            readerInstance.ParseLine(lineReader.Current.Span);
        }

        return readerInstance.Builder.MoveToImmutable();
    }

    private static string FindAffixFilePath(string dictionaryFilePath)
    {
        if (dictionaryFilePath is null) throw new ArgumentNullException(nameof(dictionaryFilePath));

        var directoryName = Path.GetDirectoryName(dictionaryFilePath);
        if (!string.IsNullOrEmpty(directoryName))
        {
            var locatedAffFile = Directory.GetFiles(directoryName, Path.GetFileNameWithoutExtension(dictionaryFilePath) + ".*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault(affFilePath => ".AFF".Equals(Path.GetExtension(affFilePath), System.StringComparison.OrdinalIgnoreCase));

            if (locatedAffFile is not null)
            {
                return locatedAffFile;
            }
        }

        return Path.ChangeExtension(dictionaryFilePath, "aff");
    }

    private bool ParseLine(ReadOnlySpan<char> line)
    {
        if (line.Length == 0)
        {
            return true;
        }

        if (!_hasInitialized)
        {
            if (AttemptToProcessInitializationLine(line))
            {
                return true;
            }

            Builder.InitializeEntriesByRoot(-1);
        }

        var parsed = ParsedWordLine.Parse(line);
        if (parsed.Word.IsEmpty)
        {
            return false;
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
                    return false;
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

        return AddWord(parsed.Word.ToString(), flags, parsed.Morphs);
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

    private bool AddWord(string word, FlagSet flags, ImmutableArray<string> morphs)
    {
        if (Affix.IgnoredChars.HasItems)
        {
            word = word.WithoutChars(Affix.IgnoredChars);
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
        return AddWord(word, flags, morphs, false, capType)
            || AddWordCapitalized(word, flags, morphs, capType);
    }

    private ImmutableArray<string> AddWord_HandleMorph(ImmutableArray<string> morphs, string word, CapitalizationType capType, ref WordEntryOptions options)
    {
        if (Affix.IsAliasM)
        {
            options |= WordEntryOptions.AliasM;
            var morphBuilder = ImmutableArray.CreateBuilder<string>();
            foreach (var originalValue in morphs)
            {
                if (IntEx.TryParseInvariant(originalValue, out var morphNumber) && Affix.TryGetAliasM(morphNumber, out var aliasedMorph))
                {
                    morphBuilder.AddRange(aliasedMorph);
                }
                else
                {
                    morphBuilder.Add(originalValue);
                }
            }

            morphs = morphBuilder.ToImmutable(allowDestructive: true);
        }

        using (var morphPhonEnumerator = morphs.Where(m => m != null && m.StartsWith(MorphologicalTags.Phon)).GetEnumerator())
        {
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
                    int strippatt = ph.IndexOf("->".AsSpan());
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
                                Builder.PhoneticReplacements.Add(new SingleReplacement(phString, wordpartLower, ReplacementValueType.Med));
                            }

                            Builder.PhoneticReplacements.Add(new SingleReplacement(phCapitalized, wordpartString, ReplacementValueType.Med));
                        }
                    }

                    Builder.PhoneticReplacements.Add(new SingleReplacement(phString, wordpartString, ReplacementValueType.Med));
                }
                while (morphPhonEnumerator.MoveNext());
            }
        }

        return morphs;
    }

    private bool AddWord(string word, FlagSet flags, ImmutableArray<string> morphs, bool onlyUpperCase, CapitalizationType capType)
    {
        // store the description string or its pointer
        var options = capType == CapitalizationType.Init ? WordEntryOptions.InitCap : WordEntryOptions.None;
        if (morphs.Length != 0)
        {
            morphs = AddWord_HandleMorph(morphs, word, capType, ref options);
        }

        var details = Builder.GetOrCreateDetailList(word);

        var upperCaseHomonym = false;
        if (!onlyUpperCase)
        {
            for (var i = 0; i < details.Count; i++)
            {
                var existingEntry = details[i];
                if (existingEntry.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                {
                    details[i] = new WordEntryDetail(flags, existingEntry.Morphs, existingEntry.Options);
                    return false;
                }
            }
        }
        else if (details.Count != 0)
        {
            upperCaseHomonym = true;
        }

        if (!upperCaseHomonym)
        {
            details.Add(
                new WordEntryDetail(
                    flags,
                    new MorphSet(morphs),
                    options));
        }

        return false;
    }

    private bool AddWordCapitalized(string word, FlagSet flags, ImmutableArray<string> morphs, CapitalizationType capType)
    {
        // add inner capitalized forms to handle the following allcap forms:
        // Mixed caps: OpenOffice.org -> OPENOFFICE.ORG
        // Allcaps with suffixes: CIA's -> CIA'S

        if (
            (
                capType == CapitalizationType.Huh
                || capType == CapitalizationType.HuhInit
                || (capType == CapitalizationType.All && flags.HasItems)
            )
            &&
            !flags.Contains(Affix.ForbiddenWord)
        )
        {
            flags = flags.Union(SpecialFlags.OnlyUpcaseFlag);
            word = HunspellTextFunctions.MakeTitleCase(word, Affix.Culture);
            return AddWord(word, flags, morphs, true, CapitalizationType.Init);
        }

        return false;
    }

    private readonly ref struct ParsedWordLine
    {
        private ParsedWordLine(ReadOnlySpan<char> word, ReadOnlySpan<char> flags, ImmutableArray<string> morphs)
        {
            Word = word;
            Flags = flags;
            Morphs = morphs;
        }

        public readonly ReadOnlySpan<char> Word;
        public readonly ReadOnlySpan<char> Flags;
        public readonly ImmutableArray<string> Morphs;

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
                flagsPart = ReadOnlySpan<char>.Empty;
            }

            if (wordPart.IsEmpty)
            {
                return default;
            }

            var morphs = ImmutableArray<string>.Empty;
            if (!morphPart.IsEmpty)
            {
                var morphsBuilder = ImmutableArray.CreateBuilder<string>();
                foreach (var morph in morphPart.SplitOnTabOrSpace())
                {
                    morphsBuilder.Add(morph.ToString());
                }

                morphs = morphsBuilder.ToImmutable(allowDestructive: true);
            }

            return new ParsedWordLine(
                word: wordPart.Replace(@"\/", @"/"),
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
