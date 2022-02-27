using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class AffixReader
{
    public static readonly Encoding DefaultEncoding = EncodingEx.GetEncodingByName("ISO8859-1") ?? Encoding.UTF8;

    private static readonly Regex AffixLineRegex = new Regex(
        @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].*)?$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly ImmutableArray<string> DefaultBreakTableEntries = ImmutableArray.Create("-", "^-", "-$");
    private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.Create("AEIOUaeiou");
    private static readonly CommandParseMap<AffixConfigOptions> BitFlagCommandMap;
    private static readonly CommandParseMap<AffixReaderCommandKind> CommandMap;

    static AffixReader()
    {
        BitFlagCommandMap = new(new KeyValuePair<string, AffixConfigOptions>[]
        {
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
        });

        CommandMap = new(new KeyValuePair<string, AffixReaderCommandKind>[]
        {
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
        });
    }

    public AffixReader(AffixConfig.Builder builder, IHunspellLineReader reader)
    {
        Builder = builder ?? new AffixConfig.Builder();
        Reader = reader;
    }

    private AffixConfig.Builder Builder { get; }

    private IHunspellLineReader Reader { get; }

    private EntryListType Initialized { get; set; } = EntryListType.None;

    public static async Task<AffixConfig> ReadAsync(IHunspellLineReader reader, AffixConfig.Builder? builder = null)
    {
        if (reader is null) throw new ArgumentNullException(nameof(reader));

        var readerInstance = new AffixReader(builder ?? new(), reader);

        await readerInstance.ReadAsync().ConfigureAwait(false);

        return readerInstance.Builder.MoveToImmutable();
    }

    private async Task ReadToEndAsync()
    {
        string? line;
        while ((line = await Reader.ReadLineAsync().ConfigureAwait(false)) != null)
        {
            ParseLine(line);
        }
    }

    private async Task ReadAsync()
    {
        await ReadToEndAsync().ConfigureAwait(false);
        AddDefaultBreakTableIfEmpty();
    }

    public static AffixConfig Read(IHunspellLineReader reader, AffixConfig.Builder? builder = null)
    {
        if (reader is null) throw new ArgumentNullException(nameof(reader));

        var readerInstance = new AffixReader(builder ?? new(), reader);

        readerInstance.Read();

        return readerInstance.Builder.MoveToImmutable();
    }

    private void ReadToEnd()
    {
        string? line;
        while ((line = Reader.ReadLine()) != null)
        {
            ParseLine(line);
        }
    }

    private void Read()
    {
        ReadToEnd();
        AddDefaultBreakTableIfEmpty();
    }

    public static async Task<AffixConfig> ReadAsync(Stream stream, AffixConfig.Builder? builder = null)
    {
        if (stream is null) throw new ArgumentNullException(nameof(stream));

        using var reader = new DynamicEncodingLineReader(stream, DefaultEncoding);
        return await ReadAsync(reader, builder).ConfigureAwait(false);
    }
    public static async Task<AffixConfig> ReadFileAsync(string filePath, AffixConfig.Builder? builder = null)
    {
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenAsyncReadFileStream(filePath);
        return await ReadAsync(stream, builder).ConfigureAwait(false);
    }

    public static AffixConfig Read(Stream stream, AffixConfig.Builder? builder = null)
    {
        if (stream is null) throw new ArgumentNullException(nameof(stream));

        using var reader = new DynamicEncodingLineReader(stream, DefaultEncoding);
        return Read(reader, builder);
    }

    public static AffixConfig ReadFile(string filePath, AffixConfig.Builder? builder = null)
    {
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenReadFileStream(filePath);
        return Read(stream, builder);
    }

    private bool ParseLine(string line)
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

        var command = line.AsSpan(commandStartIndex, commandEndIndex - commandStartIndex);

        if (parameterStartIndex <= lineEndIndex)
        {
            if (TryHandleParameterizedCommand(
                command,
                line.AsSpan(parameterStartIndex, lineEndIndex - parameterStartIndex + 1)))
            {
                return true;
            }
        }
        else if (BitFlagCommandMap.TryParse(command) is { } option)
        {
            Builder.EnableOptions(option);
            return true;
        }

        LogWarning("Failed to parse line: " + line);
        return false;

        static bool isCommentPrefix(char c) => c is '#' or '/';
    }

    private bool IsInitialized(EntryListType flags) => (Initialized & flags) == flags;

    private void SetInitialized(EntryListType flags)
    {
        Initialized |= flags;
    }

    private void AddDefaultBreakTableIfEmpty()
    {
        if (!IsInitialized(EntryListType.Break))
        {
            if (Builder.BreakPoints.Count == 0)
            {
                Builder.BreakPoints.AddRange(DefaultBreakTableEntries);
            }
        }
    }

    private bool TryHandleParameterizedCommand(ReadOnlySpan<char> commandName, ReadOnlySpan<char> parameters)
    {
#if DEBUG
        if (parameters.IsEmpty) throw new ArgumentException(nameof(parameters));
#endif

        if (CommandMap.TryParse(commandName) is not { } command)
        {
            LogWarning($"Unknown command {commandName.ToString()} with params: {parameters.ToString()}");
            return false;
        }

        switch (command)
        {
            case AffixReaderCommandKind.Flag:
                return TrySetFlagMode(parameters);
            case AffixReaderCommandKind.KeyString:
                Builder.KeyString = Builder.Dedup(parameters);
                return true;
            case AffixReaderCommandKind.TryString:
                Builder.TryString = Builder.Dedup(parameters);
                return true;
            case AffixReaderCommandKind.SetEncoding:
                if (EncodingEx.GetEncodingByName(parameters) is not { } encoding)
                {
                    LogWarning("Failed to get encoding: " + parameters.ToString());
                    return false;
                }

                Builder.Encoding = encoding;
                return true;
            case AffixReaderCommandKind.Language:
                Builder.Language = Builder.Dedup(parameters);
                Builder.Culture = GetCultureFromLanguage(Builder.Language);
                return true;
            case AffixReaderCommandKind.CompoundSyllableNum:
                Builder.CompoundSyllableNum = Builder.Dedup(parameters);
                return true;
            case AffixReaderCommandKind.WordChars:
                Builder.WordChars = CharacterSet.Create(parameters);
                return true;
            case AffixReaderCommandKind.Ignore:
                Builder.IgnoredChars = CharacterSet.Create(parameters);
                return true;
            case AffixReaderCommandKind.CompoundFlag:
                return TryParseFlag(parameters, out Builder.CompoundFlag);
            case AffixReaderCommandKind.CompoundMiddle:
                return TryParseFlag(parameters, out Builder.CompoundMiddle);
            case AffixReaderCommandKind.CompoundBegin:
                return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                    ? TryParseFlag(parameters, out Builder.CompoundEnd)
                    : TryParseFlag(parameters, out Builder.CompoundBegin);
            case AffixReaderCommandKind.CompoundEnd:
                return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                    ? TryParseFlag(parameters, out Builder.CompoundBegin)
                    : TryParseFlag(parameters, out Builder.CompoundEnd);
            case AffixReaderCommandKind.CompoundWordMax:
                Builder.CompoundWordMax = IntEx.TryParseInvariant(parameters);
                return Builder.CompoundWordMax.HasValue;
            case AffixReaderCommandKind.CompoundMin:
                Builder.CompoundMin = IntEx.TryParseInvariant(parameters);

                switch (Builder.CompoundMin)
                {
                    case null:
                        LogWarning("Failed to parse CompoundMin: " + parameters.ToString());
                        return false;
                    case < 1:
                        Builder.CompoundMin = 1;
                        break;
                }

                return true;
            case AffixReaderCommandKind.CompoundRoot:
                return TryParseFlag(parameters, out Builder.CompoundRoot);
            case AffixReaderCommandKind.CompoundPermitFlag:
                return TryParseFlag(parameters, out Builder.CompoundPermitFlag);
            case AffixReaderCommandKind.CompoundForbidFlag:
                return TryParseFlag(parameters, out Builder.CompoundForbidFlag);
            case AffixReaderCommandKind.CompoundSyllable:
                return TryParseCompoundSyllable(parameters);
            case AffixReaderCommandKind.NoSuggest:
                return TryParseFlag(parameters, out Builder.NoSuggest);
            case AffixReaderCommandKind.NoNGramSuggest:
                return TryParseFlag(parameters, out Builder.NoNgramSuggest);
            case AffixReaderCommandKind.ForbiddenWord:
                Builder.ForbiddenWord = ParseFlagOrDefault(parameters);
                return Builder.ForbiddenWord.HasValue;
            case AffixReaderCommandKind.LemmaPresent:
                return TryParseFlag(parameters, out Builder.LemmaPresent);
            case AffixReaderCommandKind.Circumfix:
                return TryParseFlag(parameters, out Builder.Circumfix);
            case AffixReaderCommandKind.OnlyInCompound:
                return TryParseFlag(parameters, out Builder.OnlyInCompound);
            case AffixReaderCommandKind.NeedAffix:
                return TryParseFlag(parameters, out Builder.NeedAffix);
            case AffixReaderCommandKind.Replacement:
                return TryParseStandardListItem(EntryListType.Replacements, parameters, Builder.Replacements, TryParseReplacements);
            case AffixReaderCommandKind.InputConversions:
                return TryParseConv(parameters, EntryListType.Iconv, ref Builder.InputConversions);
            case AffixReaderCommandKind.OutputConversions:
                return TryParseConv(parameters, EntryListType.Oconv, ref Builder.OutputConversions);
            case AffixReaderCommandKind.Phone:
                return TryParseStandardListItem(EntryListType.Phone, parameters, Builder.Phone, TryParsePhone);
            case AffixReaderCommandKind.CheckCompoundPattern:
                return TryParseStandardListItem(EntryListType.CompoundPatterns, parameters, Builder.CompoundPatterns, TryParseCheckCompoundPatternIntoCompoundPatterns);
            case AffixReaderCommandKind.CompoundRule:
                return TryParseStandardListItem(EntryListType.CompoundRules, parameters, Builder.CompoundRules, TryParseCompoundRuleIntoList);
            case AffixReaderCommandKind.Map:
                return TryParseStandardListItem(EntryListType.Map, parameters, Builder.RelatedCharacterMap, TryParseMapEntry);
            case AffixReaderCommandKind.Break:
                return TryParseStandardListItem(EntryListType.Break, parameters, Builder.BreakPoints, TryParseBreak);
            case AffixReaderCommandKind.Version:
                Builder.Version = parameters.ToString();
                return true;
            case AffixReaderCommandKind.MaxNgramSuggestions:
                Builder.MaxNgramSuggestions = IntEx.TryParseInvariant(parameters);
                return Builder.MaxNgramSuggestions.HasValue;
            case AffixReaderCommandKind.MaxDifferency:
                Builder.MaxDifferency = IntEx.TryParseInvariant(parameters);
                return Builder.MaxDifferency.HasValue;
            case AffixReaderCommandKind.MaxCompoundSuggestions:
                Builder.MaxCompoundSuggestions = IntEx.TryParseInvariant(parameters);
                return Builder.MaxCompoundSuggestions.HasValue;
            case AffixReaderCommandKind.KeepCase:
                return TryParseFlag(parameters, out Builder.KeepCase);
            case AffixReaderCommandKind.ForceUpperCase:
                return TryParseFlag(parameters, out Builder.ForceUpperCase);
            case AffixReaderCommandKind.Warn:
                return TryParseFlag(parameters, out Builder.Warn);
            case AffixReaderCommandKind.SubStandard:
                return TryParseFlag(parameters, out Builder.SubStandard);
            case AffixReaderCommandKind.Prefix:
            case AffixReaderCommandKind.Suffix:
                var parseAsPrefix = AffixReaderCommandKind.Prefix == command;

                if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                {
                    parseAsPrefix = !parseAsPrefix;
                }

                return parseAsPrefix
                    ? TryParseAffixIntoList(parameters, ref Builder.Prefixes)
                    : TryParseAffixIntoList(parameters, ref Builder.Suffixes);
            case AffixReaderCommandKind.AliasF:
                return TryParseStandardListItem(EntryListType.AliasF, parameters, Builder.AliasF, TryParseAliasF);
            case AffixReaderCommandKind.AliasM:
                return TryParseStandardListItem(EntryListType.AliasM, parameters, Builder.AliasM, TryParseAliasM);
            default:
                LogWarning($"Unknown parsed command {command}");
                return false;
        }
    }

    private delegate bool EntryParserForArray<T>(ReadOnlySpan<char> parameterText, ImmutableArray<T>.Builder entries);
    private bool TryParseStandardListItem<T>(EntryListType entryListType, ReadOnlySpan<char> parameterText, ImmutableArray<T>.Builder entries, EntryParserForArray<T> parse)
    {
        if (!IsInitialized(entryListType))
        {
            SetInitialized(entryListType);

            if (IntEx.TryParseInvariant(parameterText, out var expectedSize) && expectedSize >= 0)
            {
                entries.Capacity = expectedSize;
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
                    Builder.CompoundMaxSyllable = maxValue;
                    Builder.CompoundVowels = DefaultCompoundVowels;
                    state = 1;
                    continue;
                }
            }
            else if (state == 1)
            {
                Builder.CompoundVowels = CharacterSet.Create(part);
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

    private static CultureInfo GetCultureFromLanguage(string language)
    {
        if (string.IsNullOrEmpty(language))
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

    private bool TryParsePhone(ReadOnlySpan<char> parameterText, ImmutableArray<PhoneticEntry>.Builder entries)
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

        entries.Add(new PhoneticEntry(Builder.Dedup(rule), Builder.Dedup(replace.Without('_'))));

        return true;
    }

    private bool TryParseMapEntry(ReadOnlySpan<char> parameterText, ImmutableArray<MapEntry>.Builder entries)
    {
        var valuesBuilder = ImmutableArray.CreateBuilder<string>(parameterText.Length / 2);

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

        entries.Add(new MapEntry(Builder.DedupIntoImmutableArray(valuesBuilder, destructive: true)));

        return true;
    }

    private bool TryParseConv(ReadOnlySpan<char> parameterText, EntryListType entryListType, ref Dictionary<string, MultiReplacementEntry>? entries)
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

        entries ??= new();

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
                throw new InvalidOperationException();
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

        var pattern1String = Builder.Dedup(pattern1.ReplaceIntoString('_', ' '));

        // find existing entry
        if (!entries.TryGetValue(pattern1String, out var entry))
        {
            // make a new entry if none exists
            entry = new MultiReplacementEntry(pattern1String);
            entries[pattern1String] = entry;
        }

        entry.Set(type, Builder.Dedup(pattern2.ReplaceIntoString('_', ' ')));
        return true;
    }

    private bool TryParseBreak(ReadOnlySpan<char> parameterText, ImmutableArray<string>.Builder entries)
    {
        entries.Add(Builder.Dedup(parameterText));
        return true;
    }

    private bool TryParseAliasF(ReadOnlySpan<char> parameterText, ImmutableArray<FlagSet>.Builder entries)
    {
        entries.Add(Builder.Dedup(ParseFlagSet(parameterText)));
        return true;
    }

    private bool TryParseAliasM(ReadOnlySpan<char> parameterText, ImmutableArray<MorphSet>.Builder entries)
    {
        if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
        {
            parameterText = parameterText.Reversed();
        }

        var parts = ImmutableArray.CreateBuilder<string>();
        foreach (var part in parameterText.SplitOnTabOrSpace())
        {
            if (!part.IsEmpty)
            {
                parts.Add(Builder.Dedup(part));
            }
        }

        entries.Add(Builder.Dedup(new MorphSet(Builder.DedupIntoImmutableArray(parts, true))));

        return true;
    }

    private bool TryParseCompoundRuleIntoList(ReadOnlySpan<char> parameterText, ImmutableArray<CompoundRule>.Builder entries)
    {
        var entryBuilder = ImmutableArray.CreateBuilder<FlagValue>();

        if (parameterText.Contains('('))
        {
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
                    entryBuilder.AddRange(ParseFlagsInOrder(parameterText.Slice(indexBegin, indexEnd - indexBegin)));
                }
            }
        }
        else
        {
            entryBuilder.AddRange(ParseFlagsInOrder(parameterText));
        }

        entries.Add(new(entryBuilder.ToImmutable(allowDestructive: true)));
        return true;
    }

    private bool TryParseAffixIntoList<TEntry>(ReadOnlySpan<char> parameterText, ref List<AffixEntryGroup<TEntry>.Builder>? groups)
        where TEntry : AffixEntry
    {
        groups ??= new();
        return TryParseAffixIntoList(parameterText.ToString(), ref groups);
    }

    private bool TryParseAffixIntoList<TEntry>(string parameterText, ref List<AffixEntryGroup<TEntry>.Builder> groups)
        where TEntry : AffixEntry
    {
        var lineMatch = AffixLineRegex.Match(parameterText);
        if (!lineMatch.Success)
        {
            LogWarning("Failed to parse affix line: " + parameterText);
            return false;
        }

        var lineMatchGroups = lineMatch.Groups;

        if (!TryParseFlag(lineMatchGroups[1].Value.AsSpan(), out var characterFlag))
        {
            LogWarning($"Failed to parse affix flag for {lineMatchGroups[1].Value} from: {parameterText}");
            return false;
        }

        var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
        var contClass = FlagSet.Empty;

        if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
        {
            if (affixGroup is not null)
            {
                LogWarning($"Duplicate affix group definition for {affixGroup.AFlag} from: {parameterText}");
                return false;
            }

            var options = AffixEntryOptions.None;
            if (lineMatchGroups[2].Value.StartsWith('Y'))
            {
                options |= AffixEntryOptions.CrossProduct;
            }
            if (Builder.AliasM is { Count: > 0 })
            {
                options |= AffixEntryOptions.AliasM;
            }
            if (Builder.AliasF is { Count: > 0 })
            {
                options |= AffixEntryOptions.AliasF;
            }

            IntEx.TryParseInvariant(lineMatchGroups[3].Value, out var expectedEntryCount);

            affixGroup = new AffixEntryGroup<TEntry>.Builder(
                characterFlag,
                options,
                expectedEntryCount is > 2 and <= 1000
                    ? ImmutableArray.CreateBuilder<TEntry>(expectedEntryCount)
                    : ImmutableArray.CreateBuilder<TEntry>());

            groups.Add(affixGroup);

            return true;
        }

        if (lineMatchGroups[4].Success && lineMatchGroups[5].Success && lineMatchGroups[6].Success)
        {
            // piece 3 - is string to strip or 0 for null
            var strip = lineMatchGroups[4].Value;
            if (strip == "0")
            {
                strip = string.Empty;
            }
            else if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                strip = strip.GetReversed();
            }

            // piece 4 - is affix string or 0 for null
            var affixInput = lineMatchGroups[5].Value;
            StringBuilder affixText;
            if (affixInput.IndexOf('/') is int affixSlashIndex and >= 0)
            {
                affixText = StringBuilderPool.Get(affixInput.AsSpan(0, affixSlashIndex));

                if (Builder.AliasF is { Count: > 0 } aliasF)
                {
                    if (IntEx.TryParseInvariant(affixInput.AsSpan(affixSlashIndex + 1), out var aliasNumber) && aliasNumber > 0 && aliasNumber <= aliasF.Count)
                    {
                        contClass = aliasF[aliasNumber - 1];
                    }
                    else
                    {
                        LogWarning($"Failed to parse contclasses from : {parameterText}");
                        return false;
                    }
                }
                else
                {
                    contClass = Builder.Dedup(ParseFlagSet(affixInput.AsSpan(affixSlashIndex + 1)));
                }
            }
            else
            {
                affixText = StringBuilderPool.Get(affixInput);
            }

            if (Builder.IgnoredChars is { HasItems: true })
            {
                affixText.RemoveChars(Builder.IgnoredChars);
            }

            if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                affixText.Reverse();
            }

            if (affixText.Length == 1 && affixText[0] == '0')
            {
                affixText.Clear();
            }

            // piece 5 - is the conditions descriptions
            var conditionText = lineMatchGroups[6].Value;
            if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                conditionText = ReverseCondition(conditionText);
            }

            var conditions = CharacterCondition.Parse(conditionText);
            if (strip.Length != 0 && !conditions.AllowsAnySingleCharacter)
            {
                bool isRedundant;
                if (typeof(TEntry) == typeof(PrefixEntry))
                {
                    isRedundant = conditions.IsOnlyPossibleMatch(strip);
                }
                else if (typeof(TEntry) == typeof(SuffixEntry))
                {
                    isRedundant = conditions.IsOnlyPossibleMatch(strip);
                }
                else
                {
                    throw new NotSupportedException();
                }

                if (isRedundant)
                {
                    conditions = CharacterConditionGroup.AllowAnySingleCharacter;
                }
            }

            // piece 6
            MorphSet morph;
            if (lineMatchGroups[7].Success)
            {
                var morphAffixText = lineMatchGroups[7].Value;
                if (Builder.AliasM is { Count: > 0 } aliasM)
                {
                    if (IntEx.TryParseInvariant(morphAffixText, out var morphNumber) && morphNumber > 0 && morphNumber <= aliasM.Count)
                    {
                        morph = aliasM[morphNumber - 1];
                    }
                    else
                    {
                        LogWarning($"Failed to parse morph {morphAffixText} from: {parameterText}");
                        return false;
                    }
                }
                else
                {
                    if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                    {
                        morphAffixText = morphAffixText.GetReversed();
                    }

                    morph = Builder.Dedup(new MorphSet(Builder.DedupIntoImmutableArray(morphAffixText.SplitOnTabOrSpace(), true)));
                }
            }
            else
            {
                morph = MorphSet.Empty;
            }

            affixGroup ??= new AffixEntryGroup<TEntry>.Builder(characterFlag, AffixEntryOptions.None);

            if (!Builder.HasContClass && contClass.HasItems)
            {
                Builder.HasContClass = true;
            }

            affixGroup.Entries.Add(CreateEntry<TEntry>(
                Builder.Dedup(strip),
                Builder.Dedup(StringBuilderPool.GetStringAndReturn(affixText)),
                Builder.Dedup(conditions),
                morph,
                contClass));

            return true;
        }

        LogWarning("Affix line not fully parsed: " + parameterText);
        return false;
    }

    private static TEntry CreateEntry<TEntry>(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass)
        where TEntry : AffixEntry
    {
        if (typeof(TEntry) == typeof(PrefixEntry))
        {
            return (TEntry)((AffixEntry)new PrefixEntry(strip, affixText, conditions, morph, contClass));
        }
        if (typeof(TEntry) == typeof(SuffixEntry))
        {
            return (TEntry)((AffixEntry)new SuffixEntry(strip, affixText, conditions, morph, contClass));
        }

        throw new NotSupportedException();
    }

    private static string ReverseCondition(string conditionText)
    {
        // TODO: Would it be better to reverse the conditions after parsing?
        //       Instead of reversing a string it could reverse a CharacterConditionGroup.

        if (string.IsNullOrEmpty(conditionText))
        {
            return conditionText;
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
                        else
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

    private bool TryParseReplacements(ReadOnlySpan<char> parameterText, ImmutableArray<SingleReplacement>.Builder entries)
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
            Builder.Dedup(pattern.ReplaceIntoString('_', ' ')),
            Builder.Dedup(outString.ReplaceIntoString('_', ' ')),
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

    private bool TryParseCheckCompoundPatternIntoCompoundPatterns(ReadOnlySpan<char> parameterText, ImmutableArray<PatternEntry>.Builder entries)
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
                    condition1 = ParseFlagOrDefault(part.Slice(slashIndex + 1));
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
                    condition2 = ParseFlagOrDefault(part.Slice(slashIndex + 1));
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
                Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);

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
            Builder.Dedup(pattern1),
            Builder.Dedup(pattern2),
            Builder.Dedup(pattern3),
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

        if (Builder.FlagMode == mode)
        {
            LogWarning($"Redundant FlagMode: {modeText.ToString()}");
            return false;
        }

        Builder.FlagMode = mode;
        return true;
    }

    private void LogWarning(string text)
    {
        Builder.LogWarning(text);
    }

    private ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded) =>
        HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(decoded, Builder.Encoding ?? Reader.CurrentEncoding);

    private FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text) => Builder.FlagMode switch
    {
        FlagMode.Char => FlagValue.ParseAsChars(text),
        FlagMode.Uni => FlagValue.ParseAsChars(ReDecodeConvertedStringAsUtf8(text)),
        FlagMode.Long => FlagValue.ParseAsLongs(text),
        FlagMode.Num => FlagValue.ParseAsNumbers(text),
        _ => throw new NotSupportedException()
    };

    private FlagSet ParseFlagSet(ReadOnlySpan<char> text) => Builder.FlagMode switch
    {
        FlagMode.Char => FlagSet.ParseAsChars(text),
        FlagMode.Uni => FlagSet.ParseAsChars(ReDecodeConvertedStringAsUtf8(text)),
        FlagMode.Long => FlagSet.ParseAsLongs(text),
        FlagMode.Num => FlagSet.ParseAsNumbers(text),
        _ => throw new NotSupportedException()
    };

    private bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value) => Builder.FlagMode switch
    {
        FlagMode.Char => FlagValue.TryParseAsChar(text, out value),
        FlagMode.Uni => FlagValue.TryParseAsChar(ReDecodeConvertedStringAsUtf8(text), out value),
        FlagMode.Long => FlagValue.TryParseAsLong(text, out value),
        FlagMode.Num => FlagValue.TryParseAsNumber(text, out value),
        _ => throw new NotSupportedException()
    };

    private FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        TryParseFlag(text, out var result);
        return result;
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

    private static FlagMode? TryParseFlagMode(ReadOnlySpan<char> value)
    {
        if (value.Length >= 2)
        {
            if (value.StartsWith("CHAR", StringComparison.OrdinalIgnoreCase))
            {
                return FlagMode.Char;
            }
            if (value.StartsWith("LONG", StringComparison.OrdinalIgnoreCase))
            {
                return FlagMode.Long;
            }
            if (value.StartsWith("NUM", StringComparison.OrdinalIgnoreCase))
            {
                return FlagMode.Num;
            }
            if (value.StartsWith("UTF", StringComparison.OrdinalIgnoreCase) || value.StartsWith("UNI", StringComparison.OrdinalIgnoreCase))
            {
                return FlagMode.Uni;
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
