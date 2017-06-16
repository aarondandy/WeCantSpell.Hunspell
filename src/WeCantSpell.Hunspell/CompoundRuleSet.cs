using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class CompoundRuleSet : ListWrapper<CompoundRule>
    {
        public static readonly CompoundRuleSet Empty = TakeList(new List<CompoundRule>(0));

        private CompoundRuleSet(List<CompoundRule> rules)
            : base(rules)
        {
        }

        internal static CompoundRuleSet TakeList(List<CompoundRule> rules) =>
            rules == null ? Empty : new CompoundRuleSet(rules);

        public static CompoundRuleSet Create(IEnumerable<CompoundRule> rules) =>
            rules == null ? Empty : TakeList(rules.ToList());

        public bool EntryContainsRuleFlags(WordEntry rv) =>
            EntryContainsRuleFlags(rv?.Detail);

        public bool EntryContainsRuleFlags(WordEntryDetail details)
        {
            if (details != null && details.HasFlags)
            {
                foreach(var rule in items)
                {
                    if (rule.ContainsRuleFlagForEntry(details))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [Obsolete]
        public bool CompoundCheck(Dictionary<int, WordEntry> words, int wnum, bool all)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            var list = new List<WordEntryDetail>();
            foreach (var item in words)
            {
                if (item.Key < list.Count)
                {
                    list[item.Key] = item.Value.Detail;
                }
                else
                {
                    for(var i = item.Key - list.Count; i > 0; i--)
                    {
                        list.Add(null);
                    }

                    list.Add(item.Value.Detail);
                }
            }

            return CompoundCheckInternal(new IncrementalWordList(list, wnum), all);
        }

        internal bool CompoundCheckInternal(IncrementalWordList words, bool all)
        {
            var bt = 0;
            var btinfo = new List<MetacharData>
            {
                new MetacharData()
            };

            foreach (var compoundRule in items)
            {
                var pp = 0; // pattern position
                var wp = 0; // "words" position
                var ok = true;
                var ok2 = true;
                do
                {
                    while (pp < compoundRule.Count && wp <= words.WNum)
                    {
                        if (pp + 1 < compoundRule.Count && compoundRule.IsWildcard(pp + 1))
                        {
                            var wend = compoundRule[pp + 1] == '?' ? wp : words.WNum;
                            ok2 = true;
                            pp += 2;
                            btinfo[bt].btpp = pp;
                            btinfo[bt].btwp = wp;

                            while (wp <= wend)
                            {
                                if (!words.ContainsFlagAt(wp, compoundRule[pp - 2]))
                                {
                                    ok2 = false;
                                    break;
                                }

                                wp++;
                            }

                            if (wp <= words.WNum)
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
                            if (!words.ContainsFlagAt(wp, compoundRule[pp]))
                            {
                                ok = false;
                                break;
                            }

                            pp++;
                            wp++;

                            if (compoundRule.Count == pp && wp <= words.WNum)
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
                            compoundRule.IsWildcard(r + 1)
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
                    compoundRule.IsWildcard(pp + 1)
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
