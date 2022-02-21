using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class CompoundRuleSet : ArrayWrapper<CompoundRule>
{
    public static readonly CompoundRuleSet Empty = TakeArray(Array.Empty<CompoundRule>());

    public static CompoundRuleSet Create(IEnumerable<CompoundRule> rules) => rules is null ? Empty : TakeArray(rules.ToArray());

    internal static CompoundRuleSet TakeArray(CompoundRule[] rules) => rules is null ? Empty : new CompoundRuleSet(rules);

    private CompoundRuleSet(CompoundRule[] rules) : base(rules)
    {
    }

    internal bool EntryContainsRuleFlags(WordEntryDetail details)
    {
        if (details is not null && details.HasFlags)
        {
            foreach(var rule in Items)
            {
                if (rule.ContainsRuleFlagForEntry(details))
                {
                    return true;
                }
            }
        }

        return false;
    }

    internal bool CompoundCheck(IncrementalWordList words, bool all)
    {
        var bt = 0;
        var btinfo = new List<MetacharData>
        {
            new MetacharData()
        };

        foreach (var compoundRule in Items)
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
