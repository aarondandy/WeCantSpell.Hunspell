using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;

namespace Hunspell
{
    public sealed class MultiReplacementEntry : ReplacementEntry
    {
        public MultiReplacementEntry(string pattern)
            : base(pattern)
        {
        }

        public MultiReplacementEntry(string pattern, ReplacementValueType type, string value)
            : base(pattern)
        {
            if (type == ReplacementValueType.Med)
            {
                med = value;
            }
            else if (type == ReplacementValueType.Ini)
            {
                ini = value;
            }
            else if (type == ReplacementValueType.Fin)
            {
                fin = value;
            }
            else if (type == ReplacementValueType.Isol)
            {
                isol = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        private string med;
        private string ini;
        private string fin;
        private string isol;

        public override string Med => med;

        public override string Ini => ini;

        public override string Fin => fin;

        public override string Isol => isol;

        public override string this[ReplacementValueType type]
        {
            get
            {
                if (type == ReplacementValueType.Med)
                {
                    return med;
                }
                if (type == ReplacementValueType.Ini)
                {
                    return ini;
                }
                if (type == ReplacementValueType.Fin)
                {
                    return fin;
                }
                if (type == ReplacementValueType.Isol)
                {
                    return isol;
                }

                throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public MultiReplacementEntry With(ReplacementValueType type, string value)
        {
            var result = new MultiReplacementEntry(Pattern);

            if (type == ReplacementValueType.Med)
            {
                result.med = value;
                result.ini = ini;
                result.fin = fin;
                result.isol = isol;
            }
            else if (type == ReplacementValueType.Ini)
            {
                result.med = med;
                result.ini = value;
                result.fin = fin;
                result.isol = isol;
            }
            else if (type == ReplacementValueType.Fin)
            {
                result.med = med;
                result.ini = ini;
                result.fin = value;
                result.isol = isol;
            }
            else if (type == ReplacementValueType.Isol)
            {
                result.med = med;
                result.ini = ini;
                result.fin = fin;
                result.isol = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            return result;
        }
    }

    internal static class MultiReplacementEntryExtensions
    {
        public static bool TryConvert(this Dictionary<string, MultiReplacementEntry> entries, string text, out string converted)
        {
            var convertedBuilder = StringBuilderPool.Get(text.Length);

            var appliedConversion = false;
            for (var i = 0; i < text.Length; i++)
            {
                var replacementEntry = entries.FindLargestMatchingConversion(text.Substring(i));
                var replacementText = replacementEntry == null
                    ? string.Empty
                    : ExtractReplacementText(text.Length - i, replacementEntry, i == 0);

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
        public static MultiReplacementEntry FindLargestMatchingConversion(this Dictionary<string, MultiReplacementEntry> conversions, string text)
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

        public static bool AddReplacementEntry(this Dictionary<string, MultiReplacementEntry> list, string pattern1, string pattern2)
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
            pattern2 = pattern2.Replace('_', ' ');

            // find existing entry
            MultiReplacementEntry entry;
            if (list.TryGetValue(pattern1, out entry))
            {
                entry = entry.With(type, pattern2);
            }
            else
            {
                // make a new entry if none exists
                entry = new MultiReplacementEntry(pattern1, type, pattern2);
            }

            list[pattern1] = entry;

            return true;
        }
    }
}
