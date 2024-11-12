using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct CompoundRuleSet : IReadOnlyList<CompoundRule>
{
    public static CompoundRuleSet Empty { get; } = new([]);

    public static CompoundRuleSet Create(IEnumerable<CompoundRule> rules)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(rules);
#else
        ExceptionEx.ThrowIfArgumentNull(rules, nameof(rules));
#endif
        return new(rules.ToArray());
    }

    internal CompoundRuleSet(CompoundRule[] rules)
    {
        _rules = rules;
    }

    private readonly CompoundRule[]? _rules;

    public int Count => _rules is not null ? _rules.Length : 0;

    public bool IsEmpty => _rules is not { Length: > 0 };

    public bool HasItems => _rules is { Length: > 0 };

    public CompoundRule this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif
            if (_rules is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _rules![index];
        }
    }

    public IEnumerator<CompoundRule> GetEnumerator() => ((IEnumerable<CompoundRule>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal CompoundRule[] GetInternalArray() => _rules ?? [];

    internal bool EntryContainsRuleFlags(in FlagSet flags)
    {
        if (flags.HasItems && _rules is not null)
        {
            foreach(var rule in _rules)
            {
                if (rule.ContainsRuleFlagForEntry(flags))
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
        var btinfo = new List<MetacharData> { new() };

        foreach (var compoundRule in GetInternalArray())
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

    private sealed class MetacharData
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
