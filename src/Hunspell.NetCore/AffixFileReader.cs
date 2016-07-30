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

        private EntryListType Initialized { get; set; } = EntryListType.None;

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

        private bool IsInitialized(EntryListType flags)
        {
            return Initialized.HasFlag(flags);
        }

        private void SetInitialized(EntryListType flags)
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
                    return IntExtensions.TryParseInvariant(parameters, out Builder.CompoundWordMax);
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    if (!IntExtensions.TryParseInvariant(parameters, out Builder.CompoundMin))
                    {
                        return false;
                    }

                    if (Builder.CompoundMin < 1)
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
                    return TryParseFlag(parameters, out Builder.ForbiddenWord);
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
                    return TryParseStandardListItem(EntryListType.Map, parameters, ref Builder.MapTable, TryParseMapEntry);
                case "BREAK": // parse in the word breakpoints table
                    return TryParseStandardListItem(EntryListType.Break, parameters, ref Builder.BreakTable, TryParseBreak);
                case "VERSION":
                    Builder.Version = parameters;
                    return true;
                case "MAXNGRAMSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.MaxNgramSuggestions);
                case "MAXDIFF":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.MaxDifferency);
                case "MAXCPDSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out Builder.MaxCompoundSuggestions);
                case "KEEPCASE": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out Builder.KeepCase);
                case "FORCEUCASE":
                    return TryParseFlag(parameters, out Builder.ForceUpperCase);
                case "WARN":
                    return TryParseFlag(parameters, out Builder.Warn);
                case "SUBSTANDARD":
                    return TryParseFlag(parameters, out Builder.SubStandard);
                case "PFX":
                    return TryParseAffixIntoList(parameters, ref Builder.Prefixes);
                case "SFX":
                    return TryParseAffixIntoList(parameters, ref Builder.Suffixes);
                case "AF":
                    return TryParseStandardListItem(EntryListType.AliasF, parameters, ref Builder.AliasF, TryParseAliasF);
                case "AM":
                    return TryParseStandardListItem(EntryListType.AliasM, parameters, ref Builder.AliasM, TryParseAliasM);
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

        private bool TryParseStandardListItem<T>(EntryListType entryListType, string parameterText, ref List<T> entries, Func<string, List<T>, bool> parse)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!IsInitialized(entryListType) || entries == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    entries = new List<T>(expectedSize);
                    return true;
                }
                else if (entries == null)
                {
                    entries = new List<T>();
                }

                SetInitialized(entryListType);
            }

            return parse(parameterText, entries);
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

        private static bool TryParseMapEntry(string parameterText, List<List<string>> entries)
        {
            var entry = new List<string>();

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

            entries.Add(entry);

            return true;
        }

        private bool TryParseConv(string parameterText, EntryListType entryListType, ref Dictionary<string, string[]> entries)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!IsInitialized(entryListType) || entries == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    entries = new Dictionary<string, string[]>(expectedSize);
                    return true;
                }
                else if (entries == null)
                {
                    entries = new Dictionary<string, string[]>();
                }

                SetInitialized(entryListType);
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

        private bool TryParseAliasF(string parameterText, List<List<int>> entries)
        {
            entries.Add(DecodeFlags(parameterText).OrderBy(x => x).ToList());
            return true;
        }

        private bool TryParseAliasM(string parameterText, List<string> entries)
        {
            if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
            {
                parameterText = parameterText.Reverse();
            }

            entries.Add(parameterText);
            return true;
        }

        private bool TryParseCompoundRuleIntoList(string parameterText, List<List<int>> entries)
        {
            List<int> entry;

            if (parameterText.Contains('('))
            {
                entry = new List<int>();

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
                entry = DecodeFlags(parameterText).ToList();
            }

            entries.Add(entry);
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

            char characterFlag;
            if (!TryParseFlag(lineMatchGroups[1].Value, out characterFlag))
            {
                return false;
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = ImmutableArray<int>.Empty;

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
                if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
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
                            contClass = Builder.AliasF[aliasNumber - 1].ToImmutableArray();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        contClass = ImmutableArray.CreateRange(DecodeFlags(slashPart));
                    }
                }
                if (Builder.IgnoredChars != null)
                {
                    affixText = affixText.RemoveChars(Builder.IgnoredChars);
                }
                if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
                {
                    affixText = affixText.Reverse();
                }

                if (affixText == "0")
                {
                    affixText = string.Empty;
                }

                // piece 5 - is the conditions descriptions
                var conditionText = lineMatchGroups[6].Value;
                if (Builder.Options.HasFlag(AffixConfigOptions.ComplexPrefixes))
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
                        if (IntExtensions.TryParseInvariant(morph, out morphNumber) && morphNumber > 0 && morphNumber <= Builder.AliasM.Count)
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
                            morph = morph.Reverse();
                        }
                    }
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

                affixGroup.Entries.Add(AffixEntry.Create<TEntry>(strip, affixText, conditionText, morph, contClass));

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
                        Warnings.Add($"Failure checking {nameof(RedundantConditionPrefix)} .");
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
                        if (strip[i] == condition[j])
                        {
                            @in = true;
                        }
                    } while (j > 0 && condition[j] != '[');

                    if (j == 0 && condition[j] != '[')
                    {
                        Warnings.Add($"Failure checking {nameof(RedundantConditionSuffix)} .");
                        return false;
                    }

                    var neg = j < lastConditionIndex && condition[j + 1] == '^';
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

            var pattern = parameters[0];
            var outString = parameters.Length > 1 ? parameters[1] : string.Empty;

            ReplacementEntryType type;
            var trailingDollar = pattern.EndsWith('$');
            if (pattern.StartsWith('^'))
            {
                if (trailingDollar)
                {
                    type = ReplacementEntryType.Isol;
                    pattern = pattern.Substring(1).SubstringFromEnd(1);
                }
                else
                {
                    type = ReplacementEntryType.Ini;
                    pattern = pattern.Substring(1);
                }
            }
            else
            {
                if (trailingDollar)
                {
                    type = ReplacementEntryType.Fin;
                    pattern = pattern.SubstringFromEnd(1);
                }
                else
                {
                    type = ReplacementEntryType.Med;
                }
            }

            entries.Add(new SingleReplacementEntry(pattern.Replace('_', ' '), outString.Replace('_', ' '), type));

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(string parameterText, List<PatternEntry> entries)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 0)
            {
                return false;
            }

            string pattern = parameters[0];
            string pattern2 = null;
            string pattern3 = null;
            int condition = 0;
            int condition2 = 0;

            var slashIndex = pattern.IndexOf('/');
            if (slashIndex >= 0)
            {
                if (!TryParseFlag(pattern.Substring(slashIndex + 1), out condition))
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
                    if (!TryParseFlag(pattern2.Substring(slashIndex + 1), out condition2))
                    {
                        return false;
                    }

                    pattern2 = pattern2.Substring(0, slashIndex);
                }

                if (parameters.Length >= 3)
                {
                    pattern3 = parameters[2];
                    Builder.SimplifiedCompound = true;
                }
            }

            entries.Add(new PatternEntry(pattern, pattern2, pattern3, condition, condition2));

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
