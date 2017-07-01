using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public class MultiReplacementTable :
#if NO_READONLYCOLLECTIONS
        IEnumerable<KeyValuePair<string, MultiReplacementEntry>>
#else
        IReadOnlyDictionary<string, MultiReplacementEntry>
#endif
    {
        public static readonly MultiReplacementTable Empty = TakeDictionary(new Dictionary<string, MultiReplacementEntry>(0));

        public static MultiReplacementTable Create(IEnumerable<KeyValuePair<string, MultiReplacementEntry>> replacements) =>
            replacements == null ? Empty : TakeDictionary(replacements.ToDictionary(s => s.Key, s => s.Value));

        internal static MultiReplacementTable TakeDictionary(Dictionary<string, MultiReplacementEntry> replacements) =>
            replacements == null ? Empty : new MultiReplacementTable(replacements);

        private MultiReplacementTable(Dictionary<string, MultiReplacementEntry> replacements) =>
            this.replacements = replacements;

        private Dictionary<string, MultiReplacementEntry> replacements;

        public MultiReplacementEntry this[string key] => replacements[key];

        public int Count => replacements.Count;

        public bool HasReplacements => replacements.Count != 0;

        public IEnumerable<string> Keys => replacements.Keys;

        public IEnumerable<MultiReplacementEntry> Values => replacements.Values;

        public bool ContainsKey(string key) => replacements.ContainsKey(key);

        public bool TryGetValue(string key, out MultiReplacementEntry value) => replacements.TryGetValue(key, out value);

        internal bool TryConvert(string text, out string converted)
        {
#if DEBUG
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
#endif

            var appliedConversion = false;

            if (text.Length == 0)
            {
                converted = text;
            }
            else
            {
                var convertedBuilder = StringBuilderPool.Get(text.Length);

                for (var i = 0; i < text.Length; i++)
                {
                    var replacementEntry = FindLargestMatchingConversion(text.Subslice(i));
                    if (replacementEntry != null)
                    {
                        var replacementText = replacementEntry.ExtractReplacementText(text.Length - i, i == 0);
                        if (!string.IsNullOrEmpty(replacementText))
                        {
                            convertedBuilder.Append(replacementText);
                            i += replacementEntry.Pattern.Length - 1;
                            appliedConversion = true;
                            continue;
                        }
                    }

                    convertedBuilder.Append(text[i]);
                }

                converted = StringBuilderPool.GetStringAndReturn(convertedBuilder);
            }

            return appliedConversion;
        }

        /// <summary>
        /// Finds a conversion matching the longest version of the given <paramref name="text"/> from the left.
        /// </summary>
        /// <param name="text">The text to find a matching input conversion for.</param>
        /// <returns>The best matching input conversion.</returns>
        /// <seealso cref="MultiReplacementEntry"/>
        internal MultiReplacementEntry FindLargestMatchingConversion(StringSlice text)
        {
            for (var searchLength = text.Length; searchLength > 0; searchLength--)
            {
                if (replacements.TryGetValue(text.Substring(0, searchLength), out MultiReplacementEntry entry))
                {
                    return entry;
                }
            }

            return null;
        }

        internal Dictionary<string, MultiReplacementEntry>.Enumerator GetEnumerator() => replacements.GetEnumerator();

        IEnumerator<KeyValuePair<string, MultiReplacementEntry>> IEnumerable<KeyValuePair<string, MultiReplacementEntry>>.GetEnumerator() => replacements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => replacements.GetEnumerator();
    }
}
