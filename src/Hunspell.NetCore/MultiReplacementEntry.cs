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
        public static bool AddReplacementEntry(this Dictionary<string, string[]> list, string pattern1, string pattern2)
        {
            if (string.IsNullOrEmpty(pattern1) || pattern2 == null)
            {
                return false;
            }

            ReplacementValueType type;
            var trailingUnderscore = pattern1.EndsWith('_');
            if (pattern1.StartsWith('_'))
            {
                if (trailingUnderscore)
                {
                    type = ReplacementValueType.Isol;
                    pattern1 = pattern1.Substring(1, pattern1.Length - 2);
                }
                else
                {
                    type = ReplacementValueType.Ini;
                    pattern1 = pattern1.Substring(1);
                }
            }
            else
            {
                if (trailingUnderscore)
                {
                    type = ReplacementValueType.Fin;
                    pattern1 = pattern1.SubstringFromEnd(1);
                }
                else
                {
                    type = ReplacementValueType.Med;
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
