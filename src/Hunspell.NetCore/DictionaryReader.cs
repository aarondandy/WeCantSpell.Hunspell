using Hunspell.Utilities;
using System;
using System.Collections.Generic;
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
            @"^[\t ]*((?:[^\t \\\/]|\\\/|\\)+)(?:\/([^\t ]+))+?(?:[\t ]+([^\t ]+))*[\t ]*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private Dictionary.Builder Builder { get; }

        private AffixConfig Affix { get; }

        private bool hasInitialized;

        public static async Task<Dictionary> ReadAsync(IDictionaryLineReader reader, AffixConfig affix)
        {
            var builder = new Dictionary.Builder();
            var readerInstance = new DictionaryReader(builder, affix);

            string line;
            while (null != (line = await reader.ReadLineAsync()))
            {
                readerInstance.ParseLine(line);
            }

            return builder.ToDictionary();
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
                Builder.Entries = new List<DictionaryEntry>();
            }

            var match = WordLineRegex.Match(line);
            if (!match.Success)
            {
                return false;
            }

            var word = match.Groups[1].Value.Replace(@"\/", @"/");

            List<int> flags;
            if (match.Groups[2].Success)
            {
                flags = FlagUtilities.DecodeFlags(Affix.FlagMode, match.Groups[2].Value).ToList();
            }
            else
            {
                flags = null;
            }

            List<string> morphs;
            if (match.Groups[3].Success)
            {
                morphs = new List<string>(match.Groups[3].Captures.Count);
                foreach (Capture morphCapture in match.Groups[3].Captures)
                {
                    morphs.Add(morphCapture.Value);
                }
            }
            else
            {
                morphs = null;
            }

            throw new NotImplementedException(); // Builder.Entries.Add(new DictionaryEntry(word, flags, morphs));

            return true;
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
                        Builder.Entries = new List<DictionaryEntry>(expectedSize);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
