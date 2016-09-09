using Hunspell.Infrastructure;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hunspell
{
    public sealed class DictionaryReader
    {
        private DictionaryReader(Dictionary.Builder builder, AffixConfig affix)
        {
            Builder = builder;
            Affix = affix;
        }

        private static readonly Regex InitialLineRegex = new Regex(
            @"^\s*(\d+)\s*(?:[#].*)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex WordLineRegex = new Regex(
            @"^[\t ]*(?<word>[^\t ]+?)((?<!\\)[/](?<flags>[^\t ]+))?([\t ]+(?<morphs>[^\t ]+))*[\t ]*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

        private Dictionary.Builder Builder { get; }

        private AffixConfig Affix { get; }

        private bool hasInitialized;

        public static async Task<Dictionary> ReadAsync(IHunspellLineReader reader, AffixConfig affix)
        {
            var readerInstance = new DictionaryReader(new Dictionary.Builder(affix), affix);

            string line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.ToDictionary();
        }

        public static Dictionary Read(IHunspellLineReader reader, AffixConfig affix)
        {
            var readerInstance = new DictionaryReader(new Dictionary.Builder(affix), affix);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                readerInstance.ParseLine(line);
            }

            return readerInstance.Builder.ToDictionary();
        }

        public static async Task<Dictionary> ReadFileAsync(string filePath)
        {
            var affixFilePath = FindAffixFilePath(filePath);
            var affix = await AffixReader.ReadFileAsync(affixFilePath).ConfigureAwait(false);
            return await ReadFileAsync(filePath, affix).ConfigureAwait(false);
        }

        public static Dictionary ReadFile(string filePath)
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

        public static async Task<Dictionary> ReadFileAsync(string filePath, AffixConfig affix)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StaticEncodingLineReader(stream, affix.Encoding))
            {
                return await ReadAsync(reader, affix).ConfigureAwait(false);
            }
        }

        public static Dictionary ReadFile(string filePath, AffixConfig affix)
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

            if (Builder.Entries == null)
            {
                Builder.Entries = new Dictionary<string, List<DictionaryEntry>>();
            }

            var match = WordLineRegex.Match(line);
            if (!match.Success)
            {
                return false;
            }

            var word = match.Groups["word"].Value.Replace(@"\/", @"/");

            var flagGroup = match.Groups["flags"];
            ImmutableSortedSet<FlagValue> flags;
            if (flagGroup.Success)
            {
                if (Affix.IsAliasF)
                {
                    int flagAliasNumber;
                    if (IntEx.TryParseInvariant(flagGroup.Value, out flagAliasNumber) && flagAliasNumber > 0 && flagAliasNumber <= Affix.AliasF.Length)
                    {
                        flags = Affix.AliasF[flagAliasNumber - 1];
                    }
                    else
                    {
                        // TODO: warn
                        return false;
                    }
                }
                else if (Affix.FlagMode == FlagMode.Uni)
                {
                    var utf8Flags = Encoding.UTF8.GetString(Affix.Encoding.GetBytes(flagGroup.Value));
                    flags = FlagValue.ParseFlags(utf8Flags, FlagMode.Char).ToImmutableSortedSet();
                }
                else
                {
                    flags = FlagValue.ParseFlags(flagGroup.Value, Affix.FlagMode).ToImmutableSortedSet();
                }
            }
            else
            {
                flags = ImmutableSortedSet<FlagValue>.Empty;
            }

            ImmutableArray<string> morphs;
            var morphGroup = match.Groups["morphs"];
            if (morphGroup.Success)
            {
                var morphBuilder = ImmutableArray.CreateBuilder<string>(morphGroup.Captures.Count);
                for (int i = 0; i < morphGroup.Captures.Count; i++)
                {
                    morphBuilder.Add(morphGroup.Captures[i].Value);
                }

                morphs = morphBuilder.ToImmutableArray();
            }
            else
            {
                morphs = ImmutableArray<string>.Empty;
            }

            return AddWord(word, flags, morphs);
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
                    if (Builder.Entries == null)
                    {
                        Builder.Entries = new Dictionary<string, List<DictionaryEntry>>(expectedSize);
                    }

                    return true;
                }
            }

            return false;
        }

        private bool AddWord(string word, ImmutableSortedSet<FlagValue> flags, ImmutableArray<string> morphs)
        {
            return AddWord(word, flags, morphs, false)
                || AddWordCapitalized(word, flags, morphs, CapitalizationTypeEx.GetCapitalizationType(word, Affix));
        }

        private bool AddWord(string word, ImmutableSortedSet<FlagValue> flags, ImmutableArray<string> morphs, bool onlyUpperCase)
        {
            if (Affix.HasIgnoredChars)
            {
                word = word.RemoveChars(Affix.IgnoredChars);
            }

            if (Affix.ComplexPrefixes)
            {
                word = word.Reverse();

                if (!morphs.IsDefaultOrEmpty && !Affix.IsAliasM)
                {
                    var morphBuilder = ImmutableArray.CreateBuilder<string>(morphs.Length);
                    for (int i = morphs.Length - 1; i >= 0; i--)
                    {
                        morphBuilder.Add(morphs[i].Reverse());
                    }

                    morphs = morphBuilder.ToImmutableArray();
                }
            }

            DictionaryEntryOptions options;
            if (!morphs.IsDefaultOrEmpty)
            {
                if (Affix.IsAliasM)
                {
                    options = DictionaryEntryOptions.AliasM;
                    var morphBuilder = ImmutableArray.CreateBuilder<string>(morphs.Length);
                    for (int i = 0; i < morphs.Length; i++)
                    {
                        var originalValue = morphs[i];
                        int morphNumber;

                        if (IntEx.TryParseInvariant(originalValue, out morphNumber) && morphNumber > 0 && morphNumber <= Affix.AliasM.Length)
                        {
                            morphBuilder.AddRange(Affix.AliasM[morphNumber - 1]);
                        }
                        else
                        {
                            morphBuilder.Add(originalValue);
                        }
                    }

                    morphs = morphBuilder.ToImmutableArray();
                }
                else
                {
                    options = DictionaryEntryOptions.None;
                }

                if (morphs.Any(m => m.StartsWith(MorphologicalTags.Phon)))
                {
                    options |= DictionaryEntryOptions.Phon;
                }
            }
            else
            {
                options = DictionaryEntryOptions.None;
            }

            List<DictionaryEntry> entryList;
            if (!Builder.Entries.TryGetValue(word, out entryList) || entryList == null)
            {
                entryList = new List<DictionaryEntry>();
                Builder.Entries[word] = entryList;
            }

            var upperCaseHomonym = false;
            for (var i = 0; i < entryList.Count; i++)
            {
                var existingEntry = entryList[i];

                if (!onlyUpperCase)
                {
                    if (existingEntry.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                    {
                        existingEntry = new DictionaryEntry(existingEntry.Word, flags, existingEntry.Morphs, existingEntry.Options);
                        entryList[i] = existingEntry;
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
                entryList.Add(new DictionaryEntry(word, flags, morphs, options));
            }

            return false;
        }

        private bool AddWordCapitalized(string word, ImmutableSortedSet<FlagValue> flags, ImmutableArray<string> morphs, CapitalizationType capType)
        {
            // add inner capitalized forms to handle the following allcap forms:
            // Mixed caps: OpenOffice.org -> OPENOFFICE.ORG
            // Allcaps with suffixes: CIA's -> CIA'S

            if (
                (
                    capType == CapitalizationType.Huh
                    || capType == CapitalizationType.HuhInit
                    || (capType == CapitalizationType.All && !flags.IsEmpty)
                )
                &&
                !flags.Contains(Affix.ForbiddenWord)
            )
            {
                flags = flags.Add(SpecialFlags.OnlyUpcaseFlag);

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
    }
}
