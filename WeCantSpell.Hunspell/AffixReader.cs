using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed partial class AffixReader
{
    public static readonly Encoding DefaultEncoding = EncodingEx.GetEncodingByName("ISO8859-1") ?? Encoding.UTF8;

    private static readonly ImmutableArray<string> DefaultBreakTableEntries = ImmutableArray.Create("-", "^-", "-$");
    private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.Create("AEIOUaeiou");
    private static readonly TextDictionary<AffixConfigOptions> BitFlagCommandMap;
    private static readonly TextDictionary<AffixReaderCommandKind> CommandMap;

    static AffixReader()
    {
        BitFlagCommandMap = TextDictionary<AffixConfigOptions>.MapFromPairs(
        [
            new("CHECKCOMPOUNDDUP", AffixConfigOptions.CheckCompoundDup),
            new("CHECKCOMPOUNDREP", AffixConfigOptions.CheckCompoundRep),
            new("CHECKCOMPOUNDTRIPLE", AffixConfigOptions.CheckCompoundTriple),
            new("CHECKCOMPOUNDCASE", AffixConfigOptions.CheckCompoundCase),
            new("CHECKNUM", AffixConfigOptions.CheckNum),
            new("CHECKSHARPS", AffixConfigOptions.CheckSharps),
            new("COMPLEXPREFIXES", AffixConfigOptions.ComplexPrefixes),
            new("COMPOUNDMORESUFFIXES", AffixConfigOptions.CompoundMoreSuffixes),
            new("FULLSTRIP", AffixConfigOptions.FullStrip),
            new("FORBIDWARN", AffixConfigOptions.ForbidWarn),
            new("NOSPLITSUGS", AffixConfigOptions.NoSplitSuggestions),
            new("ONLYMAXDIFF", AffixConfigOptions.OnlyMaxDiff),
            new("SIMPLIFIEDTRIPLE", AffixConfigOptions.SimplifiedTriple),
            new("SUGSWITHDOTS", AffixConfigOptions.SuggestWithDots),
        ]);

        CommandMap = TextDictionary<AffixReaderCommandKind>.MapFromPairs(
        [
            new("AF", AffixReaderCommandKind.AliasF),
            new("AM", AffixReaderCommandKind.AliasM),
            new("BREAK", AffixReaderCommandKind.Break),
            new("COMPOUNDBEGIN", AffixReaderCommandKind.CompoundBegin),
            new("COMPOUNDEND", AffixReaderCommandKind.CompoundEnd),
            new("COMPOUNDFLAG", AffixReaderCommandKind.CompoundFlag),
            new("COMPOUNDFORBIDFLAG", AffixReaderCommandKind.CompoundForbidFlag),
            new("COMPOUNDMIDDLE", AffixReaderCommandKind.CompoundMiddle),
            new("COMPOUNDMIN", AffixReaderCommandKind.CompoundMin),
            new("COMPOUNDPERMITFLAG", AffixReaderCommandKind.CompoundPermitFlag),
            new("COMPOUNDROOT", AffixReaderCommandKind.CompoundRoot),
            new("COMPOUNDRULE", AffixReaderCommandKind.CompoundRule),
            new("COMPOUNDSYLLABLE", AffixReaderCommandKind.CompoundSyllable),
            new("COMPOUNDWORDMAX", AffixReaderCommandKind.CompoundWordMax),
            new("CIRCUMFIX", AffixReaderCommandKind.Circumfix),
            new("CHECKCOMPOUNDPATTERN", AffixReaderCommandKind.CheckCompoundPattern),
            new("FLAG", AffixReaderCommandKind.Flag),
            new("FORBIDDENWORD", AffixReaderCommandKind.ForbiddenWord),
            new("FORCEUCASE", AffixReaderCommandKind.ForceUpperCase),
            new("ICONV", AffixReaderCommandKind.InputConversions),
            new("IGNORE", AffixReaderCommandKind.Ignore),
            new("KEY", AffixReaderCommandKind.KeyString),
            new("KEEPCASE", AffixReaderCommandKind.KeepCase),
            new("LANG", AffixReaderCommandKind.Language),
            new("LEMMA_PRESENT", AffixReaderCommandKind.LemmaPresent),
            new("MAP", AffixReaderCommandKind.Map),
            new("MAXNGRAMSUGS", AffixReaderCommandKind.MaxNgramSuggestions),
            new("MAXDIFF", AffixReaderCommandKind.MaxDifferency),
            new("MAXCPDSUGS", AffixReaderCommandKind.MaxCompoundSuggestions),
            new("NEEDAFFIX", AffixReaderCommandKind.NeedAffix),
            new("NOSUGGEST", AffixReaderCommandKind.NoSuggest),
            new("NONGRAMSUGGEST", AffixReaderCommandKind.NoNGramSuggest),
            new("OCONV", AffixReaderCommandKind.OutputConversions),
            new("ONLYINCOMPOUND", AffixReaderCommandKind.OnlyInCompound),
            new("PFX", AffixReaderCommandKind.Prefix),
            new("PSEUDOROOT", AffixReaderCommandKind.NeedAffix),
            new("PHONE", AffixReaderCommandKind.Phone),
            new("REP", AffixReaderCommandKind.Replacement),
            new("SFX", AffixReaderCommandKind.Suffix),
            new("SET", AffixReaderCommandKind.SetEncoding),
            new("SYLLABLENUM", AffixReaderCommandKind.CompoundSyllableNum),
            new("SUBSTANDARD", AffixReaderCommandKind.SubStandard),
            new("TRY", AffixReaderCommandKind.TryString),
            new("VERSION", AffixReaderCommandKind.Version),
            new("WORDCHARS", AffixReaderCommandKind.WordChars),
            new("WARN", AffixReaderCommandKind.Warn),
        ]);
    }

    public AffixReader(AffixConfig.Builder? builder)
    {
        if (builder is null)
        {
            builder = new();
            _ownsBuilder = true;
        }

        _builder = builder;
        _flagParser = new(_builder.FlagMode, _builder.Encoding);
    }

    private readonly bool _ownsBuilder;
    private readonly AffixConfig.Builder _builder;
    private FlagParser _flagParser;
    private EntryListType _initialized = EntryListType.None;

    private Encoding Encoding => _builder.Encoding ?? DefaultEncoding;

    public static Task<AffixConfig> ReadFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        return ReadFileAsync(filePath, builder: null, cancellationToken);
    }

    public static async Task<AffixConfig> ReadFileAsync(string filePath, AffixConfig.Builder? builder, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(filePath);
#else
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));
#endif

        using var stream = StreamEx.OpenAsyncReadFileStream(filePath);
        return await ReadAsync(stream, builder, cancellationToken).ConfigureAwait(false);
    }

    public static Task<AffixConfig> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ReadAsync(stream, builder: null, cancellationToken);

    public static async Task<AffixConfig> ReadAsync(Stream stream, AffixConfig.Builder? builder, CancellationToken cancellationToken = default)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(stream);
#else
        if (stream is null) throw new ArgumentNullException(nameof(stream));
#endif

        return await ReadInternalAsync(stream, builder, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<AffixConfig> ReadInternalAsync(Stream stream, AffixConfig.Builder? builder, CancellationToken cancellationToken)
    {
        var readerInstance = new AffixReader(builder);

        using (var lineReader = new LineReader(stream, readerInstance.Encoding, allowEncodingChanges: true))
        {
            while (await lineReader.ReadNextAsync(cancellationToken))
            {
                readerInstance.ParseLine(lineReader.Current.Span);
            }
        }

        return readerInstance.BuildConfig(allowDestructive: true);
    }

    public static AffixConfig ReadFile(string filePath)
    {
        return ReadFile(filePath, builder: null);
    }

    public static AffixConfig ReadFile(string filePath, AffixConfig.Builder? builder)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(filePath);
#else
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));
#endif

        using var stream = StreamEx.OpenReadFileStream(filePath);
        return Read(stream, builder);
    }

    public static AffixConfig ReadFromString(string contents)
    {
        return ReadFromString(contents, builder: null);
    }

    public static AffixConfig ReadFromString(string contents, AffixConfig.Builder? builder)
    {
        var readerInstance = new AffixReader(builder);

        using var reader = new StringReader(contents);

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            readerInstance.ParseLine(line.AsSpan());
        }

        return readerInstance.BuildConfig(allowDestructive: true);
    }

    public static AffixConfig Read(Stream stream)
    {
        return Read(stream, builder: null);
    }

    public static AffixConfig Read(Stream stream, AffixConfig.Builder? builder)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(stream);
#else
        if (stream is null) throw new ArgumentNullException(nameof(stream));
#endif

        var readerInstance = new AffixReader(builder);

        using var lineReader = new LineReader(stream, readerInstance.Encoding, allowEncodingChanges: true);
        while (lineReader.ReadNext())
        {
            readerInstance.ParseLine(lineReader.Current.Span);
        }

        return readerInstance.BuildConfig(allowDestructive: true);
    }

    private bool ParseLine(ReadOnlySpan<char> line)
    {
        // read through the initial ^[ \t]*
        var commandStartIndex = 0;
        for (; commandStartIndex < line.Length && line[commandStartIndex].IsTabOrSpace(); commandStartIndex++) ;

        if (commandStartIndex == line.Length || isCommentPrefix(line[commandStartIndex]))
        {
            return true; // empty, whitespace, or comment
        }

        // read through the final [ \t]*$
        var lineEndIndex = line.Length - 1;
        for (; lineEndIndex > commandStartIndex && line[lineEndIndex].IsTabOrSpace(); lineEndIndex--) ;

        // find the end of the command
        var commandEndIndex = commandStartIndex;
        for (; commandEndIndex <= lineEndIndex && !line[commandEndIndex].IsTabOrSpace(); commandEndIndex++) ;

        // first command exists between [lineStartIndex,commandEndIndex)
        var parameterStartIndex = commandEndIndex;
        for (; parameterStartIndex <= lineEndIndex && line[parameterStartIndex].IsTabOrSpace(); parameterStartIndex++) ;

        var command = line.Slice(commandStartIndex, commandEndIndex - commandStartIndex);

        if (parameterStartIndex <= lineEndIndex)
        {
            if (TryHandleParameterizedCommand(
                command,
                line.Slice(parameterStartIndex, lineEndIndex - parameterStartIndex + 1)))
            {
                return true;
            }
        }
        else if (BitFlagCommandMap.TryGetValue(command, out var option))
        {
            _builder.EnableOptions(option);
            return true;
        }

        LogWarning("Failed to parse line: " + line.ToString());
        return false;

        static bool isCommentPrefix(char c) => c is '#' or '/';
    }

    private bool IsInitialized(EntryListType flags) => (_initialized & flags) == flags;

    private void SetInitialized(EntryListType flags)
    {
        _initialized |= flags;
    }

    private AffixConfig BuildConfig(bool allowDestructive)
    {
        if (!IsInitialized(EntryListType.Break))
        {
            if (_builder._breakPoints.Count == 0)
            {
                _builder._breakPoints.AddRange(DefaultBreakTableEntries);
            }
        }

        return _builder.ToImmutable(allowDestructive: _ownsBuilder && allowDestructive);
    }

    private bool TryHandleParameterizedCommand(ReadOnlySpan<char> commandName, ReadOnlySpan<char> parameters)
    {
        if (!CommandMap.TryGetValue(commandName, out var command))
        {
            LogWarning($"Unknown command {commandName.ToString()} with params: {parameters.ToString()}");
            return false;
        }

        switch (command)
        {
            case AffixReaderCommandKind.Flag:
                return TrySetFlagMode(parameters);
            case AffixReaderCommandKind.KeyString:
                _builder.KeyString = parameters.ToString();
                return true;
            case AffixReaderCommandKind.TryString:
                _builder.TryString = parameters.ToString();
                return true;
            case AffixReaderCommandKind.SetEncoding:
                if (EncodingEx.GetEncodingByName(parameters) is not { } encoding)
                {
                    LogWarning("Failed to get encoding: " + parameters.ToString());
                    return false;
                }

                _builder.Encoding = encoding;
                _flagParser.Encoding = encoding;
                return true;
            case AffixReaderCommandKind.Language:
                _builder.Language = parameters.ToString();
                _builder.Culture = GetCultureFromLanguage(_builder.Language);
                return true;
            case AffixReaderCommandKind.CompoundSyllableNum:
                _builder.CompoundSyllableNum = parameters.ToString();
                return true;
            case AffixReaderCommandKind.WordChars:
                _builder.WordChars = CharacterSet.Create(parameters);
                return true;
            case AffixReaderCommandKind.Ignore:
                _builder.IgnoredChars = CharacterSet.Create(parameters);
                return true;
            case AffixReaderCommandKind.CompoundFlag:
                return _flagParser.TryParseFlag(parameters, out _builder.CompoundFlag);
            case AffixReaderCommandKind.CompoundMiddle:
                return _flagParser.TryParseFlag(parameters, out _builder.CompoundMiddle);
            case AffixReaderCommandKind.CompoundBegin:
                return _builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes)
                    ? _flagParser.TryParseFlag(parameters, out _builder.CompoundEnd)
                    : _flagParser.TryParseFlag(parameters, out _builder.CompoundBegin);
            case AffixReaderCommandKind.CompoundEnd:
                return _builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes)
                    ? _flagParser.TryParseFlag(parameters, out _builder.CompoundBegin)
                    : _flagParser.TryParseFlag(parameters, out _builder.CompoundEnd);
            case AffixReaderCommandKind.CompoundWordMax:
                _builder.CompoundWordMax = IntEx.TryParseInvariant(parameters);
                return _builder.CompoundWordMax.HasValue;
            case AffixReaderCommandKind.CompoundMin:
                _builder.CompoundMin = IntEx.TryParseInvariant(parameters);

                switch (_builder.CompoundMin)
                {
                    case null:
                        LogWarning("Failed to parse CompoundMin: " + parameters.ToString());
                        return false;
                    case < 1:
                        _builder.CompoundMin = 1;
                        break;
                }

                return true;
            case AffixReaderCommandKind.CompoundRoot:
                return _flagParser.TryParseFlag(parameters, out _builder.CompoundRoot);
            case AffixReaderCommandKind.CompoundPermitFlag:
                return _flagParser.TryParseFlag(parameters, out _builder.CompoundPermitFlag);
            case AffixReaderCommandKind.CompoundForbidFlag:
                return _flagParser.TryParseFlag(parameters, out _builder.CompoundForbidFlag);
            case AffixReaderCommandKind.CompoundSyllable:
                return TryParseCompoundSyllable(parameters);
            case AffixReaderCommandKind.NoSuggest:
                return _flagParser.TryParseFlag(parameters, out _builder.NoSuggest);
            case AffixReaderCommandKind.NoNGramSuggest:
                return _flagParser.TryParseFlag(parameters, out _builder.NoNgramSuggest);
            case AffixReaderCommandKind.ForbiddenWord:
                _builder.ForbiddenWord = _flagParser.ParseFlagOrDefault(parameters);
                return _builder.ForbiddenWord.HasValue;
            case AffixReaderCommandKind.LemmaPresent:
                return _flagParser.TryParseFlag(parameters, out _builder.LemmaPresent);
            case AffixReaderCommandKind.Circumfix:
                return _flagParser.TryParseFlag(parameters, out _builder.Circumfix);
            case AffixReaderCommandKind.OnlyInCompound:
                return _flagParser.TryParseFlag(parameters, out _builder.OnlyInCompound);
            case AffixReaderCommandKind.NeedAffix:
                return _flagParser.TryParseFlag(parameters, out _builder.NeedAffix);
            case AffixReaderCommandKind.Replacement:
                return TryParseStandardListItem(EntryListType.Replacements, parameters, _builder._replacements, TryParseReplacements);
            case AffixReaderCommandKind.InputConversions:
                return TryParseConv(parameters, EntryListType.Iconv, ref _builder._inputConversions);
            case AffixReaderCommandKind.OutputConversions:
                return TryParseConv(parameters, EntryListType.Oconv, ref _builder._outputConversions);
            case AffixReaderCommandKind.Phone:
                return TryParseStandardListItem(EntryListType.Phone, parameters, _builder._phone, TryParsePhone);
            case AffixReaderCommandKind.CheckCompoundPattern:
                return TryParseStandardListItem(EntryListType.CompoundPatterns, parameters, _builder._compoundPatterns, TryParseCheckCompoundPatternIntoCompoundPatterns);
            case AffixReaderCommandKind.CompoundRule:
                return TryParseStandardListItem(EntryListType.CompoundRules, parameters, _builder._compoundRules, TryParseCompoundRuleIntoList);
            case AffixReaderCommandKind.Map:
                return TryParseStandardListItem(EntryListType.Map, parameters, _builder._relatedCharacterMap, TryParseMapEntry);
            case AffixReaderCommandKind.Break:
                return TryParseStandardListItem(EntryListType.Break, parameters, _builder._breakPoints, TryParseBreak);
            case AffixReaderCommandKind.Version:
                _builder.Version = parameters.ToString();
                return true;
            case AffixReaderCommandKind.MaxNgramSuggestions:
                _builder.MaxNgramSuggestions = IntEx.TryParseInvariant(parameters);
                return _builder.MaxNgramSuggestions.HasValue;
            case AffixReaderCommandKind.MaxDifferency:
                _builder.MaxDifferency = IntEx.TryParseInvariant(parameters);
                return _builder.MaxDifferency.HasValue;
            case AffixReaderCommandKind.MaxCompoundSuggestions:
                _builder.MaxCompoundSuggestions = IntEx.TryParseInvariant(parameters);
                return _builder.MaxCompoundSuggestions.HasValue;
            case AffixReaderCommandKind.KeepCase:
                return _flagParser.TryParseFlag(parameters, out _builder.KeepCase);
            case AffixReaderCommandKind.ForceUpperCase:
                return _flagParser.TryParseFlag(parameters, out _builder.ForceUpperCase);
            case AffixReaderCommandKind.Warn:
                return _flagParser.TryParseFlag(parameters, out _builder.Warn);
            case AffixReaderCommandKind.SubStandard:
                return _flagParser.TryParseFlag(parameters, out _builder.SubStandard);
            case AffixReaderCommandKind.Prefix:
            case AffixReaderCommandKind.Suffix:
                var parseAsPrefix = AffixReaderCommandKind.Prefix == command;

                if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
                {
                    parseAsPrefix = !parseAsPrefix;
                }

                return parseAsPrefix
                    ? TryParseAffixIntoList(parameters, _builder.Prefixes)
                    : TryParseAffixIntoList(parameters, _builder.Suffixes);
            case AffixReaderCommandKind.AliasF:
                return TryParseStandardListItem(EntryListType.AliasF, parameters, _builder.AliasF, TryParseAliasF);
            case AffixReaderCommandKind.AliasM:
                return TryParseStandardListItem(EntryListType.AliasM, parameters, _builder.AliasM, TryParseAliasM);
            default:
                LogWarning($"Unknown parsed command {command}");
                return false;
        }
    }

    private delegate bool EntryParserForImmutableArray<T>(ReadOnlySpan<char> parameterText, ImmutableArray<T>.Builder entries);
    private bool TryParseStandardListItem<T>(EntryListType entryListType, ReadOnlySpan<char> parameterText, ImmutableArray<T>.Builder entries, EntryParserForImmutableArray<T> parse)
    {
        if (!IsInitialized(entryListType))
        {
            SetInitialized(entryListType);

            if (IntEx.TryParseInvariant(parameterText, out var expectedSize) && expectedSize is >= 0 and <= CollectionsEx.CollectionPreallocationLimit)
            {
                entries.Capacity = expectedSize;
                return true;
            }
        }

        return parse(parameterText, entries);
    }

    private delegate bool EntryParserForArray<T>(ReadOnlySpan<char> parameterText, ArrayBuilder<T> entries);
    private bool TryParseStandardListItem<T>(EntryListType entryListType, ReadOnlySpan<char> parameterText, ArrayBuilder<T> entries, EntryParserForArray<T> parse)
    {
        if (!IsInitialized(entryListType))
        {
            SetInitialized(entryListType);

            if (IntEx.TryParseInvariant(parameterText, out var expectedSize) && expectedSize is >= 0 and <= CollectionsEx.CollectionPreallocationLimit)
            {
                entries.GrowToCapacity(expectedSize);
                return true;
            }
        }

        return parse(parameterText, entries);
    }

    private bool TryParseCompoundSyllable(ReadOnlySpan<char> parameters)
    {
        var state = 0;

        foreach (var part in parameters.SplitOnTabOrSpace())
        {
            if (state == 0)
            {
                if (IntEx.TryParseInvariant(part) is { } maxValue)
                {
                    _builder.CompoundMaxSyllable = maxValue;
                    _builder.CompoundVowels = DefaultCompoundVowels;
                    state = 1;
                    continue;
                }
            }
            else if (state == 1)
            {
                _builder.CompoundVowels = CharacterSet.Create(part);
                state = 2;
                continue;
            }

            state = -1;
            break;
        }

        if (state > 0)
        {
            return true;
        }

        LogWarning("Failed to parse CompoundMaxSyllable value from: " + parameters.ToString());
        return false;
    }

    private static CultureInfo GetCultureFromLanguage(string? language)
    {
        if (language is not { Length: > 0 })
        {
            return CultureInfo.InvariantCulture;
        }

        language = language.Replace('_', '-');

        try
        {
            return new CultureInfo(language);
        }
        catch (CultureNotFoundException)
        {
            if (language.IndexOf('-') is int dashIndex and > 0)
            {
                return GetCultureFromLanguage(language.Substring(0, dashIndex));
            }
            else
            {
                return CultureInfo.InvariantCulture;
            }
        }
        catch (ArgumentException)
        {
            return CultureInfo.InvariantCulture;
        }
    }

    private bool TryParsePhone(ReadOnlySpan<char> parameterText, ArrayBuilder<PhoneticEntry> entries)
    {
        var rule = ReadOnlySpan<char>.Empty;
        var replace = ReadOnlySpan<char>.Empty;
        var state = 0;

        foreach (var part in parameterText.SplitOnTabOrSpace())
        {
            if (state == 0)
            {
                rule = part;
                state = 1;
                continue;
            }
            else if (state == 1)
            {
                replace = part;
                state = 2;
                continue;
            }
            else
            {
                state = -1;
                break;
            }
        }

        if (state < 1)
        {
            LogWarning("Failed to parse phone line: " + parameterText.ToString());
            return false;
        }

        entries.Add(new PhoneticEntry(rule.ToString(), replace.ToStringWithoutChars('_')));

        return true;
    }

    private bool TryParseMapEntry(ReadOnlySpan<char> parameterText, ArrayBuilder<MapEntry> entries)
    {
        var valuesBuilder = new ArrayBuilder<string>(parameterText.Length / 2);

        for (var k = 0; k < parameterText.Length; k++)
        {
            var chb = k;
            var che = k + 1;
            if (parameterText[k] == '(' && parameterText.IndexOf(')', k) is int parpos and >= 0)
            {
                chb = k + 1;
                che = parpos;
                k = parpos;
            }

            valuesBuilder.Add(parameterText.Slice(chb, che - chb).ToString());
        }

        entries.Add(new MapEntry(valuesBuilder.Extract()));

        return true;
    }

    private bool TryParseConv(ReadOnlySpan<char> parameterText, EntryListType entryListType, ref TextDictionary<MultiReplacementEntry> entries)
    {
        if (!IsInitialized(entryListType))
        {
            SetInitialized(entryListType);

            if (IntEx.TryParseInvariant(ParseLeadingDigits(parameterText), out var expectedSize) && expectedSize >= 0)
            {
                entries ??= new(expectedSize);

                return true;
            }
        }

        entries ??= new(1);

        var pattern1 = ReadOnlySpan<char>.Empty;
        var pattern2 = ReadOnlySpan<char>.Empty;
        var state = 0;
        foreach (var part in parameterText.SplitOnTabOrSpace())
        {
            if (state == 0)
            {
                pattern1 = part;
                state = 1;
                continue;
            }
            else if (state == 1)
            {
                pattern2 = part;
                state = 2;
                break; // There may be comments after this, so processing needs to stop here
            }
            else
            {
                handleInvalidConv();
            }
        }

        if (state < 2)
        {
            LogWarning($"Bad {entryListType}: {parameterText.ToString()}");
            return false;
        }

        var type = ReplacementValueType.Med;

        if (pattern1.StartsWith('_'))
        {
            type |= ReplacementValueType.Ini;
            pattern1 = pattern1.Slice(1);
        }

        if (pattern1.EndsWith('_'))
        {
            type |= ReplacementValueType.Fin;
            pattern1 = pattern1.Slice(0, pattern1.Length - 1);
        }

        var pattern1String = pattern1.ReplaceIntoString('_', ' ');

        // find existing entry
        if (!entries.TryGetValue(pattern1String, out var entry))
        {
            // make a new entry if none exists
            entry = new MultiReplacementEntry(pattern1String);
            entries.Add(entry.Pattern, entry);
        }

        entry.Set(type, pattern2.ReplaceIntoString('_', ' '));
        return true;

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static void handleInvalidConv()
        {
            throw new InvalidOperationException();
        }
    }

    private bool TryParseBreak(ReadOnlySpan<char> parameterText, ArrayBuilder<string> entries)
    {
        entries.Add(parameterText.ToString());
        return true;
    }

    private bool TryParseAliasF(ReadOnlySpan<char> parameterText, ImmutableArray<FlagSet>.Builder entries)
    {
        entries.Add(_flagParser.ParseFlagSet(parameterText));
        return true;
    }

    private bool TryParseAliasM(ReadOnlySpan<char> parameterText, ImmutableArray<MorphSet>.Builder entries)
    {
        var parts = ArrayBuilderPool<string>.Get();

        if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
        {
            foreach (var part in parameterText.SplitOnTabOrSpace())
            {
                parts.Add(part.ToStringReversed());
            }

            parts.Reverse();
        }
        else
        {
            foreach (var part in parameterText.SplitOnTabOrSpace())
            {
                parts.Add(part.ToString());
            }
        }

        entries.Add(new MorphSet(ArrayBuilderPool<string>.ExtractAndReturn(parts)));

        return true;
    }

    private bool TryParseCompoundRuleIntoList(ReadOnlySpan<char> parameterText, ArrayBuilder<CompoundRule> entries)
    {
        FlagValue[] values;
        if (parameterText.Contains('('))
        {
            var entryBuilder = new List<FlagValue>();
            for (var index = 0; index < parameterText.Length; index++)
            {
                var indexBegin = index;
                var indexEnd = indexBegin + 1;
                if (parameterText[indexBegin] == '(' && parameterText.IndexOf(')', indexEnd) is int closeParenIndex and >= 0)
                {
                    indexBegin = indexEnd;
                    indexEnd = closeParenIndex;
                    index = closeParenIndex;
                }

                var beginFlagValue = new FlagValue(parameterText[indexBegin]);
                if (beginFlagValue.IsWildcard)
                {
                    entryBuilder.Add(beginFlagValue);
                }
                else
                {
                    entryBuilder.AddRange(_flagParser.ParseFlagsInOrder(parameterText.Slice(indexBegin, indexEnd - indexBegin)));
                }
            }

            values = entryBuilder.ToArray();
        }
        else
        {
            values = _flagParser.ParseFlagsInOrder(parameterText);
        }

        entries.Add(new(values));
        return true;
    }

    private bool TryParseAffixIntoList<TAffixEntry>(ReadOnlySpan<char> parameterText, AffixCollection<TAffixEntry>.BuilderBase affixBuilder)
        where TAffixEntry : AffixEntry
    {
        var affixParser = new AffixParametersParser(parameterText);

        if (!affixParser.TryParseNextAffixFlag(_flagParser, out var aFlag))
        {
            LogWarning("Failed to parse affix flag: " + parameterText.ToString());
            return false;
        }

        var group1 = affixParser.ParseNextArgument();
        var group2 = affixParser.ParseNextArgument();

        if (group1.IsEmpty || group2.IsEmpty)
        {
            LogWarning("Failed to parse affix line: " + parameterText.ToString());
            return false;
        }

        var contClass = FlagSet.Empty;

        var groupBuilder = affixBuilder.ForGroup(aFlag);
        if (!groupBuilder.IsInitialized)
        {
            // If the affix group is new, this should be the init line for it
            var options = AffixEntryOptions.None;
            if (group1.StartsWith('Y'))
            {
                options |= AffixEntryOptions.CrossProduct;
            }
            if (_builder.AliasM is { Count: > 0 })
            {
                options |= AffixEntryOptions.AliasM;
            }
            if (_builder.AliasF is { Count: > 0 })
            {
                options |= AffixEntryOptions.AliasF;
            }

            _ = IntEx.TryParseInvariant(group2, out var expectedEntryCount);

            groupBuilder.Initialize(options, expectedEntryCount);

            return true;
        }

        var group3 = affixParser.ParseNextArgument();
        if (group3.IsEmpty && group2.EqualsOrdinal("."))
        {
            // In some special cases it seems as if the group 2 is blank but groups 1 and 3 have values in them.
            // I think this is a way to make a blank affix value.
            group3 = group2;
            group2 = [];
        }

        // piece 3 - is string to strip or 0 for null
        string strip;
        if (group1.Equals("0".AsSpan(), StringComparison.Ordinal))
        {
            strip = string.Empty;
        }
        else if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
        {
            strip = group1.ToStringReversed();
        }
        else
        {
            strip = group1.ToString();
        }

        // piece 4 - is affix string or 0 for null
        StringBuilder affixTextBuilder;
        string affixText;

        if (group2.IndexOf('/') is int affixSlashIndex and >= 0)
        {
            affixTextBuilder = StringBuilderPool.Get(group2.Slice(0, affixSlashIndex));

            if (_builder.AliasF is { Count: > 0 } aliasF)
            {
                if (IntEx.TryParseInvariant(group2.Slice(affixSlashIndex + 1), out var aliasNumber) && aliasNumber > 0 && aliasNumber <= aliasF.Count)
                {
                    contClass = aliasF[aliasNumber - 1];
                }
                else
                {
                    LogWarning($"Failed to parse contclasses from : {parameterText.ToString()}");
                    return false;
                }
            }
            else
            {
                contClass = _flagParser.ParseFlagSet(group2.Slice(affixSlashIndex + 1));
            }
        }
        else
        {
            affixTextBuilder = StringBuilderPool.Get(group2);
        }

        if (_builder.IgnoredChars.HasItems)
        {
            affixTextBuilder.RemoveChars(_builder.IgnoredChars);
        }

        if (affixTextBuilder.Length == 1 && affixTextBuilder[0] == '0')
        {
            StringBuilderPool.Return(affixTextBuilder);
            affixText = string.Empty;
        }
        else
        {
            if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
            {
                affixTextBuilder.Reverse();
            }

            affixText = StringBuilderPool.GetStringAndReturn(affixTextBuilder);
        }

        // piece 5 - is the conditions descriptions
        var conditionText = group3;
        if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
        {
            conditionText = ReverseCondition(conditionText).AsSpan();
        }

        var conditions = CharacterConditionGroup.Parse(conditionText);
        if (strip.Length != 0 && !conditions.MatchesAnySingleCharacter)
        {
            if (conditions.IsOnlyPossibleMatch(strip.AsSpan()))
            {
                // determine if the condition is redundant
                conditions = CharacterConditionGroup.AllowAnySingleCharacter;
            }
        }

        var group4 = affixParser.ParseFinalArguments();

        // piece 6
        MorphSet morph;
        if (!group4.IsEmpty)
        {
            var morphAffixText = group4;
            if (_builder.AliasM is { Count: > 0 } aliasM)
            {
                if (IntEx.TryParseInvariant(morphAffixText, out var morphNumber) && morphNumber > 0 && morphNumber <= aliasM.Count)
                {
                    morph = aliasM[morphNumber - 1];
                }
                else
                {
                    LogWarning($"Failed to parse morph {morphAffixText.ToString()} from: {parameterText.ToString()}");
                    return false;
                }
            }
            else
            {
                var morphSetBuilder = new List<string>();

                if (_builder.Options.HasFlagEx(AffixConfigOptions.ComplexPrefixes))
                {
                    foreach (var morphValue in morphAffixText.SplitOnTabOrSpace())
                    {
                        morphSetBuilder.Insert(0, morphValue.ToStringReversed());
                    }
                }
                else
                {
                    foreach (var morphValue in morphAffixText.SplitOnTabOrSpace())
                    {
                        morphSetBuilder.Add(morphValue.ToString());
                    }
                }

                morph = MorphSet.Create(morphSetBuilder);
            }
        }
        else
        {
            morph = MorphSet.Empty;
        }

        if (!_builder.HasContClass && contClass.HasItems)
        {
            _builder.HasContClass = true;
        }

        groupBuilder.AddEntry(
            strip,
            affixText,
            conditions,
            morph,
            contClass);

        return true;
    }

    private static string ReverseCondition(ReadOnlySpan<char> conditionText)
    {
        // TODO: Would it be better to reverse the conditions after parsing?
        //       Instead of reversing a string it could reverse a CharacterConditionGroup.

        if (conditionText.IsEmpty)
        {
            return string.Empty;
        }

        var chars = StringBuilderPool.Get(conditionText);
        chars.Reverse();
        var neg = false;
        var lastIndex = chars.Length - 1;

        for (var k = lastIndex; k >= 0; k--)
        {
            switch (chars[k])
            {
                case '[':
                    if (neg)
                    {
                        if (k < lastIndex)
                        {
                            chars[k + 1] = '[';
                        }
                    }
                    else
                    {
                        chars[k] = ']';
                    }

                    break;
                case ']':
                    chars[k] = '[';
                    if (neg && k < lastIndex)
                    {
                        chars[k + 1] = '^';
                    }

                    neg = false;

                    break;
                case '^':
                    if (k < lastIndex)
                    {
                        if (chars[k + 1] == ']')
                        {
                            neg = true;
                        }
                        else if (neg)
                        {
                            chars[k + 1] = chars[k];
                        }
                    }

                    break;
                default:
                    if (neg && k < lastIndex)
                    {
                        chars[k + 1] = chars[k];
                    }
                    break;
            }
        }

        return StringBuilderPool.GetStringAndReturn(chars);
    }

    private bool TryParseReplacements(ReadOnlySpan<char> parameterText, ArrayBuilder<SingleReplacement> entries)
    {
        var pattern = ReadOnlySpan<char>.Empty;
        var outString = ReadOnlySpan<char>.Empty;
        var state = 0;
        foreach (var part in parameterText.SplitOnTabOrSpace())
        {
            if (state == 0)
            {
                pattern = part;
                state = 1;
                continue;
            }
            else if (state == 1)
            {
                outString = part;
                state = 2;
                continue;
            }
            else
            {
                break;
            }
        }

        if (state < 1)
        {
            LogWarning("Failed to parse replacements from: " + parameterText.ToString());
            return false;
        }

        var type = ReplacementValueType.Med;

        if (pattern.StartsWith('^'))
        {
            type |= ReplacementValueType.Ini;
            pattern = pattern.Slice(1);
        }

        if (pattern.EndsWith('$'))
        {
            type |= ReplacementValueType.Fin;
            pattern = pattern.Slice(0, pattern.Length - 1);
        }

        entries.Add(new SingleReplacement(
            pattern.ReplaceIntoString('_', ' '),
            outString.ReplaceIntoString('_', ' '),
            type));

        return true;
    }

    private enum ParseCheckCompoundPatternState : sbyte
    {
        ParsePattern1 = 0,
        ParsePattern2 = 1,
        ParsePattern3 = 2,
        Done = 3,
        UnknownFailure = -1,
        FailCondition1 = -2,
        FailCondition2 = -3
    }

    private bool TryParseCheckCompoundPatternIntoCompoundPatterns(ReadOnlySpan<char> parameterText, ArrayBuilder<PatternEntry> entries)
    {
        int slashIndex;
        var pattern1 = ReadOnlySpan<char>.Empty;
        var pattern2 = ReadOnlySpan<char>.Empty;
        var pattern3 = ReadOnlySpan<char>.Empty;
        FlagValue condition1 = default;
        FlagValue condition2 = default;
        var state = ParseCheckCompoundPatternState.ParsePattern1;

        foreach (var part in parameterText.SplitOnTabOrSpace())
        {
            if (state == ParseCheckCompoundPatternState.ParsePattern1)
            {
                slashIndex = part.IndexOf('/');
                if (slashIndex >= 0)
                {
                    condition1 = _flagParser.ParseFlagOrDefault(part.Slice(slashIndex + 1));
                    if (!condition1.HasValue)
                    {
                        state = ParseCheckCompoundPatternState.FailCondition1;
                        break;
                    }

                    pattern1 = part.Slice(0, slashIndex);
                }
                else
                {
                    pattern1 = part;
                }


                state = ParseCheckCompoundPatternState.ParsePattern2;
                continue;
            }
            else if (state == ParseCheckCompoundPatternState.ParsePattern2)
            {
                slashIndex = part.IndexOf('/');
                if (slashIndex >= 0)
                {
                    condition2 = _flagParser.ParseFlagOrDefault(part.Slice(slashIndex + 1));
                    if (!condition2.HasValue)
                    {
                        state = ParseCheckCompoundPatternState.FailCondition2;
                        break;
                    }

                    pattern2 = part.Slice(0, slashIndex);
                }
                else
                {
                    pattern2 = part;
                }

                state = ParseCheckCompoundPatternState.ParsePattern3;
                continue;
            }
            else if (state == ParseCheckCompoundPatternState.ParsePattern3)
            {
                pattern3 = part;
                _builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);

                state = ParseCheckCompoundPatternState.Done;
                continue;
            }
            else
            {
                state = ParseCheckCompoundPatternState.UnknownFailure;
                break;
            }
        }

        if (state < 0)
        {
            if (state == ParseCheckCompoundPatternState.FailCondition1)
            {
                LogWarning($"Failed to parse pattern condition 1 from: {parameterText.ToString()}");
            }
            else if (state == ParseCheckCompoundPatternState.FailCondition2)
            {
                LogWarning($"Failed to parse pattern condition 2 from: {parameterText.ToString()}");
            }
            else
            {
                LogWarning($"Failed to parse compound pattern from: {parameterText.ToString()}");
            }

            return false;
        }

        entries.Add(new PatternEntry(
            pattern1.ToString(),
            pattern2.ToString(),
            pattern3.ToString(),
            condition1,
            condition2));

        return true;
    }

    private bool TrySetFlagMode(ReadOnlySpan<char> modeText)
    {
        if (modeText.IsEmpty)
        {
            LogWarning("Attempt to set empty flag mode.");
            return false;
        }

        if (TryParseFlagMode(modeText) is not { } mode)
        {
            LogWarning($"Unknown FlagMode: {modeText.ToString()}");
            return false;
        }

        if (_builder.FlagMode == mode)
        {
            LogWarning($"Redundant FlagMode: {modeText.ToString()}");
            return false;
        }

        _builder.FlagMode = mode;
        _flagParser.Mode = mode;
        return true;
    }

    private void LogWarning(string text)
    {
        _builder.LogWarning(text);
    }

    private static ReadOnlySpan<char> ParseLeadingDigits(ReadOnlySpan<char> text)
    {
        text = text.TrimStart();

        if (text.IsEmpty)
        {
            return text;
        }

        var firstNonDigitIndex = 0;
        for (; firstNonDigitIndex < text.Length && char.IsDigit(text[firstNonDigitIndex]); firstNonDigitIndex++) ;

        return firstNonDigitIndex < text.Length
            ? text.Slice(0, firstNonDigitIndex)
            : text;
    }

    private static FlagParsingMode? TryParseFlagMode(ReadOnlySpan<char> value)
    {
        if (value.Length >= 3)
        {
            if (value.StartsWith("CHAR", StringComparison.OrdinalIgnoreCase))
            {
                return FlagParsingMode.Char;
            }
            if (value.StartsWith("LONG", StringComparison.OrdinalIgnoreCase))
            {
                return FlagParsingMode.Long;
            }
            if (value.StartsWith("NUM", StringComparison.OrdinalIgnoreCase))
            {
                return FlagParsingMode.Num;
            }
            if (value.StartsWith("UTF", StringComparison.OrdinalIgnoreCase) || value.StartsWith("UNI", StringComparison.OrdinalIgnoreCase))
            {
                return FlagParsingMode.Uni;
            }
        }

        return default;
    }

    [Flags]
    private enum EntryListType : short
    {
        None = 0,
        Replacements = 1 << 0,
        CompoundRules = 1 << 1,
        CompoundPatterns = 1 << 2,
        AliasF = 1 << 3,
        AliasM = 1 << 4,
        Break = 1 << 5,
        Iconv = 1 << 6,
        Oconv = 1 << 7,
        Map = 1 << 8,
        Phone = 1 << 9
    }

    private enum AffixReaderCommandKind : byte
    {
        /// <summary>
        /// parse in the try string
        /// </summary>
        Flag,
        /// <summary>
        /// parse in the keyboard string
        /// </summary>
        KeyString,
        /// <summary>
        /// parse in the try string
        /// </summary>
        TryString,
        /// <summary>
        /// parse in the name of the character set used by the .dict and .aff
        /// </summary>
        SetEncoding,
        /// <summary>
        /// parse in the language for language specific codes
        /// </summary>
        Language,
        /// <summary>
        /// parse in the flag used by compound_check() method
        /// </summary>
        CompoundSyllableNum,
        /// <summary>
        /// parse in the extra word characters
        /// </summary>
        WordChars,
        /// <summary>
        /// parse in the ignored characters (for example, Arabic optional diacretics characters)
        /// </summary>
        Ignore,
        /// <summary>
        /// parse in the flag used by the controlled compound words
        /// </summary>
        CompoundFlag,
        /// <summary>
        /// parse in the flag used by compound words
        /// </summary>
        CompoundMiddle,
        /// <summary>
        /// parse in the flag used by compound words
        /// </summary>
        CompoundBegin,
        /// <summary>
        /// parse in the flag used by compound words
        /// </summary>
        CompoundEnd,
        /// <summary>
        /// parse in the data used by compound_check() method
        /// </summary>
        CompoundWordMax,
        /// <summary>
        /// parse in the minimal length for words in compounds
        /// </summary>
        CompoundMin,
        /// <summary>
        /// parse in the flag sign compounds in dictionary
        /// </summary>
        CompoundRoot,
        /// <summary>
        /// parse in the flag used by compound_check() method
        /// </summary>
        CompoundPermitFlag,
        /// <summary>
        /// parse in the flag used by compound_check() method
        /// </summary>
        CompoundForbidFlag,
        /// <summary>
        /// parse in the max. words and syllables in compounds
        /// </summary>
        CompoundSyllable,
        NoSuggest,
        NoNGramSuggest,
        /// <summary>
        /// parse in the flag used by forbidden words
        /// </summary>
        ForbiddenWord,
        /// <summary>
        /// parse in the flag used by forbidden words
        /// </summary>
        LemmaPresent,
        /// <summary>
        /// parse in the flag used by circumfixes
        /// </summary>
        Circumfix,
        /// <summary>
        /// parse in the flag used by fogemorphemes
        /// </summary>
        OnlyInCompound,
        /// <summary>
        /// parse in the flag used by `needaffixs'
        /// </summary>
        NeedAffix,
        /// <summary>
        /// parse in the typical fault correcting table
        /// </summary>
        Replacement,
        /// <summary>
        /// parse in the input conversion table
        /// </summary>
        InputConversions,
        /// <summary>
        /// parse in the output conversion table
        /// </summary>
        OutputConversions,
        /// <summary>
        /// parse in the phonetic conversion table
        /// </summary>
        Phone,
        /// <summary>
        /// parse in the checkcompoundpattern table
        /// </summary>
        CheckCompoundPattern,
        /// <summary>
        /// parse in the defcompound table
        /// </summary>
        CompoundRule,
        /// <summary>
        /// parse in the related character map table
        /// </summary>
        Map,
        /// <summary>
        /// parse in the word breakpoints table
        /// </summary>
        Break,
        Version,
        MaxNgramSuggestions,
        MaxDifferency,
        MaxCompoundSuggestions,
        /// <summary>
        /// parse in the flag used by forbidden words
        /// </summary>
        KeepCase,
        ForceUpperCase,
        Warn,
        SubStandard,
        Prefix,
        Suffix,
        AliasF,
        AliasM
    }
}
