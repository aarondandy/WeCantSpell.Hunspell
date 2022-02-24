using System;
using System.Collections.Generic;
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

    private static readonly string[] DefaultBreakTableEntries = { "-", "^-", "-$" };

    private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.TakeArray(new[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' });

    private static readonly CommandParseMap<AffixConfigOptions> _bitFlagCommandMap;
    private static readonly CommandParseMap<AffixReaderCommandKind> _commandMap;

    static AffixReader()
    {
        _bitFlagCommandMap = new();
        _bitFlagCommandMap.Add("CHECKCOMPOUNDDUP", AffixConfigOptions.CheckCompoundDup);
        _bitFlagCommandMap.Add("CHECKCOMPOUNDREP", AffixConfigOptions.CheckCompoundRep);
        _bitFlagCommandMap.Add("CHECKCOMPOUNDTRIPLE", AffixConfigOptions.CheckCompoundTriple);
        _bitFlagCommandMap.Add("CHECKCOMPOUNDCASE", AffixConfigOptions.CheckCompoundCase);
        _bitFlagCommandMap.Add("CHECKNUM", AffixConfigOptions.CheckNum);
        _bitFlagCommandMap.Add("CHECKSHARPS", AffixConfigOptions.CheckSharps);
        _bitFlagCommandMap.Add("COMPLEXPREFIXES", AffixConfigOptions.ComplexPrefixes);
        _bitFlagCommandMap.Add("COMPOUNDMORESUFFIXES", AffixConfigOptions.CompoundMoreSuffixes);
        _bitFlagCommandMap.Add("FULLSTRIP", AffixConfigOptions.FullStrip);
        _bitFlagCommandMap.Add("FORBIDWARN", AffixConfigOptions.ForbidWarn);
        _bitFlagCommandMap.Add("NOSPLITSUGS", AffixConfigOptions.NoSplitSuggestions);
        _bitFlagCommandMap.Add("ONLYMAXDIFF", AffixConfigOptions.OnlyMaxDiff);
        _bitFlagCommandMap.Add("SIMPLIFIEDTRIPLE", AffixConfigOptions.SimplifiedTriple);
        _bitFlagCommandMap.Add("SUGSWITHDOTS", AffixConfigOptions.SuggestWithDots);

        _commandMap = new();
        _commandMap.Add("AF", AffixReaderCommandKind.AliasF);
        _commandMap.Add("AM", AffixReaderCommandKind.AliasM);
        _commandMap.Add("BREAK", AffixReaderCommandKind.Break);
        _commandMap.Add("COMPOUNDBEGIN", AffixReaderCommandKind.CompoundBegin);
        _commandMap.Add("COMPOUNDEND", AffixReaderCommandKind.CompoundEnd);
        _commandMap.Add("COMPOUNDFLAG", AffixReaderCommandKind.CompoundFlag);
        _commandMap.Add("COMPOUNDFORBIDFLAG", AffixReaderCommandKind.CompoundForbidFlag);
        _commandMap.Add("COMPOUNDMIDDLE", AffixReaderCommandKind.CompoundMiddle);
        _commandMap.Add("COMPOUNDMIN", AffixReaderCommandKind.CompoundMin);
        _commandMap.Add("COMPOUNDPERMITFLAG", AffixReaderCommandKind.CompoundPermitFlag);
        _commandMap.Add("COMPOUNDROOT", AffixReaderCommandKind.CompoundRoot);
        _commandMap.Add("COMPOUNDRULE", AffixReaderCommandKind.CompoundRule);
        _commandMap.Add("COMPOUNDSYLLABLE", AffixReaderCommandKind.CompoundSyllable);
        _commandMap.Add("COMPOUNDWORDMAX", AffixReaderCommandKind.CompoundWordMax);
        _commandMap.Add("CIRCUMFIX", AffixReaderCommandKind.Circumfix);
        _commandMap.Add("CHECKCOMPOUNDPATTERN", AffixReaderCommandKind.CheckCompoundPattern);
        _commandMap.Add("FLAG", AffixReaderCommandKind.Flag);
        _commandMap.Add("FORBIDDENWORD", AffixReaderCommandKind.ForbiddenWord);
        _commandMap.Add("FORCEUCASE", AffixReaderCommandKind.ForceUpperCase);
        _commandMap.Add("ICONV", AffixReaderCommandKind.InputConversions);
        _commandMap.Add("IGNORE", AffixReaderCommandKind.Ignore);
        _commandMap.Add("KEY", AffixReaderCommandKind.KeyString);
        _commandMap.Add("KEEPCASE", AffixReaderCommandKind.KeepCase);
        _commandMap.Add("LANG", AffixReaderCommandKind.Language);
        _commandMap.Add("LEMMA_PRESENT", AffixReaderCommandKind.LemmaPresent);
        _commandMap.Add("MAP", AffixReaderCommandKind.Map);
        _commandMap.Add("MAXNGRAMSUGS", AffixReaderCommandKind.MaxNgramSuggestions);
        _commandMap.Add("MAXDIFF", AffixReaderCommandKind.MaxDifferency);
        _commandMap.Add("MAXCPDSUGS", AffixReaderCommandKind.MaxCompoundSuggestions);
        _commandMap.Add("NEEDAFFIX", AffixReaderCommandKind.NeedAffix);
        _commandMap.Add("NOSUGGEST", AffixReaderCommandKind.NoSuggest);
        _commandMap.Add("NONGRAMSUGGEST", AffixReaderCommandKind.NoNGramSuggest);
        _commandMap.Add("OCONV", AffixReaderCommandKind.OutputConversions);
        _commandMap.Add("ONLYINCOMPOUND", AffixReaderCommandKind.OnlyInCompound);
        _commandMap.Add("PFX", AffixReaderCommandKind.Prefix);
        _commandMap.Add("PSEUDOROOT", AffixReaderCommandKind.NeedAffix);
        _commandMap.Add("PHONE", AffixReaderCommandKind.Phone);
        _commandMap.Add("REP", AffixReaderCommandKind.Replacement);
        _commandMap.Add("SFX", AffixReaderCommandKind.Suffix);
        _commandMap.Add("SET", AffixReaderCommandKind.SetEncoding);
        _commandMap.Add("SYLLABLENUM", AffixReaderCommandKind.CompoundSyllableNum);
        _commandMap.Add("SUBSTANDARD", AffixReaderCommandKind.SubStandard);
        _commandMap.Add("TRY", AffixReaderCommandKind.TryString);
        _commandMap.Add("VERSION", AffixReaderCommandKind.Version);
        _commandMap.Add("WORDCHARS", AffixReaderCommandKind.WordChars);
        _commandMap.Add("WARN", AffixReaderCommandKind.Warn);
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
        else if (_bitFlagCommandMap.TryParse(command) is { } option)
        {
            Builder.EnableOptions(option);
            return true;
        }

        return LogWarning("Failed to parse line: " + line);

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
            Builder.BreakPoints ??= new List<string>(DefaultBreakTableEntries.Length);

            if (Builder.BreakPoints.Count == 0)
            {
                Builder.BreakPoints.AddRange(DefaultBreakTableEntries.Select(Builder.Dedup));
            }
        }
    }

    private bool TryHandleParameterizedCommand(ReadOnlySpan<char> commandName, ReadOnlySpan<char> parameters)
    {
#if DEBUG
        if (parameters.IsEmpty) throw new ArgumentException(nameof(parameters));
#endif

        if (_commandMap.TryParse(commandName) is not { } command)
        {
            return LogWarning($"Unknown command {commandName.ToString()} with params: {parameters.ToString()}");
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
                    return LogWarning("Failed to get encoding: " + parameters.ToString());
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
                        return LogWarning("Failed to parse CompoundMin: " + parameters.ToString());
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
                return TryParseStandardListItem(EntryListType.Replacements, parameters, ref Builder.Replacements, TryParseReplacements);
            case AffixReaderCommandKind.InputConversions:
                return TryParseConv(parameters, EntryListType.Iconv, ref Builder.InputConversions);
            case AffixReaderCommandKind.OutputConversions:
                return TryParseConv(parameters, EntryListType.Oconv, ref Builder.OutputConversions);
            case AffixReaderCommandKind.Phone:
                return TryParseStandardListItem(EntryListType.Phone, parameters, ref Builder.Phone, TryParsePhone);
            case AffixReaderCommandKind.CheckCompoundPattern:
                return TryParseStandardListItem(EntryListType.CompoundPatterns, parameters, ref Builder.CompoundPatterns, TryParseCheckCompoundPatternIntoCompoundPatterns);
            case AffixReaderCommandKind.CompoundRule:
                return TryParseStandardListItem(EntryListType.CompoundRules, parameters, ref Builder.CompoundRules, TryParseCompoundRuleIntoList);
            case AffixReaderCommandKind.Map:
                return TryParseStandardListItem(EntryListType.Map, parameters, ref Builder.RelatedCharacterMap, TryParseMapEntry);
            case AffixReaderCommandKind.Break:
                return TryParseStandardListItem(EntryListType.Break, parameters, ref Builder.BreakPoints, TryParseBreak);
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
                return TryParseStandardListItem(EntryListType.AliasF, parameters, ref Builder.AliasF, TryParseAliasF);
            case AffixReaderCommandKind.AliasM:
                return TryParseStandardListItem(EntryListType.AliasM, parameters, ref Builder.AliasM, TryParseAliasM);
            default:
                return LogWarning($"Unknown parsed command {command}");
        }
    }

    private bool TryParseStandardListItem<T>(EntryListType entryListType, ReadOnlySpan<char> parameterText, ref List<T>? entries, EntryParser<T> parse)
    {
        if (!IsInitialized(entryListType))
        {
            SetInitialized(entryListType);

            if (IntEx.TryParseInvariant(parameterText, out var expectedSize) && expectedSize >= 0)
            {
                entries ??= new(expectedSize);

                return true;
            }
        }

        entries ??= new();

        return parse(parameterText, entries);
    }

    private bool TryParseCompoundSyllable(ReadOnlySpan<char> parameters)
    {
        var ok = parameters.SplitOnTabOrSpace((part, i) =>
        {
            switch (i)
            {
                case 0:
                    if (IntEx.TryParseInvariant(part) is { } maxValue)
                    {
                        Builder.CompoundMaxSyllable = maxValue;
                        Builder.CompoundVowels = DefaultCompoundVowels;
                        return true;
                    }

                    return false;
                case 1:
                    Builder.CompoundVowels = CharacterSet.Create(part);
                    return true;
                default:
                    return false;
            }
        });

        if (!ok)
        {
            LogWarning("Failed to parse CompoundMaxSyllable value from: " + parameters.ToString());
        }

        return ok;
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

    private bool TryParsePhone(ReadOnlySpan<char> parameterText, List<PhoneticEntry> entries)
    {
        string? rule = null;
        var replace = string.Empty;
        var ok = parameterText.SplitOnTabOrSpace((part, i) =>
        {
            switch (i)
            {
                case 0:
                    rule = Builder.Dedup(part);
                    return true;
                case 1:
                    replace = Builder.Dedup(part.Without('_'));
                    return true;
                default:
                    return false;
            }
        });

        if (rule is null)
        {
            return LogWarning("Failed to parse phone line: " + parameterText.ToString());
        }

        entries.Add(new PhoneticEntry(rule, replace));

        return true;
    }

    private bool TryParseMapEntry(ReadOnlySpan<char> parameterText, List<MapEntry> entries)
    {
        var values = new List<string>(parameterText.Length);

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

            values.Add(parameterText.Slice(chb, che - chb).ToString());
        }

        entries.Add(MapEntry.TakeArray(Builder.DedupIntoArray(values)));

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

        string? pattern1 = null;
        string? pattern2 = null;
        parameterText.SplitOnTabOrSpace((part, i) =>
        {
            switch (i)
            {
                case 0:
                    pattern1 = Builder.Dedup(part);
                    return true;
                case 1:
                    pattern2 = Builder.Dedup(part);
                    return true;
                default:
                    return false;
            }
        });

        if (pattern1 is null || pattern2 is null)
        {
            return LogWarning($"Bad {entryListType}: {parameterText.ToString()}");
        }

        entries.AddReplacementEntry(pattern1, pattern2);

        return true;
    }

    private bool TryParseBreak(ReadOnlySpan<char> parameterText, List<string> entries)
    {
        entries.Add(Builder.Dedup(parameterText));
        return true;
    }

    private bool TryParseAliasF(ReadOnlySpan<char> parameterText, List<FlagSet> entries)
    {
        entries.Add(Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(parameterText))));
        return true;
    }

    private bool TryParseAliasM(ReadOnlySpan<char> parameterText, List<MorphSet> entries)
    {
        if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
        {
            parameterText = parameterText.Reversed();
        }

        var parts = new List<string>();
        parameterText.SplitOnTabOrSpace((part, _) =>
        {
            if (!part.IsEmpty)
            {
                parts.Add(part.ToString());
            }

            return true;
        });

        entries.Add(Builder.Dedup(MorphSet.TakeArray(Builder.DedupIntoArray(parts))));

        return true;
    }

    private bool TryParseCompoundRuleIntoList(ReadOnlySpan<char> parameterText, List<CompoundRule> entries)
    {
        var entryBuilder = new List<FlagValue>();

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

        entries.Add(CompoundRule.Create(entryBuilder));
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
            return LogWarning("Failed to parse affix line: " + parameterText);
        }

        var lineMatchGroups = lineMatch.Groups;

        if (!TryParseFlag(lineMatchGroups[1].Value.AsSpan(), out var characterFlag))
        {
            return LogWarning($"Failed to parse affix flag for {lineMatchGroups[1].Value} from: {parameterText}");
        }

        var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
        var contClass = FlagSet.Empty;

        if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
        {
            if (affixGroup is not null)
            {
                return LogWarning($"Duplicate affix group definition for {affixGroup.AFlag} from: {parameterText}");
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
                    ? new(expectedEntryCount)
                    : new());

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

                if (Builder.AliasF is { } aliasF)
                {
                    if (IntEx.TryParseInvariant(affixInput.AsSpan(affixSlashIndex + 1), out var aliasNumber) && aliasNumber > 0 && aliasNumber <= aliasF.Count)
                    {
                        contClass = aliasF[aliasNumber - 1];
                    }
                    else
                    {
                        return LogWarning($"Failed to parse contclasses from : {parameterText}");
                    }
                }
                else
                {
                    contClass = Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(affixInput.AsSpan(affixSlashIndex + 1))));
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
                if (Builder.AliasM is { } aliasM)
                {
                    if (IntEx.TryParseInvariant(morphAffixText, out var morphNumber) && morphNumber > 0 && morphNumber <= aliasM.Count)
                    {
                        morph = aliasM[morphNumber - 1];
                    }
                    else
                    {
                        return LogWarning($"Failed to parse morph {morphAffixText} from: {parameterText}");
                    }
                }
                else
                {
                    if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                    {
                        morphAffixText = morphAffixText.GetReversed();
                    }

                    morph = Builder.Dedup(MorphSet.TakeArray(Builder.DedupInPlace(morphAffixText.SplitOnTabOrSpace())));
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

        return LogWarning("Affix line not fully parsed: " + parameterText);
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

    private bool TryParseReplacements(ReadOnlySpan<char> parameterText, List<SingleReplacement> entries)
    {
        string? pattern = null;
        var outString = string.Empty;
        var type = ReplacementValueType.Med;

        parameterText.SplitOnTabOrSpace((part, i) =>
        {
            switch (i)
            {
                case 0:
                    type = ReplacementValueType.Med;

                    if (part.EndsWith('$'))
                    {
                        type |= ReplacementValueType.Fin;
                        part = part.Slice(0, part.Length - 1);
                    }

                    if (part.StartsWith('^'))
                    {
                        type |= ReplacementValueType.Ini;
                        part = part.Slice(1);
                    }

                    var patternBuilder = StringBuilderPool.Get(part);
                    patternBuilder.Replace('_', ' ');
                    pattern = StringBuilderPool.GetStringAndReturn(patternBuilder);

                    return true;
                case 1:
                    outString = Builder.Dedup(part.Replace('_', ' '));
                    return true;
                default:
                    return false;
            }
        });

        if (pattern is null)
        {
            return LogWarning("Failed to parse replacements from: " + parameterText.ToString());
        }

        entries.Add(new SingleReplacement(pattern, outString, type));

        return true;
    }

    private bool TryParseCheckCompoundPatternIntoCompoundPatterns(ReadOnlySpan<char> parameterText, List<PatternEntry> entries)
    {
        string? pattern = null;
        string pattern2 = string.Empty;
        string pattern3 = string.Empty;
        FlagValue condition = default;
        FlagValue condition2 = default;
        bool failedParsePattern1 = false;
        bool failedParsePattern2 = false;

        parameterText.SplitOnTabOrSpace((part, i) =>
        {
            int slashIndex;
            switch (i)
            {
                case 0:
                    slashIndex = part.IndexOf('/');
                    if (slashIndex >= 0)
                    {
                        condition = ParseFlagOrDefault(part.Slice(slashIndex + 1));
                        if (!condition.HasValue)
                        {
                            failedParsePattern1 = true;
                            return false;
                        }

                        part = part.Slice(0, slashIndex);
                    }

                    pattern = part.ToString();
                    return true;
                case 1:
                    slashIndex = part.IndexOf('/');
                    if (slashIndex >= 0)
                    {
                        condition2 = ParseFlagOrDefault(part.Slice(slashIndex + 1));
                        if (!condition2.HasValue)
                        {
                            failedParsePattern2 = true;
                            return false;
                        }

                        part = part.Slice(0, slashIndex);
                    }

                    pattern2 = part.ToString();
                    return true;
                case 2:
                    pattern3 = part.ToString();
                    Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);
                    return true;
                default:
                    return false;
            }
        });

        if (pattern is null)
        {
            return LogWarning("Failed to parse compound pattern from: " + parameterText.ToString());
        }

        if (failedParsePattern1)
        {
            return LogWarning($"Failed to parse compound pattern 1 {pattern} from: {parameterText.ToString()}");
        }

        if (failedParsePattern2)
        {
            return LogWarning($"Failed to parse compound pattern 2 {pattern2} from: {parameterText.ToString()}");
        }

        entries.Add(new PatternEntry(
            Builder.Dedup(pattern),
            Builder.Dedup(pattern2),
            Builder.Dedup(pattern3),
            condition,
            condition2));

        return true;
    }

    private bool TrySetFlagMode(ReadOnlySpan<char> modeText)
    {
        if (modeText.IsEmpty)
        {
            return LogWarning("Attempt to set empty flag mode.");
        }

        if (TryParseFlagMode(modeText) is not { } mode)
        {
            return LogWarning($"Unknown FlagMode: {modeText.ToString()}");
        }

        if (mode == Builder.FlagMode)
        {
            return LogWarning($"Redundant FlagMode: {modeText.ToString()}");
        }

        Builder.FlagMode = mode;
        return true;
    }

    private bool LogWarning(string text)
    {
        Builder.LogWarning(text);
        return false;
    }

    private ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded) =>
        HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(decoded, Builder.Encoding ?? Reader.CurrentEncoding);

    private FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text)
    {
        var flagMode = Builder.FlagMode;
        return flagMode == FlagMode.Uni
            ? FlagValue.ParseFlagsInOrder(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char)
            : FlagValue.ParseFlagsInOrder(text, flagMode);
    }

    private bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value)
    {
        var flagMode = Builder.FlagMode;
        return flagMode == FlagMode.Uni
            ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out value)
            : FlagValue.TryParseFlag(text, flagMode, out value);
    }

    private FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        FlagValue result;
        if (Builder.FlagMode == FlagMode.Uni)
        {
            FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out result);
        }
        else
        {
            FlagValue.TryParseFlag(text, Builder.FlagMode, out result);
        }

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

    private delegate bool EntryParser<T>(ReadOnlySpan<char> parameterText, List<T> entries);

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
