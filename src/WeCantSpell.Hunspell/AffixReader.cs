using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class AffixReader
    {
        public AffixReader(AffixConfig.Builder builder, IHunspellLineReader reader)
        {
            Builder = builder ?? new AffixConfig.Builder();
            Reader = reader;
        }

        private static readonly Regex AffixLineRegex = new Regex(
            @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].*)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Dictionary<string, AffixConfigOptions> FileBitFlagCommandMappings = new Dictionary<string, AffixConfigOptions>(StringComparer.OrdinalIgnoreCase)
        {
            {"COMPLEXPREFIXES", AffixConfigOptions.ComplexPrefixes},
            {"COMPOUNDMORESUFFIXES", AffixConfigOptions.CompoundMoreSuffixes},
            {"CHECKCOMPOUNDDUP", AffixConfigOptions.CheckCompoundDup},
            {"CHECKCOMPOUNDREP", AffixConfigOptions.CheckCompoundRep},
            {"CHECKCOMPOUNDTRIPLE", AffixConfigOptions.CheckCompoundTriple},
            {"SIMPLIFIEDTRIPLE", AffixConfigOptions.SimplifiedTriple},
            {"CHECKCOMPOUNDCASE", AffixConfigOptions.CheckCompoundCase},
            {"CHECKNUM", AffixConfigOptions.CheckNum},
            {"ONLYMAXDIFF", AffixConfigOptions.OnlyMaxDiff},
            {"NOSPLITSUGS", AffixConfigOptions.NoSplitSuggestions},
            {"FULLSTRIP", AffixConfigOptions.FullStrip},
            {"SUGSWITHDOTS", AffixConfigOptions.SuggestWithDots},
            {"FORBIDWARN", AffixConfigOptions.ForbidWarn},
            {"CHECKSHARPS", AffixConfigOptions.CheckSharps}
        };

        private static readonly Dictionary<string, FlagMode> FlagModeParameterMappings = new Dictionary<string, FlagMode>(StringComparer.OrdinalIgnoreCase)
        {
            {"LONG", FlagMode.Long},
            {"CHAR", FlagMode.Char},
            {"NUM", FlagMode.Num},
            {"NUMBER", FlagMode.Num},
            {"UTF", FlagMode.Uni},
            {"UNI", FlagMode.Uni},
            {"UTF-8", FlagMode.Uni}
        };

        private static readonly Dictionary<string, AffixReaderCommandKind> CommandMap =
            new Dictionary<string, AffixReaderCommandKind>(StringComparer.OrdinalIgnoreCase)
            {
                { "FLAG", AffixReaderCommandKind.Flag },
                { "KEY", AffixReaderCommandKind.KeyString },
                { "TRY", AffixReaderCommandKind.TryString },
                { "SET", AffixReaderCommandKind.SetEncoding },
                { "LANG", AffixReaderCommandKind.Language },
                { "SYLLABLENUM", AffixReaderCommandKind.CompoundSyllableNum },
                { "WORDCHARS", AffixReaderCommandKind.WordChars },
                { "IGNORE", AffixReaderCommandKind.Ignore },
                { "COMPOUNDFLAG", AffixReaderCommandKind.CompoundFlag },
                { "COMPOUNDMIDDLE", AffixReaderCommandKind.CompoundMiddle },
                { "COMPOUNDBEGIN", AffixReaderCommandKind.CompoundBegin },
                { "COMPOUNDEND", AffixReaderCommandKind.CompoundEnd },
                { "COMPOUNDWORDMAX", AffixReaderCommandKind.CompoundWordMax },
                { "COMPOUNDMIN", AffixReaderCommandKind.CompoundMin },
                { "COMPOUNDROOT", AffixReaderCommandKind.CompoundRoot },
                { "COMPOUNDPERMITFLAG", AffixReaderCommandKind.CompoundPermitFlag },
                { "COMPOUNDFORBIDFLAG", AffixReaderCommandKind.CompoundForbidFlag },
                { "COMPOUNDSYLLABLE", AffixReaderCommandKind.CompoundSyllable },
                { "NOSUGGEST", AffixReaderCommandKind.NoSuggest },
                { "NONGRAMSUGGEST", AffixReaderCommandKind.NoNGramSuggest },
                { "FORBIDDENWORD", AffixReaderCommandKind.ForbiddenWord },
                { "LEMMA_PRESENT", AffixReaderCommandKind.LemmaPresent },
                { "CIRCUMFIX", AffixReaderCommandKind.Circumfix },
                { "ONLYINCOMPOUND", AffixReaderCommandKind.OnlyInCompound },
                { "PSEUDOROOT", AffixReaderCommandKind.NeedAffix },
                { "NEEDAFFIX", AffixReaderCommandKind.NeedAffix },
                { "REP", AffixReaderCommandKind.Replacement },
                { "ICONV", AffixReaderCommandKind.InputConversions },
                { "OCONV", AffixReaderCommandKind.OutputConversions },
                { "PHONE", AffixReaderCommandKind.Phone },
                { "CHECKCOMPOUNDPATTERN", AffixReaderCommandKind.CheckCompoundPattern },
                { "COMPOUNDRULE", AffixReaderCommandKind.CompoundRule },
                { "MAP", AffixReaderCommandKind.Map },
                { "BREAK", AffixReaderCommandKind.Break },
                { "VERSION", AffixReaderCommandKind.Version },
                { "MAXNGRAMSUGS", AffixReaderCommandKind.MaxNgramSuggestions },
                { "MAXDIFF", AffixReaderCommandKind.MaxDifferency },
                { "MAXCPDSUGS", AffixReaderCommandKind.MaxCompoundSuggestions },
                { "KEEPCASE", AffixReaderCommandKind.KeepCase },
                { "FORCEUCASE", AffixReaderCommandKind.ForceUpperCase },
                { "WARN", AffixReaderCommandKind.Warn },
                { "SUBSTANDARD", AffixReaderCommandKind.SubStandard },
                { "PFX", AffixReaderCommandKind.Prefix },
                { "SFX", AffixReaderCommandKind.Suffix },
                { "AF", AffixReaderCommandKind.AliasF },
                { "AM", AffixReaderCommandKind.AliasM }
            };

        private static readonly string[] DefaultBreakTableEntries = { "-", "^-", "-$" };

        private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.TakeArray(new[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' });

        public static readonly Encoding DefaultEncoding = EncodingEx.GetEncodingByName("ISO8859-1") ?? Encoding.UTF8;

        private AffixConfig.Builder Builder { get; }

        private IHunspellLineReader Reader { get; }

        private EntryListType Initialized { get; set; } = EntryListType.None;

#if !NO_ASYNC
        public static async Task<AffixConfig> ReadAsync(IHunspellLineReader reader, AffixConfig.Builder builder = null)
        {
            var readerInstance = new AffixReader(
                builder,
                reader ?? throw new ArgumentNullException(nameof(reader)));

            await readerInstance.ReadAsync().ConfigureAwait(false);

            return readerInstance.Builder.MoveToImmutable();
        }

        private async Task ReadToEndAsync()
        {
            string line;
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
#endif

        public static AffixConfig Read(IHunspellLineReader reader, AffixConfig.Builder builder = null)
        {
            var readerInstance = new AffixReader(
                builder,
                reader ?? throw new ArgumentNullException(nameof(reader)));

            readerInstance.Read();

            return readerInstance.Builder.MoveToImmutable();
        }

        private void ReadToEnd()
        {
            string line;
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

#if !NO_ASYNC
        public static async Task<AffixConfig> ReadAsync(Stream stream, AffixConfig.Builder builder = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return await ReadAsync(reader, builder).ConfigureAwait(false);
            }
        }
        public static async Task<AffixConfig> ReadFileAsync(string filePath, AffixConfig.Builder builder = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            {
                return await ReadAsync(stream, builder).ConfigureAwait(false);
            }
        }
#endif

        public static AffixConfig Read(Stream stream, AffixConfig.Builder builder = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return Read(reader, builder);
            }
        }

        public static AffixConfig ReadFile(string filePath, AffixConfig.Builder builder = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            {
                return Read(stream, builder);
            }
        }

        private bool ParseLine(string line)
        {
            // read through the initial ^[ \t]*
            var commandStartIndex = 0;
            for (; commandStartIndex < line.Length && StringEx.IsTabOrSpace(line[commandStartIndex]); commandStartIndex++) ;

            if (commandStartIndex == line.Length || StringEx.IsCommentPrefix(line[commandStartIndex]))
            {
                return true; // empty, whitespace, or comment
            }

            // read through the final [ \t]*$
            var lineEndIndex = line.Length - 1;
            for (; lineEndIndex > commandStartIndex && StringEx.IsTabOrSpace(line[lineEndIndex]); lineEndIndex--) ;

            // find the end of the command
            var commandEndIndex = commandStartIndex;
            for (; commandEndIndex <= lineEndIndex && !StringEx.IsTabOrSpace(line[commandEndIndex]); commandEndIndex++) ;

            // first command exists between [lineStartIndex,commandEndIndex)
            var parameterStartIndex = commandEndIndex;
            for (; parameterStartIndex <= lineEndIndex && StringEx.IsTabOrSpace(line[parameterStartIndex]); parameterStartIndex++) ;

            var command = line.Substring(commandStartIndex, commandEndIndex - commandStartIndex);

            if (parameterStartIndex <= lineEndIndex)
            {
                if (TryHandleParameterizedCommand(
                        command,
                        line.Subslice(parameterStartIndex, lineEndIndex - parameterStartIndex + 1)))
                {
                    return true;
                }
            }
            else
            {
                if (FileBitFlagCommandMappings.TryGetValue(command, out AffixConfigOptions option))
                {
                    Builder.EnableOptions(option);
                    return true;
                }
            }

            return LogWarning("Failed to parse line: " + line);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsInitialized(EntryListType flags) => EnumEx.HasFlag(Initialized, flags);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SetInitialized(EntryListType flags) =>
            Initialized |= flags;

        private void AddDefaultBreakTableIfEmpty()
        {
            if (!IsInitialized(EntryListType.Break))
            {
                if (Builder.BreakPoints == null)
                {
                    Builder.BreakPoints = new List<string>(DefaultBreakTableEntries.Length);
                }

                if (Builder.BreakPoints.Count == 0)
                {
                    Builder.BreakPoints.AddRange(DefaultBreakTableEntries.Select(Builder.Dedup));
                }
            }
        }

        private bool TryHandleParameterizedCommand(string commandName, StringSlice parameters)
        {
#if DEBUG
            if (commandName == null)
            {
                throw new ArgumentNullException(nameof(commandName));
            }
            if (parameters.IsEmpty)
            {
                throw new ArgumentException(nameof(parameters));
            }
#endif
            if (!CommandMap.TryGetValue(commandName, out AffixReaderCommandKind command))
            {
                return LogWarning($"Unknown command {commandName} with params: {parameters}");
            }

            switch (command)
            {
                case AffixReaderCommandKind.Flag:
                    return TrySetFlagMode(parameters.ToString());
                case AffixReaderCommandKind.KeyString:
                    Builder.KeyString = Builder.Dedup(parameters);
                    return true;
                case AffixReaderCommandKind.TryString:
                    Builder.TryString = Builder.Dedup(parameters);
                    return true;
                case AffixReaderCommandKind.SetEncoding:
                    var encoding = EncodingEx.GetEncodingByName(parameters);
                    if (encoding == null)
                    {
                        return LogWarning("Failed to get encoding: " + parameters);
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
                    if (!Builder.CompoundMin.HasValue)
                    {
                        return LogWarning("Failed to parse CompoundMin: " + parameters);
                    }

                    if (Builder.CompoundMin.GetValueOrDefault() < 1)
                    {
                        Builder.CompoundMin = 1;
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
                    Builder.ForbiddenWord = TryParseFlag(parameters);
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

        private bool TryParseStandardListItem<T>(EntryListType entryListType, StringSlice parameterText, ref List<T> entries, Func<StringSlice, List<T>, bool> parse)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);

                if (IntEx.TryParseInvariant(parameterText, out int expectedSize) && expectedSize >= 0)
                {
                    if (entries == null)
                    {
                        entries = new List<T>(expectedSize);
                    }

                    return true;
                }
            }

            if (entries == null)
            {
                entries = new List<T>();
            }

            return parse(parameterText, entries);
        }

        private bool TryParseCompoundSyllable(StringSlice parameters)
        {
            var parts = parameters.SliceOnTabOrSpace();

            if (parts.Count > 0)
            {
                if (IntEx.TryParseInvariant(parts[0].ToString(), out int maxValue))
                {
                    Builder.CompoundMaxSyllable = maxValue;
                }
                else
                {
                    return LogWarning("Failed to parse CompoundMaxSyllable value from: " + parameters);
                }
            }

            Builder.CompoundVowels =
                1 < parts.Count
                ? CharacterSet.Create(parts[1])
                : DefaultCompoundVowels;

            return true;
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
#if NO_CULTURENOTFOUNDEXCEPTION
            catch (ArgumentException)
#else
            catch (CultureNotFoundException)
#endif
            {
                var dashIndex = language.IndexOf('-');
                if (dashIndex > 0)
                {
                    return GetCultureFromLanguage(language.Substring(0, dashIndex));
                }
                else
                {
                    return CultureInfo.InvariantCulture;
                }
            }
#if !NO_CULTURENOTFOUNDEXCEPTION
            catch (ArgumentException)
            {
                return CultureInfo.InvariantCulture;
            }
#endif
        }

        private bool TryParsePhone(StringSlice parameterText, List<PhoneticEntry> entries)
        {
            var parts = parameterText.SliceOnTabOrSpace();
            if (parts.Count == 0)
            {
                return LogWarning("Failed to parse phone line: " + parameterText);
            }

            entries.Add(
                new PhoneticEntry(
                    Builder.Dedup(parts[0]),
                    parts.Count >= 2 ? Builder.Dedup(parts[1].ReplaceString("_", string.Empty)) : string.Empty));

            return true;
        }

        private bool TryParseMapEntry(StringSlice parameterText, List<MapEntry> entries)
        {
            var values = new List<StringSlice>(parameterText.Length);

            for (int k = 0; k < parameterText.Length; ++k)
            {
                int chb = k;
                int che = k + 1;
                if (parameterText[k] == '(')
                {
                    var parpos = parameterText.IndexOf(')', k);
                    if (parpos >= 0)
                    {
                        chb = k + 1;
                        che = parpos;
                        k = parpos;
                    }
                }

                values.Add(parameterText.Subslice(chb, che - chb));
            }

            entries.Add(MapEntry.TakeArray(Builder.DedupIntoArray(values)));

            return true;
        }

        private bool TryParseConv(StringSlice parameterText, EntryListType entryListType, ref Dictionary<string, MultiReplacementEntry> entries)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);

                if (IntEx.TryParseInvariant(parameterText, out int expectedSize) && expectedSize >= 0)
                {
                    if (entries == null)
                    {
                        entries = new Dictionary<string, MultiReplacementEntry>(expectedSize);
                    }

                    return true;
                }
            }

            if (entries == null)
            {
                entries = new Dictionary<string, MultiReplacementEntry>();
            }

            var parts = parameterText.SliceOnTabOrSpace();
            if (parts.Count < 2)
            {
                return LogWarning($"Bad {entryListType}: {parameterText}");
            }

            entries.AddReplacementEntry(Builder.Dedup(parts[0]), Builder.Dedup(parts[1]));

            return true;
        }

        private bool TryParseBreak(StringSlice parameterText, List<string> entries)
        {
            entries.Add(Builder.Dedup(parameterText));
            return true;
        }

        private bool TryParseAliasF(StringSlice parameterText, List<FlagSet> entries)
        {
            entries.Add(Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(parameterText))));
            return true;
        }

        private bool TryParseAliasM(StringSlice parameterText, List<MorphSet> entries)
        {
            List<StringSlice> parts;
            if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                parts = new StringSlice(parameterText.ReverseString()).SliceOnTabOrSpace();
            }
            else
            {
                parts = parameterText.SliceOnTabOrSpace();
            }

            entries.Add(Builder.Dedup(MorphSet.TakeArray(Builder.DedupIntoArray(parts))));

            return true;
        }

        private bool TryParseCompoundRuleIntoList(StringSlice parameterText, List<CompoundRule> entries)
        {
            var entryBuilder = new List<FlagValue>();

            if (parameterText.Contains('('))
            {
                for (var index = 0; index < parameterText.Length; index++)
                {
                    var indexBegin = index;
                    var indexEnd = indexBegin + 1;
                    if (parameterText[indexBegin] == '(')
                    {
                        var closeParenIndex = parameterText.IndexOf(')', indexEnd);
                        if (closeParenIndex >= 0)
                        {
                            indexBegin = indexEnd;
                            indexEnd = closeParenIndex;
                            index = closeParenIndex;
                        }
                    }

                    var beginFlagValue = new FlagValue(parameterText[indexBegin]);
                    if (beginFlagValue.IsWildcard)
                    {
                        entryBuilder.Add(beginFlagValue);
                    }
                    else
                    {
                        entryBuilder.AddRange(ParseFlagsInOrder(parameterText.Subslice(indexBegin, indexEnd - indexBegin)));
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

        private bool TryParseAffixIntoList<TEntry>(StringSlice parameterText, ref List<AffixEntryGroup.Builder<TEntry>> groups)
            where TEntry : AffixEntry, new()
            =>
            TryParseAffixIntoList<TEntry>(parameterText.ToString(), ref groups);

        private bool TryParseAffixIntoList<TEntry>(string parameterText, ref List<AffixEntryGroup.Builder<TEntry>> groups)
            where TEntry : AffixEntry, new()
        {
            if (groups == null)
            {
                groups = new List<AffixEntryGroup.Builder<TEntry>>();
            }

            var lineMatch = AffixLineRegex.Match(parameterText);
            if (!lineMatch.Success)
            {
                return LogWarning("Failed to parse affix line: " + parameterText);
            }

            var lineMatchGroups = lineMatch.Groups;

            if (!TryParseFlag(lineMatchGroups[1].Value, out FlagValue characterFlag))
            {
                return LogWarning($"Failed to parse affix flag for {lineMatchGroups[1].Value} from: {parameterText}");
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = FlagSet.Empty;

            if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
            {
                if (affixGroup != null)
                {
                    return LogWarning($"Duplicate affix group definition for {affixGroup.AFlag} from: {parameterText}");
                }

                var options = AffixEntryOptions.None;
                if (lineMatchGroups[2].Value.StartsWith('Y'))
                {
                    options |= AffixEntryOptions.CrossProduct;
                }
                if (Builder.IsAliasM)
                {
                    options |= AffixEntryOptions.AliasM;
                }
                if (Builder.IsAliasF)
                {
                    options |= AffixEntryOptions.AliasF;
                }

                IntEx.TryParseInvariant(lineMatchGroups[3].Value, out int expectedEntryCount);

                affixGroup = new AffixEntryGroup.Builder<TEntry>
                {
                    AFlag = characterFlag,
                    Options = options,
                    Entries = new List<TEntry>(expectedEntryCount)
                };

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
                    strip = strip.Reverse();
                }

                // piece 4 - is affix string or 0 for null
                var affixInput = lineMatchGroups[5].Value;
                var affixSlashIndex = affixInput.IndexOf('/');
                StringBuilder affixText;
                if (affixSlashIndex >= 0)
                {
                    affixText = StringBuilderPool.Get(affixInput, 0, affixSlashIndex);

                    if (Builder.IsAliasF)
                    {
                        if (IntEx.TryParseInvariant(affixInput.Substring(affixSlashIndex + 1), out int aliasNumber) && aliasNumber > 0 && aliasNumber <= Builder.AliasF.Count)
                        {
                            contClass = Builder.AliasF[aliasNumber - 1];
                        }
                        else
                        {
                            return LogWarning($"Failed to parse contclasses from : {parameterText}");
                        }
                    }
                    else
                    {
                        contClass = Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(affixInput.Subslice(affixSlashIndex + 1))));
                    }
                }
                else
                {
                    affixText = StringBuilderPool.Get(affixInput);
                }

                if (Builder.IgnoredChars != null && Builder.IgnoredChars.HasItems)
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
                    if (Builder.IsAliasM)
                    {
                        if (IntEx.TryParseInvariant(morphAffixText, out int morphNumber) && morphNumber > 0 && morphNumber <= Builder.AliasM.Count)
                        {
                            morph = Builder.AliasM[morphNumber - 1];
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
                            morphAffixText = morphAffixText.Reverse();
                        }

                        morph = Builder.Dedup(MorphSet.TakeArray(Builder.DedupInPlace(morphAffixText.SplitOnTabOrSpace())));
                    }
                }
                else
                {
                    morph = MorphSet.Empty;
                }

                if (affixGroup == null)
                {
                    affixGroup = new AffixEntryGroup.Builder<TEntry>
                    {
                        AFlag = characterFlag,
                        Options = AffixEntryOptions.None,
                        Entries = new List<TEntry>()
                    };
                }

                if (!Builder.HasContClass && contClass.HasItems)
                {
                    Builder.HasContClass = true;
                }

                affixGroup.Entries.Add(AffixEntry.CreateWithoutNullCheck<TEntry>(
                    Builder.Dedup(strip),
                    Builder.Dedup(StringBuilderPool.GetStringAndReturn(affixText)),
                    Builder.Dedup(conditions),
                    morph,
                    contClass));

                return true;
            }

            return LogWarning("Affix line not fully parsed: " + parameterText);
        }

        private static string ReverseCondition(string conditionText)
        {
            if (string.IsNullOrEmpty(conditionText))
            {
                return conditionText;
            }

            var chars = StringBuilderPool.Get(conditionText);
            chars.Reverse();
            var neg = false;
            var lastIndex = chars.Length - 1;

            for (int k = lastIndex; k >= 0; k--)
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

        private bool TryParseReplacements(StringSlice parameterText, List<SingleReplacement> entries)
        {
            var parameters = parameterText.SliceOnTabOrSpace();
            if (parameters.Count == 0)
            {
                return LogWarning("Failed to parse replacements from: " + parameterText);
            }

            var patternBuilder = StringBuilderPool.Get(parameters[0]);
            var hasTrailingDollar = patternBuilder.EndsWith('$');
            var hasStartingCarrot = patternBuilder.StartsWith('^');
            var type = ReplacementValueType.Med;

            if (hasTrailingDollar)
            {
                type |= ReplacementValueType.Fin;
                patternBuilder.Remove(patternBuilder.Length - 1, 1);
            }

            if (hasStartingCarrot)
            {
                type |= ReplacementValueType.Ini;
                patternBuilder.Remove(0, 1);
            }

            patternBuilder.Replace('_', ' ');

            entries.Add(new SingleReplacement(
                Builder.Dedup(StringBuilderPool.GetStringAndReturn(patternBuilder)),
                parameters.Count > 1 ?
                    Builder.Dedup(parameters[1].ReplaceString('_', ' '))
                    : string.Empty,
                type));

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(StringSlice parameterText, List<PatternEntry> entries)
        {
            var parameters = parameterText.SliceOnTabOrSpace();
            if (parameters.Count == 0)
            {
                return LogWarning("Failed to parse compound pattern from: " + parameterText);
            }

            var pattern = parameters[0];
            var condition = default(FlagValue);
            var slashIndex = pattern.IndexOf('/');

            if (slashIndex >= 0)
            {
                if (!TryParseFlag(pattern.Subslice(slashIndex + 1), out condition))
                {
                    return LogWarning($"Failed to parse compound pattern 1 {pattern} from: {parameterText}");
                }

                pattern = pattern.Subslice(0, slashIndex);
            }

            var pattern2 = StringSlice.Empty;
            var pattern3 = string.Empty;
            var condition2 = default(FlagValue);

            if (parameters.Count >= 2)
            {
                pattern2 = parameters[1];
                slashIndex = pattern2.IndexOf('/');
                if (slashIndex >= 0)
                {
                    if (!TryParseFlag(pattern2.Subslice(slashIndex + 1), out condition2))
                    {
                        return LogWarning($"Failed to parse compound pattern 2 {pattern2} from: {parameterText}");
                    }

                    pattern2 = pattern2.Subslice(0, slashIndex);
                }

                if (parameters.Count >= 3)
                {
                    pattern3 = parameters[2].ToString();
                    Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);
                }
            }

            entries.Add(new PatternEntry(
                Builder.Dedup(pattern.ToString()),
                Builder.Dedup(pattern2.ToString()),
                Builder.Dedup(pattern3),
                condition,
                condition2));

            return true;
        }

        private bool TrySetFlagMode(string modeText)
        {
            if (string.IsNullOrEmpty(modeText))
            {
                return LogWarning("Attempt to set empty flag mode.");
            }

            if (FlagModeParameterMappings.TryGetValue(modeText, out FlagMode mode))
            {
                if (mode == Builder.FlagMode)
                {
                    return LogWarning("Redundant FlagMode: " + modeText);
                }

                Builder.FlagMode = mode;
                return true;
            }
            else
            {
                var bestMatchFlagMode = FindBestFlagMode(modeText);

                if (bestMatchFlagMode.HasValue)
                {
                    Builder.FlagMode = bestMatchFlagMode.GetValueOrDefault();
                    return true;
                }

                return LogWarning("Unknown FlagMode: " + modeText);
            }
        }

        private FlagMode? FindBestFlagMode(string modeText)
        {
            foreach(var pair in FlagModeParameterMappings)
            {
                if (modeText.StartsWith(pair.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Value;
                }
            }

            return default(FlagMode?);
        }

        private bool LogWarning(string text)
        {
            Builder.LogWarning(text);
            return false;
        }

        private string ReDecodeConvertedStringAsUtf8(string decoded) =>
            HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(decoded, Builder.Encoding ?? Reader.CurrentEncoding);

        private string ReDecodeConvertedStringAsUtf8(StringSlice decoded) =>
            HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(decoded, Builder.Encoding ?? Reader.CurrentEncoding);

        private FlagValue[] ParseFlagsInOrder(StringSlice text)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.ParseFlagsInOrder(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char)
                : FlagValue.ParseFlagsInOrder(text, flagMode);
        }


        private bool TryParseFlag(string text, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out value)
                : FlagValue.TryParseFlag(text, flagMode, out value);
        }

        private bool TryParseFlag(StringSlice text, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out value)
                : FlagValue.TryParseFlag(text, flagMode, out value);
        }

        private FlagValue TryParseFlag(StringSlice text) =>
            TryParseFlag(text, out FlagValue value)
                ? value
                : default(FlagValue);

        [Flags]
        internal enum EntryListType : short
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
}
