using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Hunspell.Infrastructure;

namespace Hunspell
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

            internal readonly StringDeduper StringDeduper;

            public Builder()
            {
                FlagSetDeduper = new Deduper<FlagSet>(new FlagSet.Comparer());
                FlagSetDeduper.Add(FlagSet.Empty);
                StringDeduper = new StringDeduper();
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
            public List<AffixEntryGroup.Builder<SuffixEntry>> Suffixes;

            /// <summary>
            /// Preffixes attached to root words to make other words.
            /// </summary>
            /// <seealso cref="AffixConfig.Prefixes"/>
            public List<AffixEntryGroup.Builder<PrefixEntry>> Prefixes;

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
                    Version = Dedup(Version),
                    BreakPoints = BreakSet.Create(BreakPoints)
                };

                if (destructive)
                {
                    config.Replacements = SingleReplacementSet.TakeList(Steal(ref Replacements));
                    config.CompoundRules = CompoundRuleSet.TakeList(Steal(ref CompoundRules));
                    config.CompoundPatterns = PatternSet.TakeList(Steal(ref CompoundPatterns));
                    config.RelatedCharacterMap = MapTable.TakeList(Steal(ref RelatedCharacterMap));
                    config.Phone = PhoneTable.TakeList(Steal(ref Phone));
                    config.InputConversions = MultiReplacementTable.TakeDictionary(Steal(ref InputConversions));
                    config.OutputConversions = MultiReplacementTable.TakeDictionary(Steal(ref OutputConversions));

                    config.aliasF = AliasF ?? new List<FlagSet>(0);
                    AliasF = null;
                    config.aliasM = AliasM ?? new List<MorphSet>(0);
                    AliasM = null;
                }
                else
                {
                    config.Replacements = SingleReplacementSet.Create(Replacements);
                    config.CompoundRules = CompoundRuleSet.Create(CompoundRules);
                    config.CompoundPatterns = PatternSet.Create(CompoundPatterns);
                    config.RelatedCharacterMap = MapTable.Create(RelatedCharacterMap);
                    config.Phone = PhoneTable.Create(Phone);
                    config.InputConversions = MultiReplacementTable.Create(InputConversions);
                    config.OutputConversions = MultiReplacementTable.Create(OutputConversions);

                    config.aliasF = AliasF == null ? new List<FlagSet>(0) : AliasF.ToList();
                    config.aliasM = AliasM == null ? new List<MorphSet>(0) : AliasM.ToList();
                }

                config.Prefixes = AffixCollection<PrefixEntry>.Create(Prefixes);

                config.Suffixes = AffixCollection<SuffixEntry>.Create(Suffixes);

                config.ContClasses = FlagSet.Union(config.Prefixes.ContClasses, config.Suffixes.ContClasses);

                return config;
            }

            /// <summary>
            /// Enables the given <paramref name="options"/> bits.
            /// </summary>
            /// <param name="options">Various bit options to enable.</param>
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void EnableOptions(AffixConfigOptions options)
            {
                Options |= options;
            }

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private static T Steal<T>(ref T item) where T : class
            {
                var value = item;
                item = null;
                return value;
            }

            public FlagSet TakeArrayForFlagSet(FlagValue[] values) =>
                FlagSetDeduper.GetEqualOrAdd(FlagSet.TakeArray(values));

            public string Dedup(string value) =>
                value == null ? null : StringDeduper.GetEqualOrAdd(value);

            private void Dedup(ref string value)
            {
                if (value != null)
                {
                    value = StringDeduper.GetEqualOrAdd(value);
                }
            }

            public void Dedup(string[] values)
            {
                if (values != null)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        Dedup(ref values[i]);
                    }
                }
            }
        }
    }
}
