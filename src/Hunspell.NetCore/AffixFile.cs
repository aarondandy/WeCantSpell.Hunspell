using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
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

        internal int warn;

        internal int subStandard;

        /// <summary>
        /// Indicates agglutinative languages with right-to-left writing system.
        /// </summary>
        /// <remarks>
        /// Set twofold prefix stripping (but single suffix stripping) eg. for morphologically complex
        /// languages with right-to-left writing system.
        /// </remarks>
        public bool ComplexPrefixes { get; set; }

        /// <summary>
        /// Allow twofold suffixes within compounds.
        /// </summary>
        public bool CompoundMoreSuffixes { get; set; }

        public bool CheckCompoundDup { get; set; }

        /// <summary>
        /// Forbid compounding, if the (usually bad) compound word may be
        /// a non compound word with a REP fault.Useful for languages with
        /// 'compound friendly' orthography.
        /// </summary>
        public bool CheckCompoundRep { get; set; }

        public bool CheckCompoundTriple { get; set; }

        public bool SimplifiedTriple { get; set; }

        public bool CheckCompoundCase { get; set; }

        /// <summary>
        /// A flag used by the controlled compound words.
        /// </summary>
        public bool CheckNum { get; set; }

        /// <summary>
        /// Remove all bad n-gram suggestions (default mode keeps one).
        /// </summary>
        /// <seealso cref="MaxDifferency"/>
        public bool OnlyMaxDiff { get; set; }

        /// <summary>
        /// Disable word suggestions with spaces.
        /// </summary>
        public bool NoSplitSuggestions { get; set; }

        public bool FullStrip { get; set; }

        /// <summary>
        /// Add dot(s) to suggestions, if input word terminates in dot(s).
        /// </summary>
        /// <remarks>
        /// Not for LibreOffice dictionaries, because LibreOffice
        /// has an automatic dot expansion mechanism.
        /// </remarks>
        public bool SuggestWithDots { get; set; }

        /// <summary>
        /// Words with flag WARN aren't accepted by the spell checker using this parameter.
        /// </summary>
        public bool ForbidWarn { get; set; }

        public bool CheckSharps { get; set; }

        /// <summary>
        /// A string of text representing a keyboard layout.
        /// </summary>
        /// <remarks>
        /// Hunspell searches and suggests words with one different
        /// character replaced by a neighbor KEY character. Not neighbor
        /// characters in KEY string separated by vertical line characters.
        /// </remarks>
        public string KeyString { get; set; }

        /// <summary>
        /// Characters used to permit some suggestions.
        /// </summary>
        /// <remarks>
        /// Hunspell can suggest right word forms, when they differ from the
        /// bad input word by one TRY character.The parameter of TRY is case sensitive.
        /// </remarks>
        public string TryString { get; set; }

        /// <summary>
        /// The language code used for language specific functions.
        /// </summary>
        /// <remarks>
        /// Use this to activate special casing of Azeri(LANG az) and Turkish(LANG tr).
        /// </remarks>
        public string Language { get; set; } = CultureInfo.InvariantCulture.Name;

        /// <summary>
        /// The culture associated with the language.
        /// </summary>
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

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

        /// <summary>
        /// Flag indicating that a word should not be used as a suggestion.
        /// </summary>
        /// <remarks>
        /// Words signed with NOSUGGEST flag are not suggested (but still accepted when
        /// typed correctly). Proposed flag
        /// for vulgar and obscene words(see also SUBSTANDARD).
        /// </remarks>
        /// <seealso cref="SubStandard"/>
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
        /// Ignored characters (for example, Arabic optional diacretics characters)
        /// for dictionary words, affixes and input words.
        /// </summary>
        /// <remarks>
        /// Useful for optional characters, as Arabic (harakat) or Hebrew (niqqud) diacritical marks (see
        /// tests/ignore.* test dictionary in Hunspell distribution).
        /// </remarks>
        public char[] IgnoredChars { get; set; }

        /// <summary>
        /// Maximum number of n-gram suggestions. A value of 0 switches off the n-gram suggestions.
        /// </summary>
        /// <seealso cref="MaxDifferency"/>
        public int MaxNgramSuggestions
        {
            get { return maxNgramSuggestions; }
            set { maxNgramSuggestions = value; }
        }

        /// <summary>
        /// Similarity factor for the n-gram based suggestions.
        /// </summary>
        /// <remarks>
        /// Set the similarity factor for the n-gram based suggestions (5 = default value; 0 = fewer n-gram suggestions, but min. 1;
        /// 10 = MAXNGRAMSUGS (<see cref="MaxNgramSuggestions"/>) n-gram suggestions).
        /// </remarks>
        /// <seealso cref="MaxNgramSuggestions"/>
        public int MaxDifferency
        {
            get { return maxDifferency; }
            set { maxDifferency = value; }
        }

        /// <summary>
        /// Maximum number of suggested compound words generated by compound rule.
        /// </summary>
        /// <remarks>
        /// Set max. number of suggested compound words generated by compound rules. The
        /// number of the suggested compound words may be greater from the same 1-character
        /// distance type.
        /// </remarks>
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

        public int Warn
        {
            get { return warn; }
            set { warn = value; }
        }

        public int SubStandard
        {
            get { return subStandard; }
            set { subStandard = value; }
        }

        /// <summary>
        /// The encoding name to be used in morpheme, affix, and dictionary files.
        /// </summary>
        public string RequestedEncoding { get; set; }

        /// <summary>
        /// Specifies modifications to try first
        /// </summary>
        /// <remarks>
        /// This table specifies modifications to try first.
        /// First REP is the header of this table and one or more REP data
        /// line are following it.
        /// With this table, Hunspell can suggest the right forms for the typical
        /// spelling mistakes when the incorrect form differs by more
        /// than 1 letter from the right form.
        /// The search string supports the regex boundary signs (^ and $).
        /// 
        /// It's very useful to define replacements for the most typical one-character mistakes, too:
        /// with REP you can add higher priority to a subset of the TRY suggestions(suggestion list
        /// begins with the REP suggestions).
        /// 
        /// Suggesting separated words, specify spaces with underlines.
        /// 
        /// Replacement table can be used for a stricter compound word checking with the option CHECKCOMPOUNDREP (<see cref="CheckCompoundRep"/>).
        /// </remarks>
        /// <seealso cref="CheckCompoundRep"/>
        public List<ReplacementEntry> Replacements { get; set; }

        public List<AffixEntryGroup<SuffixEntry>> Suffixes { get; set; }

        public List<AffixEntryGroup<PrefixEntry>> Prefixes { get; set; }

        /// <summary>
        /// Ordinal numbers for affix flag compression.
        /// </summary>
        /// <remarks>
        /// Hunspell can substitute affix flag sets with ordinal numbers in affix rules(alias compression, see makealias tool).
        /// 
        /// If affix file contains the FLAG parameter, define it before the AF definitions.
        /// 
        /// Use makealias utility in Hunspell distribution to compress aff and dic files.
        /// </remarks>
        public List<ImmutableList<int>> AliasF { get; set; }

        /// <summary>
        /// Inidicates if any <see cref="AliasF"/> entries have been defined.
        /// </summary>
        public bool IsAliasF => AliasF != null && AliasF.Count > 0;

        /// <summary>
        /// Values used for morphological alias compression.
        /// </summary>
        public List<string> AliasM { get; set; }

        /// <summary>
        /// Indicates if any <see cref="AliasM"/> entries have been defined.
        /// </summary>
        public bool IsAliasM => AliasM != null && AliasM.Count > 0;

        public List<CompoundRule> CompoundRules { get; set; }

        public List<PatternEntry> CompoundPatterns { get; set; }

        public bool SimplifiedCompound { get; set; }

        public List<string> BreakTable { get; set; }

        public SortedDictionary<string, ReplacementEntry> InputConversions { get; set; }

        public SortedDictionary<string, ReplacementEntry> OutputConversions { get; set; }

        /// <summary>
        /// Mappings between related characters.
        /// </summary>
        /// <remarks>
        /// We can define language-dependent information on characters and
        /// character sequences that should be considered related(i.e.nearer than
        /// other chars not in the set) in the affix file(.aff)  by a map table.
        /// With this table, Hunspell can suggest the right forms for words, which
        /// incorrectly choose the wrong letter or letter groups from a related
        /// set more than once in a word (see <see cref="Replacements"/>).
        /// </remarks>
        /// <seealso cref="Replacements"/>
        public List<MapEntry> MapTable { get; set; }

        public List<PhoneticEntry> Phone { get; set; }

        public int CompoundMaxSyllable { get; set; }

        public char[] CompoundVowels { get; set; }

        public string Version { get; set; } = string.Empty;

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
