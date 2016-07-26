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

        internal int compoundFlag;

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

        /// <summary>
        /// Forbid word duplication in compounds (e.g. foofoo).
        /// </summary>
        public bool CheckCompoundDup { get; set; }

        /// <summary>
        /// Forbid compounding if the compound word may be a non compound word with a REP fault.
        /// </summary>
        /// <remarks>
        /// Forbid compounding, if the (usually bad) compound word may be
        /// a non compound word with a REP fault. Useful for languages with
        /// 'compound friendly' orthography.
        /// </remarks>
        /// <seealso cref="Replacements"/>
        public bool CheckCompoundRep { get; set; }

        /// <summary>
        /// Forbid compounding if the compound word contains triple repeating letters.
        /// </summary>
        /// <remarks>
        /// Forbid compounding, if compound word contains triple repeating letters
        /// (e.g.foo|ox or xo|oof). Bug: missing multi-byte character support
        /// in UTF-8 encoding(works only for 7-bit ASCII characters).
        /// </remarks>
        public bool CheckCompoundTriple { get; set; }

        /// <summary>
        /// Allow simplified 2-letter forms of the compounds forbidden by <see cref="CheckCompoundTriple"/>.
        /// </summary>
        /// <remarks>
        /// It's useful for Swedish and Norwegian (and for
        /// the old German orthography: Schiff|fahrt -> Schiffahrt).
        /// </remarks>
        public bool SimplifiedTriple { get; set; }

        /// <summary>
        /// Forbid upper case characters at word boundaries in compounds.
        /// </summary>
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

        /// <summary>
        /// Indicates that affix rules can strip full words.
        /// </summary>
        /// <remarks>
        /// When active, affix rules can strip full words, not only one less characters, before
        /// adding the affixes, see fullstrip.* test files in the source distribution).
        /// Note: conditions may be word length without <see cref="FullStrip"/>, too.
        /// </remarks>
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
        /// When active, words marked with the <see cref="Warn"/> flag aren't accepted by the spell checker.
        /// </summary>
        /// <remarks>
        /// Words with flag <see cref="Warn"/> aren't accepted by the spell checker using this parameter.
        /// </remarks>
        public bool ForbidWarn { get; set; }

        /// <summary>
        /// Indicates SS letter pair in uppercased (German) words may be upper case sharp s (ß).
        /// </summary>
        /// <remarks>
        /// SS letter pair in uppercased (German) words may be upper case sharp s (ß).
        /// Hunspell can handle this special casing with the CHECKSHARPS
        /// declaration (see also KEEPCASE (<see cref="KeepCase"/>) flag and tests/germancompounding example)
        /// in both spelling and suggestion.
        /// </remarks>
        /// <seealso cref="KeepCase"/>
        public bool CheckSharps { get; set; }

        /// <summary>
        /// A string of text representing a keyboard layout.
        /// </summary>
        /// <remarks>
        /// Hunspell searches and suggests words with one different
        /// character replaced by a neighbor KEY character. Not neighbor
        /// characters in KEY string separated by vertical line characters.
        /// </remarks>
        /// <example>
        /// Suggested KEY parameters for QWERTY and Dvorak keyboard layouts:
        /// <code>
        /// KEY qwertyuiop|asdfghjkl|zxcvbnm
        /// KEY pyfgcrl|aeouidhtns|qjkxbmwvz
        /// </code>
        /// </example>
        /// <example>
        /// Using the first QWERTY layout, Hunspell suggests "nude" and
        /// "node" for "*nide". A character may have more neighbors, too:
        /// <code>
        /// KEY qwertzuop|yxcvbnm|qaw|say|wse|dsx|sy|edr|fdc|dx|rft|gfv|fc|tgz|hgb|gv|zhu|jhn|hb|uji|kjm|jn|iko|lkm
        /// </code>
        /// </example>
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
        /// Flag indicating that a word may be in compound words.
        /// </summary>
        /// <remarks>
        /// Words signed with this flag may be in compound words (except when
        /// word shorter than <see cref="CompoundMin"/>). Affixes with <see cref="CompoundFlag"/> also permits
        /// compounding of affixed words.
        /// </remarks>
        public int CompoundFlag
        {
            get { return compoundFlag; }
            set { compoundFlag = value; }
        }

        /// <summary>
        /// A flag indicating that a word may be the first element in a compound word.
        /// </summary>
        /// <remarks>
        /// Words signed with this flag (or with a signed affix) may
        /// be first elements in compound words.
        /// </remarks>
        public int CompoundBegin
        {
            get { return compoundBegin; }
            set { compoundBegin = value; }
        }

        /// <summary>
        /// A flag indicating that a word may be the last element in a compound word.
        /// </summary>
        /// <remarks>
        /// Words signed with this flag (or with a signed affix) may
        /// be last elements in compound words.
        /// </remarks>
        public int CompoundEnd
        {
            get { return compoundEnd; }
            set { compoundEnd = value; }
        }

        /// <summary>
        /// A flag indicating that a word may be a middle element in a compound word.
        /// </summary>
        /// <remarks>
        /// Words signed with this flag (or with a signed affix) may be middle elements in compound words.
        /// </remarks>
        public int CompoundMiddle
        {
            get { return compoundMiddle; }
            set { compoundMiddle = value; }
        }

        /// <summary>
        /// Maximum word count in a compound word.
        /// </summary>
        /// <remarks>
        /// Set maximum word count in a compound word. (Default is unlimited.)
        /// </remarks>
        public int CompoundWordMax
        {
            get { return compoundWordMax; }
            set { compoundWordMax = value; }
        }

        /// <summary>
        /// Minimum length of words used for compounding.
        /// </summary>
        /// <remarks>
        /// Default value is documented as 3 but may be 1.
        /// </remarks>
        public int CompoundMin
        {
            get { return compoundMin; }
            set { compoundMin = value; }
        }

        /// <summary>
        /// A flag marking compounds as a compound root.
        /// </summary>
        /// <remarks>
        /// This flag signs the compounds in the dictionary
        /// (Now it is used only in the Hungarian language specific code).
        /// </remarks>
        public int CompoundRoot
        {
            get { return compoundRoot; }
            set { compoundRoot = value; }
        }

        /// <summary>
        /// A flag indicating that an affix may be inside of compounds.
        /// </summary>
        /// <remarks>
        /// Prefixes are allowed at the beginning of compounds,
        /// suffixes are allowed at the end of compounds by default.
        /// Affixes with this flag may be inside of compounds.
        /// </remarks>
        public int CompoundPermitFlag
        {
            get { return compoundPermitFlag; }
            set { compoundPermitFlag = value; }
        }

        /// <summary>
        /// A flag forbidding a suffix from compounding.
        /// </summary>
        /// <remarks>
        /// Suffixes with this flag forbid compounding of the affixed word.
        /// </remarks>
        public int CompoundForbidFlag
        {
            get { return compoundForbidFlag; }
            set { compoundForbidFlag = value; }
        }

        /// <summary>
        /// A flag used by compound check.
        /// </summary>
        /// <remarks>
        /// Need for special compounding rules in Hungarian.
        /// It appears that this string is used as a boolean where <c>null</c> or <see cref="string.Empty"/> indicates <c>false</c>.
        /// </remarks>
        public string CompoundSyllableNum { get; set; }

        /// <summary>
        /// Flag indicating that a word should not be used as a suggestion.
        /// </summary>
        /// <remarks>
        /// Words signed with this flag flag are not suggested (but still accepted when
        /// typed correctly). Proposed flag
        /// for vulgar and obscene words(see also <see cref="SubStandard"/> ).
        /// </remarks>
        /// <seealso cref="SubStandard"/>
        public int NoSuggest
        {
            get { return noSuggest; }
            set { noSuggest = value; }
        }

        /// <summary>
        /// Flag indicating that a word should not be used in ngram based suggestions.
        /// </summary>
        /// <remarks>
        /// Similar to <see cref="NoSuggest"/>, but it forbids to use the word
	    /// in ngram based(more, than 1-character distance) suggestions.
        /// </remarks>
        /// <seealso cref="NoSuggest"/>
        public int NoNgramSuggest
        {
            get { return noNgramSuggest; }
            set { noNgramSuggest = value; }
        }

        /// <summary>
        /// A flag indicating a forbidden word form.
        /// </summary>
        /// <remarks>
        /// This flag signs forbidden word form. Because affixed forms
        /// are also forbidden, we can subtract a subset from set of
        /// the accepted affixed and compound words.
        /// Note: usefull to forbid erroneous words, generated by the compounding mechanism.
        /// </remarks>
        public char ForbiddenWord
        {
            get { return forbiddenWord; }
            set { forbiddenWord = value; }
        }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        /// <remarks>
        /// Deprecated. Use "st:" field instead.
        /// </remarks>
        public int LemmaPresent
        {
            get { return lemmaPresent; }
            set { lemmaPresent = value; }
        }

        /// <summary>
        /// A flag indicating that affixes may be on a word when this word also has prefix with <see cref="Circumfix"/> flag and vice versa.
        /// </summary>
        /// <remarks>
        /// Affixes signed with this flag may be on a word when this word also has a
        /// prefix with this flag and vice versa(see circumfix.* test files in the source distribution).
        /// </remarks>
        public int Circumfix
        {
            get { return circumfix; }
            set { circumfix = value; }
        }

        /// <summary>
        /// A flag indicating that a suffix may be only inside of compounds.
        /// </summary>
        /// <remarks>
        /// Suffixes signed with this flag may be only inside of compounds
        /// (Fuge-elements in German, fogemorphemes in Swedish).
        /// This flag works also with words(see tests/onlyincompound.*).
        /// Note: also valuable to flag compounding parts which are not correct as a word
        /// by itself.
        /// </remarks>
        public int OnlyInCompound
        {
            get { return onlyInCompound; }
            set { onlyInCompound = value; }
        }

        /// <summary>
        /// A flag signing virtual stems in the dictionary.
        /// </summary>
        /// <remarks>
        /// This flag signs virtual stems in the dictionary, words only valid when affixed.
        /// Except, if the dictionary word has a homonym or a zero affix.
        /// NEEDAFFIX works also with prefixes and prefix + suffix combinations
        /// (see tests/pseudoroot5.*). This should be used instead of the deprecated PSEUDOROOT flag.
        /// </remarks>
        public int NeedAffix
        {
            get { return needAffix; }
            set { needAffix = value; }
        }

        /// <summary>
        /// Extra word characters.
        /// </summary>
        /// <remarks>
        /// Extends tokenizer of Hunspell command line interface with
        /// additional word character.
        /// For example, dot, dash, n-dash, numbers, percent sign
        /// are word character in Hungarian.
        /// </remarks>
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
        /// 10 = <see cref="MaxNgramSuggestions"/> n-gram suggestions).
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

        /// <summary>
        /// A flag indicating that uppercased and capitalized forms of words are forbidden.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Forbid uppercased and capitalized forms of words 
        /// signed with this flag. Useful for special orthographies 
        /// (measurements and currency often keep their case in uppercased
        /// texts) and writing systems(e.g.keeping lower case of IPA characters).
        /// Also valuable for words erroneously written in the wrong case.
        /// </para>
        /// <para>
        /// Note: With <see cref="CheckSharps"/> declaration, words with sharp s and <see cref="KeepCase"/> 
        /// flag may be capitalized and uppercased, but uppercased forms of these
        /// words may not contain sharp s, only SS.See germancompounding
        /// example in the tests directory of the Hunspell distribution.
        /// </para>
        /// </remarks>
        public int KeepCase
        {
            get { return keepCase; }
            set { keepCase = value; }
        }

        /// <summary>
        /// A flag forcing capitalization of the whole compound word.
        /// </summary>
        /// <remarks>
        /// Last word part of a compound with this flag forces capitalization of the whole
        /// compound word.Eg.Dutch word "straat" (street) with <see cref="ForceUpperCase"/> will allowed only
        /// in capitalized compound forms, according to the Dutch spelling rules for proper
        /// names.
        /// </remarks>
        public int ForceUpperCase
        {
            get { return forceUpperCase; }
            set { forceUpperCase = value; }
        }

        /// <summary>
        /// Flag indicating a rare word that is also often a spelling mistake.
        /// </summary>
        /// <remarks>
        /// This flag is for rare words, wich are also often spelling mistakes,
        /// see option -r of command line Hunspell and <see cref="ForbidWarn"/> .
        /// </remarks>
        /// <seealso cref="ForbidWarn"/>
        public int Warn
        {
            get { return warn; }
            set { warn = value; }
        }

        /// <summary>
        /// Flag signing affix rules and dictionary words not used in morphological generation.
        /// </summary>
        /// <remarks>
        /// This flag signs affix rules and dictionary words (allomorphs)
        /// not used in morphological generation(and in suggestion in the
        /// future versions).
        /// </remarks>
        /// <seealso cref="NoSuggest"/>
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
        /// <para>
        /// This table specifies modifications to try first.
        /// First REP is the header of this table and one or more REP data
        /// line are following it.
        /// With this table, Hunspell can suggest the right forms for the typical
        /// spelling mistakes when the incorrect form differs by more
        /// than 1 letter from the right form.
        /// The search string supports the regex boundary signs (^ and $).
        /// </para>
        /// <para>
        /// It's very useful to define replacements for the most typical one-character mistakes, too:
        /// with REP you can add higher priority to a subset of the TRY suggestions(suggestion list
        /// begins with the REP suggestions).
        /// </para>
        /// <para>
        /// Replacement table can be used for a stricter compound word checking with the option CHECKCOMPOUNDREP (<see cref="CheckCompoundRep"/>).
        /// </para>
        /// </remarks>
        /// <examples>
        /// For example a possible English replacement table definition
        /// to handle misspelled consonants:
        /// <code>
        /// REP 5
        /// REP f ph
        /// REP ph f
        /// REP tion$ shun
        /// REP ^cooccurr co-occurr
        /// REP ^alot$ a_lot
        /// </code>
        /// </examples>
        /// <example>
        /// Suggesting separated words, specify spaces with underlines.
        /// <code>
        /// REP 1
        /// REP onetwothree one_two_three
        /// </code>
        /// </example>
        /// <seealso cref="CheckCompoundRep"/>
        public List<ReplacementEntry> Replacements { get; set; }

        /// <summary>
        /// Suffixes attached to root words to make other words.
        /// </summary>
        public List<AffixEntryGroup<SuffixEntry>> Suffixes { get; set; }

        /// <summary>
        /// Preffixes attached to root words to make other words.
        /// </summary>
        public List<AffixEntryGroup<PrefixEntry>> Prefixes { get; set; }

        /// <summary>
        /// Ordinal numbers for affix flag compression.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Hunspell can substitute affix flag sets with ordinal numbers in affix rules(alias compression, see makealias tool).
        /// </para>
        /// <para>
        /// If affix file contains the FLAG parameter, define it before the AF definitions.
        /// </para>
        /// <para>
        /// Use makealias utility in Hunspell distribution to compress aff and dic files.
        /// </para>
        /// </remarks>
        /// <example>
        /// First example with alias compression:
        /// <code>
        /// 3
        /// hello
        /// try/1
        /// work/2
        /// </code>
        /// AF definitions in the affix file:
        /// <code>
        /// AF 2
        /// AF A
        /// AF AB
        /// ...
        /// </code>
        /// </example>
        /// <example>
        /// It is equivalent of the following dic file:
        /// <code>
        /// 3
        /// hello
        /// try/A
        /// work/AB
        /// </code>
        /// </example>
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

        /// <summary>
        /// Defines custom compound patterns with a regex-like syntax.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Compound patterns consist compound flags,
        /// parentheses, star and question mark meta characters.A flag followed by a `*' matches
        /// a word sequence of 0 or more matches of words signed with this compound flag.
        /// A flag followed by a `?' matches a word sequence of
        /// 0 or 1 matches of a word signed with this compound flag.
        /// See tests/compound*.* examples.
        /// </para>
        /// <para>
        /// en_US dictionary of OpenOffice.org uses COMPOUNDRULE for ordinal number recognitio
        /// (1st, 2nd, 11th, 12th, 22nd, 112th, 1000122nd etc.).
        /// </para>
        /// <para>
        /// In the case of long and numerical flag types use only parenthesized 
        /// flags: (1500)*(2000)?
        /// </para>
        /// <para>
        /// <see cref="CompoundRule"/> flags work completely separately from the
        /// compounding mechanisme using <see cref="CompoundFlag"/>, <see cref="CompoundBegin"/> , etc.compound
        /// flags. (Use these flags on different enhtries for words).
        /// </para>
        /// </remarks>
        public List<CompoundRule> CompoundRules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Forbid compounding, if the first word in the compound ends with endchars, and
        /// next word begins with beginchars and(optionally) they have the requested flags.
        /// The optional replacement parameter allows simplified compound form.
        /// </para>
        /// <para>
        /// <see cref="CompoundMin"/>  doesn't work correctly with the compound word alternation,
        /// so it may need to set <see cref="CompoundMin"/> to lower value.
        /// </para>
        /// </remarks>
        /// <example>
        /// The special "endchars" pattern 0 (zero) limits the rule to the unmodified stems (stems
        /// and stems with zero affixes):
        /// <code>
        /// CHECKCOMPOUNDPATTERN 0/x /y
        /// </code>
        /// </example>
        public List<PatternEntry> CompoundPatterns { get; set; }

        public bool SimplifiedCompound { get; set; }

        /// <summary>
        /// Defines new break points for breaking words and checking word parts separately.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use <value>^</value> and <value>$</value> to delete characters at end and
        /// start of the word.Rationale: useful for compounding with joining character or strings (for example, hyphen in English and German or hyphen and n-dash in Hungarian). Dashes are often bad break points for tokenization, because compounds with
        /// dashes may contain not valid parts, too.) 
        /// </para>
        /// <para>
        /// COMPOUNDRULE (<see cref="CompoundRules"/>) is better for handling dashes and
        /// other compound joining characters or character strings.Use BREAK, if you
        /// want to check words with dashes or other joining characters and there is no time
        /// or possibility to describe precise compound rules with COMPOUNDRULE
        /// (COMPOUNDRULE handles only the suffixation of the last word part of a
        /// compound word).
        /// </para>
        /// <para>
        /// For command line spell checking of words with extra characters,
        /// set WORDCHARS parameters: WORDCHARS -\fB--\fR(see tests/break.*) example
        /// </para>
        /// </remarks>
        /// <example>
        /// With BREAK, Hunspell can check both side of these compounds, breaking the words at dashes and n-dashes
        /// <code>
        /// BREAK 2
        /// BREAK -
        /// BREAK \fB--\fR    # n-dash
        /// </code>
        /// </example>
        /// <example>
        /// Breaking are recursive, so foo-bar, bar-foo and foo-foo\fB--\fRbar-bar 
        /// would be valid compounds.
        /// Note: The default word break of Hunspell is equivalent of the following BREAK
        /// definition.
        /// <code>
        /// BREAK 3
        /// BREAK -
        /// BREAK ^-
        /// BREAK -$
        /// </code>
        /// </example>
        /// <example>
        /// Hunspell doesn't accept the "-word" and "word-" forms by this BREAK definition:
        /// <code>
        /// BREAK 1
        /// BREAK -
        /// </code>
        /// </example>
        /// <example>
        /// Switching off the default values:
        /// <code>
        /// BREAK 0
        /// </code>
        /// </example>
        /// <seealso cref="CompoundRules"/>
        public List<string> BreakTable { get; set; }

        /// <summary>
        /// Input conversion entries.
        /// </summary>
        /// <remarks>
        /// Useful to convert one type of quote to another one, or change ligature.
        /// </remarks>
        public SortedDictionary<string, ReplacementEntry> InputConversions { get; set; }

        /// <summary>
        /// Output conversion entries.
        /// </summary>
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
        /// <example>
        /// For example a possible mapping could be for the German
        /// umlauted ü versus the regular u; the word
        /// Frühstück really should be written with umlauted u's and not regular ones
        /// <code>
        /// MAP 1
        /// MAP uü
        /// </code>
        /// </example>
        /// <example>
        /// Use parenthesized groups for character sequences (eg. for
        /// composed Unicode characters):
        /// <code>
        /// MAP 3
        /// MAP ß(ss)  (character sequence)
        /// MAP ﬁ(fi)  ("fi" compatibility characters for Unicode fi ligature)
        /// MAP(ọ́)o(composed Unicode character: ó with bottom dot)
        /// </code>
        /// </example>
        /// <seealso cref="Replacements"/>
        public List<MapEntry> MapTable { get; set; }

        /// <summary>
        /// Phonetic transcription entries.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Uses a table-driven phonetic transcription
        /// algorithm borrowed from Aspell.It is useful for languages with not
        /// pronunciation based orthography. You can add a full
        /// alphabet conversion and other rules for conversion of
        /// special letter sequences.For detailed documentation see
        /// http://aspell.net/man-html/Phonetic-Code.html.
        /// </para>
        /// <para>
        /// Note: Multibyte UTF-8 characters have not worked with
        /// bracket expression yet. Dash expression has signed bytes and not
        /// UTF-8 characters yet.
        /// </para>
        /// </remarks>
        public List<PhoneticEntry> Phone { get; set; }

        /// <summary>
        /// Maximum syllable number, that may be in a
        /// compound, if words in compounds are more than <see cref="CompoundWordMax"/>.
        /// </summary>
        /// <seealso cref="CompoundVowels"/>
        public int CompoundMaxSyllable { get; set; }

        /// <summary>
        /// Voewls for calculating syllables.
        /// </summary>
        /// <seealso cref="CompoundMaxSyllable"/>
        public char[] CompoundVowels { get; set; }

        /// <summary>
        /// Affix and dictionary file version string.
        /// </summary>
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
