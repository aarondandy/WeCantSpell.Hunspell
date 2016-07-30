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
        public static bool AddReplacementEntry(this Dictionary<string, string[]> list, string pattern1, string pattern2)
        {
            if (string.IsNullOrEmpty(pattern1) || string.IsNullOrEmpty(pattern2))
            {
                return false;
            }

            var leadingUnderscore = pattern1.StartsWith('_');
            var trailingUnderscore = pattern1.EndsWith('_');

            int type;
            if(leadingUnderscore)
            {
                if (trailingUnderscore)
                {
                    type = 3;
                    pattern1 = pattern1.Substring(1, pattern1.Length - 2);
                }
                else
                {
                    type = 1;
                    pattern1 = pattern1.Substring(1);
                }
            }
            else
            {
                if (trailingUnderscore)
                {
                    type = 2;
                    pattern1 = pattern1.SubstringFromEnd(1);
                }
                else
                {
                    type = 0;
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

            entry[type] = pattern2.Replace('_', ' ');

            return true;
        }

        public static ImmutableSortedDictionary<string, ReplacementEntry> ToImmutableReplacementEntries(this Dictionary<string, string[]> entries)
        {
            return entries.ToImmutableSortedDictionary(pair => pair.Key, pair => new ReplacementEntry(pair.Key, pair.Value));
        }
    }
}
