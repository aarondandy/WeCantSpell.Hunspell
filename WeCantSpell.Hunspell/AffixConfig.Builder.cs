﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public partial class AffixConfig
    {
        public sealed class Builder
        {
            private const int DefaultCompoundMinLength = 3;

            private const int DefaultMaxNgramSuggestions = 4;

            private const int DefaultMaxCompoundSuggestions = 3;

            private const string DefaultKeyString = "qwertyuiop|asdfghjkl|zxcvbnm";

            internal readonly Deduper<FlagSet> FlagSetDeduper;

            internal readonly Deduper<MorphSet> MorphSetDeduper;

            internal readonly Deduper<CharacterConditionGroup> CharacterConditionGroupDeduper;

            internal readonly StringDeduper StringDeduper;

            public Builder()
            {
                FlagSetDeduper = new Deduper<FlagSet>(FlagSet.DefaultComparer);
                FlagSetDeduper.Add(FlagSet.Empty);
                MorphSetDeduper = new Deduper<MorphSet>(MorphSet.DefaultComparer);
                MorphSetDeduper.Add(MorphSet.Empty);
                CharacterConditionGroupDeduper = new Deduper<CharacterConditionGroup>(CharacterConditionGroup.DefaultComparer);
                CharacterConditionGroupDeduper.Add(CharacterConditionGroup.Empty);
                CharacterConditionGroupDeduper.Add(CharacterConditionGroup.AllowAnySingleCharacter);
                StringDeduper = new StringDeduper();
                StringDeduper.Add(string.Empty);
            }

            /// <summary>
            /// Various affix options.
            /// </summary>
            /// <seealso cref="AffixConfig.Options"/>
            public AffixConfigOptions Options;

            /// <summary>
            /// The flag type.
            /// </summary>
            /// <seealso cref="AffixConfig.FlagMode"/>
            public FlagMode FlagMode = FlagMode.Char;

            /// <summary>
            /// A string of text representing a keyboard layout.
            /// </summary>
            /// <seealso cref="AffixConfig.KeyString"/>
            public string KeyString;

            /// <summary>
            /// Characters used to permit some suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.TryString"/>
            public string TryString;

            /// <summary>
            /// The language code used for language specific functions.
            /// </summary>
            /// <seealso cref="AffixConfig.Language"/>
            public string Language;

            /// <summary>
            /// The culture associated with the language.
            /// </summary>
            /// <seealso cref="AffixConfig.Culture"/>
            public CultureInfo Culture;

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
            public int? CompoundWordMax;

            /// <summary>
            /// Minimum length of words used for compounding.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundMin"/>
            public int? CompoundMin;

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
            public int? MaxNgramSuggestions;

            /// <summary>
            /// Similarity factor for the n-gram based suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxDifferency"/>
            public int? MaxDifferency;

            /// <summary>
            /// Maximum number of suggested compound words generated by compound rule.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxCompoundSuggestions"/>
            public int? MaxCompoundSuggestions;

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
            public string CompoundSyllableNum;

            /// <summary>
            /// The encoding to be used in morpheme, affix, and dictionary files.
            /// </summary>
            /// <seealso cref="AffixConfig.Encoding"/>
            public Encoding Encoding = AffixReader.DefaultEncoding;

            /// <summary>
            /// Specifies modifications to try first.
            /// </summary>
            /// <seealso cref="AffixConfig.Replacements"/>
            public List<SingleReplacement> Replacements;

            /// <summary>
            /// Suffixes attached to root words to make other words.
            /// </summary>
            /// <seealso cref="AffixConfig.Suffixes"/>
            public List<AffixEntryGroup<SuffixEntry>.Builder> Suffixes;

            /// <summary>
            /// Preffixes attached to root words to make other words.
            /// </summary>
            /// <seealso cref="AffixConfig.Prefixes"/>
            public List<AffixEntryGroup<PrefixEntry>.Builder> Prefixes;

            /// <summary>
            /// Ordinal numbers for affix flag compression.
            /// </summary>
            /// <seealso cref="AffixConfig.AliasF"/>
            public List<FlagSet> AliasF;

            /// <summary>
            /// Inidicates if any <see cref="AliasF"/> entries have been defined.
            /// </summary>
            public bool IsAliasF => AliasF != null && AliasF.Count > 0;

            /// <summary>
            /// Values used for morphological alias compression.
            /// </summary>
            /// <seealso cref="AffixConfig.AliasM"/>
            public List<MorphSet> AliasM;

            /// <summary>
            /// Indicates if any <see cref="AliasM"/> entries have been defined.
            /// </summary>
            public bool IsAliasM => AliasM != null && AliasM.Count > 0;

            /// <summary>
            /// Defines custom compound patterns with a regex-like syntax.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundRules"/>
            public List<CompoundRule> CompoundRules;

            /// <summary>
            /// Forbid compounding, if the first word in the compound ends with endchars, and
            /// next word begins with beginchars and(optionally) they have the requested flags.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundPatterns"/>
            public List<PatternEntry> CompoundPatterns;

            /// <summary>
            /// Defines new break points for breaking words and checking word parts separately.
            /// </summary>
            /// <seealso cref="AffixConfig.BreakPoints"/>
            public List<string> BreakPoints;

            /// <summary>
            /// Input conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.InputConversions"/>
            public Dictionary<string, MultiReplacementEntry> InputConversions;

            /// <summary>
            /// Output conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.OutputConversions"/>
            public Dictionary<string, MultiReplacementEntry> OutputConversions;

            /// <summary>
            /// Mappings between related characters.
            /// </summary>
            /// <seealso cref="AffixConfig.RelatedCharacterMap"/>
            public List<MapEntry> RelatedCharacterMap;

            /// <summary>
            /// Phonetic transcription entries.
            /// </summary>
            /// <seealso cref="AffixConfig.Phone"/>
            public List<PhoneticEntry> Phone;

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
            public CharacterSet CompoundVowels { get; set; }

            /// <summary>
            /// Extra word characters.
            /// </summary>
            /// <seealso cref="AffixConfig.WordChars"/>
            public CharacterSet WordChars { get; set; }

            /// <summary>
            /// Ignored characters (for example, Arabic optional diacretics characters)
            /// for dictionary words, affixes and input words.
            /// </summary>
            /// <seealso cref="AffixConfig.IgnoredChars"/>
            public CharacterSet IgnoredChars { get; set; }

            /// <summary>
            /// Affix and dictionary file version string.
            /// </summary>
            /// <seealso cref="AffixConfig.Version"/>
            public string Version { get; set; }

            /// <summary>
            /// Indicates that some of the affix entries have "cont class".
            /// </summary>
            public bool HasContClass { get; set; }

            /// <summary>
            /// A list of the warnings that were produced while reading or building an <see cref="AffixConfig"/>.
            /// </summary>
            public List<string> Warnings = new List<string>();

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

            private AffixConfig ToImmutable(bool destructive)
            {
                var culture = CultureInfo.ReadOnly(Culture ?? CultureInfo.InvariantCulture);

                var config = new AffixConfig
                {
                    Options = Options,
                    FlagMode = FlagMode,
                    KeyString = Dedup(KeyString ?? DefaultKeyString),
                    TryString = Dedup(TryString ?? string.Empty),
                    Language = Dedup(Language ?? string.Empty),
                    Culture = culture,
                    IsHungarian = string.Equals(culture?.TwoLetterISOLanguageName, "HU", StringComparison.OrdinalIgnoreCase),
                    IsGerman = string.Equals(culture?.TwoLetterISOLanguageName, "DE", StringComparison.OrdinalIgnoreCase),
                    IsLanguageWithDashUsage = !string.IsNullOrEmpty(TryString) && TryString.AsSpan().ContainsAny('-', 'a'),
                    CultureUsesDottedI =
                        string.Equals(culture?.TwoLetterISOLanguageName, "AZ", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(culture?.TwoLetterISOLanguageName, "TR", StringComparison.OrdinalIgnoreCase)
#if !NO_ISO3_LANG
                        || string.Equals(culture?.ThreeLetterISOLanguageName, "CRH", StringComparison.OrdinalIgnoreCase)
#endif
                        || string.Equals(culture?.TwoLetterISOLanguageName, "CRH", StringComparison.OrdinalIgnoreCase), // wikipedia says: this is an ISO2 code
                    StringComparer = new CulturedStringComparer(culture),
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
                    Version = Version == null ? null : Dedup(Version),
                    BreakPoints = BreakSet.Create(BreakPoints),
                    CompoundRules = CompoundRuleSet.Create(CompoundRules),
                    Replacements = SingleReplacementSet.Create(Replacements),
                    CompoundPatterns = PatternSet.Create(CompoundPatterns),
                    RelatedCharacterMap = MapTable.Create(RelatedCharacterMap),
                    Phone = PhoneTable.Create(Phone),
                    Warnings = WarningList.Create(Warnings)
                };

                if (destructive)
                {
                    config.InputConversions = MultiReplacementTable.TakeDictionary(ReferenceHelpers.Steal(ref InputConversions));
                    config.OutputConversions = MultiReplacementTable.TakeDictionary(ReferenceHelpers.Steal(ref OutputConversions));

                    config.aliasF = AliasF ?? new List<FlagSet>();
                    AliasF = null;
                    config.aliasM = AliasM ?? new List<MorphSet>();
                    AliasM = null;
                }
                else
                {
                    config.InputConversions = MultiReplacementTable.Create(InputConversions);
                    config.OutputConversions = MultiReplacementTable.Create(OutputConversions);

                    config.aliasF = AliasF == null ? new List<FlagSet>() : AliasF.ToList();
                    config.aliasM = AliasM == null ? new List<MorphSet>() : AliasM.ToList();
                }

                config.Prefixes = PrefixCollection.Create(Prefixes);

                config.Suffixes = SuffixCollection.Create(Suffixes);

                config.ContClasses = FlagSet.Union(config.Prefixes.ContClasses, config.Suffixes.ContClasses);

                return config;
            }

            /// <summary>
            /// Enables the given <paramref name="options"/> bits.
            /// </summary>
            /// <param name="options">Various bit options to enable.</param>
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void EnableOptions(AffixConfigOptions options) =>
                Options |= options;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public FlagSet Dedup(FlagSet values)
            {
#if DEBUG
                if (values == null)
                {
                    throw new ArgumentNullException(nameof(values));
                }
#endif
                return FlagSetDeduper.GetEqualOrAdd(values);
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            internal string Dedup(ReadOnlySpan<char> value) =>
                StringDeduper.GetEqualOrAdd(value.ToString());

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public string Dedup(string value)
            {
#if DEBUG
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
#endif
                return StringDeduper.GetEqualOrAdd(value);
            }

            public string[] DedupInPlace(string[] values)
            {
                if (values != null)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        ref string value = ref values[i];
                        if (value != null)
                        {
                            value = StringDeduper.GetEqualOrAdd(value);
                        }
                    }
                }

                return values;
            }

            internal string[] DedupIntoArray(List<string> values)
            {
                if (values == null || values.Count == 0)
                {
                    return ArrayEx<string>.Empty;
                }

                var result = new string[values.Count];
                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = StringDeduper.GetEqualOrAdd(values[i]);
                }

                return result;
            }

            public MorphSet Dedup(MorphSet value) =>
                value == null ? null : MorphSetDeduper.GetEqualOrAdd(value);

            public CharacterConditionGroup Dedup(CharacterConditionGroup value) =>
                CharacterConditionGroupDeduper.GetEqualOrAdd(value);

            public void LogWarning(string warning) =>
                Warnings.Add(warning);
        }
    }
}
