using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell
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

            switch (type)
            {
                case ReplacementValueType.Med:
                    result.med = value;
                    result.ini = ini;
                    result.fin = fin;
                    result.isol = isol;
                    break;
                case ReplacementValueType.Ini:
                    result.med = med;
                    result.ini = value;
                    result.fin = fin;
                    result.isol = isol;
                    break;
                case ReplacementValueType.Fin:
                    result.med = med;
                    result.ini = ini;
                    result.fin = value;
                    result.isol = isol;
                    break;
                case ReplacementValueType.Isol:
                    result.med = med;
                    result.ini = ini;
                    result.fin = fin;
                    result.isol = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            return result;
        }
    }

    internal static class MultiReplacementEntryExtensions
    {
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
            if (list.TryGetValue(pattern1, out MultiReplacementEntry entry))
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
