using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public sealed class MultiReplacementEntry : ReplacementEntry
    {
        public MultiReplacementEntry(string pattern, string[] outStrings)
            : base(pattern)
        {
            if (outStrings.Length < 4)
            {
                throw new ArgumentException(nameof(outStrings));
            }

            OutStrings = ImmutableArray.Create(outStrings);
        }

        /// <summary>
        /// Med, ini, fin, isol .
        /// </summary>
        public ImmutableArray<string> OutStrings { get; }

        public override string Med => this[ReplacementValueType.Med];

        public override string Ini => this[ReplacementValueType.Ini];

        public override string Fin => this[ReplacementValueType.Fin];

        public override string Isol => this[ReplacementValueType.Isol];

        public override string this[ReplacementValueType type] => OutStrings[(int)type];
    }

    internal static class MultiReplacementEntryExtensions
    {
        public static bool TryConvert(this ImmutableSortedDictionary<string, MultiReplacementEntry> entries, string input, out string converted)
        {
            var convertedBuilder = StringBuilderPool.Get(input.Length);

            var appliedConversion = false;
            for (var i = 0; i < input.Length; i++)
            {
                var replacementEntry = entries.FindLargestMatchingConversion(input.Substring(i));
                var replacementText = replacementEntry == null
                    ? string.Empty
                    : ExtractReplacementText(input.Length - i, replacementEntry, i == 0);

                if (replacementText.Length == 0)
                {
                    convertedBuilder.Append(input[i]);
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

        public static string ExtractReplacementText(int remainingCharactersToReplace, ReplacementEntry entry, bool atStart)
        {
            var type = remainingCharactersToReplace == entry.Pattern.Length
                ? (atStart ? ReplacementValueType.Isol : ReplacementValueType.Fin)
                : (atStart ? ReplacementValueType.Ini : ReplacementValueType.Med);

            while (type != ReplacementValueType.Med && string.IsNullOrEmpty(entry[type]))
            {
                type = (type == ReplacementValueType.Fin && !atStart) ? ReplacementValueType.Med : type - 1;
            }

            return entry[type] ?? string.Empty;
        }

        /// <summary>
        /// Finds a conversion matching the longest version of the given <paramref name="text"/> from the left.
        /// </summary>
        /// <param name="text">The text to find a matching input conversion for.</param>
        /// <param name="conversions">The conversions to search within.</param>
        /// <returns>The best matching input conversion.</returns>
        /// <seealso cref="MultiReplacementEntry"/>
        public static MultiReplacementEntry FindLargestMatchingConversion(this ImmutableSortedDictionary<string, MultiReplacementEntry> conversions, string text)
        {
            for (var searchLength = text.Length; searchLength > 0; searchLength--)
            {
                MultiReplacementEntry entry = null;
                if (conversions.TryGetValue(text.Substring(0, searchLength), out entry))
                {
                    return entry;
                }
            }

            return null;
        }

        public static bool AddReplacementEntry(this Dictionary<string, string[]> list, string pattern1, string pattern2)
        {
            if (string.IsNullOrEmpty(pattern1) || pattern2 == null)
            {
                return false;
            }

            var pattern1Builder = StringBuilderPool.Get(pattern1);
            ReplacementValueType type;
            var trailingUnderscore = pattern1Builder.EndsWith('_');
            if (pattern1Builder.StartsWith('_'))
            {
                if (trailingUnderscore)
                {
                    type = ReplacementValueType.Isol;
                    pattern1Builder.Remove(pattern1Builder.Length - 1, 1);
                }
                else
                {
                    type = ReplacementValueType.Ini;
                }

                pattern1Builder.Remove(0, 1);
            }
            else
            {
                if (trailingUnderscore)
                {
                    type = ReplacementValueType.Fin;
                    pattern1Builder.Remove(pattern1Builder.Length - 1, 1);
                }
                else
                {
                    type = ReplacementValueType.Med;
                }
            }

            pattern1Builder.Replace('_', ' ');

            pattern1 = StringBuilderPool.GetStringAndReturn(pattern1Builder);

            // find existing entry
            string[] entry;
            if (!list.TryGetValue(pattern1, out entry))
            {
                // make a new entry if none exists
                entry = new string[4];
                list.Add(pattern1, entry);
            }

            entry[(int)type] = pattern2.Replace('_', ' ');

            return true;
        }

        public static ImmutableSortedDictionary<string, MultiReplacementEntry> ToImmutableReplacementEntries(this Dictionary<string, string[]> entries)
        {
            return entries.ToImmutableSortedDictionary(pair => pair.Key, pair => new MultiReplacementEntry(pair.Key, pair.Value), StringComparer.Ordinal);
        }
    }
}
