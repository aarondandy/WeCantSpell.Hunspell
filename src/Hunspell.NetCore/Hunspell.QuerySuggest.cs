using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public partial class Hunspell
    {
        private sealed class QuerySuggest : Query
        {
            /// <summary>
            /// Timelimit: max ~1/4 sec (process time on Linux) for a time consuming function.
            /// </summary>
            private const int TimeLimit = 1000 >> 2;

            private const int MinTimer = 100;

            private const int MaxPlusTimer = 100;

            public QuerySuggest(string word, WordList wordList)
                : base(word, wordList)
            {
            }

            public List<string> Suggest()
            {
                var slst = new List<string>();

                if (!WordList.HasEntries)
                {
                    return slst;
                }

                var word = WordToCheck;
                var onlyCompoundSuggest = false;

                // process XML input of the simplified API (see manual)
                if (StringEx.EqualsOffset(word, 0, DefaultXmlToken, 0, DefaultXmlToken.Length - 3))
                {
                    throw new NotImplementedException();
                }

                if (word.Length >= MaxWordUtf8Len)
                {
                    return slst;
                }

                CapitalizationType capType;
                int abbv;
                string scw;
                string tempString;

                // input conversion
                if (!Affix.InputConversions.HasReplacements || !Affix.InputConversions.TryConvert(word, out tempString))
                {
                    tempString = word;
                }

                CleanWord2(out scw, tempString, out capType, out abbv);

                if (scw.Length == 0)
                {
                    return slst;
                }

                var capWords = false;

                if (capType == CapitalizationType.None && Affix.ForceUpperCase.HasValue)
                {
                    var info = SpellCheckResultType.OrigCap;
                    if (CheckWord(scw, ref info, out tempString) != null)
                    {
                        var form = scw;
                        form = MakeInitCap(form);
                        slst.Add(form);
                        return slst;
                    }
                }

                if (capType == CapitalizationType.None)
                {
                    Suggest(slst, scw, ref onlyCompoundSuggest);
                }
                else if (capType == CapitalizationType.Init)
                {
                    capWords = true;
                    Suggest(slst, scw, ref onlyCompoundSuggest);
                    Suggest(slst, MakeAllSmall(scw), ref onlyCompoundSuggest);
                }
                else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
                {
                    if (capType == CapitalizationType.HuhInit)
                    {
                        capWords = true;
                    }

                    Suggest(slst, scw, ref onlyCompoundSuggest);

                    // something.The -> something. The
                    var dotPos = scw.IndexOf('.');
                    if (dotPos >= 0)
                    {
                        var capTypeLocal = CapitalizationTypeEx.GetCapitalizationType(scw.Subslice(dotPos + 1), Affix);
                        if (capTypeLocal == CapitalizationType.Init)
                        {
                            InsertSuggestion(slst, scw.Insert(dotPos + 1, " "));
                        }
                    }

                    if (capType == CapitalizationType.HuhInit)
                    {
                        // TheOpenOffice.org -> The OpenOffice.org
                        Suggest(slst, MakeInitSmall(scw), ref onlyCompoundSuggest);
                    }

                    var wspace = MakeAllSmall(scw);
                    if (Check(wspace))
                    {
                        InsertSuggestion(slst, wspace);
                    }

                    var prevns = slst.Count;
                    Suggest(slst, wspace, ref onlyCompoundSuggest);

                    if (capType == CapitalizationType.HuhInit)
                    {
                        wspace = MakeInitCap(wspace);
                        if (Check(wspace))
                        {
                            InsertSuggestion(slst, wspace);
                        }

                        Suggest(slst, wspace, ref onlyCompoundSuggest);
                    }

                    // aNew -> "a New" (instead of "a new")
                    for (var j = prevns; j < slst.Count; j++)
                    {
                        var spaceIndex = slst[j].IndexOf(' ');
                        if (spaceIndex >= 0)
                        {
                            var slen = slst[j].Length - spaceIndex - 1;

                            // different case after space (need capitalisation)
                            if (slen < scw.Length && !StringEx.EqualsOffset(scw, scw.Length - slen, slst[j], 1 + spaceIndex))
                            {
                                var removed = slst[j];
                                // set as first suggestion
                                slst.RemoveAt(j);
                                slst.Insert(0, StringEx.ConcatSubstring(removed, 0, spaceIndex + 1, MakeInitCap(removed.Subslice(spaceIndex + 1))));
                            }
                        }
                    }
                }
                else if (capType == CapitalizationType.All)
                {
                    var wspace = MakeAllSmall(scw);
                    Suggest(slst, wspace, ref onlyCompoundSuggest);
                    if (Affix.KeepCase.HasValue && Check(wspace))
                    {
                        InsertSuggestion(slst, wspace);
                    }

                    wspace = MakeInitCap(wspace);
                    Suggest(slst, wspace, ref onlyCompoundSuggest);
                    for (var j = 0; j < slst.Count; j++)
                    {
                        slst[j] = MakeAllCap(slst[j]).Replace("ß", "SS");
                    }
                }

                // LANG_hu section: replace '-' with ' ' in Hungarian
                if (Affix.IsHungarian)
                {
                    for (var j = 0; j < slst.Count; j++)
                    {
                        var pos = slst[j].IndexOf('-');
                        if (pos >= 0)
                        {
                            var info = CheckDetails(slst[j].Substring(0, pos) + slst[j].Substring(pos + 1)).Info;
                            var desiredChar = info.HasFlag(SpellCheckResultType.Compound) && info.HasFlag(SpellCheckResultType.Forbidden)
                                ? ' '
                                : '-';

                            if (slst[j][pos] != desiredChar)
                            {
                                slst[j] = slst[j].Substring(0, pos) + desiredChar + slst[j].Substring(pos + 1);
                            }
                        }
                    }
                }

                // try ngram approach since found nothing or only compound words
                if (Affix.MaxNgramSuggestions != 0 && (slst.Count == 0 || onlyCompoundSuggest))
                {
                    if (capType == CapitalizationType.None)
                    {
                        NGramSuggest(slst, scw);
                    }
                    else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
                    {
                        if (capType == CapitalizationType.HuhInit)
                        {
                            capWords = true;
                        }

                        NGramSuggest(slst, MakeAllSmall(scw));
                    }
                    else if (capType == CapitalizationType.Init)
                    {
                        capWords = true;
                        NGramSuggest(slst, MakeAllSmall(scw));
                    }
                    else if (capType == CapitalizationType.All)
                    {
                        var oldns = slst.Count;
                        NGramSuggest(slst, MakeAllSmall(scw));
                        for (var j = oldns; j < slst.Count; j++)
                        {
                            slst[j] = MakeAllCap(slst[j]);
                        }
                    }
                }

                // try dash suggestion (Afo-American -> Afro-American)
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

                    while (noDashSug && !last)
                    {
                        if (dashPos == scw.Length)
                        {
                            last = true;
                        }

                        var chunk = scw.Substring(prevPos, dashPos - prevPos);
                        if (!Check(chunk))
                        {
                            var nlst = Suggest(chunk);

                            foreach (var j in nlst)
                            {
                                var wspace = last
                                    ? StringEx.ConcatSubstring(scw, 0, prevPos, j)
                                    : StringEx.ConcatSubstring(scw, 0, prevPos, j, '-', scw, dashPos + 1);

                                InsertSuggestion(slst, wspace);
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
                        slst[j] = slst[j].Reverse();
                    }
                }

                // capitalize
                if (capWords)
                {
                    for (var j = 0; j < slst.Count; j++)
                    {
                        slst[j] = MakeInitCap(slst[j]);
                    }
                }

                // expand suggestions with dot(s)
                if (abbv != 0 && Affix.SuggestWithDots)
                {
                    var abbrAppend = word.Substring(word.Length - abbv);
                    for (var j = 0; j < slst.Count; j++)
                    {
                        slst[j] = StringEx.ConcatSubstring(slst[j], word, word.Length - abbv);
                    }
                }

                // remove bad capitalized and forbidden forms
                if (Affix.KeepCase.HasValue || Affix.ForbiddenWord.HasValue)
                {
                    if (capType == CapitalizationType.Init || capType == CapitalizationType.All)
                    {
                        var l = 0;
                        for (var j = 0; j < slst.Count; j++)
                        {
                            if (!slst[j].Contains(' ') && !Check(slst[j]))
                            {
                                var s = MakeAllSmall(slst[j]);
                                if (Check(s))
                                {
                                    slst[l] = s;
                                    l++;
                                }
                                else
                                {
                                    s = MakeInitCap(s);
                                    if (Check(s))
                                    {
                                        slst[l] = s;
                                        ++l;
                                    }
                                }
                            }
                            else
                            {
                                slst[l] = slst[j];
                                l++;
                            }
                        }

                        slst.RemoveRange(l, slst.Count - l);
                    }
                }

                // remove duplications
                slst = slst.Distinct(Affix.StringComparer).ToList();

                // output conversion
                if (Affix.OutputConversions.HasReplacements)
                {
                    for (var j = 0; j < slst.Count; j++)
                    {
                        string wspace;
                        if (Affix.OutputConversions.TryConvert(slst[j], out wspace))
                        {
                            slst[j] = wspace;
                        }
                    }
                }

                return slst;
            }

            private List<string> Suggest(string word) => new QuerySuggest(word, WordList).Suggest();

            private void Suggest(List<string> slst, string w, ref bool onlyCompoundSug)
            {
                var noCompoundTwoWords = false;
                var nSugOrig = slst.Count;
                var oldSug = 0;

                // word reversing wrapper for complex prefixes
                string word;
                if (Affix.ComplexPrefixes)
                {
                    word = w.Reverse();
                }
                else
                {
                    word = w;
                }

                var cpdSuggest = false;
                do
                {
                    // limit compound suggestion
                    if (cpdSuggest)
                    {
                        oldSug = slst.Count;
                    }

                    var sugLimit = oldSug + MaxSuggestions;

                    // suggestions for an uppercase word (html -> HTML)
                    if (slst.Count < MaxSuggestions)
                    {
                        CapChars(slst, word, cpdSuggest);
                    }

                    // perhaps we made a typical fault of spelling
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        ReplChars(slst, word, cpdSuggest);
                    }

                    // perhaps we made chose the wrong char from a related set
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        MapChars(slst, word, cpdSuggest);
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

                    // did we swap the order of non adjacent chars by mistake
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        LongSwapChar(slst, word, cpdSuggest);
                    }

                    // did we just hit the wrong key in place of a good char (case and keyboard)
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        BadCharKey(slst, word, cpdSuggest);
                    }

                    // did we add a char that should not be there
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        ExtraChar(slst, word, cpdSuggest);
                    }

                    // did we forgot a char
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        ForgotChar(slst, word, cpdSuggest);
                    }

                    // did we move a char
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        MoveChar(slst, word, cpdSuggest);
                    }

                    // did we just hit the wrong key in place of a good char
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        BadChar(slst, word, cpdSuggest);
                    }

                    // did we double two characters
                    if (slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        DoubleTwoChars(slst, word, cpdSuggest);
                    }

                    // perhaps we forgot to hit space and two words ran together
                    if (!Affix.NoSplitSuggestions && slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                    {
                        TwoWords(slst, word, cpdSuggest);
                    }

                    if (noCompoundTwoWords)
                    {
                        break;
                    }

                    if (cpdSuggest)
                    {
                        break;
                    }
                    else
                    {
                        cpdSuggest = true;
                    }
                }
                while (!noCompoundTwoWords);

                if (!noCompoundTwoWords && slst.Count != 0)
                {
                    onlyCompoundSug = true;
                }
            }

            private SpellCheckResult CheckDetails(string word) => new QueryCheck(word, WordList).CheckDetails();

            /// <summary>
            /// perhaps we doubled two characters (pattern aba -> ababa, for example vacation -> vacacation)
            /// </summary>
            private int DoubleTwoChars(List<string> wlst, string word, bool cpdSuggest)
            {
                if (word.Length < 5)
                {
                    return wlst.Count;
                }

                var state = 0;
                for (var i = 2; i < word.Length; i++)
                {
                    if (word[i] == word[i - 2])
                    {
                        state++;
                        if (state == 3)
                        {
                            TestSug(wlst, StringEx.ConcatSubstring(word, 0, i - 1, word, i + 1), cpdSuggest);
                            state = 0;
                        }
                    }
                    else
                    {
                        state = 0;
                    }
                }

                return wlst.Count;
            }

            /// <summary>
            /// Error is wrong char in place of correct one.
            /// </summary>
            private int BadChar(List<string> wlst, string word, bool cpdSuggest)
            {
                if (string.IsNullOrEmpty(Affix.TryString))
                {
                    return wlst.Count;
                }

                var candidate = StringBuilderPool.Get(word, word.Length);

                long? timeLimit = Environment.TickCount;
                int? timer = MinTimer;

                // swap out each char one by one and try all the tryme
                // chars in its place to see if that makes a good word
                for (var j = 0; j < Affix.TryString.Length; j++)
                {
                    for (var aI = candidate.Length - 1; aI >= 0; aI--)
                    {
                        var tmpc = candidate[aI];
                        if (Affix.TryString[j] == tmpc)
                        {
                            continue;
                        }

                        candidate[aI] = Affix.TryString[j];
                        TestSug(wlst, candidate.ToString(), cpdSuggest, ref timer, ref timeLimit);
                        if (timer == 0)
                        {
                            return wlst.Count;
                        }

                        candidate[aI] = tmpc;
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

                    for (var q = p + 1; q < candidate.Length && q - p < 10; q++)
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

                    for (var q = p - 1; q >= 0 && p - q < 10; q--)
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
                int? timer = MinTimer;
                long? timeLimit = Environment.TickCount;

                // try inserting a tryme character before every letter (and the null terminator)
                foreach (var tryChar in Affix.TryString)
                {
                    for (var index = candidate.Length; index >= 0; index--)
                    {
                        candidate.Insert(index, tryChar);
                        TestSug(wlst, candidate.ToString(), cpdSuggest, ref timer, ref timeLimit);
                        if (timer == 0)
                        {
                            return wlst.Count;
                        }

                        candidate.Remove(index, 1);
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

                var candidate = StringBuilderPool.Get(word, word.Length);

                for (var index = candidate.Length - 1; index >= 0; index--)
                {
                    var tmpc = candidate[index];
                    candidate.Remove(index, 1);
                    TestSug(wlst, candidate.ToString(), cpdSuggest);
                    candidate.Insert(index, tmpc);
                }

                StringBuilderPool.Return(candidate);

                return wlst.Count;
            }

            /// <summary>
            /// error is wrong char in place of correct one (case and keyboard related version)
            /// </summary>
            private int BadCharKey(List<string> wlst, string word, bool cpdSuggest)
            {
                var candidate = StringBuilderPool.Get(word, word.Length);

                // swap out each char one by one and try uppercase and neighbor
                // keyboard chars in its place to see if that makes a good word
                for (var i = 0; i < candidate.Length; i++)
                {
                    var tmpc = candidate[i];
                    // check with uppercase letters
                    candidate[i] = Affix.Culture.TextInfo.ToUpper(tmpc);
                    if (tmpc != candidate[i])
                    {
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                        candidate[i] = tmpc;
                    }

                    // check neighbor characters in keyboard string
                    if (string.IsNullOrEmpty(Affix.KeyString))
                    {
                        continue;
                    }

                    var loc = Affix.KeyString.IndexOf(tmpc);
                    while (loc >= 0)
                    {
                        var priorLoc = loc - 1;
                        if (priorLoc >= 0 && Affix.KeyString[priorLoc] != '|')
                        {
                            candidate[i] = Affix.KeyString[priorLoc];
                            TestSug(wlst, candidate.ToString(), cpdSuggest);
                        }

                        var nextLoc = loc + 1;
                        if (nextLoc < Affix.KeyString.Length && Affix.KeyString[nextLoc] != '|')
                        {
                            candidate[i] = Affix.KeyString[nextLoc];
                            TestSug(wlst, candidate.ToString(), cpdSuggest);
                        }

                        loc = Affix.KeyString.IndexOf(tmpc, nextLoc);
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
                    for (var q = p + 1; q < candidate.Length; q++)
                    {
                        var c = candidate[p];
                        candidate[p] = candidate[q];
                        candidate[q] = c;
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                        candidate[q] = candidate[p];
                        candidate[p] = c;
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
                TestSug(wlst, MakeAllCap(word), cpdSuggest);

            private int MapChars(List<string> wlst, string word, bool cpdSuggest)
            {
                if (word.Length < 2 || Affix.RelatedCharacterMap.IsEmpty)
                {
                    return wlst.Count;
                }

                var candidate = string.Empty;
                int? timer = MinTimer;
                long? timeLimit = Environment.TickCount;

                return MapRelated(word, ref candidate, 0, wlst, cpdSuggest, ref timer, ref timeLimit);
            }

            private int MapRelated(string word, ref string candidate, int wn, List<string> wlst, bool cpdSuggest, ref int? timer, ref long? timeLimit)
            {
                if (wn >= word.Length)
                {
                    var cwrd = true;
                    for (var m = 0; m < wlst.Count; m++)
                    {
                        if (wlst[m] == candidate)
                        {
                            cwrd = false;
                            break;
                        }
                    }

                    if (cwrd && CheckWord(candidate, cpdSuggest, ref timer, ref timeLimit) != 0)
                    {
                        if (wlst.Count < MaxSuggestions)
                        {
                            wlst.Add(candidate);
                        }
                    }

                    return wlst.Count;
                }

                var inMap = false;
                if (Affix.RelatedCharacterMap.HasItems)
                {
                    foreach (var mapEntry in Affix.RelatedCharacterMap)
                    {
                        foreach (var mapEntryValue in mapEntry)
                        {
                            if (StringEx.EqualsOffset(mapEntryValue, 0, word, wn, mapEntryValue.Length))
                            {
                                inMap = true;
                                var candidatePrefix = candidate;
                                foreach (var otherMapEntryValue in mapEntry)
                                {
                                    candidate = candidatePrefix + otherMapEntryValue;
                                    MapRelated(word, ref candidate, wn + mapEntryValue.Length, wlst, cpdSuggest, ref timer, ref timeLimit);

                                    if (timer.HasValue && timer.GetValueOrDefault() == 0)
                                    {
                                        return wlst.Count;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!inMap)
                {
                    candidate += word[wn];
                    MapRelated(word, ref candidate, wn + 1, wlst, cpdSuggest, ref timer, ref timeLimit);
                }

                return wlst.Count;
            }

            private void TestSug(List<string> wlst, string candidate, bool cpdSuggest)
            {
                int? timer = null;
                long? timeLimit = null;
                TestSug(wlst, candidate, cpdSuggest, ref timer, ref timeLimit);
            }

            private void TestSug(List<string> wlst, StringSlice candidate, bool cpdSuggest)
            {
                int? timer = null;
                long? timeLimit = null;
                TestSug(wlst, candidate, cpdSuggest, ref timer, ref timeLimit);
            }

            private void TestSug(List<string> wlst, string candidate, bool cpdSuggest, ref int? timer, ref long? timeLimit)
            {
                if (
                    wlst.Count < MaxSuggestions
                    &&
                    !wlst.Contains(candidate)
                    &&
                    CheckWord(candidate, cpdSuggest, ref timer, ref timeLimit) != 0
                )
                {
                    wlst.Add(candidate);
                }
            }

            private void TestSug(List<string> wlst, StringSlice candidate, bool cpdSuggest, ref int? timer, ref long? timeLimit)
            {
                if (
                    wlst.Count < MaxSuggestions
                    &&
                    !wlst.Contains(candidate)
                )
                {
                    var candidateWord = candidate.ToString();
                    if (CheckWord(candidateWord, cpdSuggest, ref timer, ref timeLimit) != 0)
                    {
                        wlst.Add(candidateWord);
                    }
                }
            }

            private int CheckWord(string word, bool cpdSuggest)
            {
                int? timer = null;
                long? timeLimit = null;
                return CheckWord(word, cpdSuggest, ref timer, ref timeLimit);
            }

            /// <summary>
            /// See if a candidate suggestion is spelled correctly
            /// needs to check both root words and words with affixes.
            /// </summary>
            /// <remarks>
            /// Obsolote MySpell-HU modifications:
            /// return value 2 and 3 marks compounding with hyphen (-)
            /// `3' marks roots without suffix
            /// </remarks>
            private int CheckWord(string word, bool cpdSuggest, ref int? timer, ref long? timeLimit)
            {
                // check time limit
                if (timer.HasValue)
                {
                    timer--;
                    if (timer == 0 && timeLimit.HasValue)
                    {
                        if (Environment.TickCount - timeLimit.GetValueOrDefault() > TimeLimit)
                        {
                            return 0;
                        }

                        timer = MaxPlusTimer;
                    }
                }

                WordEntry rv;

                if (cpdSuggest)
                {
                    if (Affix.HasCompound)
                    {
                        WordEntry rv2;
                        var rwords = new Dictionary<int, WordEntry>(); // buffer for COMPOUND pattern checking
                        var info = SpellCheckResultType.None;
                        rv = CompoundCheck(word, 0, 0, 100, 0, null, ref rwords, false, 1, ref info);
                        if (
                            rv != null
                            &&
                            (
                                (rv2 = Lookup(word).FirstOrDefault()) == null
                                ||
                                !rv2.HasFlags
                                ||
                                !rv2.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest)
                            )
                        )
                        {
                            return 3; // XXX obsolote categorisation + only ICONV needs affix flag check?
                        }
                    }

                    return 0;
                }

                var rvs = Lookup(word); // get homonyms
                var rvIndex = 0;
                rv = rvs.FirstOrDefault();

                if (rv != null)
                {
                    if (rv.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest))
                    {
                        return 0;
                    }

                    while (rv != null)
                    {
                        if (rv.ContainsAnyFlags(Affix.NeedAffix, SpecialFlags.OnlyUpcaseFlag, Affix.OnlyInCompound))
                        {
                            rvIndex++;
                            rv = rvIndex < rvs.Count ? rvs[rvIndex] : null;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    rv = PrefixCheck(word, CompoundOptions.Not, default(FlagValue)); // only prefix, and prefix + suffix XXX
                }

                bool noSuffix;
                if (rv != null)
                {
                    noSuffix = true;
                }
                else
                {
                    noSuffix = false;
                    rv = SuffixCheck(word, AffixEntryOptions.None, null, default(FlagValue), default(FlagValue), CompoundOptions.Not); // only suffix
                }

                if (Affix.ContClasses.HasItems && rv == null)
                {
                    rv = SuffixCheckTwoSfx(word, AffixEntryOptions.None, null, default(FlagValue));
                    if (rv == null)
                    {
                        rv = PrefixCheckTwoSfx(word, CompoundOptions.Begin, default(FlagValue));
                    }
                }

                // check forbidden words
                if (rv != null && rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag, Affix.NoSuggest, Affix.OnlyInCompound))
                {
                    return 0;
                }

                if (rv != null)
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
                if (word.Length < 2 || Affix.Replacements.IsEmpty)
                {
                    return wlst.Count;
                }

                //for (var i = 0; i < Affix.Replacements.Length; i++)
                foreach (var replacement in Affix.Replacements)
                {
                    var r = 0;
                    // search every occurence of the pattern in the word
                    while ((r = word.IndexOf(replacement.Pattern, r, StringComparison.Ordinal)) >= 0)
                    {
                        var type = (r == 0) ? ReplacementValueType.Ini : ReplacementValueType.Med;
                        if (r - 0 + replacement.Pattern.Length == word.Length)
                        {
                            type += 2;
                        }

                        while (type != 0 && string.IsNullOrEmpty(replacement[type]))
                        {
                            type = (type == ReplacementValueType.Fin && r != 0) ? ReplacementValueType.Med : type - 1;
                        }

                        if (string.IsNullOrEmpty(replacement[type]))
                        {
                            r++;
                            continue;
                        }

                        var candidate = StringEx.ConcatSubstring(word, 0, r, replacement[type], word, r + replacement.Pattern.Length);

                        TestSug(wlst, candidate, cpdSuggest);

                        // check REP suggestions with space
                        var sp = candidate.IndexOf(' ');
                        if (sp >= 0)
                        {
                            var prev = 0;
                            while (sp >= 0)
                            {
                                if (CheckWord(candidate.Substring(prev, sp - prev), false) != 0)
                                {
                                    var oldNs = wlst.Count;
                                    TestSug(wlst, candidate.Subslice(sp + 1), cpdSuggest);
                                    if (oldNs < wlst.Count)
                                    {
                                        wlst[wlst.Count - 1] = candidate;
                                    }
                                }

                                prev = sp + 1;
                                sp = candidate.IndexOf(' ', prev);
                            }
                        }

                        r++; // search for the next letter
                    }
                }

                return wlst.Count;
            }

            /// <summary>
            /// Generate a set of suggestions for very poorly spelled words.
            /// </summary>
            private void NGramSuggest(List<string> wlst, string word)
            {
                int lval;
                int sc;

                // exhaustively search through all root words
                // keeping track of the MAX_ROOTS most similar root words
                var roots = new NGramSuggestSearchRoot[MaxRoots]; // TODO: this shoudl come from a pool
                for (var i = 0; i < roots.Length; i++)
                {
                    roots[i] = new NGramSuggestSearchRoot(i);
                }

                var lp = roots.Length - 1;
                var lpphon = lp;

                // word reversing wrapper for complex prefixes
                if (Affix.ComplexPrefixes)
                {
                    word = word.Reverse();
                }

                var hasPhoneEntries = Affix.Phone.HasItems;

                var target = hasPhoneEntries
                    ? Phonet(MakeAllCap(word))
                    : string.Empty;

                foreach (var hp in WordList.NGramAllowedEntries)
                {
                    sc = NGram(3, word, hp.Word, NGramOptions.LongerWorse | NGramOptions.Lowering)
                        + LeftCommonSubstring(word, hp.Word);

                    // check special pronounciation
                    var f = string.Empty;
                    if (hp.Options.HasFlag(WordEntryOptions.Phon) && CopyField(ref f, hp.Morphs, MorphologicalTags.Phon))
                    {
                        var sc2 = NGram(3, word, f, NGramOptions.LongerWorse | NGramOptions.Lowering)
                            + LeftCommonSubstring(word, f);

                        if (sc2 > sc)
                        {
                            sc = sc2;
                        }
                    }

                    var scphon = -20000;
                    if (hasPhoneEntries && sc > 2 && Math.Abs(word.Length - hp.Word.Length) <= 3)
                    {
                        scphon = NGram(3, target, Phonet(MakeAllCap(hp.Word)), NGramOptions.LongerWorse) * 2;
                    }

                    if (sc > roots[lp].Score)
                    {
                        roots[lp].Score = sc;
                        roots[lp].Root = hp;
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
                        roots[lpphon].RootPhon = hp.Word;
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

                // find minimum threshold for a passable suggestion
                // mangle original word three differnt ways
                // and score them to generate a minimum acceptable score
                var thresh = 0;
                var mw = StringBuilderPool.Get(word.Length);
                for (var sp = 1; sp < 4; sp++)
                {
                    mw.Clear();
                    mw.Append(word);

                    for (var k = sp; k < word.Length; k += 4)
                    {
                        mw[k] = '*';
                    }

                    thresh += NGram(word.Length, word, mw.ToString(), NGramOptions.AnyMismatch | NGramOptions.Lowering);
                }

                StringBuilderPool.Return(mw);

                thresh /= 3;
                thresh--;

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
                    if (rp != null)
                    {
                        var field = string.Empty;
                        if (!rp.Options.HasFlag(WordEntryOptions.Phon) || !CopyField(ref field, rp.Morphs, MorphologicalTags.Phon))
                        {
                            field = null;
                        }

                        int nw = ExpandRootWord(glst, rp, word, field);

                        for (var k = 0; k < nw; k++)
                        {
                            sc = NGram(word.Length, word, glst[k].Word, NGramOptions.AnyMismatch | NGramOptions.Lowering)
                                + LeftCommonSubstring(word, glst[k].Word);

                            if (sc > thresh)
                            {
                                if (sc > guesses[lp].Score)
                                {
                                    guesses[lp].Score = sc;
                                    guesses[lp].Guess = glst[k].Word;
                                    guesses[lp].GuessOrig = glst[k].Orig;
                                    lval = sc;
                                    for (var j = 0; j < guesses.Length; j++)
                                    {
                                        if (guesses[j].Score < lval)
                                        {
                                            lp = j;
                                            lval = guesses[j].Score;
                                        }
                                    }
                                }
                                else
                                {
                                    glst[k].ClearWordAndOrig();
                                }
                            }
                            else
                            {
                                glst[k].ClearWordAndOrig();
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
                var re = 0;
                var isSwap = false;

                if (Affix.MaxDifferency.HasValue && Affix.MaxDifferency.GetValueOrDefault() >= 0)
                {
                    fact = (10.0 - Affix.MaxDifferency.GetValueOrDefault()) / 5.0;
                }

                for (var i = 0; i < guesses.Length; i++)
                {
                    var guess = guesses[i];
                    if (guess.Guess != null)
                    {
                        // lowering guess[i]
                        var gl = MakeAllSmall(guess.Guess);
                        var len = guess.Guess.Length;

                        var _lcs = LcsLen(word, gl);

                        // same characters with different casing
                        if (word.Length == len && word.Length == _lcs)
                        {
                            guesses[i].Score += 2000;
                            break;
                        }

                        // using 2-gram instead of 3, and other weightening
                        re = NGram(2, word, gl, NGramOptions.AnyMismatch | NGramOptions.Weighted | NGramOptions.Lowering)
                            + NGram(2, gl, word, NGramOptions.AnyMismatch | NGramOptions.Weighted | NGramOptions.Lowering);

                        guesses[i].Score =
                            // length of longest common subsequent minus length difference
                            2 * _lcs - Math.Abs(word.Length - len)
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
                        if (root.RootPhon != null)
                        {
                            // lowering rootphon[i]
                            var gl = MakeAllSmall(root.RootPhon);
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
                    var guess = guesses[i];
                    if (guess.Guess != null)
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
                            var unique = true;
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
                                    guesses[i].ClearGuessAndOrig();
                                    continue;
                                }
                            }

                            for (var j = 0; j < wlst.Count; j++)
                            {
                                // don't suggest previous suggestions or a previous suggestion with
                                // prefixes or affixes
                                if (
                                    (guess.GuessOrig == null && guess.Guess.Contains(wlst[j]))
                                    ||
                                    (guess.GuessOrig != null && guess.GuessOrig.Contains(wlst[j]))
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

                        guesses[i].ClearGuessAndOrig();
                    }
                }

                oldns = wlst.Count;
                wlstLimit = Math.Min(MaxSuggestions, oldns + MaxPhonSugs);

                if (hasPhoneEntries)
                {
                    for (var i = 0; i < roots.Length; i++)
                    {
                        var root = roots[i];
                        if (root.RootPhon != null && wlst.Count < wlstLimit)
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
                string t;
                if (Affix.ComplexPrefixes)
                {
                    var l2 = s2.Length;
                    t = StringEx.ConcatSubstring(s2, 0, l2 - 1, Affix.Culture.TextInfo.ToLower(s2[l2 - 1]));
                }
                else
                {
                    t = MakeAllSmall(s2);
                }

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
                int m, n;
                LongestCommonSubsequenceType[] result;
                Lcs(s, s2, out m, out n, out result);

                if (result == null)
                {
                    return 0;
                }

                var i = m;
                var j = n;
                var len = 0;
                while (i != 0 && j != 0)
                {
                    if (result[i * (n + 1) + j] == LongestCommonSubsequenceType.UpLeft)
                    {
                        len++;
                        i--;
                        j--;
                    }
                    else if (result[i * (n + 1) + j] == LongestCommonSubsequenceType.Up)
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
                var c = new LongestCommonSubsequenceType[(m + 1) * nNext];
                b = new LongestCommonSubsequenceType[(m + 1) * nNext];

                // NOTE: arrays are already zero (Up)

                for (var i = 1; i <= m; i++)
                {
                    var iPrev = i - 1;
                    for (var j = 1; j <= n; j++)
                    {
                        var inj = i * nNext + j;
                        var jPrev = j - 1;
                        if (((s[iPrev] == s2[jPrev])))
                        {
                            c[inj] = c[iPrev * nNext + jPrev] + 1;
                            b[inj] = LongestCommonSubsequenceType.UpLeft;
                        }
                        else if (c[iPrev * nNext + j] >= c[inj - 1])
                        {
                            c[inj] = c[iPrev * nNext + j];
                            b[inj] = LongestCommonSubsequenceType.Up;
                        }
                        else
                        {
                            c[inj] = c[inj - 1];
                            b[inj] = LongestCommonSubsequenceType.UpLeft;
                        }
                    }
                }
            }

            private int ExpandRootWord(GuessWord[] wlst, WordEntry entry, string bad, string phon)
            {
                var nh = 0;
                // first add root word to list
                if (nh < wlst.Length && !entry.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound))
                {
                    var dupTs = MyStrDup(entry.Word);
                    wlst[nh].Word = dupTs;
                    if (wlst[nh].Word == null)
                    {
                        return 0;
                    }

                    wlst[nh].Allow = false;
                    wlst[nh].Orig = null;
                    nh++;

                    // add special phonetic version
                    if (phon != null && nh < wlst.Length)
                    {
                        wlst[nh].Word = MyStrDup(phon);
                        if (wlst[nh].Word == null)
                        {
                            return nh - 1;
                        }

                        wlst[nh].Allow = false;
                        wlst[nh].Orig = dupTs;
                        if (wlst[nh].Orig == null)
                        {
                            return nh - 1;
                        }

                        nh++;
                    }
                }

                // handle suffixes
                if (Affix.Suffixes.HasAffixes)
                {
                    for (var i = 0; i < entry.Flags.Count; i++)
                    {
                        var sptrGroup = Affix.Suffixes.GetByFlag(entry.Flags[i]);
                        if (sptrGroup == null)
                        {
                            continue;
                        }

                        foreach (var sptr in sptrGroup.Entries)
                        {
                            if (
                                (
                                    string.IsNullOrEmpty(sptr.Key)
                                    ||
                                    (
                                        bad.Length > sptr.Key.Length
                                        &&
                                        StringEx.EqualsOffset(sptr.Append, 0, bad, bad.Length - sptr.Key.Length)
                                    )
                                )
                                && // check needaffix flag
                                !(
                                    sptr.ContClass.HasItems
                                    &&
                                    sptr.ContainsAnyContClass(Affix.NeedAffix, Affix.Circumfix, Affix.OnlyInCompound)
                                )
                            )
                            {
                                var newword = Add(sptr, entry.Word);
                                if (!string.IsNullOrEmpty(newword))
                                {
                                    if (nh < wlst.Length)
                                    {
                                        wlst[nh].Word = MyStrDup(newword);
                                        wlst[nh].Allow = sptrGroup.AllowCross;
                                        wlst[nh].Orig = null;
                                        nh++;

                                        // add special phonetic version
                                        if (phon != null && nh < wlst.Length)
                                        {
                                            wlst[nh].Word = MyStrDup(phon + sptr.Key.Reverse());
                                            if (wlst[nh].Word == null)
                                            {
                                                return nh - 1;
                                            }

                                            wlst[nh].Allow = false;
                                            wlst[nh].Orig = MyStrDup(newword);
                                            if (wlst[nh].Orig == null)
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

                        for (var k = 0; k < entry.Flags.Count; k++)
                        {
                            var pfxGroup = Affix.Prefixes.GetByFlag(entry.Flags[k]);
                            if (pfxGroup == null)
                            {
                                continue;
                            }

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
                                            StringEx.EqualsOffset(cptr.Key, 0, bad, 0, cptr.Key.Length)
                                        )
                                    )
                                )
                                {
                                    var newword = Add(cptr, wlst[j].Word);
                                    if (newword.Length != 0)
                                    {
                                        if (nh < wlst.Length)
                                        {
                                            wlst[nh].Word = MyStrDup(newword);
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
                    for (var m = 0; m < entry.Flags.Count; m++)
                    {
                        var ptrGroup = Affix.Prefixes.GetByFlag(entry.Flags[m]);
                        if (ptrGroup == null)
                        {
                            continue;
                        }

                        foreach (var ptr in ptrGroup.Entries)
                        {
                            if (
                                (
                                    ptr.Key.Length == 0
                                    ||
                                    (
                                        bad.Length > ptr.Key.Length
                                        &&
                                        StringEx.EqualsOffset(ptr.Key, 0, bad, 0, ptr.Key.Length)
                                    )
                                )
                                && // check needaffix flag
                                !ptr.ContainsAnyContClass(Affix.NeedAffix, Affix.Circumfix, Affix.OnlyInCompound)
                            )
                            {
                                var newword = Add(ptr, entry.Word);
                                if (!string.IsNullOrEmpty(newword))
                                {
                                    if (nh < wlst.Length)
                                    {
                                        wlst[nh].Word = MyStrDup(newword);
                                        wlst[nh].Allow = ptrGroup.AllowCross;
                                        wlst[nh].Orig = null;
                                        nh++;
                                    }
                                }
                            }
                        }
                    }
                }

                return nh;
            }

            /// <summary>
            /// Error is should have been two words.
            /// </summary>
            private int TwoWords(List<string> wlst, string word, bool cpdSuggest)
            {
                if (word.Length < 3)
                {
                    return wlst.Count;
                }

                int c2;
                bool cwrd;

                var isHungarianAndNotForbidden = Affix.IsHungarian && !CheckForbidden(word);

                var candidate = StringBuilderPool.Get(word.Length + 2);
                candidate.Append('\0');
                candidate.Append(word);

                // split the string into two pieces after every char
                // if both pieces are good words make them a suggestion

                for (var p = 1; p + 1 < candidate.Length; p++)
                {
                    candidate[p - 1] = candidate[p];
                    candidate[p] = '\0';
                    var c1 = CheckWord(candidate.ToStringTerminated(), cpdSuggest);
                    if (c1 != 0)
                    {
                        c2 = CheckWord(candidate.ToStringTerminated(p + 1), cpdSuggest);
                        if (c2 != 0)
                        {
                            candidate[p] = ' ';

                            // spec. Hungarian code (need a better compound word support)
                            if (
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
                            )
                            {
                                candidate[p] = '-';
                            }

                            cwrd = true;

                            var currentCandidateString = candidate.ToStringTerminated();
                            for (var k = 0; k < wlst.Count; k++)
                            {
                                if (string.Equals(wlst[k], currentCandidateString, StringComparison.Ordinal))
                                {
                                    cwrd = false;
                                    break;
                                }
                            }

                            if (wlst.Count < MaxSuggestions)
                            {
                                if (cwrd)
                                {
                                    wlst.Add(currentCandidateString);
                                }
                            }
                            else
                            {
                                return wlst.Count;
                            }

                            // add two word suggestion with dash, if TRY string contains
                            // "a" or "-"
                            // NOTE: cwrd doesn't modified for REP twoword sugg.

                            if (
                                !string.IsNullOrEmpty(Affix.TryString)
                                &&
                                (
                                    Affix.TryString.Contains('a')
                                    ||
                                    Affix.TryString.Contains('-')
                                )
                                &&
                                candidate.Length - (p + 1) > 1
                                &&
                                p > 1
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

                                if (wlst.Count < MaxSuggestions)
                                {
                                    if (cwrd)
                                    {
                                        wlst.Add(currentCandidateString);
                                    }
                                }
                                else
                                {
                                    return wlst.Count;
                                }
                            }
                        }
                    }
                }

                StringBuilderPool.Return(candidate);

                return wlst.Count;
            }

            private bool CheckForbidden(string word)
            {
                var rv = Lookup(word).FirstOrDefault();
                if (rv != null && rv.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound))
                {
                    rv = null;
                }

                if (PrefixCheck(word, CompoundOptions.Begin, default(FlagValue)) == null)
                {
                    rv = SuffixCheck(word, AffixEntryOptions.None, null, default(FlagValue), default(FlagValue), CompoundOptions.Not); // prefix+suffix, suffix
                }

                // check forbidden words
                return rv != null && rv.ContainsFlag(Affix.ForbiddenWord);
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
                        word.Length >= entry.Conditions.Count
                        &&
                        TestCondition(entry, word)
                        &&
                        (
                            entry.Strip.Length == 0
                            ||
                            StringEx.EqualsOffset(word, 0, entry.Strip, 0, entry.Strip.Length)
                        )
                    )
                    // we have a match so add prefix
                    ? StringEx.ConcatSubstring(entry.Append, word, entry.Strip.Length, word.Length - entry.Strip.Length)
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
                        word.Length >= entry.Conditions.Count
                        &&
                        TestCondition(entry, word)
                        &&
                        (
                            entry.Strip.Length == 0
                            ||
                            StringEx.EqualsOffset(word, word.Length - entry.Strip.Length, entry.Strip, 0)
                        )
                    )
                    // we have a match so add suffix
                    ? StringEx.ConcatSubstring(word, 0, word.Length - entry.Strip.Length, entry.Append)
                    : string.Empty;
            }

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private bool CopyField(ref string dest, MorphSet morphs, string var) => CopyField(ref dest, morphs.Join(" "), var);

            private bool CopyField(ref string dest, string morph, string var)
            {
                if (string.IsNullOrEmpty(morph))
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

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private string MyStrDup(string s) => s;

            /// <summary>
            /// Length of the left common substring of s1 and (decapitalised) s2.
            /// </summary>
            private int LeftCommonSubstring(string s1, string s2)
            {
                if (Affix.ComplexPrefixes)
                {
                    if (s1.Length <= s2.Length && s1.Length - 1 >= 0 && s2.Length > s2.Length - 1 && s2[s1.Length - 1] == s2[s2.Length - 1])
                    {
                        return 1;
                    }
                }
                else
                {
                    // decapitalise dictionary word
                    if (0 == s1.Length || 0 == s2.Length || s1[0] == s2[0] || s1[0] == Affix.Culture.TextInfo.ToLower(s2[0]))
                    {
                        var index = 1;
                        while (index < s1.Length && index < s2.Length && s1[index] == s2[index])
                        {
                            index++;
                        };

                        return index;
                    }
                }

                return 0;
            }

            /// <summary>
            /// Generate an n-gram score comparing s1 and s2.
            /// </summary>
            private int NGram(int n, string s1, string s2, NGramOptions opt)
            {
                if (opt.HasFlag(NGramOptions.Lowering))
                {
                    s2 = MakeAllSmall(s2);
                }

                var nscore = opt.HasFlag(NGramOptions.Weighted)
                    ? NGramWeightedSearch(n, s1, s2)
                    : NGramNonWeightedSearch(n, s1, s2);

                int ns;
                if (opt.HasFlag(NGramOptions.AnyMismatch))
                {
                    ns = Math.Abs(s2.Length - s1.Length) - 2;
                }
                else if (opt.HasFlag(NGramOptions.LongerWorse))
                {
                    ns = (s2.Length - s1.Length) - 2;
                }
                else
                {
                    return nscore;
                }

                if (ns > 0)
                {
                    nscore -= ns;
                }

                return nscore;
            }

            private static int NGramWeightedSearch(int n, string s1, string t)
            {
                int ns;
                int nscore = 0;
                for (var j = 1; j <= n; j++)
                {
                    ns = 0;
                    var s1LenLessJ = s1.Length - j;
                    if (0 <= s1LenLessJ)
                    {
                        if (t.ContainsSubstringOrdinal(s1, 0, j))
                        {
                            ns++;
                        }
                        else
                        {
                            ns -= 2;
                        }

                        for (var i = 1; i < s1LenLessJ; i++)
                        {
                            if (t.ContainsSubstringOrdinal(s1, i, j))
                            {
                                ns++;
                            }
                            else
                            {
                                ns--;
                            }
                        }

                        if (0 != s1LenLessJ)
                        {
                            if (t.ContainsSubstringOrdinal(s1, s1LenLessJ, j))
                            {
                                ns++;
                            }
                            else
                            {
                                ns -= 2;
                            }
                        }
                    }

                    nscore += ns;
                }

                return nscore;
            }

            private static int NGramNonWeightedSearch(int n, string s1, string t)
            {
                int ns;
                int nscore = 0;
                for (var j = 1; j <= n; j++)
                {
                    ns = 0;
                    var s1LenLessJ = s1.Length - j;
                    for (var i = 0; i <= s1LenLessJ; i++)
                    {
                        if (t.ContainsSubstringOrdinal(s1, i, j))
                        {
                            ns++;
                        }
                    }

                    nscore += ns;

                    if (ns < 2)
                    {
                        break;
                    }
                }

                return nscore;
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

                var word = StringBuilderPool.Get(inword, 0, Math.Min(inword.Length, MaxPhoneTUtf8Len));
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
                                StringEx.MyIsAlpha(word.GetCharOrTerminator(i + k)) // NOTE: could be implied?
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
                                    !StringEx.MyIsAlpha(word.GetCharOrTerminator(i - 1))
                                )
                                &&
                                (
                                    sString.GetCharOrTerminator(sIndex + 1) != '$'
                                    ||
                                    !StringEx.MyIsAlpha(word.GetCharOrTerminator(i + k0))
                                )
                            )
                            ||
                            (
                                sChar == '$'
                                &&
                                i > 0
                                &&
                                StringEx.MyIsAlpha(word.GetCharOrTerminator(i - 1))
                                &&
                                !StringEx.MyIsAlpha(word.GetCharOrTerminator(i + k0))
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
                                        if (StringEx.MyIsAlpha(word.GetCharOrTerminator(i + k0)) && sString.IndexOf(word.GetCharOrTerminator(i + k0), sIndex + 1) >= 0)
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
                                            !StringEx.MyIsAlpha(word.GetCharOrTerminator(i + k0))
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
                            p0 = !string.IsNullOrEmpty(phoneEntry.Rule) && phoneEntry.Rule.IndexOf('<', 1) >= 0
                                ? 1
                                : 0;

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

                                if (!string.IsNullOrEmpty(phoneEntry.Rule) && phoneEntry.Rule.IndexOf("^^", StringComparison.Ordinal) >= 0)
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
                        if (k != 0 && p0 == 0 && target.Length < len && c != '\0' && (true || target.Length == 0 || !target.EndsWith(c)))
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

            private static void StrMove(StringBuilder dest, int destOffset, StringBuilder src, int srcOffset)
            {
                var destIndex = destOffset;
                for (var srcIndex = srcOffset; srcIndex < src.Length && destIndex < dest.Length; srcIndex++, destIndex++)
                {
                    dest[destIndex] = src[srcIndex];
                }

                if (destIndex < dest.Length)
                {
                    dest[destIndex] = '\0';
                }
            }

#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private static void InsertSuggestion(List<string> slst, string word) => slst.Insert(0, word);

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

                public string GuessOrig;

                public int Score;

#if !PRE_NETSTANDARD && !DEBUG
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
                public void ClearGuessAndOrig()
                {
                    Guess = null;
                    GuessOrig = null;
                }
            }

            private struct GuessWord
            {
                public string Word;

                public string Orig;

                public bool Allow;

                public void ClearWordAndOrig()
                {
                    Word = null;
                    Orig = null;
                }
            }

            [Flags]
            public enum NGramOptions : byte
            {
                LongerWorse = 1 << 0,
                AnyMismatch = 1 << 1,
                Lowering = 1 << 2,
                Weighted = 1 << 3
            }
        }
    }
}
