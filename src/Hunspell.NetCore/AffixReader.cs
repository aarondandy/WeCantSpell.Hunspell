using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Hunspell
{
    public sealed class AffixReader
    {
        private AffixReader(AffixConfig.Builder builder, IHunspellLineReader reader)
        {
            Builder = builder;
            Reader = reader;
        }

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex CommentLineRegex = new Regex(@"^\s*[#].*", RegexOptions.Compiled | RegexOptions.CultureInvariant);

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
            {"UTF", FlagMode.Uni},
            {"UNI", FlagMode.Uni},
            {"UTF-8", FlagMode.Uni}
        };

        private static readonly string[] DefaultBreakTableEntries = new[] { "-", "^-", "-$" };

        private AffixConfig.Builder Builder { get; }

        private IHunspellLineReader Reader { get; }

        private List<string> Warnings { get; } = new List<string>();

        private EntryListType Initialized { get; set; } = EntryListType.None;

        public static async Task<AffixConfig> ReadAsync(IHunspellLineReader reader)
        {
            var readerInstance = new AffixReader(new AffixConfig.Builder(), reader);

            await readerInstance.ReadToEndAsync().ConfigureAwait(false);

            readerInstance.AddDefaultBreakTableIfEmpty();

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

        public static AffixConfig Read(IHunspellLineReader reader)
        {
            var readerInstance = new AffixReader(new AffixConfig.Builder(), reader);

            readerInstance.ReadToEnd();

            readerInstance.AddDefaultBreakTableIfEmpty();

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

        public static async Task<AffixConfig> ReadFileAsync(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new DynamicEncodingLineReader(stream, AffixConfig.DefaultEncoding))
            {
                return await ReadAsync(reader).ConfigureAwait(false);
            }
        }

        public static AffixConfig ReadFile(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new DynamicEncodingLineReader(stream, AffixConfig.DefaultEncoding))
            {
                return Read(reader);
            }
        }

        private bool ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
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

            return false;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsInitialized(EntryListType flags)
        {
            return Initialized.HasFlag(flags);
        }

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
                    Builder.BreakPoints.AddRange(DefaultBreakTableEntries);
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
                    Builder.KeyString = parameters;
                    return true;
                case "TRY": // parse in the try string
                    Builder.TryString = parameters;
                    return true;
                case "SET": // parse in the name of the character set used by the .dict and .aff
                    var encoding = EncodingEx.GetEncodingByName(parameters);
                    if (encoding == null)
                    {
                        return false;
                    }

                    Builder.Encoding = encoding;
                    return true;
                case "LANG": // parse in the language for language specific codes
                    Builder.Language = parameters.Trim();
                    Builder.Culture = GetCultureFromLanguage(Builder.Language);
                    return true;
                case "SYLLABLENUM": // parse in the flag used by compound_check() method
                    Builder.CompoundSyllableNum = parameters;
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
                    return Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundEnd)
                        : TryParseFlag(parameters, out Builder.CompoundBegin);
                case "COMPOUNDEND": // parse in the flag used by compound words
                    return Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundBegin)
                        : TryParseFlag(parameters, out Builder.CompoundEnd);
                case "COMPOUNDWORDMAX": // parse in the data used by compound_check() method
                    Builder.CompoundWordMax = IntEx.TryParseInvariant(parameters);
                    return Builder.CompoundWordMax.HasValue;
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    Builder.CompoundMin = IntEx.TryParseInvariant(parameters);
                    if (!Builder.CompoundMin.HasValue)
                    {
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
                    if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
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

        private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.TakeArray(new[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' });

        private bool TryParseCompoundSyllable(string parameters)
        {
            var parts = parameters.SplitOnTabOrSpace();

            if (parts.Length > 0)
            {
                int maxValue;
                if (IntEx.TryParseInvariant(parts[0], out maxValue))
                {
                    Builder.CompoundMaxSyllable = maxValue;
                }
                else
                {
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
            catch (CultureNotFoundException)
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
            catch (ArgumentException)
            {
                return CultureInfo.InvariantCulture;
            }
        }

        private static bool TryParsePhone(string parameterText, List<PhoneticEntry> entries)
        {
            var parts = parameterText.SplitOnTabOrSpace();

            if (parts.Length == 0)
            {
                return false;
            }

            entries.Add(new PhoneticEntry(
                    parts[0],
                    parts.Length >= 2 ? parts[1].Replace("_", string.Empty) : string.Empty));

            return true;
        }

        private static bool TryParseMapEntry(string parameterText, List<MapEntry> entries)
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

                values.Add(parameterText.Substring(chb, che - chb));
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
                return false;
            }

            entries.AddReplacementEntry(parts[0], parts[1]);

            return true;
        }

        private static bool TryParseBreak(string parameterText, List<string> entries)
        {
            entries.Add(parameterText);
            return true;
        }

        private bool TryParseAliasF(string parameterText, List<FlagSet> entries)
        {
            entries.Add(ParseFlags(parameterText));
            return true;
        }

        private bool TryParseAliasM(string parameterText, List<MorphSet> entries)
        {
            if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
            {
                parameterText = parameterText.Reverse();
            }

            var parts = parameterText.SplitOnTabOrSpace();

            entries.Add(MorphSet.TakeArray(parts));

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
                        entryBuilder.AddRange(ParseFlagsInOrder(parameterText, indexBegin, indexEnd - indexBegin));
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
                return false;
            }

            var lineMatchGroups = lineMatch.Groups;

            FlagValue characterFlag;
            if (!TryParseFlag(lineMatchGroups[1].Value, out characterFlag))
            {
                return false;
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = FlagSet.Empty;

            if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
            {
                if (affixGroup != null)
                {
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
                else if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
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
                        if (IntEx.TryParseInvariant(affixInput, slashPartOffset, slashPartLength, out aliasNumber) && aliasNumber > 0 && aliasNumber <= Builder.AliasF.Count)
                        {
                            contClass = Builder.AliasF[aliasNumber - 1];
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        contClass = ParseFlags(affixInput, slashPartOffset, slashPartLength);
                    }
                }
                else
                {
                    affixText = StringBuilderPool.Get(affixInput);
                }

                if (Builder.IgnoredChars != null && Builder.IgnoredChars.HasChars)
                {
                    affixText.RemoveChars(Builder.IgnoredChars);
                }

                if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
                {
                    affixText.Reverse();
                }

                if (affixText.Length == 1 && affixText[0] == '0')
                {
                    affixText.Clear();
                }

                // piece 5 - is the conditions descriptions
                var conditionText = lineMatchGroups[6].Value;
                if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
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
                            return false;
                        }
                    }
                    else
                    {
                        if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
                        {
                            morphAffixText = morphAffixText.Reverse();
                        }

                        morph = MorphSet.TakeArray(morphAffixText.SplitOnTabOrSpace());
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

                if (!Builder.HasContClass && contClass.HasFlags)
                {
                    Builder.HasContClass = true;
                }

                affixGroup.Entries.Add(AffixEntry.Create<TEntry>(strip, StringBuilderPool.GetStringAndReturn(affixText), conditions, morph, contClass));

                return true;
            }
            else
            {
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
                        Warnings.Add($"Failure checking {nameof(RedundantConditionPrefix)} .");
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
                        Warnings.Add($"Failure checking {nameof(RedundantConditionPrefix)} .");
                        return false;
                    }

                    if (neg == @in)
                    {
                        Warnings.Add($"Failure checking {nameof(RedundantConditionPrefix)} .");
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
                        Warnings.Add($"Failure checking {nameof(RedundantConditionSuffix)} .");
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
                        Warnings.Add($"Failure checking {nameof(RedundantConditionSuffix)} .");
                        return false;
                    }

                    var neg = j < lastConditionIndex && conditions[j + 1] == '^';
                    if (neg == @in)
                    {
                        Warnings.Add($"Failure checking {nameof(RedundantConditionSuffix)} .");
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

        private bool TryParseReplacements(string parameterText, List<SingleReplacementEntry> entries)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 0)
            {
                return false;
            }

            var patternBuilder = StringBuilderPool.Get(parameters[0]);
            var outString = parameters.Length > 1 ? parameters[1] : string.Empty;

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

            entries.Add(new SingleReplacementEntry(StringBuilderPool.GetStringAndReturn(patternBuilder), outString.Replace('_', ' '), type));

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(string parameterText, List<PatternEntry> entries)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 0)
            {
                return false;
            }

            var pattern = parameters[0];
            string pattern2 = null;
            string pattern3 = null;
            var condition = default(FlagValue);
            var condition2 = default(FlagValue);

            var slashIndex = pattern.IndexOf('/');
            if (slashIndex >= 0)
            {
                if (!TryParseFlag(pattern, slashIndex + 1, out condition))
                {
                    return false;
                }

                pattern = pattern.Substring(0, slashIndex);
            }

            if (parameters.Length >= 2)
            {
                pattern2 = parameters[1];
                slashIndex = pattern2.IndexOf('/');
                if (slashIndex >= 0)
                {
                    if (!TryParseFlag(pattern2, slashIndex + 1, out condition2))
                    {
                        return false;
                    }

                    pattern2 = pattern2.Substring(0, slashIndex);
                }

                if (parameters.Length >= 3)
                {
                    pattern3 = parameters[2];
                    Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);
                }
            }

            entries.Add(new PatternEntry(pattern, pattern2, pattern3, condition, condition2));

            return true;
        }

        private bool TrySetFlagMode(string modeText)
        {
            if (string.IsNullOrEmpty(modeText))
            {
                return false;
            }

            FlagMode mode;
            if (FlagModeParameterMappings.TryGetValue(modeText, out mode))
            {
                if (mode == Builder.FlagMode)
                {
                    Warnings.Add($"Redundant {nameof(Builder.FlagMode)}: {modeText}");
                    return false;
                }

                Builder.FlagMode = mode;
                return true;
            }
            else
            {
                Warnings.Add($"Unknown {nameof(FlagMode)}: {modeText}");
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

            return Encoding.UTF8.GetString(encoding.GetBytes(decoded));
        }

        private FlagSet ParseFlags(string text) => FlagSet.TakeArray(ParseFlagsInOrder(text));

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

        private FlagSet ParseFlags(string text, int startIndex, int length) => FlagSet.TakeArray(ParseFlagsInOrder(text, startIndex, length));

        private FlagValue[] ParseFlagsInOrder(string text, int startIndex, int length)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.ParseFlagsInOrder(ReDecodeConvertedStringAsUtf8(text.Substring(startIndex, length)), FlagMode.Char)
                : FlagValue.ParseFlagsInOrder(text, startIndex, length, flagMode);
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

        private bool TryParseFlag(string text, int startIndex, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text.Substring(startIndex)), FlagMode.Char, out value)
                : FlagValue.TryParseFlag(text, startIndex, text.Length - startIndex, flagMode, out value);
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
    }
}
