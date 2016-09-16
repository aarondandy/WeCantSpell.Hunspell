using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public class MultiReplacementTable
        : IReadOnlyDictionary<string, MultiReplacementEntry>
    {
        public static readonly MultiReplacementTable Empty = TakeDictionary(new Dictionary<string, MultiReplacementEntry>(0));

        private Dictionary<string, MultiReplacementEntry> replacements;

        private MultiReplacementTable(Dictionary<string, MultiReplacementEntry> replacements)
        {
            this.replacements = replacements;
        }

        public MultiReplacementEntry this[string key] => replacements[key];

        public int Count => replacements.Count;

        public bool HasReplacements => replacements.Count != 0;

        public IEnumerable<string> Keys => replacements.Keys;

        public IEnumerable<MultiReplacementEntry> Values => replacements.Values;

        internal static MultiReplacementTable TakeDictionary(Dictionary<string, MultiReplacementEntry> replacements) =>
            replacements == null ? Empty : new MultiReplacementTable(replacements);

        public static MultiReplacementTable Create(IEnumerable<KeyValuePair<string, MultiReplacementEntry>> replacements) =>
            TakeDictionary(replacements.ToDictionary(s => s.Key, s => s.Value));

        public bool ContainsKey(string key) => replacements.ContainsKey(key);

        public bool TryGetValue(string key, out MultiReplacementEntry value) => replacements.TryGetValue(key, out value);

        public bool TryConvert(string text, out string converted)
        {
            var convertedBuilder = StringBuilderPool.Get(text.Length);

            var appliedConversion = false;
            for (var i = 0; i < text.Length; i++)
            {
                var replacementEntry = FindLargestMatchingConversion(text.Substring(i));
                var replacementText = replacementEntry == null
                    ? string.Empty
                    : replacementEntry.ExtractReplacementText(text.Length - i, i == 0);

                if (replacementText.Length == 0)
                {
                    convertedBuilder.Append(text[i]);
                }
                else
                {
                    convertedBuilder.Append(replacementText);
                    i += replacementEntry.Pattern.Length - 1;
                    appliedConversion = true;
                }
            }

            converted = StringBuilderPool.GetStringAndReturn(convertedBuilder);

            return appliedConversion;
        }

        /// <summary>
        /// Finds a conversion matching the longest version of the given <paramref name="text"/> from the left.
        /// </summary>
        /// <param name="text">The text to find a matching input conversion for.</param>
        /// <returns>The best matching input conversion.</returns>
        /// <seealso cref="MultiReplacementEntry"/>
        public MultiReplacementEntry FindLargestMatchingConversion(string text)
        {
            for (var searchLength = text.Length; searchLength > 0; searchLength--)
            {
                MultiReplacementEntry entry = null;
                if (replacements.TryGetValue(text.Substring(0, searchLength), out entry))
                {
                    return entry;
                }
            }

            return null;
        }

        public IEnumerator<KeyValuePair<string, MultiReplacementEntry>> GetEnumerator() => replacements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => replacements.GetEnumerator();
    }
}
