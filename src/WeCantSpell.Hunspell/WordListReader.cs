using System;
using WeCantSpell.Hunspell.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class WordListReader
    {
        private bool hasInitialized;

        private WordListReader(WordList.Builder builder, AffixConfig affix)
        {
            Builder = builder ?? new WordList.Builder(affix);
            Affix = affix;
        }

        private static readonly Regex InitialLineRegex = new Regex(
            @"^\s*(\d+)\s*(?:[#].*)?$",
#if !NO_COMPILEDREGEX
            RegexOptions.Compiled |
#endif
            RegexOptions.CultureInvariant);

        private WordList.Builder Builder { get; }

        private AffixConfig Affix { get; }

        private TextInfo TextInfo
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Affix.Culture.TextInfo;
        }

#if !NO_ASYNC
        public static async Task<WordList> ReadAsync(Stream dictionaryStream, Stream affixStream)
        {
            if (dictionaryStream == null)
            {
                throw new ArgumentNullException(nameof(dictionaryStream));
            }
            if (affixStream == null)
            {
                throw new ArgumentNullException(nameof(affixStream));
            }

            var affixBuilder = new AffixConfig.Builder();
            var affix = await AffixReader.ReadAsync(affixStream, affixBuilder).ConfigureAwait(false);
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper, affixBuilder.StringDeduper);
            return await ReadAsync(dictionaryStream, affix, wordListBuilder).ConfigureAwait(false);
        }

        public static async Task<WordList> ReadAsync(IHunspellLineReader dictionaryReader, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryReader == null)
            {
                throw new ArgumentNullException(nameof(dictionaryReader));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            var readerInstance = new WordListReader(builder, affix);

            string line;
            while ((line = await dictionaryReader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.MoveToImmutable();
        }

#if !NO_IO_FILE
        public static async Task<WordList> ReadFileAsync(string dictionaryFilePath)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }

            var affixFilePath = FindAffixFilePath(dictionaryFilePath);
            return await ReadFileAsync(dictionaryFilePath, affixFilePath).ConfigureAwait(false);
        }

        public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, string affixFilePath)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }
            if (affixFilePath == null)
            {
                throw new ArgumentNullException(nameof(affixFilePath));
            }

            var affixBuilder = new AffixConfig.Builder();
            var affix = await AffixReader.ReadFileAsync(affixFilePath, affixBuilder).ConfigureAwait(false);
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper, affixBuilder.StringDeduper);
            return await ReadFileAsync(dictionaryFilePath, affix, wordListBuilder).ConfigureAwait(false);
        }
#endif

        public static async Task<WordList> ReadAsync(Stream dictionaryStream, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryStream == null)
            {
                throw new ArgumentNullException(nameof(dictionaryStream));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            using (var reader = new StaticEncodingLineReader(dictionaryStream, affix.Encoding))
            {
                return await ReadAsync(reader, affix, builder).ConfigureAwait(false);
            }
        }

#if !NO_IO_FILE
        public static async Task<WordList> ReadFileAsync(string dictionaryFilePath, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            using (var stream = FileStreamEx.OpenAsyncReadFileStream(dictionaryFilePath))
            {
                return await ReadAsync(stream, affix, builder).ConfigureAwait(false);
            }
        }
#endif

#endif

        public static WordList Read(Stream dictionaryStream, Stream affixStream)
        {
            if (dictionaryStream == null)
            {
                throw new ArgumentNullException(nameof(dictionaryStream));
            }
            if (affixStream == null)
            {
                throw new ArgumentNullException(nameof(affixStream));
            }

            var affixBuilder = new AffixConfig.Builder();
            var affix = AffixReader.Read(affixStream, affixBuilder);
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper, affixBuilder.StringDeduper);
            return Read(dictionaryStream, affix, wordListBuilder);
        }

        public static WordList Read(IHunspellLineReader dictionaryReader, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryReader == null)
            {
                throw new ArgumentNullException(nameof(dictionaryReader));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            var readerInstance = new WordListReader(builder, affix);

            string line;
            while ((line = dictionaryReader.ReadLine()) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.MoveToImmutable();
        }

#if !NO_IO_FILE
        public static WordList ReadFile(string dictionaryFilePath)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }

            var affixFilePath = FindAffixFilePath(dictionaryFilePath);
            return ReadFile(dictionaryFilePath, AffixReader.ReadFile(affixFilePath));
        }

        public static WordList ReadFile(string dictionaryFilePath, string affixFilePath)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }
            if (affixFilePath == null)
            {
                throw new ArgumentNullException(nameof(affixFilePath));
            }

            var affixBuilder = new AffixConfig.Builder();
            var affix = AffixReader.ReadFile(affixFilePath, affixBuilder);
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper, affixBuilder.StringDeduper);
            return ReadFile(dictionaryFilePath, affix, wordListBuilder);
        }

        private static string FindAffixFilePath(string dictionaryFilePath)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }

            var directoryName = Path.GetDirectoryName(dictionaryFilePath);
            if (!string.IsNullOrEmpty(directoryName))
            {
                var locatedAffFile = Directory.GetFiles(directoryName, Path.GetFileNameWithoutExtension(dictionaryFilePath) + ".*", SearchOption.TopDirectoryOnly)
                    .FirstOrDefault(affFilePath => ".AFF".Equals(Path.GetExtension(affFilePath), System.StringComparison.OrdinalIgnoreCase));

                if (locatedAffFile != null)
                {
                    return locatedAffFile;
                }
            }

            return Path.ChangeExtension(dictionaryFilePath, "aff");
        }
#endif

        public static WordList Read(Stream dictionaryStream, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryStream == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            using (var reader = new StaticEncodingLineReader(dictionaryStream, affix.Encoding))
            {
                return Read(reader, affix, builder);
            }
        }

#if !NO_IO_FILE
        public static WordList ReadFile(string dictionaryFilePath, AffixConfig affix, WordList.Builder builder = null)
        {
            if (dictionaryFilePath == null)
            {
                throw new ArgumentNullException(nameof(dictionaryFilePath));
            }
            if (affix == null)
            {
                throw new ArgumentNullException(nameof(affix));
            }

            using (var stream = FileStreamEx.OpenReadFileStream(dictionaryFilePath))
            {
                return Read(stream, affix, builder);
            }
        }
#endif

        private bool ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return true;
            }

            if (!hasInitialized && AttemptToProcessInitializationLine(line))
            {
                return true;
            }

            if (Builder.EntriesByRoot == null)
            {
                Builder.InitializeEntriesByRoot(-1);
            }

            var parsed = ParsedWordLine.Parse(line);
            if (string.IsNullOrEmpty(parsed.Word))
            {
                return false;
            }

            FlagSet flags;
            if (!parsed.Flags.IsEmpty)
            {
                if (Affix.IsAliasF)
                {
                    if (IntEx.TryParseInvariant(parsed.Flags.ToString(), out int flagAliasNumber) && Affix.TryGetAliasF(flagAliasNumber, out FlagSet aliasedFlags))
                    {
                        flags = aliasedFlags;
                    }
                    else
                    {
                        // TODO: warn
                        return false;
                    }
                }
                else if (Affix.FlagMode == FlagMode.Uni)
                {
                    flags = Builder.Dedup(FlagValue.ParseFlags(HunspellTextFunctions.ReDecodeConvertedStringAsUtf8(parsed.Flags, Affix.Encoding), FlagMode.Char));
                }
                else
                {
                    flags = Builder.Dedup(FlagValue.ParseFlags(parsed.Flags, Affix.FlagMode));
                }
            }
            else
            {
                flags = FlagSet.Empty;
            }

            MorphSet morphs;
            if (parsed.Morphs != null && parsed.Morphs.Length != 0)
            {
                var morphValues = new string[parsed.Morphs.Length];
                for (int i = 0; i < parsed.Morphs.Length; i++)
                {
                    morphValues[i] = parsed.Morphs[i];
                }

                morphs = Builder.Dedup(MorphSet.TakeArray(morphValues));
            }
            else
            {
                morphs = MorphSet.Empty;
            }

            return AddWord(parsed.Word, flags, morphs);
        }

        private bool AttemptToProcessInitializationLine(string line)
        {
            hasInitialized = true;

            var initLineMatch = InitialLineRegex.Match(line);
            if (initLineMatch.Success)
            {
                if (IntEx.TryParseInvariant(initLineMatch.Groups[1].Value, out int expectedSize))
                {
                    if (Builder.EntriesByRoot == null)
                    {
                        Builder.InitializeEntriesByRoot(expectedSize);
                    }

                    return true;
                }
            }

            return false;
        }

        private bool AddWord(string word, FlagSet flags, MorphSet morphs) =>
            AddWord(word, flags, morphs, false)
            || AddWordCapitalized(word, flags, morphs, CapitalizationTypeEx.GetCapitalizationType(word, TextInfo));

        private bool AddWord(string word, FlagSet flags, MorphSet morphs, bool onlyUpperCase)
        {
            if (Affix.IgnoredChars.HasItems)
            {
                word = word.RemoveChars(Affix.IgnoredChars);
            }

            if (Affix.ComplexPrefixes)
            {
                word = word.Reverse();

                if (morphs.HasItems && !Affix.IsAliasM)
                {
                    var newMorphs = new string[morphs.Count];
                    for (int i = 0; i < morphs.Count; i++)
                    {
                        newMorphs[i] = morphs[morphs.Count - i - 1].Reverse();
                    }

                    morphs = MorphSet.TakeArray(newMorphs);
                }
            }

            WordEntryOptions options;
            if (morphs.HasItems)
            {
                if (Affix.IsAliasM)
                {
                    options = WordEntryOptions.AliasM;
                    var morphBuilder = new List<string>();
                    foreach (var originalValue in morphs)
                    {
                        if (IntEx.TryParseInvariant(originalValue, out int morphNumber) && Affix.TryGetAliasM(morphNumber, out MorphSet aliasedMorph))
                        {
                            morphBuilder.AddRange(aliasedMorph);
                        }
                        else
                        {
                            morphBuilder.Add(originalValue);
                        }
                    }

                    morphs = MorphSet.Create(morphBuilder);
                }
                else
                {
                    options = WordEntryOptions.None;
                }

                if (morphs.AnyStartsWith(MorphologicalTags.Phon))
                {
                    options |= WordEntryOptions.Phon;
                }
            }
            else
            {
                options = WordEntryOptions.None;
            }

            bool saveEntryList = false;
            word = Builder.Dedup(word);
            if (!Builder.EntriesByRoot.TryGetValue(word, out WordEntrySet entryList))
            {
                saveEntryList = true;
                entryList = WordEntrySet.Empty;
            }

            var upperCaseHomonym = false;
            for (var i = 0; i < entryList.Count; i++)
            {
                var existingEntry = entryList[i];

                if (!onlyUpperCase)
                {
                    if (existingEntry.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                    {
                        existingEntry = new WordEntry(
                            existingEntry.Word,
                            flags,
                            existingEntry.Morphs,
                            existingEntry.Options);
                        entryList.DestructiveReplace(i, existingEntry);
                        return false;
                    }
                }
                else
                {
                    upperCaseHomonym = true;
                }
            }

            if (!upperCaseHomonym)
            {
                saveEntryList = true;
                entryList = WordEntrySet.CopyWithItemAdded(entryList, new WordEntry(
                    word,
                    flags,
                    Builder.Dedup(morphs),
                    options));
            }

            if (saveEntryList)
            {
                Builder.EntriesByRoot[word] = entryList;
            }

            return false;
        }

        private bool AddWordCapitalized(string word, FlagSet flags, MorphSet morphs, CapitalizationType capType)
        {
            // add inner capitalized forms to handle the following allcap forms:
            // Mixed caps: OpenOffice.org -> OPENOFFICE.ORG
            // Allcaps with suffixes: CIA's -> CIA'S

            if (
                (
                    capType == CapitalizationType.Huh
                    || capType == CapitalizationType.HuhInit
                    || (capType == CapitalizationType.All && flags.HasItems)
                )
                &&
                !flags.Contains(Affix.ForbiddenWord)
            )
            {
                flags = Builder.Dedup(FlagSet.Union(flags, SpecialFlags.OnlyUpcaseFlag));

                var textInfo = TextInfo;
                var initCapBuilder = StringBuilderPool.Get(word);
                if (initCapBuilder.Length > 0)
                {
                    initCapBuilder[0] = textInfo.ToUpper(initCapBuilder[0]);

                    for (var i = 1; i < initCapBuilder.Length; i++)
                    {
                        initCapBuilder[i] = textInfo.ToLower(initCapBuilder[i]);
                    }
                }

                return AddWord(StringBuilderPool.GetStringAndReturn(initCapBuilder), flags, morphs, true);
            }

            return false;
        }

        private struct ParsedWordLine
        {
            public string Word;
            public StringSlice Flags;
            public string[] Morphs;

            private static readonly Regex MorphPartRegex = new Regex(
                @"\G([\t ]+(?<morphs>[^\t ]+))*[\t ]*$",
#if !NO_COMPILEDREGEX
                RegexOptions.Compiled |
#endif
                RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            public static ParsedWordLine Parse(string line)
            {
                var firstNonDelimiterPosition = StringEx.IndexOfNonSpaceOrTab(line, 0);
                if (firstNonDelimiterPosition >= 0)
                {
                    var endOfWordAndFlagsPosition = FindIndexOfFirstMorphByColonChar(line, firstNonDelimiterPosition);
                    if (endOfWordAndFlagsPosition <= firstNonDelimiterPosition)
                    {
                        endOfWordAndFlagsPosition = line.IndexOf('\t', firstNonDelimiterPosition);
                        if (endOfWordAndFlagsPosition < 0)
                        {
                            endOfWordAndFlagsPosition = line.Length;
                        }
                    }

                    while(endOfWordAndFlagsPosition > firstNonDelimiterPosition && StringEx.IsSpaceOrTab(line[endOfWordAndFlagsPosition - 1]))
                    {
                        --endOfWordAndFlagsPosition;
                    }

                    var flagsDelimiterPosition = IndexOfFlagsDelimiter(line, firstNonDelimiterPosition, endOfWordAndFlagsPosition);

                    string word;
                    StringSlice flagsPart;
                    if (flagsDelimiterPosition < 0)
                    {
                        word = line.Substring(firstNonDelimiterPosition, endOfWordAndFlagsPosition - firstNonDelimiterPosition);
                        flagsPart = StringSlice.Empty;
                    }
                    else
                    {
                        word = line.Substring(firstNonDelimiterPosition, flagsDelimiterPosition - firstNonDelimiterPosition);
                        flagsPart = line.Subslice(flagsDelimiterPosition + 1, endOfWordAndFlagsPosition - flagsDelimiterPosition - 1);
                    }

                    if (word.Length != 0)
                    {
                        var morphGroup = endOfWordAndFlagsPosition >= 0 && endOfWordAndFlagsPosition != line.Length
                            ? MorphPartRegex.Match(line, endOfWordAndFlagsPosition).Groups["morphs"]
                            : null;

                        return new ParsedWordLine
                        {
                            Word = word.Replace(@"\/", @"/"),
                            Flags = flagsPart,
                            Morphs = morphGroup != null && morphGroup.Success ? GetCapturesAsTest(morphGroup.Captures) : null
                        };
                    }
                }

                return default(ParsedWordLine);
            }

            private static int FindIndexOfFirstMorphByColonChar(string text, int index)
            {
                while ((index = text.IndexOf(':', index)) >= 0)
                {
                    var checkLocation = index - 3;
                    if (checkLocation >= 0 && StringEx.IsSpaceOrTab(text[checkLocation]))
                    {
                        return checkLocation;
                    }

                    index = index + 1;
                }

                return -1;
            }

            private static string[] GetCapturesAsTest(CaptureCollection collection)
            {
                var results = new string[collection.Count];
                for (var i = 0; i < collection.Count; i++)
                {
                    results[i] = collection[i].Value;
                }

                return results;
            }

            private static int IndexOfFlagsDelimiter(string text, int startIndex, int boundaryIndex)
            {
                // NOTE: the first character is ignored as a single slash should be treated as a word
                for (var i = startIndex + 1; i < boundaryIndex; i++)
                {
                    if (text[i] == '/' && text[i - 1] != '\\')
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }
}
