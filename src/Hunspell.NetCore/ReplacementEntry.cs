using Hunspell.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public class ReplacementEntry
    {
        public ReplacementEntry(string pattern, string[] outStrings)
        {
            Pattern = pattern;
            OutStrings = ImmutableArray.Create(outStrings);
        }

        public string Pattern { get; }

        /// <summary>
        /// Med, ini, fin, isol .
        /// </summary>
        public ImmutableArray<string> OutStrings { get; }

        public string Med => OutStrings[(int)Type.Med];

        public string Ini => OutStrings[(int)Type.Ini];

        public string Fin => OutStrings[(int)Type.Fin];

        public string Isol => OutStrings[(int)Type.Isol];

        public enum Type : int
        {
            Med = 0,
            Ini = 1,
            Fin = 2,
            Isol = 3
        }
    }

    internal static class ReplacementEntryExtensions
    {
        public static bool AddReplacementEntry(this SortedDictionary<string, string[]> list, string pattern1, string pattern2)
        {
            if (string.IsNullOrEmpty(pattern1) || string.IsNullOrEmpty(pattern2))
            {
                return false;
            }

            int type = 0;

            // analyse word context
            if (pattern1.StartsWith('_'))
            {
                pattern1 = pattern1.Substring(1);
                type += 1;
            }

            if (pattern1.EndsWith('_'))
            {
                type += 2;
                pattern1 = pattern1.Substring(0, pattern1.Length - 1);
            }

            pattern1 = pattern1.Replace('_', ' ');
            pattern2 = pattern2.Replace('_', ' ');
            string[] entry;

            // find existing entry
            if (!list.TryGetValue(pattern1, out entry))
            {
                // make a new entry if none exists
                entry = new string[4];
                list.Add(pattern1, entry);
            }

            entry[type] = pattern2;

            return true;
        }

        public static ImmutableSortedDictionary<string, ReplacementEntry> ToImmutableReplacementEntries(this SortedDictionary<string, string[]> entries)
        {
            return entries.ToImmutableSortedDictionary(pair => pair.Key, pair => new ReplacementEntry(pair.Key, pair.Value));
        }
    }
}
