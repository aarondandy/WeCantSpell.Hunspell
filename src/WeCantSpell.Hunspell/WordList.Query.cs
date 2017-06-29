using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public partial class WordList
    {
        private abstract class Query
        {
            protected Query(string word, WordList wordList)
            {
                WordToCheck = word;
                WordList = wordList;
                Affix = wordList.Affix;
            }

            protected const string DefaultXmlToken = "<?xml?>";

            protected const int MaxSharps = 5;

            protected const int MaxWordUtf8Len = MaxWordLen * 3;

            protected const int MaxSuggestions = 15;

            protected const int MaxRoots = 100;

            protected const int MaxWords = 100;

            protected const int MaxGuess = 200;

            protected const int MaxPhonSugs = 2;

            protected const int MaxPhoneTLen = 256;

            protected const int MaxPhoneTUtf8Len = MaxPhoneTLen * 4;

            public string WordToCheck { get; private set; }

            public WordList WordList { get; }

            public AffixConfig Affix { get; }

            public TextInfo TextInfo
            {
#if !NO_INLINE
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
                get => Affix.Culture.TextInfo;
            }

            private Affix<PrefixEntry> Prefix { get; set; }

            /// <summary>
            /// Previous prefix for counting syllables of the prefix.
            /// </summary>
            private string PrefixAppend { get; set; }

            private Affix<SuffixEntry> Suffix { get; set; }

            private FlagValue SuffixFlag { get; set; }

            /// <summary>
            /// Modifier for syllable count of <see cref="SuffixAppend"/>.
            /// </summary>
            private bool SuffixExtra { get; set; }

            /// <summary>
            /// Previous suffix for counting syllables of the suffix.
            /// </summary>
            private string SuffixAppend { get; set; }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearPrefix() =>
                Prefix = default(Affix<PrefixEntry>);

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearSuffix() =>
                Suffix = default(Affix<SuffixEntry>);

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearSuffixAndFlag()
            {
                ClearSuffix();
                SuffixFlag = default(FlagValue);
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearSuffixAppendAndExtra()
            {
                SuffixAppend = null;
                SuffixExtra = false;
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearAllAppendAndExtra()
            {
                PrefixAppend = null;
                SuffixAppend = null;
                SuffixExtra = false;
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetPrefix(Affix<PrefixEntry> prefix) =>
                Prefix = prefix;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetSuffix(Affix<SuffixEntry> suffix) =>
                Suffix = suffix;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetSuffixFlag(FlagValue flag) =>
                SuffixFlag = flag;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetSuffixExtra(bool extra) =>
                SuffixExtra = extra;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetSuffixAppend(string append) =>
                SuffixAppend = append;

            private bool AffixContainsContClass(FlagValue value) =>
                value.HasValue
                &&
                (
                    (Prefix != null && Prefix.Entry.ContainsContClass(value))
                    ||
                    (Suffix != null && Suffix.Entry.ContainsContClass(value))
                );

            private bool ContainFlagsOrBlockSuggest(WordEntryDetail rv, int isSug, FlagValue a, FlagValue b) =>
                rv.HasFlags // TODO: consider removing
                &&
                (
                    rv.ContainsAnyFlags(a, b)
                    ||
                    (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                );

            private bool ContainFlagsOrBlockSuggest(WordEntryDetail rv, int isSug, FlagValue a, FlagValue b, FlagValue c) =>
                rv.HasFlags // TODO: consider removing
                &&
                (
                    rv.ContainsAnyFlags(a, b, c)
                    ||
                    (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                );

            private bool HasSpecialInitCap(SpellCheckResultType info, WordEntryDetail he) =>
                EnumEx.HasFlag(info, SpellCheckResultType.InitCap) && he.ContainsFlag(SpecialFlags.OnlyUpcaseFlag);

            protected bool Check(string word) => new QueryCheck(word, WordList).Check();

            protected WordEntry CheckWord(string word, ref SpellCheckResultType info, out string root)
            {
                root = null;

                if (string.IsNullOrEmpty(word))
                {
                    return null;
                }

                if (Affix.IgnoredChars.HasItems)
                {
                    word = word.RemoveChars(Affix.IgnoredChars);

                    if (word.Length == 0)
                    {
                        return null;
                    }
                }

                // word reversing wrapper for complex prefixes
                if (Affix.ComplexPrefixes)
                {
                    word = word.Reverse();
                }

                // look word in hash table
                var details = WordList.FindEntryDetailsByRootWord(word);
                if (details.Length != 0)
                {
                    var heDetails = details[0];

                    // check forbidden and onlyincompound words
                    if (heDetails.ContainsFlag(Affix.ForbiddenWord))
                    {
                        info |= SpellCheckResultType.Forbidden;

                        if (heDetails.ContainsFlag(Affix.CompoundFlag) && Affix.IsHungarian)
                        {
                            info |= SpellCheckResultType.Compound;
                        }

                        return null;
                    }

                    // he = next not needaffix, onlyincompound homonym or onlyupcase word
                    var heIndex = 0;
                    while (
                        heDetails != null
                        &&
                        heDetails.HasFlags
                        &&
                        (
                            heDetails.ContainsAnyFlags(Affix.NeedAffix, Affix.OnlyInCompound)
                            ||
                            HasSpecialInitCap(info, heDetails)
                        )
                    )
                    {
                        heIndex++;
                        heDetails = heIndex < details.Length ? details[heIndex] : null;
                    }

                    if (heDetails != null)
                    {
                        return new WordEntry(word, heDetails);
                    }
                }

                // check with affixes

                // try stripping off affixes
                var he = AffixCheck(word, default(FlagValue), CompoundOptions.Not);

                // check compound restriction and onlyupcase
                if (
                    he != null
                    &&
                    he.Detail.HasFlags
                    &&
                    (
                        he.Detail.ContainsFlag(Affix.OnlyInCompound)
                        ||
                        HasSpecialInitCap(info, he.Detail)
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
                    var rwords = new IncrementalWordList();
                    he = CompoundCheck(word, 0, 0, 100, null, rwords, false, 0, ref info);

                    if (he == null && word.EndsWith('-') && Affix.IsHungarian)
                    {
                        // LANG_hu section: `moving rule' with last dash
                        he = CompoundCheck(word.Substring(0, word.Length - 1), -5, 0, 100, null, rwords, true, 0, ref info);
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

                return he;
            }

            private WordEntryDetail CompoundCheckWordSearchCompoundOnlyDetail(
                string searchEntryWord,
                PatternEntry scpdPatternEntry,
                ref IncrementalWordList words,
                IncrementalWordList rwords,
                bool cantCheckScpdFlags)
            {
#if DEBUG
                if (searchEntryWord == null)
                {
                    throw new ArgumentNullException(nameof(searchEntryWord));
                }
#endif

                if (cantCheckScpdFlags)
                {
                    foreach (var searchEntryDetail in LookupDetails(searchEntryWord))
                    {
                        if (
                            !searchEntryDetail.ContainsFlag(Affix.NeedAffix)
                            &&
                            DefCompoundCheck(ref words, searchEntryDetail, rwords, false)
                        )
                        {
                            return searchEntryDetail;
                        }
                    }
                }
                else
                {
                    foreach (var searchEntryDetail in LookupDetails(searchEntryWord))
                    {
                        if (
                            !searchEntryDetail.ContainsFlag(Affix.NeedAffix)
                            &&
                            // NOTE: do not reorder due to side effects
                            DefCompoundCheck(ref words, searchEntryDetail, rwords, false)
                            &&
                            searchEntryDetail.ContainsFlag(scpdPatternEntry.Condition)
                        )
                        {
                            return searchEntryDetail;
                        }
                    }
                }

                return null;
            }

            private WordEntryDetail CompoundCheckWordSearchMultiDetail(
                string searchEntryWord,
                PatternEntry scpdPatternEntry,
                IncrementalWordList words,
                int wordNum,
                bool cantCheckScpdFlags)
            {
#if DEBUG
                if (searchEntryWord == null)
                {
                    throw new ArgumentNullException(nameof(searchEntryWord));
                }
#endif

                if (words == null)
                {
                    var compoundPart = wordNum == 0 ? Affix.CompoundBegin : Affix.CompoundMiddle;
                    if (cantCheckScpdFlags)
                    {
                        foreach (var searchEntryDetail in LookupDetails(searchEntryWord))
                        {
                            if (
                                !searchEntryDetail.ContainsFlag(Affix.NeedAffix)
                                &&
                                searchEntryDetail.ContainsAnyFlags(Affix.CompoundFlag, compoundPart)
                            )
                            {
                                return searchEntryDetail;
                            }
                        }
                    }
                    else
                    {
                        foreach (var searchEntryDetail in LookupDetails(searchEntryWord))
                        {
                            if (
                                !searchEntryDetail.ContainsFlag(Affix.NeedAffix)
                                &&
                                searchEntryDetail.ContainsAnyFlags(Affix.CompoundFlag, scpdPatternEntry.Condition, compoundPart)
                            )
                            {
                                return searchEntryDetail;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var searchEntryDetail in LookupDetails(searchEntryWord))
                    {
                        if (
                            wordNum == 0
                            &&
                            !searchEntryDetail.ContainsFlag(Affix.NeedAffix)
                            &&
                            searchEntryDetail.ContainsFlag(Affix.CompoundBegin)
                            &&
                            (
                                cantCheckScpdFlags
                                ||
                                searchEntryDetail.ContainsFlag(scpdPatternEntry.Condition)
                            )
                        )
                        {
                            return searchEntryDetail;
                        }
                    }
                }

                

                return null;
            }

            private WordEntry CompoundCheckWordSearch(
                string searchEntryWord,
                PatternEntry scpdPatternEntry,
                ref IncrementalWordList words,
                IncrementalWordList rwords,
                int wordNum,
                bool huMovRule,
                bool onlycpdrule,
                bool conditionBypassAllCompounds)
            {
#if DEBUG
                if (searchEntryWord == null)
                {
                    throw new ArgumentNullException(nameof(searchEntryWord));
                }
#endif

                WordEntryDetail detail;

                if (huMovRule)
                {
                    detail = LookupFirstDetail(searchEntryWord);
                }
                else
                {
                    var cantCheckScpdFlags = scpdPatternEntry == null || !scpdPatternEntry.Condition.HasValue;
                    detail = onlycpdrule
                        ? (
                            (!Affix.CompoundRules.IsEmpty && (wordNum == 0 || words != null))
                            ? CompoundCheckWordSearchCompoundOnlyDetail(searchEntryWord, scpdPatternEntry, ref words, rwords, cantCheckScpdFlags)
                            : null
                        )
                        : (
                            (!conditionBypassAllCompounds)
                            ? CompoundCheckWordSearchMultiDetail(searchEntryWord, scpdPatternEntry, words, wordNum, cantCheckScpdFlags)
                            : null
                        );
                }

                return detail == null
                    ? null
                    : new WordEntry(searchEntryWord, detail);
            }

            private WordEntry HomonymWordSearch(string homonymWord, PatternEntry scpdPatternEntry, IncrementalWordList words, int scpd)
            {
#if DEBUG
                if (homonymWord == null)
                {
                    throw new ArgumentNullException(nameof(homonymWord));
                }
#endif

                WordEntryDetail rvDetail = null;
                foreach (var homonymCandidate in LookupDetails(homonymWord))
                {
                    if (
                        !homonymCandidate.ContainsFlag(Affix.NeedAffix)
                        &&
                        (
                            words == null
                            ? (
                                homonymCandidate.ContainsFlag(Affix.CompoundFlag)
                                ||
                                homonymCandidate.ContainsFlag(Affix.CompoundEnd)
                            )
                            : (
                                Affix.CompoundRules.HasItems
                                && DefCompoundCheck(words.CreateIncremented(), homonymCandidate, true)
                            )
                        )
                        &&
                        (
                            scpd == 0
                            || !scpdPatternEntry.Condition2.HasValue
                            || homonymCandidate.ContainsFlag(scpdPatternEntry.Condition2)
                        )
                    )
                    {
                        rvDetail = homonymCandidate;
                        break;
                    }
                }

                return rvDetail == null
                    ? null
                    : new WordEntry(homonymWord, rvDetail);
            }

            protected WordEntry CompoundCheck(string word, int wordNum, int numSyllable, int maxwordnum, IncrementalWordList words, IncrementalWordList rwords, bool huMovRule, int isSug, ref SpellCheckResultType info)
            {
#if DEBUG
                if (word == null)
                {
                    throw new ArgumentNullException(nameof(word));
                }
#endif
                int oldnumsyllable, oldnumsyllable2, oldwordnum, oldwordnum2;
                WordEntry rv;
                WordEntry rvFirst;
                var ch = '\0';
                var simplifiedTripple = false;
                var scpd = 0;
                var soldi = 0;
                var oldcmin = 0;
                var oldcmax = 0;
                var oldlen = 0;
                var checkedSimplifiedTriple = false;
                var affixed = false;

                var oldwords = words;
                var len = word.Length;
                var cmin = Affix.CompoundMin;
                var cmax = word.Length - cmin + 1;

                var st = new SimulatedCString(word);

                var conditionBypassAllCompounds = Affix.CompoundFlag.IsZero && Affix.CompoundBegin.IsZero && Affix.CompoundMiddle.IsZero;

                var movCompoundOptions = huMovRule ? CompoundOptions.Other : CompoundOptions.Begin;

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
                            PatternEntry scpdPatternEntry;
                            if (scpd > 0)
                            {
                                for (; scpd <= Affix.CompoundPatterns.Count && Affix.CompoundPatterns[scpd - 1].Pattern3DoesNotMatch(word, i); scpd++)
                                {
                                }

                                if (scpd > Affix.CompoundPatterns.Count)
                                {
                                    break; // break simplified checkcompoundpattern loop
                                }
                                else
                                {
                                    scpdPatternEntry = Affix.CompoundPatterns[scpd - 1];
                                }

                                st.WriteChars(scpdPatternEntry.Pattern, i);

                                soldi = i;
                                i += scpdPatternEntry.Pattern.Length;

                                st.WriteChars(scpdPatternEntry.Pattern2, i);

                                st.WriteChars(soldi + scpdPatternEntry.Pattern3.Length, word, i + scpdPatternEntry.Pattern2.Length);

                                oldlen = len;
                                len += scpdPatternEntry.Pattern.Length + scpdPatternEntry.Pattern2.Length + scpdPatternEntry.Pattern3.Length;
                                oldcmin = cmin;
                                oldcmax = cmax;
                                cmin = Affix.CompoundMin;
                                cmax = len - cmin + 1;
                            }
                            else
                            {
                                scpdPatternEntry = null;
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

                            rv = CompoundCheckWordSearch(
                                st.ToString(),
                                scpdPatternEntry,
                                ref words,
                                rwords,
                                wordNum,
                                huMovRule,
                                onlycpdrule,
                                conditionBypassAllCompounds);

                            if (rv == null)
                            {
                                if (onlycpdrule)
                                {
                                    break;
                                }

                                if (Affix.CompoundFlag.HasValue)
                                {
                                    rv = PrefixCheck(st.ToString(), movCompoundOptions, Affix.CompoundFlag);
                                    if (rv == null)
                                    {
                                        rv = SuffixCheck(st.ToString(), 0, default(Affix<PrefixEntry>), new FlagValue(), Affix.CompoundFlag, movCompoundOptions);
                                        if (rv == null && Affix.CompoundMoreSuffixes)
                                        {
                                            rv = SuffixCheckTwoSfx(st.ToString(), 0, default(Affix<PrefixEntry>), Affix.CompoundFlag);
                                        }

                                        if (
                                            rv != null
                                            &&
                                            !huMovRule
                                            &&
                                            Suffix != null
                                            &&
                                            Suffix.Entry.ContainsAnyContClass(Affix.CompoundForbidFlag, Affix.CompoundEnd)
                                        )
                                        {
                                            rv = null;
                                        }
                                    }
                                }

                                if (rv != null)
                                {
                                    checkedPrefix = true;
                                }
                                else if (wordNum == 0 && Affix.CompoundBegin.HasValue)
                                {
                                    rv = SuffixCheck(st.ToString(), 0, default(Affix<PrefixEntry>), default(FlagValue), Affix.CompoundBegin, movCompoundOptions);

                                    if(rv == null)
                                    {
                                        if(Affix.CompoundMoreSuffixes)
                                        {
                                            rv = SuffixCheckTwoSfx(st.ToString(), 0, default(Affix<PrefixEntry>), Affix.CompoundBegin);
                                            if (rv != null)
                                            {
                                                checkedPrefix = true;
                                            }
                                        }

                                        if (rv == null)
                                        {
                                            rv = PrefixCheck(st.ToString(), movCompoundOptions, Affix.CompoundBegin);
                                            if (rv != null)
                                            {
                                                checkedPrefix = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        checkedPrefix = true;
                                    }
                                }
                                else if (wordNum > 0 && Affix.CompoundMiddle.HasValue)
                                {
                                    rv = SuffixCheck(st.ToString(), 0, default(Affix<PrefixEntry>), default(FlagValue), Affix.CompoundMiddle, movCompoundOptions);
                                    if (rv == null)
                                    {
                                        if (Affix.CompoundMoreSuffixes)
                                        {
                                            rv = SuffixCheckTwoSfx(st.ToString(), 0, default(Affix<PrefixEntry>), Affix.CompoundMiddle);
                                            if (rv != null)
                                            {
                                                checkedPrefix = true;
                                            }
                                        }

                                        if (rv == null)
                                        {
                                            rv = PrefixCheck(st.ToString(), movCompoundOptions, Affix.CompoundMiddle);
                                            if (rv != null)
                                            {
                                                checkedPrefix = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        checkedPrefix = true;
                                    }
                                }

                            }
                            else
                            {
                                affixed = false;

                                if (ContainFlagsOrBlockSuggest(rv.Detail, isSug, Affix.ForbiddenWord, Affix.NeedAffix, SpecialFlags.OnlyUpcaseFlag))
                                {
                                    // else check forbiddenwords and needaffix
                                    if (i < st.BufferLength)
                                    {
                                        st[i] = ch;
                                    }

                                    break;
                                }
                            }

                            if(
                                rv != null
                                &&
                                !huMovRule
                                &&
                                (
                                    // check non_compound flag in suffix and prefix
                                    AffixContainsContClass(Affix.CompoundForbidFlag)
                                    ||
                                    (
                                        !checkedPrefix
                                        &&
                                        (
                                            // check compoundend flag in suffix and prefix
                                            AffixContainsContClass(Affix.CompoundEnd)
                                            ||
                                            // check compoundmiddle flag in suffix and prefix
                                            (wordNum == 0 && AffixContainsContClass(Affix.CompoundMiddle))
                                        )
                                    )
                                )
                            )
                            {
                                rv = null;
                            }

                            bool firstWordCompoundAcceptable;

                            if (rv != null)
                            {
                                // check forbiddenwords
                                if (ContainFlagsOrBlockSuggest(rv.Detail, isSug, Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag))
                                {
                                    st.Destroy();
                                    return null;
                                }

                                // increment word number, if the second root has a compoundroot flag
                                if (rv.Detail.ContainsFlag(Affix.CompoundRoot))
                                {
                                    wordNum++;
                                }

                                firstWordCompoundAcceptable =
                                    (
                                        checkedPrefix
                                        ||
                                        (words != null && words.CheckIfCurrentIsNotNull())
                                        ||
                                        rv.Detail.ContainsFlag(Affix.CompoundFlag)
                                        ||
                                        (
                                            oldwordnum == 0
                                                ? rv.Detail.ContainsFlag(Affix.CompoundBegin)
                                                : (oldwordnum > 0 && rv.Detail.ContainsFlag(Affix.CompoundMiddle))
                                        )
                                        ||
                                        (
                                            huMovRule
                                            && // LANG_hu section: spec. Hungarian rule
                                            Affix.IsHungarian
                                            && // XXX hardwired Hungarian dictionary codes
                                            rv.Detail.ContainsAnyFlags(SpecialFlags.LetterF, SpecialFlags.LetterG, SpecialFlags.LetterH)
                                        ) // END of LANG_hu section
                                    )
                                    && // test CHECKCOMPOUNDPATTERN conditions
                                    (
                                        scpd == 0
                                        ||
                                        scpdPatternEntry.Condition.IsZero
                                        ||
                                        rv.Detail.ContainsFlag(scpdPatternEntry.Condition)
                                    )
                                    &&
                                    (
                                        scpd != 0
                                        ||
                                        words != null
                                        ||
                                        (
                                            (
                                                !Affix.CheckCompoundTriple
                                                || // test triple letters
                                                i <= 0
                                                ||
                                                i >= word.Length
                                                ||
                                                word[i - 1] != word[i]
                                                ||
                                                (
                                                    (i < 2 || word[i - 1] != word[i - 2])
                                                    &&
                                                    (i + 1 >= word.Length || word[i - 1] != word[i + 1]) // may be word[i+1] == '\0'
                                                )
                                            )
                                            &&
                                            (
                                                !Affix.CheckCompoundCase
                                                ||
                                                !CompoundCaseCheck(word, i)
                                            )
                                        )
                                    );
                            }
                            else if (huMovRule && Affix.IsHungarian)
                            {
                                rv = AffixCheck(st.ToString(), default(FlagValue), CompoundOptions.Not);

                                firstWordCompoundAcceptable =
                                    rv != null
                                    && Suffix != null // XXX hardwired Hungarian dic. codes
                                    && Suffix.Entry.ContainsAnyContClass(SpecialFlags.LetterXLower, SpecialFlags.LetterPercent);
                            }
                            else
                            {
                                firstWordCompoundAcceptable = false;
                            }

                            // first word is acceptable in compound words?
                            if (firstWordCompoundAcceptable)
                            {
                                // first word is ok condition
                                if (Affix.IsHungarian)
                                {
                                    // calculate syllable number of the word
                                    numSyllable += GetSyllable(st.ToString().Subslice(0, i));

                                    // - affix syllable num.
                                    // XXX only second suffix (inflections, not derivations)
                                    if (SuffixAppend != null)
                                    {
                                        numSyllable -= GetSyllable(SuffixAppend.Reverse()) + (SuffixExtra ? 1 : 0);
                                    }

                                    // + 1 word, if syllable number of the prefix > 1 (hungarian convention)
                                    if (Prefix != null && GetSyllable(Prefix.Entry.Key) > 1)
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
                                        if (simplifiedTripple)
                                        {
                                            checkedSimplifiedTriple = true;
                                            i--; // check "fahrt" instead of "ahrt" in "Schiffahrt"
                                        }
                                        else if (i > 2 && word[i - 1] == word[i - 2])
                                        {
                                            simplifiedTripple = true;
                                        }
                                    }

                                    rv = HomonymWordSearch(st.ToString().Substring(i), scpdPatternEntry, words, scpd);

                                    if (rv != null)
                                    {
                                        // check FORCEUCASE
                                        if (!EnumEx.HasFlag(info, SpellCheckResultType.OrigCap) && rv.ContainsFlag(Affix.ForceUpperCase))
                                        {
                                            rv = null;
                                        }
                                        else if (words != null && words.CheckIfNextIsNotNull())
                                        {
                                            st.Destroy();
                                            return rvFirst;
                                        }
                                    }

                                    oldnumsyllable2 = numSyllable;
                                    oldwordnum2 = wordNum;

                                    if (rv != null)
                                    {
                                        if (
                                            Affix.IsHungarian
                                            &&
                                            rv.ContainsFlag(SpecialFlags.LetterI)
                                            &&
                                            !rv.ContainsFlag(SpecialFlags.LetterJ)
                                        )
                                        {
                                            numSyllable--;
                                        }

                                        // increment word number, if the second root has a compoundroot flag
                                        if (rv.ContainsFlag(Affix.CompoundRoot))
                                        {
                                            wordNum++;
                                        }

                                        // check forbiddenwords
                                        if (ContainFlagsOrBlockSuggest(rv.Detail, isSug, Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag))
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
                                            rv.Detail.ContainsAnyFlags(Affix.CompoundFlag, Affix.CompoundEnd)
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
                                            &&
                                            (
                                                !Affix.CheckCompoundDup
                                                ||
                                                rv != rvFirst
                                            )
                                            && // test CHECKCOMPOUNDPATTERN
                                            (
                                                Affix.CompoundPatterns.IsEmpty
                                                ||
                                                scpd != 0
                                                ||
                                                !Affix.CompoundPatterns.Check(word, i, rvFirst, rv, false)
                                            )
                                            && // test CHECKCOMPOUNDPATTERN conditions
                                            (
                                                scpd == 0
                                                ||
                                                scpdPatternEntry.Condition2.IsZero
                                                ||
                                                rv.ContainsFlag(scpdPatternEntry.Condition2)
                                            )
                                        )
                                        {
                                            st.Destroy();

                                            // forbid compound word, if it is a non compound word with typical fault
                                            return (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Subslice(0, len)))
                                                ? null
                                                : rvFirst;
                                        }
                                    }

                                    numSyllable = oldnumsyllable2;
                                    wordNum = oldwordnum2;

                                    // perhaps second word has prefix or/and suffix
                                    ClearSuffixAndFlag();

                                    rv = (!onlycpdrule && Affix.CompoundFlag.HasValue)
                                         ? AffixCheck(word.Substring(i), Affix.CompoundFlag, CompoundOptions.End)
                                         : null;

                                    if (rv == null && Affix.CompoundEnd.HasValue && !onlycpdrule)
                                    {
                                        ClearSuffix();
                                        ClearPrefix();
                                        rv = AffixCheck(word.Substring(i), Affix.CompoundEnd, CompoundOptions.End);
                                    }

                                    if (rv == null && Affix.CompoundRules.HasItems && words != null)
                                    {
                                        rv = AffixCheck(word.Substring(i), new FlagValue(), CompoundOptions.End);
                                        if (rv != null && DefCompoundCheck(words.CreateIncremented(), rv.Detail, true))
                                        {
                                            st.Destroy();
                                            return rvFirst;
                                        }

                                        rv = null;
                                    }

                                    if(
                                        rv != null
                                        &&
                                        (
                                            // check FORCEUCASE
                                            (
                                                !EnumEx.HasFlag(info, SpellCheckResultType.OrigCap)
                                                &&
                                                rv.ContainsFlag(Affix.ForceUpperCase)
                                            )
                                            ||
                                            // check non_compound flag in suffix and prefix
                                            AffixContainsContClass(Affix.CompoundForbidFlag)
                                            ||
                                            (
                                                scpd != 0
                                                // test CHECKCOMPOUNDPATTERN conditions (allowed forms)
                                                ? (
                                                    scpdPatternEntry.Condition2.HasValue
                                                    &&
                                                    !rv.ContainsFlag(scpdPatternEntry.Condition2)
                                                )
                                                // test CHECKCOMPOUNDPATTERN conditions (forbidden compounds)
                                                : (
                                                    Affix.CompoundPatterns.HasItems
                                                    &&
                                                    Affix.CompoundPatterns.Check(word, i, rvFirst, rv, affixed)
                                                )
                                            )
                                        )
                                    )
                                    {
                                        rv = null;
                                    }

                                    // check forbiddenwords
                                    if (
                                        rv != null
                                        &&
                                        ContainFlagsOrBlockSuggest(rv.Detail, isSug, Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                    )
                                    {
                                        st.Destroy();
                                        return null;
                                    }

                                    // pfxappnd = prefix of word+i, or NULL
                                    // calculate syllable number of prefix.
                                    // hungarian convention: when syllable number of prefix is more,
                                    // than 1, the prefix+word counts as two words.

                                    if (Affix.IsHungarian)
                                    {
                                        // calculate syllable number of the word
                                        numSyllable += GetSyllable(word.Subslice(0, i));

                                        // - affix syllable num.
                                        // XXX only second suffix (inflections, not derivations)
                                        if (SuffixAppend != null)
                                        {
                                            numSyllable -= GetSyllable(SuffixAppend.Reverse()) + (SuffixExtra ? 1 : 0);
                                        }

                                        // + 1 word, if syllable number of the prefix > 1 (hungarian
                                        // convention)
                                        if (Prefix != null && GetSyllable(Prefix.Entry.Key) > 1)
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

                                    if (rv != null)
                                    {
                                        // increment word number, if the second word has a compoundroot flag
                                        if (rv.ContainsFlag(Affix.CompoundRoot))
                                        {
                                            wordNum++;
                                        }

                                        // second word is acceptable, as a word with prefix or/and suffix?
                                        // hungarian conventions: compounding is acceptable,
                                        // when compound forms consist 2 word, otherwise
                                        // the syllable number of root words is 6, or lesser.
                                        if (
                                            (
                                                !Affix.CompoundWordMax.HasValue
                                                ||
                                                wordNum + 1 < Affix.CompoundWordMax.GetValueOrDefault()
                                                ||
                                                (
                                                    Affix.CompoundMaxSyllable != 0
                                                    &&
                                                    numSyllable <= Affix.CompoundMaxSyllable
                                                )
                                            )
                                            &&
                                            (
                                                !Affix.CheckCompoundDup
                                                ||
                                                rv != rvFirst
                                            )
                                        )
                                        {
                                            st.Destroy();

                                            // forbid compound word, if it is a non compound word with typical fault
                                            return (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Subslice(0, len)))
                                                ? null
                                                : rvFirst;
                                        }
                                    }

                                    numSyllable = oldnumsyllable2;
                                    wordNum = oldwordnum2;

                                    // perhaps second word is a compound word (recursive call)
                                    if (wordNum < maxwordnum)
                                    {
                                        rv = CompoundCheck(st.ToString().Substring(i), wordNum + 1, numSyllable, maxwordnum, words?.CreateIncremented(), rwords.CreateIncremented(), false, isSug, ref info);

                                        if (
                                            rv != null
                                            &&
                                            Affix.CompoundPatterns.HasItems
                                            &&
                                            (scpd != 0 ^ Affix.CompoundPatterns.Check(word, i, rvFirst, rv, affixed))
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

                                                if (Affix.CheckCompoundRep && CompoundReplacementCheck(st.ToString()))
                                                {
                                                    if (i + rv.Word.Length < st.BufferLength)
                                                    {
                                                        st[i + rv.Word.Length] = r;
                                                    }

                                                    continue;
                                                }

                                                if (Affix.ForbiddenWord.HasValue)
                                                {
                                                    var rv2 = LookupFirst(word);

                                                    if (rv2 == null)
                                                    {
                                                        rv2 = AffixCheck(word.Substring(0, len), default(FlagValue), CompoundOptions.Not);
                                                    }

                                                    if (
                                                        rv2 != null
                                                        && rv2.ContainsFlag(Affix.ForbiddenWord)
                                                        && StringEx.EqualsLimited(rv2.Word, st.ToString(), i + rv.Word.Length)
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
                                while (simplifiedTripple && !checkedSimplifiedTriple);  // end of striple loop

                                if (checkedSimplifiedTriple)
                                {
                                    i++;
                                    checkedSimplifiedTriple = false;
                                    simplifiedTripple = false;
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
                        while (!onlycpdrule && Affix.SimplifiedCompound && scpd <= Affix.CompoundPatterns.Count); // end of simplifiedcpd loop

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
                    while (Affix.CompoundRules.HasItems && oldwordnum == 0 && InversePostfixIncrement(ref onlycpdrule));
                }

                st.Destroy();
                return null;
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private static bool InversePostfixIncrement(ref bool b)
            {
                if (b)
                {
                    return false;
                }
                else
                {
                    b = true;
                    return true;
                }
            }

            /// <summary>
            /// Check if word with affixes is correctly spelled.
            /// </summary>
            private WordEntry AffixCheck(string word, FlagValue needFlag, CompoundOptions inCompound)
            {
#if DEBUG
                if (word == null)
                {
                    throw new ArgumentNullException(nameof(word));
                }
#endif
                // check all prefixes (also crossed with suffixes if allowed)
                var rv = PrefixCheck(word, inCompound, needFlag);
                if (rv == null)
                {
                    // if still not found check all suffixes
                    rv = SuffixCheck(word, 0, default(Affix<PrefixEntry>), default(FlagValue), needFlag, inCompound);

                    if (Affix.ContClasses.HasItems)
                    {
                        ClearSuffix();
                        ClearPrefix();

                        if (rv == null)
                        {
                            rv =
                                // if still not found check all two-level suffixes
                                SuffixCheckTwoSfx(word, 0, default(Affix<PrefixEntry>), needFlag)
                                ??
                                // if still not found check all two-level prefixes
                                PrefixCheckTwoSfx(word, CompoundOptions.Not, needFlag);
                        }
                    }
                }

                return rv;
            }

            /// <summary>
            /// Check word for prefixes
            /// </summary>
            protected WordEntry PrefixCheck(string word, CompoundOptions inCompound, FlagValue needFlag)
            {
                ClearPrefix();
                ClearAllAppendAndExtra();
                WordEntry rv;

                var isEndCompound = inCompound == CompoundOptions.End;
                if (isEndCompound && Affix.CompoundPermitFlag.IsZero)
                {
                    // not possible to permit prefixes in compounds
                    return null;
                }

                // first handle the special case of 0 length prefixes
                foreach (var peGroup in Affix.Prefixes.AffixesWithEmptyKeys)
                {
                    foreach (var pe in peGroup.Entries)
                    {
                        if (
                            // fogemorpheme
                            (inCompound != CompoundOptions.Not || !pe.ContainsContClass(Affix.OnlyInCompound))
                            &&
                            // permit prefixes in compounds
                            (!isEndCompound || pe.ContainsContClass(Affix.CompoundPermitFlag))
                        )
                        {
                            // check prefix
                            var affix = Affix<PrefixEntry>.Create(pe, peGroup);
                            rv = CheckWordPrefix(affix, word, inCompound, needFlag);
                            if (rv != null)
                            {
                                SetPrefix(affix);
                                return rv;
                            }
                        }
                    }
                }

                // now handle the general case
                foreach (var pptr in Affix.Prefixes.GetMatchingAffixes(word))
                {
                    if (
                        // fogemorpheme
                        (inCompound != CompoundOptions.Not || !pptr.Entry.ContainsContClass(Affix.OnlyInCompound))
                        &&
                        // permit prefixes in compounds
                        (!isEndCompound || pptr.Entry.ContainsContClass(Affix.CompoundPermitFlag))
                    )
                    {
                        // check prefix
                        rv = CheckWordPrefix(pptr, word, inCompound, needFlag);
                        if (rv != null)
                        {
                            SetPrefix(pptr);
                            return rv;
                        }
                    }
                }

                return null;
            }

            protected WordEntry PrefixCheckTwoSfx(string word, CompoundOptions inCompound, FlagValue needFlag)
            {
                ClearPrefix();
                ClearSuffixAppendAndExtra();
                WordEntry rv;

                // first handle the special case of 0 length prefixes
                foreach (var peGroup in Affix.Prefixes.AffixesWithEmptyKeys)
                {
                    foreach (var pe in peGroup.GetAffixesInternal())
                    {
                        rv = CheckTwoSfx(pe, word, inCompound, needFlag);
                        if (rv != null)
                        {
                            return rv;
                        }
                    }
                }

                // now handle the general case
                foreach (var pptr in Affix.Prefixes.GetMatchingAffixes(word))
                {
                    rv = CheckTwoSfx(pptr, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        SetPrefix(pptr);
                        return rv;
                    }
                }

                return null;
            }

            /// <summary>
            /// Check if this prefix entry matches.
            /// </summary>
            private WordEntry CheckTwoSfx(Affix<PrefixEntry> pe, string word, CompoundOptions inCompound, FlagValue needFlag)
            {
                // on entry prefix is 0 length or already matches the beginning of the word.
                // So if the remaining root word has positive length
                // and if there are enough chars in root word and added back strip chars
                // to meet the number of characters conditions, then test it

                var peEntry = pe.Entry;
                var tmpl = word.Length - peEntry.Append.Length; // length of tmpword

                if (
                    (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                    &&
                    (tmpl + peEntry.Strip.Length >= peEntry.Conditions.Count)
                )
                {
                    // generate new root word by removing prefix and adding
                    // back any characters that would have been stripped

                    var tmpword = StringEx.ConcatString(peEntry.Strip, word, peEntry.Append.Length, word.Length - peEntry.Append.Length);

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then check if resulting
                    // root word in the dictionary

                    if (TestCondition(pe.Entry, tmpword))
                    {
                        // prefix matched but no root word was found
                        // if CrossProduct is allowed, try again but now
                        // cross checked combined with a suffix

                        if (EnumEx.HasFlag(pe.Options, AffixEntryOptions.CrossProduct) && (inCompound != CompoundOptions.Begin))
                        {
                            // find hash entry of root word
                            var he = SuffixCheckTwoSfx(tmpword, AffixEntryOptions.CrossProduct, pe, needFlag);
                            if (he != null)
                            {
                                return he;
                            }
                        }
                    }
                }

                return null;
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            protected bool TestCondition(PrefixEntry entry, string word) => entry.Conditions.IsStartingMatch(word);

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            protected bool TestCondition(SuffixEntry entry, string word) => entry.Conditions.IsEndingMatch(word);

            protected WordEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, Affix<PrefixEntry> pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
            {
                WordEntry rv;

                if (!Affix.Suffixes.HasAffixes)
                {
                    return null;
                }

                var isBeginCompound = inCompound == CompoundOptions.Begin;
                if (isBeginCompound && Affix.CompoundPermitFlag.IsZero)
                {
                    // not possible to be signed with compoundpermitflag flag
                    return null;
                }

                var checkWordCclassFlag = inCompound != CompoundOptions.Not ? default(FlagValue) : Affix.OnlyInCompound;

                var pfxHasCircumfix = false;
                var pfxDoesNotNeedAffix = false;
                if (pfx != null)
                {
                    pfxHasCircumfix = pfx.Entry.ContainsContClass(Affix.Circumfix);
                    pfxDoesNotNeedAffix = !pfx.Entry.ContainsContClass(Affix.NeedAffix);
                }

                // first handle the special case of 0 length suffixes
                foreach (var seGroup in Affix.Suffixes.AffixesWithEmptyKeys)
                {
                    foreach (var se in seGroup.Entries)
                    {
                        // suffixes are not allowed in beginning of compounds
                        if (
                            (
                                !isBeginCompound
                                ||
                                // except when signed with compoundpermitflag flag
                                se.ContainsContClass(Affix.CompoundPermitFlag)
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
                                    ? se.ContClass.HasItems
                                    : (pfxDoesNotNeedAffix || !se.ContainsContClass(Affix.NeedAffix))
                            )
                            &&
                            (
                                Affix.Circumfix.IsZero
                                ||
                                // no circumfix flag in prefix and suffix
                                // circumfix flag in prefix AND suffix
                                se.ContainsContClass(Affix.Circumfix) == pfxHasCircumfix
                            )
                        )
                        {
                            var affix = Affix<SuffixEntry>.Create(se, seGroup);
                            rv = CheckWordSuffix(affix, word, sfxOpts, pfx, cclass, needFlag, checkWordCclassFlag);
                            if (rv != null)
                            {
                                SetSuffix(affix);
                                return rv;
                            }
                        }
                    }
                }

                // now handle the general case
                if (word.Length == 0)
                {
                    return null;
                }

                foreach (var sptr in Affix.Suffixes.GetMatchingAffixes(word))
                {
                    var sptrEntry = sptr.Entry;
                    if (
                        (
                            // suffixes are not allowed in beginning of compounds
                            !isBeginCompound
                            ||
                            // except when signed with compoundpermitflag flag
                            sptrEntry.ContainsContClass(Affix.CompoundPermitFlag)
                        )
                        &&
                        // fogemorpheme
                        (
                            inCompound != CompoundOptions.Not
                            ||
                            !sptrEntry.ContainsContClass(Affix.OnlyInCompound)
                        )
                        &&
                        (
                            inCompound != CompoundOptions.End
                            ||
                            pfx != null
                            ||
                            !sptrEntry.ContainsContClass(Affix.OnlyInCompound)
                        )
                        &&
                        // needaffix on prefix or first suffix
                        (
                            pfxDoesNotNeedAffix
                            ||
                            cclass.HasValue
                            ||
                            !sptrEntry.ContainsContClass(Affix.NeedAffix)
                        )
                        &&
                        (
                            Affix.Circumfix.IsZero
                            ||
                            // no circumfix flag in prefix and suffix
                            // circumfix flag in prefix AND suffix
                            sptrEntry.ContainsContClass(Affix.Circumfix) == pfxHasCircumfix
                        )
                    )
                    {
                        rv = CheckWordSuffix(sptr, word, sfxOpts, pfx, cclass, needFlag, checkWordCclassFlag);
                        if (rv != null)
                        {
                            SetSuffix(sptr);
                            SuffixFlag = sptr.AFlag;

                            if (!sptrEntry.ContClass.HasItems)
                            {
                                SetSuffixAppend(sptrEntry.Key);
                            }
                            else if (
                                Affix.IsHungarian
                                && sptrEntry.Key.Length >= 2
                                && sptrEntry.Key[0] == 'i'
                                && sptrEntry.Key[1] != 'y'
                                && sptrEntry.Key[1] != 't'
                            )
                            {
                                // LANG_hu section: spec. Hungarian rule
                                SetSuffixExtra(true);
                            }

                            // END of LANG_hu section
                            return rv;
                        }
                    }
                }

                return null;
            }

            /// <summary>
            /// Check word for two-level suffixes.
            /// </summary>
            protected WordEntry SuffixCheckTwoSfx(string word, AffixEntryOptions sfxopts, Affix<PrefixEntry> pfx, FlagValue needflag)
            {
#if DEBUG
                if (word == null)
                {
                    throw new ArgumentNullException(nameof(word));
                }
#endif
                WordEntry rv;

                // first handle the special case of 0 length suffixes
                foreach (var seGroup in Affix.Suffixes.GetAffixesWithEmptyKeysAndFlag(Affix.ContClasses))
                {
                    foreach (var se in seGroup.GetAffixesInternal())
                    {
                        rv = CheckTwoSfx(se, word, sfxopts, pfx, needflag);
                        if (rv != null)
                        {
                            return rv;
                        }
                    }
                }

                // now handle the general case
                if (word.Length == 0)
                {
                    return null; // FULLSTRIP
                }

                foreach (var sptr in Affix.Suffixes.GetMatchingAffixes(word))
                {
                    if (Affix.ContClasses.Contains(sptr.AFlag))
                    {
                        rv = CheckTwoSfx(sptr, word, sfxopts, pfx, needflag);
                        if (rv != null)
                        {
                            SetSuffixFlag(Suffix.AFlag);
                            if (!sptr.Entry.ContClass.HasItems)
                            {
                                SetSuffixAppend(sptr.Entry.Key);
                            }

                            return rv;
                        }
                    }
                }

                return null;
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            internal WordEntry LookupFirst(string word) => WordList.FindFirstEntryByRootWord(word);

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            internal WordEntryDetail[] LookupDetails(string word) => WordList.FindEntryDetailsByRootWord(word);

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            internal WordEntryDetail LookupFirstDetail(string word) => WordList.FindFirstEntryDetailByRootWord(word);

            /// <summary>
            /// Compound check patterns.
            /// </summary>
            private bool DefCompoundCheck(ref IncrementalWordList words, WordEntryDetail rv, IncrementalWordList def, bool all)
            {
                if (words == null)
                {
                    if (def != null && DefCompoundCheck(def, rv, all))
                    {
                        words = def;
                        return true;
                    }

                    return false;
                }

                return DefCompoundCheck(words, rv, all);
            }

            /// <summary>
            /// Compound check patterns.
            /// </summary>
            private bool DefCompoundCheck(IncrementalWordList words, WordEntryDetail rv, bool all)
            {
                // has the last word COMPOUNDRULE flag?
                if (Affix.CompoundRules.EntryContainsRuleFlags(rv))
                {
                    words.SetCurrent(rv);
                    if (Affix.CompoundRules.CompoundCheckInternal(words, all))
                    {
                        return true;
                    }
                }

                words.ClearCurrent();
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
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private int GetSyllable(string word) => GetSyllable(new StringSlice(word));

            /// <summary>
            /// Calculate number of syllable for compound-checking.
            /// </summary>
            private int GetSyllable(StringSlice word)
            {
                var num = 0;

                if (Affix.CompoundMaxSyllable != 0 && Affix.CompoundVowels.HasItems)
                {
                    var maxIndex = word.Offset + word.Length;
                    for (var i = word.Offset; i < maxIndex; i++)
                    {
                        if (Affix.CompoundVowels.Contains(word.Text[i]))
                        {
                            num++;
                        }
                    }
                }

                return num;
            }

            /// <summary>
            /// Is word a non compound with a REP substitution?
            /// </summary>
            /// <seealso cref="AffixConfig.CheckCompoundRep"/>
            /// <seealso cref="AffixConfig.Replacements"/>
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private bool CompoundReplacementCheck(string word) =>
                CompoundReplacementCheck(new StringSlice(word));

            /// <summary>
            /// Is word a non compound with a REP substitution?
            /// </summary>
            /// <seealso cref="AffixConfig.CheckCompoundRep"/>
            /// <seealso cref="AffixConfig.Replacements"/>
            private bool CompoundReplacementCheck(StringSlice wordSlice)
            {
                if (wordSlice.Length < 2 || Affix.Replacements.IsEmpty)
                {
                    return false;
                }

                foreach (var replacementEntry in Affix.Replacements)
                {
                    // search every occurence of the pattern in the word
                    var rIndex = wordSlice.IndexOf(replacementEntry.Pattern, StringComparison.Ordinal);
                    if (rIndex > 0)
                    {
                        var word = wordSlice.ToString();
                        while (rIndex >= 0)
                        {
                            var type = rIndex == 0 ? ReplacementValueType.Isol : ReplacementValueType.Med;
                            var replacement = replacementEntry[type];
                            if (replacement != null && CandidateCheck(word.Replace(rIndex, replacementEntry.Pattern.Length, replacement)))
                            {
                                return true;
                            }

                            rIndex = word.IndexOfOrdinal(replacementEntry.Pattern, rIndex + 1); // search for the next letter
                        }
                    }
                }

                return false;
            }

            private WordEntry CheckWordPrefix(Affix<PrefixEntry> affix, string word, CompoundOptions inCompound, FlagValue needFlag)
            {
                // on entry prefix is 0 length or already matches the beginning of the word.
                // So if the remaining root word has positive length
                // and if there are enough chars in root word and added back strip chars
                // to meet the number of characters conditions, then test it

                var tmpl = word.Length - affix.Entry.Append.Length; // length of tmpword

                if (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                {
                    // generate new root word by removing prefix and adding
                    // back any characters that would have been stripped

                    var tmpword = StringEx.ConcatString(affix.Entry.Strip, word, affix.Entry.Append.Length, word.Length - affix.Entry.Append.Length);

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then check if resulting
                    // root word in the dictionary

                    if (TestCondition(affix.Entry, tmpword))
                    {
                        foreach (var detail in LookupDetails(tmpword))
                        {
                            if (
                                detail.ContainsFlag(affix.AFlag)
                                &&
                                !affix.Entry.ContainsContClass(Affix.NeedAffix) // forbid single prefixes with needaffix flag
                                &&
                                (
                                    needFlag.IsZero
                                    ||
                                    detail.ContainsFlag(needFlag)
                                    ||
                                    affix.Entry.ContainsContClass(needFlag)
                                )
                            )
                            {
                                return new WordEntry(tmpword, detail);
                            }
                        }

                        // prefix matched but no root word was found
                        // if aeXPRODUCT is allowed, try again but now
                        // ross checked combined with a suffix

                        if (EnumEx.HasFlag(affix.Options, AffixEntryOptions.CrossProduct))
                        {
                            var he = SuffixCheck(tmpword, AffixEntryOptions.CrossProduct, affix, default(FlagValue), needFlag, inCompound);
                            if (he != null)
                            {
                                return he;
                            }
                        }

                    }
                }

                return null;
            }

            private WordEntry CheckWordSuffix(Affix<SuffixEntry> affix, string word, AffixEntryOptions optFlags, Affix<PrefixEntry> pfx, FlagValue cclass, FlagValue needFlag, FlagValue badFlag)
            {
                // if this suffix is being cross checked with a prefix
                // but it does not support cross products skip it

                var optFlagsHasCrossProduct = EnumEx.HasFlag(optFlags, AffixEntryOptions.CrossProduct);
                if (
                    (optFlagsHasCrossProduct && !EnumEx.HasFlag(affix.Options, AffixEntryOptions.CrossProduct))
                    ||
                    (cclass.HasValue && !affix.Entry.ContainsContClass(cclass)) // ! handle cont. class
                    ||
                    (optFlagsHasCrossProduct && pfx == null) // enabled by prefix is impossible
                )
                {
                    return null;
                }

                // upon entry suffix is 0 length or already matches the end of the word.
                // So if the remaining root word has positive length
                // and if there are enough chars in root word and added back strip chars
                // to meet the number of characters conditions, then test it

                var tmpl = word.Length - affix.Entry.Append.Length;
                // the second condition is not enough for UTF-8 strings
                // it checked in test_condition()

                if (
                    (tmpl > 0 || (Affix.FullStrip && tmpl == 0))
                    &&
                    (tmpl + affix.Entry.Strip.Length >= affix.Entry.Conditions.Count)
                )
                {
                    // generate new root word by removing suffix and adding
                    // back any characters that would have been stripped or
                    // or null terminating the shorter string

                    var tmpstring = StringEx.ConcatString(word, 0, tmpl, affix.Entry.Strip);

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then check if resulting
                    // root word in the dictionary
                    if (affix.Entry.Conditions.IsEndingMatch(tmpstring))
                    {
                        foreach (var heDetail in LookupDetails(tmpstring))
                        {
                            if (
                                (
                                    heDetail.ContainsFlag(affix.AFlag)
                                    ||
                                    (
                                        pfx != null
                                        &&
                                        pfx.Entry.ContainsContClass(affix.AFlag)
                                    )
                                )
                                &&
                                (
                                    !optFlagsHasCrossProduct
                                    ||
                                    (
                                        pfx != null
                                        &&
                                        (
                                            heDetail.ContainsFlag(pfx.AFlag)
                                            ||
                                            affix.Entry.ContainsContClass(pfx.AFlag) // enabled by prefix
                                        )
                                    )
                                )
                                && // check only in compound homonyms (bad flags)
                                !heDetail.ContainsFlag(badFlag)
                                && // handle required flag
                                (
                                    needFlag.IsZero
                                    ||
                                    heDetail.ContainsFlag(needFlag)
                                    ||
                                    affix.Entry.ContainsContClass(needFlag)
                                )
                            )
                            {
                                return new WordEntry(tmpstring, heDetail);
                            }
                        }
                    }
                }

                return null;
            }

            /// <summary>
            /// See if two-level suffix is present in the word.
            /// </summary>
            private WordEntry CheckTwoSfx(Affix<SuffixEntry> se, string word, AffixEntryOptions optflags, Affix<PrefixEntry> ppfx, FlagValue needflag)
            {
                // if this suffix is being cross checked with a prefix
                // but it does not support cross products skip it

                if (EnumEx.HasFlag(optflags, AffixEntryOptions.CrossProduct) && !EnumEx.HasFlag(se.Options, AffixEntryOptions.CrossProduct))
                {
                    return null;
                }

                // upon entry suffix is 0 length or already matches the end of the word.
                // So if the remaining root word has positive length
                // and if there are enough chars in root word and added back strip chars
                // to meet the number of characters conditions, then test it

                var tmpl = word.Length - se.Entry.Append.Length; // length of tmpword

                // the second condition is not enough for UTF-8 strings
                // it checked in test_condition()

                if (
                    (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
                    &&
                    (tmpl + se.Entry.Strip.Length >= se.Entry.Conditions.Count)
                )
                {
                    // generate new root word by removing suffix and adding
                    // back any characters that would have been stripped or
                    // or null terminating the shorter string

                    var tmpword = StringEx.ConcatString(word, 0, Math.Min(tmpl, word.Length), se.Entry.Strip);

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then recall suffix_check
                    if (TestCondition(se.Entry, tmpword))
                    {
                        var he = ppfx != null && se.Entry.ContainsContClass(ppfx.AFlag)
                            // handle conditional suffix
                            ? SuffixCheck(tmpword, AffixEntryOptions.None, default(Affix<PrefixEntry>), se.AFlag, needflag, CompoundOptions.Not)
                            : SuffixCheck(tmpword, optflags, ppfx, se.AFlag, needflag, CompoundOptions.Not);

                        if (he != null)
                        {
                            return he;
                        }
                    }
                }

                return null;
            }

            private bool CandidateCheck(string word) =>
                WordList.ContainsEntriesForRootWord(word) || AffixCheck(word, default(FlagValue), CompoundOptions.Not) != null;

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
            protected int CleanWord2(out string dest, string src, out CapitalizationType capType, out int abbv)
            {
                // first skip over any leading blanks
                var qIndex = HunspellTextFunctions.CountMatchingFromLeft(src, ' ');

                // now strip off any trailing periods (recording their presence)
                abbv = HunspellTextFunctions.CountMatchingFromRight(src, '.');

                var nl = src.Length - qIndex - abbv;

                // if no characters are left it can't be capitalized
                if (nl <= 0)
                {
                    dest = string.Empty;
                    capType = CapitalizationType.None;
                    return 0;
                }

                dest = src.Substring(qIndex, nl);
                capType = HunspellTextFunctions.GetCapitalizationType(new StringSlice(dest), TextInfo);
                return dest.Length;
            }

            protected enum CompoundOptions : byte
            {
                Not = 0,
                Begin = 1,
                End = 2,
                Other = 3
            }
        }
    }
}
