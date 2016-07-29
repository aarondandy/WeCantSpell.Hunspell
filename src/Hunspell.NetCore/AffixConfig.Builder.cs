using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace Hunspell
{
    public partial class AffixConfig
    {
        public class Builder
        {
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
            internal int forbiddenWord;
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
            /// Various affix options.
            /// </summary>
            /// <seealso cref="AffixConfig.Options"/>
            public AffixConfigOptions Options { get; set; }

            /// <summary>
            /// The flag type.
            /// </summary>
            /// <seealso cref="AffixConfig.FlagMode"/>
            public FlagMode FlagMode { get; set; } = FlagMode.Char;

            /// <summary>
            /// A string of text representing a keyboard layout.
            /// </summary>
            /// <seealso cref="AffixConfig.KeyString"/>
            public string KeyString { get; set; }

            /// <summary>
            /// Characters used to permit some suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.TryString"/>
            public string TryString { get; set; }

            /// <summary>
            /// The language code used for language specific functions.
            /// </summary>
            /// <seealso cref="AffixConfig.Language"/>
            public string Language { get; set; }

            /// <summary>
            /// The culture associated with the language.
            /// </summary>
            /// <seealso cref="AffixConfig.Culture"/>
            public CultureInfo Culture { get; set; }

            /// <summary>
            /// Flag indicating that a word may be in compound words.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundFlag"/>
            public int CompoundFlag
            {
                get { return compoundFlag; }
                set { compoundFlag = value; }
            }

            /// <summary>
            /// A flag indicating that a word may be the first element in a compound word.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundBegin"/>
            public int CompoundBegin
            {
                get { return compoundBegin; }
                set { compoundBegin = value; }
            }

            /// <summary>
            /// A flag indicating that a word may be the last element in a compound word.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundEnd"/>
            public int CompoundEnd
            {
                get { return compoundEnd; }
                set { compoundEnd = value; }
            }

            /// <summary>
            /// A flag indicating that a word may be a middle element in a compound word.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundMiddle"/>
            public int CompoundMiddle
            {
                get { return compoundMiddle; }
                set { compoundMiddle = value; }
            }

            /// <summary>
            /// Maximum word count in a compound word.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundWordMax"/>
            public int CompoundWordMax
            {
                get { return compoundWordMax; }
                set { compoundWordMax = value; }
            }

            /// <summary>
            /// Minimum length of words used for compounding.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundMin"/>
            public int CompoundMin
            {
                get { return compoundMin; }
                set { compoundMin = value; }
            }

            /// <summary>
            /// A flag marking compounds as a compound root.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundRoot"/>
            public int CompoundRoot
            {
                get { return compoundRoot; }
                set { compoundRoot = value; }
            }

            /// <summary>
            /// A flag indicating that an affix may be inside of compounds.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundPermitFlag"/>
            public int CompoundPermitFlag
            {
                get { return compoundPermitFlag; }
                set { compoundPermitFlag = value; }
            }

            /// <summary>
            /// A flag forbidding a suffix from compounding.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundForbidFlag"/>
            public int CompoundForbidFlag
            {
                get { return compoundForbidFlag; }
                set { compoundForbidFlag = value; }
            }

            /// <summary>
            /// Flag indicating that a word should not be used as a suggestion.
            /// </summary>
            /// <seealso cref="AffixConfig.NoSuggest"/>
            public int NoSuggest
            {
                get { return noSuggest; }
                set { noSuggest = value; }
            }

            /// <summary>
            /// Flag indicating that a word should not be used in ngram based suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.NoNgramSuggest"/>
            public int NoNgramSuggest
            {
                get { return noNgramSuggest; }
                set { noNgramSuggest = value; }
            }

            /// <summary>
            /// A flag indicating a forbidden word form.
            /// </summary>
            /// <seealso cref="AffixConfig.ForbiddenWord"/>
            public int ForbiddenWord
            {
                get { return forbiddenWord; }
                set { forbiddenWord = value; }
            }

            /// <summary>
            /// A flag used by forbidden words.
            /// </summary>
            /// <seealso cref="AffixConfig.LemmaPresent"/>
            public int LemmaPresent
            {
                get { return lemmaPresent; }
                set { lemmaPresent = value; }
            }

            /// <summary>
            /// A flag indicating that affixes may be on a word when this word also has prefix with <see cref="Circumfix"/> flag and vice versa.
            /// </summary>
            /// <seealso cref="AffixConfig.Circumfix"/>
            public int Circumfix
            {
                get { return circumfix; }
                set { circumfix = value; }
            }

            /// <summary>
            /// A flag indicating that a suffix may be only inside of compounds.
            /// </summary>
            /// <seealso cref="AffixConfig.OnlyInCompound"/>
            public int OnlyInCompound
            {
                get { return onlyInCompound; }
                set { onlyInCompound = value; }
            }

            /// <summary>
            /// A flag signing virtual stems in the dictionary.
            /// </summary>
            /// <seealso cref="AffixConfig.NeedAffix"/>
            public int NeedAffix
            {
                get { return needAffix; }
                set { needAffix = value; }
            }

            /// <summary>
            /// Maximum number of n-gram suggestions. A value of 0 switches off the n-gram suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxNgramSuggestions"/>
            public int MaxNgramSuggestions
            {
                get { return maxNgramSuggestions; }
                set { maxNgramSuggestions = value; }
            }

            /// <summary>
            /// Similarity factor for the n-gram based suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxDifferency"/>
            public int MaxDifferency
            {
                get { return maxDifferency; }
                set { maxDifferency = value; }
            }

            /// <summary>
            /// Maximum number of suggested compound words generated by compound rule.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxCompoundSuggestions"/>
            public int MaxCompoundSuggestions
            {
                get { return maxCompoundSuggestions; }
                set { maxCompoundSuggestions = value; }
            }

            /// <summary>
            /// A flag indicating that uppercased and capitalized forms of words are forbidden.
            /// </summary>
            /// <seealso cref="AffixConfig.KeepCase"/>
            public int KeepCase
            {
                get { return keepCase; }
                set { keepCase = value; }
            }

            /// <summary>
            /// A flag forcing capitalization of the whole compound word.
            /// </summary>
            /// <seealso cref="AffixConfig.ForceUpperCase"/>
            public int ForceUpperCase
            {
                get { return forceUpperCase; }
                set { forceUpperCase = value; }
            }

            /// <summary>
            /// Flag indicating a rare word that is also often a spelling mistake.
            /// </summary>
            /// <seealso cref="AffixConfig.Warn"/>
            public int Warn
            {
                get { return warn; }
                set { warn = value; }
            }

            /// <summary>
            /// Flag signing affix rules and dictionary words not used in morphological generation.
            /// </summary>
            /// <seealso cref="AffixConfig.SubStandard"/>
            public int SubStandard
            {
                get { return subStandard; }
                set { subStandard = value; }
            }

            /// <summary>
            /// A flag used by compound check.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundSyllableNum"/>
            public string CompoundSyllableNum { get; set; }

            /// <summary>
            /// The encoding name to be used in morpheme, affix, and dictionary files.
            /// </summary>
            /// <seealso cref="AffixConfig.RequestedEncoding"/>
            public string RequestedEncoding { get; set; }

            /// <summary>
            /// Specifies modifications to try first
            /// </summary>
            /// <seealso cref="AffixConfig.Replacements"/>
            public List<ReplacementEntry> Replacements { get; set; }

            /// <summary>
            /// Suffixes attached to root words to make other words.
            /// </summary>
            /// <seealso cref="AffixConfig.Suffixes"/>
            public List<AffixEntryGroup.Builder<SuffixEntry>> Suffixes { get; set; }

            /// <summary>
            /// Preffixes attached to root words to make other words.
            /// </summary>
            /// <seealso cref="AffixConfig.Prefixes"/>
            public List<AffixEntryGroup.Builder<PrefixEntry>> Prefixes { get; set; }

            /// <summary>
            /// Ordinal numbers for affix flag compression.
            /// </summary>
            /// <seealso cref="AffixConfig.AliasF"/>
            public List<List<int>> AliasF { get; set; }

            /// <summary>
            /// Values used for morphological alias compression.
            /// </summary>
            /// <seealso cref="AffixConfig.AliasM"/>
            public List<string> AliasM { get; set; }

            /// <summary>
            /// Defines custom compound patterns with a regex-like syntax.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundRules"/>
            public List<List<int>> CompoundRules { get; set; }

            /// <summary>
            /// Forbid compounding, if the first word in the compound ends with endchars, and
            /// next word begins with beginchars and(optionally) they have the requested flags.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundPatterns"/>
            public List<PatternEntry> CompoundPatterns { get; set; }

            /// <seealso cref="AffixConfig.SimplifiedCompound"/>
            public bool SimplifiedCompound { get; set; }

            /// <summary>
            /// Defines new break points for breaking words and checking word parts separately.
            /// </summary>
            /// <seealso cref="AffixConfig.BreakTable"/>
            public List<string> BreakTable { get; set; }

            /// <summary>
            /// Input conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.InputConversions"/>
            public SortedDictionary<string, string[]> InputConversions { get; set; }

            /// <summary>
            /// Output conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.OutputConversions"/>
            public SortedDictionary<string, string[]> OutputConversions { get; set; }

            /// <summary>
            /// Mappings between related characters.
            /// </summary>
            /// <seealso cref="AffixConfig.MapTable"/>
            public List<List<string>> MapTable { get; set; }

            /// <summary>
            /// Phonetic transcription entries.
            /// </summary>
            /// <seealso cref="AffixConfig.Phone"/>
            public List<PhoneticEntry> Phone { get; set; }

            /// <summary>
            /// Maximum syllable number, that may be in a
            /// compound, if words in compounds are more than <see cref="CompoundWordMax"/>.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundMaxSyllable"/>
            public int CompoundMaxSyllable { get; set; }

            /// <summary>
            /// Voewls for calculating syllables.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundVowels"/>
            public char[] CompoundVowels { get; set; }

            /// <summary>
            /// Extra word characters.
            /// </summary>
            /// <seealso cref="AffixConfig.WordChars"/>
            public char[] WordChars { get; set; }

            /// <summary>
            /// Ignored characters (for example, Arabic optional diacretics characters)
            /// for dictionary words, affixes and input words.
            /// </summary>
            /// <seealso cref="AffixConfig.IgnoredChars"/>
            public char[] IgnoredChars { get; set; }

            /// <summary>
            /// Affix and dictionary file version string.
            /// </summary>
            /// <seealso cref="AffixConfig.Version"/>
            public string Version { get; set; }

            /// <summary>
            /// Constructs a <see cref="AffixConfig"/> based on the values set in the builder.
            /// </summary>
            /// <returns>A constructed affix config.</returns>
            /// <seealso cref="AffixConfig"/>
            public AffixConfig ToConfiguration()
            {
                return new AffixConfig
                {
                    Options = Options,
                    FlagMode = FlagMode,
                    KeyString = KeyString ?? string.Empty,
                    TryString = TryString ?? string.Empty,
                    Language = Language ?? CultureInfo.InvariantCulture.Name,
                    Culture = Culture ?? CultureInfo.InvariantCulture,
                    CompoundFlag = CompoundFlag,
                    CompoundBegin = CompoundBegin,
                    CompoundEnd = CompoundEnd,
                    CompoundMiddle = CompoundMiddle,
                    CompoundWordMax = CompoundWordMax,
                    CompoundMin = CompoundMin,
                    CompoundRoot = CompoundRoot,
                    CompoundPermitFlag = CompoundPermitFlag,
                    CompoundForbidFlag = CompoundForbidFlag,
                    NoSuggest = NoSuggest,
                    NoNgramSuggest = NoNgramSuggest,
                    ForbiddenWord = ForbiddenWord,
                    LemmaPresent = LemmaPresent,
                    Circumfix = Circumfix,
                    OnlyInCompound = OnlyInCompound,
                    NeedAffix = NeedAffix,
                    MaxNgramSuggestions = MaxNgramSuggestions,
                    MaxDifferency = MaxDifferency,
                    MaxCompoundSuggestions = MaxCompoundSuggestions,
                    KeepCase = KeepCase,
                    ForceUpperCase = ForceUpperCase,
                    Warn = Warn,
                    SubStandard = SubStandard,
                    CompoundSyllableNum = CompoundSyllableNum,
                    RequestedEncoding = RequestedEncoding,
                    Replacements = ToImmutableArray(Replacements),
                    Suffixes = EmptyIfNull(Suffixes).Select(b => b.ToGroup()).ToImmutableArray(),
                    Prefixes = EmptyIfNull(Prefixes).Select(b => b.ToGroup()).ToImmutableArray(),
                    AliasF = EmptyIfNull(AliasF).Select(ToImmutableArray).ToImmutableArray(),
                    AliasM = ToImmutableArray(AliasM),
                    CompoundRules = EmptyIfNull(CompoundRules).Select(ToImmutableArray).ToImmutableArray(),
                    CompoundPatterns = ToImmutableArray(CompoundPatterns),
                    BreakTable = ToImmutableArray(BreakTable),
                    InputConversions = EmptyIfNull(InputConversions).ToImmutableReplacementEntries(),
                    OutputConversions = EmptyIfNull(OutputConversions).ToImmutableReplacementEntries(),
                    MapTable = EmptyIfNull(MapTable).Select(ToImmutableArray).ToImmutableArray(),
                    Phone = ToImmutableArray(Phone),
                    CompoundMaxSyllable = CompoundMaxSyllable,
                    CompoundVowels = ToImmutableArray(CompoundVowels),
                    WordChars = ToImmutableArray(WordChars),
                    IgnoredChars = ToImmutableArray(IgnoredChars),
                    Version = Version
                };
            }

            /// <summary>
            /// Enables the given <paramref name="options"/> bits.
            /// </summary>
            /// <param name="options">Various bit options to enable.</param>
            public void EnableOptions(AffixConfigOptions options)
            {
                Options |= options;
            }

            private static ImmutableArray<T> ToImmutableArray<T>(IEnumerable<T> items)
            {
                return items == null ? ImmutableArray<T>.Empty : items.ToImmutableArray();
            }

            private static IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> items)
            {
                return items == null ? Enumerable.Empty<T>() : items;
            }

            private static SortedDictionary<K, V> EmptyIfNull<K, V>(SortedDictionary<K, V> items)
            {
                return items ?? new SortedDictionary<K, V>();
            }
        }
    }
}
