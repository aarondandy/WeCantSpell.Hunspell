using Hunspell.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Hunspell
{
    public sealed class WordListReader
    {
        private WordListReader(WordList.Builder builder, AffixConfig affix)
        {
            Builder = builder;
            Affix = affix;
        }

        private static readonly Regex InitialLineRegex = new Regex(
            @"^\s*(\d+)\s*(?:[#].*)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private WordList.Builder Builder { get; }

        private AffixConfig Affix { get; }

        private bool hasInitialized;

        public static async Task<WordList> ReadAsync(IHunspellLineReader reader, AffixConfig affix)
        {
            var readerInstance = new WordListReader(new WordList.Builder(affix), affix);

            string line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.MoveToImmutable();
        }

        public static WordList Read(IHunspellLineReader reader, AffixConfig affix)
        {
            var readerInstance = new WordListReader(new WordList.Builder(affix), affix);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.MoveToImmutable();
        }

        public static async Task<WordList> ReadFileAsync(string filePath)
        {
            var affixFilePath = FindAffixFilePath(filePath);
            var affix = await AffixReader.ReadFileAsync(affixFilePath).ConfigureAwait(false);
            return await ReadFileAsync(filePath, affix).ConfigureAwait(false);
        }

        public static WordList ReadFile(string filePath)
        {
            var affixFilePath = FindAffixFilePath(filePath);
            var affix = AffixReader.ReadFile(affixFilePath);
            return ReadFile(filePath, affix);
        }

        public static string FindAffixFilePath(string dictionaryFilePath)
        {
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

        public static async Task<WordList> ReadFileAsync(string filePath, AffixConfig affix)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StaticEncodingLineReader(stream, affix.Encoding))
            {
                return await ReadAsync(reader, affix).ConfigureAwait(false);
            }
        }

        public static WordList ReadFile(string filePath, AffixConfig affix)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StaticEncodingLineReader(stream, affix.Encoding))
            {
                return Read(reader, affix);
            }
        }

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
            if (!string.IsNullOrEmpty(parsed.Flags))
            {
                if (Affix.IsAliasF)
                {
                    int flagAliasNumber;
                    FlagSet aliasedFlags;
                    if (IntEx.TryParseInvariant(parsed.Flags, out flagAliasNumber) && Affix.TryGetAliasF(flagAliasNumber, out aliasedFlags))
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
                    var utf8Flags = Encoding.UTF8.GetString(Affix.Encoding.GetBytes(parsed.Flags));
                    flags = FlagValue.ParseFlags(utf8Flags, FlagMode.Char);
                }
                else
                {
                    flags = FlagValue.ParseFlags(parsed.Flags, Affix.FlagMode);
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

                morphs = MorphSet.TakeArray(morphValues);
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
                int expectedSize;
                if (IntEx.TryParseInvariant(initLineMatch.Groups[1].Value, out expectedSize))
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

        private bool AddWord(string word, FlagSet flags, MorphSet morphs)
        {
            return AddWord(word, flags, morphs, false)
                || AddWordCapitalized(word, flags, morphs, CapitalizationTypeEx.GetCapitalizationType(word, Affix));
        }

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
                        int morphNumber;
                        MorphSet aliasedMorph;
                        if (IntEx.TryParseInvariant(originalValue, out morphNumber) && Affix.TryGetAliasM(morphNumber, out aliasedMorph))
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
            WordEntrySet entryList;
            if (!Builder.EntriesByRoot.TryGetValue(word, out entryList))
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
                        existingEntry = new WordEntry(existingEntry.Word, flags, existingEntry.Morphs, existingEntry.Options);
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
                entryList = WordEntrySet.CopyWithItemAdded(entryList, new WordEntry(word, flags, morphs, options));
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
                flags = FlagSet.Union(flags, SpecialFlags.OnlyUpcaseFlag);

                var textInfo = Affix.Culture.TextInfo;
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
            public string Flags;
            public string[] Morphs;

            private static readonly Regex MorphPartRegex = new Regex(
                @"\G([\t ]+(?<morphs>[^\t ]+))*[\t ]*$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            public static ParsedWordLine Parse(string line)
            {
                var firstNonDelimiterPosition = IndexOfNonDelimiter(line, 0);
                if (firstNonDelimiterPosition >= 0)
                {
                    var endOfWordAndFlagsPosition = IndexOfDelimiter(line, firstNonDelimiterPosition + 1);
                    if (endOfWordAndFlagsPosition < 0)
                    {
                        endOfWordAndFlagsPosition = line.Length;
                    }

                    var flagsDelimiterPosition = IndexOfFlagsDelimiter(line, firstNonDelimiterPosition, endOfWordAndFlagsPosition);

                    string word;
                    string flagsPart;
                    if (flagsDelimiterPosition < 0)
                    {
                        word = line.Substring(firstNonDelimiterPosition, endOfWordAndFlagsPosition - firstNonDelimiterPosition);
                        flagsPart = null;
                    }
                    else
                    {
                        word = line.Substring(firstNonDelimiterPosition, flagsDelimiterPosition - firstNonDelimiterPosition);
                        flagsPart = line.Substring(flagsDelimiterPosition + 1, endOfWordAndFlagsPosition - flagsDelimiterPosition - 1);
                    }

                    if (!string.IsNullOrEmpty(word))
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

            private static int IndexOfNonDelimiter(string text, int startIndex)
            {
                for (var i = startIndex; i < text.Length; i++)
                {
                    if (IsNotDelimiter(text[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }

            private static int IndexOfDelimiter(string text, int startIndex)
            {
                for (var i = startIndex; i < text.Length; i++)
                {
                    if (IsDelimiter(text[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private static bool IsDelimiter(char c)
            {
                return c == ' ' || c == '\t';
            }

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private static bool IsNotDelimiter(char c)
            {
                return c != ' ' && c != '\t';
            }
        }
    }
}
