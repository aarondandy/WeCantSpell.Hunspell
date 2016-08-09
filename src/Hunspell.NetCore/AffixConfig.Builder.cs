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
            [Obsolete("This will likely change type to a data structure")]
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
            public int CompoundWordMax;

            /// <summary>
            /// Minimum length of words used for compounding.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundMin"/>
            public int CompoundMin;

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
            public FlagValue ForbiddenWord;

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
            public int MaxNgramSuggestions;

            /// <summary>
            /// Similarity factor for the n-gram based suggestions.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxDifferency"/>
            public int MaxDifferency;

            /// <summary>
            /// Maximum number of suggested compound words generated by compound rule.
            /// </summary>
            /// <seealso cref="AffixConfig.MaxCompoundSuggestions"/>
            public int MaxCompoundSuggestions;

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
            /// The encoding name to be used in morpheme, affix, and dictionary files.
            /// </summary>
            /// <seealso cref="AffixConfig.RequestedEncodingName"/>
            public string RequestedEncodingName;

            /// <summary>
            /// Specifies modifications to try first.
            /// </summary>
            /// <seealso cref="AffixConfig.Replacements"/>
            public List<SingleReplacementEntry> Replacements;

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
            public List<ImmutableSortedSet<FlagValue>> AliasF;

            /// <summary>
            /// Inidicates if any <see cref="AliasF"/> entries have been defined.
            /// </summary>
            public bool IsAliasF => AliasF != null && AliasF.Count > 0;

            /// <summary>
            /// Values used for morphological alias compression.
            /// </summary>
            /// <seealso cref="AffixConfig.AliasM"/>
            public List<ImmutableArray<string>> AliasM;

            /// <summary>
            /// Indicates if any <see cref="AliasM"/> entries have been defined.
            /// </summary>
            public bool IsAliasM => AliasM != null && AliasM.Count > 0;

            /// <summary>
            /// Defines custom compound patterns with a regex-like syntax.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundRules"/>
            public List<List<FlagValue>> CompoundRules;

            /// <summary>
            /// Forbid compounding, if the first word in the compound ends with endchars, and
            /// next word begins with beginchars and(optionally) they have the requested flags.
            /// </summary>
            /// <seealso cref="AffixConfig.CompoundPatterns"/>
            public List<PatternEntry> CompoundPatterns;

            /// <summary>
            /// Defines new break points for breaking words and checking word parts separately.
            /// </summary>
            /// <seealso cref="AffixConfig.BreakTable"/>
            public List<string> BreakTable;

            /// <summary>
            /// Input conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.InputConversions"/>
            public Dictionary<string, string[]> InputConversions;

            /// <summary>
            /// Output conversion entries.
            /// </summary>
            /// <seealso cref="AffixConfig.OutputConversions"/>
            public Dictionary<string, string[]> OutputConversions;

            /// <summary>
            /// Mappings between related characters.
            /// </summary>
            /// <seealso cref="AffixConfig.MapTable"/>
            public List<List<string>> MapTable;

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
            /// Indicates that some of the affix entries have "cont class".
            /// </summary>
            public bool HasContClass { get; set; }

            /// <summary>
            /// Constructs a <see cref="AffixConfig"/> based on the values set in the builder.
            /// </summary>
            /// <returns>A constructed affix config.</returns>
            /// <seealso cref="AffixConfig"/>
            public AffixConfig ToAffixConfig()
            {
                // TODO: ProcessPfxTreeToList and ProcessSfxTreeToList
                // TODO: ProcessPfxOrder and ProcessSfxOrder
                // TODO: optimize memory reuse for Immutable collections, focused on AliasF and AliasM

                var config = new AffixConfig
                {
                    Options = Options,
                    FlagMode = FlagMode,
                    KeyString = KeyString ?? string.Empty,
                    TryString = TryString ?? string.Empty,
                    Language = Language ?? string.Empty,
                    Culture = CultureInfo.ReadOnly(Culture ?? CultureInfo.InvariantCulture),
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
                    RequestedEncodingName = RequestedEncodingName,
                    Replacements = EmptyIfNull(Replacements).ToImmutableArray(),
                    Suffixes = EmptyIfNull(Suffixes).Select(b => b.ToGroup()).ToImmutableArray(),
                    Prefixes = EmptyIfNull(Prefixes).Select(b => b.ToGroup()).ToImmutableArray(),
                    AliasF = EmptyIfNull(AliasF).ToImmutableArray(),
                    AliasM = EmptyIfNull(AliasM).ToImmutableArray(),
                    CompoundRules = EmptyIfNull(CompoundRules).Select(ToImmutableArray).ToImmutableArray(),
                    CompoundPatterns = ToImmutableArray(CompoundPatterns),
                    BreakTable = ToImmutableArray(BreakTable),
                    InputConversions = EmptyIfNull(InputConversions).ToImmutableReplacementEntries(),
                    OutputConversions = EmptyIfNull(OutputConversions).ToImmutableReplacementEntries(),
                    MapTable = EmptyIfNull(MapTable).Select(ToImmutableArray).ToImmutableArray(),
                    Phone = ToImmutableArray(Phone),
                    CompoundMaxSyllable = CompoundMaxSyllable,
                    CompoundVowels = ToImmutableSortedSet(CompoundVowels),
                    WordChars = ToImmutableSortedSet(WordChars),
                    IgnoredChars = ToImmutableSortedSet(IgnoredChars),
                    Version = Version
                };

                config.ContClasses = ImmutableSortedSet.CreateRange(
                    config.Prefixes.SelectMany(g => g.Entries.SelectMany(e => e.ContClass))
                    .Concat(config.Suffixes.SelectMany(g => g.Entries.SelectMany(e => e.ContClass))));

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

            private static ImmutableSortedSet<T> ToImmutableSortedSet<T>(IEnumerable<T> items)
            {
                return items == null ? ImmutableSortedSet<T>.Empty : items.ToImmutableSortedSet();
            }

            private static ImmutableArray<T> ToImmutableArray<T>(IEnumerable<T> items)
            {
                return items == null ? ImmutableArray<T>.Empty : items.ToImmutableArray();
            }

            private static IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> items)
            {
                return items == null ? Enumerable.Empty<T>() : items;
            }

            private static Dictionary<K, V> EmptyIfNull<K, V>(Dictionary<K, V> items)
            {
                return items ?? new Dictionary<K, V>();
            }
        }
    }
}
