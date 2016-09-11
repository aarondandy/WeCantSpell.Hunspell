using Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Hunspell
{
    internal class HunspellQueryState
    {
        internal HunspellQueryState(string word, AffixConfig affixConfig, Dictionary dictionary)
        {
            WordToCheck = word;
            Affix = affixConfig;
            Dictionary = dictionary;
        }

        private const string DefaultXmlToken = "<?xml?>";

        private const int MaxSharps = 5;

        internal const int MaxWordLen = 176;

        private const int MaxWordUtf8Len = MaxWordLen * 3;

        private const int MaxSuggestions = 15;

        private const int MaxRoots = 100;

        private const int MaxWords = 100;

        private const int MaxGuess = 200;

        private const int MaxPhonSugs = 2;

        private const int MaxPhoneTLen = 256;

        private const int MaxPhoneTUtf8Len = MaxPhoneTLen * 4;


        /// <summary>
        /// Timelimit: max ~1/4 sec (process time on Linux) for a time consuming function.
        /// </summary>
        private const int TimeLimit = 1000 >> 2;

        private const int MinTimer = 100;

        private const int MaxPlusTimer = 100;

        public string WordToCheck { get; }

        public AffixConfig Affix { get; }

        public Dictionary Dictionary { get; }

        public AffixEntryGroup<PrefixEntry> PrefixGroup { get; set; }

        public PrefixEntry Prefix { get; set; }

        /// <summary>
        /// Previous prefix for counting syllables of the prefix.
        /// </summary>
        public string PrefixAppend { get; set; }

        public AffixEntryGroup<SuffixEntry> SuffixGroup { get; set; }

        public SuffixEntry Suffix { get; set; }

        public FlagValue SuffixFlag { get; set; }

        /// <summary>
        /// Modifier for syllable count of <see cref="SuffixAppend"/>.
        /// </summary>
        public int SuffixExtra { get; set; }

        /// <summary>
        /// Previous suffix for counting syllables of the suffix.
        /// </summary>
        public string SuffixAppend { get; set; }

        private void ClearPrefix()
        {
            PrefixGroup = null;
            Prefix = null;
        }

        private void ClearSuffix()
        {
            SuffixGroup = null;
            Suffix = null;
        }

        private void SetPrefix(AffixEntryGroup<PrefixEntry> group, PrefixEntry entry)
        {
            PrefixGroup = group;
            Prefix = entry;
        }

        private void SetSuffix(AffixEntryGroup<SuffixEntry> group, SuffixEntry entry)
        {
            SuffixGroup = group;
            Suffix = entry;
        }

        public bool Check()
        {
            return CheckDetails().Correct;
        }

        private bool Check(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Check();
        }

        private SpellCheckResult CheckDetails(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).CheckDetails();
        }

        public SpellCheckResult CheckDetails()
        {
            var word = WordToCheck;

            if (string.IsNullOrEmpty(word) || !Dictionary.HasEntries)
            {
                return new SpellCheckResult(false);
            }

            // Hunspell supports XML input of the simplified API (see manual)
            if (word == DefaultXmlToken)
            {
                return new SpellCheckResult(true);
            }

            if (word.Length >= MaxWordUtf8Len)
            {
                return new SpellCheckResult(false);
            }

            CapitalizationType capType;
            int abbv;
            string scw;

            // input conversion
            string convertedWord;
            if (!Affix.HasInputConversions || !Affix.InputConversions.TryConvert(word, out convertedWord))
            {
                convertedWord = word;
            }

            var wl = CleanWord2(out scw, convertedWord, out capType, out abbv);
            if (wl == 0)
            {
                return new SpellCheckResult(false);
            }

            // allow numbers with dots, dashes and commas (but forbid double separators: "..", "--" etc.)
            if (IsNumericWord(word))
            {
                return new SpellCheckResult(true);
            }

            var resultType = SpellCheckResultType.None;
            string root = null;

            DictionaryEntry rv;

            if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit || capType == CapitalizationType.None)
            {
                if (capType == CapitalizationType.HuhInit)
                {
                    resultType |= SpellCheckResultType.OrigCap;
                }

                rv = CheckWord(scw, ref resultType, out root);
                if (abbv != 0 && rv == null)
                {
                    var u8buffer = scw + ".";
                    rv = CheckWord(u8buffer, ref resultType, out root);
                }
            }
            else if (capType == CapitalizationType.All)
            {
                rv = CheckDetailsAllCap(abbv, ref scw, ref resultType, out root);
            }
            else
            {
                rv = null;
            }

            if (capType == CapitalizationType.Init || (capType == CapitalizationType.All && rv == null))
            {
                rv = CheckDetailsInitCap(abbv, capType, ref scw, ref resultType, out root);
            }

            if (rv != null)
            {
                var isFound = true;
                if (rv.ContainsFlag(Affix.Warn))
                {
                    resultType |= SpellCheckResultType.Warn;

                    if (Affix.ForbidWarn)
                    {
                        isFound = false;
                    }
                }

                return new SpellCheckResult(root, resultType, isFound);
            }

            // recursive breaking at break points
            if (Affix.HasBreakEntries)
            {
                int nbr = 0;
                wl = scw.Length;

                // calculate break points for recursion limit
                foreach (var breakEntry in Affix.BreakTable)
                {
                    int pos = 0;
                    while ((pos = scw.IndexOf(breakEntry, pos, StringComparison.Ordinal)) >= 0)
                    {
                        ++nbr;
                        pos += breakEntry.Length;
                    }
                }

                if (nbr >= 10)
                {
                    return new SpellCheckResult(root, resultType, false);
                }

                // check boundary patterns (^begin and end$)
                foreach (var breakEntry in Affix.BreakTable)
                {
                    var plen = breakEntry.Length;
                    if (plen == 1 || plen > wl)
                    {
                        continue;
                    }

                    if (
                        breakEntry.StartsWith('^')
                        && StringEx.EqualsOffset(scw, 0, breakEntry, 1, plen - 1)
                        && Check(scw.Substring(plen - 1))
                    )
                    {
                        return new SpellCheckResult(root, resultType, true);
                    }

                    if (
                        breakEntry.EndsWith('$')
                        && StringEx.EqualsOffset(scw, wl - plen + 1, breakEntry, 0, plen - 1)
                    )
                    {
                        if (Check(scw.Substring(0, wl - plen + 1)))
                        {
                            return new SpellCheckResult(root, resultType, true);
                        }
                    }
                }

                // other patterns
                foreach (var breakEntry in Affix.BreakTable)
                {
                    var plen = breakEntry.Length;
                    var found = scw.IndexOf(breakEntry, StringComparison.Ordinal);

                    if (found > 0 && found < wl - plen)
                    {
                        if (!Check(scw.Substring(found + plen)))
                        {
                            continue;
                        }

                        // examine 2 sides of the break point
                        if (Check(scw.Substring(0, found)))
                        {
                            return new SpellCheckResult(root, resultType, true);
                        }

                        // LANG_hu: spec. dash rule
                        if (breakEntry == "-" && Affix.Culture.IsHungarianLanguage())
                        {
                            if (Check(scw.Substring(0, found + 1)))
                            {
                                return new SpellCheckResult(root, resultType, true);
                            }
                        }
                    }
                }
            }

            return new SpellCheckResult(root, resultType, false);
        }

        private DictionaryEntry CheckDetailsAllCap(int abbv, ref string scw, ref SpellCheckResultType resultType, out string root)
        {
            resultType |= SpellCheckResultType.OrigCap;
            var rv = CheckWord(scw, ref resultType, out root);
            if (rv != null)
            {
                return rv;
            }

            if (abbv != 0)
            {
                var u8buffer = scw + ".";
                rv = CheckWord(u8buffer, ref resultType, out root);
                if (rv != null)
                {
                    return rv;
                }
            }

            // Spec. prefix handling for Catalan, French, Italian:
            // prefixes separated by apostrophe (SANT'ELIA -> Sant'+Elia).

            var apos = scw.IndexOf('\'');
            if (apos >= 0)
            {
                scw = MakeAllSmall(scw);

                // conversion may result in string with different len than before MakeAllSmall2 so re-scan
                if (apos < scw.Length - 1)
                {
                    scw = StringEx.ConcatSubstring(scw, 0, apos + 1, MakeInitCap(scw.Substring(apos + 1)));
                    rv = CheckWord(scw, ref resultType, out root);
                    if (rv != null)
                    {
                        return rv;
                    }

                    scw = MakeInitCap(scw);
                    rv = CheckWord(scw, ref resultType, out root);
                    if (rv != null)
                    {
                        return rv;
                    }
                }
            }

            if (Affix.CheckSharps && scw.Contains("SS"))
            {
                scw = MakeAllSmall(scw);
                var u8buffer = scw;
                rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, out root);
                if (rv == null)
                {
                    scw = MakeInitCap(scw);
                    rv = SpellSharps(ref scw, 0, 0, 0, ref resultType, out root);
                }

                if (abbv != 0 && rv == null)
                {
                    u8buffer += ".";
                    rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, out root);
                    if (rv == null)
                    {
                        u8buffer = scw + ".";
                        rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, out root);
                    }
                }
            }

            return rv;
        }

        private DictionaryEntry CheckDetailsInitCap(int abbv, CapitalizationType capType, ref string scw, ref SpellCheckResultType resultType, out string root)
        {
            resultType |= SpellCheckResultType.OrigCap;
            var u8buffer = MakeAllSmall(scw);
            scw = MakeInitCap(u8buffer);

            if (capType == CapitalizationType.Init)
            {
                resultType |= SpellCheckResultType.InitCap;
            }

            var rv = CheckWord(scw, ref resultType, out root);

            if (capType == CapitalizationType.Init)
            {
                resultType &= ~SpellCheckResultType.InitCap;
            }

            // forbid bad capitalization
            // (for example, ijs -> Ijs instead of IJs in Dutch)
            // use explicit forms in dic: Ijs/F (F = FORBIDDENWORD flag)

            if (resultType.HasFlag(SpellCheckResultType.Forbidden))
            {
                rv = null;
                return rv;
            }

            if (rv != null && IsKeepCase(rv) && capType == CapitalizationType.All)
            {
                rv = null;
            }

            if (rv != null)
            {
                return rv;
            }

            rv = CheckWord(u8buffer, ref resultType, out root);

            if (abbv != 0 && rv == null)
            {
                u8buffer += ".";
                rv = CheckWord(u8buffer, ref resultType, out root);
                if (rv == null)
                {
                    u8buffer = scw;
                    u8buffer += ".";
                    if (capType == CapitalizationType.Init)
                    {
                        resultType |= SpellCheckResultType.InitCap;
                    }

                    rv = CheckWord(u8buffer, ref resultType, out root);

                    if (capType == CapitalizationType.Init)
                    {
                        resultType &= ~SpellCheckResultType.InitCap;
                    }

                    if (rv != null && IsKeepCase(rv) && capType == CapitalizationType.All)
                    {
                        rv = null;
                    }

                    return rv;
                }
            }

            if (
                rv != null
                && IsKeepCase(rv)
                &&
                (
                    capType == CapitalizationType.All
                    ||
                    // if CHECKSHARPS: KEEPCASE words with \xDF  are allowed
                    // in INITCAP form, too.
                    !(Affix.CheckSharps && u8buffer.Contains('ß'))
                )
            )
            {
                rv = null;
            }

            return rv;
        }

        private List<string> Suggest(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Suggest();
        }

        public List<string> Suggest()
        {
            var word = WordToCheck;
            var slst = new List<string>();

            int onlyCompoundSuggest = 0;
            if (!Dictionary.HasEntries)
            {
                return slst;
            }

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
            string root;

            // input conversion
            string convertedWord;
            if (!Affix.HasInputConversions || !Affix.InputConversions.TryConvert(word, out convertedWord))
            {
                convertedWord = word;
            }

            var wl = CleanWord2(out scw, convertedWord, out capType, out abbv);
            if (wl == 0)
            {
                return slst;
            }

            int capWords = 0;

            if (capType == CapitalizationType.None && Affix.ForceUpperCase.HasValue)
            {
                var info = SpellCheckResultType.OrigCap;
                if (CheckWord(scw, ref info, out root) != null)
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
                capWords = 1;
                Suggest(slst, scw, ref onlyCompoundSuggest);
                Suggest(slst, MakeAllSmall(scw), ref onlyCompoundSuggest);
            }
            else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
            {
                if (capType == CapitalizationType.HuhInit)
                {
                    capWords = 1;
                }

                Suggest(slst, scw, ref onlyCompoundSuggest);

                // something.The -> something. The
                var dotPos = scw.IndexOf('.');
                if (dotPos >= 0)
                {
                    var capTypeLocal = CapitalizationTypeEx.GetCapitalizationType(scw.Substring(dotPos + 1), Affix);
                    if (capTypeLocal == CapitalizationType.Init)
                    {
                        var str = scw;
                        str = str.Insert(dotPos + 1, " ");
                        InsertSuggestion(slst, str);
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
                        if ((slen < wl) && !StringEx.EqualsOffset(scw, wl - slen, slst[j], 1 + spaceIndex))
                        {
                            var removed = slst[j];
                            // set as first suggestion
                            slst.RemoveAt(j);
                            slst.Insert(0, StringEx.ConcatSubstring(removed, 0, spaceIndex + 1, MakeInitCap(removed.Substring(spaceIndex + 1))));
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
            if (Affix.Culture.IsHungarianLanguage())
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
            if ((slst.Count == 0 || onlyCompoundSuggest != 0) && Affix.MaxNgramSuggestions != 0)
            {
                if (capType == CapitalizationType.None)
                {
                    NGramSuggest(slst, scw);
                }
                else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
                {
                    if (capType == CapitalizationType.HuhInit)
                    {
                        capWords = 1;
                    }

                    NGramSuggest(slst, MakeAllSmall(scw));
                }
                else if (capType == CapitalizationType.Init)
                {
                    capWords = 1;
                    NGramSuggest(slst, MakeAllSmall(scw));
                }
                else if (capType == CapitalizationType.All)
                {
                    var wspace = MakeAllSmall(scw);
                    var oldns = slst.Count;
                    NGramSuggest(slst, wspace);
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
                for (var j = 0; j < slst.Count && noDashSug; j++)
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
                                ? scw.Substring(0, prevPos) + j
                                : scw.Substring(0, prevPos) + j + "-" + scw.Substring(dashPos + 1);

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
            if (capWords != 0)
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
                    slst[j] += abbrAppend;
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
            slst = slst
                .Distinct(Affix.StringComparer)
                .ToList();

            // output conversion
            if (Affix.HasOutputConversions)
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

        private void Suggest(List<string> slst, string w, ref int onlyCompoundSug)
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
                onlyCompoundSug = 1;
            }
        }

        /// <summary>
        /// Error is should have been two words.
        /// </summary>
        private int TwoWords(List<string> wlst, string word, bool cpdSuggest)
        {
            int c2;
            int cwrd;

            var wl = word.Length;
            if (wl < 3)
            {
                return wlst.Count;
            }

            var isHungarianAndNotForbidden = Affix.Culture.IsHungarianLanguage() && !CheckForbidden(word);

            var candidate = StringBuilderPool.Get(wl + 2);
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

                        cwrd = 1;

                        var currentCandidateString = candidate.ToStringTerminated();
                        for (var k = 0; k < wlst.Count; k++)
                        {
                            if (string.Equals(wlst[k], currentCandidateString, StringComparison.Ordinal))
                            {
                                cwrd = 0;
                                break;
                            }
                        }

                        if (wlst.Count < MaxSuggestions)
                        {
                            if (cwrd != 0)
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
                                    cwrd = 0;
                                    break;
                                }
                            }

                            if (wlst.Count < MaxSuggestions)
                            {
                                if (cwrd != 0)
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
                rv = SuffixCheck(word, AffixEntryOptions.None, null, null, default(FlagValue), default(FlagValue), CompoundOptions.Not); // prefix+suffix, suffix
            }

            // check forbidden words
            return rv != null && rv.ContainsFlag(Affix.ForbiddenWord);
        }

        /// <summary>
        /// perhaps we doubled two characters (pattern aba -> ababa, for example vacation -> vacacation)
        /// </summary>
        private int DoubleTwoChars(List<string> wlst, string word, bool cpdSuggest)
        {
            var state = 0;
            if (word.Length < 5)
            {
                return wlst.Count;
            }

            for (var i = 2; i < word.Length; i++)
            {
                if (word[i] == word[i - 2])
                {
                    state++;
                    if (state == 3)
                    {
                        TestSug(wlst, word.Substring(0, i - 1) + word.Substring(i + 1), cpdSuggest);
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
                for (var q = 0; q < candidate.Length; q++)
                {
                    if (p != q)
                    {
                        candidate.Swap(p, q);
                        TestSug(wlst, candidate.ToString(), cpdSuggest);
                        candidate.Swap(p, q);
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
            for (var i = 0; i < candidate.Length - 1; i++)
            {
                candidate.Swap(i, i + 1);
                TestSug(wlst, candidate.ToString(), cpdSuggest);
                candidate.Swap(i, i + 1);
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

        private void CapChars(List<string> wlst, string word, bool cpdSuggest)
        {
            TestSug(wlst, MakeAllCap(word), cpdSuggest);
        }

        private int MapChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2 || !Affix.HasMapTableEntries)
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
            if (Affix.HasMapTableEntries)
            {
                for (var j = 0; j < Affix.MapTable.Length; j++)
                {
                    var mapEntry = Affix.MapTable[j];
                    for (var k = 0; k < mapEntry.Length; k++)
                    {
                        var len = mapEntry[k].Length;
                        if (StringEx.EqualsOffset(mapEntry[k], 0, word, wn, len))
                        {
                            inMap = true;
                            var cn = candidate.Length;
                            for (var l = 0; l < mapEntry.Length; l++)
                            {
                                candidate = candidate.Substring(0, cn) + mapEntry[l];
                                MapRelated(word, ref candidate, wn + len, wlst, cpdSuggest, ref timer, ref timeLimit);

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

            DictionaryEntry rv;

            if (cpdSuggest)
            {
                if (Affix.HasCompound)
                {
                    DictionaryEntry rv2 = null;
                    var rwords = new Dictionary<int, DictionaryEntry>(); // buffer for COMPOUND pattern checking
                    var info = SpellCheckResultType.None;
                    rv = CompoundCheck(word, 0, 0, 100, 0, null, ref rwords, 0, 1, ref info);
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
                        rv = rvIndex < rvs.Length ? rvs[rvIndex] : null;
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
                rv = SuffixCheck(word, AffixEntryOptions.None, null, null, default(FlagValue), default(FlagValue), CompoundOptions.Not); // only suffix
            }

            if (rv == null && Affix.HasContClass)
            {
                rv = SuffixCheckTwoSfx(word, AffixEntryOptions.None, null, null, default(FlagValue));
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
            if (word.Length < 2 || !Affix.HasReplacements)
            {
                return wlst.Count;
            }

            for (var i = 0; i < Affix.Replacements.Length; i++)
            {
                var r = 0;
                // search every occurence of the pattern in the word
                while ((r = word.IndexOf(Affix.Replacements[i].Pattern, r, StringComparison.Ordinal)) >= 0)
                {
                    var type = (r == 0) ? ReplacementValueType.Ini : ReplacementValueType.Med;
                    if (r - 0 + Affix.Replacements[i].Pattern.Length == word.Length)
                    {
                        type += 2;
                    }

                    while (type != 0 && string.IsNullOrEmpty(Affix.Replacements[i][type]))
                    {
                        type = (type == ReplacementValueType.Fin && r != 0) ? ReplacementValueType.Med : type - 1;
                    }

                    var @out = Affix.Replacements[i][type];
                    if (string.IsNullOrEmpty(@out))
                    {
                        r++;
                        continue;
                    }

                    var candidate = word.Substring(0, r)
                        + Affix.Replacements[i][type]
                        + word.Substring(r + Affix.Replacements[i].Pattern.Length);

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
                                TestSug(wlst, candidate.Substring(sp + 1), cpdSuggest);
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

        private struct NGramSuggestSearchRoot
        {
            public NGramSuggestSearchRoot(int i)
            {
                Root = null;
                Score = -100 * i;
                RootPhon = null;
                ScorePhone = Score;
            }

            public DictionaryEntry Root;

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
        }

        private struct GuessWord
        {
            public string Word;

            public string Orig;

            public bool Allow;
        }

        /// <summary>
        /// Generate a set of suggestions for very poorly spelled words.
        /// </summary>
        private void NGramSuggest(List<string> wlst, string w)
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
            const NGramOptions low = NGramOptions.Lowering;

            // word reversing wrapper for complex prefixes
            string w2;
            string word;
            if (Affix.ComplexPrefixes)
            {
                w2 = w.Reverse();
                word = w2;
            }
            else
            {
                w2 = string.Empty;
                word = w;
            }

            var n = word.Length;

            string candidate;
            string target;
            if (Affix.HasPhoneEntires)
            {
                candidate = MakeAllCap(word);
                target = Phonet(candidate);
            }
            else
            {
                candidate = string.Empty;
                target = string.Empty;
            }

            foreach (var hp in Dictionary.NGramAllowedEntries)
            {
                sc = NGram(3, word, hp.Word, NGramOptions.LongerWorse | low)
                    + LeftCommonSubstring(word, hp.Word);

                // check special pronounciation
                var f = string.Empty;
                if (hp.Options.HasFlag(DictionaryEntryOptions.Phon) && CopyField(ref f, hp.Morphs, MorphologicalTags.Phon))
                {
                    var sc2 = NGram(3, word, f, NGramOptions.LongerWorse | low)
                        + LeftCommonSubstring(word, f);

                    if (sc2 > sc)
                    {
                        sc = sc2;
                    }
                }

                var scphon = -20000;
                if (Affix.HasPhoneEntires && sc > 2 && Math.Abs(n - hp.Word.Length) <= 3)
                {
                    candidate = MakeAllCap(hp.Word);
                    var target2 = Phonet(candidate);
                    scphon = 2 * NGram(3, target, target2, NGramOptions.LongerWorse);
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
            for (var sp = 1; sp < 4; sp++)
            {
                var mw = StringBuilderPool.Get(word);
                for (var k = sp; k < n; k += 4)
                {
                    mw[k] = '*';
                }

                thresh += NGram(n, word, StringBuilderPool.GetStringAndReturn(mw), NGramOptions.AnyMismatch | low);
            }

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
                    if (!rp.Options.HasFlag(DictionaryEntryOptions.Phon) || !CopyField(ref field, rp.Morphs, MorphologicalTags.Phon))
                    {
                        field = null;
                    }

                    int nw = ExpandRootWord(glst, rp, word, field);

                    for (var k = 0; k < nw; k++)
                    {
                        sc = NGram(n, word, glst[k].Word, NGramOptions.AnyMismatch | low)
                            + LeftCommonSubstring(word, glst[k].Word);

                        if (sc > thresh)
                        {
                            if (sc > guesses[lp].Score)
                            {
                                if (guesses[lp].Guess != null)
                                {
                                    guesses[lp].Guess = null;
                                    guesses[lp].GuessOrig = null;
                                }

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
                                glst[k].Word = null;
                                glst[k].Orig = null;
                            }
                        }
                        else
                        {
                            glst[k].Word = null;
                            glst[k].Orig = null;
                        }
                    }
                }
            }

            glst = null;

            // now we are done generating guesses
            // sort in order of decreasing score

            guesses = guesses.OrderByDescending(g => g.Score).ToArray(); // NOTE: OrderBy is used because a stable sort may be required
            if (Affix.HasPhoneEntires)
            {
                roots = roots.OrderByDescending(r => r.ScorePhone).ToArray(); // NOTE: OrderBy is used because a stable sort may be required
            }

            // weight suggestions with a similarity index, based on
            // the longest common subsequent algorithm and resort

            var fact = 1.0;
            var re = 0;
            var isSwap = false;

            {
                var maxd = Affix.MaxDifferency;
                if (maxd.HasValue && maxd.GetValueOrDefault() >= 0)
                {
                    fact = (10.0 - maxd.GetValueOrDefault()) / 5.0;
                }
            }

            for (var i = 0; i < guesses.Length; i++)
            {
                if (guesses[i].Guess != null)
                {
                    // lowering guess[i]
                    var gl = MakeAllSmall(guesses[i].Guess);
                    var len = guesses[i].Guess.Length;

                    var _lcs = LcsLen(word, gl);

                    // same characters with different casing
                    if (n == len && n == _lcs)
                    {
                        guesses[i].Score += 2000;
                        break;
                    }
                    // using 2-gram instead of 3, and other weightening

                    re = NGram(2, word, gl, NGramOptions.AnyMismatch | NGramOptions.Weighted | low)
                        + NGram(2, gl, word, NGramOptions.AnyMismatch | NGramOptions.Weighted | low);

                    guesses[i].Score =
                        // length of longest common subsequent minus length difference
                        2 * _lcs - Math.Abs(n - len)
                        // weight length of the left common substring
                        + LeftCommonSubstring(word, gl)
                        // weight equal character positions
                        + ((CommonCharacterPositions(word, gl, ref isSwap) != 0) ? 1 : 0)
                        // swap character (not neighboring)
                        + (isSwap ? 10 : 0)
                        // ngram
                        + NGram(4, word, gl, NGramOptions.AnyMismatch | low)
                        // weighted ngrams
                        + re
                        // different limit for dictionaries with PHONE rules
                        + (Affix.HasPhoneEntires ? (re < len * fact ? -1000 : 0) : (re < (n + len) * fact ? -1000 : 0));
                }
            }

            guesses = guesses.OrderByDescending(g => g.Score).ToArray(); // NOTE: OrderBy is used because a stable sort may be required

            // phonetic version
            if (Affix.HasPhoneEntires)
            {
                for (var i = 0; i < roots.Length; i++)
                {
                    if (roots[i].RootPhon != null)
                    {
                        // lowering rootphon[i]
                        var gl = MakeAllSmall(roots[i].RootPhon);
                        var len = roots[i].RootPhon.Length;

                        // heuristic weigthing of ngram scores
                        roots[i].ScorePhone += 2 * LcsLen(word, gl) - Math.Abs(n - len)
                            // weight length of the left common substring
                            + LeftCommonSubstring(word, gl);
                    }
                }

                roots = roots.OrderByDescending(r => r.ScorePhone).ToArray(); // NOTE: OrderBy is used because a stable sort may be required
            }

            // copy over
            var oldns = wlst.Count;

            var same = false;
            for (var i = 0; i < guesses.Length; i++)
            {
                if (guesses[i].Guess != null)
                {
                    if (
                        wlst.Count < oldns + Affix.MaxNgramSuggestions
                        &&
                        wlst.Count < MaxSuggestions
                        &&
                        (
                            !same
                            ||
                            guesses[i].Score > 1000
                        )
                    )
                    {
                        var unique = true;
                        // leave only excellent suggestions, if exists
                        if (guesses[i].Score > 1000)
                        {
                            same = true;
                        }
                        else if (guesses[i].Score < -100)
                        {
                            same = true;
                            // keep the best ngram suggestions, unless in ONLYMAXDIFF mode
                            if (
                                wlst.Count > oldns
                                ||
                                Affix.OnlyMaxDiff
                            )
                            {
                                guesses[i].Guess = null;
                                guesses[i].GuessOrig = null;
                                continue;
                            }
                        }

                        for (var j = 0; j < wlst.Count; j++)
                        {
                            // don't suggest previous suggestions or a previous suggestion with
                            // prefixes or affixes
                            if (
                                (guesses[i].GuessOrig == null && guesses[i].Guess.Contains(wlst[j]))
                                ||
                                (guesses[i].GuessOrig != null && guesses[i].GuessOrig.Contains(wlst[j]))
                                || // check forbidden words
                                CheckWord(guesses[i].Guess, false) == 0
                            )
                            {
                                unique = false;
                                break;
                            }
                        }

                        if (unique)
                        {
                            if (guesses[i].GuessOrig != null)
                            {
                                wlst.Add(guesses[i].GuessOrig);
                            }
                            else
                            {
                                wlst.Add(guesses[i].Guess);
                            }
                        }

                        guesses[i].Guess = null;
                        guesses[i].GuessOrig = null;
                    }
                    else
                    {
                        guesses[i].Guess = null;
                        guesses[i].GuessOrig = null;
                    }
                }
            }

            oldns = wlst.Count;
            if (Affix.HasPhoneEntires)
            {
                for (var i = 0; i < roots.Length; i++)
                {
                    if (roots[i].RootPhon != null)
                    {
                        if (
                            wlst.Count < oldns + MaxPhonSugs
                            &&
                            wlst.Count < MaxSuggestions
                        )
                        {
                            var unique = true;
                            for (var j = 0; j < wlst.Count; j++)
                            {
                                // don't suggest previous suggestions or a previous suggestion with
                                // prefixes or affixes
                                if (
                                    roots[i].RootPhon.Contains(wlst[j])
                                    || // check forbidden words
                                    CheckWord(roots[i].RootPhon, false) == 0
                                )
                                {
                                    unique = false;
                                    break;
                                }
                            }

                            if (unique)
                            {
                                wlst.Add(roots[i].RootPhon);
                            }
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
                t = s2.Substring(0, l2 - 1) + Affix.Culture.TextInfo.ToLower(s2[l2 - 1]);
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
                    i < s1.Length
                    &&
                    i == t.Length
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
            var c = new LongestCommonSubsequenceType[(m + 1) * (n + 1)];
            b = new LongestCommonSubsequenceType[(m + 1) * (n + 1)];

            // NOTE: arrays are already zero (Up)

            for (var i = 1; i <= m; i++)
            {
                for (var j = 1; j <= n; j++)
                {
                    if (((s[i - 1] == s2[j - 1])))
                    {
                        c[i * (n + 1) + j] = c[(i - 1) * (n + 1) + j - 1] + 1;
                        b[i * (n + 1) + j] = LongestCommonSubsequenceType.UpLeft;
                    }
                    else if (c[(i - 1) * (n + 1) + j] >= c[i * (n + 1) + j - 1])
                    {
                        c[i * (n + 1) + j] = c[(i - 1) * (n + 1) + j];
                        b[i * (n + 1) + j] = LongestCommonSubsequenceType.Up;
                    }
                    else
                    {
                        c[i * (n + 1) + j] = c[i * (n + 1) + j - 1];
                        b[i * (n + 1) + j] = LongestCommonSubsequenceType.UpLeft;
                    }
                }
            }
        }

        private int ExpandRootWord(GuessWord[] wlst, DictionaryEntry entry, string bad, string phon)
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
            if (Affix.HasSuffixes)
            {
                for (var i = 0; i < entry.Flags.Count; i++)
                {
                    foreach (var sptrGroup in Affix.Suffixes)
                    {
                        foreach (var sptr in sptrGroup.Entries)
                        {
                            if (
                                sptrGroup.AFlag == entry.Flags[i]
                                &&
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
                                    sptr.HasContClasses
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
                                            var prefix = phon;
                                            var key = sptr.Key;
                                            key = key.Reverse();
                                            prefix += key;
                                            wlst[nh].Word = MyStrDup(prefix);
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
            }

            var n = nh;

            // handle cross products of prefixes and suffixes
            if (Affix.HasPrefixes)
            {
                for (var j = 1; j < n; j++)
                {
                    if (wlst[j].Allow)
                    {
                        for (var k = 0; k < entry.Flags.Count; k++)
                        {
                            foreach (var pfxGroup in Affix.Prefixes)
                            {
                                foreach (var cptr in pfxGroup.Entries)
                                {
                                    if (
                                        pfxGroup.AFlag == entry.Flags[k]
                                        &&
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
                                        var l1 = (wlst[j].Word?.Length).GetValueOrDefault();
                                        var newword = Add(cptr, wlst[j].Word);
                                        if (!string.IsNullOrEmpty(newword))
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
                }
            }

            // now handle pure prefixes
            if (Affix.HasPrefixes)
            {
                for (var m = 0; m < entry.Flags.Count; m++)
                {
                    foreach (var ptrGroup in Affix.Prefixes)
                    {
                        foreach (var ptr in ptrGroup.Entries)
                        {
                            if (
                                ptrGroup.AFlag == entry.Flags[m]
                                &&
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
            }

            return nh;
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

        private string MyStrDup(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var terminalPos = s.IndexOf('\0');
            return terminalPos >= 0 ? s.Substring(0, terminalPos) : s;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool CopyField(ref string dest, IEnumerable<string> morphs, string var)
        {
            return CopyField(ref dest, string.Join(" ", morphs), var);
        }

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
                var index1 = 0;
                var index2 = 0;

                // decapitalise dictionary word
                if (index1 < s1.Length && index2 < s2.Length && s1[index1] != s2[index2] && s1[index1] != Affix.Culture.TextInfo.ToLower(s2[index2]))
                {
                    return 0;
                }

                do
                {
                    index1++;
                    index2++;
                }
                while (index1 < s1.Length && index2 < s2.Length && s1[index1] == s2[index2]);

                return index1;
            }

            return 0;
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

            var t = opt.HasFlag(NGramOptions.Lowering)
                ? MakeAllSmall(s2)
                : s2;

            var nscore = opt.HasFlag(NGramOptions.Weighted)
                ? NGramWeightedSearch(n, s1, t)
                : NGramNonWeightedSearch(n, s1, t);

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

            return ns > 0
                ? nscore - ns
                : nscore;
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

            var wordCapacity = Math.Min(inword.Length, MaxPhoneTUtf8Len);
            var word = StringBuilderPool.Get(inword, 0, wordCapacity);
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
                            MyIsAlpha(word.GetCharOrTerminator(i + k)) // NOTE: could be implied?
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

                    if (char.IsDigit(sChar))
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
                                !MyIsAlpha(word.GetCharOrTerminator(i - 1))
                            )
                            &&
                            (
                                sString.GetCharOrTerminator(sIndex + 1) != '$'
                                ||
                                !MyIsAlpha(word.GetCharOrTerminator(i + k0))
                            )
                        )
                        ||
                        (
                            sChar == '$'
                            &&
                            i > 0
                            &&
                            MyIsAlpha(word.GetCharOrTerminator(i - 1))
                            &&
                            !MyIsAlpha(word.GetCharOrTerminator(i + k0))
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
                                    if (MyIsAlpha(word.GetCharOrTerminator(i + k0)) && sString.IndexOf(word.GetCharOrTerminator(i + k0), sIndex + 1) >= 0)
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
                                        !MyIsAlpha(word.GetCharOrTerminator(i + k0))
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

        private void StrMove(StringBuilder dest, int destOffset, StringBuilder src, int srcOffset)
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
        private static bool MyIsAlpha(char ch)
        {
            return ch < 128 || char.IsLetter(ch);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void InsertSuggestion(List<string> slst, string word)
        {
            slst.Insert(0, word);
        }

        private bool IsKeepCase(DictionaryEntry rv)
        {
            return rv.ContainsFlag(Affix.KeepCase);
        }

        /// <summary>
        /// Recursive search for right ss - sharp s permutations
        /// </summary>
        private DictionaryEntry SpellSharps(ref string @base, int nPos, int n, int repNum, ref SpellCheckResultType info, out string root)
        {
            var pos = @base.IndexOf("ss", nPos, StringComparison.Ordinal);
            if (pos >= 0 && n < MaxSharps)
            {
                var baseBuilder = StringBuilderPool.Get(@base, @base.Length);
                baseBuilder[pos] = 'ß';
                baseBuilder.Remove(pos + 1, 1);
                @base = baseBuilder.ToString();

                var h = SpellSharps(ref @base, pos + 1, n + 1, repNum + 1, ref info, out root);
                if (h != null)
                {
                    return h;
                }

                baseBuilder.Clear();
                baseBuilder.Append(@base);
                baseBuilder[pos] = 's';
                baseBuilder.Insert(pos + 1, 's');
                @base = StringBuilderPool.GetStringAndReturn(baseBuilder);

                h = SpellSharps(ref @base, pos + 2, n + 1, repNum, ref info, out root);
                if (h != null)
                {
                    return h;
                }
            }
            else if (repNum > 0)
            {
                return CheckWord(@base, ref info, out root);
            }

            root = null;
            return null;
        }

        private string MakeInitCap(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var builder = StringBuilderPool.Get(s, s.Length);
            builder[0] = Affix.Culture.TextInfo.ToUpper(builder[0]);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        /// <summary>
        /// Convert to all little.
        /// </summary>
        private string MakeAllSmall(string s)
        {
            return Affix.Culture.TextInfo.ToLower(s);
        }

        private string MakeInitSmall(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var builder = StringBuilderPool.Get(s, s.Length);
            builder[0] = Affix.Culture.TextInfo.ToLower(builder[0]);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        private string MakeAllCap(string s)
        {
            return Affix.Culture.TextInfo.ToUpper(s);
        }

        private DictionaryEntry CheckWord(string w, ref SpellCheckResultType info, out string root)
        {
            root = null;
            string w2;
            string word;
            bool useBuffer;
            if (Affix.HasIgnoredChars)
            {
                w2 = w.RemoveChars(Affix.IgnoredChars);
                word = w2;
                useBuffer = true;
            }
            else
            {
                w2 = string.Empty;
                word = w;
                useBuffer = false;
            }

            if (word.Length == 0)
            {
                return null;
            }

            // word reversing wrapper for complex prefixes
            if (Affix.ComplexPrefixes)
            {
                if (!useBuffer)
                {
                    w2 = word;
                    useBuffer = true;
                }

                w2 = w2.Reverse();
            }

            if (useBuffer)
            {
                word = w2;
            }

            // look word in hash table
            DictionaryEntry he;
            var entries = Dictionary.FindEntriesByRootWord(word);
            if (entries.IsDefaultOrEmpty)
            {
                he = null;
            }
            else
            {
                he = entries.First();

                // check forbidden and onlyincompound words
                if (he.ContainsFlag(Affix.ForbiddenWord))
                {
                    info |= SpellCheckResultType.Forbidden;

                    if (he.ContainsFlag(Affix.CompoundFlag) && Affix.Culture.IsHungarianLanguage())
                    {
                        info |= SpellCheckResultType.Compound;
                    }

                    return null;
                }

                // he = next not needaffix, onlyincompound homonym or onlyupcase word
                var heIndex = 0;
                while (
                    he != null
                    &&
                    he.HasFlags
                    &&
                    (
                        he.ContainsFlag(Affix.NeedAffix)
                        ||
                        he.ContainsFlag(Affix.OnlyInCompound)
                        ||
                        (
                            info.HasFlag(SpellCheckResultType.InitCap)
                            &&
                            he.ContainsFlag(SpecialFlags.OnlyUpcaseFlag)
                        )
                    )
                )
                {
                    heIndex++;
                    he = heIndex < entries.Length ? entries[heIndex] : null;
                }
            }

            // check with affixes
            if (he == null)
            {
                // try stripping off affixes
                he = AffixCheck(word, default(FlagValue), CompoundOptions.Not);

                // check compound restriction and onlyupcase
                if (
                    he != null
                    && he.HasFlags
                    &&
                    (
                        he.ContainsFlag(Affix.OnlyInCompound)
                        ||
                        (info.HasFlag(SpellCheckResultType.InitCap) && he.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
                    )
                )
                {
                    he = null;
                }

                if (he != null)
                {
                    if (he.ContainsFlag(Affix.ForbiddenWord))
                    {
                        info |= SpellCheckResultType.Forbidden;

                        return null;
                    }

                    root = he.Word;
                    if (Affix.ComplexPrefixes)
                    {
                        root = root.Reverse();
                    }
                }
                else if (Affix.HasCompound)
                {
                    // try check compound word
                    var rwords = new Dictionary<int, DictionaryEntry>();
                    he = CompoundCheck(word, 0, 0, 100, 0, null, ref rwords, 0, 0, ref info);

                    if (he == null && word.EndsWith('-') && Affix.Culture.IsHungarianLanguage())
                    {
                        // LANG_hu section: `moving rule' with last dash
                        var dup = word.Substring(0, word.Length - 1);
                        he = CompoundCheck(dup, -5, 0, 100, 0, null, ref rwords, 1, 0, ref info);
                    }

                    if (he != null)
                    {
                        root = he.Word;
                        if (Affix.ComplexPrefixes)
                        {
                            root = root.Reverse();
                        }

                        info |= SpellCheckResultType.Compound;
                    }
                }
            }

            return he;
        }

        private DictionaryEntry CompoundCheck(string word, int wordNum, int numSyllable, int maxwordnum, int wnum, Dictionary<int, DictionaryEntry> words, ref Dictionary<int, DictionaryEntry> rwords, int huMovRule, int isSug, ref SpellCheckResultType info)
        {
            int oldnumsyllable, oldnumsyllable2, oldwordnum, oldwordnum2;
            DictionaryEntry rv;
            DictionaryEntry rvFirst;
            var ch = '\0';
            var striple = false;
            var scpd = 0;
            var soldi = 0;
            var oldcmin = 0;
            var oldcmax = 0;
            var oldlen = 0;
            var checkedStriple = false;
            var affixed = false;

            var oldwords = words;
            var len = word.Length;
            var cmin = Affix.CompoundMin;
            var cmax = word.Length - cmin + 1;

            var st = new SimulatedCString(word);

            for (var i = cmin; i < cmax; i++)
            {
                words = oldwords;
                var onlycpdrule = words != null;
                do // onlycpdrule loop
                {

                    oldnumsyllable = numSyllable;
                    oldwordnum = wordNum;
                    var checkedPrefix = false;

                    do // simplified checkcompoundpattern loop
                    {

                        if (scpd > 0)
                        {
                            for (
                                ;
                                scpd <= Affix.CompoundPatterns.Length
                                &&
                                (
                                    string.IsNullOrEmpty(Affix.CompoundPatterns[scpd - 1].Pattern3)
                                    ||
                                    !StringEx.EqualsOffset(word, i, Affix.CompoundPatterns[scpd - 1].Pattern3, 0, Affix.CompoundPatterns[scpd - 1].Pattern3.Length)
                                )
                                ;
                                scpd++
                            )
                            {
                                ;
                            }

                            if (scpd > Affix.CompoundPatterns.Length)
                            {
                                break; // break simplified checkcompoundpattern loop
                            }

                            var scpdPatternEntry = Affix.CompoundPatterns[scpd - 1];

                            var neededSize = i + scpdPatternEntry.Pattern.Length + scpdPatternEntry.Pattern2.Length + (word.Length - (i + scpdPatternEntry.Pattern3.Length));

                            st.WriteChars(scpdPatternEntry.Pattern, i);

                            soldi = i;
                            i += scpdPatternEntry.Pattern.Length;

                            st.WriteChars(scpdPatternEntry.Pattern2, i);

                            st.WriteChars(soldi + scpdPatternEntry.Pattern3.Length, word, i + scpdPatternEntry.Pattern2.Length);

                            oldlen = len;
                            len += scpdPatternEntry.Pattern.Length + scpdPatternEntry.Pattern2.Length - scpdPatternEntry.Pattern3.Length;
                            oldcmin = cmin;
                            oldcmax = cmax;
                            cmin = Affix.CompoundMin;
                            cmax = len - Affix.CompoundMin + 1;
                        }

                        if (i < st.BufferLength)
                        {
                            ch = st[i];
                            st[i] = '\0';
                        }
                        else
                        {
                            ch = default(char);
                        }

                        ClearSuffix();
                        ClearPrefix();

                        // FIRST WORD

                        affixed = true;
                        var searchEntries = Lookup(st.ToString()); // perhaps without prefix
                        var searchEntriesIndex = 0;

                        rv = searchEntriesIndex < searchEntries.Length ? searchEntries[searchEntriesIndex] : null;

                        // search homonym with compound flag
                        while (
                            rv != null
                            && huMovRule == 0
                            &&
                            (
                                rv.ContainsFlag(Affix.NeedAffix)
                                ||
                                !(
                                    (
                                        !onlycpdrule
                                        &&
                                        words == null
                                        &&
                                        rv.ContainsFlag(Affix.CompoundFlag)
                                    )
                                    ||
                                    (
                                        wordNum == 0
                                        &&
                                        !onlycpdrule
                                        &&
                                        rv.ContainsFlag(Affix.CompoundBegin)
                                    )
                                    ||
                                    (
                                        wordNum != 0
                                        &&
                                        !onlycpdrule
                                        &&
                                        words == null
                                        &&
                                        rv.ContainsFlag(Affix.CompoundMiddle)
                                    )
                                    ||
                                    (
                                        Affix.HasCompoundRules
                                        &&
                                        onlycpdrule
                                        &&
                                        (
                                            (
                                                words == null
                                                &&
                                                wordNum == 0
                                                &&
                                                DefCompoundCheck(ref words, wnum, rv, rwords, 0)
                                            )
                                            ||
                                            (
                                                words != null
                                                &&
                                                DefCompoundCheck(ref words, wnum, rv, rwords, 0)
                                            )
                                        )
                                    )
                                )
                                ||
                                (
                                    scpd != 0
                                    &&
                                    Affix.CompoundPatterns[scpd - 1].Condition.HasValue
                                    &&
                                    !rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition)
                                )
                            )
                        )
                        {
                            searchEntriesIndex++;
                            rv = searchEntriesIndex < searchEntries.Length ? searchEntries[searchEntriesIndex] : null;
                        }

                        if (rv != null)
                        {
                            affixed = false;
                        }

                        if (rv == null)
                        {
                            if (onlycpdrule)
                            {
                                break;
                            }

                            if (
                                Affix.CompoundFlag.HasValue
                                &&
                                (
                                    rv = PrefixCheck(st, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundFlag)
                                ) == null
                            )
                            {
                                if (
                                    (
                                        (
                                            rv = SuffixCheck(st, 0, null, null, new FlagValue(), Affix.CompoundFlag, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st, 0, null, null, Affix.CompoundFlag)
                                            ) != null
                                        )
                                    )
                                    &&
                                    huMovRule == 0
                                    &&
                                    Suffix != null
                                    &&
                                    Suffix.ContainsAnyContClass(Affix.CompoundForbidFlag, Affix.CompoundEnd)
                                )
                                {
                                    rv = null;
                                }
                            }

                            if (
                                rv != null
                                ||
                                (
                                    wordNum == 0
                                    &&
                                    Affix.CompoundBegin.HasValue
                                    &&
                                    (
                                        (
                                            rv = SuffixCheck(st, 0, null, null, new FlagValue(), Affix.CompoundBegin, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st, 0, null, null, Affix.CompoundBegin)
                                            ) != null
                                        )
                                        || // twofold suffixes + compound
                                        (
                                            rv = PrefixCheck(st, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundBegin)
                                        ) != null
                                    )
                                )
                                ||
                                (
                                    wordNum > 0
                                    &&
                                    Affix.CompoundMiddle.HasValue
                                    &&
                                    (
                                        (
                                            rv = SuffixCheck(st, 0, null, null, new FlagValue(), Affix.CompoundMiddle, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st, 0, null, null, Affix.CompoundMiddle)
                                            ) != null
                                        )
                                        || // twofold suffixes + compound
                                        (
                                            rv = PrefixCheck(st, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundMiddle)
                                        ) != null
                                    )
                                )
                            )
                            {
                                checkedPrefix = true;
                            }
                        }
                        else if (
                            rv.HasFlags
                            &&
                            (
                                rv.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NeedAffix, SpecialFlags.OnlyUpcaseFlag)
                                ||
                                (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                            )
                        )
                        {
                            // else check forbiddenwords and needaffix
                            if (i < st.BufferLength)
                            {
                                st[i] = ch;
                            }

                            break;
                        }

                        // check non_compound flag in suffix and prefix
                        if (
                            rv != null
                            &&
                            huMovRule == 0
                            &&
                            Affix.CompoundForbidFlag.HasValue
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundForbidFlag))
                                ||
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundForbidFlag))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check compoundend flag in suffix and prefix
                        if (
                            rv != null
                            &&
                            !checkedPrefix
                            &&
                            Affix.CompoundEnd.HasValue
                            &&
                            huMovRule == 0
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundEnd))
                                ||
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundEnd))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check compoundmiddle flag in suffix and prefix
                        if (
                            rv != null
                            &&
                            !checkedPrefix
                            &&
                            wordNum == 0
                            &&
                            Affix.CompoundMiddle.HasValue
                            &&
                            huMovRule == 0
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundMiddle))
                                ||
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundMiddle))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check forbiddenwords
                        if (
                            rv != null
                            &&
                            rv.HasFlags
                            &&
                            (
                                rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                ||
                                (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                            )
                        )
                        {
                            st.Destroy();
                            return null;
                        }

                        // increment word number, if the second root has a compoundroot flag
                        if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                        {
                            wordNum++;
                        }

                        // first word is acceptable in compound words?
                        if (
                            (
                                rv != null
                                &&
                                (
                                    checkedPrefix
                                    ||
                                    (
                                        words != null
                                        &&
                                        words.ContainsKey(wnum)
                                        &&
                                        words[wnum] != null
                                    )
                                    ||
                                    rv.ContainsFlag(Affix.CompoundFlag)
                                    ||
                                    (oldwordnum == 0 && rv.ContainsFlag(Affix.CompoundBegin))
                                    ||
                                    (oldwordnum > 0 && rv.ContainsFlag(Affix.CompoundMiddle))
                                    ||
                                    (
                                        huMovRule != 0
                                        && // LANG_hu section: spec. Hungarian rule
                                        Affix.Culture.IsHungarianLanguage()
                                        && // XXX hardwired Hungarian dictionary codes
                                        rv.ContainsAnyFlags(SpecialFlags.LetterF, SpecialFlags.LetterG, SpecialFlags.LetterH)
                                    ) // END of LANG_hu section
                                )
                                && // test CHECKCOMPOUNDPATTERN conditions
                                (
                                    scpd == 0
                                    ||
                                    !Affix.CompoundPatterns[scpd - 1].Condition.HasValue
                                    ||
                                    rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition)
                                )
                                &&
                                !(
                                    (
                                        Affix.CheckCompoundTriple
                                        &&
                                        scpd == 0
                                        &&
                                        words == null
                                        && // test triple letters
                                        i > 0
                                        &&
                                        i < word.Length
                                        &&
                                        word[i - 1] == word[i]
                                        &&
                                        (
                                            (i - 2 >= 0 && word[i - 1] == word[i - 2])
                                            ||
                                            (i + 1 < word.Length && word[i - 1] == word[i + 1])
                                        )
                                    )
                                    ||
                                    (
                                        Affix.CheckCompoundCase
                                        &&
                                        scpd == 0
                                        &&
                                        words == null
                                        &&
                                        CompoundCaseCheck(word, i)
                                    )
                                )
                            )
                            || // LANG_hu section: spec. Hungarian rule
                            (
                                rv == null
                                &&
                                huMovRule != 0
                                &&
                                Affix.Culture.IsHungarianLanguage()
                                &&
                                (
                                    rv = AffixCheck(st, new FlagValue(), CompoundOptions.Not)
                                ) != null
                                &&
                                Suffix != null // XXX hardwired Hungarian dic. codes
                                &&
                                Suffix.ContainsAnyContClass(SpecialFlags.LetterXLower, SpecialFlags.LetterPercent)
                            )
                        )
                        {
                            // first word is ok condition
                            if (Affix.Culture.IsHungarianLanguage())
                            {
                                // calculate syllable number of the word
                                numSyllable += GetSyllable(st.Substring(i));

                                // - affix syllable num.
                                // XXX only second suffix (inflections, not derivations)
                                if (SuffixAppend != null)
                                {
                                    numSyllable -= GetSyllable(SuffixAppend.Reverse()) + SuffixExtra;
                                }

                                // + 1 word, if syllable number of the prefix > 1 (hungarian convention)
                                if (Prefix != null && GetSyllable(Prefix.Key) > 1)
                                {
                                    wordNum++;
                                }
                            }

                            // NEXT WORD(S)
                            rvFirst = rv;

                            if (i < st.BufferLength)
                            {
                                st[i] = ch;
                            }

                            do
                            {
                                // striple loop

                                // check simplifiedtriple
                                if (Affix.SimplifiedTriple)
                                {
                                    if (striple)
                                    {
                                        checkedStriple = true;
                                        i--; // check "fahrt" instead of "ahrt" in "Schiffahrt"
                                    }
                                    else if (i > 2 && word[i - 1] == word[i - 2])
                                    {
                                        striple = true;
                                    }
                                }

                                var homonyms = Lookup(st.Substring(i));  // perhaps without prefix
                                var homonymIndex = 0;

                                rv = homonymIndex < homonyms.Length ? homonyms[homonymIndex] : null;
                                // search homonym with compound flag
                                while (
                                    rv != null
                                    &&
                                    (
                                        rv.ContainsFlag(Affix.NeedAffix)
                                        ||
                                        !(
                                            (words == null && rv.ContainsFlag(Affix.CompoundFlag))
                                            ||
                                            (words == null && rv.ContainsFlag(Affix.CompoundEnd))
                                            ||
                                            (
                                                Affix.HasCompoundRules
                                                && words != null
                                                && DefCompoundCheck(ref words, wnum + 1, rv, null, 1)
                                            )
                                        )
                                        ||
                                        (
                                            scpd != 0
                                            && Affix.CompoundPatterns[scpd - 1].Condition2.HasValue
                                            && !rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition2)
                                        )
                                    )
                                )
                                {
                                    homonymIndex++;
                                    rv = homonymIndex < homonyms.Length ? homonyms[homonymIndex] : null;
                                }

                                // check FORCEUCASE
                                if (
                                    rv != null
                                    &&
                                    rv.ContainsFlag(Affix.ForceUpperCase)
                                    &&
                                    !info.HasFlag(SpellCheckResultType.OrigCap)
                                )
                                {
                                    rv = null;
                                }

                                if (
                                    rv != null
                                    &&
                                    words != null
                                    &&
                                    words.ContainsKey(wnum + 1)
                                    &&
                                    words[wnum + 1] != null
                                )
                                {
                                    st.Destroy();
                                    return rvFirst;
                                }

                                oldnumsyllable2 = numSyllable;
                                oldwordnum2 = wordNum;

                                if (
                                    rv != null
                                    &&
                                    Affix.Culture.IsHungarianLanguage()
                                    &&
                                    rv.ContainsFlag(SpecialFlags.LetterI)
                                    &&
                                    !rv.ContainsFlag(SpecialFlags.LetterJ)
                                )
                                {
                                    numSyllable--;
                                }

                                // increment word number, if the second root has a compoundroot flag
                                if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                                {
                                    wordNum++;
                                }

                                // check forbiddenwords
                                if (
                                    rv != null
                                    &&
                                    rv.HasFlags
                                    &&
                                    (
                                        rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                        ||
                                        (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                                    )
                                )
                                {
                                    st.Destroy();
                                    return null;
                                }

                                // second word is acceptable, as a root?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist of 2 words, or if more,
                                // then the syllable number of root words must be 6, or lesser.

                                if (
                                    rv != null
                                    &&
                                    rv.ContainsAnyFlags(Affix.CompoundFlag, Affix.CompoundEnd)
                                    &&
                                    (
                                        !Affix.CompoundWordMax.HasValue
                                        ||
                                        wordNum + 1 < Affix.CompoundWordMax
                                        ||
                                        (
                                            Affix.CompoundMaxSyllable != 0
                                            &&
                                            numSyllable + GetSyllable(rv.Word) <= Affix.CompoundMaxSyllable
                                        )
                                    )
                                    && // test CHECKCOMPOUNDPATTERN
                                    (
                                        !Affix.HasCompoundPatterns
                                        ||
                                        scpd != 0
                                        ||
                                        !CompoundPatternCheck(word, i, rvFirst, rv, false)
                                    )
                                    &&
                                    (
                                        !Affix.CheckCompoundDup
                                        ||
                                        rv != rvFirst
                                    )
                                    && // test CHECKCOMPOUNDPATTERN conditions
                                    (
                                        scpd == 0
                                        ||
                                        !Affix.CompoundPatterns[scpd - 1].Condition2.HasValue
                                        ||
                                        rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition2)
                                    )
                                )
                                {
                                    st.Destroy();

                                    // forbid compound word, if it is a non compound word with typical fault
                                    if (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Substring(0, len)))
                                    {
                                        return null;
                                    }

                                    return rvFirst;
                                }

                                numSyllable = oldnumsyllable2;
                                wordNum = oldwordnum2;

                                // perhaps second word has prefix or/and suffix
                                ClearSuffix();
                                SuffixFlag = default(FlagValue);

                                rv = (!onlycpdrule && Affix.CompoundFlag.HasValue)
                                     ? AffixCheck(word.Substring(i), Affix.CompoundFlag, CompoundOptions.End)
                                     : null;

                                if (rv == null && Affix.CompoundEnd.HasValue && !onlycpdrule)
                                {
                                    ClearSuffix();
                                    ClearPrefix();
                                    rv = AffixCheck(word.Substring(i), Affix.CompoundEnd, CompoundOptions.End);
                                }

                                if (rv == null && Affix.HasCompoundRules && words != null)
                                {
                                    rv = AffixCheck(word.Substring(i), new FlagValue(), CompoundOptions.End);
                                    if (rv != null && DefCompoundCheck(ref words, wnum + 1, rv, null, 1))
                                    {
                                        st.Destroy();
                                        return rvFirst;
                                    }

                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (allowed forms)
                                if (
                                    rv != null
                                    &&
                                    !(
                                        scpd == 0
                                        ||
                                        !Affix.CompoundPatterns[scpd - 1].Condition2.HasValue
                                        ||
                                        rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition2)
                                    )
                                )
                                {
                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (forbidden compounds)
                                if (
                                    rv != null
                                    &&
                                    scpd == 0
                                    &&
                                    Affix.HasCompoundPatterns
                                    &&
                                    CompoundPatternCheck(word, i, rvFirst, rv, affixed)
                                )
                                {
                                    rv = null;
                                }

                                // check non_compound flag in suffix and prefix
                                if (
                                    rv != null
                                    &&
                                    Affix.CompoundForbidFlag.HasValue
                                    &&
                                    (
                                        (Prefix != null && Prefix.ContainsContClass(Affix.CompoundForbidFlag))
                                        ||
                                        (Suffix != null && Suffix.ContainsContClass(Affix.CompoundForbidFlag))
                                    )
                                )
                                {
                                    rv = null;
                                }

                                // check FORCEUCASE
                                if (
                                    rv != null
                                    &&
                                    rv.ContainsFlag(Affix.ForceUpperCase)
                                    &&
                                    !info.HasFlag(SpellCheckResultType.OrigCap)
                                )
                                {
                                    rv = null;
                                }

                                // check forbiddenwords
                                if (
                                    rv != null
                                    &&
                                    rv.HasFlags
                                    &&
                                    (
                                        rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                        ||
                                        (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                                    )
                                )
                                {
                                    st.Destroy();
                                    return null;
                                }

                                // pfxappnd = prefix of word+i, or NULL
                                // calculate syllable number of prefix.
                                // hungarian convention: when syllable number of prefix is more,
                                // than 1, the prefix+word counts as two words.

                                if (Affix.Culture.IsHungarianLanguage())
                                {
                                    // calculate syllable number of the word
                                    numSyllable += GetSyllable(word.Substring(i));

                                    // - affix syllable num.
                                    // XXX only second suffix (inflections, not derivations)
                                    if (SuffixAppend != null)
                                    {
                                        numSyllable -= GetSyllable(SuffixAppend.Reverse()) + SuffixExtra;
                                    }

                                    // + 1 word, if syllable number of the prefix > 1 (hungarian
                                    // convention)
                                    if (Prefix != null && GetSyllable(Prefix.Key) > 1)
                                    {
                                        wordNum++;
                                    }

                                    // increment syllable num, if last word has a SYLLABLENUM flag
                                    // and the suffix is beginning `s'

                                    if (!string.IsNullOrEmpty(Affix.CompoundSyllableNum))
                                    {
                                        if (SuffixFlag == SpecialFlags.LetterCLower)
                                        {
                                            numSyllable += 2;
                                        }
                                        else if (SuffixFlag == SpecialFlags.LetterJ)
                                        {
                                            numSyllable += 1;
                                        }
                                        else if (SuffixFlag == SpecialFlags.LetterI)
                                        {
                                            if (rv != null && rv.ContainsFlag(SpecialFlags.LetterJ))
                                            {
                                                numSyllable += 1;
                                            }
                                        }
                                    }
                                }

                                // increment word number, if the second word has a compoundroot flag
                                if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                                {
                                    wordNum++;
                                }

                                // second word is acceptable, as a word with prefix or/and suffix?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist 2 word, otherwise
                                // the syllable number of root words is 6, or lesser.
                                if (
                                    rv != null
                                    &&
                                    (
                                        !Affix.CompoundWordMax.HasValue
                                        || wordNum + 1 < Affix.CompoundWordMax
                                        ||
                                        (
                                            Affix.CompoundMaxSyllable != 0
                                            && numSyllable <= Affix.CompoundMaxSyllable
                                        )
                                    )
                                    &&
                                    (
                                        !Affix.CheckCompoundDup || rv != rvFirst
                                    )
                                )
                                {
                                    st.Destroy();

                                    // forbid compound word, if it is a non compound word with typical fault
                                    if (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Substring(0, len)))
                                    {
                                        return null;
                                    }

                                    return rvFirst;
                                }

                                numSyllable = oldnumsyllable2;
                                wordNum = oldwordnum2;

                                // perhaps second word is a compound word (recursive call)
                                if (wordNum < maxwordnum)
                                {
                                    rv = CompoundCheck(st.Substring(i), wordNum + 1, numSyllable, maxwordnum, wnum + 1, words, ref rwords, 0, isSug, ref info);

                                    if (
                                        rv != null
                                        && Affix.HasCompoundPatterns
                                        &&
                                        (
                                            (scpd == 0 && CompoundPatternCheck(word, i, rvFirst, rv, affixed))
                                            ||
                                            (scpd != 0 && !CompoundPatternCheck(word, i, rvFirst, rv, affixed))
                                        )
                                    )
                                    {
                                        rv = null;
                                    }
                                }
                                else
                                {
                                    rv = null;
                                }

                                if (rv != null)
                                {
                                    // forbid compound word, if it is a non compound word with typical fault
                                    if (Affix.CheckCompoundRep || Affix.ForbiddenWord.HasValue)
                                    {
                                        if (Affix.CheckCompoundRep && CompoundReplacementCheck(word))
                                        {
                                            st.Destroy();
                                            return null;
                                        }

                                        // check first part
                                        if (StringEx.EqualsOffset(rv.Word, 0, word, i, rv.Word.Length))
                                        {
                                            var r = st[i + rv.Word.Length];
                                            if (i + rv.Word.Length < st.BufferLength)
                                            {
                                                st[i + rv.Word.Length] = '\0';
                                            }

                                            if (Affix.CheckCompoundRep && CompoundReplacementCheck(st))
                                            {
                                                if (i + rv.Word.Length < st.BufferLength)
                                                {
                                                    st[i + rv.Word.Length] = r;
                                                }

                                                continue;
                                            }

                                            if (Affix.ForbiddenWord.HasValue)
                                            {
                                                var rv2 = Lookup(word)
                                                    .FirstOrDefault();

                                                if (rv2 == null)
                                                {
                                                    rv2 = AffixCheck(word.Substring(0, len), default(FlagValue), CompoundOptions.Not);
                                                }

                                                if (
                                                    rv2 != null
                                                    && rv2.ContainsFlag(Affix.ForbiddenWord)
                                                    && StringEx.EqualsOffset(rv2.Word, 0, st, 0, i + rv.Word.Length)
                                                )
                                                {
                                                    st.Destroy();
                                                    return null;
                                                }
                                            }


                                            if (i + rv.Word.Length < st.BufferLength)
                                            {
                                                st[i + rv.Word.Length] = r;
                                            }
                                        }
                                    }

                                    st.Destroy();
                                    return rvFirst;
                                }
                            }
                            while (striple && !checkedStriple);  // end of striple loop

                            if (checkedStriple)
                            {
                                i++;
                                checkedStriple = false;
                                striple = false;
                            }

                        } // first word is ok condition

                        if (soldi != 0)
                        {
                            i = soldi;
                            soldi = 0;
                            len = oldlen;
                            cmin = oldcmin;
                            cmax = oldcmax;
                        }

                        scpd++;
                    }
                    while (!onlycpdrule && Affix.SimplifiedCompound && scpd <= Affix.CompoundPatterns.Length); // end of simplifiedcpd loop

                    scpd = 0;
                    wordNum = oldwordnum;
                    numSyllable = oldnumsyllable;

                    if (soldi != 0)
                    {
                        i = soldi;
                        st.Assign(word);
                        soldi = 0;
                    }
                    else
                    {
                        if (i < st.BufferLength)
                        {
                            st[i] = ch;
                        }
                    }
                }
                while (Affix.HasCompoundRules && oldwordnum == 0 && !PostfixIncrement(ref onlycpdrule));
            }

            st.Destroy();
            return null;
        }

        private static bool PostfixIncrement(ref bool b)
        {
            if (b)
            {
                return true;
            }
            else
            {
                b = true;
                return false;
            }
        }

        /// <summary>
        /// Check if word with affixes is correctly spelled.
        /// </summary>
        private DictionaryEntry AffixCheck(string word, FlagValue needFlag, CompoundOptions inCompound)
        {
            // check all prefixes (also crossed with suffixes if allowed)
            var rv = PrefixCheck(word, inCompound, needFlag);
            if (rv != null)
            {
                return rv;
            }

            // if still not found check all suffixes
            rv = SuffixCheck(word, 0, null, null, new FlagValue(), needFlag, inCompound);

            if (Affix.HasContClass)
            {
                ClearSuffix();
                ClearPrefix();

                if (rv != null)
                {
                    return rv;
                }

                // if still not found check all two-level suffixes
                rv = SuffixCheckTwoSfx(word, 0, null, null, needFlag);
                if (rv != null)
                {
                    return rv;
                }

                // if still not found check all two-level prefixes
                rv = PrefixCheckTwoSfx(word, CompoundOptions.Not, needFlag);
            }

            return rv;
        }

        /// <summary>
        /// Check word for prefixes
        /// </summary>
        private DictionaryEntry PrefixCheck(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            ClearPrefix();
            PrefixAppend = null;
            SuffixAppend = null;
            SuffixExtra = 0;
            DictionaryEntry rv;

            // first handle the special case of 0 length prefixes
            foreach (var peGroup in Affix.Prefixes)
            {
                foreach (var pe in peGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    if (
                        // fogemorpheme
                        (inCompound != CompoundOptions.Not || !pe.ContainsContClass(Affix.OnlyInCompound))
                        &&
                        // permit prefixes in compounds
                        (inCompound != CompoundOptions.End || pe.ContainsContClass(Affix.CompoundPermitFlag))
                    )
                    {
                        // check prefix
                        rv = CheckWordPrefix(peGroup, pe, word, inCompound, needFlag);
                        if (rv != null)
                        {
                            SetPrefix(peGroup, pe);
                            return rv;
                        }
                    }
                }
            }

            // now handle the general case
            foreach (var pptrGroup in Affix.Prefixes)
            {
                foreach (var pptr in pptrGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (StringEx.IsSubset(pptr.Key, word))
                    {
                        if (
                            // fogemorpheme
                            (inCompound != CompoundOptions.Not || !pptr.ContainsContClass(Affix.OnlyInCompound))
                            &&
                            // permit prefixes in compounds
                            (inCompound != CompoundOptions.End || pptr.ContainsContClass(Affix.CompoundPermitFlag))
                        )
                        {
                            // check prefix
                            rv = CheckWordPrefix(pptrGroup, pptr, word, inCompound, needFlag);
                            if (rv != null)
                            {
                                SetPrefix(pptrGroup, pptr);
                                return rv;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private DictionaryEntry PrefixCheckTwoSfx(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            ClearPrefix();
            SuffixAppend = null;
            SuffixExtra = 0;
            DictionaryEntry rv;

            // first handle the special case of 0 length prefixes
            foreach (var peGroup in Affix.Prefixes)
            {
                foreach (var pe in peGroup.Entries.Where(e => e.Key.Length == 0))
                {
                    rv = CheckTwoSfx(peGroup, pe, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        return rv;
                    }
                }
            }

            // now handle the general case
            foreach (var pptrGroup in Affix.Prefixes)
            {
                foreach (var pptr in pptrGroup.Entries.Where(e => e.Key.Length != 0))
                {
                    if (StringEx.IsSubset(pptr.Key, word))
                    {
                        rv = CheckTwoSfx(pptrGroup, pptr, word, inCompound, needFlag);
                        if (rv != null)
                        {
                            SetPrefix(pptrGroup, pptr);
                            return rv;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Check if this prefix entry matches.
        /// </summary>
        private DictionaryEntry CheckTwoSfx(AffixEntryGroup<PrefixEntry> prefixGroup, PrefixEntry pe, string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            // on entry prefix is 0 length or already matches the beginning of the word.
            // So if the remaining root word has positive length
            // and if there are enough chars in root word and added back strip chars
            // to meet the number of characters conditions, then test it

            var tmpl = word.Length - pe.Append.Length; // length of tmpword

            if (
                (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                &&
                (tmpl + pe.Strip.Length >= pe.Conditions.Count)
            )
            {
                // generate new root word by removing prefix and adding
                // back any characters that would have been stripped

                var tmpword = pe.Strip + word.Substring(pe.Append.Length);

                // now make sure all of the conditions on characters
                // are met.  Please see the appendix at the end of
                // this file for more info on exactly what is being
                // tested

                // if all conditions are met then check if resulting
                // root word in the dictionary

                if (TestCondition(pe, tmpword))
                {
                    // prefix matched but no root word was found
                    // if CrossProduct is allowed, try again but now
                    // cross checked combined with a suffix

                    if (prefixGroup.Options.HasFlag(AffixEntryOptions.CrossProduct) && (inCompound != CompoundOptions.Begin))
                    {
                        // find hash entry of root word
                        var he = SuffixCheckTwoSfx(tmpword, AffixEntryOptions.CrossProduct, prefixGroup, pe, needFlag);
                        if (he != null)
                        {
                            return he;
                        }
                    }
                }
            }

            return null;
        }

        private DictionaryEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
        {
            DictionaryEntry rv = null;

            if (!Affix.HasSuffixes)
            {
                return null;
            }

            // first handle the special case of 0 length suffixes
            foreach (var seGroup in Affix.Suffixes)
            {
                foreach (var se in seGroup.Entries.Where(e => e.Key.Length == 0))
                {
                    if (!cclass.HasValue || se.HasContClasses)
                    {
                        // suffixes are not allowed in beginning of compounds
                        if (
                            (
                                inCompound != CompoundOptions.Begin
                                ||
                                // except when signed with compoundpermitflag flag
                                se.ContainsContClass(Affix.CompoundPermitFlag)
                            )
                            &&
                            (
                                !Affix.Circumfix.HasValue
                                ||
                                // no circumfix flag in prefix and suffix
                                (
                                    (
                                        pfx == null
                                        ||
                                        !pfx.HasContClasses
                                        ||
                                        !pfx.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    (
                                        !se.HasContClasses
                                        ||
                                        !se.ContainsContClass(Affix.Circumfix)
                                    )
                                )
                                ||
                                // circumfix flag in prefix AND suffix
                                (
                                    pfx != null
                                    && pfx.ContainsContClass(Affix.Circumfix)
                                    && se.ContainsContClass(Affix.Circumfix)
                                )
                            )
                            &&
                            // fogemorpheme
                            (
                                inCompound != CompoundOptions.Not
                                ||
                                !se.ContainsContClass(Affix.OnlyInCompound)
                            )
                            &&
                            // needaffix on prefix or first suffix
                            (
                                cclass.HasValue
                                ||
                                !se.ContainsContClass(Affix.NeedAffix)
                                ||
                                (
                                    pfx != null
                                    &&
                                    !pfx.ContainsContClass(Affix.NeedAffix)
                                )
                            )
                        )
                        {
                            rv = CheckWordSuffix(seGroup, se, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? default(FlagValue) : Affix.OnlyInCompound);
                            if (rv != null)
                            {
                                SetSuffix(seGroup, se);
                                return rv;
                            }
                        }
                    }
                }
            }

            // now handle the general case
            if (string.IsNullOrEmpty(word))
            {
                return null;
            }

            foreach (var sptrGroup in Affix.Suffixes)
            {
                foreach (var sptr in sptrGroup.Entries.Where(e => e.Key.Length != 0))
                {
                    if (StringEx.IsReverseSubset(sptr.Key, word))
                    {
                        if (
                            (
                                // suffixes are not allowed in beginning of compounds
                                inCompound != CompoundOptions.Begin
                                ||
                                // except when signed with compoundpermitflag flag
                                sptr.ContainsContClass(Affix.CompoundPermitFlag)
                            )
                            &&
                            (
                                !Affix.Circumfix.HasValue
                                ||
                                // no circumfix flag in prefix and suffix
                                (
                                    (
                                        pfx == null
                                        ||
                                        !pfx.HasContClasses
                                        ||
                                        !pfx.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    (
                                        !sptr.HasContClasses
                                        ||
                                        !sptr.ContainsContClass(Affix.Circumfix)
                                    )
                                )
                                ||
                                // circumfix flag in prefix AND suffix
                                (
                                    pfx != null
                                    && pfx.ContainsContClass(Affix.Circumfix)
                                    && sptr.ContainsContClass(Affix.Circumfix)
                                )
                            )
                            &&
                            // fogemorpheme
                            (
                                inCompound != CompoundOptions.Not
                                ||
                                !sptr.ContainsContClass(Affix.OnlyInCompound)
                            )
                            &&
                            // needaffix on prefix or first suffix
                            (
                                cclass.HasValue
                                ||
                                !sptr.ContainsContClass(Affix.NeedAffix)
                                ||
                                (
                                    pfx != null
                                    &&
                                    !pfx.ContainsContClass(Affix.NeedAffix)
                                )
                            )
                        )
                        {
                            if (
                                inCompound != CompoundOptions.End
                                ||
                                pfx != null
                                ||
                                !sptr.ContainsContClass(Affix.OnlyInCompound)
                            )
                            {
                                rv = CheckWordSuffix(sptrGroup, sptr, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, (inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound));
                                if (rv != null)
                                {
                                    SetSuffix(sptrGroup, sptr);
                                    SuffixFlag = sptrGroup.AFlag;

                                    if (!sptr.HasContClasses)
                                    {
                                        SuffixAppend = sptr.Key;
                                    }
                                    else if (
                                        Affix.Culture.IsHungarianLanguage()
                                        && sptr.Key.Length >= 2
                                        && sptr.Key[0] == 'i'
                                        && sptr.Key[1] != 'y'
                                        && sptr.Key[1] != 't'
                                    )
                                    {
                                        // LANG_hu section: spec. Hungarian rule
                                        SuffixExtra = 1;
                                    }

                                    // END of LANG_hu section
                                    return rv;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Check word for two-level suffixes.
        /// </summary>
        private DictionaryEntry SuffixCheckTwoSfx(string word, AffixEntryOptions sfxopts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue needflag)
        {
            DictionaryEntry rv;

            // first handle the special case of 0 length suffixes
            foreach (var seGroup in Affix.Suffixes)
            {
                foreach (var se in seGroup.Entries.Where(e => e.Key.Length == 0))
                {
                    if (Affix.ContClasses.Contains(seGroup.AFlag))
                    {
                        rv = CheckTwoSfx(seGroup, se, word, sfxopts, pfxGroup, pfx, needflag);
                        if (rv != null)
                        {
                            return rv;
                        }
                    }
                }
            }

            // now handle the general case
            if (string.IsNullOrEmpty(word))
            {
                return null; // FULLSTRIP
            }

            foreach (var sptrGroup in Affix.Suffixes)
            {
                foreach (var sptr in sptrGroup.Entries.Where(e => e.Key.Length != 0))
                {
                    if (StringEx.IsReverseSubset(sptr.Key, word))
                    {
                        if (Affix.ContClasses.Contains(sptrGroup.AFlag))
                        {
                            rv = CheckTwoSfx(sptrGroup, sptr, word, sfxopts, pfxGroup, pfx, needflag);
                            if (rv != null)
                            {
                                SuffixFlag = SuffixGroup.AFlag;
                                if (!sptr.HasContClasses)
                                {
                                    SuffixAppend = sptr.Key;
                                }

                                return rv;
                            }
                        }
                    }
                }
            }

            return null;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private ImmutableArray<DictionaryEntry> Lookup(string word)
        {
            return Dictionary.FindEntriesByRootWord(word);
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

        /// <summary>
        /// Compound check patterns.
        /// </summary>
        private bool DefCompoundCheck(ref Dictionary<int, DictionaryEntry> words, int wnum, DictionaryEntry rv, Dictionary<int, DictionaryEntry> def, int all)
        {
            var w = false;

            if (words == null)
            {
                w = true;
                words = def;

                if (words == null)
                {
                    return false;
                }
            }

            var btinfo = new List<MetacharData>
            {
                new MetacharData()
            };

            var bt = 0;

            words[wnum] = rv;

            // has the last word COMPOUNDRULE flag?
            if (!rv.HasFlags)
            {
                words[wnum] = null;
                if (w)
                {
                    words = null;
                }

                return false;
            }

            var ok = false;
            foreach (var compoundRule in Affix.CompoundRules)
            {
                foreach (var flag in compoundRule)
                {
                    if (!flag.Equals('*') && !flag.Equals('?') && rv.ContainsFlag(flag))
                    {
                        ok = true;
                        break;
                    }
                }
            }

            if (!ok)
            {
                words[wnum] = null;
                if (w)
                {
                    words = null;
                }

                return false;
            }

            for (var i = 0; i < Affix.CompoundRules.Length; i++)
            {
                var pp = 0; // pattern position
                var wp = 0; // "words" position
                ok = true;
                var ok2 = true;
                do
                {
                    while (pp < Affix.CompoundRules[i].Length && wp <= wnum)
                    {
                        if (
                            pp + 1 < Affix.CompoundRules[i].Length
                            &&
                            (
                                Affix.CompoundRules[i][pp + 1] == '*'
                                ||
                                Affix.CompoundRules[i][pp + 1] == '?'
                            )
                        )
                        {
                            var wend = Affix.CompoundRules[i][pp + 1] == '?' ? wp : wnum;
                            ok2 = true;
                            pp += 2;
                            btinfo[bt].btpp = pp;
                            btinfo[bt].btwp = wp;

                            while (wp <= wend)
                            {
                                if (!words[wp].HasFlags || !words[wp].ContainsFlag(Affix.CompoundRules[i][pp - 2]))
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
                                !words[wp].ContainsFlag(Affix.CompoundRules[i][pp])
                            )
                            {
                                ok = false;
                                break;
                            }

                            pp++;
                            wp++;

                            if (Affix.CompoundRules[i].Length == pp && wp <= wnum)
                            {
                                ok = false;
                            }
                        }
                    }

                    if (ok && ok2)
                    {
                        var r = pp;
                        while (
                            Affix.CompoundRules[i].Length > r
                            &&
                            r + 1 < Affix.CompoundRules[i].Length
                            &&
                            (
                                Affix.CompoundRules[i][r + 1] == '*'
                                ||
                                Affix.CompoundRules[i][r + 1] == '?'
                            )
                        )
                        {
                            r += 2;
                        }

                        if (Affix.CompoundRules[i].Length <= r)
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
                        all == 0
                        ||
                        Affix.CompoundRules[i].Length <= pp
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
                    pp < Affix.CompoundRules[i].Length
                    &&
                    pp + 1 < Affix.CompoundRules[i].Length
                    &&
                    (
                        (Affix.CompoundRules[i][pp + 1] == '*')
                        ||
                        (Affix.CompoundRules[i][pp + 1] == '?')
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
                    Affix.CompoundRules[i].Length <= pp
                )
                {
                    return true;
                }
            }

            words[wnum] = null;
            if (w)
            {
                words = null;
            }

            return false;
        }

        /// <summary>
        /// Forbid compounding with neighbouring upper and lower case characters at word bounds.
        /// </summary>
        private static bool CompoundCaseCheck(string word, int pos)
        {
            // NOTE: this implementation could be much simpler but an attempt is made here
            // to preserve the same result when indexes may be out of bounds
            var hasUpper = false;

            if (pos > 0)
            {
                var a = pos - 1;
                if (word[a] == '-')
                {
                    return false;
                }

                if (char.IsUpper(word, a))
                {
                    hasUpper = true;
                }
            }

            if (pos < word.Length)
            {
                if (word[pos] == '-')
                {
                    return false;
                }

                if (!hasUpper && char.IsUpper(word, pos))
                {
                    hasUpper = true;
                }
            }

            return hasUpper;
        }

        /// <summary>
        /// Calculate number of syllable for compound-checking.
        /// </summary>
        private int GetSyllable(string word)
        {
            if (Affix.CompoundMaxSyllable == 0 || !Affix.HasCompoundVowels)
            {
                return 0;
            }

            var num = 0;

            for (var i = 0; i < word.Length; i++)
            {
                if (Affix.CompoundVowels.Contains(word[i]))
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// Forbid compoundings when there are special patterns at word bound.
        /// </summary>
        private bool CompoundPatternCheck(string word, int pos, DictionaryEntry r1, DictionaryEntry r2, bool affixed)
        {
            var wordAfterPos = word.Substring(pos);

            for (var i = 0; i < Affix.CompoundPatterns.Length; i++)
            {
                var patternEntry = Affix.CompoundPatterns[i];
                int len;

                if (
                    StringEx.IsSubset(patternEntry.Pattern2, wordAfterPos)
                    &&
                    (
                        r1 == null
                        ||
                        !patternEntry.Condition.HasValue
                        ||
                        r1.ContainsFlag(patternEntry.Condition)
                    )
                    &&
                    (
                        r2 == null
                        ||
                        !patternEntry.Condition2.HasValue
                        ||
                        r2.ContainsFlag(patternEntry.Condition2)
                    )
                    &&
                    // zero length pattern => only TESTAFF
                    // zero pattern (0/flag) => unmodified stem (zero affixes allowed)
                    (
                        string.IsNullOrEmpty(patternEntry.Pattern)
                        ||
                        (
                            (
                                patternEntry.Pattern.StartsWith('0')
                                && r1.Word.Length <= pos
                                && StringEx.EqualsOffset(word, pos - r1.Word.Length, r1.Word, 0, r1.Word.Length)
                            )
                            ||
                            (
                                !patternEntry.Pattern.StartsWith('0')
                                &&
                                (
                                    (
                                        len = patternEntry.Pattern.Length
                                    ) != 0
                                )
                                &&
                                StringEx.EqualsOffset(word, pos - len, patternEntry.Pattern, 0, len)
                            )
                        )
                    )
                )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Is word a non compound with a REP substitution?
        /// </summary>
        /// <seealso cref="AffixConfig.CheckCompoundRep"/>
        /// <seealso cref="AffixConfig.Replacements"/>
        private bool CompoundReplacementCheck(string word)
        {
            if (word.Length < 2 || !Affix.HasReplacements)
            {
                return false;
            }

            foreach (var replacementEntry in Affix.Replacements)
            {
                // search every occurence of the pattern in the word
                var rIndex = word.IndexOf(replacementEntry.Pattern, StringComparison.Ordinal);
                while (rIndex >= 0)
                {
                    var type = rIndex == 0 ? ReplacementValueType.Isol : ReplacementValueType.Med;
                    var replacement = replacementEntry[type];
                    if (replacement != null && CandidateCheck(word.Replace(rIndex, replacementEntry.Pattern.Length, replacement)))
                    {
                        return true;
                    }

                    rIndex = word.IndexOf(replacementEntry.Pattern, rIndex + 1, StringComparison.Ordinal); // search for the next letter
                }
            }

            return false;
        }

        private DictionaryEntry CheckWordPrefix(AffixEntryGroup<PrefixEntry> group, PrefixEntry entry, string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            // on entry prefix is 0 length or already matches the beginning of the word.
            // So if the remaining root word has positive length
            // and if there are enough chars in root word and added back strip chars
            // to meet the number of characters conditions, then test it

            var tmpl = word.Length - entry.Append.Length; // length of tmpword

            if (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
            {
                // generate new root word by removing prefix and adding
                // back any characters that would have been stripped

                var tmpword = StringEx.ConcatSubstring(entry.Strip, word, entry.Append.Length, word.Length - entry.Append.Length);

                // now make sure all of the conditions on characters
                // are met.  Please see the appendix at the end of
                // this file for more info on exactly what is being
                // tested

                // if all conditions are met then check if resulting
                // root word in the dictionary

                if (TestCondition(entry, tmpword))
                {
                    foreach (var dictionaryEntry in Lookup(tmpword))
                    {
                        if (
                            dictionaryEntry.ContainsFlag(group.AFlag)
                            && !entry.ContainsContClass(Affix.NeedAffix) // forbid single prefixes with needaffix flag
                            &&
                            (
                                !needFlag.HasValue
                                ||
                                dictionaryEntry.ContainsFlag(needFlag)
                                ||
                                entry.ContainsContClass(needFlag)
                            )
                        )
                        {
                            return dictionaryEntry;
                        }
                    }

                    // prefix matched but no root word was found
                    // if aeXPRODUCT is allowed, try again but now
                    // ross checked combined with a suffix

                    if (group.Options.HasFlag(AffixEntryOptions.CrossProduct))
                    {
                        var he = SuffixCheck(tmpword, AffixEntryOptions.CrossProduct, group, entry, new FlagValue(), needFlag, inCompound);
                        if (he != null)
                        {
                            return he;
                        }
                    }

                }
            }

            return null;
        }

        private DictionaryEntry CheckWordSuffix(AffixEntryGroup<SuffixEntry> group, SuffixEntry entry, string word, AffixEntryOptions optFlags, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, FlagValue badFlag)
        {
            // if this suffix is being cross checked with a prefix
            // but it does not support cross products skip it

            if (optFlags.HasFlag(AffixEntryOptions.CrossProduct) && !group.Options.HasFlag(AffixEntryOptions.CrossProduct))
            {
                return null;
            }

            // upon entry suffix is 0 length or already matches the end of the word.
            // So if the remaining root word has positive length
            // and if there are enough chars in root word and added back strip chars
            // to meet the number of characters conditions, then test it

            var tmpl = word.Length - entry.Append.Length;
            // the second condition is not enough for UTF-8 strings
            // it checked in test_condition()

            if (
                (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                &&
                (tmpl + entry.Strip.Length >= entry.Conditions.Count)
            )
            {
                // generate new root word by removing suffix and adding
                // back any characters that would have been stripped or
                // or null terminating the shorter string

                var tmpstring = string.IsNullOrEmpty(entry.Strip)
                    ? word.Substring(0, tmpl)
                    : StringEx.ConcatSubstring(word, 0, tmpl, entry.Strip);

                // now make sure all of the conditions on characters
                // are met.  Please see the appendix at the end of
                // this file for more info on exactly what is being
                // tested

                // if all conditions are met then check if resulting
                // root word in the dictionary

                if (entry.Conditions.IsEndingMatch(tmpstring))
                {
                    foreach (var he in Lookup(tmpstring))
                    {
                        if (
                            (
                                he.ContainsFlag(group.AFlag)
                                ||
                                (pfx != null && pfx.ContainsContClass(group.AFlag))
                            )
                            &&
                            (
                                !optFlags.HasFlag(AffixEntryOptions.CrossProduct)
                                ||
                                (pfx != null && he.ContainsFlag(pfxGroup.AFlag))
                                || // enabled by prefix
                                (pfx != null && entry.ContainsContClass(pfxGroup.AFlag))
                            )
                            && // handle cont. class
                            (cclass == 0 || entry.ContainsContClass(cclass))
                            && // check only in compound homonyms (bad flags)
                            !he.ContainsFlag(badFlag)
                            && // handle required flag
                            (
                                !needFlag.HasValue
                                ||
                                he.ContainsFlag(needFlag)
                                ||
                                entry.ContainsContClass(needFlag)
                            )
                        )
                        {
                            return he;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// See if two-level suffix is present in the word.
        /// </summary>
        private DictionaryEntry CheckTwoSfx(AffixEntryGroup<SuffixEntry> suffixGroup, SuffixEntry se, string word, AffixEntryOptions optflags, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry ppfx, FlagValue needflag)
        {
            // if this suffix is being cross checked with a prefix
            // but it does not support cross products skip it

            if (optflags.HasFlag(AffixEntryOptions.CrossProduct) && !suffixGroup.Options.HasFlag(AffixEntryOptions.CrossProduct))
            {
                return null;
            }

            // upon entry suffix is 0 length or already matches the end of the word.
            // So if the remaining root word has positive length
            // and if there are enough chars in root word and added back strip chars
            // to meet the number of characters conditions, then test it

            var tmpl = word.Length - se.Append.Length; // length of tmpword

            // the second condition is not enough for UTF-8 strings
            // it checked in test_condition()

            if (
                (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                &&
                (tmpl + se.Strip.Length >= se.Conditions.Count)
            )
            {
                // generate new root word by removing suffix and adding
                // back any characters that would have been stripped or
                // or null terminating the shorter string

                var tmpword = StringEx.ConcatSubstring(word, 0, Math.Min(tmpl, word.Length), se.Strip);

                // now make sure all of the conditions on characters
                // are met.  Please see the appendix at the end of
                // this file for more info on exactly what is being
                // tested

                // if all conditions are met then recall suffix_check
                if (TestCondition(se, tmpword))
                {
                    var he = ppfx != null && se.ContainsContClass(pfxGroup.AFlag)
                        // handle conditional suffix
                        ? SuffixCheck(tmpword, AffixEntryOptions.None, null, null, suffixGroup.AFlag, needflag, CompoundOptions.Not)
                        : SuffixCheck(tmpword, optflags, pfxGroup, ppfx, suffixGroup.AFlag, needflag, CompoundOptions.Not);

                    if (he != null)
                    {
                        return he;
                    }
                }
            }

            return null;
        }

        private bool CandidateCheck(string word)
        {
            return !Lookup(word).IsDefaultOrEmpty
                || AffixCheck(word, new FlagValue(), CompoundOptions.Not) != null;
        }

        private bool TestCondition(PrefixEntry entry, string word)
        {
            return entry.Conditions.IsStartingMatch(word);
        }

        private bool TestCondition(SuffixEntry entry, string word)
        {
            return entry.Conditions.IsEndingMatch(word);
        }

        /// <summary>
        /// Make a copy of <paramref name="src"/> at <paramref name="dest"/> while removing all leading
        /// blanks and removing any trailing periods.
        /// </summary>
        /// <param name="dest">The cleaned source text.</param>
        /// <param name="src">The source text to clean and classify.</param>
        /// <param name="capType">The capitalization type the <paramref name="src"/> is classified as.</param>
        /// <param name="abbv">Abbreviation flag indicating the presence of trailing periods.</param>
        /// <returns></returns>
        /// <remarks>
        /// Removes all leading blanks and removes any trailing periods after recording
        /// their presence with the abbreviation flag (<paramref name="abbv"/>)
        /// also since already going through character by character,
        /// set the capitalization type (<paramref name="capType"/>) and
        /// return the length of the "cleaned" (and UTF-8 encoded) word
        /// </remarks>
        private int CleanWord2(out string dest, string src, out CapitalizationType capType, out int abbv)
        {
            // first skip over any leading blanks
            var qIndex = CountMatchingFromLeft(src, ' ');

            // now strip off any trailing periods (recording their presence)
            abbv = CountMatchingFromRight(src, '.');

            var nl = src.Length - qIndex - abbv;

            // if no characters are left it can't be capitalized
            if (nl <= 0)
            {
                dest = string.Empty;
                capType = CapitalizationType.None;
                return 0;
            }

            dest = src.Substring(qIndex, nl);
            capType = CapitalizationTypeEx.GetCapitalizationType(dest, Affix);
            return dest.Length;
        }

        private static int CountMatchingFromLeft(string text, char character)
        {
            var count = 0;
            while (count < text.Length && text[count] == character)
            {
                count++;
            }

            return count;
        }

        private static int CountMatchingFromRight(string text, char character)
        {
            var lastIndex = text.Length - 1;
            var searchIndex = lastIndex;
            while (searchIndex >= 0 && text[searchIndex] == character)
            {
                searchIndex--;
            }

            return lastIndex - searchIndex;
        }

        private static bool IsNumericWord(string word)
        {
            int i;
            byte state = 0; // 0 = begin, 1 = number, 2 = separator
            for (i = 0; i < word.Length; i++)
            {
                var c = word[i];
                if (char.IsNumber(c))
                {
                    state = 1;
                }
                else if (c == ',' || c == '.' || c == '-')
                {
                    if (state == 2 || i == 0)
                    {
                        break;
                    }

                    state = 2;
                }
                else
                {
                    break;
                }
            }

            return i == word.Length && state == 1;
        }
    }
}
