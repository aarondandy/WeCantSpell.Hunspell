using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace WeCantSpell.Hunspell;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0305 // Simplify collection initialization
#pragma warning disable IDE0028 // Simplify collection initialization

public partial class AffixConfig
{
    [DebuggerDisplay("Prefixes = {Prefixes}, Suffixes = {Suffixes}")]
    public sealed class Builder
    {
        public Builder()
        {
        }

        /// <summary>
        /// Various affix options.
        /// </summary>
        /// <seealso cref="AffixConfig.Options"/>
        public AffixConfigOptions Options { get; set; }

        /// <summary>
        /// The flag type.
        /// </summary>
        /// <seealso cref="AffixConfig.FlagMode"/>
        public FlagParsingMode FlagMode { get; set; } = FlagParsingMode.Char;

        /// <summary>
        /// A string of text representing a keyboard layout.
        /// </summary>
        /// <seealso cref="AffixConfig.KeyString"/>
        public string? KeyString { get; set; }

        /// <summary>
        /// Characters used to permit some suggestions.
        /// </summary>
        /// <seealso cref="AffixConfig.TryString"/>
        public string? TryString { get; set; }

        /// <summary>
        /// The language code used for language specific functions.
        /// </summary>
        /// <seealso cref="AffixConfig.Language"/>
        public string? Language { get; set; }

        /// <summary>
        /// The culture associated with the language.
        /// </summary>
        /// <seealso cref="AffixConfig.Culture"/>
        public CultureInfo? Culture { get; set; }

        /// <summary>
        /// Flag indicating that a word may be in compound words.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundFlag"/>
        public FlagValue CompoundFlag;

        /// <summary>
        /// A flag indicating that a word may be the first element in a compound word.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundBegin"/>
        public FlagValue CompoundBegin;

        /// <summary>
        /// A flag indicating that a word may be the last element in a compound word.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundEnd"/>
        public FlagValue CompoundEnd;

        /// <summary>
        /// A flag indicating that a word may be a middle element in a compound word.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundMiddle"/>
        public FlagValue CompoundMiddle;

        /// <summary>
        /// Maximum word count in a compound word.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundWordMax"/>
        public int? CompoundWordMax { get; set; }

        /// <summary>
        /// Minimum length of words used for compounding.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundMin"/>
        public int? CompoundMin { get; set; }

        /// <summary>
        /// A flag marking compounds as a compound root.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundRoot"/>
        public FlagValue CompoundRoot;

        /// <summary>
        /// A flag indicating that an affix may be inside of compounds.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundPermitFlag"/>
        public FlagValue CompoundPermitFlag;

        /// <summary>
        /// A flag forbidding a suffix from compounding.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundForbidFlag"/>
        public FlagValue CompoundForbidFlag;

        /// <summary>
        /// Flag indicating that a word should not be used as a suggestion.
        /// </summary>
        /// <seealso cref="AffixConfig.NoSuggest"/>
        public FlagValue NoSuggest;

        /// <summary>
        /// Flag indicating that a word should not be used in ngram based suggestions.
        /// </summary>
        /// <seealso cref="AffixConfig.NoNgramSuggest"/>
        public FlagValue NoNgramSuggest;

        /// <summary>
        /// A flag indicating a forbidden word form.
        /// </summary>
        /// <seealso cref="AffixConfig.ForbiddenWord"/>
        public FlagValue? ForbiddenWord;

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        /// <seealso cref="AffixConfig.LemmaPresent"/>
        public FlagValue LemmaPresent;

        /// <summary>
        /// A flag indicating that affixes may be on a word when this word also has prefix with <see cref="Circumfix"/> flag and vice versa.
        /// </summary>
        /// <seealso cref="AffixConfig.Circumfix"/>
        public FlagValue Circumfix;

        /// <summary>
        /// A flag indicating that a suffix may be only inside of compounds.
        /// </summary>
        /// <seealso cref="AffixConfig.OnlyInCompound"/>
        public FlagValue OnlyInCompound;

        /// <summary>
        /// A flag signing virtual stems in the dictionary.
        /// </summary>
        /// <seealso cref="AffixConfig.NeedAffix"/>
        public FlagValue NeedAffix;

        /// <summary>
        /// Maximum number of n-gram suggestions. A value of 0 switches off the n-gram suggestions.
        /// </summary>
        /// <seealso cref="AffixConfig.MaxNgramSuggestions"/>
        public int? MaxNgramSuggestions { get; set; }

        /// <summary>
        /// Similarity factor for the n-gram based suggestions.
        /// </summary>
        /// <seealso cref="AffixConfig.MaxDifferency"/>
        public int? MaxDifferency { get; set; }

        /// <summary>
        /// Maximum number of suggested compound words generated by compound rule.
        /// </summary>
        /// <seealso cref="AffixConfig.MaxCompoundSuggestions"/>
        public int? MaxCompoundSuggestions { get; set; }

        /// <summary>
        /// A flag indicating that uppercased and capitalized forms of words are forbidden.
        /// </summary>
        /// <seealso cref="AffixConfig.KeepCase"/>
        public FlagValue KeepCase;

        /// <summary>
        /// A flag forcing capitalization of the whole compound word.
        /// </summary>
        /// <seealso cref="AffixConfig.ForceUpperCase"/>
        public FlagValue ForceUpperCase;

        /// <summary>
        /// Flag indicating a rare word that is also often a spelling mistake.
        /// </summary>
        /// <seealso cref="AffixConfig.Warn"/>
        public FlagValue Warn;

        /// <summary>
        /// Flag signing affix rules and dictionary words not used in morphological generation.
        /// </summary>
        /// <seealso cref="AffixConfig.SubStandard"/>
        public FlagValue SubStandard;

        /// <summary>
        /// A flag used by compound check.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundSyllableNum"/>
        public string? CompoundSyllableNum;

        /// <summary>
        /// The encoding to be used in morpheme, affix, and dictionary files.
        /// </summary>
        /// <seealso cref="AffixConfig.Encoding"/>
        public Encoding Encoding { get; set; } = AffixReader.DefaultEncoding;

        /// <summary>
        /// Specifies modifications to try first.
        /// </summary>
        /// <seealso cref="AffixConfig.Replacements"/>
        public IList<SingleReplacement> Replacements => _replacements;

        internal ArrayBuilder<SingleReplacement> _replacements = new();

        /// <summary>
        /// Suffixes attached to root words to make other words.
        /// </summary>
        /// <seealso cref="AffixConfig.Suffixes"/>
        public SuffixCollection.Builder Suffixes = new();

        /// <summary>
        /// Preffixes attached to root words to make other words.
        /// </summary>
        /// <seealso cref="AffixConfig.Prefixes"/>
        public PrefixCollection.Builder Prefixes = new();

        /// <summary>
        /// Ordinal numbers for affix flag compression.
        /// </summary>
        /// <seealso cref="AffixConfig.AliasF"/>
        public IList<FlagSet> AliasF => _aliasF;

        internal ArrayBuilder<FlagSet> _aliasF = new();

        /// <summary>
        /// Values used for morphological alias compression.
        /// </summary>
        /// <seealso cref="AffixConfig.AliasM"/>
        public IList<MorphSet> AliasM => _aliasM;

        internal ArrayBuilder<MorphSet> _aliasM = new();

        /// <summary>
        /// Defines custom compound patterns with a regex-like syntax.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundRules"/>
        public IList<CompoundRule> CompoundRules => _compoundRules;

        internal ArrayBuilder<CompoundRule> _compoundRules = new();

        /// <summary>
        /// Forbid compounding, if the first word in the compound ends with endchars, and
        /// next word begins with beginchars and(optionally) they have the requested flags.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundPatterns"/>
        public IList<PatternEntry> CompoundPatterns => _compoundPatterns;

        internal ArrayBuilder<PatternEntry> _compoundPatterns = new();

        /// <summary>
        /// Defines new break points for breaking words and checking word parts separately.
        /// </summary>
        /// <seealso cref="AffixConfig.BreakPoints"/>
        public IList<string> BreakPoints => _breakPoints;

        internal ArrayBuilder<string> _breakPoints = new();

        /// <summary>
        /// Input conversion entries.
        /// </summary>
        /// <seealso cref="AffixConfig.InputConversions"/>
        public IDictionary<string, MultiReplacementEntry> InputConversions => _inputConversions;

        internal TextDictionary<MultiReplacementEntry> _inputConversions = new(0);

        /// <summary>
        /// Output conversion entries.
        /// </summary>
        /// <seealso cref="AffixConfig.OutputConversions"/>
        public IDictionary<string, MultiReplacementEntry> OutputConversions => _outputConversions;

        internal TextDictionary<MultiReplacementEntry> _outputConversions = new(0);

        /// <summary>
        /// Mappings between related characters.
        /// </summary>
        /// <seealso cref="AffixConfig.RelatedCharacterMap"/>
        public IList<MapEntry> RelatedCharacterMap => _relatedCharacterMap;

        internal ArrayBuilder<MapEntry> _relatedCharacterMap = new();

        /// <summary>
        /// Phonetic transcription entries.
        /// </summary>
        /// <seealso cref="AffixConfig.Phone"/>
        public IList<PhoneticEntry> Phone => _phone;

        internal ArrayBuilder<PhoneticEntry> _phone = new();

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
        public CharacterSet CompoundVowels { get; set; } = CharacterSet.Empty;

        /// <summary>
        /// Extra word characters.
        /// </summary>
        /// <seealso cref="AffixConfig.WordChars"/>
        public CharacterSet WordChars { get; set; } = CharacterSet.Empty;

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics characters)
        /// for dictionary words, affixes and input words.
        /// </summary>
        /// <seealso cref="AffixConfig.IgnoredChars"/>
        public CharacterSet IgnoredChars { get; set; } = CharacterSet.Empty;

        /// <summary>
        /// Affix and dictionary file version string.
        /// </summary>
        /// <seealso cref="AffixConfig.Version"/>
        public string? Version { get; set; }

        /// <summary>
        /// Indicates that some of the affix entries have "cont class".
        /// </summary>
        public bool HasContClass { get; set; }

        /// <summary>
        /// A list of the warnings that were produced while reading or building an <see cref="AffixConfig"/>.
        /// </summary>
        public List<string> Warnings { get; } = [];

        /// <summary>
        /// Builds a new <see cref="AffixConfig"/> based on the values set in this builder.
        /// </summary>
        /// <returns>A new affix config.</returns>
        /// <seealso cref="AffixConfig"/>
        public AffixConfig Build() => BuildOrExtract(extract: false);

        /// <summary>
        /// Builds a new <see cref="AffixConfig"/> based on the values set in the builder.
        /// </summary>
        /// <returns>A new affix config.</returns>
        /// <remarks>
        /// This method can leave the builder in an invalid state
        /// but provides better performance for file reads.
        /// </remarks>
        /// <seealso cref="AffixConfig"/>
        public AffixConfig Extract() => BuildOrExtract(extract: true);

        /// <summary>
        /// Builds a new <see cref="AffixConfig"/> based on the values set in the builder.
        /// </summary>
        /// <param name="extract"><c>true</c> to build an <see cref="AffixConfig"/> at the expense of this builder.</param>
        /// <returns>A new affix config.</returns>
        /// <seealso cref="AffixConfig"/>
        /// <remarks>
        /// This method can leave the builder in an invalid state
        /// but provides better performance for file reads.
        /// </remarks>
        private AffixConfig BuildOrExtract(bool extract)
        {
            var culture = CultureInfo.InvariantCulture;
            var comparer = StringComparer.InvariantCulture;

            if (Culture is not null)
            {
                culture = CultureInfo.ReadOnly(Culture);
                comparer = Culture.CompareInfo.GetStringComparer(CompareOptions.None) ?? StringComparer.InvariantCulture;
            }

            var config = new AffixConfig
            {
                Options = Options,
                FlagMode = FlagMode,
                KeyString = KeyString ?? DefaultKeyString,
                TryString = TryString ?? string.Empty,
                Language = Language,
                Culture = culture,
                IsHungarian = string.Equals(culture.TwoLetterISOLanguageName, "HU", StringComparison.OrdinalIgnoreCase),
                IsGerman = string.Equals(culture.TwoLetterISOLanguageName, "DE", StringComparison.OrdinalIgnoreCase),
                IsLanguageWithDashUsage = !string.IsNullOrEmpty(TryString) && TryString!.ContainsAny('-', 'a'),
                CultureUsesDottedI =
                    "AZ".Equals(culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
                    || "TR".Equals(culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
                    || "CRH".Equals(culture.ThreeLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
                    || "CRH".Equals(culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase), // wikipedia says: this is an ISO2 code even though it has 3 characters
                StringComparer = comparer,
                CompoundFlag = CompoundFlag,
                CompoundBegin = CompoundBegin,
                CompoundEnd = CompoundEnd,
                CompoundMiddle = CompoundMiddle,
                CompoundWordMax = CompoundWordMax,
                CompoundMin = CompoundMin ?? DefaultCompoundMinLength,
                CompoundRoot = CompoundRoot,
                CompoundPermitFlag = CompoundPermitFlag,
                CompoundForbidFlag = CompoundForbidFlag,
                NoSuggest = NoSuggest,
                NoNgramSuggest = NoNgramSuggest,
                ForbiddenWord = ForbiddenWord ?? SpecialFlags.ForbiddenWord,
                LemmaPresent = LemmaPresent,
                Circumfix = Circumfix,
                OnlyInCompound = OnlyInCompound,
                NeedAffix = NeedAffix,
                MaxNgramSuggestions = MaxNgramSuggestions ?? DefaultMaxNgramSuggestions,
                MaxDifferency = MaxDifferency,
                MaxCompoundSuggestions = MaxCompoundSuggestions ?? DefaultMaxCompoundSuggestions,
                KeepCase = KeepCase,
                ForceUpperCase = ForceUpperCase,
                Warn = Warn,
                SubStandard = SubStandard,
                CompoundSyllableNum = CompoundSyllableNum,
                Encoding = Encoding,
                CompoundMaxSyllable = CompoundMaxSyllable,
                CompoundVowels = CompoundVowels,
                WordChars = WordChars,
                IgnoredChars = IgnoredChars,
                Version = Version
            };

            if (extract)
            {
                config.InputConversions = _inputConversions.HasItems
                    ? MultiReplacementTable.TakeDictionary(_inputConversions)
                    : MultiReplacementTable.Empty;
                _inputConversions = new(0);
                config.OutputConversions = _outputConversions.HasItems
                    ? MultiReplacementTable.TakeDictionary(_outputConversions)
                    : MultiReplacementTable.Empty;
                _outputConversions = new(0);
            }
            else
            {
                config.InputConversions = _inputConversions.HasItems
                    ? MultiReplacementTable.Create(_inputConversions)
                    : MultiReplacementTable.Empty;
                config.OutputConversions = _outputConversions.HasItems
                    ? MultiReplacementTable.Create(_outputConversions)
                    : MultiReplacementTable.Empty;
            }

            config.AliasF = new(_aliasF.MakeOrExtractArray(extract));
            config.AliasM = new(_aliasM.MakeOrExtractArray(extract));
            config.BreakPoints = new(_breakPoints.MakeOrExtractArray(extract));
            config.Replacements = new(_replacements.MakeOrExtractArray(extract));
            config.CompoundRules = new(_compoundRules.MakeOrExtractArray(extract));
            config.CompoundPatterns = new(_compoundPatterns.MakeOrExtractArray(extract));
            config.RelatedCharacterMap = new(_relatedCharacterMap.MakeOrExtractArray(extract));
            config.Phone = new(_phone.MakeOrExtractArray(extract));

            config.Prefixes = Prefixes.BuildCollection(extract);
            config.Suffixes = Suffixes.BuildCollection(extract);

            config.ContClasses = config.Prefixes.ContClasses.Union(config.Suffixes.ContClasses);

            config.Flags_CompoundFlag_CompoundBegin = FlagSet.Create(config.CompoundFlag, config.CompoundBegin);
            config.Flags_CompoundFlag_CompoundMiddle = FlagSet.Create(config.CompoundFlag, config.CompoundMiddle);
            config.Flags_CompoundFlag_CompoundEnd = FlagSet.Create(config.CompoundFlag, config.CompoundEnd);
            config.Flags_CompoundForbid_CompoundEnd = FlagSet.Create(config.CompoundForbidFlag, config.CompoundEnd);
            config.Flags_CompoundForbid_CompoundMiddle_CompoundEnd = config.Flags_CompoundForbid_CompoundEnd.Union(config.CompoundMiddle);
            config.Flags_OnlyInCompound_OnlyUpcase = FlagSet.Create(config.OnlyInCompound, SpecialFlags.OnlyUpcaseFlag);
            config.Flags_NeedAffix_OnlyInCompound = FlagSet.Create(config.NeedAffix, config.OnlyInCompound);
            config.Flags_NeedAffix_OnlyInCompound_OnlyUpcase = config.Flags_NeedAffix_OnlyInCompound.Union(SpecialFlags.OnlyUpcaseFlag);
            config.Flags_NeedAffix_OnlyInCompound_Circumfix = config.Flags_NeedAffix_OnlyInCompound.Union(config.Circumfix);
            config.Flags_NeedAffix_ForbiddenWord_OnlyUpcase = FlagSet.Create(config.NeedAffix, config.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag);
            config.Flags_NeedAffix_ForbiddenWord_OnlyUpcase_NoSuggest = config.Flags_NeedAffix_ForbiddenWord_OnlyUpcase.Union(config.NoSuggest);
            config.Flags_ForbiddenWord_OnlyUpcase = FlagSet.Create(config.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag);
            config.Flags_ForbiddenWord_OnlyUpcase_NoSuggest = config.Flags_ForbiddenWord_OnlyUpcase.Union(config.NoSuggest);
            config.Flags_ForbiddenWord_OnlyUpcase_NoSuggest_OnlyInCompound = config.Flags_ForbiddenWord_OnlyUpcase_NoSuggest.Union(config.OnlyInCompound);
            config.Flags_ForbiddenWord_NoSuggest = FlagSet.Create(config.ForbiddenWord, config.NoSuggest);
            config.Flags_ForbiddenWord_NoSuggest_SubStandard = config.Flags_ForbiddenWord_NoSuggest.Union(config.SubStandard);

            config.Warnings = Warnings.ToArray();

            return config;
        }

        /// <summary>
        /// Enables the given <paramref name="options"/> bits.
        /// </summary>
        /// <param name="options">Various bit options to enable.</param>
        public void EnableOptions(AffixConfigOptions options)
        {
            Options |= options;
        }

        public void LogWarning(string warning)
        {
            Warnings.Add(warning);
        }
    }
}
