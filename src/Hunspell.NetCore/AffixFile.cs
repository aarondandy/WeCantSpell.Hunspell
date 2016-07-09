using Hunspell.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hunspell
{
    public class AffixFile
    {
        public AffixFile()
        {
            flagMode = FlagMode.Char;
        }

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$");
        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$");
        private static readonly Regex CommentLineRegex = new Regex(@"^\s*[#].*");

        private int compoundWordMax;

        private int compoundMin;

        private int compoundFlag;

        private int compoundBegin;

        private int compoundMiddle;

        private int compoundEnd;

        private int compoundRoot;

        private int compoundForbidFlag;

        private int noSuggest;

        private int noNgramSuggest;

        private int forbiddenWord;

        private int lemmaPresent;

        private int circumfix;

        private int onlyInCompound;

        private int needAffix;

        private int maxNgramSuggestions;

        private int maxDifferency;

        private int maxCompoundSuggestions;

        private int keepCase;

        private int forceUpperCase;

        private FlagMode flagMode;

        /// <summary>
        /// Indicates agglutinative languages with right-to-left writing system.
        /// </summary>
        public bool ComplexPrefixes { get; set; }

        public bool CompoundMoreSuffixes { get; set; }

        public bool CheckCompoundDup { get; set; }

        public bool CheckCompoundRep { get; set; }

        public bool CheckCompoundTriple { get; set; }

        public bool SimplifiedTriple { get; set; }

        public bool CheckCompoundCase { get; set; }

        /// <summary>
        /// A flag used by the controlled compound words.
        /// </summary>
        public bool CheckNum { get; set; }

        public bool OnlyMaxDiff { get; set; }

        public bool NoSplitSuggestions { get; set; }

        public bool FullStrip { get; set; }

        public bool SuggestWithDots { get; set; }

        public bool ForbidWarn { get; set; }

        public bool CheckSharps { get; set; }

        /// <summary>
        /// The keyboard string.
        /// </summary>
        public string KeyString { get; set; }

        /// <summary>
        /// The try string.
        /// </summary>
        public string TryString { get; private set; }

        /// <summary>
        /// The language for language specific codes.
        /// </summary>
        private string Language { get; set; }

        /// <summary>
        /// The flag used by the controlled compound words.
        /// </summary>
        public int CompoundFlag
        {
            get { return compoundFlag; }
            set { compoundFlag = value; }
        }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        public int CompoundBegin
        {
            get { return compoundBegin; }
            set { compoundBegin = value; }
        }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        public int CompoundEnd
        {
            get { return compoundEnd; }
            set { compoundEnd = value; }
        }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        public int CompoundMiddle
        {
            get { return compoundMiddle; }
            set { compoundMiddle = value; }
        }

        /// <summary>
        /// Used by compound check.
        /// </summary>
        public int CompoundWordMax
        {
            get { return compoundWordMax; }
            set { compoundWordMax = value; }
        }

        /// <summary>
        /// The minimal length for words in compounds.
        /// </summary>
        public int CompoundMin
        {
            get { return compoundMin; }
            set { compoundMin = value; }
        }

        /// <summary>
        /// The flag sign compounds in dictionary.
        /// </summary>
        public int CompoundRoot
        {
            get { return compoundRoot; }
            set { compoundRoot = value; }
        }

        /// <summary>
        /// Used in the compound check method.
        /// </summary>
        public int CompoundForbidFlag
        {
            get { return compoundForbidFlag; }
            set { compoundForbidFlag = value; }
        }

        /// <summary>
        /// A flag used by compound check.
        /// </summary>
        /// <remarks>
        /// It appears that this string is used as a boolean where <c>null</c> or <see cref="string.Empty"/> is <c>false</c>.
        /// </remarks>
        public string CpdSyllableNum { get; set; }

        public int NoSuggest
        {
            get { return noSuggest; }
            set { noSuggest = value; }
        }

        public int NoNgramSuggest
        {
            get { return noNgramSuggest; }
            set { noNgramSuggest = value; }
        }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        public int ForbiddenWord
        {
            get { return forbiddenWord; }
            set { forbiddenWord = value; }
        }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        public int LemmaPresent
        {
            get { return lemmaPresent; }
            set { lemmaPresent = value; }
        }

        /// <summary>
        /// A flag used by circumfixes.
        /// </summary>
        public int Circumfix
        {
            get { return circumfix; }
            set { circumfix = value; }
        }

        /// <summary>
        /// A flag used by fogemorphemes.
        /// </summary>
        public int OnlyInCompound
        {
            get { return onlyInCompound; }
            set { onlyInCompound = value; }
        }

        /// <summary>
        /// A flag used by needaffixs.
        /// </summary>
        public int NeedAffix
        {
            get { return needAffix; }
            set { needAffix = value; }
        }

        /// <summary>
        /// Extra word characters.
        /// </summary>
        public string WordChars { get; set; }

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics characters).
        /// </summary>
        public string IgnoredChars { get; set; }

        /// <summary>
        /// Maximum number of n-gram suggestions. A value of 0 switches off the n-gram suggestions.
        /// </summary>
        public int MaxNgramSuggestions
        {
            get { return maxNgramSuggestions; }
            set { maxNgramSuggestions = value; }
        }

        /// <summary>
        /// Differency limit for n-gram suggestions.
        /// </summary>
        public int MaxDifferency
        {
            get { return maxDifferency; }
            set { maxDifferency = value; }
        }

        /// <summary>
        /// Maximum number of suggested compound words generated by compound rule.
        /// </summary>
        public int MaxCompoundSuggestions
        {
            get { return maxCompoundSuggestions; }
            set { maxCompoundSuggestions = value; }
        }

        public int KeepCase
        {
            get { return keepCase; }
            set { keepCase = value; }
        }

        public int ForceUpperCase
        {
            get { return forceUpperCase; }
            set { forceUpperCase = value; }
        }

        public string RequestedEncoding { get; set; }

        public List<ReplacementEntry> Replacements { get; set; }

        public static async Task<AffixFile> ReadAsync(IAffixFileLineReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var result = new AffixFile();

            string line;
            while (null != (line = await reader.ReadLineAsync()))
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
                    && result.TryHandleTwoPartCommand(multiPartCommandParsed.Groups[1].Value, multiPartCommandParsed.Groups[2].Value))
                {
                    continue;
                }
            }

            return result;
        }

        private bool TrySetOption(string name, bool value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            switch (CultureInfo.InvariantCulture.TextInfo.ToUpper(name))
            {
                /* parse COMPLEXPREFIXES for agglutinative languages with right-to-left writing system */
                case "COMPLEXPREFIXES":
                    ComplexPrefixes = value;
                    return true;
                case "COMPOUNDMORESUFFIXES":
                    CompoundMoreSuffixes = value;
                    return true;
                case "CHECKCOMPOUNDDUP":
                    CheckCompoundDup = value;
                    return true;
                case "CHECKCOMPOUNDREP":
                    CheckCompoundRep = value;
                    return true;
                case "CHECKCOMPOUNDTRIPLE":
                    CheckCompoundTriple = value;
                    return true;
                case "SIMPLIFIEDTRIPLE":
                    SimplifiedTriple = value;
                    return true;
                case "CHECKCOMPOUNDCASE":
                    CheckCompoundCase = value;
                    return true;
                /* parse in the flag used by the controlled compound words */
                case "CHECKNUM":
                    CheckNum = value;
                    return true;
                case "ONLYMAXDIFF":
                    OnlyMaxDiff = value;
                    return true;
                case "NOSPLITSUGS":
                    NoSplitSuggestions = value;
                    return true;
                case "FULLSTRIP":
                    FullStrip = value;
                    return true;
                case "SUGSWITHDOTS":
                    SuggestWithDots = value;
                    return true;
                case "FORBIDWARN":
                    ForbidWarn = value;
                    return true;
                case "CHECKSHARPS":
                    CheckSharps = value;
                    return true;
                default:
                    return false;
            }
        }

        private bool TryHandleTwoPartCommand(string name, string parameters)
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
                case "KEY": // parse in the keyboard string
                    KeyString = parameters;
                    return true;
                case "TRY": // parse in the try string
                    TryString = parameters;
                    return true;
                case "SET": // parse in the name of the character set used by the .dict and .aff
                    RequestedEncoding = parameters;
                    return true;
                case "LANG": // parse in the language for language specific codes
                    Language = parameters;
                    return true;
                case "SYLLABLENUM": // parse in the flag used by compound_check() method
                    CpdSyllableNum = parameters;
                    return true;
                case "WORDCHARS": // parse in the extra word characters
                    WordChars = parameters;
                    return true;
                case "IGNORE": // parse in the ignored characters (for example, Arabic optional diacretics characters)
                    IgnoredChars = parameters;
                    return true;
                case "COMPOUNDFLAG": // parse in the flag used by the controlled compound words
                    return TryParseFlag(parameters, out compoundFlag);
                case "COMPOUNDMIDDLE": // parse in the flag used by compound words
                    return TryParseFlag(parameters, out compoundMiddle);
                case "COMPOUNDBEGIN": // parse in the flag used by compound words
                    return ComplexPrefixes
                        ? TryParseFlag(parameters, out compoundEnd)
                        : TryParseFlag(parameters, out compoundBegin);
                case "COMPOUNDEND": // parse in the flag used by compound words
                    return ComplexPrefixes
                        ? TryParseFlag(parameters, out compoundBegin)
                        : TryParseFlag(parameters, out compoundEnd);
                case "COMPOUNDWORDMAX": // parse in the data used by compound_check() method
                    return IntExtensions.TryParseInvariant(parameters, out compoundWordMax);
                case "COMPOUNDMIN": // parse in the minimal length for words in compounds
                    return IntExtensions.TryParseInvariant(parameters, out compoundMin);
                case "COMPOUNDROOT": // parse in the flag sign compounds in dictionary
                    return TryParseFlag(parameters, out compoundRoot);
                case "COMPOUNDFORBIDFLAG": // parse in the flag used by compound_check() method
                    return TryParseFlag(parameters, out compoundForbidFlag);
                case "COMPOUNDSYLLABLE": // parse in the max. words and syllables in compounds
                    throw new NotImplementedException();
                case "NOSUGGEST":
                    return TryParseFlag(parameters, out noSuggest);
                case "NONGRAMSUGGEST":
                    return TryParseFlag(parameters, out noNgramSuggest);
                case "FORBIDDENWORD": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out forbiddenWord);
                case "LEMMA_PRESENT": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out lemmaPresent);
                case "CIRCUMFIX": // parse in the flag used by circumfixes
                    return TryParseFlag(parameters, out circumfix);
                case "ONLYINCOMPOUND": // parse in the flag used by fogemorphemes
                    return TryParseFlag(parameters, out onlyInCompound);
                case "PSEUDOROOT": // parse in the flag used by `needaffixs'
                case "NEEDAFFIX": // parse in the flag used by `needaffixs'
                    return TryParseFlag(parameters, out needAffix);
                case "REP": // parse in the typical fault correcting table
                    return TryParseReplacementEntryLine(parameters);
                case "ICONV": // parse in the input conversion table
                    throw new NotImplementedException();
                case "OCONV": // parse in the input conversion table
                    throw new NotImplementedException();
                case "PHONE": // parse in the input conversion table
                    throw new NotImplementedException();
                case "CHECKCOMPOUNDPATTERN": // parse in the checkcompoundpattern table
                    throw new NotImplementedException();
                case "COMPOUNDRULE": // parse in the defcompound table
                    throw new NotImplementedException();
                case "MAP": // parse in the related character map table
                    throw new NotImplementedException();
                case "BREAK": // parse in the word breakpoints table
                    throw new NotImplementedException();
                case "VERSION":
                    throw new NotImplementedException();
                case "MAXNGRAMSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out maxNgramSuggestions);
                case "MAXDIFF":
                    return IntExtensions.TryParseInvariant(parameters, out maxDifferency);
                case "MAXCPDSUGS":
                    return IntExtensions.TryParseInvariant(parameters, out maxCompoundSuggestions);
                case "KEEPCASE": // parse in the flag used by forbidden words
                    return TryParseFlag(parameters, out keepCase);
                case "FORCEUCASE":
                    return TryParseFlag(parameters, out forceUpperCase);
                case "WARN":
                    throw new NotImplementedException();
                case "SUBSTANDARD":
                    throw new NotImplementedException();
                case "PFX":
                    throw new NotImplementedException();
                case "SFX":
                    throw new NotImplementedException();
                default:
                    return false;
            }
        }

        private bool TryParseReplacementEntryLine(string parameterText)
        {
            var parameters = parameterText.SplitOnTabOrSpace();
            if (parameters.Length == 1)
            {
                if (Replacements == null)
                {
                    int expectedCount;
                    if (IntExtensions.TryParseInvariant(parameters[0], out expectedCount))
                    {
                        Replacements = new List<ReplacementEntry>(Math.Max(1, expectedCount));
                    }

                    return true;
                }

                return false;
            }

            if (parameters.Length < 2)
            {
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

            if (Replacements == null)
            {
                Replacements = new List<ReplacementEntry>();
            }

            var replacement = new ReplacementEntry(pattern);
            replacement.OutStrings[type] = outString;
            Replacements.Add(replacement);

            return true;
        }

        private bool TryParseFlag(string text, out int result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = 0;
                return false;
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                    if(text.Length >= 1)
                    {
                        result = text[0];
                        return true;
                    }

                    break;
                case FlagMode.Long:
                    if(text.Length >= 2)
                    {
                        result = (text[0] << 8) | (byte)(text[1]);
                        return true;
                    }

                    break;
                case FlagMode.Num:
                    throw new NotImplementedException();
                case FlagMode.Uni:
                    throw new NotImplementedException();
            }

            result = 0;
            return false;
        }
    }
}
