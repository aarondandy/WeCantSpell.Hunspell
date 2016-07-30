using Hunspell.Utilities;
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
            OutStrings = ImmutableArray.Create(outStrings);
        }

        /// <summary>
        /// Med, ini, fin, isol .
        /// </summary>
        public ImmutableArray<string> OutStrings { get; }

        public override string Med => this[ReplacementEntryType.Med];

        public override string Ini => this[ReplacementEntryType.Ini];

        public override string Fin => this[ReplacementEntryType.Fin];

        public override string Isol => this[ReplacementEntryType.Isol];

        public override string this[ReplacementEntryType type] => OutStrings[(int)type];
    }

    internal static class MultiReplacementEntryExtensions
    {
        public static bool AddReplacementEntry(this Dictionary<string, string[]> list, string pattern1, string pattern2)
        {
            if (string.IsNullOrEmpty(pattern1) || pattern2 == null)
            {
                return false;
            }

            ReplacementEntryType type;
            var trailingUnderscore = pattern1.EndsWith('_');
            if (pattern1.StartsWith('_'))
            {
                if (trailingUnderscore)
                {
                    type = ReplacementEntryType.Isol;
                    pattern1 = pattern1.Substring(1, pattern1.Length - 2);
                }
                else
                {
                    type = ReplacementEntryType.Ini;
                    pattern1 = pattern1.Substring(1);
                }
            }
            else
            {
                if (trailingUnderscore)
                {
                    type = ReplacementEntryType.Fin;
                    pattern1 = pattern1.SubstringFromEnd(1);
                }
                else
                {
                    type = ReplacementEntryType.Med;
                }
            }

            pattern1 = pattern1.Replace('_', ' ');

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
