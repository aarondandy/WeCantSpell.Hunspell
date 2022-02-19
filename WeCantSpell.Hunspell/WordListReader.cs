﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class WordListReader
    {
        private static readonly Regex InitialLineRegex = new Regex(
            @"^\s*(\d+)\s*(?:[#].*)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private WordListReader(WordList.Builder builder, AffixConfig affix)
        {
            Builder = builder ?? new WordList.Builder(affix);
            Affix = affix;
        }

        private bool hasInitialized;

        private WordList.Builder Builder { get; }

        private AffixConfig Affix { get; }

        private TextInfo TextInfo
        {
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get => Affix.Culture.TextInfo;
        }

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
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper);
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
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper);
            return await ReadFileAsync(dictionaryFilePath, affix, wordListBuilder).ConfigureAwait(false);
        }

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
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper);
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
            var wordListBuilder = new WordList.Builder(affix, affixBuilder.FlagSetDeduper, affixBuilder.MorphSetDeduper);
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

        private bool ParseLine(string line)
        {
#if DEBUG
            if (line == null) throw new ArgumentNullException(nameof(line));
#endif

            if (line.Length == 0)
            {
                return true;
            }

            if (!hasInitialized)
            {
                if (AttemptToProcessInitializationLine(line))
                {
                    return true;
                }

                Builder.InitializeEntriesByRoot(-1);
            }

            var parsed = ParsedWordLine.Parse(line);
            if (parsed.Word.IsEmpty)
            {
                return false;
            }

            FlagSet flags;
            if (!parsed.Flags.IsEmpty)
            {
                if (Affix.IsAliasF)
                {
                    if (IntEx.TryParseInvariant(parsed.Flags, out int flagAliasNumber) && Affix.TryGetAliasF(flagAliasNumber, out FlagSet aliasedFlags))
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

            var morphValues = (parsed.Morphs != null && parsed.Morphs.Length != 0)
                ? parsed.Morphs
                : ArrayEx<string>.Empty;

            return AddWord(parsed.Word.ToString(), flags, morphValues);
        }

        private bool AttemptToProcessInitializationLine(string line)
        {
            hasInitialized = true;

            var initLineMatch = InitialLineRegex.Match(line);
            if (initLineMatch.Success)
            {
                if (IntEx.TryParseInvariant(initLineMatch.Groups[1].Value, out int expectedSize))
                {
                    Builder.InitializeEntriesByRoot(expectedSize);

                    return true;
                }
            }

            return false;
        }

        private bool AddWord(string word, FlagSet flags, string[] morphs)
        {
            if (Affix.IgnoredChars.HasItems)
            {
                word = word.WithoutChars(Affix.IgnoredChars);
            }

            if (Affix.ComplexPrefixes)
            {
                word = word.GetReversed();

                if (morphs.Length != 0 && !Affix.IsAliasM)
                {
                    morphs = MorphSet.CreateReversed(morphs);
                }
            }

            var capType = HunspellTextFunctions.GetCapitalizationType(word, TextInfo);
            return AddWord(word, flags, morphs, false, capType)
                || AddWordCapitalized(word, flags, morphs, capType);
        }

        private string[] AddWord_HandleMorph(string[] morphs, string word, CapitalizationType capType, ref WordEntryOptions options)
        {
            if (Affix.IsAliasM)
            {
                options |= WordEntryOptions.AliasM;
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

                morphs = morphBuilder.ToArray();
            }

            using (var morphPhonEnumerator = morphs.Where(m => m != null && m.StartsWith(MorphologicalTags.Phon)).GetEnumerator())
            {
                if (morphPhonEnumerator.MoveNext())
                {
                    options |= WordEntryOptions.Phon;
                    // store ph: fields (pronounciation, misspellings, old orthography etc.)
                    // of a morphological description in reptable to use in REP replacements.
                    if (Builder.PhoneticReplacements == null)
                    {
                        Builder.PhoneticReplacements = new List<SingleReplacement>();
                    }

                    do
                    {
                        var ph = morphPhonEnumerator.Current.AsSpan(MorphologicalTags.Phon.Length);
                        if (ph.Length == 0)
                        {
                            continue;
                        }

                        ReadOnlySpan<char> wordpart;
                        // dictionary based REP replacement, separated by "->"
                        // for example "pretty ph:prity ph:priti->pretti" to handle
                        // both prity -> pretty and pritier -> prettiest suggestions.
                        int strippatt = ph.IndexOf("->".AsSpan());
                        if (strippatt > 0 && strippatt < (ph.Length - 2))
                        {
                            wordpart = ph.Slice(strippatt + 2);
                            ph = ph.Slice(0, strippatt);
                        }
                        else
                        {
                            wordpart = word.AsSpan();
                        }

                        // when the ph: field ends with the character *,
                        // strip last character of the pattern and the replacement
                        // to match in REP suggestions also at character changes,
                        // for example, "pretty ph:prity*" results "prit->prett"
                        // REP replacement instead of "prity->pretty", to get
                        // prity->pretty and pritiest->prettiest suggestions.
                        if (ph.EndsWith('*'))
                        {
                            if (ph.Length > 2 && wordpart.Length > 1)
                            {
                                ph = ph.Slice(0, ph.Length - 2);
                                wordpart = wordpart.Slice(0, word.Length - 1);
                            }
                        }

                        var phString = ph.ToString();
                        var wordpartString = wordpart.ToString();

                        // capitalize lowercase pattern for capitalized words to support
                        // good suggestions also for capitalized misspellings, eg.
                        // Wednesday ph:wendsay
                        // results wendsay -> Wednesday and Wendsay -> Wednesday, too.
                        if (capType == CapitalizationType.Init)
                        {
                            var phCapitalized = HunspellTextFunctions.MakeInitCap(phString, Affix.Culture.TextInfo);
                            if (phCapitalized.Length != 0)
                            {
                                // add also lowercase word in the case of German or
                                // Hungarian to support lowercase suggestions lowercased by
                                // compound word generation or derivational suffixes
                                // (for example by adjectival suffix "-i" of geographical
                                // names in Hungarian:
                                // Massachusetts ph:messzecsuzec
                                // messzecsuzeci -> massachusettsi (adjective)
                                // For lowercasing by conditional PFX rules, see
                                // tests/germancompounding test example or the
                                // Hungarian dictionary.)
                                if (Affix.IsGerman || Affix.IsHungarian)
                                {
                                    var wordpartLower = HunspellTextFunctions.MakeAllSmall(wordpartString, Affix.Culture.TextInfo);
                                    Builder.PhoneticReplacements.Add(new SingleReplacement(phString, wordpartLower, ReplacementValueType.Med));
                                }

                                Builder.PhoneticReplacements.Add(new SingleReplacement(phCapitalized, wordpartString, ReplacementValueType.Med));
                            }
                        }

                        Builder.PhoneticReplacements.Add(new SingleReplacement(phString, wordpartString, ReplacementValueType.Med));
                    }
                    while (morphPhonEnumerator.MoveNext());
                }
            }

            return morphs;
        }

        private bool AddWord(string word, FlagSet flags, string[] morphs, bool onlyUpperCase, CapitalizationType capType)
        {
            // store the description string or its pointer
            var options = capType == CapitalizationType.Init ? WordEntryOptions.InitCap : WordEntryOptions.None;
            if (morphs.Length != 0)
            {
                morphs = AddWord_HandleMorph(morphs, word, capType, ref options);
            }

            var details = Builder.GetOrCreateDetailList(word);

            var upperCaseHomonym = false;
            if (!onlyUpperCase)
            {
                for (var i = 0; i < details.Count; i++)
                {
                    var existingEntry = details[i];
                    if (existingEntry.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                    {
                        details[i] = Builder.Dedup(new WordEntryDetail(flags, existingEntry.Morphs, existingEntry.Options));
                        return false;
                    }
                }
            }
            else if (details.Count != 0)
            {
                upperCaseHomonym = true;
            }

            if (!upperCaseHomonym)
            {
                details.Add(
                    Builder.Dedup(
                        new WordEntryDetail(
                            flags,
                            Builder.Dedup(MorphSet.TakeArray(morphs)),
                            options)));
            }

            return false;
        }

        private bool AddWordCapitalized(string word, FlagSet flags, string[] morphs, CapitalizationType capType)
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
                word = HunspellTextFunctions.MakeTitleCase(word, Affix.Culture);
                return AddWord(word, flags, morphs, true, CapitalizationType.Init);
            }

            return false;
        }

        private readonly ref struct ParsedWordLine
        {
            public readonly ReadOnlySpan<char> Word;
            public readonly ReadOnlySpan<char> Flags;
            public readonly string[] Morphs;

            private ParsedWordLine(ReadOnlySpan<char> word, ReadOnlySpan<char> flags, string[] morphs)
            {
                Word = word;
                Flags = flags;
                Morphs = morphs;
            }

            private static readonly Regex MorphPartRegex = new Regex(
                @"\G([\t ]+(?<morphs>[^\t ]+))*[\t ]*$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

            public static ParsedWordLine Parse(string line)
            {
#if DEBUG
                if (line == null) throw new ArgumentNullException(nameof(line));
#endif

                int firstNonDelimiterPosition = 0;
                for (; firstNonDelimiterPosition < line.Length && line[firstNonDelimiterPosition].IsTabOrSpace(); ++firstNonDelimiterPosition) ;
                if (firstNonDelimiterPosition >= line.Length)
                {
                    return default;
                }

                var endOfWordAndFlagsPosition = FindIndexOfFirstMorphByColonChar(line, firstNonDelimiterPosition);
                if (endOfWordAndFlagsPosition <= firstNonDelimiterPosition)
                {
                    endOfWordAndFlagsPosition = line.IndexOf('\t', firstNonDelimiterPosition);
                    if (endOfWordAndFlagsPosition < 0)
                    {
                        endOfWordAndFlagsPosition = line.Length;
                    }
                }

                for(; endOfWordAndFlagsPosition > firstNonDelimiterPosition && line[endOfWordAndFlagsPosition - 1].IsTabOrSpace(); --endOfWordAndFlagsPosition) ;

                var flagsDelimiterPosition = IndexOfFlagsDelimiter(line, firstNonDelimiterPosition, endOfWordAndFlagsPosition);

                ReadOnlySpan<char> word;
                ReadOnlySpan<char> flagsPart;
                if (flagsDelimiterPosition < 0)
                {
                    word = line.AsSpan(firstNonDelimiterPosition, endOfWordAndFlagsPosition - firstNonDelimiterPosition);
                    flagsPart = ReadOnlySpan<char>.Empty;
                }
                else
                {
                    word = line.AsSpan(firstNonDelimiterPosition, flagsDelimiterPosition - firstNonDelimiterPosition);
                    flagsPart = line.AsSpan(flagsDelimiterPosition + 1, endOfWordAndFlagsPosition - flagsDelimiterPosition - 1);
                }

                if (!word.IsEmpty)
                {
                    var morphGroup = endOfWordAndFlagsPosition >= 0 && endOfWordAndFlagsPosition != line.Length
                        ? MorphPartRegex.Match(line, endOfWordAndFlagsPosition).Groups["morphs"]
                        : null;

                    return new ParsedWordLine(
                        word: word.Replace(@"\/", @"/"),
                        flags: flagsPart,
                        morphs: morphGroup != null && morphGroup.Success ? GetCapturesAsTest(morphGroup.Captures) : null);
                }

                return default;
            }

            private static int FindIndexOfFirstMorphByColonChar(string text, int index)
            {
                while ((index = text.IndexOf(':', index)) >= 0)
                {
                    var checkLocation = index - 3;
                    if (checkLocation >= 0 && text[checkLocation].IsTabOrSpace())
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
