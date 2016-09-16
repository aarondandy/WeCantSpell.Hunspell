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
            public Encoding Encoding = DefaultEncoding;

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
            /// <seealso cref="AffixConfig.BreakTable"/>
            public List<string> BreakTable;

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
            /// <seealso cref="AffixConfig.MapTable"/>
            public List<MapEntry> MapTable;

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
                    KeyString = KeyString ?? DefaultKeyString,
                    TryString = TryString ?? string.Empty,
                    Language = Language ?? string.Empty,
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
                    Version = Version
                };

                if (destructive)
                {
                    config.Replacements = Replacements == null ? SingleReplacementTable.Empty : SingleReplacementTable.TakeList(Replacements);
                    Replacements = null;
                    config.aliasF = AliasF ?? new List<FlagSet>(0);
                    AliasF = null;
                    config.aliasM = AliasM ?? new List<MorphSet>(0);
                    AliasM = null;
                    config.CompoundRules = CompoundRules == null ? CompoundRuleTable.Empty : CompoundRuleTable.TakeList(CompoundRules);
                    CompoundRules = null;
                    config.CompoundPatterns = CompoundPatterns == null ? CompoundPatternTable.Empty : CompoundPatternTable.TakeList(CompoundPatterns);
                    CompoundPatterns = null;
                    config.breakTable = BreakTable ?? new List<string>(0);
                    BreakTable = null;
                    config.mapTable = MapTable ?? new List<MapEntry>(0);
                    MapTable = null;
                    config.phone = Phone ?? new List<PhoneticEntry>(0);
                    Phone = null;
                    config.inputConversions = InputConversions ?? new Dictionary<string, MultiReplacementEntry>(0);
                    InputConversions = null;
                    config.outputConversions = OutputConversions ?? new Dictionary<string, MultiReplacementEntry>(0);
                    OutputConversions = null;
                }
                else
                {
                    config.Replacements = SingleReplacementTable.Create(Replacements);
                    config.aliasF = AliasF == null ? new List<FlagSet>(0) : AliasF.ToList();
                    config.aliasM = AliasM == null ? new List<MorphSet>(0) : AliasM.ToList();
                    config.CompoundRules = CompoundRuleTable.Create(CompoundRules);
                    config.CompoundPatterns = CompoundPatternTable.Create(CompoundPatterns);
                    config.breakTable = BreakTable == null ? new List<string>(0) : BreakTable.ToList();
                    config.mapTable = MapTable == null ? new List<MapEntry>(0) : MapTable.ToList();
                    config.phone = Phone == null ? new List<PhoneticEntry>(0) : Phone.ToList();
                    config.inputConversions = InputConversions == null ? new Dictionary<string, MultiReplacementEntry>(0) : new Dictionary<string, MultiReplacementEntry>(InputConversions);
                    config.outputConversions = OutputConversions == null ? new Dictionary<string, MultiReplacementEntry>(0) : new Dictionary<string, MultiReplacementEntry>(OutputConversions);
                }

                BuildAffixCollections(
                    Prefixes,
                    out config.prefixesByFlag,
                    out config.prefixesWithEmptyKeys,
                    out config.prefixesByIndexedKeyCharacter);

                BuildAffixCollections(
                    Suffixes,
                    out config.suffixesByFlag,
                    out config.suffixesWithEmptyKeys,
                    out config.suffixsByIndexedKeyCharacter);

                config.ContClasses = FlagSet.Create(
                    Enumerable.Concat<AffixEntry>(config.prefixesByFlag.Values.SelectMany(g => g.Entries), config.suffixesByFlag.Values.SelectMany(g => g.Entries))
                    .SelectMany(e => e.ContClass));

                return config;
            }

            private void BuildAffixCollections<TEntry>(
                List<AffixEntryGroup.Builder<TEntry>> builders,
                out Dictionary<FlagValue, AffixEntryGroup<TEntry>> entriesByFlag,
                out List<AffixEntryWithDetail<TEntry>> affixesWithEmptyKeys,
                out Dictionary<char, List<AffixEntryWithDetail<TEntry>>> affixesByIndexedKeyCharacter)
                where TEntry : AffixEntry
            {
                if (builders == null || builders.Count == 0)
                {
                    entriesByFlag = new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(0);
                    affixesWithEmptyKeys = new List<AffixEntryWithDetail<TEntry>>(0);
                    affixesByIndexedKeyCharacter = new Dictionary<char, List<AffixEntryWithDetail<TEntry>>>(0);
                    return;
                }

                entriesByFlag = new Dictionary<FlagValue, AffixEntryGroup<TEntry>>(builders.Count);
                affixesWithEmptyKeys = new List<AffixEntryWithDetail<TEntry>>();
                affixesByIndexedKeyCharacter = new Dictionary<char, List<AffixEntryWithDetail<TEntry>>>();

                for (var i = 0; i < builders.Count; i++)
                {
                    var group = builders[i].ToGroup();
                    entriesByFlag.Add(group.AFlag, group);

                    foreach (var entry in group.Entries)
                    {
                        var key = entry.Key;
                        var entryWithDetail = new AffixEntryWithDetail<TEntry>(group, entry);
                        if (string.IsNullOrEmpty(key))
                        {
                            affixesWithEmptyKeys.Add(entryWithDetail);
                        }
                        else
                        {
                            var indexedChar = key[0];
                            List<AffixEntryWithDetail<TEntry>> keyedAffixes;
                            if (affixesByIndexedKeyCharacter.TryGetValue(indexedChar, out keyedAffixes))
                            {
                                keyedAffixes.Add(entryWithDetail);
                            }
                            else
                            {
                                affixesByIndexedKeyCharacter.Add(indexedChar, new List<AffixEntryWithDetail<TEntry>>
                                {
                                    entryWithDetail
                                });
                            }
                        }
                    }
                }
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
        }
    }
}
