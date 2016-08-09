using Hunspell.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
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

        public static async Task<Dictionary> ReadAsync(IDictionaryLineReader reader, AffixConfig affix)
        {
            var builder = new Dictionary.Builder
            {
                Affix = affix
            };

            var readerInstance = new DictionaryReader(builder, affix);

            string line;
            while (null != (line = await reader.ReadLineAsync()))
            {
                readerInstance.ParseLine(line);
            }

            return builder.ToDictionary();
        }

        public static async Task<Dictionary> ReadFileAsync(string filePath)
        {
            var affixFilePath = Path.ChangeExtension(filePath, "aff");
            var affix = await AffixReader.ReadFileAsync(affixFilePath);
            return await ReadFileAsync(filePath, affix);
        }

        public static async Task<Dictionary> ReadFileAsync(string filePath, AffixConfig affix)
        {
            using (var reader = new UtfStreamLineReader(filePath))
            {
                return await ReadAsync(reader, affix);
            }
        }

        private bool ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return false;
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
            ImmutableArray<FlagValue> flags;
            if (flagGroup.Success)
            {
                if (Affix.IsAliasF)
                {
                    int flagAliasNumber;
                    if (IntExtensions.TryParseInvariant(flagGroup.Value, out flagAliasNumber) && flagAliasNumber > 0 && flagAliasNumber <= Affix.AliasF.Length)
                    {
                        flags = Affix.AliasF[flagAliasNumber - 1];
                    }
                    else
                    {
                        // TODO: warn
                        return false;
                    }
                }
                else
                {
                    flags = FlagUtilities.DecodeFlags(Affix.FlagMode, flagGroup.Value).OrderBy(x => x).ToImmutableArray();
                }
            }
            else
            {
                flags = ImmutableArray<FlagValue>.Empty;
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
                if (IntExtensions.TryParseInvariant(initLineMatch.Groups[1].Value, out expectedSize))
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

        private bool AddWord(string word, ImmutableArray<FlagValue> flags, ImmutableArray<string> morphs)
        {
            return AddWord(word, flags, morphs, false)
                || AddWordCapitalized(word, flags, morphs, CapitalizationTypeUtilities.GetCapitalizationType(word));
        }

        private bool AddWord(string word, ImmutableArray<FlagValue> flags, ImmutableArray<string> morphs, bool onlyUpperCase)
        {
            if (!Affix.IgnoredChars.IsEmpty)
            {
                word = word.RemoveChars(Affix.IgnoredChars);
            }

            if (Affix.ComplexPrefixes)
            {
                word = word.Reverse();

                if (morphs.Length != 0 && !Affix.IsAliasM)
                {
                    if (Affix.ComplexPrefixes)
                    {
                        var morphBuilder = ImmutableArray.CreateBuilder<string>(morphs.Length);
                        for (int i = morphs.Length - 1; i >= 0; i--)
                        {
                            morphBuilder.Add(morphs[i].Reverse());
                        }

                        morphs = morphBuilder.ToImmutableArray();
                    }
                }
            }

            DictionaryEntryOptions options;
            if (morphs.Length != 0)
            {
                if (Affix.IsAliasM)
                {
                    options = DictionaryEntryOptions.AliasM;
                    var morphBuilder = ImmutableArray.CreateBuilder<string>(morphs.Length);
                    for (int i = 0; i < morphs.Length; i++)
                    {
                        var originalValue = morphs[i];
                        int morphNumber;

                        if (IntExtensions.TryParseInvariant(originalValue, out morphNumber) && morphNumber > 0 && morphNumber <= Affix.AliasM.Length)
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
                    if (existingEntry.Flags != null && existingEntry.Flags.Contains(SpecialFlags.OnlyUpcaseFlag))
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

        private bool AddWordCapitalized(string word, ImmutableArray<FlagValue> flags, ImmutableArray<string> morphs, CapitalizationType capType)
        {
            // add inner capitalized forms to handle the following allcap forms:
            // Mixed caps: OpenOffice.org -> OPENOFFICE.ORG
            // Allcaps with suffixes: CIA's -> CIA'S

            if (
                (
                    capType == CapitalizationType.Huh
                    || capType == CapitalizationType.HuhInit
                    || (capType == CapitalizationType.All && flags.Length != 0)
                )
                &&
                !flags.Contains(Affix.ForbiddenWord)
            )
            {
                flags = flags.Add(SpecialFlags.OnlyUpcaseFlag);
                word =
                    Affix.Culture.TextInfo.ToUpper(word.Substring(0, 1))
                    + Affix.Culture.TextInfo.ToLower(word.Substring(1));

                return AddWord(word, flags, morphs, true);
            }

            return false;
        }
    }
}
