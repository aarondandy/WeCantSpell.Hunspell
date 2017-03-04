using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace Hunspell
{
    public sealed class AffixReader
    {
        public AffixReader(AffixConfig.Builder builder, IHunspellLineReader reader)
        {
            Builder = builder ?? new AffixConfig.Builder();
            Reader = reader;
        }

        private const RegexOptions DefaultRegexOptions =
#if !NO_COMPILED_REGEX
            RegexOptions.Compiled |
#endif
            RegexOptions.CultureInvariant;

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$", DefaultRegexOptions);

        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$", DefaultRegexOptions);

        private static readonly Regex CommentLineRegex = new Regex(@"^\s*(#|//)", DefaultRegexOptions | RegexOptions.ExplicitCapture);

        private static readonly Regex AffixLineRegex = new Regex(
            @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].*)?$",
            DefaultRegexOptions);

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
            {"UTF", FlagMode.Uni},
            {"UNI", FlagMode.Uni},
            {"UTF-8", FlagMode.Uni}
        };

        private static readonly string[] DefaultBreakTableEntries = new[] { "-", "^-", "-$" };

        private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.TakeArray(new[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' });

        public static readonly Encoding DefaultEncoding = Encoding.GetEncoding("ISO8859-1");

        private AffixConfig.Builder Builder { get; }

        private IHunspellLineReader Reader { get; }

        private EntryListType Initialized { get; set; } = EntryListType.None;


#if !NO_ASYNC
        public static async Task<AffixConfig> ReadAsync(IHunspellLineReader reader, AffixConfig.Builder builder = null)
        {
            var readerInstance = new AffixReader(builder, reader);

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
            var readerInstance = new AffixReader(builder, reader);

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
            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return await ReadAsync(reader, builder).ConfigureAwait(false);
            }
        }
#if !NO_IO_FILE
        public static async Task<AffixConfig> ReadFileAsync(string filePath, AffixConfig.Builder builder = null)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return await ReadAsync(stream, builder).ConfigureAwait(false);
            }
        }
#endif
#endif

        public static AffixConfig Read(Stream stream, AffixConfig.Builder builder = null)
        {
            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return Read(reader, builder);
            }
        }

#if !NO_IO_FILE
        public static AffixConfig ReadFile(string filePath, AffixConfig.Builder builder = null)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Read(stream, builder);
            }
        }
#endif

        private bool ParseLine(string line)
        {
            if (StringEx.IsNullOrWhiteSpace(line))
            {
                return true;
            }

            if (CommentLineRegex.IsMatch(line))
            {
                return true;
            }

            AffixConfigOptions option;
            var singleCommandParsed = SingleCommandParseRegex.Match(line);
            if (singleCommandParsed.Success && FileBitFlagCommandMappings.TryGetValue(singleCommandParsed.Groups[1].Value, out option))
            {
                Builder.EnableOptions(option);
                return true;
            }

            var multiPartCommandParsed = LineStringParseRegex.Match(line);
            if (
                multiPartCommandParsed.Success
                && TryHandleParameterizedCommand(multiPartCommandParsed.Groups[1].Value, multiPartCommandParsed.Groups[2].Value)
            )
            {
                return true;
            }

            Builder.LogWarning("Failed to parse line: " + line);
            return false;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsInitialized(EntryListType flags) => EnumEx.HasFlag(Initialized, flags);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SetInitialized(EntryListType flags)
        {
            Initialized |= flags;
        }

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

        private bool TryHandleParameterizedCommand(string name, string parameters)
        {
            var commandName = name.ToUpperInvariant();
            switch (commandName)
            {
                case "FLAG": // parse in the try string
                    return TrySetFlagMode(parameters);
                case "KEY": // parse in the keyboard string
                    Builder.KeyString = Builder.Dedup(parameters);
                    return true;
                case "TRY": // parse in the try string
                    Builder.TryString = Builder.Dedup(parameters);
                    return true;
                case "SET": // parse in the name of the character set used by the .dict and .aff
                    var encoding = EncodingEx.GetEncodingByName(parameters);
                    if (encoding == null)
                    {
                        Builder.LogWarning("Failed to get encoding: " + parameters);
                        return false;
                    }

                    Builder.Encoding = encoding;
                    return true;
                case "LANG": // parse in the language for language specific codes
                    Builder.Language = Builder.Dedup(parameters.Trim());
                    Builder.Culture = GetCultureFromLanguage(Builder.Language);
                    return true;
                case "SYLLABLENUM": // parse in the flag used by compound_check() method
                    Builder.CompoundSyllableNum = Builder.Dedup(parameters);
                    return true;
                case "WORDCHARS": // parse in the extra word characters
                    Builder.WordChars = CharacterSet.Create(parameters);
                    return true;
                case "IGNORE": // parse in the ignored characters (for example, Arabic optional diacretics characters)
                    Builder.IgnoredChars = CharacterSet.Create(parameters);
                    return true;
                case "COMPOUNDFLAG": // parse in the flag used by the controlled compound words
                    return TryParseFlag(parameters, out Builder.CompoundFlag);
                case "COMPOUNDMIDDLE": // parse in the flag used by compound words
                    return TryParseFlag(parameters, out Builder.CompoundMiddle);
                case "COMPOUNDBEGIN": // parse in the flag used by compound words
                    return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundEnd)
                        : TryParseFlag(parameters, out Builder.CompoundBegin);
                case "COMPOUNDEND": // parse in the flag used by compound words
                    return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundBegin)
                        : TryParseFlag(parameters, out Builder.CompoundEnd);
                case "COMPOUNDWORDMAX": // parse in the data used by compound_check() method
                    Builder.CompoundWordMax = IntEx.TryParseInvariant(parameters);
                    return Builder.CompoundWordMax.HasValue;
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    Builder.CompoundMin = IntEx.TryParseInvariant(parameters);
                    if (!Builder.CompoundMin.HasValue)
                    {
                        Builder.LogWarning("Failed to parse CompoundMin: " + parameters);
                        return false;
                    }

                    if (Builder.CompoundMin.GetValueOrDefault() < 1)
                    {
                        Builder.CompoundMin = 1;
                    }

                    return true;
                case "COMPOUNDROOT": // parse in the flag sign compounds in dictionary
                    return TryParseFlag(parameters, out Builder.CompoundRoot);
                case "COMPOUNDPERMITFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out Builder.CompoundPermitFlag);
                case "COMPOUNDFORBIDFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out Builder.CompoundForbidFlag);
                case "COMPOUNDSYLLABLE": // parse in the max. words and syllables in compounds
                    return TryParseCompoundSyllable(parameters);
                case "NOSUGGEST":
                    return TryParseFlag(parameters, out Builder.NoSuggest);
                case "NONGRAMSUGGEST":
                    return TryParseFlag(parameters, out Builder.NoNgramSuggest);
                case "FORBIDDENWORD": // parse in the flag used by forbidden words
                    Builder.ForbiddenWord = TryParseFlag(parameters);
                    return Builder.ForbiddenWord.HasValue;
                case "LEMMA_PRESENT": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.LemmaPresent);
                case "CIRCUMFIX": // parse in the flag used by circumfixes
                    return TryParseFlag(parameters, out Builder.Circumfix);
                case "ONLYINCOMPOUND": // parse in the flag used by fogemorphemes
                    return TryParseFlag(parameters, out Builder.OnlyInCompound);
                case "PSEUDOROOT": // parse in the flag used by `needaffixs'
                case "NEEDAFFIX": // parse in the flag used by `needaffixs'
                    return TryParseFlag(parameters, out Builder.NeedAffix);
                case "REP": // parse in the typical fault correcting table
                    return TryParseStandardListItem(EntryListType.Replacements, parameters, ref Builder.Replacements, TryParseReplacements);
                case "ICONV": // parse in the input conversion table
                    return TryParseConv(parameters, EntryListType.Iconv, ref Builder.InputConversions);
                case "OCONV": // parse in the output conversion table
                    return TryParseConv(parameters, EntryListType.Oconv, ref Builder.OutputConversions);
                case "PHONE": // parse in the phonetic conversion table
                    return TryParseStandardListItem(EntryListType.Phone, parameters, ref Builder.Phone, TryParsePhone);
                case "CHECKCOMPOUNDPATTERN": // parse in the checkcompoundpattern table
                    return TryParseStandardListItem(EntryListType.CompoundPatterns, parameters, ref Builder.CompoundPatterns, TryParseCheckCompoundPatternIntoCompoundPatterns);
                case "COMPOUNDRULE": // parse in the defcompound table
                    return TryParseStandardListItem(EntryListType.CompoundRules, parameters, ref Builder.CompoundRules, TryParseCompoundRuleIntoList);
                case "MAP": // parse in the related character map table
                    return TryParseStandardListItem(EntryListType.Map, parameters, ref Builder.RelatedCharacterMap, TryParseMapEntry);
                case "BREAK": // parse in the word breakpoints table
                    return TryParseStandardListItem(EntryListType.Break, parameters, ref Builder.BreakPoints, TryParseBreak);
                case "VERSION":
                    Builder.Version = parameters;
                    return true;
                case "MAXNGRAMSUGS":
                    Builder.MaxNgramSuggestions = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxNgramSuggestions.HasValue;
                case "MAXDIFF":
                    Builder.MaxDifferency = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxDifferency.HasValue;
                case "MAXCPDSUGS":
                    Builder.MaxCompoundSuggestions = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxCompoundSuggestions.HasValue;
                case "KEEPCASE": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.KeepCase);
                case "FORCEUCASE":
                    return TryParseFlag(parameters, out Builder.ForceUpperCase);
                case "WARN":
                    return TryParseFlag(parameters, out Builder.Warn);
                case "SUBSTANDARD":
                    return TryParseFlag(parameters, out Builder.SubStandard);
                case "PFX":
                case "SFX":
                    var parseAsPrefix = "PFX" == commandName;
                    if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                    {
                        parseAsPrefix = !parseAsPrefix;
                    }

                    return parseAsPrefix
                        ? TryParseAffixIntoList(parameters, ref Builder.Prefixes)
                        : TryParseAffixIntoList(parameters, ref Builder.Suffixes);
                case "AF":
                    return TryParseStandardListItem(EntryListType.AliasF, parameters, ref Builder.AliasF, TryParseAliasF);
                case "AM":
                    return TryParseStandardListItem(EntryListType.AliasM, parameters, ref Builder.AliasM, TryParseAliasM);
                default:
                    Builder.LogWarning($"Unknown command {commandName} with params: {parameters}");
                    return false;
            }
        }

        private bool TryParseStandardListItem<T>(EntryListType entryListType, string parameterText, ref List<T> entries, Func<string, List<T>, bool> parse)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);

                int expectedSize;
                if (IntEx.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
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

        private bool TryParseCompoundSyllable(string parameters)
        {
            var parts = parameters.SliceOnTabOrSpace();

            if (parts.Length > 0)
            {
                int maxValue;
                if (IntEx.TryParseInvariant(parts[0], out maxValue))
                {
                    Builder.CompoundMaxSyllable = maxValue;
                }
                else
                {
                    Builder.LogWarning("Failed to parse CompoundMaxSyllable value from: " + parameters);
                    return false;
                }
            }

            Builder.CompoundVowels =
                1 < parts.Length
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
#if !NET_3_5
            catch (CultureNotFoundException)
#else
            catch (ArgumentException)
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
#if !NET_3_5
            catch (ArgumentException)
            {
                return CultureInfo.InvariantCulture;
            }
#endif
        }

        private bool TryParsePhone(string parameterText, List<PhoneticEntry> entries)
        {
            var parts = parameterText.SplitOnTabOrSpace();

            if (parts.Length == 0)
            {
                Builder.LogWarning("Failed to parse phone line: " + parameterText);
                return false;
            }

            entries.Add(new PhoneticEntry(
                    Builder.Dedup(parts[0]),
                    parts.Length >= 2 ? Builder.Dedup(parts[1].Replace("_", string.Empty)) : string.Empty));

            return true;
        }

        private bool TryParseMapEntry(string parameterText, List<MapEntry> entries)
        {
            var values = new List<string>(parameterText.Length);

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

                values.Add(Builder.Dedup(parameterText.Substring(chb, che - chb)));
            }

            entries.Add(MapEntry.Create(values));

            return true;
        }

        private bool TryParseConv(string parameterText, EntryListType entryListType, ref Dictionary<string, MultiReplacementEntry> entries)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);

                int expectedSize;
                if (IntEx.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
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

            var parts = parameterText.SplitOnTabOrSpace();
            if (parts.Length < 2)
            {
                Builder.LogWarning($"Bad {entryListType}: {parameterText}");
                return false;
            }

            entries.AddReplacementEntry(Builder.Dedup(parts[0]), Builder.Dedup(parts[1]));

            return true;
        }

        private bool TryParseBreak(string parameterText, List<string> entries)
        {
            entries.Add(Builder.Dedup(parameterText));
            return true;
        }

        private bool TryParseAliasF(string parameterText, List<FlagSet> entries)
        {
            entries.Add(ParseFlags(parameterText));
            return true;
        }

        private bool TryParseAliasM(string parameterText, List<MorphSet> entries)
        {
            if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                parameterText = parameterText.Reverse();
            }

            var parts = parameterText.SplitOnTabOrSpace();

            Builder.DedupInPlace(parts);

            entries.Add(Builder.Dedup(MorphSet.TakeArray(parts)));

            return true;
        }

        private bool TryParseCompoundRuleIntoList(string parameterText, List<CompoundRule> entries)
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

                    if (parameterText[indexBegin] == '*' || parameterText[indexBegin] == '?')
                    {
                        entryBuilder.Add(new FlagValue(parameterText[indexBegin]));
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
                Builder.LogWarning("Failed to parse affix line: " + parameterText);
                return false;
            }

            var lineMatchGroups = lineMatch.Groups;

            FlagValue characterFlag;
            if (!TryParseFlag(lineMatchGroups[1].Value, out characterFlag))
            {
                Builder.LogWarning($"Failed to parse affix flag for {lineMatchGroups[1].Value} from: {parameterText}");
                return false;
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = FlagSet.Empty;

            if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
            {
                if (affixGroup != null)
                {
                    Builder.LogWarning($"Duplicate affix group definition for {affixGroup.AFlag} from: {parameterText}");
                    return false;
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

                int expectedEntryCount;
                IntEx.TryParseInvariant(lineMatchGroups[3].Value, out expectedEntryCount);

                affixGroup = new AffixEntryGroup.Builder<TEntry>
                {
                    AFlag = characterFlag,
                    Options = options,
                    Entries = new List<TEntry>(expectedEntryCount)
                };

                groups.Add(affixGroup);

                return true;
            }
            else if (lineMatchGroups[4].Success && lineMatchGroups[5].Success && lineMatchGroups[6].Success)
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
                    var slashPartOffset = affixSlashIndex + 1;
                    var slashPartLength = affixInput.Length - slashPartOffset;
                    affixText = StringBuilderPool.Get(affixInput, 0, affixSlashIndex);

                    if (Builder.IsAliasF)
                    {
                        int aliasNumber;
                        if (IntEx.TryParseInvariant(affixInput.Subslice(slashPartOffset, slashPartLength), out aliasNumber) && aliasNumber > 0 && aliasNumber <= Builder.AliasF.Count)
                        {
                            contClass = Builder.AliasF[aliasNumber - 1];
                        }
                        else
                        {
                            Builder.LogWarning($"Failed to parse contclasses from : {parameterText}");
                            return false;
                        }
                    }
                    else
                    {
                        contClass = ParseFlags(affixInput.Subslice(slashPartOffset, slashPartLength));
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

                if (!string.IsNullOrEmpty(strip) && !conditions.AllowsAnySingleCharacter)
                {
                    bool isRedundant;
                    if (typeof(TEntry) == typeof(PrefixEntry))
                    {
                        isRedundant = RedundantConditionPrefix(strip, conditions);
                    }
                    else if (typeof(TEntry) == typeof(SuffixEntry))
                    {
                        isRedundant = RedundantConditionSuffix(strip, conditions);
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
                        int morphNumber;
                        if (IntEx.TryParseInvariant(morphAffixText, out morphNumber) && morphNumber > 0 && morphNumber <= Builder.AliasM.Count)
                        {
                            morph = Builder.AliasM[morphNumber - 1];
                        }
                        else
                        {
                            Builder.LogWarning($"Failed to parse morph {morphAffixText} from: {parameterText}");
                            return false;
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

                affixGroup.Entries.Add(AffixEntry.Create<TEntry>(
                    Builder.Dedup(strip),
                    Builder.Dedup(StringBuilderPool.GetStringAndReturn(affixText)),
                    Builder.Dedup(conditions),
                    morph,
                    contClass));

                return true;
            }
            else
            {
                Builder.LogWarning("Affix line not fully parsed: " + parameterText);
                return false;
            }
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

        private bool RedundantConditionPrefix(string text, CharacterConditionGroup conditions)
        {
            return conditions.IsOnlyPossibleMatch(text);
        }

        private bool RedundantConditionPrefix(string text, string conditions)
        {
            if (text.StartsWith(conditions))
            {
                return true;
            }

            var lastConditionIndex = conditions.Length - 1;
            int i, j;
            for (i = 0, j = 0; i < text.Length && j < conditions.Length; i++, j++)
            {
                if (conditions[j] != '[')
                {
                    if (conditions[j] != text[i])
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionPrefix)} with {text} and {conditions}");
                        return false;
                    }
                }
                else if (j < lastConditionIndex)
                {
                    var neg = conditions[j + 1] == '^';
                    var @in = false;

                    do
                    {
                        j++;
                        if (text[i] == conditions[j])
                        {
                            @in = true;
                        }
                    } while (j < lastConditionIndex && conditions[j] != ']');

                    if (j == lastConditionIndex && conditions[j] != ']')
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionPrefix)} with {text} and {conditions}");
                        return false;
                    }

                    if (neg == @in)
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionPrefix)} with {text} and {conditions}");
                        return false;
                    }
                }
            }

            return j >= conditions.Length;
        }

        private bool RedundantConditionSuffix(string text, CharacterConditionGroup conditions)
        {
            return conditions.IsOnlyPossibleMatch(text);
        }

        private bool RedundantConditionSuffix(string text, string conditions)
        {
            if (text.EndsWith(conditions))
            {
                return true;
            }

            var lastConditionIndex = conditions.Length - 1;
            int i, j;
            for (i = text.Length - 1, j = conditions.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (conditions[j] != ']')
                {
                    if (conditions[j] != text[i])
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionSuffix)} with {text} and {conditions}");
                        return false;
                    }
                }
                else if (j > 0)
                {
                    var @in = false;
                    do
                    {
                        j--;
                        if (text[i] == conditions[j])
                        {
                            @in = true;
                        }
                    } while (j > 0 && conditions[j] != '[');

                    if (j == 0 && conditions[j] != '[')
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionSuffix)} with {text} and {conditions}");
                        return false;
                    }

                    var neg = j < lastConditionIndex && conditions[j + 1] == '^';
                    if (neg == @in)
                    {
                        Builder.LogWarning($"Failure checking {nameof(RedundantConditionSuffix)} with {text} and {conditions}");
                        return false;
                    }
                }
            }

            if (j < 0)
            {
                return true;
            }

            return false;
        }

        private bool TryParseReplacements(string parameterText, List<SingleReplacement> entries)
        {
            var parameters = parameterText.SliceOnTabOrSpace();
            if (parameters.Length == 0)
            {
                Builder.LogWarning("Failed to parse replacements from: " + parameterText);
                return false;
            }

            var patternBuilder = StringBuilderPool.Get(parameters[0]);
            var outString = parameters.Length > 1 ? parameters[1].ToString() : string.Empty;

            ReplacementValueType type;
            var hasTrailingDollar = patternBuilder.EndsWith('$');
            if (patternBuilder.StartsWith('^'))
            {
                if (hasTrailingDollar)
                {
                    type = ReplacementValueType.Isol;
                    patternBuilder.Remove(patternBuilder.Length - 1, 1);
                }
                else
                {
                    type = ReplacementValueType.Ini;
                }

                patternBuilder.Remove(0, 1);
            }
            else
            {
                if (hasTrailingDollar)
                {
                    type = ReplacementValueType.Fin;
                    patternBuilder.Remove(patternBuilder.Length - 1, 1);
                }
                else
                {
                    type = ReplacementValueType.Med;
                }
            }

            patternBuilder.Replace('_', ' ');

            entries.Add(new SingleReplacement(
                Builder.Dedup(StringBuilderPool.GetStringAndReturn(patternBuilder)),
                Builder.Dedup(outString.Replace('_', ' ')),
                type));

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(string parameterText, List<PatternEntry> entries)
        {
            var parameters = parameterText.SliceOnTabOrSpace();
            if (parameters.Length == 0)
            {
                Builder.LogWarning("Failed to parse compound pattern from: " + parameterText);
                return false;
            }

            var pattern = parameters[0];
            StringSlice pattern2 = StringSlice.Null;
            StringSlice pattern3 = StringSlice.Null;
            var condition = default(FlagValue);
            var condition2 = default(FlagValue);

            var slashIndex = pattern.IndexOf('/');
            if (slashIndex >= 0)
            {
                if (!TryParseFlag(pattern.Subslice(slashIndex + 1), out condition))
                {
                    Builder.LogWarning($"Failed to parse compound pattern 1 {pattern} from: {parameterText}");
                    return false;
                }

                pattern = pattern.Subslice(0, slashIndex);
            }

            if (parameters.Length >= 2)
            {
                pattern2 = parameters[1];
                slashIndex = pattern2.IndexOf('/');
                if (slashIndex >= 0)
                {
                    if (!TryParseFlag(pattern2.Subslice(slashIndex + 1), out condition2))
                    {
                        Builder.LogWarning($"Failed to parse compound pattern 2 {pattern2} from: {parameterText}");
                        return false;
                    }

                    pattern2 = pattern2.Subslice(0, slashIndex);
                }

                if (parameters.Length >= 3)
                {
                    pattern3 = parameters[2];
                    Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);
                }
            }

            entries.Add(new PatternEntry(
                Builder.Dedup(pattern.ToString()),
                Builder.Dedup(pattern2.ToString()),
                Builder.Dedup(pattern3.ToString()),
                condition,
                condition2));

            return true;
        }

        private bool TrySetFlagMode(string modeText)
        {
            if (string.IsNullOrEmpty(modeText))
            {
                Builder.LogWarning($"Attempt to set empty flag mode.");
                return false;
            }

            FlagMode mode;
            if (FlagModeParameterMappings.TryGetValue(modeText, out mode))
            {
                if (mode == Builder.FlagMode)
                {
                    Builder.LogWarning($"Redundant {nameof(Builder.FlagMode)}: {modeText}");
                    return false;
                }

                Builder.FlagMode = mode;
                return true;
            }
            else
            {
                Builder.LogWarning($"Unknown {nameof(FlagMode)}: {modeText}");
                return false;
            }
        }

        private string ReDecodeConvertedStringAsUtf8(string decoded)
        {
            var encoding = Builder.Encoding ?? Reader.CurrentEncoding;
            if (encoding == Encoding.UTF8)
            {
                return decoded;
            }

            var encodedBytes = encoding.GetBytes(decoded);
            return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);
        }

        private StringSlice ReDecodeConvertedStringAsUtf8(StringSlice decoded)
        {
            var encoding = Builder.Encoding ?? Reader.CurrentEncoding;
            if (encoding == Encoding.UTF8)
            {
                return decoded;
            }

            var encodedBytes = new byte[encoding.GetMaxByteCount(decoded.Length)];
            var byteEncodedCount = encoding.GetBytes(decoded.Text, decoded.Offset, decoded.Length, encodedBytes, 0);
            return StringSlice.Create(Encoding.UTF8.GetString(encodedBytes, 0, byteEncodedCount));
        }

        private FlagSet ParseFlags(string text) => Builder.TakeArrayForFlagSet(ParseFlagsInOrder(text));

        private FlagValue[] ParseFlagsInOrder(string text)
        {
            var flagMode = Builder.FlagMode;
            if (flagMode == FlagMode.Uni)
            {
                text = ReDecodeConvertedStringAsUtf8(text);
                flagMode = FlagMode.Char;
            }

            return FlagValue.ParseFlagsInOrder(text, flagMode);
        }

        private FlagSet ParseFlags(StringSlice text) => Builder.TakeArrayForFlagSet(ParseFlagsInOrder(text));

        private FlagValue[] ParseFlagsInOrder(StringSlice text)
        {
            var flagMode = Builder.FlagMode;
            if (flagMode == FlagMode.Uni)
            {
                text = ReDecodeConvertedStringAsUtf8(text);
                flagMode = FlagMode.Char;
            }

            return FlagValue.ParseFlagsInOrder(text, flagMode);
        }

        private bool TryParseFlag(string text, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            if (flagMode == FlagMode.Uni)
            {
                text = ReDecodeConvertedStringAsUtf8(text);
                flagMode = FlagMode.Char;
            }

            return FlagValue.TryParseFlag(text, flagMode, out value);
        }

        private bool TryParseFlag(StringSlice text, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out value)
                : FlagValue.TryParseFlag(text, flagMode, out value);
        }

        private FlagValue TryParseFlag(string text)
        {
            var flagMode = Builder.FlagMode;
            if (Builder.FlagMode == FlagMode.Uni)
            {
                text = ReDecodeConvertedStringAsUtf8(text);
                flagMode = FlagMode.Char;
            }

            FlagValue value;
            return FlagValue.TryParseFlag(text, flagMode, out value)
                ? value
                : default(FlagValue);
        }

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
    }
}
