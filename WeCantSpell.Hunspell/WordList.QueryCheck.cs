﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    private struct QueryCheck
    {
        public QueryCheck(WordList wordList, QueryOptions? options, CancellationToken cancellationToken)
        {
            _query = new(wordList, options, cancellationToken);
        }

        internal QueryCheck(in Query source)
        {
            _query = new(source);
        }

        private Query _query;

        public readonly WordList WordList => _query.WordList;

        public readonly AffixConfig Affix => _query.Affix;

        public readonly TextInfo TextInfo => _query.TextInfo;

        public readonly QueryOptions Options => _query.Options;

        public readonly int MaxSharps => Options.MaxSharps;

        public bool Check(string word) => CheckDetails(word).Correct;

        public bool Check(ReadOnlySpan<char> word) => CheckDetails(word).Correct;

        private readonly bool CheckNested(ReadOnlySpan<char> word) => new QueryCheck(_query).Check(word);

        public SpellCheckResult CheckDetails(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length >= Options.MaxWordLen || !WordList.HasEntries)
            {
                return SpellCheckResult.DefaultWrong;
            }

            if (word.StartsWith(Query.DefaultXmlTokenCheckPrefix, StringComparison.Ordinal))
            {
                // Hunspell supports XML input of the simplified API (see manual)
                return SpellCheckResult.DefaultCorrect;
            }

            if (HunspellTextFunctions.IsNumericWord(word.AsSpan()))
            {
                // allow numbers with dots, dashes and commas (but forbid double separators: "..", "--" etc.)
                return SpellCheckResult.DefaultCorrect;
            }

            // something very broken if spell ends up calling itself with the same word
            if (_query._spellCandidateStack.Contains(word))
            {
                return SpellCheckResult.DefaultWrong;
            }

            // input conversion
            if (!Affix.InputConversions.TryConvert(word, out var scw))
            {
                scw = word;
            }

            scw = _query.CleanWord2(scw, out var capType, out var abbv);

            if (scw.Length == 0)
            {
                return SpellCheckResult.DefaultWrong;
            }

            CandidateStack.Push(ref _query._spellCandidateStack, word);

            var result = CheckDetailsInternal(scw, capType, abbv != 0);

            CandidateStack.Pop(ref _query._spellCandidateStack);

            return result;
        }

        public SpellCheckResult CheckDetails(ReadOnlySpan<char> word)
        {
            if (word.IsEmpty || word.Length >= Options.MaxWordLen || !WordList.HasEntries)
            {
                return SpellCheckResult.DefaultWrong;
            }

            if (word.StartsWith(Query.DefaultXmlTokenCheckPrefix, StringComparison.Ordinal))
            {
                // Hunspell supports XML input of the simplified API (see manual)
                return SpellCheckResult.DefaultCorrect;
            }

            if (HunspellTextFunctions.IsNumericWord(word))
            {
                // allow numbers with dots, dashes and commas (but forbid double separators: "..", "--" etc.)
                return SpellCheckResult.DefaultCorrect;
            }

            // something very broken if spell ends up calling itself with the same word
            if (_query._spellCandidateStack.Contains(word))
            {
                return SpellCheckResult.DefaultWrong;
            }

            // input conversion
            CapitalizationType capType;
            int abbv;
            if (Affix.InputConversions.TryConvert(word, out var scw))
            {
                scw = _query.CleanWord2(scw, out capType, out abbv);
            }
            else
            {
                scw = _query.CleanWord2(word, out capType, out abbv);
            }

            if (scw.Length == 0)
            {
                return SpellCheckResult.DefaultWrong;
            }

            // NOTE: because a string isn't formed until this point, scw is pushed instead. It isn't the same, but might be good enough.
            CandidateStack.Push(ref _query._spellCandidateStack, scw);

            var result = CheckDetailsInternal(scw, capType, abbv != 0);

            CandidateStack.Pop(ref _query._spellCandidateStack);

            return result;
        }

        private SpellCheckResult CheckDetailsInternal(string scw, CapitalizationType capType, bool abbv)
        {
            var resultType = SpellCheckResultType.None;
            string? root;

            {
                WordEntry? rv = null;

                switch (capType)
                {
                    case CapitalizationType.HuhInit:
                        resultType |= SpellCheckResultType.OrigCap;
                        goto case CapitalizationType.Huh;

                    case CapitalizationType.Huh:
                    case CapitalizationType.None:
                        rv = _query.CheckWord(scw, ref resultType, out root);
                        if (abbv && rv is null)
                        {
                            rv = _query.CheckWord(scw + ".", ref resultType, out root);
                        }

                        break;

                    case CapitalizationType.All:
                        rv = CheckDetailsAllCap(abbv, ref scw, ref resultType, out root);
                        if (rv is null)
                        {
                            goto case CapitalizationType.Init;
                        }

                        break;

                    case CapitalizationType.Init:
                        rv = CheckDetailsInitCap(abbv, capType, ref scw, ref resultType, out root);
                        break;

                    default:
                        root = null;
                        break;
                }

                if (rv is not null)
                {
                    if (rv.ContainsFlag(Affix.Warn))
                    {
                        resultType |= SpellCheckResultType.Warn;

                        if (Affix.ForbidWarn)
                        {
                            return new SpellCheckResult(root, resultType, false);
                        }
                    }

                    return new SpellCheckResult(root, resultType, true);
                }
            }

            // recursive breaking at break points
            if (Affix.BreakPoints.HasItems && !resultType.HasFlagEx(SpellCheckResultType.Forbidden))
            {
                return CheckDetailsInternalBreakPoints(scw, root, resultType);
            }

            return new SpellCheckResult(root, resultType, false);
        }

        private readonly SpellCheckResult CheckDetailsInternalBreakPoints(string scw, string? root, SpellCheckResultType resultType)
        {
            // calculate break points for recursion limit
            if (Affix.BreakPoints.FindRecursionLimit(scw) >= 10)
            {
                return new SpellCheckResult(root, resultType, false);
            }

            // check boundary patterns (^begin and end$)
            foreach (var breakEntry in Affix.BreakPoints.GetInternalArray())
            {
                if (breakEntry.Length <= 1 || breakEntry.Length > scw.Length)
                {
                    continue;
                }

                var pLastIndex = breakEntry.Length - 1;
                if (
                    breakEntry.StartsWith('^')
                    && scw.AsSpan(0, pLastIndex).EqualsOrdinal(breakEntry.AsSpan(1))
                    && CheckNested(scw.AsSpan(pLastIndex))
                )
                {
                    return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true);
                }

                if (breakEntry.EndsWith('$'))
                {
                    var wlLessBreakIndex = scw.Length - breakEntry.Length + 1;
                    if (
                        scw.AsSpan(wlLessBreakIndex).EqualsOrdinal(breakEntry.AsSpan(0, pLastIndex))
                        && CheckNested(scw.AsSpan(0, wlLessBreakIndex))
                    )
                    {
                        return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true);
                    }
                }
            }

            if (scw.Length > 2)
            {
                List<(string breakEntry, int found)>? reSearch = null;
                // other patterns
                foreach (var breakEntry in Affix.BreakPoints.GetInternalArray())
                {
                    var found = scw.IndexOf(breakEntry, 1, scw.Length - 2, StringComparison.Ordinal);
                    if (found >= 0)
                    {
                        (reSearch ??= []).Add((breakEntry, found));

                        // try to break at the second occurance
                        // to recognize dictionary words with wordbreak
                        if (scw.Length - found > 2 && scw.IndexOf(breakEntry, found + 1, scw.Length - 2 - found, StringComparison.Ordinal) is int found2 and >= 0)
                        {
                            found = found2;
                        }

                        if (CheckNested(scw.AsSpan(found + breakEntry.Length)))
                        {
                            // examine 2 sides of the break point
                            if (CheckNested(scw.AsSpan(0, found)))
                            {
                                return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true);
                            }

                            // LANG_hu: spec. dash rule
                            if (Affix.IsHungarian && "-".Equals(breakEntry, StringComparison.Ordinal))
                            {
                                if (CheckNested(scw.AsSpan(0, found + 1)))
                                {
                                    return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true); // check the first part with dash
                                }
                            }
                        }
                    }
                }

                if (reSearch is not null)
                {
                    // other patterns (break at first break point)
                    foreach (var (breakEntry, found) in reSearch)
                    {
                        if (CheckNested(scw.AsSpan(found + breakEntry.Length)))
                        {
                            // examine 2 sides of the break point
                            if (CheckNested(scw.AsSpan(0, found)))
                            {
                                return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true);
                            }

                            // LANG_hu: spec. dash rule
                            if (Affix.IsHungarian && "-".Equals(breakEntry, StringComparison.Ordinal))
                            {
                                if (CheckNested(scw.AsSpan(0, found + 1)))
                                {
                                    return new SpellCheckResult(root, resultType | SpellCheckResultType.Compound, true); // check the first part with dash
                                }
                            }
                        }
                    }
                }
            }

            return new SpellCheckResult(root, resultType, false);
        }

        private WordEntry? CheckDetailsAllCap(bool abbv, ref string scw, ref SpellCheckResultType resultType, out string? root)
        {
            resultType |= SpellCheckResultType.OrigCap;
            var rv = _query.CheckWord(scw, ref resultType, out root);
            if (rv is not null)
            {
                return rv;
            }

            if (abbv)
            {
                rv = _query.CheckWord(scw + ".", ref resultType, out root);
                if (rv is not null)
                {
                    return rv;
                }
            }

            // Spec. prefix handling for Catalan, French, Italian:
            // prefixes separated by apostrophe (SANT'ELIA -> Sant'+Elia).
            var textInfo = TextInfo;
            var apos = scw.IndexOf('\'');
            if (apos >= 0)
            {
                scw = HunspellTextFunctions.MakeAllSmall(scw, textInfo);

                // conversion may result in string with different len than before MakeAllSmall2 so re-scan
                if (apos < scw.Length - 1)
                {
                    scw = StringEx.ConcatString(scw.AsSpan(0, apos + 1), HunspellTextFunctions.MakeInitCap(scw.AsSpan(apos + 1), textInfo));
                    rv = _query.CheckWord(scw, ref resultType, out root);
                    if (rv is not null)
                    {
                        return rv;
                    }

                    scw = HunspellTextFunctions.MakeInitCap(scw, textInfo);
                    rv = _query.CheckWord(scw, ref resultType, out root);
                    if (rv is not null)
                    {
                        return rv;
                    }
                }
            }

            if (Affix.CheckSharps && scw.Contains("SS"))
            {
                scw = HunspellTextFunctions.MakeAllSmall(scw, textInfo);
                var u8buffer = scw;
                rv = SpellSharps(ref u8buffer, ref resultType, out root);
                if (rv is null)
                {
                    scw = HunspellTextFunctions.MakeInitCap(scw, textInfo);
                    rv = SpellSharps(ref scw, ref resultType, out root);
                }

                if (abbv && rv is null)
                {
                    u8buffer += ".";
                    rv = SpellSharps(ref u8buffer, ref resultType, out root);
                    if (rv is null)
                    {
                        u8buffer = scw + ".";
                        rv = SpellSharps(ref u8buffer, ref resultType, out root);
                    }
                }
            }

            return rv;
        }

        private WordEntry? CheckDetailsInitCap(bool abbv, CapitalizationType capType, ref string scw, ref SpellCheckResultType resultType, out string? root)
        {
            var u8buffer = HunspellTextFunctions.MakeAllSmall(scw, TextInfo);
            scw = HunspellTextFunctions.MakeInitCap(u8buffer, TextInfo);

            resultType |= SpellCheckResultType.OrigCap;
            if (capType == CapitalizationType.Init)
            {
                resultType |= SpellCheckResultType.InitCap;
            }

            var rv = _query.CheckWord(scw, ref resultType, out root);

            if (capType == CapitalizationType.Init)
            {
                resultType &= ~SpellCheckResultType.InitCap;
            }

            // forbid bad capitalization
            // (for example, ijs -> Ijs instead of IJs in Dutch)
            // use explicit forms in dic: Ijs/F (F = FORBIDDENWORD flag)

            if (resultType.HasFlagEx(SpellCheckResultType.Forbidden))
            {
                rv = null;
                return rv;
            }

            if (capType == CapitalizationType.All && rv is not null && IsKeepCase(rv))
            {
                rv = null;
            }

            if (rv is not null || (!Affix.CultureUsesDottedI && scw.StartsWith('İ')))
            {
                return rv;
            }

            rv = _query.CheckWord(u8buffer, ref resultType, out root);

            if (abbv && rv is null)
            {
                u8buffer += ".";
                rv = _query.CheckWord(u8buffer, ref resultType, out root);
                if (rv is null)
                {
                    u8buffer = scw + ".";
                    if (capType == CapitalizationType.Init)
                    {
                        resultType |= SpellCheckResultType.InitCap;
                    }

                    rv = _query.CheckWord(u8buffer, ref resultType, out root);

                    if (capType == CapitalizationType.Init)
                    {
                        resultType &= ~SpellCheckResultType.InitCap;
                    }

                    if (capType == CapitalizationType.All && rv is not null && IsKeepCase(rv))
                    {
                        rv = null;
                    }

                    return rv;
                }
            }

            if (
                rv is not null
                &&
                IsKeepCase(rv)
                &&
                (
                    capType == CapitalizationType.All
                    ||
                    // if CHECKSHARPS: KEEPCASE words with \xDF  are allowed in INITCAP form, too.
                    !(Affix.CheckSharps && u8buffer.Contains('ß'))
                )
            )
            {
                rv = null;
            }

            return rv;
        }

        /// <summary>
        /// Recursive search for right ss - sharp s permutations
        /// </summary>
        private WordEntry? SpellSharps(ref string @base, ref SpellCheckResultType info, out string? root) =>
            SpellSharps(ref @base, 0, 0, 0, ref info, out root);

        /// <summary>
        /// Recursive search for right ss - sharp s permutations
        /// </summary>
        private WordEntry? SpellSharps(ref string @base, int nPos, int n, int repNum, ref SpellCheckResultType info, out string? root)
        {
            var pos = @base.IndexOf("ss", nPos, StringComparison.Ordinal);
            if (pos >= 0 && n < MaxSharps)
            {
                var baseBuilder = new StringBuilderSpan(@base);
                baseBuilder[pos] = 'ß';
                baseBuilder.Remove(pos + 1, 1);
                @base = baseBuilder.ToString();

                var h = SpellSharps(ref @base, pos + 1, n + 1, repNum + 1, ref info, out root);
                if (h is not null)
                {
                    return h;
                }

                baseBuilder.Clear();
                baseBuilder.Append(@base);
                baseBuilder[pos] = 's';
                baseBuilder.Insert(pos + 1, 's');
                @base = baseBuilder.GetStringAndDispose();

                h = SpellSharps(ref @base, pos + 2, n + 1, repNum, ref info, out root);
                if (h is not null)
                {
                    return h;
                }
            }
            else if (repNum > 0)
            {
                return _query.CheckWord(@base, ref info, out root);
            }

            root = null;
            return null;
        }

        private readonly bool IsKeepCase(WordEntry rv) => rv.ContainsFlag(Affix.KeepCase);
    }
}
