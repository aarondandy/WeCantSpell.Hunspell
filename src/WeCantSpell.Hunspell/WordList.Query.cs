using WeCantSpell.Hunspell.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

            private AffixEntryWithDetail<PrefixEntry> Prefix { get; set; }

            /// <summary>
            /// Previous prefix for counting syllables of the prefix.
            /// </summary>
            private string PrefixAppend { get; set; }

            private AffixEntryWithDetail<SuffixEntry> Suffix { get; set; }

            private FlagValue SuffixFlag { get; set; }

            /// <summary>
            /// Modifier for syllable count of <see cref="SuffixAppend"/>.
            /// </summary>
            private bool SuffixExtra { get; set; }

            private int SuffixExtraInt => SuffixExtra ? 1 : 0;

            /// <summary>
            /// Previous suffix for counting syllables of the suffix.
            /// </summary>
            private string SuffixAppend { get; set; }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearPrefix() =>
                Prefix = null;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void ClearSuffix() =>
                Suffix = null;

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
            private void SetPrefix(AffixEntryWithDetail<PrefixEntry> entry) =>
                Prefix = entry;

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private void SetSuffix(AffixEntryWithDetail<SuffixEntry> entry) =>
                Suffix = entry;

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

            protected bool Check(string word) => new QueryCheck(word, WordList).Check();

            protected string MakeInitCap(string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return s;
                }

                var builder = StringBuilderPool.Get(s, s.Length);
                builder[0] = Affix.Culture.TextInfo.ToUpper(builder[0]);
                return StringBuilderPool.GetStringAndReturn(builder);
            }

            protected string MakeInitCap(StringSlice s)
            {
                if (s.IsNullOrEmpty)
                {
                    return string.Empty;
                }

                var builder = StringBuilderPool.Get(s.Length);
                builder.Append(Affix.Culture.TextInfo.ToUpper(s.Text[s.Offset]));
                builder.Append(s.Text, s.Offset + 1, s.Length - 1);
                return StringBuilderPool.GetStringAndReturn(builder);
            }

            /// <summary>
            /// Convert to all little.
            /// </summary>
#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            protected string MakeAllSmall(string s) => Affix.Culture.TextInfo.ToLower(s);

            protected string MakeInitSmall(string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return s;
                }

                var builder = StringBuilderPool.Get(s, s.Length);
                builder[0] = Affix.Culture.TextInfo.ToLower(builder[0]);
                return StringBuilderPool.GetStringAndReturn(builder);
            }

#if !NO_INLINE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            protected string MakeAllCap(string s) => Affix.Culture.TextInfo.ToUpper(s);

            protected WordEntry CheckWord(string word, ref SpellCheckResultType info, out string root)
            {
                root = null;

                if (word == null || word.Length == 0)
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
                WordEntry he;
                var entries = WordList.FindEntriesByRootWord(word);
                if (entries.IsEmpty)
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

                        if (he.ContainsFlag(Affix.CompoundFlag) && Affix.IsHungarian)
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
                                EnumEx.HasFlag(info, SpellCheckResultType.InitCap)
                                &&
                                he.ContainsFlag(SpecialFlags.OnlyUpcaseFlag)
                            )
                        )
                    )
                    {
                        heIndex++;
                        he = heIndex < entries.Count ? entries[heIndex] : null;
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
                            (EnumEx.HasFlag(info, SpellCheckResultType.InitCap) && he.ContainsFlag(SpecialFlags.OnlyUpcaseFlag))
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
                        var rwords = new Dictionary<int, WordEntry>();
                        he = CompoundCheck(word, 0, 0, 100, 0, null, ref rwords, false, 0, ref info);

                        if (he == null && word.EndsWith('-') && Affix.IsHungarian)
                        {
                            // LANG_hu section: `moving rule' with last dash
                            var dup = word.Subslice(0, word.Length - 1);
                            he = CompoundCheck(dup, -5, 0, 100, 0, null, ref rwords, true, 0, ref info);
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

            protected WordEntry CompoundCheck(StringSlice word, int wordNum, int numSyllable, int maxwordnum, int wnum, Dictionary<int, WordEntry> words, ref Dictionary<int, WordEntry> rwords, bool huMovRule, int isSug, ref SpellCheckResultType info) =>
                CompoundCheck(word.ToString(), wordNum, numSyllable, maxwordnum, wnum, words, ref rwords, huMovRule, isSug, ref info);

            protected WordEntry CompoundCheck(string word, int wordNum, int numSyllable, int maxwordnum, int wnum, Dictionary<int, WordEntry> words, ref Dictionary<int, WordEntry> rwords, bool huMovRule, int isSug, ref SpellCheckResultType info)
            {
                int oldnumsyllable, oldnumsyllable2, oldwordnum, oldwordnum2;
                WordEntry rv;
                WordEntry rvFirst;
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

                var conditionBypassA = Affix.CompoundFlag.IsZero && Affix.CompoundBegin.IsZero && Affix.CompoundMiddle.IsZero;

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
                                for (
                                    ;
                                    Affix.CompoundPatterns.TryGetPattern(scpd, out scpdPatternEntry)
                                    &&
                                    (
                                        string.IsNullOrEmpty(scpdPatternEntry.Pattern3)
                                        ||
                                        !StringEx.EqualsOffset(word, i, scpdPatternEntry.Pattern3, 0, scpdPatternEntry.Pattern3.Length)
                                    )
                                    ;
                                    scpd++
                                )
                                {
                                    ;
                                }

                                if (scpd > Affix.CompoundPatterns.Count)
                                {
                                    break; // break simplified checkcompoundpattern loop
                                }

                                Affix.CompoundPatterns.TryGetPattern(scpd, out scpdPatternEntry);

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
                            var searchEntriesEnumerator = Lookup(st.ToString()).GetEnumerator(); // perhaps without prefix

                            rv = searchEntriesEnumerator.MoveNext() ? searchEntriesEnumerator.Current : null;

                            // search homonym with compound flag
                            while (
                                rv != null
                                &&
                                !huMovRule
                                &&
                                (
                                    rv.ContainsFlag(Affix.NeedAffix)
                                    ||
                                    (
                                        onlycpdrule
                                            ? (
                                                Affix.CompoundRules.IsEmpty
                                                ||
                                                (
                                                    wordNum != 0
                                                    &&
                                                    words == null
                                                )
                                                ||
                                                !DefCompoundCheck(ref words, wnum, rv, rwords, false)
                                            )
                                            : (
                                                conditionBypassA
                                                ||
                                                (
                                                    (
                                                        words != null
                                                        ||
                                                        !rv.ContainsFlag(Affix.CompoundFlag)
                                                    )
                                                    &&
                                                    (
                                                        wordNum == 0
                                                        ? !rv.ContainsFlag(Affix.CompoundBegin)
                                                        : (
                                                            words != null
                                                            ||
                                                            !rv.ContainsFlag(Affix.CompoundMiddle)
                                                        )
                                                    )
                                                )
                                            )
                                    )
                                    ||
                                    (
                                        scpd != 0
                                        &&
                                        scpdPatternEntry.Condition.HasValue
                                        &&
                                        !rv.ContainsFlag(scpdPatternEntry.Condition)
                                    )
                                )
                            )
                            {
                                rv = searchEntriesEnumerator.MoveNext() ? searchEntriesEnumerator.Current : null;
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
                                        rv = PrefixCheck(st, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundFlag)
                                    ) == null
                                )
                                {
                                    if (
                                        (
                                            (
                                                rv = SuffixCheck(st, 0, null, new FlagValue(), Affix.CompoundFlag, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin)
                                            ) != null
                                            ||
                                            (
                                                Affix.CompoundMoreSuffixes
                                                &&
                                                (
                                                    rv = SuffixCheckTwoSfx(st, 0, null, Affix.CompoundFlag)
                                                ) != null
                                            )
                                        )
                                        &&
                                        !huMovRule
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
                                                rv = SuffixCheck(st, 0, null, default(FlagValue), Affix.CompoundBegin, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin)
                                            ) != null
                                            ||
                                            (
                                                Affix.CompoundMoreSuffixes
                                                &&
                                                (
                                                    rv = SuffixCheckTwoSfx(st, 0, null, Affix.CompoundBegin)
                                                ) != null
                                            )
                                            || // twofold suffixes + compound
                                            (
                                                rv = PrefixCheck(st, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundBegin)
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
                                                rv = SuffixCheck(st, 0, null, default(FlagValue), Affix.CompoundMiddle, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin)
                                            ) != null
                                            ||
                                            (
                                                Affix.CompoundMoreSuffixes
                                                &&
                                                (
                                                    rv = SuffixCheckTwoSfx(st, 0, null, Affix.CompoundMiddle)
                                                ) != null
                                            )
                                            || // twofold suffixes + compound
                                            (
                                                rv = PrefixCheck(st, huMovRule ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundMiddle)
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
                                !huMovRule
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
                                !huMovRule
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
                                !huMovRule
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
                                            huMovRule
                                            && // LANG_hu section: spec. Hungarian rule
                                            Affix.IsHungarian
                                            && // XXX hardwired Hungarian dictionary codes
                                            rv.ContainsAnyFlags(SpecialFlags.LetterF, SpecialFlags.LetterG, SpecialFlags.LetterH)
                                        ) // END of LANG_hu section
                                    )
                                    && // test CHECKCOMPOUNDPATTERN conditions
                                    (
                                        scpd == 0
                                        ||
                                        scpdPatternEntry.Condition.IsZero
                                        ||
                                        rv.ContainsFlag(scpdPatternEntry.Condition)
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
                                    huMovRule
                                    &&
                                    Affix.IsHungarian
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
                                if (Affix.IsHungarian)
                                {
                                    // calculate syllable number of the word
                                    numSyllable += GetSyllable(st.Subslice(i));

                                    // - affix syllable num.
                                    // XXX only second suffix (inflections, not derivations)
                                    if (SuffixAppend != null)
                                    {
                                        numSyllable -= GetSyllable(SuffixAppend.Reverse()) + SuffixExtraInt;
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

                                    var homonymEnumerator = Lookup(st.Substring(i)).GetEnumerator();  // perhaps without prefix

                                    rv = homonymEnumerator.MoveNext() ? homonymEnumerator.Current : null;

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
                                                    Affix.CompoundRules.HasItems
                                                    && words != null
                                                    && DefCompoundCheck(words, wnum + 1, rv, true)
                                                )
                                            )
                                            ||
                                            (
                                                scpd != 0
                                                && scpdPatternEntry.Condition2.HasValue
                                                && !rv.ContainsFlag(scpdPatternEntry.Condition2)
                                            )
                                        )
                                    )
                                    {
                                        rv = homonymEnumerator.MoveNext() ? homonymEnumerator.Current : null;
                                    }

                                    // check FORCEUCASE
                                    if (
                                        rv != null
                                        &&
                                        rv.ContainsFlag(Affix.ForceUpperCase)
                                        &&
                                        !EnumEx.HasFlag(info, SpellCheckResultType.OrigCap)
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
                                            Affix.CompoundPatterns.IsEmpty
                                            ||
                                            scpd != 0
                                            ||
                                            !Affix.CompoundPatterns.Check(word, i, rvFirst, rv, false)
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
                                            scpdPatternEntry.Condition2.IsZero
                                            ||
                                            rv.ContainsFlag(scpdPatternEntry.Condition2)
                                        )
                                    )
                                    {
                                        st.Destroy();

                                        // forbid compound word, if it is a non compound word with typical fault
                                        if (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Subslice(0, len)))
                                        {
                                            return null;
                                        }

                                        return rvFirst;
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
                                        if (rv != null && DefCompoundCheck(words, wnum + 1, rv, true))
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
                                            scpdPatternEntry.Condition2.IsZero
                                            ||
                                            rv.ContainsFlag(scpdPatternEntry.Condition2)
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
                                        Affix.CompoundPatterns.HasItems
                                        &&
                                        Affix.CompoundPatterns.Check(word, i, rvFirst, rv, affixed)
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
                                        !EnumEx.HasFlag(info, SpellCheckResultType.OrigCap)
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

                                    if (Affix.IsHungarian)
                                    {
                                        // calculate syllable number of the word
                                        numSyllable += GetSyllable(word.Substring(i));

                                        // - affix syllable num.
                                        // XXX only second suffix (inflections, not derivations)
                                        if (SuffixAppend != null)
                                        {
                                            numSyllable -= GetSyllable(SuffixAppend.Reverse()) + SuffixExtraInt;
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
                                        rv = CompoundCheck(st.Subslice(i), wordNum + 1, numSyllable, maxwordnum, wnum + 1, words, ref rwords, false, isSug, ref info);

                                        if (
                                            rv != null
                                            && Affix.CompoundPatterns.HasItems
                                            &&
                                            (
                                                (scpd == 0 && Affix.CompoundPatterns.Check(word, i, rvFirst, rv, affixed))
                                                ||
                                                (scpd != 0 && !Affix.CompoundPatterns.Check(word, i, rvFirst, rv, affixed))
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
                                                    var rv2 = Lookup(word).FirstOrDefault();

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
                    while (Affix.CompoundRules.HasItems && oldwordnum == 0 && !BoolEx.PostfixIncrement(ref onlycpdrule));
                }

                st.Destroy();
                return null;
            }

            /// <summary>
            /// Check if word with affixes is correctly spelled.
            /// </summary>
            private WordEntry AffixCheck(string word, FlagValue needFlag, CompoundOptions inCompound)
            {
                // check all prefixes (also crossed with suffixes if allowed)
                var rv = PrefixCheck(word, inCompound, needFlag);
                if (rv == null)
                {
                    // if still not found check all suffixes
                    rv = SuffixCheck(word, 0, null, default(FlagValue), needFlag, inCompound);

                    if (Affix.ContClasses.HasItems)
                    {
                        ClearSuffix();
                        ClearPrefix();

                        if (rv == null)
                        {
                            rv =
                                // if still not found check all two-level suffixes
                                SuffixCheckTwoSfx(word, 0, null, needFlag)
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
                foreach (var pe in Affix.Prefixes.AffixesWithEmptyKeys)
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
                        rv = CheckWordPrefix(pe, word, inCompound, needFlag);
                        if (rv != null)
                        {
                            SetPrefix(pe);
                            return rv;
                        }
                    }
                }

                // now handle the general case
                foreach (var pptr in Affix.Prefixes.GetMatchingAffixes(word))
                {
                    if (
                        // fogemorpheme
                        (inCompound != CompoundOptions.Not || !pptr.ContainsContClass(Affix.OnlyInCompound))
                        &&
                        // permit prefixes in compounds
                        (!isEndCompound || pptr.ContainsContClass(Affix.CompoundPermitFlag))
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
                foreach (var pe in Affix.Prefixes.AffixesWithEmptyKeys)
                {
                    rv = CheckTwoSfx(pe, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        return rv;
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
            private WordEntry CheckTwoSfx(AffixEntryWithDetail<PrefixEntry> pe, string word, CompoundOptions inCompound, FlagValue needFlag)
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

                    var tmpword = StringEx.ConcatSubstring(pe.Strip, word, pe.Append.Length, word.Length - pe.Append.Length);

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then check if resulting
                    // root word in the dictionary

                    if (TestCondition(pe.AffixEntry, tmpword))
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

            protected WordEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryWithDetail<PrefixEntry> pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
            {
                WordEntry rv;

                if (Affix.Suffixes.IsEmpty)
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

                // first handle the special case of 0 length suffixes
                foreach (var se in Affix.Suffixes.AffixesWithEmptyKeys)
                {
                    // suffixes are not allowed in beginning of compounds
                    if (
                        (
                            cclass.IsZero
                            ||
                            se.HasContClasses
                        )
                        &&
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
                            ||
                            !se.ContainsContClass(Affix.NeedAffix)
                            ||
                            (
                                pfx != null
                                &&
                                !pfx.ContainsContClass(Affix.NeedAffix)
                            )
                        )
                        &&
                        (
                            Affix.Circumfix.IsZero
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
                                &&
                                pfx.ContainsContClass(Affix.Circumfix)
                                &&
                                se.ContainsContClass(Affix.Circumfix)
                            )
                        )
                    )
                    {
                        rv = CheckWordSuffix(se, word, sfxOpts, pfx, cclass, needFlag, checkWordCclassFlag);
                        if (rv != null)
                        {
                            SetSuffix(se);
                            return rv;
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
                    if (
                        (
                            // suffixes are not allowed in beginning of compounds
                            !isBeginCompound
                            ||
                            // except when signed with compoundpermitflag flag
                            sptr.ContainsContClass(Affix.CompoundPermitFlag)
                        )
                        &&
                        // fogemorpheme
                        (
                            inCompound != CompoundOptions.Not
                            ||
                            !sptr.ContainsContClass(Affix.OnlyInCompound)
                        )
                        &&
                        (
                            inCompound != CompoundOptions.End
                            ||
                            pfx != null
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
                        &&
                        (
                            Affix.Circumfix.IsZero
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
                                &&
                                pfx.ContainsContClass(Affix.Circumfix)
                                &&
                                sptr.ContainsContClass(Affix.Circumfix)
                            )
                        )
                    )
                    {
                        rv = CheckWordSuffix(sptr, word, sfxOpts, pfx, cclass, needFlag, checkWordCclassFlag);
                        if (rv != null)
                        {
                            SetSuffix(sptr);
                            SuffixFlag = sptr.AFlag;

                            if (!sptr.HasContClasses)
                            {
                                SetSuffixAppend(sptr.Key);
                            }
                            else if (
                                Affix.IsHungarian
                                && sptr.Key.Length >= 2
                                && sptr.Key[0] == 'i'
                                && sptr.Key[1] != 'y'
                                && sptr.Key[1] != 't'
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
            protected WordEntry SuffixCheckTwoSfx(string word, AffixEntryOptions sfxopts, AffixEntryWithDetail<PrefixEntry> pfx, FlagValue needflag)
            {
                WordEntry rv;

                // first handle the special case of 0 length suffixes
                foreach (var se in Affix.Suffixes.AffixesWithEmptyKeys)
                {
                    if (Affix.ContClasses.Contains(se.AFlag))
                    {
                        rv = CheckTwoSfx(se, word, sfxopts, pfx, needflag);
                        if (rv != null)
                        {
                            return rv;
                        }
                    }
                }

                // now handle the general case
                if (string.IsNullOrEmpty(word))
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
                            if (!sptr.HasContClasses)
                            {
                                SetSuffixAppend(sptr.Key);
                            }

                            return rv;
                        }
                    }
                }

                return null;
            }

            protected WordEntrySet Lookup(string word) => WordList.FindEntriesByRootWord(word);

            /// <summary>
            /// Compound check patterns.
            /// </summary>
            private bool DefCompoundCheck(ref Dictionary<int, WordEntry> words, int wnum, WordEntry rv, Dictionary<int, WordEntry> def, bool all)
            {
                var givenWordsWasNull = words == null;
                if (givenWordsWasNull)
                {
                    words = def;

                    if (words == null)
                    {
                        return false;
                    }
                }

                var result = DefCompoundCheck(words, wnum, rv, all);

                if (!result && givenWordsWasNull)
                {
                    words = null;
                }

                return result;
            }

            /// <summary>
            /// Compound check patterns.
            /// </summary>
            private bool DefCompoundCheck(Dictionary<int, WordEntry> words, int wnum, WordEntry rv, bool all)
            {
                words[wnum] = rv;

                // has the last word COMPOUNDRULE flag?
                if (!rv.HasFlags)
                {
                    words[wnum] = null;
                    return false;
                }

                var ok = Affix.CompoundRules.EntryContainsRuleFlags(rv);
                if (!ok)
                {
                    words[wnum] = null;
                    return false;
                }

                if (Affix.CompoundRules.CompoundCheck(words, wnum, all))
                {
                    return true;
                }

                words[wnum] = null;
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
                var num = 0;

                if (Affix.CompoundMaxSyllable != 0 && Affix.CompoundVowels.HasItems)
                {
                    for (var i = 0; i < word.Length; i++)
                    {
                        if (Affix.CompoundVowels.Contains(word[i]))
                        {
                            num++;
                        }
                    }
                }

                return num;
            }

            /// <summary>
            /// Calculate number of syllable for compound-checking.
            /// </summary>
            private int GetSyllable(StringSlice word)
            {
                var num = 0;

                if (Affix.CompoundMaxSyllable != 0 && Affix.CompoundVowels.HasItems)
                {
                    for (var i = 0; i < word.Length; i++)
                    {
                        if (Affix.CompoundVowels.Contains(word.Text[word.Offset + i]))
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
            private bool CompoundReplacementCheck(string word)
            {
                if (word.Length < 2 || Affix.Replacements.IsEmpty)
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

                            rIndex = word.IndexOf(replacementEntry.Pattern, rIndex + 1, StringComparison.Ordinal); // search for the next letter
                        }
                    }
                }

                return false;
            }

            private WordEntry CheckWordPrefix(AffixEntryWithDetail<PrefixEntry> entry, string word, CompoundOptions inCompound, FlagValue needFlag)
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

                    if (TestCondition(entry.AffixEntry, tmpword))
                    {
                        foreach (var dictionaryEntry in Lookup(tmpword))
                        {
                            if (
                                dictionaryEntry.ContainsFlag(entry.AFlag)
                                &&
                                !entry.ContainsContClass(Affix.NeedAffix) // forbid single prefixes with needaffix flag
                                &&
                                (
                                    needFlag.IsZero
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

                        if (EnumEx.HasFlag(entry.Options, AffixEntryOptions.CrossProduct))
                        {
                            var he = SuffixCheck(tmpword, AffixEntryOptions.CrossProduct, entry, default(FlagValue), needFlag, inCompound);
                            if (he != null)
                            {
                                return he;
                            }
                        }

                    }
                }

                return null;
            }

            private WordEntry CheckWordSuffix(AffixEntryWithDetail<SuffixEntry> entry, string word, AffixEntryOptions optFlags, AffixEntryWithDetail<PrefixEntry> pfx, FlagValue cclass, FlagValue needFlag, FlagValue badFlag)
            {
                // if this suffix is being cross checked with a prefix
                // but it does not support cross products skip it

                var optFlagsHasCrossProduct = EnumEx.HasFlag(optFlags, AffixEntryOptions.CrossProduct);
                if (
                    (optFlagsHasCrossProduct && !EnumEx.HasFlag(entry.Options, AffixEntryOptions.CrossProduct))
                    ||
                    (cclass.HasValue && !entry.ContainsContClass(cclass)) // ! handle cont. class
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

                var tmpl = word.Length - entry.Append.Length;
                // the second condition is not enough for UTF-8 strings
                // it checked in test_condition()

                if (
                    (tmpl > 0 || (Affix.FullStrip && tmpl == 0))
                    &&
                    (tmpl + entry.Strip.Length >= entry.Conditions.Count)
                )
                {
                    // generate new root word by removing suffix and adding
                    // back any characters that would have been stripped or
                    // or null terminating the shorter string

                    var tmpstring = string.IsNullOrEmpty(entry.Strip)
                        ? word.Subslice(0, tmpl)
                        : StringSlice.Create(StringEx.ConcatSubstring(word, 0, tmpl, entry.Strip));

                    // now make sure all of the conditions on characters
                    // are met.  Please see the appendix at the end of
                    // this file for more info on exactly what is being
                    // tested

                    // if all conditions are met then check if resulting
                    // root word in the dictionary
                    if (entry.Conditions.IsEndingMatch(tmpstring))
                    {
                        foreach (var he in Lookup(tmpstring.ToString()))
                        {
                            if (
                                (
                                    he.ContainsFlag(entry.AFlag)
                                    ||
                                    (
                                        pfx != null
                                        &&
                                        pfx.ContainsContClass(entry.AFlag)
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
                                            he.ContainsFlag(pfx.AFlag)
                                            ||
                                            entry.ContainsContClass(pfx.AFlag) // enabled by prefix
                                        )
                                    )
                                )
                                && // check only in compound homonyms (bad flags)
                                !he.ContainsFlag(badFlag)
                                && // handle required flag
                                (
                                    needFlag.IsZero
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
            private WordEntry CheckTwoSfx(AffixEntryWithDetail<SuffixEntry> se, string word, AffixEntryOptions optflags, AffixEntryWithDetail<PrefixEntry> ppfx, FlagValue needflag)
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
                    if (TestCondition(se.AffixEntry, tmpword))
                    {
                        var he = ppfx != null && se.ContainsContClass(ppfx.AFlag)
                            // handle conditional suffix
                            ? SuffixCheck(tmpword, AffixEntryOptions.None, null, se.AFlag, needflag, CompoundOptions.Not)
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
                Lookup(word).HasItems || AffixCheck(word, default(FlagValue), CompoundOptions.Not) != null;

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
                var qIndex = StringEx.CountMatchingFromLeft(src, ' ');

                // now strip off any trailing periods (recording their presence)
                abbv = StringEx.CountMatchingFromRight(src, '.');

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
