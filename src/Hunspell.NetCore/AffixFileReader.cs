using Hunspell.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hunspell
{
    public class AffixFileReader
    {
        private AffixFileReader(AffixConfig.Builder builder)
        {
            Builder = builder;
        }

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex CommentLineRegex = new Regex(@"^\s*[#].*", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex AffixLineRegex = new Regex(
            @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].+)?$",
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
            {"UTF", FlagMode.Uni}
        };

        private AffixConfig.Builder Builder { get; }

        private List<string> Warnings { get; } = new List<string>();

        private InitializationFlags Initialized { get; set; } = InitializationFlags.None;

        public static async Task<AffixConfig> ReadAsync(IAffixFileLineReader reader)
        {
            var builder = new AffixConfig.Builder();
            var readerInstance = new AffixFileReader(builder);

            string line;
            while (null != (line = await reader.ReadLineAsync()))
            {
                readerInstance.ParseLine(line);
            }

            return builder.ToConfiguration();
        }

        private bool ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }

            if (CommentLineRegex.IsMatch(line))
            {
                return false;
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

        private bool IsInitialized(InitializationFlags flags)
        {
            return Initialized.HasFlag(flags);
        }

        private void SetInitialized(InitializationFlags flags)
        {
            Initialized |= flags;
        }

        private bool TryHandleParameterizedCommand(string name, string parameters)
        {
            if (string.IsNullOrEmpty(name) || parameters == null)
            {
                return false;
            }

            switch (CultureInfo.InvariantCulture.TextInfo.ToUpper(name))
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
                    Builder.RequestedEncoding = parameters;
                    return true;
                case "LANG": // parse in the language for language specific codes
                    Builder.Language = parameters;
                    Builder.Culture = GetCultureFromLanguage(Builder.Language);
                    return true;
                case "SYLLABLENUM": // parse in the flag used by compound_check() method
                    Builder.CompoundSyllableNum = parameters;
                    return true;
                case "WORDCHARS": // parse in the extra word characters
                    Builder.WordChars = parameters.ToCharArray();
                    return true;
                case "IGNORE": // parse in the ignored characters (for example, Arabic optional diacretics characters)
                    Builder.IgnoredChars = parameters.ToCharArray();
                    return true;
                case "COMPOUNDFLAG": // parse in the flag used by the controlled compound words
                    return TryParseFlag(parameters, out Builder.compoundFlag);
                case "COMPOUNDMIDDLE": // parse in the flag used by compound words
                    return TryParseFlag(parameters, out Builder.compoundMiddle);
                case "COMPOUNDBEGIN": // parse in the flag used by compound words
                    return Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.compoundEnd)
                        : TryParseFlag(parameters, out Builder.compoundBegin);
                case "COMPOUNDEND": // parse in the flag used by compound words
                    return Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.compoundBegin)
                        : TryParseFlag(parameters, out Builder.compoundEnd);
                case "COMPOUNDWORDMAX": // parse in the data used by compound_check() method
                    return IntExtensions.TryParseInvariant(parameters, out Builder.compoundWordMax);
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    if (!IntExtensions.TryParseInvariant(parameters, out Builder.compoundMin))
                    {
                        return false;
                    }

                    if (Builder.compoundMin < 1)
                    {
                        Builder.compoundMin = 1;
                    }

                    return true;
                case "COMPOUNDROOT": // parse in the flag sign compounds in dictionary
                    return TryParseFlag(parameters, out Builder.compoundRoot);
                case "COMPOUNDPERMITFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out Builder.compoundPermitFlag);
                case "COMPOUNDFORBIDFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out Builder.compoundForbidFlag);
                case "COMPOUNDSYLLABLE": // parse in the max. words and syllables in compounds
                    return TryParseCompoundSyllable(parameters);
                case "NOSUGGEST":
                    return TryParseFlag(parameters, out Builder.noSuggest);
                case "NONGRAMSUGGEST":
                    return TryParseFlag(parameters, out Builder.noNgramSuggest);
                case "FORBIDDENWORD": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.forbiddenWord);
                case "LEMMA_PRESENT": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.lemmaPresent);
                case "CIRCUMFIX": // parse in the flag used by circumfixes
                    return TryParseFlag(parameters, out Builder.circumfix);
                case "ONLYINCOMPOUND": // parse in the flag used by fogemorphemes
                    return TryParseFlag(parameters, out Builder.onlyInCompound);
                case "PSEUDOROOT": // parse in the flag used by `needaffixs'
                case "NEEDAFFIX": // parse in the flag used by `needaffixs'
                    return TryParseFlag(parameters, out Builder.needAffix);
                case "REP": // parse in the typical fault correcting table
                    return TryParseReplacementEntryLineIntoReplacements(parameters);
                case "ICONV": // parse in the input conversion table
                    return TryParseIconv(parameters);
                case "OCONV": // parse in the input conversion table
                    return TryParseOconv(parameters);
                case "PHONE": // parse in the phonetic conversion table
                    return TryParsePhone(parameters);
                case "CHECKCOMPOUNDPATTERN": // parse in the checkcompoundpattern table
                    return TryParseCheckCompoundPatternIntoCompoundPatterns(parameters);
                case "COMPOUNDRULE": // parse in the defcompound table
                    return TryParseCompoundRuleIntoList(parameters);
                case "MAP": // parse in the related character map table
                    return TryParseMapEntry(parameters);
                case "BREAK": // parse in the word breakpoints table
                    return TryParseBreak(parameters);
                case "VERSION":
                    Builder.Version = parameters;
                    return true;
                case "MAXNGRAMSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.maxNgramSuggestions);
                case "MAXDIFF":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.maxDifferency);
                case "MAXCPDSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.maxCompoundSuggestions);
                case "KEEPCASE": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.keepCase);
                case "FORCEUCASE":
                    return TryParseFlag(parameters, out Builder.forceUpperCase);
                case "WARN":
                    return TryParseFlag(parameters, out Builder.warn);
                case "SUBSTANDARD":
                    return TryParseFlag(parameters, out Builder.subStandard);
                case "PFX":
                    return TryParseAffixIntoList(parameters, Builder.Prefixes);
                case "SFX":
                    return TryParseAffixIntoList(parameters, Builder.Suffixes);
                case "AF":
                    return TryParseAliasF(parameters);
                case "AM":
                    return TryParseAliasM(parameters);
                default:
                    return false;
            }
        }

        private bool TryParseCompoundSyllable(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
            {
                return false;
            }

            var parts = parameters.SplitOnTabOrSpace();

            if (parts.Length > 0)
            {
                int maxValue;
                if (IntExtensions.TryParseInvariant(parts[0], out maxValue))
                {
                    Builder.CompoundMaxSyllable = maxValue;
                }
                else
                {
                    return false;
                }
            }

            var compoundVowels = (parts.Length > 1 ? parts[1] : "AEIOUaeiou").ToCharArray();
            Array.Sort(compoundVowels);
            Builder.CompoundVowels = compoundVowels;

            return true;
        }

        private CultureInfo GetCultureFromLanguage(string language)
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

        private bool TryParsePhone(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (IsInitialized(InitializationFlags.Phone) || Builder.Phone == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.Phone = new List<PhoneticEntry>(expectedSize);
                    return true;
                }
                else if (Builder.Phone == null)
                {
                    Builder.Phone = new List<PhoneticEntry>();
                }

                SetInitialized(InitializationFlags.Phone);
            }

            var parts = parameterText.SplitOnTabOrSpace();

            string item1, item2;
            if (parts.Length >= 2)
            {
                item1 = parts[0];
                item2 = parts[1];
            }
            else if (parts.Length == 1)
            {
                item1 = parts[0];
                item2 = string.Empty;
            }
            else
            {
                return false;
            }

            item2 = item2.Replace("_", string.Empty);
            var entry = new PhoneticEntry(item1, item2);
            Builder.Phone.Add(entry);
            return true;
        }

        private bool TryParseMapEntry(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedMap || Builder.MapTable == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.MapTable = new List<MapEntry>(expectedSize);
                    return true;
                }
                else if (Builder.MapTable == null)
                {
                    Builder.MapTable = new List<MapEntry>();
                }

                hasInitializedMap = true;
            }

            var entry = new MapEntry();

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

                entry.Add(parameterText.Substring(chb, che - chb));
            }

            Builder.MapTable.Add(entry);
            return true;
        }

        private bool TryParseIconv(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedIconv || Builder.InputConversions == null)
            {
                affixFile.InputConversions = new SortedDictionary<string, ReplacementEntry>();
                hasInitializedIconv = true;

                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    return true;
                }
            }

            var parts = parameterText.SplitOnTabOrSpace();
            if (parts.Length < 2)
            {
                return false;
            }

            Builder.InputConversions.AddReplacementEntry(parts[0], parts[1]);

            return true;
        }

        private bool TryParseOconv(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedOconv || Builder.OutputConversions == null)
            {
                Builder.OutputConversions = new SortedDictionary<string, ReplacementEntry>();
                hasInitializedOconv = true;

                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    return true;
                }
            }

            var parts = parameterText.SplitOnTabOrSpace();
            if (parts.Length < 2)
            {
                return false;
            }

            Builder.OutputConversions.AddReplacementEntry(parts[0], parts[1]);

            return true;
        }

        private bool TryParseBreak(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedBreak || Builder.BreakTable == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.BreakTable = new List<string>(expectedSize);
                    return true;
                }
                else if (Builder.BreakTable == null)
                {
                    Builder.BreakTable = new List<string>();
                }

                hasInitializedBreak = true;
            }

            Builder.BreakTable.Add(parameterText);
            return true;
        }

        private bool TryParseAliasF(AffixConfig.Builder builder, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedAliasF || Builder.AliasF == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.AliasF = new List<ImmutableList<int>>(expectedSize);
                    return true;
                }
                else if (Builder.AliasF == null)
                {
                    Builder.AliasF = new List<ImmutableList<int>>();
                }

                hasInitializedAliasF = true;
            }

            var flags = ImmutableList.CreateRange(DecodeFlags(parameterText).OrderBy(x => x));
            Builder.AliasF.Add(flags);
            return true;
        }

        private bool TryParseAliasM(AffixConfig.Builder builder, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedAliasM || Builder.AliasM == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.AliasM = new List<string>(expectedSize);
                    return true;
                }
                else if (Builder.AliasM == null)
                {
                    Builder.AliasM = new List<string>();
                }

                hasInitializedAliasM = true;
            }

            var chunk = parameterText;
            if (Builder.ComplexPrefixes)
            {
                chunk = chunk.Reverse();
            }

            Builder.AliasM.Add(chunk);
            return true;
        }

        private bool TryParseCompoundRuleIntoList(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedCompoundRules || Builder.CompoundRules == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    Builder.CompoundRules = new List<CompoundRule>(expectedSize);
                    return true;
                }
                else if (Builder.CompoundRules == null)
                {
                    Builder.CompoundRules = new List<CompoundRule>();
                }

                hasInitializedCompoundRules = true;
            }

            var entry = new CompoundRule();

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
                        entry.Add(parameterText[indexBegin]);
                    }
                    else
                    {
                        entry.AddRange(DecodeFlags(parameterText.Substring(indexBegin, indexEnd - indexBegin)));
                    }
                }
            }
            else
            {
                entry.AddRange(DecodeFlags(parameterText));
            }

            Builder.CompoundRules.Add(entry);
            return true;
        }

        private bool TryParseAffixIntoList<TEntry>(string parameterText, List<AffixEntryGroup<TEntry>> groups)
            where TEntry : AffixEntry, new()
        {
            var lineMatch = AffixLineRegex.Match(parameterText);
            if (!lineMatch.Success)
            {
                return false;
            }

            var lineMatchGroups = lineMatch.Groups;

            char characterFlag;
            if (!TryParseFlag(lineMatchGroups[1].Value, out characterFlag))
            {
                return false;
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = ImmutableList<int>.Empty;

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
                IntExtensions.TryParseInvariant(lineMatchGroups[3].Value, out expectedEntryCount);

                affixGroup = new AffixEntryGroup<TEntry>(characterFlag, options, expectedEntryCount);
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
                if (Builder.ComplexPrefixes)
                {
                    strip = strip.Reverse();
                }

                // piece 4 - is affix string or 0 for null
                var affixText = lineMatchGroups[5].Value;
                var affixSlashIndex = affixText.IndexOf('/');
                if (affixSlashIndex >= 0)
                {
                    var slashPart = affixText.Substring(affixSlashIndex + 1);
                    affixText = affixText.Substring(0, affixSlashIndex);

                    if (Builder.IsAliasF)
                    {
                        int aliasNumber;
                        if (int.TryParse(slashPart, out aliasNumber) && aliasNumber > 0 && aliasNumber <= Builder.AliasF.Count)
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
                        contClass = ImmutableList.CreateRange(DecodeFlags(slashPart));
                    }
                }
                if (Builder.IgnoredChars != null)
                {
                    affixText = affixText.RemoveChars(Builder.IgnoredChars);
                }
                if (Builder.ComplexPrefixes)
                {
                    affixText = affixText.Reverse();
                }

                if (affixText == "0")
                {
                    affixText = string.Empty;
                }

                // piece 5 - is the conditions descriptions
                var conditionText = lineMatchGroups[6].Value;
                if (Builder.ComplexPrefixes)
                {
                    conditionText = conditionText.Reverse();
                    conditionText = ReverseCondition(conditionText);
                }

                if (!string.IsNullOrEmpty(strip) && conditionText != ".")
                {
                    bool isRedundant;
                    if (typeof(TEntry) == typeof(PrefixEntry))
                    {
                        isRedundant = RedundantConditionPrefix(strip, conditionText);
                    }
                    else if (typeof(TEntry) == typeof(SuffixEntry))
                    {
                        isRedundant = RedundantConditionSuffix(strip, conditionText);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    if (isRedundant)
                    {
                        conditionText = ".";
                    }
                }

                if (typeof(TEntry) == typeof(SuffixEntry))
                {
                    // TODO: reverse some stuff or do it in SuffixEntry somehow, or dont at all!
                    conditionText = conditionText.Reverse();
                    conditionText = ReverseCondition(conditionText);
                }

                // piece 6
                string morph = null;
                if (lineMatchGroups[7].Success)
                {
                    morph = lineMatchGroups[7].Value;
                    if (Builder.IsAliasM)
                    {
                        int morphNumber;
                        if (IntExtensions.TryParseInvariant(morph, out morphNumber) && morphNumber > 0 && morphNumber <= affixFile.AliasM.Count)
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
                        if (Builder.ComplexPrefixes)
                        {
                            morph = morph.Reverse();
                        }
                    }
                }

                if (affixGroup == null)
                {
                    affixGroup = new AffixEntryGroup<TEntry>(characterFlag, AffixEntryOptions.None, 0);
                }

                affixGroup.Entries.Add(new TEntry
                {
                    Strip = strip,
                    Append = affixText,
                    ConditionText = conditionText,
                    MorphCode = morph,
                    ContClass = contClass
                });

                return true;
            }
            else
            {
                return false;
            }
        }

        private string ReverseCondition(string conditionText)
        {
            if (string.IsNullOrEmpty(conditionText))
            {
                return conditionText;
            }

            bool neg = false;
            var chars = conditionText.ToCharArray();
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

            return new string(chars);
        }

        private bool RedundantConditionPrefix(string strip, string condition)
        {
            if (strip.StartsWith(condition))
            {
                return true;
            }

            var lastConditionIndex = condition.Length - 1;
            int i, j;
            for (i = 0, j = 0; i < strip.Length && j < condition.Length; i++, j++)
            {
                if (condition[j] == '[')
                {
                    if (condition[j] != strip[i])
                    {
                        // TODO: warn
                        return false;
                    }
                }
                else if (j < lastConditionIndex)
                {
                    var neg = condition[j + 1] == '^';
                    var @in = false;

                    do
                    {
                        j++;
                        if (strip[i] == condition[j])
                        {
                            @in = true;
                        }
                    } while (j < lastConditionIndex && condition[j] != ']');

                    if (j == lastConditionIndex && condition[j] != ']')
                    {
                        // TODO: warn
                        return false;
                    }

                    if (neg == @in)
                    {
                        // TODO: warn
                        return false;
                    }
                }
            }

            return j >= condition.Length;
        }

        private bool RedundantConditionSuffix(string strip, string condition)
        {
            if (strip.EndsWith(condition))
            {
                return true;
            }

            var lastConditionIndex = condition.Length - 1;
            int i, j;
            for (i = strip.Length - 1, j = condition.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (condition[j] != ']')
                {
                    if (condition[j] != strip[i])
                    {
                        // TODO: warn
                        return false;
                    }
                }
                else if (j > 0)
                {
                    var @in = false;
                    do
                    {
                        j--;
                        if (strip[i] == condition[j])
                        {
                            @in = true;
                        }
                    } while (j > 0 && condition[j] != '[');

                    if (j == 0 && condition[j] != '[')
                    {
                        // TODO: warn
                        return false;
                    }

                    var neg = j < lastConditionIndex && condition[j + 1] == '^';
                    if (neg == @in)
                    {
                        // TODO: warn
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

        private bool TryParseReplacementEntryLineIntoReplacements(string parameterText)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 0)
            {
                return false;
            }

            if (parameters.Length == 1)
            {
                if (!hasInitializedReplacements)
                {
                    int expectedCount;
                    if (IntExtensions.TryParseInvariant(parameters[0], out expectedCount) && expectedCount >= 0)
                    {
                        Builder.Replacements = new List<ReplacementEntry>(Math.Max(1, expectedCount));
                        hasInitializedReplacements = true;
                        return true;
                    }
                }

                return false;
            }

            var pattern = parameters[0].Replace('_', ' ');
            var outString = parameters[1].Replace('_', ' ');
            int type = 0;

            if (pattern.StartsWith('^'))
            {
                pattern = pattern.Substring(1);
                type = 1;
            }

            if (pattern.EndsWith('$'))
            {
                pattern = pattern.SubstringFromEnd(1);
                type |= 2;
            }

            if (Builder.Replacements == null)
            {
                Builder.Replacements = new List<ReplacementEntry>();
                hasInitializedReplacements = true;
            }

            var replacement = new ReplacementEntry(pattern);
            replacement.OutStrings[type] = outString;
            Builder.Replacements.Add(replacement);

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(string parameterText)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 0)
            {
                return false;
            }

            if (parameters.Length == 1)
            {
                if (!hasInitializedCompoundPatterns)
                {
                    int expectedCount;
                    if (IntExtensions.TryParseInvariant(parameters[0], out expectedCount) && expectedCount >= 0)
                    {
                        Builder.CompoundPatterns = new List<PatternEntry>(Math.Max(1, expectedCount));
                        hasInitializedCompoundPatterns = true;
                        return true;
                    }
                }
            }

            int flag;
            string chunk;
            var patternEntry = new PatternEntry
            {
                Pattern = parameters[0]
            };

            var slashIndex = patternEntry.Pattern.IndexOf('/');
            if (slashIndex >= 0)
            {
                chunk = patternEntry.Pattern.Substring(slashIndex + 1);
                patternEntry.Pattern = patternEntry.Pattern.Substring(0, slashIndex);
                if (TryParseFlag(chunk, out flag))
                {
                    patternEntry.Condition = flag;
                }
                else
                {
                    return false;
                }
            }

            if (parameters.Length >= 2)
            {
                patternEntry.Pattern2 = parameters[1];
                slashIndex = patternEntry.Pattern2.IndexOf('/');
                if (slashIndex >= 0)
                {
                    chunk = patternEntry.Pattern2.Substring(slashIndex + 1);
                    patternEntry.Pattern2 = patternEntry.Pattern2.Substring(0, slashIndex);
                    if (TryParseFlag(chunk, out flag))
                    {
                        patternEntry.Condition2 = flag;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (parameters.Length >= 3)
                {
                    patternEntry.Pattern3 = parameters[2];
                    Builder.SimplifiedCompound = true;
                }
            }

            if (Builder.CompoundPatterns == null)
            {
                Builder.CompoundPatterns = new List<PatternEntry>();
            }

            Builder.CompoundPatterns.Add(patternEntry);

            return true;
        }

        private IEnumerable<int> DecodeFlags(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return Enumerable.Empty<int>();
            }

            switch (Builder.FlagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    return parameterText.Select(c => (int)c);
                case FlagMode.Long:
                    return DecodeLongFlags(parameterText);
                case FlagMode.Num:
                    return DecodeNumFlags(parameterText);
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEnumerable<int> DecodeLongFlags(string text)
        {
            if (text == null)
            {
                yield break;
            }

            for (int i = 0; i < text.Length - 1; i += 2)
            {
                yield return unchecked((byte)text[i] << 8 | (byte)text[i + 1]);
            }

            if (text.Length % 2 == 1)
            {
                yield return text[text.Length - 1];
            }
        }

        private static IEnumerable<int> DecodeNumFlags(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<int>();
            }

            return text
                .SplitOnComma()
                .Select(textValue =>
                {
                    int intValue;
                    IntExtensions.TryParseInvariant(textValue, out intValue);
                    return intValue;
                });
        }

        private bool TryParseFlag(string text, out int result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = 0;
                return false;
            }

            switch (Builder.FlagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    if (text.Length >= 2)
                    {
                        result = MergeCharacterBytes(text[0], text[1]);
                        return true;
                    }

                    result = text[0];
                    return true;
                case FlagMode.Long:
                    if (text.Length >= 2)
                    {
                        result = unchecked(((byte)text[0] << 8) | (byte)text[1]);
                    }
                    else
                    {
                        result = text[0];
                    }

                    return true;
                case FlagMode.Num:
                    return IntExtensions.TryParseInvariant(text, out result);
                default:
                    throw new NotSupportedException();
            }
        }

        private bool TryParseFlag(string text, out char result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = default(char);
                return false;
            }

            switch (Builder.FlagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    if (text.Length >= 2)
                    {
                        result = (char)MergeCharacterBytes(text[0], text[1]);
                        return true;
                    }

                    result = text[0];
                    return true;
                case FlagMode.Long:
                    if (text.Length >= 2)
                    {
                        result = unchecked((char)(((byte)text[0] << 8) | (byte)text[1]));
                    }
                    else
                    {
                        result = text[0];
                    }

                    return true;
                case FlagMode.Num:
                    return IntExtensions.TryParseInvariantAsChar(text, out result);
                default:
                    throw new NotSupportedException();
            }
        }

        [Obsolete("This method may be able to be replaced by a simple left shift operation like long flags are")]
        private static ushort MergeCharacterBytes(char first, char second)
        {
            return BitConverter.ToUInt16(new byte[] { unchecked((byte)first), unchecked((byte)second) }, 0);
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

        [Flags]
        private enum InitializationFlags
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
