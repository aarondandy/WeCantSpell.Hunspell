using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class AffixReader
    {
        public static readonly Encoding DefaultEncoding = EncodingEx.GetEncodingByName("ISO8859-1".AsSpan()) ?? Encoding.UTF8;

        private static readonly Regex AffixLineRegex = new Regex(
            @"^[\t ]*([^\t ]+)[\t ]+(?:([^\t ]+)[\t ]+([^\t ]+)|([^\t ]+)[\t ]+([^\t ]+)[\t ]+([^\t ]+)(?:[\t ]+(.+))?)[\t ]*(?:[#].*)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly string[] DefaultBreakTableEntries = { "-", "^-", "-$" };

        private static readonly CharacterSet DefaultCompoundVowels = CharacterSet.TakeArray(new[] { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' });

        public AffixReader(AffixConfig.Builder builder, IHunspellLineReader reader)
        {
            Builder = builder ?? new AffixConfig.Builder();
            Reader = reader;
        }

        private AffixConfig.Builder Builder { get; }

        private IHunspellLineReader Reader { get; }

        private EntryListType Initialized { get; set; } = EntryListType.None;

        public static async Task<AffixConfig> ReadAsync(IHunspellLineReader reader, AffixConfig.Builder builder = null)
        {
            var readerInstance = new AffixReader(
                builder,
                reader ?? throw new ArgumentNullException(nameof(reader)));

            await readerInstance.ReadAsync().ConfigureAwait(false);

            return readerInstance.Builder.MoveToImmutable();
        }

        private async Task ReadToEndAsync()
        {
            string line;
            while ((line = await Reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                ParseLine(line);
            }
        }

        private async Task ReadAsync()
        {
            await ReadToEndAsync().ConfigureAwait(false);
            AddDefaultBreakTableIfEmpty();
        }

        public static AffixConfig Read(IHunspellLineReader reader, AffixConfig.Builder builder = null)
        {
            var readerInstance = new AffixReader(
                builder,
                reader ?? throw new ArgumentNullException(nameof(reader)));

            readerInstance.Read();

            return readerInstance.Builder.MoveToImmutable();
        }

        private void ReadToEnd()
        {
            string line;
            while ((line = Reader.ReadLine()) != null)
            {
                ParseLine(line);
            }
        }

        private void Read()
        {
            ReadToEnd();
            AddDefaultBreakTableIfEmpty();
        }

        public static async Task<AffixConfig> ReadAsync(Stream stream, AffixConfig.Builder builder = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return await ReadAsync(reader, builder).ConfigureAwait(false);
            }
        }
        public static async Task<AffixConfig> ReadFileAsync(string filePath, AffixConfig.Builder builder = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(filePath))
            {
                return await ReadAsync(stream, builder).ConfigureAwait(false);
            }
        }

        public static AffixConfig Read(Stream stream, AffixConfig.Builder builder = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new DynamicEncodingLineReader(stream, DefaultEncoding))
            {
                return Read(reader, builder);
            }
        }

        public static AffixConfig ReadFile(string filePath, AffixConfig.Builder builder = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(filePath))
            {
                return Read(stream, builder);
            }
        }

        private bool ParseLine(string line)
        {
            // read through the initial ^[ \t]*
            var commandStartIndex = 0;
            for (; commandStartIndex < line.Length && line[commandStartIndex].IsTabOrSpace(); commandStartIndex++) ;

            if (commandStartIndex == line.Length || IsCommentPrefix(line[commandStartIndex]))
            {
                return true; // empty, whitespace, or comment
            }

            // read through the final [ \t]*$
            var lineEndIndex = line.Length - 1;
            for (; lineEndIndex > commandStartIndex && line[lineEndIndex].IsTabOrSpace(); lineEndIndex--) ;

            // find the end of the command
            var commandEndIndex = commandStartIndex;
            for (; commandEndIndex <= lineEndIndex && !line[commandEndIndex].IsTabOrSpace(); commandEndIndex++) ;

            // first command exists between [lineStartIndex,commandEndIndex)
            var parameterStartIndex = commandEndIndex;
            for (; parameterStartIndex <= lineEndIndex && line[parameterStartIndex].IsTabOrSpace(); parameterStartIndex++) ;

            var command = line.AsSpan(commandStartIndex, commandEndIndex - commandStartIndex);

            if (parameterStartIndex <= lineEndIndex)
            {
                if (TryHandleParameterizedCommand(
                        command,
                        line.AsSpan(parameterStartIndex, lineEndIndex - parameterStartIndex + 1)))
                {
                    return true;
                }
            }
            else
            {
                var option = TryParseFileBitFlagCommand(command);
                if (option.HasValue)
                {
                    Builder.EnableOptions(option.GetValueOrDefault());
                    return true;
                }
            }

            return LogWarning("Failed to parse line: " + line);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool IsCommentPrefix(char c) => c == '#' || c == '/';

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsInitialized(EntryListType flags) => HasFlag(Initialized, flags);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SetInitialized(EntryListType flags) =>
            Initialized |= flags;

        private void AddDefaultBreakTableIfEmpty()
        {
            if (!IsInitialized(EntryListType.Break))
            {
                if (Builder.BreakPoints == null)
                {
                    Builder.BreakPoints = new List<string>(DefaultBreakTableEntries.Length);
                }

                if (Builder.BreakPoints.Count == 0)
                {
                    Builder.BreakPoints.AddRange(DefaultBreakTableEntries.Select(Builder.Dedup));
                }
            }
        }

        private bool TryHandleParameterizedCommand(ReadOnlySpan<char> commandName, ReadOnlySpan<char> parameters)
        {
#if DEBUG
            if (commandName == null)
            {
                throw new ArgumentNullException(nameof(commandName));
            }
            if (parameters.IsEmpty)
            {
                throw new ArgumentException(nameof(parameters));
            }
#endif

            var command = TryParseCommandKind(commandName);
            if (!command.HasValue)
            {
                return LogWarning($"Unknown command {commandName.ToString()} with params: {parameters.ToString()}");
            }

            switch (command.GetValueOrDefault())
            {
                case AffixReaderCommandKind.Flag:
                    return TrySetFlagMode(parameters);
                case AffixReaderCommandKind.KeyString:
                    Builder.KeyString = Builder.Dedup(parameters);
                    return true;
                case AffixReaderCommandKind.TryString:
                    Builder.TryString = Builder.Dedup(parameters);
                    return true;
                case AffixReaderCommandKind.SetEncoding:
                    var encoding = EncodingEx.GetEncodingByName(parameters);
                    if (encoding == null)
                    {
                        return LogWarning("Failed to get encoding: " + parameters.ToString());
                    }

                    Builder.Encoding = encoding;
                    return true;
                case AffixReaderCommandKind.Language:
                    Builder.Language = Builder.Dedup(parameters);
                    Builder.Culture = GetCultureFromLanguage(Builder.Language);
                    return true;
                case AffixReaderCommandKind.CompoundSyllableNum:
                    Builder.CompoundSyllableNum = Builder.Dedup(parameters);
                    return true;
                case AffixReaderCommandKind.WordChars:
                    Builder.WordChars = CharacterSet.Create(parameters);
                    return true;
                case AffixReaderCommandKind.Ignore:
                    Builder.IgnoredChars = CharacterSet.Create(parameters);
                    return true;
                case AffixReaderCommandKind.CompoundFlag:
                    return TryParseFlag(parameters, out Builder.CompoundFlag);
                case AffixReaderCommandKind.CompoundMiddle:
                    return TryParseFlag(parameters, out Builder.CompoundMiddle);
                case AffixReaderCommandKind.CompoundBegin:
                    return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundEnd)
                        : TryParseFlag(parameters, out Builder.CompoundBegin);
                case AffixReaderCommandKind.CompoundEnd:
                    return EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes)
                        ? TryParseFlag(parameters, out Builder.CompoundBegin)
                        : TryParseFlag(parameters, out Builder.CompoundEnd);
                case AffixReaderCommandKind.CompoundWordMax:
                    Builder.CompoundWordMax = IntEx.TryParseInvariant(parameters);
                    return Builder.CompoundWordMax.HasValue;
                case AffixReaderCommandKind.CompoundMin:
                    Builder.CompoundMin = IntEx.TryParseInvariant(parameters);
                    if (!Builder.CompoundMin.HasValue)
                    {
                        return LogWarning("Failed to parse CompoundMin: " + parameters.ToString());
                    }

                    if (Builder.CompoundMin.GetValueOrDefault() < 1)
                    {
                        Builder.CompoundMin = 1;
                    }

                    return true;
                case AffixReaderCommandKind.CompoundRoot:
                    return TryParseFlag(parameters, out Builder.CompoundRoot);
                case AffixReaderCommandKind.CompoundPermitFlag:
                    return TryParseFlag(parameters, out Builder.CompoundPermitFlag);
                case AffixReaderCommandKind.CompoundForbidFlag:
                    return TryParseFlag(parameters, out Builder.CompoundForbidFlag);
                case AffixReaderCommandKind.CompoundSyllable:
                    return TryParseCompoundSyllable(parameters);
                case AffixReaderCommandKind.NoSuggest:
                    return TryParseFlag(parameters, out Builder.NoSuggest);
                case AffixReaderCommandKind.NoNGramSuggest:
                    return TryParseFlag(parameters, out Builder.NoNgramSuggest);
                case AffixReaderCommandKind.ForbiddenWord:
                    Builder.ForbiddenWord = TryParseFlag(parameters);
                    return Builder.ForbiddenWord.HasValue;
                case AffixReaderCommandKind.LemmaPresent:
                    return TryParseFlag(parameters, out Builder.LemmaPresent);
                case AffixReaderCommandKind.Circumfix:
                    return TryParseFlag(parameters, out Builder.Circumfix);
                case AffixReaderCommandKind.OnlyInCompound:
                    return TryParseFlag(parameters, out Builder.OnlyInCompound);
                case AffixReaderCommandKind.NeedAffix:
                    return TryParseFlag(parameters, out Builder.NeedAffix);
                case AffixReaderCommandKind.Replacement:
                    return TryParseStandardListItem(EntryListType.Replacements, parameters, ref Builder.Replacements, TryParseReplacements);
                case AffixReaderCommandKind.InputConversions:
                    return TryParseConv(parameters, EntryListType.Iconv, ref Builder.InputConversions);
                case AffixReaderCommandKind.OutputConversions:
                    return TryParseConv(parameters, EntryListType.Oconv, ref Builder.OutputConversions);
                case AffixReaderCommandKind.Phone:
                    return TryParseStandardListItem(EntryListType.Phone, parameters, ref Builder.Phone, TryParsePhone);
                case AffixReaderCommandKind.CheckCompoundPattern:
                    return TryParseStandardListItem(EntryListType.CompoundPatterns, parameters, ref Builder.CompoundPatterns, TryParseCheckCompoundPatternIntoCompoundPatterns);
                case AffixReaderCommandKind.CompoundRule:
                    return TryParseStandardListItem(EntryListType.CompoundRules, parameters, ref Builder.CompoundRules, TryParseCompoundRuleIntoList);
                case AffixReaderCommandKind.Map:
                    return TryParseStandardListItem(EntryListType.Map, parameters, ref Builder.RelatedCharacterMap, TryParseMapEntry);
                case AffixReaderCommandKind.Break:
                    return TryParseStandardListItem(EntryListType.Break, parameters, ref Builder.BreakPoints, TryParseBreak);
                case AffixReaderCommandKind.Version:
                    Builder.Version = parameters.ToString();
                    return true;
                case AffixReaderCommandKind.MaxNgramSuggestions:
                    Builder.MaxNgramSuggestions = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxNgramSuggestions.HasValue;
                case AffixReaderCommandKind.MaxDifferency:
                    Builder.MaxDifferency = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxDifferency.HasValue;
                case AffixReaderCommandKind.MaxCompoundSuggestions:
                    Builder.MaxCompoundSuggestions = IntEx.TryParseInvariant(parameters);
                    return Builder.MaxCompoundSuggestions.HasValue;
                case AffixReaderCommandKind.KeepCase:
                    return TryParseFlag(parameters, out Builder.KeepCase);
                case AffixReaderCommandKind.ForceUpperCase:
                    return TryParseFlag(parameters, out Builder.ForceUpperCase);
                case AffixReaderCommandKind.Warn:
                    return TryParseFlag(parameters, out Builder.Warn);
                case AffixReaderCommandKind.SubStandard:
                    return TryParseFlag(parameters, out Builder.SubStandard);
                case AffixReaderCommandKind.Prefix:
                case AffixReaderCommandKind.Suffix:
                    var parseAsPrefix = AffixReaderCommandKind.Prefix == command;
                    if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                    {
                        parseAsPrefix = !parseAsPrefix;
                    }

                    return parseAsPrefix
                        ? TryParseAffixIntoList(parameters, ref Builder.Prefixes)
                        : TryParseAffixIntoList(parameters, ref Builder.Suffixes);
                case AffixReaderCommandKind.AliasF:
                    return TryParseStandardListItem(EntryListType.AliasF, parameters, ref Builder.AliasF, TryParseAliasF);
                case AffixReaderCommandKind.AliasM:
                    return TryParseStandardListItem(EntryListType.AliasM, parameters, ref Builder.AliasM, TryParseAliasM);
                default:
                    return LogWarning($"Unknown parsed command {command}");
            }
        }

        private bool TryParseStandardListItem<T>(EntryListType entryListType, ReadOnlySpan<char> parameterText, ref List<T> entries, EntryParser<T> parse)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);

                if (IntEx.TryParseInvariant(parameterText, out int expectedSize) && expectedSize >= 0)
                {
                    if (entries == null)
                    {
                        entries = new List<T>(expectedSize);
                    }

                    return true;
                }
            }

            if (entries == null)
            {
                entries = new List<T>();
            }

            return parse(parameterText, entries);
        }

        private bool TryParseCompoundSyllable(ReadOnlySpan<char> parameters)
        {
            var ok = parameters.SplitOnTabOrSpace((part, i) =>
            {
                switch(i)
                {
                    case 0:
                        var maxValue = IntEx.TryParseInvariant(part);
                        if (maxValue.HasValue)
                        {
                            Builder.CompoundMaxSyllable = maxValue.GetValueOrDefault();
                            Builder.CompoundVowels = DefaultCompoundVowels;
                            return true;
                        }
                        return false;
                    case 1:
                        Builder.CompoundVowels = CharacterSet.Create(part);
                        return true;
                    default:
                        return false;
                }
            });

            if (!ok)
            {
                LogWarning("Failed to parse CompoundMaxSyllable value from: " + parameters.ToString());
            }

            return ok;
        }

        private static CultureInfo GetCultureFromLanguage(string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                return CultureInfo.InvariantCulture;
            }

            language = language.Replace('_', '-');

            try
            {
                return new CultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                var dashIndex = language.IndexOf('-');
                if (dashIndex > 0)
                {
                    return GetCultureFromLanguage(language.Substring(0, dashIndex));
                }
                else
                {
                    return CultureInfo.InvariantCulture;
                }
            }
            catch (ArgumentException)
            {
                return CultureInfo.InvariantCulture;
            }
        }

        private bool TryParsePhone(ReadOnlySpan<char> parameterText, List<PhoneticEntry> entries)
        {
            string rule = null;
            string replace = string.Empty;
            var ok = parameterText.SplitOnTabOrSpace((part, i) =>
            {
                switch (i)
                {
                    case 0:
                        rule = Builder.Dedup(part);
                        return true;
                    case 1:
                        replace = Builder.Dedup(part.Remove('_'));
                        return true;
                    default:
                        return false;
                }
            });

            if (rule == null)
            {
                return LogWarning("Failed to parse phone line: " + parameterText.ToString());
            }

            entries.Add(new PhoneticEntry(rule, replace));

            return true;
        }

        private bool TryParseMapEntry(ReadOnlySpan<char> parameterText, List<MapEntry> entries)
        {
            var values = new List<string>(parameterText.Length);

            for (int k = 0; k < parameterText.Length; ++k)
            {
                int chb = k;
                int che = k + 1;
                if (parameterText[k] == '(')
                {
                    var parpos = parameterText.IndexOf(')', k);
                    if (parpos >= 0)
                    {
                        chb = k + 1;
                        che = parpos;
                        k = parpos;
                    }
                }

                values.Add(parameterText.Slice(chb, che - chb).ToString());
            }

            entries.Add(MapEntry.TakeArray(Builder.DedupIntoArray(values)));

            return true;
        }

        private bool TryParseConv(ReadOnlySpan<char> parameterText, EntryListType entryListType, ref Dictionary<string, MultiReplacementEntry> entries)
        {
            if (!IsInitialized(entryListType))
            {
                SetInitialized(entryListType);
                
                if (IntEx.TryParseInvariant(ParseLeadingDigits(parameterText), out int expectedSize) && expectedSize >= 0)
                {
                    if (entries == null)
                    {
                        entries = new Dictionary<string, MultiReplacementEntry>(expectedSize);
                    }

                    return true;
                }
            }

            if (entries == null)
            {
                entries = new Dictionary<string, MultiReplacementEntry>();
            }

            string pattern1 = null;
            string pattern2 = null;
            parameterText.SplitOnTabOrSpace((part, i) =>
            {
                switch (i)
                {
                    case 0:
                        pattern1 = Builder.Dedup(part);
                        return true;
                    case 1:
                        pattern2 = Builder.Dedup(part);
                        return true;
                    default:
                        return false;
                }
            });

            if (pattern1 == null || pattern2 == null)
            {
                return LogWarning($"Bad {entryListType}: {parameterText.ToString()}");
            }

            entries.AddReplacementEntry(pattern1, pattern2);

            return true;
        }

        private bool TryParseBreak(ReadOnlySpan<char> parameterText, List<string> entries)
        {
            entries.Add(Builder.Dedup(parameterText));
            return true;
        }

        private bool TryParseAliasF(ReadOnlySpan<char> parameterText, List<FlagSet> entries)
        {
            entries.Add(Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(parameterText))));
            return true;
        }

        private bool TryParseAliasM(ReadOnlySpan<char> parameterText, List<MorphSet> entries)
        {
            if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
            {
                parameterText = parameterText.Reversed();
            }

            var parts = new List<string>();
            parameterText.SplitOnTabOrSpace((part, _) =>
            {
                if (!part.IsEmpty)
                {
                    parts.Add(part.ToString());
                }

                return true;
            });

            entries.Add(Builder.Dedup(MorphSet.TakeArray(Builder.DedupIntoArray(parts))));

            return true;
        }

        private bool TryParseCompoundRuleIntoList(ReadOnlySpan<char> parameterText, List<CompoundRule> entries)
        {
            var entryBuilder = new List<FlagValue>();

            if (parameterText.Contains('('))
            {
                for (var index = 0; index < parameterText.Length; index++)
                {
                    var indexBegin = index;
                    var indexEnd = indexBegin + 1;
                    if (parameterText[indexBegin] == '(')
                    {
                        var closeParenIndex = parameterText.IndexOf(')', indexEnd);
                        if (closeParenIndex >= 0)
                        {
                            indexBegin = indexEnd;
                            indexEnd = closeParenIndex;
                            index = closeParenIndex;
                        }
                    }

                    var beginFlagValue = new FlagValue(parameterText[indexBegin]);
                    if (beginFlagValue.IsWildcard)
                    {
                        entryBuilder.Add(beginFlagValue);
                    }
                    else
                    {
                        entryBuilder.AddRange(ParseFlagsInOrder(parameterText.Slice(indexBegin, indexEnd - indexBegin)));
                    }
                }
            }
            else
            {
                entryBuilder.AddRange(ParseFlagsInOrder(parameterText));
            }

            entries.Add(CompoundRule.Create(entryBuilder));
            return true;
        }

        private bool TryParseAffixIntoList<TEntry>(ReadOnlySpan<char> parameterText, ref List<AffixEntryGroup<TEntry>.Builder> groups)
            where TEntry : AffixEntry
            => TryParseAffixIntoList(parameterText.ToString(), ref groups);

        private bool TryParseAffixIntoList<TEntry>(string parameterText, ref List<AffixEntryGroup<TEntry>.Builder> groups)
            where TEntry : AffixEntry
        {
            if (groups == null)
            {
                groups = new List<AffixEntryGroup<TEntry>.Builder>();
            }

            var lineMatch = AffixLineRegex.Match(parameterText);
            if (!lineMatch.Success)
            {
                return LogWarning("Failed to parse affix line: " + parameterText);
            }

            var lineMatchGroups = lineMatch.Groups;

            if (!TryParseFlag(lineMatchGroups[1].Value.AsSpan(), out FlagValue characterFlag))
            {
                return LogWarning($"Failed to parse affix flag for {lineMatchGroups[1].Value} from: {parameterText}");
            }

            var affixGroup = groups.FindLast(g => g.AFlag == characterFlag);
            var contClass = FlagSet.Empty;

            if (lineMatchGroups[2].Success && lineMatchGroups[3].Success)
            {
                if (affixGroup != null)
                {
                    return LogWarning($"Duplicate affix group definition for {affixGroup.AFlag} from: {parameterText}");
                }

                var options = AffixEntryOptions.None;
                if (lineMatchGroups[2].Value.StartsWith('Y'))
                {
                    options |= AffixEntryOptions.CrossProduct;
                }
                if (Builder.IsAliasM)
                {
                    options |= AffixEntryOptions.AliasM;
                }
                if (Builder.IsAliasF)
                {
                    options |= AffixEntryOptions.AliasF;
                }

                IntEx.TryParseInvariant(lineMatchGroups[3].Value, out int expectedEntryCount);

                affixGroup = new AffixEntryGroup<TEntry>.Builder
                {
                    AFlag = characterFlag,
                    Options = options,
                    Entries = new List<TEntry>(expectedEntryCount)
                };

                groups.Add(affixGroup);

                return true;
            }

            if (lineMatchGroups[4].Success && lineMatchGroups[5].Success && lineMatchGroups[6].Success)
            {
                // piece 3 - is string to strip or 0 for null
                var strip = lineMatchGroups[4].Value;
                if (strip == "0")
                {
                    strip = string.Empty;
                }
                else if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                {
                    strip = strip.GetReversed();
                }

                // piece 4 - is affix string or 0 for null
                var affixInput = lineMatchGroups[5].Value;
                var affixSlashIndex = affixInput.IndexOf('/');
                StringBuilder affixText;
                if (affixSlashIndex >= 0)
                {
                    affixText = StringBuilderPool.Get(affixInput, 0, affixSlashIndex);

                    if (Builder.IsAliasF)
                    {
                        if (IntEx.TryParseInvariant(affixInput.AsSpan(affixSlashIndex + 1), out int aliasNumber) && aliasNumber > 0 && aliasNumber <= Builder.AliasF.Count)
                        {
                            contClass = Builder.AliasF[aliasNumber - 1];
                        }
                        else
                        {
                            return LogWarning($"Failed to parse contclasses from : {parameterText}");
                        }
                    }
                    else
                    {
                        contClass = Builder.Dedup(FlagSet.TakeArray(ParseFlagsInOrder(affixInput.AsSpan(affixSlashIndex + 1))));
                    }
                }
                else
                {
                    affixText = StringBuilderPool.Get(affixInput);
                }

                if (Builder.IgnoredChars != null && Builder.IgnoredChars.HasItems)
                {
                    affixText.RemoveChars(Builder.IgnoredChars);
                }

                if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                {
                    affixText.Reverse();
                }

                if (affixText.Length == 1 && affixText[0] == '0')
                {
                    affixText.Clear();
                }

                // piece 5 - is the conditions descriptions
                var conditionText = lineMatchGroups[6].Value;
                if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                {
                    conditionText = ReverseCondition(conditionText);
                }

                var conditions = CharacterCondition.Parse(conditionText);
                if (strip.Length != 0 && !conditions.AllowsAnySingleCharacter)
                {
                    bool isRedundant;
                    if (typeof(TEntry) == typeof(PrefixEntry))
                    {
                        isRedundant = conditions.IsOnlyPossibleMatch(strip);
                    }
                    else if (typeof(TEntry) == typeof(SuffixEntry))
                    {
                        isRedundant = conditions.IsOnlyPossibleMatch(strip);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    if (isRedundant)
                    {
                        conditions = CharacterConditionGroup.AllowAnySingleCharacter;
                    }
                }

                // piece 6
                MorphSet morph;
                if (lineMatchGroups[7].Success)
                {
                    var morphAffixText = lineMatchGroups[7].Value;
                    if (Builder.IsAliasM)
                    {
                        if (IntEx.TryParseInvariant(morphAffixText, out int morphNumber) && morphNumber > 0 && morphNumber <= Builder.AliasM.Count)
                        {
                            morph = Builder.AliasM[morphNumber - 1];
                        }
                        else
                        {
                            return LogWarning($"Failed to parse morph {morphAffixText} from: {parameterText}");
                        }
                    }
                    else
                    {
                        if (EnumEx.HasFlag(Builder.Options, AffixConfigOptions.ComplexPrefixes))
                        {
                            morphAffixText = morphAffixText.GetReversed();
                        }

                        morph = Builder.Dedup(MorphSet.TakeArray(Builder.DedupInPlace(morphAffixText.SplitOnTabOrSpace())));
                    }
                }
                else
                {
                    morph = MorphSet.Empty;
                }

                if (affixGroup == null)
                {
                    affixGroup = new AffixEntryGroup<TEntry>.Builder
                    {
                        AFlag = characterFlag,
                        Options = AffixEntryOptions.None,
                        Entries = new List<TEntry>()
                    };
                }

                if (!Builder.HasContClass && contClass.HasItems)
                {
                    Builder.HasContClass = true;
                }

                affixGroup.Entries.Add(CreateEntry<TEntry>(
                    Builder.Dedup(strip),
                    Builder.Dedup(StringBuilderPool.GetStringAndReturn(affixText)),
                    Builder.Dedup(conditions),
                    morph,
                    contClass));

                return true;
            }

            return LogWarning("Affix line not fully parsed: " + parameterText);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static TEntry CreateEntry<TEntry>(string strip,
            string affixText,
            CharacterConditionGroup conditions,
            MorphSet morph,
            FlagSet contClass)
            where TEntry : AffixEntry
        {
            if (typeof(TEntry) == typeof(PrefixEntry))
            {
                return (TEntry)((AffixEntry)new PrefixEntry(strip, affixText, conditions, morph, contClass));
            }
            if (typeof(TEntry) == typeof(SuffixEntry))
            {
                return (TEntry)((AffixEntry)new SuffixEntry(strip, affixText, conditions, morph, contClass));
            }

            throw new NotSupportedException();
        }

        private static string ReverseCondition(string conditionText)
        {
            if (string.IsNullOrEmpty(conditionText))
            {
                return conditionText;
            }

            var chars = StringBuilderPool.Get(conditionText);
            chars.Reverse();
            var neg = false;
            var lastIndex = chars.Length - 1;

            for (int k = lastIndex; k >= 0; k--)
            {
                switch (chars[k])
                {
                    case '[':
                        if (neg)
                        {
                            if (k < lastIndex)
                            {
                                chars[k + 1] = '[';
                            }
                        }
                        else
                        {
                            chars[k] = ']';
                        }

                        break;
                    case ']':
                        chars[k] = '[';
                        if (neg && k < lastIndex)
                        {
                            chars[k + 1] = '^';
                        }

                        neg = false;

                        break;
                    case '^':
                        if (k < lastIndex)
                        {
                            if (chars[k + 1] == ']')
                            {
                                neg = true;
                            }
                            else
                            {
                                chars[k + 1] = chars[k];
                            }
                        }

                        break;
                    default:
                        if (neg && k < lastIndex)
                        {
                            chars[k + 1] = chars[k];
                        }
                        break;
                }
            }

            return StringBuilderPool.GetStringAndReturn(chars);
        }

        private bool TryParseReplacements(ReadOnlySpan<char> parameterText, List<SingleReplacement> entries)
        {
            string pattern = null;
            string outString = string.Empty;
            ReplacementValueType type = ReplacementValueType.Med;

            parameterText.SplitOnTabOrSpace((part, i) =>
            {
                switch (i)
                {
                    case 0:
                        var hasStartingCarrot = part.StartsWith('^');
                        var hasTrailingDollar = part.EndsWith('$');

                        var patternBuilder = StringBuilderPool.Get(part);
                        type = ReplacementValueType.Med;

                        if (hasTrailingDollar)
                        {
                            type |= ReplacementValueType.Fin;
                            patternBuilder.Remove(patternBuilder.Length - 1, 1);
                        }

                        if (hasStartingCarrot)
                        {
                            type |= ReplacementValueType.Ini;
                            patternBuilder.Remove(0, 1);
                        }

                        patternBuilder.Replace('_', ' ');
                        pattern = StringBuilderPool.GetStringAndReturn(patternBuilder);
                        return true;
                    case 1:
                        outString = Builder.Dedup(part.Replace('_', ' '));
                        return true;
                    default:
                        return false;
                }
            });

            if (pattern == null)
            {
                return LogWarning("Failed to parse replacements from: " + parameterText.ToString());
            }

            entries.Add(new SingleReplacement(pattern, outString, type));

            return true;
        }

        private bool TryParseCheckCompoundPatternIntoCompoundPatterns(ReadOnlySpan<char> parameterText, List<PatternEntry> entries)
        {
            string pattern = null;
            string pattern2 = string.Empty;
            string pattern3 = string.Empty;
            FlagValue condition = default;
            FlagValue condition2 = default;
            bool failedParsePattern1 = false;
            bool failedParsePattern2 = false;

            parameterText.SplitOnTabOrSpace((part, i) =>
            {
                int slashIndex;
                switch (i)
                {
                    case 0:
                        slashIndex = part.IndexOf('/');
                        if (slashIndex >= 0)
                        {
                            condition = TryParseFlag(part.Slice(slashIndex + 1));
                            if (!condition.HasValue)
                            {
                                failedParsePattern1 = true;
                                return false;
                            }

                            part = part.Slice(0, slashIndex);
                        }

                        pattern = part.ToString();
                        return true;
                    case 1:
                        slashIndex = part.IndexOf('/');
                        if (slashIndex >= 0)
                        {
                            condition2 = TryParseFlag(part.Slice(slashIndex + 1));
                            if (!condition2.HasValue)
                            {
                                failedParsePattern2 = true;
                                return false;
                            }

                            part = part.Slice(0, slashIndex);
                        }

                        pattern2 = part.ToString();
                        return true;
                    case 2:
                        pattern3 = part.ToString();
                        Builder.EnableOptions(AffixConfigOptions.SimplifiedCompound);
                        return true;
                    default:
                        return false;
                }
            });

            if (pattern == null)
            {
                return LogWarning("Failed to parse compound pattern from: " + parameterText.ToString());
            }

            if (failedParsePattern1)
            {
                return LogWarning($"Failed to parse compound pattern 1 {pattern} from: {parameterText.ToString()}");
            }

            if (failedParsePattern2)
            {
                return LogWarning($"Failed to parse compound pattern 2 {pattern2} from: {parameterText.ToString()}");
            }

            entries.Add(new PatternEntry(
                Builder.Dedup(pattern),
                Builder.Dedup(pattern2),
                Builder.Dedup(pattern3),
                condition,
                condition2));

            return true;
        }

        private bool TrySetFlagMode(ReadOnlySpan<char> modeText)
        {
            if (modeText.IsEmpty)
            {
                return LogWarning("Attempt to set empty flag mode.");
            }

            var mode = TryParseFlagMode(modeText);
            if (mode.HasValue)
            {
                if (mode == Builder.FlagMode)
                {
                    return LogWarning("Redundant FlagMode: " + modeText.ToString());
                }

                Builder.FlagMode = mode.GetValueOrDefault();
                return true;
            }
            else
            {
                return LogWarning("Unknown FlagMode: " + modeText.ToString());
            }
        }

        private bool LogWarning(string text)
        {
            Builder.LogWarning(text);
            return false;
        }

        private ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded) =>
            HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(decoded, Builder.Encoding ?? Reader.CurrentEncoding);

        private FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.ParseFlagsInOrder(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char)
                : FlagValue.ParseFlagsInOrder(text, flagMode);
        }

        private bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value)
        {
            var flagMode = Builder.FlagMode;
            return flagMode == FlagMode.Uni
                ? FlagValue.TryParseFlag(ReDecodeConvertedStringAsUtf8(text), FlagMode.Char, out value)
                : FlagValue.TryParseFlag(text, flagMode, out value);
        }

        private FlagValue TryParseFlag(ReadOnlySpan<char> text) =>
            TryParseFlag(text, out FlagValue value)
                ? value
                : default;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool HasFlag(EntryListType value, EntryListType flag) => (value & flag) == flag;

        private static ReadOnlySpan<char> ParseLeadingDigits(ReadOnlySpan<char> text)
        {
            text = text.TrimStart();

            if (text.IsEmpty)
            {
                return text;
            }

            var firstNonDigitIndex = 0;
            for (; firstNonDigitIndex < text.Length && char.IsDigit(text[firstNonDigitIndex]); firstNonDigitIndex++)
            {
                ;
            }

            return (firstNonDigitIndex < text.Length)
                ? text.Slice(0, firstNonDigitIndex)
                : text;
        }

        private static AffixConfigOptions? TryParseFileBitFlagCommand(ReadOnlySpan<char> value)
        {
            if (value.Length >= 2)
            {
                switch (value[0])
                {
                    case 'C':
                    case 'c':
                        if (value.StartsWith("CHECK", StringComparison.OrdinalIgnoreCase))
                        {
                            value = value.Slice(5);
                            if (value.Equals("COMPOUNDDUP", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckCompoundDup;
                            }
                            if (value.Equals("COMPOUNDREP", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckCompoundRep;
                            }
                            if (value.Equals("COMPOUNDTRIPLE", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckCompoundTriple;
                            }
                            if (value.Equals("COMPOUNDCASE", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckCompoundCase;
                            }
                            if (value.Equals("NUM", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckNum;
                            }
                            if (value.Equals("SHARPS", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixConfigOptions.CheckSharps;
                            }
                        }
                        else if (value.Equals("COMPLEXPREFIXES", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.ComplexPrefixes;
                        }
                        else if (value.Equals("COMPOUNDMORESUFFIXES", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.CompoundMoreSuffixes;
                        }

                        break;

                    case 'F':
                    case 'f':
                        if (value.Equals("FULLSTRIP", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.FullStrip;
                        }
                        if (value.Equals("FORBIDWARN", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.ForbidWarn;
                        }

                        break;

                    case 'N':
                    case 'n':
                        if (value.Equals("NOSPLITSUGS", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.NoSplitSuggestions;
                        }

                        break;

                    case 'O':
                    case 'o':
                        if (value.Equals("ONLYMAXDIFF", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.OnlyMaxDiff;
                        }

                        break;

                    case 'S':
                    case 's':
                        if (value.Equals("SIMPLIFIEDTRIPLE", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.SimplifiedTriple;
                        }
                        if (value.Equals("SUGSWITHDOTS", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixConfigOptions.SuggestWithDots;
                        }

                        break;
                }
            }

            return null;
        }

        private static AffixReaderCommandKind? TryParseCommandKind(ReadOnlySpan<char> value)
        {
            if (value.Length >= 2)
            {
                switch(value[0])
                {
                    case 'A':
                    case 'a':
                        if (value.Equals("AF", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.AliasF;
                        }
                        if (value.Equals("AM", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.AliasM;
                        }

                        break;

                    case 'B':
                    case 'b':
                        if (value.Equals("BREAK", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Break;
                        }

                        break;

                    case 'C':
                    case 'c':
                        if (value.Length > 8 && value.StartsWith("COMPOUND", StringComparison.OrdinalIgnoreCase))
                        {
                            value = value.Slice(8);
                            if (value.Equals("BEGIN", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundBegin;
                            }
                            if (value.Equals("END", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundEnd;
                            }
                            if (value.Equals("FLAG", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundFlag;
                            }
                            if (value.Equals("FORBIDFLAG", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundForbidFlag;
                            }
                            if (value.Equals("MIDDLE", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundMiddle;
                            }
                            if (value.Equals("MIN", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundMin;
                            }
                            if (value.Equals("PERMITFLAG", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundPermitFlag;
                            }
                            if (value.Equals("ROOT", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundRoot;
                            }
                            if (value.Equals("RULE", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundRule;
                            }
                            if (value.Equals("SYLLABLE", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundSyllable;
                            }
                            if (value.Equals("WORDMAX", StringComparison.OrdinalIgnoreCase))
                            {
                                return AffixReaderCommandKind.CompoundWordMax;
                            }
                        }
                        else if (value.Equals("CIRCUMFIX", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Circumfix;
                        }
                        else if (value.Equals("CHECKCOMPOUNDPATTERN", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.CheckCompoundPattern;
                        }

                        break;

                    case 'F':
                    case 'f':
                        if (value.Equals("FLAG", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Flag;
                        }
                        if (value.Equals("FORBIDDENWORD", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.ForbiddenWord;
                        }
                        if (value.Equals("FORCEUCASE", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.ForceUpperCase;
                        }

                        break;

                    case 'I':
                    case 'i':
                        if (value.Equals("ICONV", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.InputConversions;
                        }
                        if (value.Equals("IGNORE", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Ignore;
                        }

                        break;

                    case 'K':
                    case 'k':
                        if (value.Equals("KEY", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.KeyString;
                        }
                        if (value.Equals("KEEPCASE", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.KeepCase;
                        }

                        break;

                    case 'L':
                    case 'l':
                        if (value.Equals("LANG", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Language;
                        } 
                        if (value.Equals("LEMMA_PRESENT", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.LemmaPresent;
                        }

                        break;

                    case 'M':
                    case 'm':
                        if (value.Equals("MAP", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Map;
                        }
                        if (value.Equals("MAXNGRAMSUGS", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.MaxNgramSuggestions;
                        }
                        if (value.Equals("MAXDIFF", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.MaxDifferency;
                        }
                        if (value.Equals("MAXCPDSUGS", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.MaxCompoundSuggestions;
                        }

                        break;

                    case 'N':
                    case 'n':
                        if (value.Equals("NEEDAFFIX", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.NeedAffix;
                        }
                        if (value.Equals("NOSUGGEST", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.NoSuggest;
                        }
                        if (value.Equals("NONGRAMSUGGEST", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.NoNGramSuggest;
                        }

                        break;

                    case 'O':
                    case 'o':
                        if (value.Equals("OCONV", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.OutputConversions;
                        }
                        if (value.Equals("ONLYINCOMPOUND", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.OnlyInCompound;
                        }

                        break;

                    case 'P':
                    case 'p':
                        if (value.Equals("PFX", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Prefix;
                        }
                        if (value.Equals("PSEUDOROOT", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.NeedAffix;
                        }
                        if (value.Equals("PHONE", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Phone;
                        }

                        break;

                    case 'R':
                    case 'r':
                        if (value.Equals("REP", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Replacement;
                        }

                        break;

                    case 'S':
                    case 's':
                        if (value.Equals("SFX", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Suffix;
                        }
                        if (value.Equals("SET", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.SetEncoding;
                        }
                        if (value.Equals("SYLLABLENUM", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.CompoundSyllableNum;
                        }
                        if (value.Equals("SUBSTANDARD", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.SubStandard;
                        }

                        break;

                    case 'T':
                    case 't':
                        if (value.Equals("TRY", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.TryString;
                        }

                        break;

                    case 'V':
                    case 'v':
                        if (value.Equals("VERSION", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Version;
                        }

                        break;

                    case 'W':
                    case 'w':
                        if (value.Equals("WORDCHARS", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.WordChars;
                        }
                        if (value.Equals("WARN", StringComparison.OrdinalIgnoreCase))
                        {
                            return AffixReaderCommandKind.Warn;
                        }

                        break;
                }
            }

            return default;
        }

        private static FlagMode? TryParseFlagMode(ReadOnlySpan<char> value)
        {
            if (value.Length >= 2)
            {
                switch(value[0])
                {
                    case 'C':
                    case 'c':
                        if (value.StartsWith("CHAR", StringComparison.OrdinalIgnoreCase))
                        {
                            return FlagMode.Char;
                        }
                        break;
                    case 'L':
                    case 'l':
                        if (value.StartsWith("LONG", StringComparison.OrdinalIgnoreCase))
                        {
                            return FlagMode.Long;
                        }
                        break;
                    case 'N':
                    case 'n':
                        if (value.StartsWith("NUM", StringComparison.OrdinalIgnoreCase))
                        {
                            return FlagMode.Num;
                        }
                        break;
                    case 'U':
                    case 'u':
                        if (value.StartsWith("UTF", StringComparison.OrdinalIgnoreCase) || value.StartsWith("UNI", StringComparison.OrdinalIgnoreCase))
                        {
                            return FlagMode.Uni;
                        }
                        break;
                }
            }

            return default;
        }

        private delegate bool EntryParser<T>(ReadOnlySpan<char> parameterText, List<T> entries);

        [Flags]
        private enum EntryListType : short
        {
            None = 0,
            Replacements = 1 << 0,
            CompoundRules = 1 << 1,
            CompoundPatterns = 1 << 2,
            AliasF = 1 << 3,
            AliasM = 1 << 4,
            Break = 1 << 5,
            Iconv = 1 << 6,
            Oconv = 1 << 7,
            Map = 1 << 8,
            Phone = 1 << 9
        }

        private enum AffixReaderCommandKind : byte
        {
            /// <summary>
            /// parse in the try string
            /// </summary>
            Flag,
            /// <summary>
            /// parse in the keyboard string
            /// </summary>
            KeyString,
            /// <summary>
            /// parse in the try string
            /// </summary>
            TryString,
            /// <summary>
            /// parse in the name of the character set used by the .dict and .aff
            /// </summary>
            SetEncoding,
            /// <summary>
            /// parse in the language for language specific codes
            /// </summary>
            Language,
            /// <summary>
            /// parse in the flag used by compound_check() method
            /// </summary>
            CompoundSyllableNum,
            /// <summary>
            /// parse in the extra word characters
            /// </summary>
            WordChars,
            /// <summary>
            /// parse in the ignored characters (for example, Arabic optional diacretics characters)
            /// </summary>
            Ignore,
            /// <summary>
            /// parse in the flag used by the controlled compound words
            /// </summary>
            CompoundFlag,
            /// <summary>
            /// parse in the flag used by compound words
            /// </summary>
            CompoundMiddle,
            /// <summary>
            /// parse in the flag used by compound words
            /// </summary>
            CompoundBegin,
            /// <summary>
            /// parse in the flag used by compound words
            /// </summary>
            CompoundEnd,
            /// <summary>
            /// parse in the data used by compound_check() method
            /// </summary>
            CompoundWordMax,
            /// <summary>
            /// parse in the minimal length for words in compounds
            /// </summary>
            CompoundMin,
            /// <summary>
            /// parse in the flag sign compounds in dictionary
            /// </summary>
            CompoundRoot,
            /// <summary>
            /// parse in the flag used by compound_check() method
            /// </summary>
            CompoundPermitFlag,
            /// <summary>
            /// parse in the flag used by compound_check() method
            /// </summary>
            CompoundForbidFlag,
            /// <summary>
            /// parse in the max. words and syllables in compounds
            /// </summary>
            CompoundSyllable,
            NoSuggest,
            NoNGramSuggest,
            /// <summary>
            /// parse in the flag used by forbidden words
            /// </summary>
            ForbiddenWord,
            /// <summary>
            /// parse in the flag used by forbidden words
            /// </summary>
            LemmaPresent,
            /// <summary>
            /// parse in the flag used by circumfixes
            /// </summary>
            Circumfix,
            /// <summary>
            /// parse in the flag used by fogemorphemes
            /// </summary>
            OnlyInCompound,
            /// <summary>
            /// parse in the flag used by `needaffixs'
            /// </summary>
            NeedAffix,
            /// <summary>
            /// parse in the typical fault correcting table
            /// </summary>
            Replacement,
            /// <summary>
            /// parse in the input conversion table
            /// </summary>
            InputConversions,
            /// <summary>
            /// parse in the output conversion table
            /// </summary>
            OutputConversions,
            /// <summary>
            /// parse in the phonetic conversion table
            /// </summary>
            Phone,
            /// <summary>
            /// parse in the checkcompoundpattern table
            /// </summary>
            CheckCompoundPattern,
            /// <summary>
            /// parse in the defcompound table
            /// </summary>
            CompoundRule,
            /// <summary>
            /// parse in the related character map table
            /// </summary>
            Map,
            /// <summary>
            /// parse in the word breakpoints table
            /// </summary>
            Break,
            Version,
            MaxNgramSuggestions,
            MaxDifferency,
            MaxCompoundSuggestions,
            /// <summary>
            /// parse in the flag used by forbidden words
            /// </summary>
            KeepCase,
            ForceUpperCase,
            Warn,
            SubStandard,
            Prefix,
            Suffix,
            AliasF,
            AliasM
        }
    }
}
