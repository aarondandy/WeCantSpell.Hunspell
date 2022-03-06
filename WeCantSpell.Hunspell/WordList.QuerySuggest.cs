using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    private sealed class QuerySuggest : Query
    {
        /// <summary>
        /// Timelimit: max ~1/4 sec (process time on Linux) for a time consuming function.
        /// </summary>
        private const int TimeLimitMs = 1000 >> 2;

        private const int MinTimer = 100;

        private const int MaxPlusTimer = 100;

        private const int MaxCharDistance = 4;

        private const int TimeLimitCompoundSuggestMs = 1000 / 10;

        public QuerySuggest(WordList wordList)
            : base(wordList)
        {
        }

        /// <summary>
        /// Used to abort long running compound check calls.
        /// </summary>
        private OperationTimeLimiter? CompoundSuggestTimeLimiter;

        private List<string> SuggestNested(string word) => new QuerySuggest(WordList).Suggest(word);

        private bool Check(string word) => new QueryCheck(WordList).Check(word);

        public List<string> Suggest(string word)
        {
            var slst = new List<string>();

            if (!WordList.HasEntries)
            {
                return slst;
            }

            var onlyCompoundSuggest = false;

            // process XML input of the simplified API (see manual)
            if (word.AsSpan().StartsWith(DefaultXmlToken.AsSpan(0, DefaultXmlToken.Length - 3)))
            {
                return slst; // TODO: complete support for XML input
            }

            if (word.Length >= MaxWordUtf8Len)
            {
                return slst;
            }

            // input conversion
            if (!Affix.InputConversions.HasReplacements || !Affix.InputConversions.TryConvert(word, out string tempString))
            {
                tempString = word;
            }

            var scw = CleanWord2(tempString, out CapitalizationType capType, out int abbv);
            if (string.IsNullOrEmpty(scw))
            {
                return slst;
            }

            if (GlobalTimeLimiter is null)
            {
                GlobalTimeLimiter = OperationTimeLimiter.Create(TimeLimitGlobalMs);
            }
            else
            {
                GlobalTimeLimiter.Reset();
            }

            var textInfo = TextInfo;

            // check capitalized form for FORCEUCASE
            if (capType == CapitalizationType.None && Affix.ForceUpperCase.HasValue)
            {
                var info = SpellCheckResultType.OrigCap;
                if (CheckWord(scw, ref info, out tempString) is not null)
                {
                    slst.Add(HunspellTextFunctions.MakeInitCap(scw, textInfo));
                    return slst;
                }
            }

            var capWords = false;
            var good = false;

            if (capType == CapitalizationType.None)
            {
                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                if (abbv != 0)
                {
                    var wspace = scw + ".";
                    good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                    if (GlobalTimeLimiter.QueryForExpiration())
                    {
                        return slst;
                    }
                }
            }
            else if (capType == CapitalizationType.Init)
            {
                capWords = true;
                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                good |= Suggest(slst, HunspellTextFunctions.MakeAllSmall(scw, textInfo), ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }
            }
            else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
            {
                capWords = capType == CapitalizationType.HuhInit;

                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                // something.The -> something. The
                var dotPos = scw.IndexOf('.');
                if (
                    dotPos >= 0
                    &&
                    HunspellTextFunctions.GetCapitalizationType(scw.AsSpan(dotPos + 1), textInfo) == CapitalizationType.Init
                )
                {
                    InsertSuggestion(slst, scw.Insert(dotPos + 1, " "));
                }

                if (capType == CapitalizationType.HuhInit)
                {
                    // TheOpenOffice.org -> The OpenOffice.org
                    good |= Suggest(slst, HunspellTextFunctions.MakeInitSmall(scw, textInfo), ref onlyCompoundSuggest);

                    if (GlobalTimeLimiter.QueryForExpiration())
                    {
                        return slst;
                    }
                }

                var wspace = HunspellTextFunctions.MakeAllSmall(scw, textInfo);
                if (Check(wspace))
                {
                    InsertSuggestion(slst, wspace);
                }

                var prevns = slst.Count;
                good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                if (capType == CapitalizationType.HuhInit)
                {
                    wspace = HunspellTextFunctions.MakeInitCap(wspace, textInfo);
                    if (Check(wspace))
                    {
                        InsertSuggestion(slst, wspace);
                    }

                    good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                    if (GlobalTimeLimiter.QueryForExpiration())
                    {
                        return slst;
                    }
                }

                // aNew -> "a New" (instead of "a new")
                for (var j = prevns; j < slst.Count; j++)
                {
                    var toRemove = slst[j];
                    var spaceIndex = toRemove.IndexOf(' ');
                    if (spaceIndex >= 0)
                    {
                        var slen = toRemove.Length - spaceIndex - 1;

                        // different case after space (need capitalisation)
                        if (slen < scw.Length && !scw.AsSpan(scw.Length - slen).EqualsOrdinal(toRemove.AsSpan(spaceIndex + 1)))
                        {
                            // set as first suggestion
                            RemoveFromIndexThenInsertAtFront(
                                slst,
                                j,
                                StringEx.ConcatString(
                                    toRemove, 0, spaceIndex + 1,
                                    HunspellTextFunctions.MakeInitCap(toRemove.AsSpan(spaceIndex + 1), textInfo)));
                        }
                    }
                }
            }
            else if (capType == CapitalizationType.All)
            {
                var wspace = HunspellTextFunctions.MakeAllSmall(scw, textInfo);
                good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                if (Affix.KeepCase.HasValue && Check(wspace))
                {
                    InsertSuggestion(slst, wspace);
                }

                wspace = HunspellTextFunctions.MakeInitCap(wspace, textInfo);
                good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }

                for (var j = 0; j < slst.Count; j++)
                {
                    slst[j] = HunspellTextFunctions.MakeAllCap(slst[j], textInfo).Replace("ß", "SS");
                }
            }

            // LANG_hu section: replace '-' with ' ' in Hungarian
            if (Affix.IsHungarian)
            {
                for (var j = 0; j < slst.Count; j++)
                {
                    var sitem = slst[j];
                    var pos = sitem.IndexOf('-');
                    if (pos >= 0)
                    {
                        var info = CheckDetails(sitem.WithoutIndex(pos)).Info;
                        var desiredChar = EnumEx.HasFlag(info, SpellCheckResultType.Compound | SpellCheckResultType.Forbidden)
                            ? ' '
                            : '-';

                        if (sitem[pos] != desiredChar)
                        {
                            slst[j] = StringEx.ConcatString(sitem, 0, pos, desiredChar, sitem, pos + 1);
                        }
                    }
                }
            }

            // try ngram approach since found nothing good suggestion
            if (!good && Affix.MaxNgramSuggestions != 0 && (slst.Count == 0 || onlyCompoundSuggest))
            {
                if (capType == CapitalizationType.None)
                {
                    NGramSuggest(slst, scw, capType);
                }
                else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
                {
                    if (capType == CapitalizationType.HuhInit)
                    {
                        capWords = true;
                    }

                    NGramSuggest(slst, HunspellTextFunctions.MakeAllSmall(scw, textInfo), CapitalizationType.Huh);
                }
                else if (capType == CapitalizationType.Init)
                {
                    capWords = true;
                    NGramSuggest(slst, HunspellTextFunctions.MakeAllSmall(scw, textInfo), capType);
                }
                else if (capType == CapitalizationType.All)
                {
                    var oldns = slst.Count;
                    NGramSuggest(slst, HunspellTextFunctions.MakeAllSmall(scw, textInfo), capType);

                    for (var j = oldns; j < slst.Count; j++)
                    {
                        slst[j] = HunspellTextFunctions.MakeAllCap(slst[j], textInfo);
                    }
                }

                if (GlobalTimeLimiter.QueryForExpiration())
                {
                    return slst;
                }
            }

            // try dash suggestion (Afo-American -> Afro-American)
            // Note: LibreOffice was modified to treat dashes as word
            // characters to check "scot-free" etc. word forms, but
            // we need to handle suggestions for "Afo-American", etc.,
            // while "Afro-American" is missing from the dictionary.
            // TODO avoid possible overgeneration
            var dashPos = scw.IndexOf('-');
            if (dashPos >= 0)
            {
                var noDashSug = true;
                for (var j = 0; j < slst.Count; j++)
                {
                    if (slst[j].Contains('-'))
                    {
                        noDashSug = false;
                        break;
                    }
                }

                var prevPos = 0;
                var last = false;

                while (!good && noDashSug && !last)
                {
                    if (dashPos == scw.Length)
                    {
                        last = true;
                    }

                    var chunk = scw.Substring(prevPos, dashPos - prevPos);
                    if (!Check(chunk))
                    {
                        var nlst = SuggestNested(chunk);

                        foreach (var j in nlst)
                        {
                            var wspace = last
                                ? StringEx.ConcatString(scw, 0, prevPos, j)
                                : StringEx.ConcatString(scw, 0, prevPos, j, '-', scw, dashPos + 1);

                            var info = SpellCheckResultType.None;
                            if (Affix.ForbiddenWord.HasValue)
                            {
                                CheckWord(wspace, ref info, out var _);
                            }
                            if (!EnumEx.HasFlag(info, SpellCheckResultType.Forbidden))
                            {
                                InsertSuggestion(slst, wspace);
                            }
                        }

                        noDashSug = false;
                    }

                    if (!last)
                    {
                        prevPos = dashPos + 1;
                        dashPos = scw.IndexOf('-', prevPos);
                    }

                    if (dashPos < 0)
                    {
                        dashPos = scw.Length;
                    }
                }
            }

            // word reversing wrapper for complex prefixes
            if (Affix.ComplexPrefixes)
            {
                for (var j = 0; j < slst.Count; j++)
                {
                    slst[j] = slst[j].GetReversed();
                }
            }

            // capitalize
            if (capWords)
            {
                for (var j = 0; j < slst.Count; j++)
                {
                    slst[j] = HunspellTextFunctions.MakeInitCap(slst[j], textInfo);
                }
            }

            // expand suggestions with dot(s)
            if (abbv != 0 && Affix.SuggestWithDots)
            {
                for (var j = 0; j < slst.Count; j++)
                {
                    slst[j] = slst[j].ConcatString(word.AsSpan(word.Length - abbv));
                }
            }

            // remove bad capitalized and forbidden forms
            if (
                (Affix.KeepCase.HasValue || Affix.ForbiddenWord.HasValue)
                &&
                (capType == CapitalizationType.Init || capType == CapitalizationType.All)
            )
            {
                var l = 0;
                for (var j = 0; j < slst.Count; j++)
                {
                    var sitem = slst[j];
                    if (!sitem.Contains(' ') && !Check(sitem))
                    {
                        var s = HunspellTextFunctions.MakeAllSmall(sitem, textInfo);
                        if (Check(s))
                        {
                            slst[l++] = s;
                        }
                        else
                        {
                            s = HunspellTextFunctions.MakeInitCap(s, textInfo);
                            if (Check(s))
                            {
                                slst[l++] = s;
                            }
                        }
                    }
                    else
                    {
                        slst[l++] = sitem;
                    }
                }

                slst.RemoveRange(l, slst.Count - l);
            }

            // remove duplications
            slst.RemoveDuplicates(Affix.StringComparer);

            // output conversion
            if (Affix.OutputConversions.HasReplacements)
            {
                for (var j = 0; j < slst.Count; j++)
                {
                    if (Affix.OutputConversions.TryConvert(slst[j], out string wspace))
                    {
                        slst[j] = wspace;
                    }
                }
            }

            return slst;
        }

        private static void RemoveFromIndexThenInsertAtFront(List<string> list, int removeIndex, string insertValue)
        {
            if (list.Count != 0)
            {
                while (removeIndex > 0)
                {
                    var sourceIndex = removeIndex - 1;
                    list[removeIndex] = list[sourceIndex];
                    removeIndex = sourceIndex;
                }

                list[0] = insertValue;
            }
        }

        /// <summary>
        /// Generate suggestions for a misspelled word
        /// </summary>
        /// <param name="slst">Resulting suggestion list.</param>
        /// <param name="word">The word to base suggestions on.</param>
        /// <param name="onlyCompoundSug">Indicates there may be bad suggestions.</param>
        /// <returns>True when there may be a good suggestion.</returns>
        private bool Suggest(List<string> slst, string word, ref bool onlyCompoundSug)
        {
            var noCompoundTwoWords = false;
            var nSugOrig = slst.Count;
            var oldSug = 0;
            var cpdSuggest = false;
            var goodSuggestion = false;

            // word reversing wrapper for complex prefixes
            if (Affix.ComplexPrefixes)
            {
                word = word.GetReversed();
            }

            CompoundSuggestTimeLimiter ??= OperationTimeLimiter.Create(TimeLimitCompoundSuggestMs);

            do
            {
                CompoundSuggestTimeLimiter.Reset();

                // limit compound suggestion
                if (cpdSuggest)
                {
                    oldSug = slst.Count;
                }

                var sugLimit = oldSug + MaxCompoundSuggestions;

                // suggestions for an uppercase word (html -> HTML)
                if (slst.Count < MaxSuggestions)
                {
                    var i = slst.Count;
                    CapChars(slst, word, cpdSuggest);
                    if (slst.Count > i)
                    {
                        goodSuggestion = true;
                    }
                }

                // perhaps we made a typical fault of spelling
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    var i = slst.Count;
                    ReplChars(slst, word, cpdSuggest);
                    if (slst.Count > i)
                    {
                        goodSuggestion = true;
                    }
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // perhaps we made chose the wrong char from a related set
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    MapChars(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // only suggest compound words when no other suggestion
                if (!cpdSuggest && slst.Count > nSugOrig)
                {
                    noCompoundTwoWords = true;
                }

                // did we swap the order of chars by mistake
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    SwapChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we swap the order of non adjacent chars by mistake
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    LongSwapChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we just hit the wrong key in place of a good char (case and keyboard)
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    BadCharKey(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we add a char that should not be there
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    ExtraChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we forgot a char
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    ForgotChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we move a char
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    MoveChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we just hit the wrong key in place of a good char
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    BadChar(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // did we double two characters
                if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                {
                    DoubleTwoChars(slst, word, cpdSuggest);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }

                // perhaps we forgot to hit space and two words ran together
                // (dictionary word pairs have top priority here, so
                // we always suggest them, in despite of nosplitsugs, and
                // drop compound word and other suggestions)
                //if (!Affix.NoSplitSuggestions && slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                if (!cpdSuggest || (!Affix.NoSplitSuggestions && slst.Count < sugLimit))
                {
                    goodSuggestion = TwoWords(slst, word, cpdSuggest, goodSuggestion);
                }

                if (CompoundSuggestTimeLimiter.QueryForExpiration())
                {
                    return goodSuggestion;
                }
            }
            while (!noCompoundTwoWords && IntEx.InversePostfixIncrement(ref cpdSuggest) && !goodSuggestion);

            if (!noCompoundTwoWords && slst.Count != 0)
            {
                onlyCompoundSug = true;
            }

            return goodSuggestion;
        }

        private SpellCheckResult CheckDetails(string word) => new QueryCheck(WordList).CheckDetails(word);

        /// <summary>
        /// perhaps we doubled two characters (pattern aba -> ababa, for example vacation -> vacacation)
        /// </summary>
        /// <remarks>
        /// (for example vacation -> vacacation)
        /// The recognized pattern with regex back-references:
        /// "(.)(.)\1\2\1" or "..(.)(.)\1\2"
        /// </remarks>
        private int DoubleTwoChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 5)
            {
                return wlst.Count;
            }

            var state = 0;
            var builder = StringBuilderPool.Get(word.Length);
            for (var i = 2; i < word.Length; i++)
            {
                if (word[i] == word[i - 2])
                {
                    state++;
                    if (state == 3 || (state == 2 && i >= 4))
                    {
                        builder.Clear();
                        builder.Append(word, 0, i - 1);
                        builder.Append(word, i + 1, word.Length - i - 1);
                        TestSug(wlst, builder.ToString(), cpdSuggest);
                        state = 0;
                    }
                }
                else
                {
                    state = 0;
                }
            }

            StringBuilderPool.Return(builder);

            return wlst.Count;
        }

        /// <summary>
        /// Error is wrong char in place of correct one.
        /// </summary>
        private int BadChar(List<string> wlst, string word, bool cpdSuggest)
        {
            if (Affix.TryString is not { Length: > 0 } tryString)
            {
                return wlst.Count;
            }

            var candidate = StringBuilderPool.Get(word);

            var timer = OperationTimeLimiter.Create(TimeLimitMs, MinTimer);

            // swap out each char one by one and try all the tryme
            // chars in its place to see if that makes a good word
            for (var j = 0; j < tryString.Length; j++)
            {
                for (var i = candidate.Length - 1; i >= 0; i--)
                {
                    var tmpc = candidate[i];
                    if (tryString[j] == tmpc)
                    {
                        continue;
                    }

                    candidate[i] = tryString[j];
                    TestSug(wlst, candidate.ToString(), cpdSuggest, timer);
                    candidate[i] = tmpc;

                    if (timer.QueryCounter == 0)
                    {
                        return wlst.Count;
                    }
                }
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        /// <summary>
        /// Error is a letter was moved.
        /// </summary>
        private int MoveChar(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2)
            {
                return wlst.Count;
            }

            var candidate = StringBuilderPool.Get(word.Length);

            // try moving a char
            for (var p = 0; p < word.Length; p++)
            {
                candidate.Clear();
                candidate.Append(word);

                var qMax = Math.Min(MaxCharDistance + p + 1, candidate.Length);
                for (var q = p + 1; q < qMax; q++)
                {
                    candidate.Swap(q, q - 1);

                    if (q - p < 2)
                    {
                        continue; // omit swap char
                    }

                    TestSug(wlst, candidate.ToString(), cpdSuggest);
                }
            }

            for (var p = word.Length - 1; p >= 0; p--)
            {
                candidate.Clear();
                candidate.Append(word);

                var qMin = Math.Max(p - MaxCharDistance, 0);
                for (var q = p - 1; q >= qMin; q--)
                {
                    candidate.Swap(q, q + 1);

                    if (p - q < 2)
                    {
                        continue;  // omit swap char
                    }

                    TestSug(wlst, candidate.ToString(), cpdSuggest);
                }
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        /// <summary>
        /// Error is missing a letter it needs.
        /// </summary>
        private int ForgotChar(List<string> wlst, string word, bool cpdSuggest)
        {
            if (string.IsNullOrEmpty(Affix.TryString))
            {
                return wlst.Count;
            }

            var candidate = StringBuilderPool.Get(word, word.Length + 1);

            var timer = OperationTimeLimiter.Create(TimeLimitMs, MinTimer);

            // try inserting a tryme character before every letter (and the null terminator)
            foreach (var tryChar in Affix.TryString)
            {
                for (var index = candidate.Length; index >= 0; index--)
                {
                    TestSug(wlst, candidate.ToStringWithInsert(index, tryChar), cpdSuggest, timer);

                    if (timer.QueryCounter == 0)
                    {
                        return wlst.Count;
                    }
                }
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        /// <summary>
        /// Error is word has an extra letter it does not need.
        /// </summary>
        private int ExtraChar(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2)
            {
                return wlst.Count;
            }

            for (var index = word.Length - 1; index >= 0; index--)
            {
                TestSug(wlst, word.WithoutIndex(index), cpdSuggest);
            }

            return wlst.Count;
        }

        /// <summary>
        /// error is wrong char in place of correct one (case and keyboard related version)
        /// </summary>
        private int BadCharKey(List<string> wlst, string word, bool cpdSuggest)
        {
            var candidate = StringBuilderPool.Get(word);
            var keyString = Affix.KeyString;

            // swap out each char one by one and try uppercase and neighbor
            // keyboard chars in its place to see if that makes a good word
            for (var i = 0; i < candidate.Length; i++)
            {
                var tmpc = candidate[i];
                // check with uppercase letters
                candidate[i] = TextInfo.ToUpper(tmpc);
                if (tmpc != candidate[i])
                {
                    TestSug(wlst, candidate.ToString(), cpdSuggest);
                    candidate[i] = tmpc;
                }

                // check neighbor characters in keyboard string
                var loc = keyString.IndexOf(tmpc);
                while (loc >= 0)
                {
                    var targetLoc = loc - 1;
                    if (targetLoc >= 0 && keyString[targetLoc] != '|')
                    {
                        candidate[i] = keyString[targetLoc];
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                    }

                    targetLoc = loc + 1;
                    if (targetLoc < keyString.Length && keyString[targetLoc] != '|')
                    {
                        candidate[i] = keyString[targetLoc];
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                    }

                    loc = keyString.IndexOf(tmpc, targetLoc);
                }

                candidate[i] = tmpc;
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        /// <summary>
        /// Error is not adjacent letter were swapped.
        /// </summary>
        private int LongSwapChar(List<string> wlst, string word, bool cpdSuggest)
        {
            var candidate = StringBuilderPool.Get(word, word.Length);
            // try swapping not adjacent chars one by one
            for (var p = 0; p < candidate.Length; p++)
            {
                var oldp = candidate[p];
                var qMax = Math.Min(candidate.Length, p + MaxCharDistance + 1);
                var pLow = p - 1;
                var pHigh = p + 1;
                for (var q = Math.Max(0, p - MaxCharDistance); q < qMax; q++)
                {
                    if (q < pLow || q > pHigh)
                    {
                        var oldq = candidate[q];
                        candidate[p] = oldq;
                        candidate[q] = oldp;
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                        candidate[q] = oldq;
                        candidate[p] = oldp;
                    }
                }
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        /// <summary>
        /// Error is adjacent letter were swapped.
        /// </summary>
        private int SwapChar(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2)
            {
                return wlst.Count;
            }

            var candidate = StringBuilderPool.Get(word, word.Length);

            // try swapping adjacent chars one by one
            var lastCandidateIndex = candidate.Length - 1;
            for (var i = 0; i < lastCandidateIndex; i++)
            {
                var nexti = i + 1;
                var c = candidate[i];
                candidate[i] = candidate[nexti];
                candidate[nexti] = c;
                TestSug(wlst, candidate.ToString(), cpdSuggest);
                candidate[nexti] = candidate[i];
                candidate[i] = c;
            }

            // try double swaps for short words
            // ahev -> have, owudl -> would
            if (candidate.Length == 4 || candidate.Length == 5)
            {
                candidate[0] = word[1];
                candidate[1] = word[0];
                candidate[2] = word[2];
                candidate[candidate.Length - 2] = word[candidate.Length - 1];
                candidate[candidate.Length - 1] = word[candidate.Length - 2];

                TestSug(wlst, candidate.ToString(), cpdSuggest);

                if (candidate.Length == 5)
                {
                    candidate[0] = word[0];
                    candidate[1] = word[2];
                    candidate[2] = word[1];

                    TestSug(wlst, candidate.ToString(), cpdSuggest);
                }
            }

            StringBuilderPool.Return(candidate);

            return wlst.Count;
        }

        private void CapChars(List<string> wlst, string word, bool cpdSuggest) =>
            TestSug(wlst, HunspellTextFunctions.MakeAllCap(word, TextInfo), cpdSuggest);

        private int MapChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2 || Affix.RelatedCharacterMap.IsEmpty)
            {
                return wlst.Count;
            }

            var candidate = string.Empty;
            return MapRelated(word, ref candidate, 0, wlst, cpdSuggest, OperationTimeLimiter.Create(TimeLimitMs, MinTimer));
        }

        private int MapRelated(string word, ref string candidate, int wn, List<string> wlst, bool cpdSuggest, OperationTimeLimiter timer)
        {
            if (wn >= word.Length)
            {
                if (
                    !wlst.Contains(candidate)
                    &&
                    CheckWord(candidate, cpdSuggest, timer) != 0
                    &&
                    wlst.Count < MaxSuggestions
                )
                {
                    wlst.Add(candidate);
                }

                return wlst.Count;
            }

            var inMap = false;
            foreach (var mapEntry in Affix.RelatedCharacterMap)
            {
                foreach (var mapEntryValue in mapEntry)
                {
                    if (word.AsSpan(wn).StartsWith(mapEntryValue.AsSpan()))
                    {
                        inMap = true;
                        var candidatePrefix = candidate;
                        foreach (var otherMapEntryValue in mapEntry)
                        {
                            candidate = candidatePrefix + otherMapEntryValue;
                            MapRelated(word, ref candidate, wn + mapEntryValue.Length, wlst, cpdSuggest, timer);

                            if (timer is not null && timer.QueryCounter == 0)
                            {
                                return wlst.Count;
                            }
                        }
                    }
                }
            }

            if (!inMap)
            {
                candidate += word[wn];
                MapRelated(word, ref candidate, wn + 1, wlst, cpdSuggest, timer);
            }

            return wlst.Count;
        }

        private void TestSug(List<string> wlst, string candidate, bool cpdSuggest, OperationTimeLimiter? timer = null)
        {
            if (
                wlst.Count < MaxSuggestions
                &&
                !wlst.Contains(candidate)
                &&
                CheckWord(candidate, cpdSuggest, timer) != 0
            )
            {
                wlst.Add(candidate);
            }
        }

        private void TestSug(List<string> wlst, ReadOnlySpan<char> candidate, bool cpdSuggest, OperationTimeLimiter? timer = null)
        {
            if (wlst.Count < MaxSuggestions)
            {
                var candidateWord = candidate.ToString();
                if (!wlst.Contains(candidateWord) && CheckWord(candidateWord, cpdSuggest, timer) != 0)
                {
                    wlst.Add(candidateWord);
                    return;
                }
            }
        }

        private int CheckWord(ReadOnlySpan<char> word, bool cpdSuggest, OperationTimeLimiter? timer = null) =>
            CheckWord(word.ToString(), cpdSuggest, timer);

        /// <summary>
        /// See if a candidate suggestion is spelled correctly
        /// needs to check both root words and words with affixes.
        /// </summary>
        /// <remarks>
        /// Obsolote MySpell-HU modifications:
        /// return value 2 and 3 marks compounding with hyphen (-)
        /// `3' marks roots without suffix
        /// </remarks>
        private int CheckWord(string word, bool cpdSuggest, OperationTimeLimiter? timer = null)
        {
#if DEBUG
            if (word is null) throw new ArgumentNullException(nameof(word));
#endif

            if (timer is not null && timer.QueryForExpiration())
            {
                return 0;
            }

            WordEntry? rv;
            if (cpdSuggest)
            {
                if (Affix.HasCompound)
                {
                    var rwords = new IncrementalWordList(); // buffer for COMPOUND pattern checking
                    var info = SpellCheckResultType.None;
                    rv = CompoundCheck(word, 0, 0, 100, null, rwords, false, 1, ref info);
                    if (rv is not null)
                    {
                        var rvDetail = LookupFirstDetail(word);
                        if (rvDetail is null || !rvDetail.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest))
                        {
                            return 3; // XXX obsolote categorisation + only ICONV needs affix flag check?
                        }
                    }
                }

                return 0;
            }

            var rvDetails = LookupDetails(word); // get homonyms
            if (rvDetails.Length != 0)
            {
                var rvDetail = rvDetails[0];

                if (rvDetail.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest, Affix.SubStandard))
                {
                    return 0;
                }

                var rvIndex = 0;
                while (rvDetail is not null)
                {
                    if (rvDetail.ContainsAnyFlags(Affix.NeedAffix, SpecialFlags.OnlyUpcaseFlag, Affix.OnlyInCompound))
                    {
                        rvIndex++;
                        rvDetail = rvIndex < rvDetails.Length ? rvDetails[rvIndex] : null;
                    }
                    else
                    {
                        break;
                    }
                }

                rv = rvDetail is null ? null : new WordEntry(word, rvDetail);
            }
            else
            {
                rv = PrefixCheck(word, CompoundOptions.Not, default); // only prefix, and prefix + suffix XXX
            }

            var noSuffix = rv is not null;
            if (!noSuffix)
            {
                rv = SuffixCheck(word, AffixEntryOptions.None, default, default, default, CompoundOptions.Not); // only suffix
            }

            if (Affix.ContClasses.HasItems && rv is null)
            {
                rv = SuffixCheckTwoSfx(word, AffixEntryOptions.None, default, default)
                    ?? PrefixCheckTwoSfx(word, CompoundOptions.Not, default);
            }

            // check forbidden words
            if (rv is not null && rv.Detail.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag, Affix.NoSuggest, Affix.OnlyInCompound))
            {
                return 0;
            }

            if (rv is not null)
            {
                // XXX obsolete
                if (rv.ContainsFlag(Affix.CompoundFlag))
                {
                    return noSuffix ? 3 : 2;
                }

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Suggestions for a typical fault of spelling, that
        /// differs with more, than 1 letter from the right form.
        /// </summary>
        private int ReplChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2 || WordList.AllReplacements.IsEmpty)
            {
                return wlst.Count;
            }

            foreach (var replacement in WordList.AllReplacements)
            {
                if (string.IsNullOrEmpty(replacement.Pattern))
                {
                    continue;
                }

                // search every occurence of the pattern in the word
                for (
                    var r = word.IndexOf(replacement.Pattern, StringComparison.Ordinal)
                    ;
                    r >= 0
                    ; 
                    r = word.IndexOf(replacement.Pattern, r + 1, StringComparison.Ordinal) // search for the next letter
                )
                {
                    var type = (r == 0) ? ReplacementValueType.Ini : ReplacementValueType.Med;
                    if (r + replacement.Pattern.Length == word.Length)
                    {
                        type |= ReplacementValueType.Fin;
                    }

                    while (type != ReplacementValueType.Med && string.IsNullOrEmpty(replacement[type]))
                    {
                        type = (type == ReplacementValueType.Fin && r != 0) ? ReplacementValueType.Med : type - 1;
                    }

                    if (!string.IsNullOrEmpty(replacement[type]))
                    {
                        var candidate = StringEx.ConcatString(word, 0, r, replacement[type], word, r + replacement.Pattern.Length);

                        TestSug(wlst, candidate, cpdSuggest);

                        // check REP suggestions with space
                        var sp = candidate.IndexOf(' ');
                        var prev = 0;
                        while (sp >= 0)
                        {
                            if (CheckWord(candidate.AsSpan(prev, sp - prev), false) != 0)
                            {
                                var oldNs = wlst.Count;
                                TestSug(wlst, candidate.AsSpan(sp + 1), cpdSuggest);
                                if (oldNs < wlst.Count)
                                {
                                    wlst[wlst.Count - 1] = candidate;
                                }
                            }

                            prev = sp + 1;
                            sp = candidate.IndexOf(' ', prev);
                        }
                    }
                }
            }

            return wlst.Count;
        }

        /// <summary>
        /// Generate a set of suggestions for very poorly spelled words.
        /// </summary>
        private void NGramSuggest(List<string> wlst, string word, CapitalizationType capType)
        {
            int lval;
            int sc;

            // exhaustively search through all root words
            // keeping track of the MAX_ROOTS most similar root words
            var roots = new NGramSuggestSearchRoot[MaxRoots]; // TODO: this should come from a pool
            for (var i = 0; i < roots.Length; i++)
            {
                roots[i] = new NGramSuggestSearchRoot(i);
            }

            var lp = roots.Length - 1;
            var lpphon = lp;

            // word reversing wrapper for complex prefixes
            if (Affix.ComplexPrefixes)
            {
                word = word.GetReversed();
            }

            var hasPhoneEntries = Affix.Phone.HasItems;
            var textInfo = TextInfo;
            var target = hasPhoneEntries
                ? Phonet(HunspellTextFunctions.MakeAllCap(word, textInfo))
                : string.Empty;
            var isNonGermanLowercase = !Affix.IsGerman && capType == CapitalizationType.None;

            var minAllowedLength = Math.Max(word.Length - 4, 0);
            var maxAllowedLength = word.Length + 4;

            foreach (var hpSet in WordList.GetNGramAllowedDetails(key =>
            {
                // skip it, if the word length different by 5 or
                // more characters (to avoid strange suggestions)
                return key.Length >= minAllowedLength && key.Length <= maxAllowedLength;
            }))
            {
                var wordKeyLengthDifference = Math.Abs(word.Length - hpSet.Key.Length);

                foreach (var hpDetail in hpSet.Value)
                {
                    // don't suggest capitalized dictionary words for
                    // lower case misspellings in ngram suggestions, except
                    // - PHONE usage, or
                    // - in the case of German, where not only proper
                    //   nouns are capitalized, or
                    // - the capitalized word has special pronunciation
                    if (isNonGermanLowercase && EnumEx.HasFlag(hpDetail.Options, WordEntryOptions.InitCap) && !hasPhoneEntries && !EnumEx.HasFlag(hpDetail.Options, WordEntryOptions.Phon))
                    {
                        continue;
                    }

                    sc = NGram(3, word, hpSet.Key, NGramOptions.LongerWorse | NGramOptions.Lowering)
                        + LeftCommonSubstring(word, hpSet.Key);

                    // check special pronunciation
                    var f = string.Empty;
                    if (EnumEx.HasFlag(hpDetail.Options, WordEntryOptions.Phon) && QuerySuggest.CopyField(ref f, hpDetail.Morphs, MorphologicalTags.Phon))
                    {
                        var sc2 = NGram(3, word, f, NGramOptions.LongerWorse | NGramOptions.Lowering)
                            + LeftCommonSubstring(word, f);

                        if (sc2 > sc)
                        {
                            sc = sc2;
                        }
                    }

                    var scphon = -20000;
                    if (hasPhoneEntries && sc > 2 && wordKeyLengthDifference <= 3)
                    {
                        scphon = NGram(3, target, Phonet(HunspellTextFunctions.MakeAllCap(hpSet.Key, textInfo)), NGramOptions.LongerWorse) * 2;
                    }

                    if (sc > roots[lp].Score)
                    {
                        roots[lp].Score = sc;
                        roots[lp].Root = new WordEntry(hpSet.Key, hpDetail);
                        lval = sc;
                        for (var j = 0; j < roots.Length; j++)
                        {
                            if (roots[j].Score < lval)
                            {
                                lp = j;
                                lval = roots[j].Score;
                            }
                        }
                    }

                    if (scphon > roots[lpphon].ScorePhone)
                    {
                        roots[lpphon].ScorePhone = scphon;
                        roots[lpphon].RootPhon = hpSet.Key;
                        lval = scphon;
                        for (var j = 0; j < roots.Length; j++)
                        {
                            if (roots[j].ScorePhone < lval)
                            {
                                lpphon = j;
                                lval = roots[j].ScorePhone;
                            }
                        }
                    }
                }
            }

            // find minimum threshold for a passable suggestion
            // mangle original word three differnt ways
            // and score them to generate a minimum acceptable score
            var thresh = 0;
            var mw = StringBuilderPool.Get(word.Length);
            for (var sp = 1; sp < 4; sp++)
            {
                mw.Clear();
                mw.Append(word);

                for (var k = sp; k < mw.Length; k += 4)
                {
                    mw[k] = '*';
                }

                thresh += NGram(word.Length, word, mw.ToString(), NGramOptions.AnyMismatch | NGramOptions.Lowering);
            }

            StringBuilderPool.Return(mw);

            thresh = (thresh / 3) - 1;

            // now expand affixes on each of these root words and
            // and use length adjusted ngram scores to select
            // possible suggestions
            var guesses = new NGramGuess[MaxGuess];
            for (var i = 0; i < guesses.Length; i++)
            {
                guesses[i] = new NGramGuess(i);
            }

            lp = guesses.Length - 1;

            var glst = new GuessWord[MaxWords];
            for (var i = 0; i < roots.Length; i++)
            {
                var rp = roots[i].Root;
                if (rp is not null)
                {
                    var field = string.Empty;
                    if (!EnumEx.HasFlag(rp.Detail.Options, WordEntryOptions.Phon) || !QuerySuggest.CopyField(ref field, rp.Detail.Morphs, MorphologicalTags.Phon))
                    {
                        field = null;
                    }

                    int nw = ExpandRootWord(glst, rp, word, field);

                    for (var k = 0; k < nw; k++)
                    {
                        ref var guessWordK = ref glst[k];
                        sc = NGram(word.Length, word, guessWordK.Word, NGramOptions.AnyMismatch | NGramOptions.Lowering)
                            + LeftCommonSubstring(word, guessWordK.Word);

                        if (sc > thresh)
                        {
                            ref var guessNGramLp = ref guesses[lp];
                            if (sc > guessNGramLp.Score)
                            {
                                guessNGramLp.Score = sc;
                                guessNGramLp.Guess = guessWordK.Word;
                                guessNGramLp.GuessOrig = guessWordK.Orig;
                                lval = sc;
                                for (var j = 0; j < guesses.Length; j++)
                                {
                                    ref var guessNGramJ = ref guesses[j];
                                    if (guessNGramJ.Score < lval)
                                    {
                                        lp = j;
                                        lval = guessNGramJ.Score;
                                    }
                                }
                            }
                            else
                            {
                                guessWordK.ClearWordAndOrig();
                            }
                        }
                        else
                        {
                            guessWordK.ClearWordAndOrig();
                        }
                    }
                }
            }

            glst = null;

            // now we are done generating guesses
            // sort in order of decreasing score

            Array.Sort(guesses, (a, b) => b.Score.CompareTo(a.Score));
            if (hasPhoneEntries)
            {
                Array.Sort(roots, (a, b) => b.ScorePhone.CompareTo(a.ScorePhone));
            }

            // weight suggestions with a similarity index, based on
            // the longest common subsequent algorithm and resort

            var fact = 1.0;
            var isSwap = false;

            if (Affix.MaxDifferency.HasValue && Affix.MaxDifferency.GetValueOrDefault() >= 0)
            {
                fact = (10.0 - Affix.MaxDifferency.GetValueOrDefault()) / 5.0;
            }

            for (var i = 0; i < guesses.Length; i++)
            {
                var guess = guesses[i];
                if (guess.Guess is not null)
                {
                    // lowering guess[i]
                    var gl = HunspellTextFunctions.MakeAllSmall(guess.Guess, textInfo);
                    var len = guess.Guess.Length;

                    var lcsLength = LcsLen(word, gl);

                    // same characters with different casing
                    if (word.Length == len && word.Length == lcsLength)
                    {
                        guesses[i].Score += 2000;
                        break;
                    }

                    // using 2-gram instead of 3, and other weightening
                    var re = NGram(2, word, gl, NGramOptions.AnyMismatch | NGramOptions.Weighted | NGramOptions.Lowering)
                        + NGram(2, gl, word, NGramOptions.AnyMismatch | NGramOptions.Weighted | NGramOptions.Lowering);

                    guesses[i].Score =
                        // length of longest common subsequent minus length difference
                        (2 * lcsLength) - Math.Abs(word.Length - len)
                        // weight length of the left common substring
                        + LeftCommonSubstring(word, gl)
                        // weight equal character positions
                        + ((CommonCharacterPositions(word, gl, ref isSwap) != 0) ? 1 : 0)
                        // swap character (not neighboring)
                        + (isSwap ? 10 : 0)
                        // ngram
                        + NGram(4, word, gl, NGramOptions.AnyMismatch | NGramOptions.Lowering)
                        // weighted ngrams
                        + re
                        // different limit for dictionaries with PHONE rules
                        + (hasPhoneEntries ? (re < len * fact ? -1000 : 0) : (re < (word.Length + len) * fact ? -1000 : 0));
                }
            }

            Array.Sort(guesses, (a, b) => b.Score.CompareTo(a.Score));

            // phonetic version
            if (hasPhoneEntries)
            {
                for (var i = 0; i < roots.Length; i++)
                {
                    var root = roots[i];
                    if (root.RootPhon is not null)
                    {
                        // lowering rootphon[i]
                        var gl = HunspellTextFunctions.MakeAllSmall(root.RootPhon, textInfo);
                        var len = root.RootPhon.Length;

                        // heuristic weigthing of ngram scores
                        roots[i].ScorePhone += 2 * LcsLen(word, gl) - Math.Abs(word.Length - len)
                            // weight length of the left common substring
                            + LeftCommonSubstring(word, gl);
                    }
                }

                Array.Sort(roots, (a, b) => b.ScorePhone.CompareTo(a.ScorePhone));
            }

            // copy over
            var oldns = wlst.Count;

            var wlstLimit = Math.Min(MaxSuggestions, oldns + Affix.MaxNgramSuggestions);

            var same = false;
            for (var i = 0; i < guesses.Length; i++)
            {
                ref var guess = ref guesses[i];
                if (guess.Guess is not null)
                {
                    if (
                        wlst.Count < wlstLimit
                        &&
                        (
                            !same
                            ||
                            guess.Score > 1000
                        )
                    )
                    {
                        // leave only excellent suggestions, if exists
                        if (guess.Score > 1000)
                        {
                            same = true;
                        }
                        else if (guess.Score < -100)
                        {
                            same = true;
                            // keep the best ngram suggestions, unless in ONLYMAXDIFF mode
                            if (
                                wlst.Count > oldns
                                ||
                                Affix.OnlyMaxDiff
                            )
                            {
                                guess.ClearGuessAndOrig();
                                continue;
                            }
                        }

                        var unique = true;
                        for (var j = 0; j < wlst.Count; j++)
                        {
                            // don't suggest previous suggestions or a previous suggestion with
                            // prefixes or affixes
                            if (
                                (guess.GuessOrig is null && guess.Guess.Contains(wlst[j]))
                                ||
                                (guess.GuessOrig is not null && guess.GuessOrig.Contains(wlst[j]))
                                || // check forbidden words
                                CheckWord(guess.Guess, false) == 0
                            )
                            {
                                unique = false;
                                break;
                            }
                        }

                        if (unique)
                        {
                            wlst.Add(guess.GuessOrig ?? guess.Guess);
                        }
                    }

                    guess.ClearGuessAndOrig();
                }
            }

            oldns = wlst.Count;
            wlstLimit = Math.Min(MaxSuggestions, oldns + MaxPhonSugs);

            if (hasPhoneEntries)
            {
                for (var i = 0; i < roots.Length; i++)
                {
                    ref var root = ref roots[i];
                    if (root.RootPhon is not null && wlst.Count < wlstLimit)
                    {
                        var unique = true;
                        for (var j = 0; j < wlst.Count; j++)
                        {
                            // don't suggest previous suggestions or a previous suggestion with
                            // prefixes or affixes
                            if (
                                root.RootPhon.Contains(wlst[j])
                                || // check forbidden words
                                CheckWord(root.RootPhon, false) == 0
                            )
                            {
                                unique = false;
                                break;
                            }
                        }

                        if (unique)
                        {
                            wlst.Add(root.RootPhon);
                        }
                    }
                }
            }
        }

        private int CommonCharacterPositions(string s1, string s2, ref bool isSwap)
        {
            // decapitalize dictionary word
            var t = Affix.ComplexPrefixes
                ? s2.AsSpan(0, s2.Length - 1).ConcatString(TextInfo.ToLower(s2[s2.Length - 1]))
                : HunspellTextFunctions.MakeAllSmall(s2, TextInfo);

            var num = 0;
            var diff = 0;
            var diffPos0 = 0;
            var diffPos1 = 0;
            int i;
            for (i = 0; i < t.Length && i < s1.Length; i++)
            {
                if (s1[i] == t[i])
                {
                    num++;
                }
                else
                {
                    if (diff == 0)
                    {
                        diffPos0 = i;
                    }
                    else if (diff == 1)
                    {
                        diffPos1 = i;
                    }

                    diff++;
                }
            }

            isSwap =
                (
                    diff == 2
                    &&
                    i == t.Length
                    &&
                    i < s1.Length
                    &&
                    s1[diffPos0] == t[diffPos1]
                    &&
                    s1[diffPos1] == t[diffPos0]
                );

            return num;
        }

        private static int LcsLen(string s, string s2)
        {
            Lcs(s, s2, out int i, out int n, out LongestCommonSubsequenceType[] result);

            if (result is null)
            {
                return 0;
            }

            var j = n;
            var len = 0;
            while (i != 0 && j != 0)
            {
                if (result[(i * (n + 1)) + j] == LongestCommonSubsequenceType.UpLeft)
                {
                    len++;
                    i--;
                    j--;
                }
                else if (result[(i * (n + 1)) + j] == LongestCommonSubsequenceType.Up)
                {
                    i--;
                }
                else
                {
                    j--;
                }
            }

            return len;
        }

        /// <summary>
        /// Longest common subsequence.
        /// </summary>
        private static void Lcs(string s, string s2, out int m, out int n, out LongestCommonSubsequenceType[] b)
        {
            m = s.Length;
            n = s2.Length;
            var nNext = n + 1;
            var c = new LongestCommonSubsequenceType[(m + 1) * nNext]; // NOTE: arrays are already zero (Up);
            b = new LongestCommonSubsequenceType[(m + 1) * nNext]; // NOTE: arrays are already zero (Up);

            for (var i = 1; i <= m; i++)
            {
                var iPrev = i - 1;
                for (var j = 1; j <= n; j++)
                {
                    var inj = i * nNext + j;
                    ref var cInj = ref c[inj];
                    ref var bInj = ref b[inj];

                    var jPrev = j - 1;
                    var iPrevXNNext = iPrev * nNext;
                    if (((s[iPrev] == s2[jPrev])))
                    {
                        cInj = c[iPrevXNNext + jPrev] + 1;
                        bInj = LongestCommonSubsequenceType.UpLeft;
                        continue;
                    }

                    var cIPrevXNNext = c[iPrevXNNext + j];
                    var cInjMinux1 = c[inj - 1];
                    if (cIPrevXNNext >= cInjMinux1)
                    {
                        cInj = cIPrevXNNext;
                        bInj = LongestCommonSubsequenceType.Up;
                    }
                    else
                    {
                        cInj = cInjMinux1;
                        bInj = LongestCommonSubsequenceType.UpLeft;
                    }
                }
            }
        }

        private int ExpandRootWord(GuessWord[] wlst, WordEntry entry, string bad, string phon)
        {
            var nh = 0;
            // first add root word to list
            if (nh < wlst.Length && !entry.Detail.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound))
            {
                wlst[nh].Word = entry.Word;
                if (wlst[nh].Word is null)
                {
                    return 0;
                }

                wlst[nh].Allow = false;
                wlst[nh].Orig = null;
                nh++;

                // add special phonetic version
                if (phon is not null && nh < wlst.Length)
                {
                    wlst[nh].Word = phon;
                    if (wlst[nh].Word is null)
                    {
                        return nh - 1;
                    }

                    wlst[nh].Allow = false;
                    wlst[nh].Orig = entry.Word;
                    if (wlst[nh].Orig is null)
                    {
                        return nh - 1;
                    }

                    nh++;
                }
            }

            // handle suffixes
            if (Affix.Suffixes.HasAffixes)
            {
                foreach (var sptrGroup in Affix.Suffixes.GetByFlags(entry.Detail.Flags))
                {
                    foreach (var sptr in sptrGroup.Entries)
                    {
                        var key = sptr.Key;
                        if (
                            (
                                string.IsNullOrEmpty(key)
                                ||
                                (
                                    bad.Length > key.Length
                                    &&
                                    bad.AsSpan(bad.Length - key.Length).EqualsOrdinal(sptr.Append.AsSpan())
                                )
                            )
                            && // check needaffix flag
                            !sptr.ContainsAnyContClass(Affix.NeedAffix, Affix.Circumfix, Affix.OnlyInCompound)
                        )
                        {
                            var newword = Add(sptr, entry.Word);
                            if (newword.Length != 0)
                            {
                                if (nh < wlst.Length)
                                {
                                    wlst[nh].Word = newword;
                                    wlst[nh].Allow = sptrGroup.AllowCross;
                                    wlst[nh].Orig = null;
                                    nh++;

                                    // add special phonetic version
                                    if (phon is not null && nh < wlst.Length)
                                    {
                                        wlst[nh].Word = phon + key.GetReversed();
                                        if (wlst[nh].Word is null)
                                        {
                                            return nh - 1;
                                        }

                                        wlst[nh].Allow = false;
                                        wlst[nh].Orig = newword;
                                        if (wlst[nh].Orig is null)
                                        {
                                            return nh - 1;
                                        }

                                        nh++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var n = nh;

            // handle cross products of prefixes and suffixes
            if (Affix.Prefixes.HasAffixes)
            {
                for (var j = 1; j < n; j++)
                {
                    if (!wlst[j].Allow)
                    {
                        continue;
                    }

                    foreach (var pfxGroup in Affix.Prefixes.GetByFlags(entry.Detail.Flags))
                    {
                        foreach (var cptr in pfxGroup.Entries)
                        {
                            if (
                                pfxGroup.AllowCross
                                &&
                                (
                                    cptr.Key.Length == 0
                                    ||
                                    (
                                        bad.Length > cptr.Key.Length
                                        &&
                                        bad.StartsWith(cptr.Key, StringComparison.Ordinal)
                                    )
                                )
                            )
                            {
                                var newword = Add(cptr, wlst[j].Word);
                                if (newword.Length != 0)
                                {
                                    if (nh < wlst.Length)
                                    {
                                        wlst[nh].Word = newword;
                                        wlst[nh].Allow = pfxGroup.AllowCross;
                                        wlst[nh].Orig = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // now handle pure prefixes
            if (Affix.Prefixes.HasAffixes)
            {
                foreach (var ptrGroup in Affix.Prefixes.GetByFlags(entry.Detail.Flags))
                {
                    foreach (var ptr in ptrGroup.Entries)
                    {
                        var key = ptr.Key;
                        if (
                            (
                                key.Length == 0
                                ||
                                (
                                    bad.Length > key.Length
                                    &&
                                    bad.StartsWith(key, StringComparison.Ordinal)
                                )
                            )
                            && // check needaffix flag
                            !ptr.ContainsAnyContClass(Affix.NeedAffix, Affix.Circumfix, Affix.OnlyInCompound)
                        )
                        {
                            var newword = Add(ptr, entry.Word);
                            if (newword.Length != 0 && nh < wlst.Length)
                            {
                                wlst[nh].Word = newword;
                                wlst[nh].Allow = ptrGroup.AllowCross;
                                wlst[nh].Orig = null;
                                nh++;
                            }
                        }
                    }
                }
            }

            return nh;
        }

        /// <summary>
        /// Error if should have been two words.
        /// </summary>
        /// <returns>Trye if there is a dictionary word pair or there was already a good suggestion before calling.</returns>
        private bool TwoWords(List<string> wlst, string word, bool cpdSuggest, bool good)
        {
            if (word.Length < 3)
            {
                return false;
            }

            var isHungarianAndNotForbidden = Affix.IsHungarian && !CheckForbidden(word);

            var candidate = StringBuilderPool.Get(word.Length + 2);
            candidate.Append('\0');
            candidate.Append(word);

            // split the string into two pieces after every char
            // if both pieces are good words make them a suggestion

            for (var p = 1; p + 1 < candidate.Length; p++)
            {
                string currentCandidateString;

                candidate[p - 1] = candidate[p];

                // Suggest only word pairs, if they are listed in the dictionary.
                // For example, adding "a lot" to the English dic file will
                // result only "alot" -> "a lot" suggestion instead of
                // "alto, slot, alt, lot, allot, aloft, aloe, clot, plot, blot, a lot".
                // Note: using "ph:alot" keeps the other suggestions:
                // a lot ph:alot
                // alot -> a lot, alto, slot...
                candidate[p] = ' ';
                currentCandidateString = candidate.ToStringTerminated();
                if (!cpdSuggest && CheckWord(currentCandidateString, cpdSuggest) != 0)
                {
                    // remove not word pair suggestions
                    if (!good)
                    {
                        good = true;
                        wlst.Clear();
                    }
                    wlst.Insert(0, currentCandidateString);
                }

                // word pairs with dash?
                if (Affix.IsLanguageWithDashUsage)
                {
                    candidate[p] = '-';
                    currentCandidateString = candidate.ToStringTerminated();
                    if (!cpdSuggest && CheckWord(currentCandidateString, cpdSuggest) != 0)
                    {
                        // remove not word pair suggestions
                        if (!good)
                        {
                            good = true;
                            wlst.Clear();
                        }
                        wlst.Insert(0, currentCandidateString);
                    }
                }

                if (wlst.Count < MaxSuggestions && !Affix.NoSplitSuggestions && !good)
                {
                    candidate[p] = '\0';

                    var c1 = CheckWord(candidate.ToStringTerminated(), cpdSuggest);
                    if (c1 != 0)
                    {
                        var c2 = CheckWord(candidate.ToStringTerminated(p + 1), cpdSuggest);
                        if (c2 != 0)
                        {
                            // spec. Hungarian code (need a better compound word support)
                            candidate[p] =
                                (
                                    isHungarianAndNotForbidden
                                    && // if 3 repeating letter, use - instead of space
                                    (
                                        (
                                            candidate[p - 1] == candidate[p]
                                            &&
                                            (
                                                (
                                                    p > 1
                                                    &&
                                                    candidate[p - 1] == candidate[p - 2]
                                                )
                                                ||
                                                (
                                                    candidate[p - 1] == candidate[p + 2]
                                                )
                                            )
                                        )
                                        || // or multiple compounding, with more, than 6 syllables
                                        (
                                            c1 == 3
                                            &&
                                            c2 >= 2
                                        )
                                    )
                                ) ? '-' : ' ';

                            var cwrd = true;
                            currentCandidateString = candidate.ToStringTerminated();
                            for (var k = 0; k < wlst.Count; k++)
                            {
                                if (string.Equals(wlst[k], currentCandidateString, StringComparison.Ordinal))
                                {
                                    cwrd = false;
                                    break;
                                }
                            }

                            if (cwrd && wlst.Count < MaxSuggestions)
                            {
                                wlst.Add(currentCandidateString);
                            }

                            // add two word suggestion with dash, depending on the language
                            // Note that cwrd doesn't modified for REP twoword sugg.
                            if (
                                !Affix.NoSplitSuggestions
                                &&
                                Affix.IsLanguageWithDashUsage
                                &&
                                p > 1
                                &&
                                candidate.Length - (p + 1) > 1
                            )
                            {
                                candidate[p] = '-';
                                currentCandidateString = candidate.ToStringTerminated();

                                for (var k = 0; k < wlst.Count; k++)
                                {
                                    if (string.Equals(wlst[k], currentCandidateString, StringComparison.Ordinal))
                                    {
                                        cwrd = false;
                                        break;
                                    }
                                }

                                if (cwrd && wlst.Count < MaxSuggestions)
                                {
                                    wlst.Add(currentCandidateString);
                                }
                            }
                        }
                    }
                }
            }

            StringBuilderPool.Return(candidate);

            return good;
        }

        private bool CheckForbidden(string word)
        {
#if DEBUG
            if (word is null) throw new ArgumentNullException(nameof(word));
#endif
            var rv = LookupFirstDetail(word);
            if (rv is not null && rv.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound))
            {
                rv = null;
            }

            if (PrefixCheck(word, CompoundOptions.Begin, default) is null)
            {
                rv = SuffixCheck(word, AffixEntryOptions.None, default, default, default, CompoundOptions.Not)?.Detail; // prefix+suffix, suffix
            }

            // check forbidden words
            return rv is not null && rv.ContainsFlag(Affix.ForbiddenWord);
        }

        /// <summary>
        /// Add prefix to this word assuming conditions hold.
        /// </summary>
        private string Add(PrefixEntry entry, string word)
        {
            return
                (
                    (
                        word.Length > entry.Strip.Length
                        ||
                        (
                            word.Length == 0
                            &&
                            Affix.FullStrip
                        )
                    )
                    &&
                    entry.TestCondition(word.AsSpan())
                    &&
                    (
                        entry.Strip.Length == 0
                        ||
                        word.StartsWith(entry.Strip, StringComparison.Ordinal)
                    )
                )
                // we have a match so add prefix
                ? StringEx.ConcatString(entry.Append, word, entry.Strip.Length, word.Length - entry.Strip.Length)
                : string.Empty;
        }

        /// <summary>
        /// Add suffix to this word assuming conditions hold.
        /// </summary>
        private string Add(SuffixEntry entry, string word)
        {
            // make sure all conditions match
            return
                (
                    (
                        word.Length > entry.Strip.Length
                        ||
                        (
                            word.Length == 0
                            &&
                            Affix.FullStrip
                        )
                    )
                    &&
                    entry.TestCondition(word.AsSpan())
                    &&
                    (
                        entry.Strip.Length == 0
                        ||
                        word.AsSpan(word.Length - entry.Strip.Length).Equals(entry.Strip.AsSpan(), StringComparison.Ordinal)
                    )
                )
                // we have a match so add suffix
                ? StringEx.ConcatString(word, 0, word.Length - entry.Strip.Length, entry.Append)
                : string.Empty;
        }

        private static bool CopyField(ref string dest, MorphSet morphs, string var)
        {
            if (morphs.Count == 0)
            {
                return false;
            }

            var morph = morphs.Join(" ");

            if (morph.Length == 0)
            {
                return false;
            }

            var pos = morph.IndexOf(var, StringComparison.Ordinal);
            if (pos < 0)
            {
                return false;
            }

            var begOffset = pos + MorphologicalTags.TagLength;
            var builder = StringBuilderPool.Get(morph.Length - begOffset);

            for (var i = begOffset; i < morph.Length; i++)
            {
                var c = morph[i];
                if (c == ' ' || c == '\t' || c == '\n')
                {
                    break;
                }

                builder.Append(c);
            }

            dest = StringBuilderPool.GetStringAndReturn(builder);
            return true;
        }

        /// <summary>
        /// Length of the left common substring of s1 and (decapitalised) s2.
        /// </summary>
        private int LeftCommonSubstring(string s1, string s2)
        {
            if (Affix.ComplexPrefixes)
            {
                return leftCommonSubstringComplex(s1, s2);
            }

            if (s1[0] != s2[0] && s1[0] != TextInfo.ToLower(s2[0]))
            {
                return 0;
            }

            var minIndex = Math.Min(s1.Length, s2.Length);
            int index = 1;

            for ( ; index < minIndex && s1[index] == s2[index]; index++) ;

            return index;

            static int leftCommonSubstringComplex(string s1, string s2) =>
                (
                    s1.Length > 0
                    &&
                    s2.Length > 0
                    &&
                    s1[s1.Length - 1] == s2[s2.Length - 1]
                )
                ? 1
                : 0;
        }

        /// <summary>
        /// Generate an n-gram score comparing s1 and s2.
        /// </summary>
        private int NGram(int n, string s1, string s2, NGramOptions opt)
        {
            if (s2.Length == 0)
            {
                return 0;
            }

            if (HasFlag(opt, NGramOptions.Lowering))
            {
                s2 = HunspellTextFunctions.MakeAllSmall(s2, TextInfo);
            }

            var nscore = HasFlag(opt, NGramOptions.Weighted)
                ? NGramWeightedSearch(n, s1.AsSpan(), s2.AsSpan())
                : NGramNonWeightedSearch(n, s1.AsSpan(), s2.AsSpan());

            int ns;
            if (HasFlag(opt, NGramOptions.AnyMismatch))
            {
                ns = Math.Abs(s2.Length - s1.Length) - 2;
            }
            else if (HasFlag(opt, NGramOptions.LongerWorse))
            {
                ns = (s2.Length - s1.Length) - 2;
            }
            else
            {
                ns = 0;
            }

            if (ns > 0)
            {
                nscore -= ns;
            }

            return nscore;
        }

        private static int NGramWeightedSearch(int n, ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            int nscore = NGramWeightedSearch_Iteration1(s1, t);
            for (var nGramLength = 2; nGramLength <= n; nGramLength++)
            {
                nscore += NGramWeightedSearch_IterationN(nGramLength, s1, t);
            }

            return nscore;
        }

        private static int NGramWeightedSearch_Iteration1(ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            var ns = 0;
            var maxSearchIndex = s1.Length - 1;
            for (var i = 0; i <= maxSearchIndex; ++i)
            {
                if (t.Contains(s1[i]))
                {
                    ++ns;
                }
                else
                {
                    if (i == 0 || i == maxSearchIndex)
                    {
                        ns -= 2;
                    }
                    else
                    {
                        --ns;
                    }
                }
            }

            return ns;
        }

        private static int NGramWeightedSearch_IterationN(int nGramLength, ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            var ns = 0;
            var maxSearchIndex = s1.Length - nGramLength;
            for (var i = 0; i <= maxSearchIndex; ++i)
            {
                if (t.Contains(s1.Slice(i, nGramLength)))
                {
                    ++ns;
                }
                else
                {
                    if (i == 0 || i == maxSearchIndex)
                    {
                        ns -= 2;
                    }
                    else
                    {
                        --ns;
                    }
                }
            }

            return ns;
        }

        private static int NGramNonWeightedSearch(int n, ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            int ns = NGramNonWeightedSearch_Iteration1(s1, t);
            int nscore = ns;
            for (var nGramLength = 2; nGramLength <= n && ns >= 2; ++nGramLength)
            {
                ns = NGramNonWeightedSearch_IterationN(nGramLength, s1, t);
                nscore += ns;
            }

            return nscore;
        }

        private static int NGramNonWeightedSearch_Iteration1(ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            var ns = 0;
            for (var i = 0; i < s1.Length; ++i)
            {
                if (t.Contains(s1[i]))
                {
                    ++ns;
                }
            }
            return ns;
        }

        private static int NGramNonWeightedSearch_IterationN(int nGramLength, ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
            var ns = 0;
            var maxIndex = s1.Length - nGramLength;
            for (var i = 0; i <= maxIndex; ++i)
            {
                if (t.Contains(s1.Slice(i, nGramLength)))
                {
                    ++ns;
                }
            }
            return ns;
        }

        /// <summary>
        /// Do phonetic transformation.
        /// </summary>
        /// <param name="inword">An uppercase string.</param>
        /// <remarks>
        /// Phonetic transcription algorithm
        /// see: http://aspell.net/man-html/Phonetic-Code.html
        /// convert string to uppercase before this call
        /// </remarks>
        private string Phonet(string inword)
        {
            var len = inword.Length;
            if (len > MaxPhoneTUtf8Len)
            {
                return string.Empty;
            }

            var word = StringBuilderPool.Get(inword.AsSpan(0, Math.Min(inword.Length, MaxPhoneTUtf8Len)));
            var target = StringBuilderPool.Get();

            // check word
            var i = 0;
            var z = false;
            var p0 = -333;
            var k = 0;
            int p;
            int k0;
            char c;

            while ((c = word.GetCharOrTerminator(i)) != '\0')
            {
                var z0 = false;

                // check all rules for the same letter
                foreach (var phoneEntry in Affix.Phone.Where(pe => pe.Rule.StartsWith(c)))
                {
                    // check whole string
                    k = 1; // number of found letters
                    p = 5; // default priority
                    var sString = phoneEntry.Rule;
                    var sIndex = 0;
                    sIndex++; // important for (see below)  "*(s-1)"
                    var sChar = sString.GetCharOrTerminator(sIndex);

                    while (sChar != '\0' && word.GetCharOrTerminator(i + k) == sChar && !char.IsDigit(sChar) && !"(-<^$".Contains(sChar))
                    {
                        k++;
                        sChar = sString.GetCharOrTerminator(++sIndex);
                    }

                    if (sChar == '(')
                    {
                        // check letters in "(..)"
                        if (
                            HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i + k)) // NOTE: could be implied?
                            &&
                            sString.IndexOf(word.GetCharOrTerminator(i + k), sIndex + 1) >= 0
                        )
                        {
                            k++;
                            while (sChar != ')' && sChar != '\0')
                            {
                                sChar = sString.GetCharOrTerminator(++sIndex);
                            }

                            sChar = sString.GetCharOrTerminator(++sIndex);
                        }
                    }

                    p0 = (int)sChar;
                    k0 = k;

                    while (sChar == '-' && k > 1)
                    {
                        k--;
                        sChar = sString.GetCharOrTerminator(++sIndex);
                    }

                    if (sChar == '<')
                    {
                        sChar = sString.GetCharOrTerminator(++sIndex);
                    }

                    if (sChar < 128 && char.IsDigit(sChar))
                    {
                        // determine priority
                        p = sChar - '0';
                        sChar = sString.GetCharOrTerminator(++sIndex);
                    }

                    if (sChar == '^' && sString.GetCharOrTerminator(sIndex + 1) == '^')
                    {
                        sChar = sString.GetCharOrTerminator(++sIndex);
                    }

                    if (
                        sChar == '\0'
                        ||
                        (
                            sChar == '^'
                            &&
                            (
                                i == 0
                                ||
                                !HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i - 1))
                            )
                            &&
                            (
                                sString.GetCharOrTerminator(sIndex + 1) != '$'
                                ||
                                !HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i + k0))
                            )
                        )
                        ||
                        (
                            sChar == '$'
                            &&
                            i > 0
                            &&
                            HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i - 1))
                            &&
                            !HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i + k0))
                        )
                    )
                    {
                        // search for followup rules, if:
                        // parms.followup and k > 1  and  NO '-' in searchstring

                        var c0 = word.GetCharOrTerminator(i + k - 1);

                        if (k > 1 && p0 != '-' && word.GetCharOrTerminator(i + k) != '\0')
                        {
                            // test follow-up rule for "word[i+k]"
                            foreach (var phoneEntryNested in Affix.Phone.Where(pe => pe.Rule.StartsWith(c0)))
                            {
                                // check whole string
                                k0 = k;
                                p0 = 5;
                                sString = phoneEntryNested.Rule;
                                sChar = sString.GetCharOrTerminator(++sIndex);

                                while (sChar != '\0' && word.GetCharOrTerminator(i + k0) == sChar && !char.IsDigit(sChar) && !"(-<^$".Contains(sChar))
                                {
                                    k0++;
                                    sChar = sString.GetCharOrTerminator(++sIndex);
                                }

                                if (sChar == '(')
                                {
                                    // check letters
                                    if (HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i + k0)) && sString.IndexOf(word.GetCharOrTerminator(i + k0), sIndex + 1) >= 0)
                                    {
                                        k0++;
                                        while (sChar != ')' && sChar != '\0')
                                        {
                                            sChar = sString.GetCharOrTerminator(++sIndex);
                                        }
                                        if (sChar == ')')
                                        {
                                            sChar = sString.GetCharOrTerminator(++sIndex);
                                        }
                                    }
                                }

                                while (sChar == '-')
                                {
                                    // "k0" gets NOT reduced
                                    // because "if (k0 == k)"
                                    sChar = sString.GetCharOrTerminator(++sIndex);
                                }

                                if (sChar == '<')
                                {
                                    sChar = sString.GetCharOrTerminator(++sIndex);
                                }

                                if (char.IsDigit(sChar))
                                {
                                    p0 = sChar - '0';
                                    sChar = sString.GetCharOrTerminator(++sIndex);
                                }

                                if (
                                    sChar == '\0'
                                    ||
                                    (
                                        sChar == '$'
                                        &&
                                        !HunspellTextFunctions.MyIsAlpha(word.GetCharOrTerminator(i + k0))
                                    )
                                )
                                {
                                    if (k0 == k)
                                    {
                                        // this is just a piece of the string
                                        continue;
                                    }

                                    if (p0 < p)
                                    {
                                        // priority too low
                                        continue;
                                    }

                                    break;
                                }
                            }

                            if (p0 >= p)
                            {
                                continue;
                            }
                        }

                        // replace string
                        sString = phoneEntry.Replace;
                        sIndex = 0;
                        sChar = sString.GetCharOrTerminator(sIndex);
                        p0 = phoneEntry.Rule.IndexOf('<', 1) >= 0 ? 1 : 0;

                        if (p0 == 1 && !z)
                        {
                            // rule with '<' is used
                            if (target.Length != 0 && sChar != '\0' && (target.EndsWith(c) || target.EndsWith(sChar)))
                            {
                                target.Remove(target.Length - 1, 1);
                            }

                            z0 = true;
                            z = true;
                            k0 = 0;

                            while (sChar != '\0' && word.GetCharOrTerminator(i + k0) != '\0')
                            {
                                word[i + k0] = sChar;
                                k0++;
                                sChar = sString.GetCharOrTerminator(++sIndex);
                            }

                            if (k > k0)
                            {
                                StrMove(word, i + k0, word, i + k);
                            }

                            c = word[i];
                        }
                        else
                        {
                            // no '<' rule used
                            i += k - 1;
                            z = false;
                            while (sChar != '\0' && sString.GetCharOrTerminator(sIndex + 1) != '\0' && target.Length < len)
                            {
                                if (target.Length == 0 || !target.EndsWith(sChar))
                                {
                                    target.Append(sChar);
                                }

                                sChar = sString.GetCharOrTerminator(++sIndex);
                            }

                            // new "actual letter"
                            c = sChar;

                            if (phoneEntry.Rule.Contains("^^"))
                            {
                                if (c != '\0')
                                {
                                    target.Append(c);
                                }

                                StrMove(word, 0, word, i + 1);
                                len = 0;
                                z0 = true;
                            }
                        }

                        break;
                    }
                }

                if (!z0)
                {
                    if (k != 0 && p0 == 0 && target.Length < len && c != '\0')
                    {
                        // condense only double letters
                        target.Append(c);
                    }

                    i++;
                    z = false;
                    k = 0;
                }
            }

            StringBuilderPool.Return(word);

            return StringBuilderPool.GetStringAndReturn(target);
        }

        private static void StrMove(StringBuilder dest, int destIndex, StringBuilder src, int srcOffset)
        {
            for (var srcIndex = srcOffset; srcIndex < src.Length && destIndex < dest.Length; srcIndex++, destIndex++)
            {
                dest[destIndex] = src[srcIndex];
            }

            if (destIndex < dest.Length)
            {
                dest[destIndex] = '\0';
            }
        }

        private static void InsertSuggestion(List<string> slst, string word)
        {
            slst.Insert(0, word);
        }

        private struct NGramSuggestSearchRoot
        {
            public NGramSuggestSearchRoot(int i)
            {
                Root = null;
                Score = -100 * i;
                RootPhon = null;
                ScorePhone = Score;
            }

            public WordEntry Root;

            public string RootPhon;

            public int Score;

            public int ScorePhone;
        }

        private struct NGramGuess
        {
            public NGramGuess(int i)
            {
                Guess = null;
                GuessOrig = null;
                Score = -100 * i;
            }

            public string Guess;

            public string? GuessOrig;

            public int Score;

            public void ClearGuessAndOrig()
            {
                Guess = null;
                GuessOrig = null;
            }
        }

        private struct GuessWord
        {
            public string Word;

            public string? Orig;

            public bool Allow;

            public void ClearWordAndOrig()
            {
                Word = null;
                Orig = null;
            }
        }

        private static bool HasFlag(NGramOptions value, NGramOptions flag) => (value & flag) == flag;

        [Flags]
        private enum NGramOptions : byte
        {
            None = 0,
            LongerWorse = 1 << 0,
            AnyMismatch = 1 << 1,
            Lowering = 1 << 2,
            Weighted = 1 << 3
        }

        public enum LongestCommonSubsequenceType : byte
        {
            Up = 0,
            Left = 1,
            UpLeft = 2
        }
    }
}
