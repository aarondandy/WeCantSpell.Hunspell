using Hunspell.Utilities;
using System.Collections.Generic;

namespace Hunspell
{
    public class ReplacementEntry
    {
        public enum Type : int
        {
            Med = 0,
            Ini = 1,
            Fin = 2,
            Isol = 3
        }

        public ReplacementEntry(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }

        /// <summary>
        /// Med, ini, fin, isol .
        /// </summary>
        public string[] OutStrings { get; } = new string[4];

        public string Med
        {
            get { return OutStrings[(int)Type.Med]; }
            set { OutStrings[(int)Type.Med] = value; }
        }

        public string Ini
        {
            get { return OutStrings[(int)Type.Ini]; }
            set { OutStrings[(int)Type.Ini] = value; }
        }

        public string Fin
        {
            get { return OutStrings[(int)Type.Fin]; }
            set { OutStrings[(int)Type.Fin] = value; }
        }

        public string Isol
        {
            get { return OutStrings[(int)Type.Isol]; }
            set { OutStrings[(int)Type.Isol] = value; }
        }
    }

    public static class ReplacementEntryExtensions
    {
        public static bool Add(this SortedDictionary<string, ReplacementEntry> list, string pattern1, string pattern2)
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
            ReplacementEntry entry;

            // find existing entry
            if (!list.TryGetValue(pattern1, out entry))
            {
                // make a new entry if none exists
                entry = new ReplacementEntry(pattern1);
                list.Add(entry.Pattern, entry);
            }

            entry.OutStrings[type] = pattern2;

            return true;
        }
    }
}
