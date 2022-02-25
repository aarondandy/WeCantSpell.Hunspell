using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class AffixConfig
{
    public sealed class Builder
    {
        public Builder()
        {
            FlagSetDeduper = new Deduper<FlagSet>(FlagSet.DefaultComparer);
            FlagSetDeduper.Add(FlagSet.Empty);
            MorphSetDeduper = new Deduper<MorphSet>(MorphSet.DefaultComparer);
            MorphSetDeduper.Add(MorphSet.Empty);
            _characterConditionGroupDeduper = new Deduper<CharacterConditionGroup>(CharacterConditionGroup.DefaultComparer);
            _characterConditionGroupDeduper.Add(CharacterConditionGroup.Empty);
            _characterConditionGroupDeduper.Add(CharacterConditionGroup.AllowAnySingleCharacter);
            _stringDeduper = new Deduper<string>(StringComparer.Ordinal);
            _stringDeduper.Add(string.Empty);
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
        public FlagMode FlagMode { get; set; } = FlagMode.Char;

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
        public ImmutableArray<SingleReplacement>.Builder Replacements { get; } = ImmutableArray.CreateBuilder<SingleReplacement>();

        /// <summary>
        /// Suffixes attached to root words to make other words.
        /// </summary>
        /// <seealso cref="AffixConfig.Suffixes"/>
        public List<AffixEntryGroup<SuffixEntry>.Builder>? Suffixes;

        /// <summary>
        /// Preffixes attached to root words to make other words.
        /// </summary>
        /// <seealso cref="AffixConfig.Prefixes"/>
        public List<AffixEntryGroup<PrefixEntry>.Builder>? Prefixes;

        /// <summary>
        /// Ordinal numbers for affix flag compression.
        /// </summary>
        /// <seealso cref="AffixConfig.AliasF"/>
        public ImmutableArray<FlagSet>.Builder AliasF { get; } = ImmutableArray.CreateBuilder<FlagSet>();

        /// <summary>
        /// Values used for morphological alias compression.
        /// </summary>
        /// <seealso cref="AffixConfig.AliasM"/>
        public ImmutableArray<MorphSet>.Builder AliasM { get; } = ImmutableArray.CreateBuilder<MorphSet>();

        /// <summary>
        /// Defines custom compound patterns with a regex-like syntax.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundRules"/>
        public ImmutableArray<CompoundRule>.Builder CompoundRules { get; } = ImmutableArray.CreateBuilder<CompoundRule>();

        /// <summary>
        /// Forbid compounding, if the first word in the compound ends with endchars, and
        /// next word begins with beginchars and(optionally) they have the requested flags.
        /// </summary>
        /// <seealso cref="AffixConfig.CompoundPatterns"/>
        public ImmutableArray<PatternEntry>.Builder CompoundPatterns { get; } = ImmutableArray.CreateBuilder<PatternEntry>();

        /// <summary>
        /// Defines new break points for breaking words and checking word parts separately.
        /// </summary>
        /// <seealso cref="AffixConfig.BreakPoints"/>
        public ImmutableArray<string>.Builder BreakPoints { get; } = ImmutableArray.CreateBuilder<string>();

        /// <summary>
        /// Input conversion entries.
        /// </summary>
        /// <seealso cref="AffixConfig.InputConversions"/>
        public Dictionary<string, MultiReplacementEntry>? InputConversions;

        /// <summary>
        /// Output conversion entries.
        /// </summary>
        /// <seealso cref="AffixConfig.OutputConversions"/>
        public Dictionary<string, MultiReplacementEntry>? OutputConversions;

        /// <summary>
        /// Mappings between related characters.
        /// </summary>
        /// <seealso cref="AffixConfig.RelatedCharacterMap"/>
        public ImmutableArray<MapEntry>.Builder RelatedCharacterMap { get; } = ImmutableArray.CreateBuilder<MapEntry>();

        /// <summary>
        /// Phonetic transcription entries.
        /// </summary>
        /// <seealso cref="AffixConfig.Phone"/>
        public ImmutableArray<PhoneticEntry>.Builder Phone { get; } = ImmutableArray.CreateBuilder<PhoneticEntry>();

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
        public CharacterSet? CompoundVowels { get; set; }

        /// <summary>
        /// Extra word characters.
        /// </summary>
        /// <seealso cref="AffixConfig.WordChars"/>
        public CharacterSet? WordChars { get; set; }

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics characters)
        /// for dictionary words, affixes and input words.
        /// </summary>
        /// <seealso cref="AffixConfig.IgnoredChars"/>
        public CharacterSet? IgnoredChars { get; set; }

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
        public List<string> Warnings { get; } = new();

        /// <summary>
        /// Constructs a <see cref="AffixConfig"/> based on the values set in the builder.
        /// </summary>
        /// <returns>A constructed affix config.</returns>
        /// <seealso cref="AffixConfig"/>
        public AffixConfig ToImmutable() => ToImmutable(destructive: false);

        /// <summary>
        /// Constructs a <see cref="AffixConfig"/> based on the values set in the builder
        /// destroying the builder in the process.
        /// </summary>
        /// <returns>A constructed affix config.</returns>
        /// <seealso cref="AffixConfig"/>
        /// <remarks>
        /// This method can leave the builder in an invalid state
        /// but provides better performance for file reads.
        /// </remarks>
        public AffixConfig MoveToImmutable() => ToImmutable(destructive: true);

        internal Deduper<FlagSet> FlagSetDeduper { get; }

        internal Deduper<MorphSet> MorphSetDeduper { get; }

        private readonly Deduper<CharacterConditionGroup> _characterConditionGroupDeduper;

        private readonly Deduper<string> _stringDeduper;

        private AffixConfig ToImmutable(bool destructive)
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
                KeyString = Dedup(KeyString ?? DefaultKeyString),
                TryString = Dedup(TryString ?? string.Empty),
                Language = Language is null ? null : Dedup(Language),
                Culture = culture,
                IsHungarian = string.Equals(culture.TwoLetterISOLanguageName, "HU", StringComparison.OrdinalIgnoreCase),
                IsGerman = string.Equals(culture.TwoLetterISOLanguageName, "DE", StringComparison.OrdinalIgnoreCase),
                IsLanguageWithDashUsage = !string.IsNullOrEmpty(TryString) && TryString.AsSpan().ContainsAny('-', 'a'),
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
                CompoundVowels = CompoundVowels ?? CharacterSet.Empty,
                WordChars = WordChars ?? CharacterSet.Empty,
                IgnoredChars = IgnoredChars ?? CharacterSet.Empty,
                Version = Version is null ? null : Dedup(Version),
                Warnings = WarningList.Create(Warnings)
            };

            if (destructive)
            {
                config.InputConversions = MultiReplacementTable.TakeDictionary(InputConversions);
                InputConversions = null;
                config.OutputConversions = MultiReplacementTable.TakeDictionary(OutputConversions);
                OutputConversions = null;
            }
            else
            {
                config.InputConversions = MultiReplacementTable.Create(InputConversions);
                config.OutputConversions = MultiReplacementTable.Create(OutputConversions);
            }

            config.AliasF = AliasF.ToImmutable(destructive);
            config.AliasM = AliasM.ToImmutable(destructive);
            config.BreakPoints = new(BreakPoints.ToImmutable(destructive));
            config.Replacements = new(Replacements.ToImmutable(destructive));
            config.CompoundRules = new(CompoundRules.ToImmutable(destructive));
            config.CompoundPatterns = new(CompoundPatterns.ToImmutable(destructive));
            config.RelatedCharacterMap = new(RelatedCharacterMap.ToImmutable(destructive));
            config.Phone = new(Phone.ToImmutable(destructive));

            config.Prefixes = PrefixCollection.Create(Prefixes);

            config.Suffixes = SuffixCollection.Create(Suffixes);

            config.ContClasses = FlagSet.Union(config.Prefixes.ContClasses, config.Suffixes.ContClasses);

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

        internal FlagSet Dedup(FlagSet values)
        {
#if DEBUG
            if (values is null) throw new ArgumentNullException(nameof(values));
#endif
            return FlagSetDeduper.GetEqualOrAdd(values);
        }

        internal string Dedup(ReadOnlySpan<char> value) => _stringDeduper.GetEqualOrAdd(value.ToString());

        internal string Dedup(string value)
        {
#if DEBUG
            if (value is null) throw new ArgumentNullException(nameof(value));
#endif
            return _stringDeduper.GetEqualOrAdd(value);
        }

        internal string[] DedupInPlace(string[] values)
        {
#if DEBUG
            if (values is null) throw new ArgumentNullException(nameof(values));
#endif

            for (var i = 0; i < values.Length; i++)
            {
                ref var value = ref values[i];
                if (value is not null)
                {
                    value = _stringDeduper.GetEqualOrAdd(value);
                }
            }

            return values;
        }

        internal string[] DedupIntoArray(List<string> values)
        {
#if DEBUG
            if (values is null) throw new ArgumentNullException(nameof(values));
#endif

            if (values is not { Count: > 0 })
            {
                return Array.Empty<string>();
            }

            return DedupInPlace(values.ToArray());
        }

        internal MorphSet Dedup(MorphSet value) => MorphSetDeduper.GetEqualOrAdd(value);

        public CharacterConditionGroup Dedup(CharacterConditionGroup value) => _characterConditionGroupDeduper.GetEqualOrAdd(value);

        public void LogWarning(string warning)
        {
            Warnings.Add(warning);
        }
    }
}
