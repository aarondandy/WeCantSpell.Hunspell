using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hunspell
{
    public class AffixFile
    {
        public AffixFile()
        {
            Prefixes = new List<AffixEntryGroup<PrefixEntry>>();
            Suffixes = new List<AffixEntryGroup<SuffixEntry>>();
        }

        internal int compoundWordMax;

        internal int compoundMin;

        internal char compoundFlag;

        internal int compoundBegin;

        internal int compoundMiddle;

        internal int compoundEnd;

        internal int compoundRoot;

        internal int compoundPermitFlag;

        internal int compoundForbidFlag;

        internal int noSuggest;

        internal int noNgramSuggest;

        internal char forbiddenWord;

        internal int lemmaPresent;

        internal int circumfix;

        internal int onlyInCompound;

        internal int needAffix;

        internal int maxNgramSuggestions;

        internal int maxDifferency;

        internal int maxCompoundSuggestions;

        internal int keepCase;

        internal int forceUpperCase;

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
        public string TryString { get; set; }

        /// <summary>
        /// The language for language specific codes.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The flag used by the controlled compound words.
        /// </summary>
        public char CompoundFlag
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

        public int CompoundPermitFlag
        {
            get { return compoundPermitFlag; }
            set { compoundPermitFlag = value; }
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
        public string CompoundSyllableNum { get; set; }

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
        public char ForbiddenWord
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
        public char[] WordChars { get; set; }

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics characters).
        /// </summary>
        public char[] IgnoredChars { get; set; }

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

        public List<AffixEntryGroup<SuffixEntry>> Suffixes { get; set; }

        public List<AffixEntryGroup<PrefixEntry>> Prefixes { get; set; }

        public List<ImmutableList<int>> AliasF { get; set; }

        public bool IsAliasF => AliasF != null && AliasF.Count > 0;

        public List<string> AliasM { get; set; }

        public bool IsAliasM => AliasM != null && AliasM.Count > 0;

        public List<CompoundRule> CompoundRules { get; set; }

        public List<PatternEntry> CompoundPatterns { get; set; }

        public bool SimplifiedCompound { get; set; }

        public static Task<AffixFile> ReadAsync(AffixUtfStreamLineReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var fileReader = new AffixFileReader(reader);
            return fileReader.GetOrReadAsync();
        }

        public bool TrySetOption(string name, bool value)
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
    }
}
