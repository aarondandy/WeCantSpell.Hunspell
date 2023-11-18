using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public partial class WordList
{
    private struct QuerySuggest
    {
        private const int MaxPhoneTLen = 256;
        private const int MaxPhoneTUtf8Len = MaxPhoneTLen * 4;

        public QuerySuggest(WordList wordList, QueryOptions? options, CancellationToken cancellationToken)
        {
            _query = new(wordList, options, cancellationToken);
        }

        internal QuerySuggest(in Query source)
        {
            _query = new(source.WordList, source.Options, source.CancellationToken);
        }

        private Query _query;

        public WordList WordList => _query.WordList;
        public AffixConfig Affix => _query.Affix;
        public TextInfo TextInfo => _query.TextInfo;
        public QueryOptions Options => _query.Options;
        public int MaxCharDistance => Options.MaxCharDistance;
        public int MaxCompoundSuggestions => Options.MaxCompoundSuggestions;
        public int MaxSuggestions => Options.MaxSuggestions;
        public int MaxRoots => Options.MaxRoots;
        public int MaxWords => Options.MaxWords;
        public int MaxGuess => Options.MaxGuess;
        public int MaxPhonSugs => Options.MaxPhoneticSuggestions;

        public List<string> Suggest(string word)
        {
            if (!_query.WordList.HasEntries)
            {
                return new();
            }

            // process XML input of the simplified API (see manual)
            if (word.StartsWith(Query.DefaultXmlTokenCheckPrefix, StringComparison.Ordinal))
            {
                return new(); // TODO: complete support for XML input
            }

            if (word.Length >= MaxWordUtf8Len)
            {
                return new();
            }

            // input conversion
            if (!Affix.InputConversions.HasReplacements || !Affix.InputConversions.TryConvert(word, out var scw))
            {
                scw = word;
            }

            scw = _query.CleanWord2(scw, out var capType, out var abbv);

            if (string.IsNullOrEmpty(scw))
            {
                return new();
            }

            return SuggestInternal(word.AsSpan(), scw, capType, abbv);
        }

        public List<string> Suggest(ReadOnlySpan<char> word)
        {
            if (!_query.WordList.HasEntries)
            {
                return new();
            }

            // process XML input of the simplified API (see manual)
            if (word.StartsWith(Query.DefaultXmlTokenCheckPrefix, StringComparison.Ordinal))
            {
                return new(); // TODO: complete support for XML input
            }

            if (word.Length >= MaxWordUtf8Len)
            {
                return new();
            }

            // input conversion
            CapitalizationType capType;
            int abbv;
            if (Affix.InputConversions.HasReplacements && Affix.InputConversions.TryConvert(word, out var scw))
            {
                scw = _query.CleanWord2(scw, out capType, out abbv);
            }
            else
            {
                scw = _query.CleanWord2(word, out capType, out abbv);
            }

            if (string.IsNullOrEmpty(scw))
            {
                return new();
            }

            return SuggestInternal(word, scw, capType, abbv);
        }

        public List<string> SuggestInternal(ReadOnlySpan<char> word, string scw, CapitalizationType capType, int abbv)
        {
            var slst = new List<string>();

            var opLimiter = new OperationTimedLimiter(Options.TimeLimitSuggestGlobal, _query.CancellationToken);

            var textInfo = _query.TextInfo;

            // check capitalized form for FORCEUCASE
            if (capType == CapitalizationType.None && Affix.ForceUpperCase.HasValue)
            {
                var info = SpellCheckResultType.OrigCap;
                if (_query.CheckWord(scw, ref info, out _) is not null)
                {
                    slst.Add(HunspellTextFunctions.MakeInitCap(scw, textInfo));
                    return slst;
                }
            }

            var onlyCompoundSuggest = false;
            var capWords = false;
            var good = false;

            if (capType == CapitalizationType.None)
            {
                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
                {
                    return slst;
                }

                if (abbv != 0)
                {
                    var wspace = scw + ".";
                    good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                    if (opLimiter.QueryForCancellation())
                    {
                        return slst;
                    }
                }
            }
            else if (capType == CapitalizationType.Init)
            {
                capWords = true;
                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
                {
                    return slst;
                }

                good |= Suggest(slst, HunspellTextFunctions.MakeAllSmall(scw, textInfo), ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
                {
                    return slst;
                }
            }
            else if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit)
            {
                capWords = capType == CapitalizationType.HuhInit;

                good |= Suggest(slst, scw, ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
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

                    if (opLimiter.QueryForCancellation())
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

                if (opLimiter.QueryForCancellation())
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

                    if (opLimiter.QueryForCancellation())
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
                            removeFromIndexThenInsertAtFront(
                                slst,
                                j,
                                StringEx.ConcatString(
                                    toRemove.AsSpan(0, spaceIndex + 1),
                                    HunspellTextFunctions.MakeInitCap(toRemove.AsSpan(spaceIndex + 1), textInfo)));

                            static void removeFromIndexThenInsertAtFront(List<string> list, int removeIndex, string insertValue)
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
                        }
                    }
                }
            }
            else if (capType == CapitalizationType.All)
            {
                var wspace = HunspellTextFunctions.MakeAllSmall(scw, textInfo);
                good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
                {
                    return slst;
                }

                if (Affix.KeepCase.HasValue && Check(wspace))
                {
                    InsertSuggestion(slst, wspace);
                }

                wspace = HunspellTextFunctions.MakeInitCap(wspace, textInfo);
                good |= Suggest(slst, wspace, ref onlyCompoundSuggest);

                if (opLimiter.QueryForCancellation())
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
                        var desiredChar = EnumEx.HasFlag(CheckDetails(sitem.Remove(pos, 1)).Info, SpellCheckResultType.Compound | SpellCheckResultType.Forbidden)
                            ? ' '
                            : '-';

                        if (sitem[pos] != desiredChar)
                        {
                            slst[j] = StringEx.ConcatString(sitem.AsSpan(0, pos), desiredChar, sitem.AsSpan(pos + 1));
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

                if (opLimiter.QueryForCancellation())
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

                    var chunk = scw.AsSpan(prevPos, dashPos - prevPos);
                    if (!chunk.EqualsOrdinal(word) && !Check(chunk))
                    {
                        var nlst = SuggestNested(chunk);

                        foreach (var j in nlst)
                        {
                            var wspace = last
                                ? scw.AsSpan(0, prevPos).ConcatString(j)
                                : StringEx.ConcatString(scw.AsSpan(0, prevPos), j, '-', scw.AsSpan(dashPos + 1));

                            var info = SpellCheckResultType.None;
                            if (Affix.ForbiddenWord.HasValue)
                            {
                                _ = _query.CheckWord(wspace, ref info, out _);
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
                    slst[j] = slst[j].ConcatString(word.Slice(word.Length - abbv));
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
                    if (Affix.OutputConversions.TryConvert(slst[j], out var wspace))
                    {
                        slst[j] = wspace;
                    }
                }
            }

            return slst;
        }

        private List<string> SuggestNested(ReadOnlySpan<char> word) => new QuerySuggest(in _query).Suggest(word);

        private bool Check(string word) => new QueryCheck(in _query).Check(word);

        private bool Check(ReadOnlySpan<char> word) => new QueryCheck(in _query).Check(word);

        private WordEntryDetail? LookupFirstDetail(ReadOnlySpan<char> word) => WordList.FindFirstEntryDetailByRootWord(word);
        
        private bool TryLookupFirstDetail(ReadOnlySpan<char> word, out WordEntryDetail wordEntryDetail) => WordList.TryFindFirstEntryDetailByRootWord(word, out wordEntryDetail);

        private bool TryLookupFirstDetail(string word, out WordEntryDetail wordEntryDetail) => WordList.TryFindFirstEntryDetailByRootWord(word, out wordEntryDetail);

        private ref struct SuggestState
        {
            public SuggestState(string word, List<string> slst)
            {
                Word = word.AsSpan();
                _rawCandidateBuffer = ArrayPool<char>.Shared.Rent(word.Length + 2);
                CandidateBuffer = _rawCandidateBuffer.AsSpan(0, word.Length + 2);
                SuggestionList = slst;
                CpdSuggest = false;
                GoodSuggestion = false;
            }

            public ReadOnlySpan<char> Word;
            public Span<char> CandidateBuffer;
            public bool CpdSuggest;
            public bool GoodSuggestion;
            public List<string> SuggestionList;
            private char[] _rawCandidateBuffer;

            public Span<char> GetBufferForWord()
            {
                var result = CandidateBuffer.Slice(0, Word.Length);
                Word.CopyTo(result);
                return result;
            }

            public void DestroyBuffer()
            {
                if (_rawCandidateBuffer.Length != 0)
                {
                    ArrayPool<char>.Shared.Return(_rawCandidateBuffer);
                    _rawCandidateBuffer = Array.Empty<char>();
                    CandidateBuffer = Span<char>.Empty;
                }
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

            // word reversing wrapper for complex prefixes
            if (Affix.ComplexPrefixes)
            {
                word = word.GetReversed();
            }

            var opLimiter = new OperationTimedLimiter(Options.TimeLimitCompoundSuggest, _query.CancellationToken);

            var state = new SuggestState(word, slst);

            do
            {
                // limit compound suggestion
                opLimiter.Reset();

                if (state.CpdSuggest)
                {
                    oldSug = slst.Count;
                }

                var sugLimit = oldSug + MaxCompoundSuggestions;

                // suggestions for an uppercase word (html -> HTML)
                if (slst.Count < MaxSuggestions)
                {
                    var i = slst.Count;
                    CapChars(slst, word, state.CpdSuggest);
                    if (slst.Count > i)
                    {
                        state.GoodSuggestion = true;
                    }
                }

                // perhaps we made a typical fault of spelling
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    var i = slst.Count;
                    ReplChars(slst, word, state.CpdSuggest);
                    if (slst.Count > i)
                    {
                        state.GoodSuggestion = true;
                    }
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // perhaps we made chose the wrong char from a related set
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    MapChars(slst, word, state.CpdSuggest);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // only suggest compound words when no other suggestion
                if (!state.CpdSuggest && slst.Count > nSugOrig)
                {
                    noCompoundTwoWords = true;
                }

                // did we swap the order of chars by mistake
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    SwapChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we swap the order of non adjacent chars by mistake
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    LongSwapChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we just hit the wrong key in place of a good char (case and keyboard)
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    BadCharKey(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we add a char that should not be there
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    ExtraChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we forgot a char
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    ForgotChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we move a char
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    MoveChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we just hit the wrong key in place of a good char
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    BadChar(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // did we double two characters
                if (slst.Count < MaxSuggestions && (!state.CpdSuggest || slst.Count < sugLimit))
                {
                    DoubleTwoChars(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }

                // perhaps we forgot to hit space and two words ran together
                // (dictionary word pairs have top priority here, so
                // we always suggest them, in despite of nosplitsugs, and
                // drop compound word and other suggestions)
                //if (!Affix.NoSplitSuggestions && slst.Count < MaxSuggestions && (!cpdSuggest || slst.Count < sugLimit))
                if (!state.CpdSuggest || (!Affix.NoSplitSuggestions && slst.Count < sugLimit))
                {
                    TwoWords(ref state);
                }

                if (opLimiter.QueryForCancellation())
                {
                    goto timerExit;
                }
            }
            while (!noCompoundTwoWords && IntEx.InversePostfixIncrement(ref state.CpdSuggest) && !state.GoodSuggestion);

            if (!noCompoundTwoWords && slst.Count != 0)
            {
                onlyCompoundSug = true;
            }

        timerExit:

            state.DestroyBuffer();

            return state.GoodSuggestion;
        }

        private SpellCheckResult CheckDetails(string word) => new QueryCheck(in _query).CheckDetails(word);

        /// <summary>
        /// perhaps we doubled two characters (pattern aba -> ababa, for example vacation -> vacacation)
        /// </summary>
        /// <remarks>
        /// (for example vacation -> vacacation)
        /// The recognized pattern with regex back-references:
        /// "(.)(.)\1\2\1" or "..(.)(.)\1\2"
        /// </remarks>
        private void DoubleTwoChars(ref SuggestState sugState)
        {
            var word = sugState.Word;
            if (word.Length < 5)
            {
                return;
            }

            var state = 0;
            var candidate = sugState.CandidateBuffer.Slice(0, word.Length - 2);
            for (var i = 2; i < word.Length; i++)
            {
                if (word[i] == word[i - 2])
                {
                    state++;
                    if (state == 3 || (state == 2 && i >= 4))
                    {
                        word.Slice(0, i - 1).CopyTo(candidate);
                        word.Slice(i + 1).CopyTo(candidate.Slice(i - 1));

                        TestSug(sugState.SuggestionList, candidate, sugState.CpdSuggest);
                        state = 0;
                    }
                }
                else
                {
                    state = 0;
                }
            }
        }

        /// <summary>
        /// Error is wrong char in place of correct one.
        /// </summary>
        private void BadChar(ref SuggestState state)
        {
            if (Affix.TryString is { Length: > 0 } tryString)
            {
                var timer = new OperationTimedCountLimiter(Options.TimeLimitSuggestStep, Options.MinTimer, _query.CancellationToken);

                var candidate = state.GetBufferForWord();

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
                        TestSug(state.SuggestionList, candidate, state.CpdSuggest, timer);
                        candidate[i] = tmpc;

                        if (timer.HasBeenCanceled)
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Error is a letter was moved.
        /// </summary>
        private void MoveChar(ref SuggestState state)
        {
            var word = state.Word;
            if (word.Length < 2)
            {
                return;
            }

            var candidate = state.GetBufferForWord();

            // try moving a char
            for (var p = 0; p < word.Length; p++)
            {
                word.CopyTo(candidate);

                var qMax = Math.Min(MaxCharDistance + p + 1, candidate.Length);
                for (var q = p + 1; q < qMax; q++)
                {
                    candidate.Swap(q, q - 1);

                    if (q - p < 2)
                    {
                        continue; // omit swap char
                    }

                    TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                }
            }

            for (var p = word.Length - 1; p >= 0; p--)
            {
                word.CopyTo(candidate);

                var qMin = Math.Max(p - MaxCharDistance, 0);
                for (var q = p - 1; q >= qMin; q--)
                {
                    candidate.Swap(q, q + 1);

                    if (p - q < 2)
                    {
                        continue;  // omit swap char
                    }

                    TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                }
            }
        }

        /// <summary>
        /// Error is missing a letter it needs.
        /// </summary>
        private void ForgotChar(ref SuggestState state)
        {
            if (Affix.TryString is { Length: > 0 })
            {
                var timer = new OperationTimedCountLimiter(Options.TimeLimitSuggestStep, Options.MinTimer, _query.CancellationToken);

                var word = state.Word;
                var candidate = state.CandidateBuffer.Slice(0, word.Length + 1);

                // try inserting a tryme character before every letter (and the null terminator)
                foreach (var tryChar in Affix.TryString)
                {
                    word.CopyTo(candidate);
                    candidate[word.Length] = tryChar;

                    TestSug(state.SuggestionList, candidate, state.CpdSuggest, timer);

                    if (timer.HasBeenCanceled)
                    {
                        return;
                    }

                    for (var index = word.Length; index >= 0; index--)
                    {
                        candidate[index] = tryChar;
                        word.Slice(index).CopyTo(candidate.Slice(index + 1));

                        TestSug(state.SuggestionList, candidate, state.CpdSuggest, timer);

                        if (timer.HasBeenCanceled)
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Error is word has an extra letter it does not need.
        /// </summary>
        private void ExtraChar(ref SuggestState state)
        {
            var word = state.Word;

            if (word.Length >= 2)
            {
                var buffer = state.CandidateBuffer.Slice(0, word.Length - 1);
                word.Slice(0, word.Length - 1).CopyTo(buffer);

                TestSug(state.SuggestionList, buffer, state.CpdSuggest);

                for (var index = word.Length - 2; index > 0; index--)
                {
                    word.Slice(index + 1).CopyTo(buffer.Slice(index));
                    TestSug(state.SuggestionList, buffer, state.CpdSuggest);
                }

                TestSug(state.SuggestionList, word.Slice(1), state.CpdSuggest);
            }
        }

        /// <summary>
        /// error is wrong char in place of correct one (case and keyboard related version)
        /// </summary>
        private void BadCharKey(ref SuggestState state)
        {
            var candidate = state.GetBufferForWord();
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
                    TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                    candidate[i] = tmpc;
                }

                // check neighbor characters in keyboard string
                var loc = keyString.IndexOf(tmpc);
                while (loc >= 0)
                {
                    if (loc > 0 && keyString[loc - 1] != '|')
                    {
                        candidate[i] = keyString[loc - 1];
                        TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                    }

                    if ((loc + 1) < keyString.Length && keyString[loc + 1] != '|')
                    {
                        candidate[i] = keyString[loc + 1];
                        TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                    }

                    loc = keyString.IndexOf(tmpc, loc + 1);
                }

                candidate[i] = tmpc;
            }
        }

        /// <summary>
        /// Error is not adjacent letter were swapped.
        /// </summary>
        private void LongSwapChar(ref SuggestState state)
        {
            var candidate = state.GetBufferForWord();

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

                        TestSug(state.SuggestionList, candidate, state.CpdSuggest);

                        candidate[q] = oldq;
                        candidate[p] = oldp;
                    }
                }
            }
        }

        /// <summary>
        /// Error is adjacent letter were swapped.
        /// </summary>
        private void SwapChar(ref SuggestState state)
        {
            var word = state.Word;
            if (word.Length < 2)
            {
                return;
            }

            var candidate = state.GetBufferForWord();

            // try swapping adjacent chars one by one
            for (var i = 1; i < candidate.Length; i++)
            {
                var c = candidate[i];
                candidate[i] = candidate[i - 1];
                candidate[i - 1] = c;
                TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                candidate[i - 1] = candidate[i];
                candidate[i] = c;
            }

            // try double swaps for short words
            // ahev -> have, owudl -> would
            if (candidate.Length == 4 || candidate.Length == 5)
            {
                candidate[0] = word[1];
                candidate[1] = word[0];
                candidate[2] = word[2];
                candidate[candidate.Length - 2] = word[word.Length - 1];
                candidate[candidate.Length - 1] = word[word.Length - 2];

                TestSug(state.SuggestionList, candidate, state.CpdSuggest);

                if (candidate.Length == 5)
                {
                    candidate[0] = word[0];
                    candidate[1] = word[2];
                    candidate[2] = word[1];

                    TestSug(state.SuggestionList, candidate, state.CpdSuggest);
                }
            }
        }

        private void CapChars(List<string> wlst, string word, bool cpdSuggest) =>
            TestSug(wlst, HunspellTextFunctions.MakeAllCap(word, TextInfo), cpdSuggest);

        private void MapChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2 || Affix.RelatedCharacterMap.IsEmpty)
            {
                return;
            }

            var candidate = string.Empty;
            var timer = new OperationTimedCountLimiter(Options.TimeLimitSuggestStep, Options.MinTimer, _query.CancellationToken);
            MapRelated(word, ref candidate, wn: 0, wlst, cpdSuggest, timer, 0);
        }

        private void MapRelated(string word, ref string candidate, int wn, List<string> wlst, bool cpdSuggest, OperationTimedCountLimiter timer, int depth)
        {
            if (wn >= word.Length)
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

                return;
            }

            if (depth > RecursiveDepthLimit)
            {
                return;
            }

            var inMap = false;
            foreach (var mapEntry in Affix.RelatedCharacterMap.GetInternalArray())
            {
                foreach (var mapEntryValue in mapEntry.GetInternalArray())
                {
                    if (word.AsSpan(wn).StartsWith(mapEntryValue, StringComparison.Ordinal))
                    {
                        inMap = true;
                        var candidatePrefix = candidate;
                        foreach (var otherMapEntryValue in mapEntry.GetInternalArray())
                        {
                            candidate = candidatePrefix + otherMapEntryValue;
                            MapRelated(word, ref candidate, wn + mapEntryValue.Length, wlst, cpdSuggest, timer, depth + 1);

                            if (timer.HasBeenCanceled)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            if (!inMap)
            {
                candidate += word[wn];
                MapRelated(word, ref candidate, wn + 1, wlst, cpdSuggest, timer, depth + 1);
            }
        }

        private void TestSug(List<string> wlst, ReadOnlySpan<char> candidate, bool cpdSuggest, OperationTimedCountLimiter timer)
        {
            if (
                wlst.Count < MaxSuggestions
                &&
                !wlst.Contains(candidate)
                &&
                CheckWord(candidate, cpdSuggest, timer) != 0
            )
            {
                wlst.Add(candidate.ToString());
            }
        }

        private void TestSug(List<string> wlst, string candidate, bool cpdSuggest)
        {
            if (
                wlst.Count < MaxSuggestions
                &&
                !wlst.Contains(candidate)
                &&
                CheckWord(candidate, cpdSuggest) != 0
            )
            {
                wlst.Add(candidate);
            }
        }

        private void TestSug(List<string> wlst, ReadOnlySpan<char> candidate, bool cpdSuggest)
        {
            if (
                wlst.Count < MaxSuggestions
                &&
                !wlst.Contains(candidate)
                &&
                CheckWord(candidate, cpdSuggest) != 0
            )
            {
                wlst.Add(candidate.ToString());
            }
        }

        private byte CheckWord(string word, bool cpdSuggest, OperationTimedCountLimiter timer)
        {
            if (timer.QueryForCancellation())
            {
                return 0;
            }

            return CheckWord(word, cpdSuggest);
        }

        private byte CheckWord(ReadOnlySpan<char> word, bool cpdSuggest, OperationTimedCountLimiter timer)
        {
            if (timer.QueryForCancellation())
            {
                return 0;
            }

            return CheckWord(word, cpdSuggest);
        }

        private static WordEntry? CheckWordHomonymPortion(string word, WordEntryDetail[] rvDetails, AffixConfig affix)
        {
#if DEBUG
            if (rvDetails.Length <= 0) throw new ArgumentOutOfRangeException(nameof(rvDetails));
#endif

            WordEntryDetail rvDetail;
            var rvIndex = 0;
            do
            {
                rvDetail = rvDetails[rvIndex];
                if (!rvDetail.ContainsAnyFlags(affix.NeedAffix, SpecialFlags.OnlyUpcaseFlag, affix.OnlyInCompound))
                {
                    break;
                }

                rvIndex++;
            }
            while (rvIndex < rvDetails.Length);

            if (rvIndex >= rvDetails.Length)
            {
                return null;
            }

            return new WordEntry(word, rvDetail);
        }

        private byte CheckWordAffixPortion(WordEntry? rv, ReadOnlySpan<char> word)
        {
            var noSuffix = rv is not null;
            if (!noSuffix)
            {
                rv = _query.SuffixCheck(word, AffixEntryOptions.None, null, default, default, CompoundOptions.Not); // only suffix
            }

            if (Affix.ContClasses.HasItems && rv is null)
            {
                rv = _query.SuffixCheckTwoSfx(word, AffixEntryOptions.None, null, default)
                    ?? _query.PrefixCheckTwoSfx(word, CompoundOptions.Not, default);
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
                    return noSuffix ? (byte)3 : (byte)2;
                }

                return 1;
            }

            return 0;
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
        private byte CheckWord(ReadOnlySpan<char> word, bool cpdSuggest)
        {
            WordEntry? rv;
            if (cpdSuggest)
            {
                if (Affix.HasCompound)
                {
                    var rwords = new IncrementalWordList(); // buffer for COMPOUND pattern checking
                    var info = SpellCheckResultType.None;
                    rv = _query.CompoundCheck(word, 0, 0, 100, null, rwords, huMovRule: false, isSug: true, ref info);
                    if (rv is not null && (!TryLookupFirstDetail(word, out var rvDetail) || !rvDetail.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest)))
                    {
                        return 3; // XXX obsolote categorisation + only ICONV needs affix flag check?
                    }
                }

                return 0;
            }

            // get homonyms
            if (_query.TryLookupDetails(word, out var wordString, out var rvDetails) && rvDetails is { Length: > 0 })
            {
                if (rvDetails[0].ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest, Affix.SubStandard))
                {
                    return 0;
                }

                rv = CheckWordHomonymPortion(wordString, rvDetails, Affix);
            }
            else
            {
                rv = _query.PrefixCheck(word, CompoundOptions.Not, default); // only prefix, and prefix + suffix XXX
            }

            return CheckWordAffixPortion(rv, word);
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
        private byte CheckWord(string word, bool cpdSuggest)
        {
            WordEntry? rv;
            if (cpdSuggest)
            {
                if (Affix.HasCompound)
                {
                    var rwords = new IncrementalWordList(); // buffer for COMPOUND pattern checking
                    var info = SpellCheckResultType.None;
                    rv = _query.CompoundCheck(word.AsSpan(), 0, 0, 100, null, rwords, huMovRule: false, isSug: true, ref info);
                    if (rv is not null && (!TryLookupFirstDetail(word, out var rvDetail) || !rvDetail.ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest)))
                    {
                        return 3; // XXX obsolote categorisation + only ICONV needs affix flag check?
                    }
                }

                return 0;
            }

            // get homonyms
            if (_query.TryLookupDetails(word, out var rvDetails) && rvDetails is { Length: > 0 })
            {
                if (rvDetails[0].ContainsAnyFlags(Affix.ForbiddenWord, Affix.NoSuggest, Affix.SubStandard))
                {
                    return 0;
                }

                rv = CheckWordHomonymPortion(word, rvDetails, Affix);
            }
            else
            {
                rv = _query.PrefixCheck(word.AsSpan(), CompoundOptions.Not, default); // only prefix, and prefix + suffix XXX
            }

            return CheckWordAffixPortion(rv, word.AsSpan());
        }

        /// <summary>
        /// Suggestions for a typical fault of spelling, that
        /// differs with more, than 1 letter from the right form.
        /// </summary>
        private void ReplChars(List<string> wlst, string word, bool cpdSuggest)
        {
            if (word.Length < 2 || WordList.AllReplacements.IsEmpty)
            {
                return;
            }

            foreach (var replacement in WordList.AllReplacements.GetInternalArray())
            {
                if (replacement.Pattern.Length == 0)
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
                    if ((replacement.Pattern.Length + r) == word.Length)
                    {
                        type |= ReplacementValueType.Fin;
                    }

                    while (type != ReplacementValueType.Med && replacement[type] is not { Length: > 0 })
                    {
                        type = (type == ReplacementValueType.Fin && r != 0) ? ReplacementValueType.Med : type - 1;
                    }

                    if (replacement[type] is { Length: > 0 } replacementValue)
                    {
                        var candidate = StringEx.ConcatString(word.AsSpan(0, r), replacementValue, word.AsSpan(r + replacement.Pattern.Length));

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

            return;
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

            var rootsRental = ArrayPool<NGramSuggestSearchRoot>.Shared.Rent(MaxRoots);
            var roots = rootsRental.AsSpan(0, MaxRoots);

            for (var i = 0; i < roots.Length; i++)
            {
                roots[i] = new(i);
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

            foreach (var hpSet in WordList.GetNGramAllowedDetailsByKeyLength(
                // skip it, if the word length different by 5 or
                // more characters (to avoid strange suggestions)
                minKeyLength: Math.Max(word.Length - 4, 0),
                maxKeyLength: word.Length + 4)
            )
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
                    if (isNonGermanLowercase && !hasPhoneEntries && ((hpDetail.Options & (WordEntryOptions.InitCap | WordEntryOptions.Phon)) == WordEntryOptions.InitCap))
                    {
                        continue;
                    }

                    sc = NGram(3, word, hpSet.Key, NGramOptions.LongerWorse | NGramOptions.Lowering)
                        + LeftCommonSubstring(word, hpSet.Key);

                    // check special pronunciation
                    var f = string.Empty;
                    if (EnumEx.HasFlag(hpDetail.Options, WordEntryOptions.Phon) && CopyField(ref f, hpDetail.Morphs, MorphologicalTags.Phon))
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
            var guessesRental = ArrayPool<NGramGuess>.Shared.Rent(MaxGuess);
            var guesses = guessesRental.AsSpan(0, MaxGuess);
            for (var i = 0; i < guesses.Length; i++)
            {
                guesses[i] = new(i);
            }

            lp = guesses.Length - 1;

            var glstRental = ArrayPool<GuessWord>.Shared.Rent(MaxWords);
            var glst = glstRental.AsSpan(0, MaxWords);
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

                    var nw = ExpandRootWord(glst, rp, word, field);

                    for (var k = 0; k < nw; k++)
                    {
                        ref var guessWordK = ref glst[k];

                        if (guessWordK.Word is { Length: > 0 })
                        {
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

                                    continue;
                                }
                            }
                        }

                        guessWordK.ClearWordAndOrig();
                    }
                }
            }

            ArrayPool<GuessWord>.Shared.Return(glstRental);
            glstRental = null!;
            glst = Span<GuessWord>.Empty;

            // now we are done generating guesses
            // sort in order of decreasing score

#if NO_SPAN_COMPARISON_SORT
            Array.Sort(guessesRental, 0, guesses.Length, NGramGuess.ScoreComparer.Default);

            if (hasPhoneEntries)
            {
                Array.Sort(rootsRental, 0, roots.Length, NGramSuggestSearchRoot.ScorePhoneComparer.Default);
            }

#else
            guesses.Sort(NGramGuess.ScoreComparer.Comparison);

            if (hasPhoneEntries)
            {
                roots.Sort(NGramSuggestSearchRoot.ScorePhoneComparer.Comparison);
            }
#endif

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
                    var gl = textInfo.ToLower(guess.Guess);
                    var len = guess.Guess.Length;

                    var lcsLength = LcsLen(word.AsSpan(), gl.AsSpan());

                    // same characters with different casing
                    if (word.Length == len && word.Length == lcsLength)
                    {
                        guesses[i].Score += 2000;
                        break;
                    }

                    // using 2-gram instead of 3, and other weightening
                    var re = NGram(2, word, gl, NGramOptions.AnyMismatch | NGramOptions.Weighted) // gl has already been lowered
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
                        + NGram(4, word, gl, NGramOptions.AnyMismatch) // gl has already been lowered
                        // weighted ngrams
                        + re
                        // different limit for dictionaries with PHONE rules
                        + (hasPhoneEntries ? (re < len * fact ? -1000 : 0) : (re < (word.Length + len) * fact ? -1000 : 0));
                }
            }

#if NO_SPAN_COMPARISON_SORT
            Array.Sort(guessesRental, 0, guesses.Length, NGramGuess.ScoreComparer.Default);
#else
            guesses.Sort(NGramGuess.ScoreComparer.Comparison);
#endif

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
                        roots[i].ScorePhone += 2 * LcsLen(word.AsSpan(), gl.AsSpan()) - Math.Abs(word.Length - len)
                            // weight length of the left common substring
                            + LeftCommonSubstring(word, gl);
                    }
                }

#if NO_SPAN_COMPARISON_SORT
                Array.Sort(rootsRental, 0, roots.Length, NGramSuggestSearchRoot.ScorePhoneComparer.Default);
#else
                roots.Sort(NGramSuggestSearchRoot.ScorePhoneComparer.Comparison);
#endif
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
                                (guess.GuessOrig ?? guess.Guess).Contains(wlst[j])
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

            ArrayPool<NGramGuess>.Shared.Return(guessesRental);
            guessesRental = null!;
            guesses = Span<NGramGuess>.Empty;

            oldns = wlst.Count;
            wlstLimit = Math.Min(MaxSuggestions, oldns + MaxPhonSugs);

            if (hasPhoneEntries)
            {
                for (var i = 0; i < roots.Length; i++)
                {
                    if (roots[i].RootPhon is { } rootPhon && wlst.Count < wlstLimit)
                    {
                        var unique = true;
                        for (var j = 0; j < wlst.Count; j++)
                        {
                            // don't suggest previous suggestions or a previous suggestion with
                            // prefixes or affixes
                            if (
                                rootPhon.Contains(wlst[j])
                                || // check forbidden words
                                CheckWord(rootPhon, false) == 0
                            )
                            {
                                unique = false;
                                break;
                            }
                        }

                        if (unique)
                        {
                            wlst.Add(rootPhon);
                        }
                    }
                }
            }

            ArrayPool<NGramSuggestSearchRoot>.Shared.Return(rootsRental);
            rootsRental = null!;
            roots = Span<NGramSuggestSearchRoot>.Empty;
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

        /// <summary>
        /// Longest common subsequence.
        /// </summary>
        private static int LcsLen(ReadOnlySpan<char> s, ReadOnlySpan<char> s2)
        {
            var lcsLength = 0;
            var matchCount = s.CountMatchesFromLeft(s2);
            if (matchCount > 0)
            {
                lcsLength = matchCount;
                s = s.Slice(matchCount);
                s2 = s2.Slice(matchCount);
            }

            if (s.Length > 1 || s2.Length > 1)
            {
                matchCount = s.CountMatchesFromRight(s2);
                if (matchCount > 0)
                {
                    lcsLength += matchCount;
                    s = s.Slice(0, s.Length - matchCount);
                    s2 = s2.Slice(0, s2.Length - matchCount);
                }

                if (s.Length > 1 || s2.Length > 1)
                {
                    lcsLength += lcsAlgorithm(s, s2);
                }
            }

            return lcsLength;

            static int lcsAlgorithm(ReadOnlySpan<char> s, ReadOnlySpan<char> s2)
            {
                var nNext = s2.Length + 1;
                var requiredCapacity = (s.Length + 1) * nNext;

                var c = ArrayPool<LongestCommonSubsequenceType>.Shared.Rent(requiredCapacity);
                Array.Clear(c, 0, requiredCapacity);
                var b = ArrayPool<LongestCommonSubsequenceType>.Shared.Rent(requiredCapacity);
                Array.Clear(b, 0, requiredCapacity);

                int i, j;
                for (i = 1; i <= s.Length; i++)
                {
                    var iPrev = i - 1;
                    for (j = 1; j <= s2.Length; j++)
                    {
                        var inj = (i * nNext) + j;
                        ref var cInj = ref c[inj];
                        ref var bInj = ref b[inj];

                        var jPrev = j - 1;
                        var iPrevXNNext = iPrev * nNext;
                        if (s[iPrev] == s2[jPrev])
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

                ArrayPool<LongestCommonSubsequenceType>.Shared.Return(c);

                i = s.Length;
                j = s2.Length;
                var len = 0;
                while (i > 0 && j > 0)
                {
                    switch (b[(i * nNext) + j])
                    {
                        case LongestCommonSubsequenceType.UpLeft:
                            len++;
                            i--;
                            j--;
                            break;
                        case LongestCommonSubsequenceType.Up:
                            i--;
                            break;
                        default:
                            j--;
                            break;
                    }
                }

                ArrayPool<LongestCommonSubsequenceType>.Shared.Return(b);

                return len;
            }
        }

        private int ExpandRootWord(Span<GuessWord> wlst, WordEntry entry, string bad, string? phon)
        {
            if (wlst.IsEmpty)
            {
                return 0;
            }

            ref var wlstNh = ref wlst[0];

            var nh = 0;
            // first add root word to list
            if (nh < wlst.Length && !entry.Detail.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound))
            {
                wlstNh = ref wlst[nh];

                wlstNh.Word = entry.Word;
                if (wlstNh.Word is null)
                {
                    return 0;
                }

                wlstNh.Allow = false;
                wlstNh.Orig = null;

                nh++;

                // add special phonetic version
                if (phon is not null && nh < wlst.Length)
                {
                    wlstNh = ref wlst[nh];

                    wlstNh.Word = phon;
                    if (wlstNh.Word is null)
                    {
                        return nh - 1;
                    }

                    wlstNh.Allow = false;
                    wlstNh.Orig = entry.Word;
                    if (wlstNh.Orig is null)
                    {
                        return nh - 1;
                    }

                    nh++;
                }
            }

            // handle suffixes
            foreach (var sptr in Affix.Suffixes.GetByFlags(entry.Detail.Flags))
            {
                if (
                    (
                        sptr.Append.Length == 0
                        ||
                        (
                            bad.Length > sptr.Append.Length
                            &&
                            bad.EndsWith(sptr.Append, StringComparison.Ordinal)
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
                            wlstNh = ref wlst[nh];
                            wlstNh.Word = newword;
                            wlstNh.Allow = sptr.Options.AllowCross();
                            wlstNh.Orig = null;

                            nh++;

                            // add special phonetic version
                            if (phon is not null && nh < wlst.Length)
                            {
                                wlstNh = ref wlst[nh];
                                wlstNh.Word = phon + sptr.Append;
                                if (wlstNh.Word is null)
                                {
                                    return nh - 1;
                                }

                                wlstNh.Allow = false;
                                wlstNh.Orig = newword;
                                if (wlstNh.Orig is null)
                                {
                                    return nh - 1;
                                }

                                nh++;
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

                    foreach (var pfxGroup in Affix.Prefixes.GetGroupsByFlags(entry.Detail.Flags))
                    {
                        if (pfxGroup.Options.AllowCross())
                        {
                            foreach (var cptr in pfxGroup.Entries)
                            {
                                if (
                                    cptr.Append.Length == 0
                                    ||
                                    (
                                        bad.Length > cptr.Append.Length
                                        &&
                                        bad.StartsWith(cptr.Append, StringComparison.Ordinal)
                                    )
                                )
                                {
                                    if (Add(cptr, wlst[j].Word ?? string.Empty) is { Length: > 0 } newword)
                                    {
                                        if (nh < wlst.Length)
                                        {
                                            wlstNh = ref wlst[nh];
                                            wlstNh.Word = newword;
                                            wlstNh.Allow = pfxGroup.Options.AllowCross();
                                            wlstNh.Orig = null;
                                        }
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
                foreach (var ptr in Affix.Prefixes.GetByFlags(entry.Detail.Flags))
                {
                    if (
                        (
                            ptr.Append.Length == 0
                            ||
                            (
                                bad.Length > ptr.Append.Length
                                &&
                                bad.StartsWith(ptr.Append, StringComparison.Ordinal)
                            )
                        )
                        && // check needaffix flag
                        !ptr.ContainsAnyContClass(Affix.NeedAffix, Affix.Circumfix, Affix.OnlyInCompound)
                    )
                    {
                        var newword = Add(ptr, entry.Word);
                        if (newword.Length != 0 && nh < wlst.Length)
                        {
                            wlstNh = ref wlst[nh];
                            wlstNh.Word = newword;
                            wlstNh.Allow = ptr.Options.AllowCross();
                            wlstNh.Orig = null;
                            nh++;
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
        private void TwoWords(ref SuggestState state)
        {
            ref var good = ref state.GoodSuggestion;
            var word = state.Word;
            var wlst = state.SuggestionList;
            var cpdSuggest = state.CpdSuggest;

            if (word.Length < 3)
            {
                good = false;
                return;
            }

            var isHungarianAndNotForbidden = Affix.IsHungarian && !CheckForbidden(word);

            var candidate = new SimulatedCString(word.Length + 2);
            candidate[0] = '\0';
            candidate.WriteChars(word, 1);
            candidate[word.Length + 1] = '\0';

            // split the string into two pieces after every char
            // if both pieces are good words make them a suggestion

            for (var p = 1; p + 1 < candidate.BufferLength; p++)
            {
                candidate[p - 1] = candidate[p];

                // Suggest only word pairs, if they are listed in the dictionary.
                // For example, adding "a lot" to the English dic file will
                // result only "alot" -> "a lot" suggestion instead of
                // "alto, slot, alt, lot, allot, aloft, aloe, clot, plot, blot, a lot".
                // Note: using "ph:alot" keeps the other suggestions:
                // a lot ph:alot
                // alot -> a lot, alto, slot...
                candidate[p] = ' ';
                if (!cpdSuggest && CheckWord(candidate.TerminatedSpan, cpdSuggest) != 0)
                {
                    // remove not word pair suggestions
                    if (!good)
                    {
                        good = true;
                        wlst.Clear();
                    }

                    InsertSuggestion(wlst, candidate.TerminatedSpan.ToString());
                }

                // word pairs with dash?
                if (Affix.IsLanguageWithDashUsage)
                {
                    candidate[p] = '-';
                    if (!cpdSuggest && CheckWord(candidate.TerminatedSpan, cpdSuggest) != 0)
                    {
                        // remove not word pair suggestions
                        if (!good)
                        {
                            good = true;
                            wlst.Clear();
                        }

                        InsertSuggestion(wlst, candidate.TerminatedSpan.ToString());
                    }
                }

                if (wlst.Count < MaxSuggestions && !Affix.NoSplitSuggestions && !good)
                {
                    candidate[p] = '\0';

                    var c1 = CheckWord(candidate.TerminatedSpan, cpdSuggest);
                    if (c1 != 0)
                    {
                        var c2 = CheckWord(candidate.SliceToTerminator(p + 1), cpdSuggest);
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
                            var currentCandidateString = candidate.TerminatedSpan.ToString();
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
                                candidate.BufferLength - (p + 1) > 1
                            )
                            {
                                candidate[p] = '-';
                                currentCandidateString = candidate.TerminatedSpan.ToString();

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

            candidate.Destroy();
        }

        private bool CheckForbidden(ReadOnlySpan<char> word)
        {
            var rv = LookupFirstDetail(word);
            if ((rv?.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound)).GetValueOrDefault())
            {
                rv = null;
            }

            if (_query.PrefixCheck(word, CompoundOptions.Begin, default) is null)
            {
                rv = _query.SuffixCheck(word, AffixEntryOptions.None, null, default, default, CompoundOptions.Not)?.Detail; // prefix+suffix, suffix
            }

            // check forbidden words
            return (rv?.ContainsFlag(Affix.ForbiddenWord)).GetValueOrDefault();
        }

        /// <summary>
        /// Add prefix to this word assuming conditions hold.
        /// </summary>
        private string Add(PrefixEntry entry, string word)
        {
            if (word.Length > entry.Strip.Length || (word.Length == 0 && Affix.FullStrip))
            {
                if (entry.TestCondition(word.AsSpan()))
                {
                    if (entry.Strip.Length == 0)
                    {
                        // we have a match so add prefix
                        return string.Concat(entry.Append, word);
                    }

                    if (word.StartsWith(entry.Strip, StringComparison.Ordinal))
                    {
                        // we have a match so add prefix
                        return StringEx.ConcatString(entry.Append, word.AsSpan(entry.Strip.Length));
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Add suffix to this word assuming conditions hold.
        /// </summary>
        private string Add(SuffixEntry entry, string word)
        {
            // make sure all conditions match
            if (word.Length > entry.Strip.Length || (word.Length == 0 && Affix.FullStrip))
            {
                if (entry.TestCondition(word.AsSpan()))
                {
                    if (entry.Strip.Length == 0)
                    {
                        // we have a match so add suffix
                        return string.Concat(word, entry.Append);
                    }

                    if (word.AsSpan(word.Length - entry.Strip.Length).EqualsOrdinal(entry.Strip))
                    {
                        // we have a match so add suffix
                        return word.AsSpanRemoveFromEnd(entry.Strip.Length).ConcatString(entry.Append);
                    }
                }
            }

            return string.Empty;
        }

        private static bool CopyField(ref string dest, MorphSet morphs, string var)
        {
            if (morphs.Count == 0)
            {
                return false;
            }

            var morph = morphs.Join(' ');

            if (morph.Length == 0)
            {
                return false;
            }

            var pos = morph.IndexOf(var, StringComparison.Ordinal);
            if (pos < 0)
            {
                return false;
            }

            var begOffset = pos + MorphologicalTags.Stem.Length;
            var builder = StringBuilderPool.Get(morph.Length - begOffset);

            for (var i = begOffset; i < morph.Length; i++)
            {
                var c = morph[i];
                if (c is ' ' or '\t' or '\n')
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
            if (s1.Length == 0 || s2.Length == 0)
            {
                return 0;
            }

            if (Affix.ComplexPrefixes)
            {
                return leftCommonSubstringComplex(s1, s2);
            }

            if (s1[0] != s2[0] && s1[0] != TextInfo.ToLower(s2[0]))
            {
                return 0;
            }

            var minIndex = Math.Min(s1.Length, s2.Length);
            var index = 1;

            for ( ; index < minIndex && s1[index] == s2[index]; index++) ;

            return index;

            static int leftCommonSubstringComplex(string s1, string s2) =>
                (s1[s1.Length - 1] == s2[s2.Length - 1]) ? 1 : 0;
        }

        /// <summary>
        /// Generate an n-gram score comparing s1 and s2.
        /// </summary>
        private int NGram(int n, string s1, string s2, NGramOptions opt)
        {
            if (s1.Length == 0)
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

        /// <summary>Calculates a weighted score based on substring matching.</summary>
        /// <param name="n">The maximum size of substrings to generate.</param>
        /// <param name="s1">The text to generate substrings from.</param>
        /// <param name="t">The text to search.</param>
        /// <returns>The score.</returns>
        /// <remarks>
        /// This algorithm calculates a score which is based on all substrings in <paramref name="s1"/> that have a
        /// length between 1 and <paramref name="n"/>, that are also found in <paramref name="t"/>. The score is
        /// calculated as the number of substrings found minus the number of substrings that are not found. Also,
        /// for the substrings that are aligned to the beginning of s1 or the end of s1 the penalty for them not
        /// being found is doubled.
        ///
        /// To use an example, and invocation of (2, "nano", "banana") would produce 7 subrstrings to check and 5 would be found,
        /// resulting in a score of 1. The produced set of subrstrings would be: "n", "na", "a", "an", "n", "no", and "o".
        /// The reason the score is 1 instead of 3 is because the last two substrings deduct double from the score because they are
        /// aligned to the end of s1.
        /// Also note that in this example, the substring "n" from <paramref name="s1"/> is checked against <paramref name="t"/>
        /// twice and counted twice.
        /// </remarks>
        private static int NGramWeightedSearch(int n, ReadOnlySpan<char> s1, ReadOnlySpan<char> t)
        {
#if DEBUG
            if (s1.IsEmpty) throw new ArgumentOutOfRangeException(nameof(s1));
#endif

            // all substrings are left aligned for this first iteration so anything not matching needs to be double counted
            var needle = s1.Limit(n);
            var matchLength = FindLongestSubstringMatch(needle, t);
            var nscore = matchLength - ((needle.Length - matchLength) * 2);

            while (s1.Length > 1)
            {
                s1 = s1.Slice(1);

                needle = s1.Limit(n);
                matchLength = FindLongestSubstringMatch(needle, t);

                nscore += matchLength - (needle.Length - matchLength);

                if (needle.Length == s1.Length && needle.Length > matchLength)
                {
                    // in this case only the largest substring can be aligned to the end of s1 for double counting
                    nscore--;
                }
            }

            return nscore;
        }

        /// <summary>Calculates a non-weighted score based on substring matching.</summary>
        /// <param name="n">The maximum size of substrings to generate.</param>
        /// <param name="s1">The text to generate substrings from.</param>
        /// <param name="s2">The text to search.</param>
        /// <returns>The score.</returns>
        /// <remarks>
        /// This algorithm calculates a score which is the number of all substrings in <paramref name="s1"/> that have a
        /// length between 1 and <paramref name="n"/>, that are also found in <paramref name="s2"/>.
        ///
        /// To use an example, and invocation of (2, "nano", "banana") would produce 7 subrstrings to check and 5 would be found,
        /// resulting in a score of 5. The produced set of subrstrings would be: "n", "na", "a", "an", "n", "no", and "o".
        /// Note that in this example, the substring "n" from <paramref name="s1"/> is checked against <paramref name="s2"/> twice and counted twice.
        /// </remarks>
        private static int NGramNonWeightedSearch(int n, ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
#if DEBUG
            if (s1.IsEmpty) throw new ArgumentOutOfRangeException(nameof(s1));
#endif

            var nscore = 0;

            do
            {
                nscore += FindLongestSubstringMatch(s1.Limit(n), s2);
                s1 = s1.Slice(1);
            }
            while (!s1.IsEmpty);

            return nscore;
        }

        private static int FindLongestSubstringMatch(ReadOnlySpan<char> needle, ReadOnlySpan<char> haystack)
        {
#if DEBUG
            if (needle.IsEmpty) throw new ArgumentOutOfRangeException(nameof(needle));
#endif

            // This brute force algorithm leans heavily on the performance benefits of IndexOf.
            // As an optimization, break out when a better result is not possible.

            var best = 0;
            var searchIndex = haystack.IndexOf(needle[0]);
            while (searchIndex >= 0)
            {
                haystack = haystack.Slice(searchIndex);

                for (searchIndex = best + 1; searchIndex < haystack.Length && searchIndex < needle.Length && needle[searchIndex] == haystack[searchIndex]; searchIndex++) ;

                if (searchIndex > best)
                {
                    best = searchIndex;

                    if (best >= needle.Length)
                    {
                        break;
                    }
                }

                searchIndex = haystack.IndexOf(needle.Slice(0, best + 1), 1);
            }

            return best;
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

            var word = inword.ToCharArray().AsSpan();
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

                            // It's safe to Assume if (sChar == ')') because the length check is built into GetCharOrTerminator
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
                                StrMove(word, i + k0, i + k);
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

                                StrMove(word, 0, i + 1);
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

            return StringBuilderPool.GetStringAndReturn(target);
        }

        private static void StrMove(Span<char> span, int destIndex, int srcOffset)
        {
            for (var srcIndex = srcOffset; srcIndex < span.Length && destIndex < span.Length; srcIndex++, destIndex++)
            {
                span[destIndex] = span[srcIndex];
            }

            if (destIndex < span.Length)
            {
                span[destIndex] = '\0';
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

            public WordEntry? Root;

            public string? RootPhon;

            public int Score;

            public int ScorePhone;

            public class ScorePhoneComparer : IComparer<NGramSuggestSearchRoot>
            {
                public static readonly ScorePhoneComparer Default = new();

                private ScorePhoneComparer()
                {
                }

                public static int Comparison(NGramSuggestSearchRoot x, NGramSuggestSearchRoot y) => y.ScorePhone.CompareTo(x.ScorePhone);

                public int Compare(NGramSuggestSearchRoot x, NGramSuggestSearchRoot y) => Comparison(x, y);
            }
        }

        private struct NGramGuess
        {
            public NGramGuess(int i)
            {
                Guess = null;
                GuessOrig = null;
                Score = -100 * i;
            }

            public string? Guess;

            public string? GuessOrig;

            public int Score;

            public void ClearGuessAndOrig()
            {
                Guess = null;
                GuessOrig = null;
            }

            public class ScoreComparer : IComparer<NGramGuess>
            {
                public static readonly ScoreComparer Default = new();

                private ScoreComparer()
                {
                }

                public static int Comparison(NGramGuess x, NGramGuess y) => y.Score.CompareTo(x.Score);

                public int Compare(NGramGuess x, NGramGuess y) => Comparison(x, y);
            }
        }

        private struct GuessWord
        {
            public string? Word;

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
