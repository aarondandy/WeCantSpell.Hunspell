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
    public class AffixFileReader : IDisposable
    {
        public AffixFileReader(IAffixFileLineReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            Reader = reader;
        }

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$");
        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$");
        private static readonly Regex CommentLineRegex = new Regex(@"^\s*[#].*");
        private static readonly Regex AffixLineRegex = new Regex(
            @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].+)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public FlagMode FlagMode { get; private set; } = FlagMode.Char;

        public IAffixFileLineReader Reader { get; }

        private AffixFile Result { get; set; }

        private bool hasInitializedReplacements = false;

        private bool hasInitializedCompoundRules = false;

        private bool hasInitializedCompoundPatterns = false;

        private bool hasInitializedAliasF = false;

        private bool hasInitializedAliasM = false;

        private bool hasInitializedBreak = false;

        private bool hasInitializedIconv = false;

        private bool hasInitializedOconv = false;

        private bool hasInitializedMap = false;

        private bool hasInitializedPhone = false;

        private bool ownsReaderLifetime = true;

        private bool attemptDisposeWhenDone = true;

        /// <summary>
        /// This method may be called multiple times.
        /// </summary>
        /// <returns>An object representing the affix file.</returns>
        public async Task<AffixFile> GetOrReadAsync()
        {
            return Result ?? (Result = await ReadAsync());
        }

        private async Task<AffixFile> ReadAsync()
        {
            var result = new AffixFile();

            string line;
            while (null != (line = await Reader.ReadLineAsync()))
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (CommentLineRegex.IsMatch(line))
                {
                    continue;
                }

                var singleCommandParsed = SingleCommandParseRegex.Match(line);
                if (
                    singleCommandParsed.Success
                    && result.TrySetOption(singleCommandParsed.Groups[1].Value, true))
                {
                    continue;
                }

                var multiPartCommandParsed = LineStringParseRegex.Match(line);
                if (
                    multiPartCommandParsed.Success
                    && TryHandleTwoPartCommand(result, multiPartCommandParsed.Groups[1].Value, multiPartCommandParsed.Groups[2].Value))
                {
                    continue;
                }
            }

            if (attemptDisposeWhenDone)
            {
                Dispose();
            }

            return result;
        }


        private bool TryHandleTwoPartCommand(AffixFile affixFile, string name, string parameters)
        {
            if (name == null || parameters == null)
            {
                return false;
            }

            name = name.Trim();

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            switch (CultureInfo.InvariantCulture.TextInfo.ToUpper(name))
            {
                case "FLAG": // parse in the try string
                    return SetFlagMode(parameters);
                case "KEY": // parse in the keyboard string
                    affixFile.KeyString = parameters;
                    return true;
                case "TRY": // parse in the try string
                    affixFile.TryString = parameters;
                    return true;
                case "SET": // parse in the name of the character set used by the .dict and .aff
                    affixFile.RequestedEncoding = parameters;
                    return true;
                case "LANG": // parse in the language for language specific codes
                    affixFile.Language = parameters;
                    throw new NotImplementedException("May need to extract langnum");
                case "SYLLABLENUM": // parse in the flag used by compound_check() method
                    affixFile.CompoundSyllableNum = parameters;
                    return true;
                case "WORDCHARS": // parse in the extra word characters
                    affixFile.WordChars = parameters.ToCharArray();
                    return true;
                case "IGNORE": // parse in the ignored characters (for example, Arabic optional diacretics characters)
                    affixFile.IgnoredChars = parameters.ToCharArray();
                    return true;
                case "COMPOUNDFLAG": // parse in the flag used by the controlled compound words
                    return TryParseFlag(parameters, out affixFile.compoundFlag);
                case "COMPOUNDMIDDLE": // parse in the flag used by compound words
                    return TryParseFlag(parameters, out affixFile.compoundMiddle);
                case "COMPOUNDBEGIN": // parse in the flag used by compound words
                    return affixFile.ComplexPrefixes
                        ? TryParseFlag(parameters, out affixFile.compoundEnd)
                        : TryParseFlag(parameters, out affixFile.compoundBegin);
                case "COMPOUNDEND": // parse in the flag used by compound words
                    return affixFile.ComplexPrefixes
                        ? TryParseFlag(parameters, out affixFile.compoundBegin)
                        : TryParseFlag(parameters, out affixFile.compoundEnd);
                case "COMPOUNDWORDMAX": // parse in the data used by compound_check() method
                    return IntExtensions.TryParseInvariant(parameters, out affixFile.compoundWordMax);
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    return IntExtensions.TryParseInvariant(parameters, out affixFile.compoundMin);
                case "COMPOUNDROOT": // parse in the flag sign compounds in dictionary
                    return TryParseFlag(parameters, out affixFile.compoundRoot);
                case "COMPOUNDPERMITFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out affixFile.compoundPermitFlag);
                case "COMPOUNDFORBIDFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out affixFile.compoundForbidFlag);
                case "COMPOUNDSYLLABLE": // parse in the max. words and syllables in compounds
                    throw new NotImplementedException();
                case "NOSUGGEST":
                    return TryParseFlag(parameters, out affixFile.noSuggest);
                case "NONGRAMSUGGEST":
                    return TryParseFlag(parameters, out affixFile.noNgramSuggest);
                case "FORBIDDENWORD": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out affixFile.forbiddenWord);
                case "LEMMA_PRESENT": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out affixFile.lemmaPresent);
                case "CIRCUMFIX": // parse in the flag used by circumfixes
                    return TryParseFlag(parameters, out affixFile.circumfix);
                case "ONLYINCOMPOUND": // parse in the flag used by fogemorphemes
                    return TryParseFlag(parameters, out affixFile.onlyInCompound);
                case "PSEUDOROOT": // parse in the flag used by `needaffixs'
                case "NEEDAFFIX": // parse in the flag used by `needaffixs'
                    return TryParseFlag(parameters, out affixFile.needAffix);
                case "REP": // parse in the typical fault correcting table
                    return TryParseReplacementEntryLineIntoReplacements(affixFile, parameters);
                case "ICONV": // parse in the input conversion table
                    return TryParseIconv(affixFile, parameters);
                case "OCONV": // parse in the input conversion table
                    return TryParseOconv(affixFile, parameters);
                case "PHONE": // parse in the phonetic conversion table
                    return TryParsePhone(affixFile, parameters);
                case "CHECKCOMPOUNDPATTERN": // parse in the checkcompoundpattern table
                    return TryParseCheckCompoundPatternIntoCompoundPatterns(affixFile, parameters);
                case "COMPOUNDRULE": // parse in the defcompound table
                    return TryParseCompoundRuleIntoList(affixFile, parameters);
                case "MAP": // parse in the related character map table
                    return TryParseMapEntry(affixFile, parameters);
                case "BREAK": // parse in the word breakpoints table
                    return TryParseBreak(affixFile, parameters);
                case "VERSION":
                    throw new NotImplementedException();
                case "MAXNGRAMSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out affixFile.maxNgramSuggestions);
                case "MAXDIFF":
                    return IntExtensions.TryParseInvariant(parameters, out affixFile.maxDifferency);
                case "MAXCPDSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out affixFile.maxCompoundSuggestions);
                case "KEEPCASE": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out affixFile.keepCase);
                case "FORCEUCASE":
                    return TryParseFlag(parameters, out affixFile.forceUpperCase);
                case "WARN":
                    return TryParseFlag(parameters, out affixFile.warn);
                case "SUBSTANDARD":
                    throw new NotImplementedException();
                case "PFX":
                    return TryParseAffixIntoList(affixFile, parameters, affixFile.Prefixes);
                case "SFX":
                    return TryParseAffixIntoList(affixFile, parameters, affixFile.Suffixes);
                case "AF":
                    return TryParseAliasF(affixFile, parameters);
                case "AM":
                    return TryParseAliasM(affixFile, parameters);
                default:
                    return false;
            }
        }

        private bool TryParsePhone(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedPhone || affixFile.Phone == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.Phone = new List<PhoneticEntry>(expectedSize);
                    return true;
                }
                else if (affixFile.Phone == null)
                {
                    affixFile.Phone = new List<PhoneticEntry>();
                }

                hasInitializedPhone = true;
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
            affixFile.Phone.Add(entry);
            return true;
        }

        private bool TryParseMapEntry(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedMap || affixFile.MapTable == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.MapTable = new List<MapEntry>(expectedSize);
                    return true;
                }
                else if (affixFile.MapTable == null)
                {
                    affixFile.MapTable = new List<MapEntry>();
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

            affixFile.MapTable.Add(entry);
            return true;
        }

        private bool TryParseIconv(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedIconv || affixFile.InputConversions == null)
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

            affixFile.InputConversions.Add(parts[0], parts[1]);

            return true;
        }

        private bool TryParseOconv(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedOconv || affixFile.OutputConversions == null)
            {
                affixFile.OutputConversions = new SortedDictionary<string, ReplacementEntry>();
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

            affixFile.OutputConversions.Add(parts[0], parts[1]);

            return true;
        }

        private bool TryParseBreak(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedBreak || affixFile.BreakTable == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.BreakTable = new List<string>(expectedSize);
                    return true;
                }
                else if (affixFile.BreakTable == null)
                {
                    affixFile.BreakTable = new List<string>();
                }

                hasInitializedBreak = true;
            }

            affixFile.BreakTable.Add(parameterText);
            return true;
        }

        private bool TryParseAliasF(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedAliasF || affixFile.AliasF == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.AliasF = new List<ImmutableList<int>>(expectedSize);
                    return true;
                }
                else if (affixFile.AliasF == null)
                {
                    affixFile.AliasF = new List<ImmutableList<int>>();
                }

                hasInitializedAliasF = true;
            }

            var flags = ImmutableList.CreateRange(DecodeFlags(parameterText).OrderBy(x => x));
            affixFile.AliasF.Add(flags);
            return true;
        }

        private bool TryParseAliasM(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedAliasM || affixFile.AliasM == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.AliasM = new List<string>(expectedSize);
                    return true;
                }
                else if (affixFile.AliasM == null)
                {
                    affixFile.AliasM = new List<string>();
                }

                hasInitializedAliasM = true;
            }

            var chunk = parameterText;
            if (affixFile.ComplexPrefixes)
            {
                chunk = chunk.Reverse();
            }

            affixFile.AliasM.Add(chunk);
            return true;
        }

        private bool TryParseCompoundRuleIntoList(AffixFile affixFile, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return false;
            }

            if (!hasInitializedCompoundRules || affixFile.CompoundRules == null)
            {
                int expectedSize;
                if (IntExtensions.TryParseInvariant(parameterText, out expectedSize) && expectedSize >= 0)
                {
                    affixFile.CompoundRules = new List<CompoundRule>(expectedSize);
                    return true;
                }
                else if (affixFile.CompoundRules == null)
                {
                    affixFile.CompoundRules = new List<CompoundRule>();
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

            affixFile.CompoundRules.Add(entry);
            return true;
        }

        private bool TryParseAffixIntoList<TEntry>(AffixFile affixFile, string parameterText, List<AffixEntryGroup<TEntry>> groups)
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
                if (affixFile.IsAliasM)
                {
                    options |= AffixEntryOptions.AliasM;
                }
                if (affixFile.IsAliasF)
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
                if (affixFile.ComplexPrefixes)
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

                    if (affixFile.IsAliasF)
                    {
                        int aliasNumber;
                        if (int.TryParse(slashPart, out aliasNumber) && aliasNumber > 0 && aliasNumber <= affixFile.AliasF.Count)
                        {
                            contClass = affixFile.AliasF[aliasNumber - 1];
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
                if (affixFile.IgnoredChars != null)
                {
                    affixText = affixText.RemoveChars(affixFile.IgnoredChars);
                }
                if (affixFile.ComplexPrefixes)
                {
                    affixText = affixText.Reverse();
                }

                if (affixText == "0")
                {
                    affixText = string.Empty;
                }

                // piece 5 - is the conditions descriptions
                var conditionText = lineMatchGroups[6].Value;
                if (affixFile.ComplexPrefixes)
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
                    if (affixFile.IsAliasM)
                    {
                        int morphNumber;
                        if (IntExtensions.TryParseInvariant(morph, out morphNumber) && morphNumber > 0 && morphNumber <= affixFile.AliasM.Count)
                        {
                            morph = affixFile.AliasM[morphNumber - 1];
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (affixFile.ComplexPrefixes)
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

        private bool TryParseReplacementEntryLineIntoReplacements(AffixFile affixFile, string parameterText)
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
                        affixFile.Replacements = new List<ReplacementEntry>(Math.Max(1, expectedCount));
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

            if (affixFile.Replacements == null)
            {
                affixFile.Replacements = new List<ReplacementEntry>();
                hasInitializedReplacements = true;
            }

            var replacement = new ReplacementEntry(pattern);
            replacement.OutStrings[type] = outString;
            affixFile.Replacements.Add(replacement);

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(AffixFile affixFile, string parameterText)
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
                        affixFile.CompoundPatterns = new List<PatternEntry>(Math.Max(1, expectedCount));
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
                    affixFile.SimplifiedCompound = true;
                }
            }

            if (affixFile.CompoundPatterns == null)
            {
                affixFile.CompoundPatterns = new List<PatternEntry>();
            }

            affixFile.CompoundPatterns.Add(patternEntry);

            return true;
        }

        private IEnumerable<int> DecodeFlags(string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return Enumerable.Empty<int>();
            }

            switch (FlagMode)
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

            switch (FlagMode)
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

            switch (FlagMode)
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

        private bool SetFlagMode(string modeText)
        {
            if (string.IsNullOrEmpty(modeText))
            {
                return false;
            }

            FlagMode newMode;
            if (modeText.ContainsOrdinalIgnoreCase("LONG"))
            {
                newMode = FlagMode.Long;
            }
            else if (modeText.ContainsOrdinalIgnoreCase("NUM"))
            {
                newMode = FlagMode.Num;
            }
            else if (modeText.ContainsOrdinalIgnoreCase("UTF"))
            {
                newMode = FlagMode.Uni;
            }
            else
            {
                return false;
            }

            if (newMode == FlagMode)
            {
                // TODO: warn
            }

            FlagMode = newMode;
            return true;
        }

        public void Dispose()
        {
            if (ownsReaderLifetime)
            {
                var disposableReader = Reader as IDisposable;
                if (disposableReader != null)
                {
                    disposableReader.Dispose();
                }
            }
        }
    }
}
