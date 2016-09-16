using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CompoundRuleTable :
        IReadOnlyList<CompoundRule>
    {
        public static readonly CompoundRuleTable Empty = TakeList(new List<CompoundRule>(0));

        private List<CompoundRule> rules;

        private CompoundRuleTable(List<CompoundRule> rules)
        {
            this.rules = rules;
        }

        public bool HasRules => rules.Count != 0;

        public CompoundRule this[int index]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return rules[index];
            }
        }

        public int Count
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get
            {
                return rules.Count;
            }
        }

        internal static CompoundRuleTable TakeList(List<CompoundRule> rules) => new CompoundRuleTable(rules);

        public static CompoundRuleTable Create(IEnumerable<CompoundRule> rules) => rules == null ? Empty : TakeList(rules.ToList());

        public bool EntryContainsRuleFlags(DictionaryEntry rv)
        {
            foreach (var rule in rules)
            {
                foreach (var flag in rule)
                {
                    if (!flag.Equals('*') && !flag.Equals('?') && rv.ContainsFlag(flag))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CompoundCheck(Dictionary<int, DictionaryEntry> words, int wnum, bool all)
        {
            var bt = 0;
            var btinfo = new List<MetacharData>
            {
                new MetacharData()
            };

            foreach (var compoundRule in rules)
            {
                var pp = 0; // pattern position
                var wp = 0; // "words" position
                var ok = true;
                var ok2 = true;
                do
                {
                    while (pp < compoundRule.Count && wp <= wnum)
                    {
                        if (
                            pp + 1 < compoundRule.Count
                            &&
                            (
                                compoundRule[pp + 1] == '*'
                                ||
                                compoundRule[pp + 1] == '?'
                            )
                        )
                        {
                            var wend = compoundRule[pp + 1] == '?' ? wp : wnum;
                            ok2 = true;
                            pp += 2;
                            btinfo[bt].btpp = pp;
                            btinfo[bt].btwp = wp;

                            while (wp <= wend)
                            {
                                if (!words[wp].HasFlags || !words[wp].ContainsFlag(compoundRule[pp - 2]))
                                {
                                    ok2 = false;
                                    break;
                                }

                                wp++;
                            }

                            if (wp <= wnum)
                            {
                                ok2 = false;
                            }

                            btinfo[bt].btnum = wp - btinfo[bt].btwp;

                            if (btinfo[bt].btnum > 0)
                            {
                                ++bt;
                                btinfo.Add(new MetacharData());
                            }
                            if (ok2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            ok2 = true;
                            if (
                                words[wp] == null
                                ||
                                !words[wp].HasFlags
                                ||
                                !words[wp].ContainsFlag(compoundRule[pp])
                            )
                            {
                                ok = false;
                                break;
                            }

                            pp++;
                            wp++;

                            if (compoundRule.Count == pp && wp <= wnum)
                            {
                                ok = false;
                            }
                        }
                    }

                    if (ok && ok2)
                    {
                        var r = pp;
                        while (
                            compoundRule.Count > r
                            &&
                            r + 1 < compoundRule.Count
                            &&
                            (
                                compoundRule[r + 1] == '*'
                                ||
                                compoundRule[r + 1] == '?'
                            )
                        )
                        {
                            r += 2;
                        }

                        if (compoundRule.Count <= r)
                        {
                            return true;
                        }
                    }

                    // backtrack
                    if (bt != 0)
                    {
                        do
                        {
                            ok = true;
                            btinfo[bt - 1].btnum--;
                            pp = btinfo[bt - 1].btpp;
                            wp = btinfo[bt - 1].btwp + btinfo[bt - 1].btnum;
                        }
                        while ((btinfo[bt - 1].btnum < 0) && (--bt != 0));
                    }

                }
                while (bt != 0);

                if (
                    ok
                    &&
                    ok2
                    &&
                    (
                        !all
                        ||
                        compoundRule.Count <= pp
                    )
                )
                {
                    return true;
                }

                // check zero ending
                while (
                    ok
                    &&
                    ok2
                    &&
                    pp + 1 < compoundRule.Count
                    &&
                    (
                        (compoundRule[pp + 1] == '*')
                        ||
                        (compoundRule[pp + 1] == '?')
                    )
                )
                {
                    pp += 2;
                }

                if (
                    ok
                    &&
                    ok2
                    &&
                    compoundRule.Count <= pp
                )
                {
                    return true;
                }
            }

            return false;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastListEnumerator<CompoundRule> GetEnumerator() => new FastListEnumerator<CompoundRule>(rules);

        IEnumerator<CompoundRule> IEnumerable<CompoundRule>.GetEnumerator() => rules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => rules.GetEnumerator();

        private class MetacharData
        {
            /// <summary>
            /// Metacharacter (*, ?) position for backtracking.
            /// </summary>
            public int btpp;
            /// <summary>
            /// Word position for metacharacters.
            /// </summary>
            public int btwp;
            /// <summary>
            /// Number of matched characters in metacharacter.
            /// </summary>
            public int btnum;
        }
    }
}
