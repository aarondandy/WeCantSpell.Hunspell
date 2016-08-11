using Hunspell.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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

        public bool Check()
        {
            return CheckDetails().Correct;
        }

        public SpellCheckResult CheckDetails()
        {
            var word = WordToCheck;

            if (string.IsNullOrEmpty(word) || Dictionary.Entries.Count == 0)
            {
                return new SpellCheckResult(false);
            }

            if (word == DefaultXmlToken)
            {
                return new SpellCheckResult(true);
            }

            string wordToClean;
            if (!Affix.InputConversions.IsEmpty)
            {
                ConvertInput(word, out wordToClean);
            }
            else
            {
                wordToClean = word;
            }

            CapitalizationType capType;
            int abbv;
            string scw;
            var wl = CleanWord2(out scw, wordToClean, out capType, out abbv);
            if (wl == 0)
            {
                return new SpellCheckResult(false);
            }

            if (IsNumericWord(word))
            {
                return new SpellCheckResult(true);
            }

            DictionaryEntry rv;
            var resultType = SpellCheckResultType.None;
            var root = string.Empty;

            switch (capType)
            {
                case CapitalizationType.Huh:
                case CapitalizationType.HuhInit:
                case CapitalizationType.None:
                    if (capType == CapitalizationType.HuhInit)
                    {
                        resultType |= SpellCheckResultType.OrigCap;
                    }

                    rv = CheckWord(scw, ref resultType, out root);
                    if (rv == null && abbv != 0)
                    {
                        scw += ".";
                        rv = CheckWord(scw, ref resultType, out root);
                    }

                    break;
                case CapitalizationType.All:
                    throw new NotImplementedException();
                case CapitalizationType.Init:
                    throw new NotImplementedException();
                default:
                    throw new NotSupportedException(capType.ToString());
            }

            if (rv != null)
            {
                var isFound = true;
                if (rv.ContainsFlag(Affix.Warn))
                {
                    if (Affix.ForbiddenWord.HasValue)
                    {
                        isFound = false;
                    }
                }

                return new SpellCheckResult(root, resultType, isFound);
            }

            // recursive breaking at break points
            if (!Affix.BreakTable.IsDefaultOrEmpty)
            {
                int nbr = 0;
                wl = scw.Length;

                // calculate break points for recursion limit
                for (var j = 0; j < Affix.BreakTable.Length; j++)
                {
                    var breakEntry = Affix.BreakTable[j];
                    int pos = 0;
                    while ((pos = scw.IndexOf(breakEntry, pos)) >= 0)
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
                for (var j = 0; j < Affix.BreakTable.Length; j++)
                {
                    var breakEntry = Affix.BreakTable[j];
                    var plen = breakEntry.Length;
                    if (plen == 1 || plen > wl)
                    {
                        continue;
                    }

                    if (
                        breakEntry.StartsWith('^')
                        && scw.Substring(0, plen - 1) == breakEntry.Substring(1)
                        && Check(scw.Substring(plen - 1))
                    )
                    {
                        return new SpellCheckResult(root, resultType, true);
                    }

                    if (
                        breakEntry.EndsWith('$')
                        && scw.Substring(wl - plen + 1, plen - 1) == breakEntry.Substring(0, plen - 1)
                    )
                    {
                        var suffix = scw.Substring(wl - plen + 1);

                        scw = scw.Substring(0, wl - plen + 1);

                        if (Check(scw))
                        {
                            return new SpellCheckResult(root, resultType, true);
                        }

                        scw += suffix;
                    }
                }

                // other patterns
                for (var j = 0; j < Affix.BreakTable.Length; j++)
                {
                    var breakEntry = Affix.BreakTable[j];
                    var plen = breakEntry.Length;
                    var found = scw.IndexOf(breakEntry);

                    if (found > 0 && found < wl - plen)
                    {
                        if (!Check(scw.Substring(found + plen)))
                        {
                            continue;
                        }

                        var suffix = scw.Substring(found);
                        scw = scw.Substring(found);

                        // examine 2 sides of the break point
                        if (Check(scw))
                        {
                            return new SpellCheckResult(root, resultType, true);
                        }

                        scw += suffix;

                        // LANG_hu: spec. dash rule
                        if (Affix.Culture.IsHungarianLanguage() && breakEntry == "-")
                        {
                            suffix = scw.Substring(found + 1);
                            scw = scw.Substring(0, found + 1);
                            if (Check(scw))
                            {
                                return new SpellCheckResult(root, resultType, true);
                            }

                            scw += suffix;
                        }
                    }
                }
            }

            return new SpellCheckResult(root, resultType, false);
        }

        private bool Check(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Check();
        }

        private DictionaryEntry CheckWord(string w, ref SpellCheckResultType info, out string root)
        {
            root = string.Empty;
            var useBuffer = false;
            string w2;
            string word;

            if (!Affix.IgnoredChars.IsEmpty)
            {
                w2 = w;
                w2 = w2.RemoveChars(Affix.IgnoredChars);
                word = w2;
                useBuffer = true;
            }
            else
            {
                w2 = string.Empty;
                word = w;
            }

            if (string.IsNullOrEmpty(word))
            {
                return null;
            }

            if (Affix.ComplexPrefixes)
            {
                if (!useBuffer)
                {
                    w2 = word;
                    useBuffer = false;
                }

                w2 = w2.Reverse();
            }

            if (useBuffer)
            {
                word = w2;
            }

            DictionaryEntry he = null;

            // look word in hash table
            ImmutableArray<DictionaryEntry> entries;
            if (Dictionary.Entries.TryGetValue(word, out entries))
            {
                // TODO: this loop can be optimized away if !Affix.ForbiddenWord.HasValue : to `he = entries.LastOrDefault();`
                foreach (var entry in entries)
                {
                    he = entry;

                    // check forbidden and onlyincompound words
                    if (entry.ContainsFlag(Affix.ForbiddenWord))
                    {
                        info |= SpellCheckResultType.Forbidden;

                        if (entry.ContainsFlag(Affix.CompoundFlag) && Affix.Culture.IsHungarianLanguage())
                        {
                            info |= SpellCheckResultType.Compound;
                        }

                        return null;
                    }
                }
            }

            // check with affixes
            if (he == null)
            {
                // try stripping off affixes
                he = AffixCheck(word, new FlagValue(), CompoundOptions.Not);

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
                    var rwords = new List<DictionaryEntry>();
                    he = CompoundCheck(word, 0, 0, int.MaxValue, 0, null, ref rwords, 0, 0, ref info);

                    // LANG_hu section: `moving rule' with last dash
                    if (he != null && word.EndsWith('-') && Affix.Culture.IsHungarianLanguage())
                    {
                        var dup = word.Substring(0, word.Length - 1);
                        he = CompoundCheck(dup, -5, 0, int.MaxValue, 0, null, ref rwords, 1, 0, ref info);
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

        private DictionaryEntry CompoundCheck(string word, int wordnum, int numsyllable, int maxwordnum, int wnum, List<DictionaryEntry> words, ref List<DictionaryEntry> rwords, int huMovRule, int isSug, ref SpellCheckResultType info)
        {
            int i;
            int oldnumsyllable, oldnumsyllable2, oldwordnum, oldwordnum2;
            DictionaryEntry rv = null;
            DictionaryEntry rv_first = null;
            char ch = '\0';
            int striple = 0;
            int scpd = 0;
            int soldi = 0;
            int oldcmin = 0;
            int oldcmax = 0;
            int oldlen = 0;
            int checkedstriple = 0;
            int affixed = 0; // TODO: consider converting to boolean
            var oldwords = words;
            var len = word.Length;

            int checked_prefix;
            var cmin = Affix.CompoundMin;
            var cmax = word.Length - Affix.CompoundMin + 1;

            var st = word;

            for (i = cmin; i < cmax; i++)
            {
                words = oldwords;
                var onlycpdrule = words != null && words.Count > 0 ? 1 : 0; // TODO: consider converting to boolean
                do // onlycpdrule loop
                {

                    oldnumsyllable = numsyllable;
                    oldwordnum = wordnum;
                    checked_prefix = 0;

                    do // simplified checkcompoundpattern loop
                    {

                        if (scpd > 0)
                        {
                            for (; scpd <= Affix.CompoundPatterns.Length; scpd++)
                            {
                                var pattern3 = Affix.CompoundPatterns[scpd - 1].Pattern3;
                                if (string.IsNullOrEmpty(pattern3) || StringExtensions.EqualsOffset(word, i, pattern3, 0))
                                {
                                    break;
                                }
                            }

                            if (scpd > Affix.CompoundPatterns.Length)
                            {
                                break;
                            }

                            var scpdPatternEntry = Affix.CompoundPatterns[scpd - 1];

                            st = st.ReplaceToEnd(i, scpdPatternEntry.Pattern);

                            soldi = i;
                            i += scpdPatternEntry.Pattern.Length;

                            st = st.ReplaceToEnd(i, scpdPatternEntry.Pattern2);

                            st = st.ReplaceToEnd(i + scpdPatternEntry.Pattern2.Length, word.Substring(soldi + scpdPatternEntry.Pattern3.Length));

                            oldlen = len;
                            len += scpdPatternEntry.Pattern.Length + scpdPatternEntry.Pattern2.Length - scpdPatternEntry.Pattern3.Length;
                            oldcmin = cmin;
                            oldcmax = cmax;
                            cmin = Affix.CompoundMin;
                            cmax = st.Length - Affix.CompoundMin + 1;

                            cmax = len - Affix.CompoundMin + 1;
                        }

                        ch = i < st.Length ? st[i] : '\0';
                        st = st.Substring(0, i);

                        SuffixGroup = null;
                        Suffix = null;
                        PrefixGroup = null;
                        Prefix = null;

                        // FIRST WORD

                        affixed = 1;
                        var searchEntries = Lookup(st); // perhaps without prefix
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
                                        onlycpdrule == 0
                                        && (words == null || words.Count == 0)
                                        && rv.ContainsFlag(Affix.CompoundFlag)
                                    )
                                    ||
                                    (
                                        wordnum == 0
                                        && onlycpdrule == 0
                                        && rv.ContainsFlag(Affix.CompoundBegin)
                                    )
                                    ||
                                    (
                                        wordnum != 0
                                        && onlycpdrule == 0
                                        && (words == null || words.Count == 0)
                                        && rv.ContainsFlag(Affix.CompoundMiddle)
                                    )
                                    ||
                                    (
                                        Affix.HasCompoundRules
                                        && onlycpdrule != 0
                                        &&
                                        (
                                            (
                                                wordnum == 0
                                                && (words == null || words.Count == 0)
                                                && DefCpdCheck(ref words, wnum, rv, ref rwords, 0)
                                            )
                                            ||
                                            (words != null && DefCpdCheck(ref words, wnum, rv, ref rwords, 0))
                                        )
                                    )
                                )
                                ||
                                (
                                    scpd != 0
                                    && Affix.CompoundPatterns[scpd - 1].Condition.HasValue
                                    && !rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition)
                                )
                            )
                        )
                        {
                            searchEntriesIndex++;
                            rv = searchEntriesIndex < searchEntries.Length ? searchEntries[searchEntriesIndex] : null;
                        }

                        if (rv != null)
                        {
                            affixed = 0;
                        }

                        if (rv == null)
                        {
                            if (onlycpdrule != 0)
                            {
                                break;
                            }

                            if (
                                Affix.CompoundFlag.HasValue
                                &&
                                (
                                    rv = PrefixCheck(st.Substring(0, i), huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundFlag)
                                ) == null
                            )
                            {
                                if (
                                    (
                                        (
                                            rv = SuffixCheck(st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundFlag, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st.Substring(0, i), 0, null, null, Affix.CompoundFlag)
                                            ) != null
                                        )
                                    )
                                    && huMovRule == 0
                                    && Suffix != null
                                    && Suffix.ContainsAnyContClass(Affix.CompoundForbidFlag, Affix.CompoundEnd)
                                )
                                {
                                    rv = null;
                                }
                            }

                            if (
                                rv != null
                                ||
                                (
                                    wordnum == 0
                                    && Affix.CompoundBegin.HasValue
                                    &&
                                    (
                                        (
                                            rv = SuffixCheck(st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundBegin, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st.Substring(0, i), 0, null, null, Affix.CompoundBegin)
                                            ) != null
                                        )
                                        ||  // twofold suffixes + compound
                                        (
                                            rv = PrefixCheck(st.Substring(0, i), huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundBegin)
                                        ) != null
                                    )
                                )
                                ||
                                (
                                    wordnum > 0
                                    && Affix.CompoundMiddle.HasValue
                                    &&
                                    (
                                        (
                                            rv = SuffixCheck(st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundMiddle, huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)
                                        ) != null
                                        ||
                                        (
                                            Affix.CompoundMoreSuffixes
                                            &&
                                            (
                                                rv = SuffixCheckTwoSfx(st.Substring(0, i), 0, null, null, Affix.CompoundMiddle)
                                            ) != null
                                        )
                                        ||  // twofold suffixes + compound
                                        (
                                            rv = PrefixCheck(st.Substring(0, i), huMovRule != 0 ? CompoundOptions.Other : CompoundOptions.Begin, Affix.CompoundMiddle)
                                        ) != null
                                    )
                                )
                            )
                            {
                                checked_prefix = 1;
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
                            st = ch == '\0'
                                ? st.Substring(0, i)
                                : st.SetChar(ch, i);

                            break;
                        }

                        // check non_compound flag in suffix and prefix
                        if (
                            rv != null
                            && huMovRule == 0
                            && Affix.CompoundForbidFlag.HasValue
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundForbidFlag))
                                || // TODO: pfx and sfx have shared state bugs
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundForbidFlag))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check compoundend flag in suffix and prefix
                        if (
                            rv != null
                            && checked_prefix == 0
                            && Affix.CompoundEnd.HasValue
                            && huMovRule == 0
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundEnd))
                                || // TODO: pfx and sfx have shared state bugs
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundEnd))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check compoundmiddle flag in suffix and prefix
                        if (
                            rv != null
                            && checked_prefix == 0
                            && wordnum == 0
                            && Affix.CompoundMiddle.HasValue
                            && huMovRule == 0
                            &&
                            (
                                (Prefix != null && Prefix.ContainsContClass(Affix.CompoundMiddle))
                                || // TODO: pfx and sfx have shared state bugs
                                (Suffix != null && Suffix.ContainsContClass(Affix.CompoundMiddle))
                            )
                        )
                        {
                            rv = null;
                        }

                        // check forbiddenwords
                        if (
                            rv != null
                            && rv.HasFlags
                            &&
                            (
                                rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                ||
                                (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                            )
                        )
                        {
                            return null;
                        }

                        // increment word number, if the second root has a compoundroot flag
                        if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                        {
                            wordnum++;
                        }

                        // first word is acceptable in compound words?

                        if (
                            (
                                rv != null
                                &&
                                (
                                    checked_prefix != 0
                                    ||
                                    (
                                        words != null
                                        && wnum < words.Count
                                        && words[wnum] != null
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
                                        // LANG_hu section: spec. Hungarian rule
                                        && Affix.Culture.IsHungarianLanguage()
                                        // XXX hardwired Hungarian dictionary codes
                                        && rv.ContainsAnyFlags(SpecialFlags.LetterF, SpecialFlags.LetterG, SpecialFlags.LetterH)
                                    ) // END of LANG_hu section
                                )
                                && // test CHECKCOMPOUNDPATTERN conditions
                                (
                                    scpd == 0
                                    || !Affix.CompoundPatterns[scpd - 1].Condition.HasValue
                                    || rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition)
                                )
                                &&
                                !(
                                    (
                                        Affix.CheckCompoundTriple
                                        && scpd == 0
                                        && (words == null || words.Count == 0)
                                        && (word[i - 1] == word[i]) // test triple letters
                                        &&
                                        (
                                            (i > 1 && word[i - 1] == word[i - 2])
                                            ||
                                            (word[i - 1] == word[i + 1])  // may be word[i+1] == '\0'
                                        )
                                    )
                                    ||
                                    (
                                        Affix.CheckCompoundCase
                                        && scpd == 0
                                        && (words == null || words.Count == 0)
                                        && CompoundCaseCheck(word, i)
                                    )
                                )
                            )
                            || // LANG_hu section: spec. Hungarian rule
                            (
                                rv == null
                                && Affix.Culture.IsHungarianLanguage()
                                && huMovRule != 0
                                &&
                                (
                                    rv = AffixCheck(st.Substring(0, i), new FlagValue(), CompoundOptions.Not)
                                ) != null
                                && Suffix != null // XXX hardwired Hungarian dic. codes
                                && Suffix.ContainsAnyContClass(SpecialFlags.LetterXLower, SpecialFlags.LetterPercent)
                            )
                        )
                        {
                            // first word is ok condition
                            if (Affix.Culture.IsHungarianLanguage())
                            {
                                // calculate syllable number of the word
                                numsyllable += GetSyllable(st.Substring(i));

                                // - affix syllable num.
                                // XXX only second suffix (inflections, not derivations)
                                if (SuffixAppend != null)
                                {
                                    var tmp = SuffixAppend;
                                    tmp = tmp.Reverse();
                                    numsyllable -= GetSyllable(tmp) + SuffixExtra;
                                }

                                // + 1 word, if syllable number of the prefix > 1 (hungarian
                                // convention)
                                if (Prefix != null && GetSyllable(Prefix.Key) > 1)
                                {
                                    // TODO: pfx shared state bug
                                    wordnum++;
                                }
                            }

                            // NEXT WORD(S)
                            rv_first = rv;
                            st = st.SetChar(ch, i);

                            do
                            {
                                // striple loop

                                // check simplifiedtriple
                                if (Affix.SimplifiedTriple)
                                {
                                    if (striple != 0)
                                    {
                                        checkedstriple = 1;
                                        i--; // check "fahrt" instead of "ahrt" in "Schiffahrt"
                                    }
                                    else if (i > 2 && word[i - 1] == word[i - 2])
                                    {
                                        striple = 1;
                                    }
                                }

                                var homonyms = Lookup(st.Substring(i));  // perhaps without prefix
                                var homonymIndex = 0;
                                List<DictionaryEntry> junkDictionaryEntryList = null;

                                rv = homonymIndex < homonyms.Length ? homonyms[homonymIndex] : null;
                                // search homonym with compound flag
                                while (
                                    rv != null
                                    &&
                                    (
                                        rv.ContainsFlag(Affix.NeedAffix)
                                        ||
                                        !(
                                            ((words == null || words.Count == 0) && rv.ContainsFlag(Affix.CompoundFlag))
                                            ||
                                            ((words == null || words.Count == 0) && rv.ContainsFlag(Affix.CompoundEnd))
                                            ||
                                            (
                                                Affix.HasCompoundRules
                                                && words != null
                                                && words.Count != 0
                                                && DefCpdCheck(ref words, wnum + 1, rv, ref junkDictionaryEntryList, 1)
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
                                    && rv.ContainsFlag(Affix.ForceUpperCase)
                                    && !info.HasFlag(SpellCheckResultType.OrigCap)
                                )
                                {
                                    rv = null;
                                }

                                if (
                                    rv != null
                                    && words != null
                                    && words.Count != 0
                                    && words[wnum + 1] != null
                                )
                                {
                                    return rv_first;
                                }

                                oldnumsyllable2 = numsyllable;
                                oldwordnum2 = wordnum;

                                if (
                                    rv != null
                                    && Affix.Culture.IsHungarianLanguage()
                                    && rv.ContainsFlag(SpecialFlags.LetterI)
                                    && !rv.ContainsFlag(SpecialFlags.LetterJ)
                                )
                                {
                                    numsyllable--;
                                }

                                // increment word number, if the second root has a compoundroot flag
                                if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                                {
                                    wordnum++;
                                }

                                // check forbiddenwords
                                if (
                                    rv != null
                                    && rv.HasFlags
                                    &&
                                    (
                                        rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                        ||
                                        (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                                    )
                                )
                                {
                                    return null;
                                }

                                // second word is acceptable, as a root?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist of 2 words, or if more,
                                // then the syllable number of root words must be 6, or lesser.

                                if (
                                    rv != null
                                    && rv.ContainsAnyFlags(Affix.CompoundFlag, Affix.CompoundEnd)
                                    &&
                                    (
                                        Affix.CompoundWordMax == -1
                                        ||
                                        wordnum + 1 < Affix.CompoundWordMax
                                        ||
                                        (
                                            Affix.CompoundMaxSyllable != 0
                                            &&
                                            numsyllable + GetSyllable(rv.Word) <= Affix.CompoundMaxSyllable
                                        )
                                    )
                                    && // test CHECKCOMPOUNDPATTERN
                                    (
                                        !Affix.HasCompoundPatterns
                                        || scpd != 0
                                        || !CompoundPatternCheck(word, i, rv_first, rv, 0)
                                    )
                                    &&
                                    (
                                        !Affix.CheckCompoundDup || rv != rv_first
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
                                    // forbid compound word, if it is a non compound word with typical fault
                                    if (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Substring(0, len)))
                                    {
                                        return null;
                                    }

                                    return rv_first;
                                }

                                numsyllable = oldnumsyllable2;
                                wordnum = oldwordnum2;

                                // perhaps second word has prefix or/and suffix
                                ClearSuffix();
                                SuffixFlag = default(FlagValue);

                                rv = (onlycpdrule == 0 && Affix.CompoundFlag.HasValue)
                                     ? AffixCheck(word.Substring(i), Affix.CompoundFlag, CompoundOptions.End)
                                     : null;

                                if (rv == null && Affix.CompoundEnd.HasValue && onlycpdrule == 0)
                                {
                                    ClearSuffix();
                                    ClearPrefix();
                                    rv = AffixCheck(word.Substring(i), Affix.CompoundEnd, CompoundOptions.End);
                                }

                                if (rv == null && Affix.HasCompoundRules && words != null && words.Count != 0)
                                {
                                    rv = AffixCheck(word.Substring(i), new FlagValue(), CompoundOptions.End);
                                    List<DictionaryEntry> junkEntries = null;
                                    if (rv != null && DefCpdCheck(ref words, wnum + 1, rv, ref junkEntries, 1))
                                    {
                                        return rv_first;
                                    }

                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (allowed forms)
                                if (
                                    rv != null
                                    && scpd != 0
                                    && Affix.CompoundPatterns[scpd - 1].Condition2.HasValue
                                    && !rv.ContainsFlag(Affix.CompoundPatterns[scpd - 1].Condition2)
                                )
                                {
                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (forbidden compounds)
                                if (
                                    rv != null
                                    && scpd == 0
                                    && Affix.HasCompoundPatterns
                                    && CompoundPatternCheck(word, i, rv_first, rv, affixed)
                                )
                                {
                                    rv = null;
                                }

                                // check non_compound flag in suffix and prefix
                                if (
                                    rv != null
                                    && Affix.CompoundForbidFlag.HasValue
                                    &&
                                    (
                                        (Prefix != null && Prefix.ContainsContClass(Affix.CompoundForbidFlag))
                                        || // TODO: pfx and sfx shared state bugs
                                        (Suffix != null && Suffix.ContainsContClass(Affix.CompoundForbidFlag))
                                    )
                                )
                                {
                                    rv = null;
                                }

                                // check FORCEUCASE
                                if (
                                    rv != null
                                    && rv.ContainsFlag(Affix.ForceUpperCase)
                                    && !info.HasFlag(SpellCheckResultType.OrigCap)
                                )
                                {
                                    rv = null;
                                }

                                // check forbiddenwords
                                if (
                                    rv != null
                                    && rv.HasFlags
                                    &&
                                    (
                                        rv.ContainsAnyFlags(Affix.ForbiddenWord, SpecialFlags.OnlyUpcaseFlag)
                                        ||
                                        (isSug != 0 && rv.ContainsFlag(Affix.NoSuggest))
                                    )
                                )
                                {
                                    return null;
                                }

                                // pfxappnd = prefix of word+i, or NULL
                                // calculate syllable number of prefix.
                                // hungarian convention: when syllable number of prefix is more,
                                // than 1, the prefix+word counts as two words.

                                if (Affix.Culture.IsHungarianLanguage())
                                {
                                    throw new NotImplementedException();
                                }

                                // increment word number, if the second word has a compoundroot flag
                                if (rv != null && rv.ContainsFlag(Affix.CompoundRoot))
                                {
                                    wordnum++;
                                }

                                // second word is acceptable, as a word with prefix or/and suffix?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist 2 word, otherwise
                                // the syllable number of root words is 6, or lesser.
                                if (
                                    rv != null
                                    &&
                                    (
                                        Affix.CompoundWordMax == -1
                                        || wordnum + 1 < Affix.CompoundWordMax
                                        ||
                                        (
                                            Affix.CompoundMaxSyllable != 0
                                            && numsyllable <= Affix.CompoundMaxSyllable
                                        )
                                    )
                                    &&
                                    (
                                        !Affix.CheckCompoundDup || rv != rv_first
                                    )
                                )
                                {
                                    // forbid compound word, if it is a non compound word with typical
                                    // fault
                                    if (Affix.CheckCompoundRep && CompoundReplacementCheck(word))
                                    {
                                        return null;
                                    }

                                    return rv_first;
                                }

                                numsyllable = oldnumsyllable2;
                                wordnum = oldwordnum2;

                                // perhaps second word is a compound word (recursive call)
                                if (wordnum < maxwordnum)
                                {
                                    rv = CompoundCheck(st.Substring(i), wordnum + 1, numsyllable, maxwordnum, wnum + 1, words, ref rwords, 0, isSug, ref info);

                                    if (
                                        rv != null
                                        && Affix.HasCompoundPatterns
                                        &&
                                        (
                                            (scpd == 0 && CompoundPatternCheck(word, i, rv_first, rv, affixed))
                                            || // TODO: can these both be optimized as (scpd == 0) == (...) ?
                                            (scpd != 0 && !CompoundPatternCheck(word, i, rv_first, rv, affixed))
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
                                    // forbid compound word, if it is a non compound word with typical
                                    // fault

                                    throw new NotImplementedException();
                                }
                            }
                            while (striple != 0 && checkedstriple == 0);  // end of striple loop

                            if (checkedstriple != 0)
                            {
                                i++;
                                checkedstriple = 0;
                                striple = 0;
                            }

                        }

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
                    while (onlycpdrule == 0 && Affix.SimplifiedCompound && scpd < Affix.CompoundPatterns.Length);

                    scpd = 0;
                    wordnum = oldwordnum;
                    numsyllable = oldnumsyllable;

                    if (soldi != 0)
                    {
                        i = soldi;
                        st = word;
                        soldi = 0;
                    }
                    else
                    {
                        st = st.SetChar(ch, i);
                    }
                }
                while (Affix.CompoundRules.Length != 0 && oldwordnum == 0 && onlycpdrule++ < 1);
            }

            return null;
        }

        private DictionaryEntry AffixCheck(string word, FlagValue needFlag, CompoundOptions inCompound)
        {
            DictionaryEntry rv = null;

            rv = PrefixCheck(word, inCompound, needFlag);
            if (rv != null)
            {
                return rv;
            }

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

        private DictionaryEntry PrefixCheck(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            ClearPrefix();
            PrefixAppend = null;
            SuffixAppend = null;
            SuffixExtra = 0;

            // first handle the special case of 0 length prefixes
            foreach (var affixGroup in Affix.Prefixes)
            {
                foreach (var pfx in affixGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    if (
                        // fogemorpheme
                        (inCompound != CompoundOptions.Not || !pfx.ContainsContClass(Affix.OnlyInCompound))
                        // permit prefixes in compounds
                        && (inCompound != CompoundOptions.End || pfx.ContainsContClass(Affix.CompoundPermitFlag))
                    )
                    {
                        var entry = CheckWordPrefix(affixGroup, pfx, word, inCompound, needFlag);
                        if (entry != null)
                        {
                            PrefixGroup = affixGroup;
                            Prefix = pfx;
                            return entry;
                        }
                    }
                }
            }

            // now handle the general case
            foreach (var affixGroup in Affix.Prefixes)
            {
                foreach (var pfx in affixGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (IsSubset(pfx.Key, word))
                    {
                        if (
                            // fogemorpheme
                            (inCompound != CompoundOptions.Not || !pfx.ContainsContClass(Affix.OnlyInCompound))
                            // permit prefixes in compounds
                            && (inCompound != CompoundOptions.End || pfx.ContainsContClass(Affix.CompoundPermitFlag))
                        )
                        {
                            // check prefix

                            var entry = CheckWordPrefix(affixGroup, pfx, word, inCompound, needFlag);
                            if (entry != null)
                            {
                                PrefixGroup = affixGroup;
                                Prefix = pfx;
                                return entry;
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

            // first handle the special case of 0 length prefixes
            foreach (var prefixGroup in Affix.Prefixes)
            {
                foreach (var pe in prefixGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    var rv = CheckTwoSfx(prefixGroup, pe, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        return rv;
                    }
                }
            }

            foreach (var prefixGroup in Affix.Prefixes)
            {
                foreach (var pptr in prefixGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    var rv = CheckTwoSfx(prefixGroup, pptr, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        Prefix = pptr;
                        return rv;
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

            int tmpl = word.Length - pe.Append.Length; // length of tmpword

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
                    tmpl += pe.Strip.Length;

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

        private string PrefixCheckMorph(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            ClearPrefix();
            SuffixExtra = 0;

            throw new NotImplementedException();
        }

        private string PrefixCheckTwoSfxMorph(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            ClearPrefix();
            SuffixExtra = 0;

            throw new NotImplementedException();
        }

        private DictionaryEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
        {
            if (Affix.Suffixes.Length == 0)
            {
                return null;
            }

            // first handle the special case of 0 length suffixes
            foreach (var affixGroup in Affix.Suffixes)
            {
                foreach (var affixEntry in affixGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    if (!cclass.HasValue || affixEntry.HasContClasses)
                    {
                        // suffixes are not allowed in beginning of compounds
                        if (
                            (
                                inCompound != CompoundOptions.Begin
                                || // except when signed with compoundpermitflag flag
                                affixEntry.ContainsContClass(Affix.CompoundPermitFlag)
                            )
                            &&
                            (
                                !Affix.Circumfix.HasValue
                                || // no circumfix flag in prefix and suffix
                                (
                                    (
                                        pfx == null
                                        ||
                                        !pfx.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    !affixEntry.ContainsContClass(Affix.Circumfix)
                                )
                                || // circumfix flag in prefix AND suffix
                                (
                                    pfx != null
                                    && pfx.ContainsContClass(Affix.Circumfix)
                                    && affixEntry.ContainsContClass(Affix.Circumfix)
                                )
                            )
                            && // fogemorpheme
                            (
                                inCompound != CompoundOptions.Not
                                ||
                                !affixEntry.ContainsContClass(Affix.OnlyInCompound)
                            )
                            && // needaffix on prefix or first suffix
                            (
                                cclass != 0
                                ||
                                !affixEntry.ContainsContClass(Affix.NeedAffix)
                                ||
                                (
                                    pfx != null
                                    &&
                                    !pfx.ContainsContClass(Affix.NeedAffix)
                                )
                            )
                        )
                        {
                            var rv = CheckWordSuffix(affixGroup, affixEntry, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound);
                            if (rv != null)
                            {
                                SuffixGroup = affixGroup;
                                Suffix = affixEntry;
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

            var ep = pfx;

            foreach (var affixGroup in Affix.Suffixes)
            {
                foreach (var sptr in affixGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (word.EndsWith(sptr.Append))
                    {
                        if (
                            (
                                inCompound != CompoundOptions.Begin
                                ||
                                sptr.ContainsContClass(Affix.CompoundPermitFlag) // except when signed with compoundpermitflag flag
                            )
                            &&
                            (
                                !Affix.Circumfix.HasValue
                                || // no circumfix flag in prefix and suffix
                                (
                                    (
                                        pfx == null
                                        ||
                                        !ep.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    !sptr.ContainsContClass(Affix.Circumfix)
                                )
                                || // circumfix flag in prefix AND suffix
                                (
                                    pfx != null
                                    && ep.ContainsContClass(Affix.Circumfix)
                                    && sptr.ContainsContClass(Affix.Circumfix)
                                )
                            )
                            && // fogemorpheme
                            (
                                inCompound != CompoundOptions.Not
                                ||
                                !sptr.ContainsContClass(Affix.OnlyInCompound)
                            )
                            && // needaffix on prefix or first suffix
                            (
                                cclass != 0
                                ||
                                !sptr.ContainsContClass(Affix.NeedAffix)
                                ||
                                (pfx != null && !ep.ContainsContClass(Affix.NeedAffix))
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
                                var rv = CheckWordSuffix(affixGroup, sptr, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound);
                                if (rv != null)
                                {
                                    SuffixGroup = affixGroup;
                                    Suffix = sptr;
                                    SuffixFlag = affixGroup.AFlag;

                                    if (!sptr.HasContClasses)
                                    {
                                        SuffixAppend = sptr.Key;
                                    }
                                    else if (
                                        Affix.Culture.IsHungarianLanguage()
                                        && sptr.Key.Length >= 2
                                        && sptr.Key.StartsWith('i')
                                        && sptr.Key[1] != 'y'
                                        && sptr.Key[1] != 't'
                                    )
                                    {
                                        SuffixExtra = 1;
                                    }

                                    return rv;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private DictionaryEntry SuffixCheckTwoSfx(string word, AffixEntryOptions sfxopts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry ppfx, FlagValue needflag)
        {
            DictionaryEntry rv = null;

            // first handle the special case of 0 length suffixes
            foreach (var suffixGroup in Affix.Suffixes)
            {
                foreach (var se in suffixGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    if (Affix.ContClasses.Contains(suffixGroup.AFlag))
                    {
                        rv = CheckTwoSfx(suffixGroup, se, word, sfxopts, pfxGroup, ppfx, needflag);
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

            foreach (var suffixGroup in Affix.Suffixes)
            {
                foreach (var sptr in suffixGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (word.EndsWith(sptr.Key))
                    {
                        if (Affix.ContClasses.Contains(suffixGroup.AFlag))
                        {
                            rv = CheckTwoSfx(suffixGroup, sptr, word, sfxopts, pfxGroup, ppfx, needflag);
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

        private string SuffixCheckTwoSfxMorph(string word, int sfxopts, PrefixEntry ppfx, FlagValue needFlag)
        {
            throw new NotImplementedException();
        }

        private ImmutableArray<DictionaryEntry> Lookup(string word)
        {
            ImmutableArray<DictionaryEntry> entries;
            return Dictionary.Entries.TryGetValue(word, out entries) ? entries : ImmutableArray<DictionaryEntry>.Empty;
        }

        private bool DefCpdCheck(ref List<DictionaryEntry> words, int wnum, DictionaryEntry rv, ref List<DictionaryEntry> def, int all)
        {
            var w = 0;

            if (words == null || words.Count == 0)
            {
                w = 1;
                words = def;
            }

            if (words == null || words.Count == 0)
            {
                return false;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Forbid compounding with neighbouring upper and lower case characters at word bounds.
        /// </summary>
        private bool CompoundCaseCheck(string word, int b)
        {
            var a = b - 1;

            return a >= 0
                && b < word.Length
                && word[a] != '-'
                && word[b] != '-'
                && (char.IsUpper(word, a) || char.IsUpper(word, b));
        }

        private int GetSyllable(string word)
        {
            if (Affix.CompoundMaxSyllable == 0)
            {
                return 0;
            }

            var num = 0;

            for (var i = 0; i < word.Length; i++)
            {
                if (Affix.CompoundVowels.Contains(word[i]))
                {
                    ++num;
                }
            }

            return num;
        }

        /// <summary>
        /// Forbid compoundings when there are special patterns at word bound.
        /// </summary>
        private bool CompoundPatternCheck(string word, int pos, DictionaryEntry r1, DictionaryEntry r2, int affixed)
        {
            for (var i = 0; i < Affix.CompoundPatterns.Length; i++)
            {
                var patternEntry = Affix.CompoundPatterns[i];

                int len;
                if (
                    IsSubset(patternEntry.Pattern2, word.Substring(pos))
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
                                && word.Substring(pos - r1.Word.Length).StartsWith(r1.Word)
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
                                word.Substring(pos - len).StartsWith(patternEntry.Pattern.Substring(0, len))
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
        /// Is word a non compound with a REP substitution (see checkcompoundrep)?
        /// </summary>
        /// <seealso cref="AffixConfig.CheckCompoundRep"/>
        private bool CompoundReplacementCheck(string word)
        {
            if (word.Length < 2 || !Affix.HasReplacements)
            {
                return false;
            }

            for (var i = 0; i < Affix.Replacements.Length; i++)
            {
                var replacementEntry = Affix.Replacements[i];
                int rIndex = 0;
                var lenp = replacementEntry.Pattern.Length;
                // search every occurence of the pattern in the word
                while ((rIndex = word.IndexOf(replacementEntry.Pattern, rIndex)) >= 0)
                {
                    var candidate = word;

                    var type = rIndex + replacementEntry.Pattern.Length == lenp
                        ? rIndex == 0 ? ReplacementValueType.Isol : ReplacementValueType.Fin
                        : rIndex == 0 ? ReplacementValueType.Ini : ReplacementValueType.Med;

                    var replacement = replacementEntry[type];
                    if (replacement != null)
                    {
                        candidate = candidate.Replace(rIndex, lenp, replacement);

                        if (CandidateCheck(candidate))
                        {
                            return true;
                        }
                    }

                    rIndex++;
                }
            }

            return false;
        }

        private DictionaryEntry CheckWordPrefix(AffixEntryGroup<PrefixEntry> group, PrefixEntry entry, string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            DictionaryEntry he;

            // on entry prefix is 0 length or already matches the beginning of the word.
            // So if the remaining root word has positive length
            // and if there are enough chars in root word and added back strip chars
            // to meet the number of characters conditions, then test it

            var tmpl = word.Length - entry.Append.Length; // length of tmpword

            if (tmpl > 0 || (tmpl == 0 && Affix.FullStrip))
            {
                // generate new root word by removing prefix and adding
                // back any characters that would have been stripped

                var tmpword = entry.Strip;
                tmpword += word.Substring(entry.Append.Length);

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
                        he = SuffixCheck(tmpword, AffixEntryOptions.CrossProduct, group, entry, new FlagValue(), needFlag, inCompound);
                        if (he != null)
                        {
                            return he;
                        }
                    }

                }
            }

            return null;
        }

        private bool IsSubset(string s1, string s2)
        {
            if (s1.Length > s2.Length)
            {
                return false;
            }

            for (var i = 0; i < s1.Length; i++)
            {
                if (s1[i] != '.' && s1[i] != s2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private DictionaryEntry CheckWordSuffix(AffixEntryGroup<SuffixEntry> group, SuffixEntry entry, string word, AffixEntryOptions optFlags, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, FlagValue badFlag)
        {
            var ep = pfx;
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
                (
                    tmpl > 0
                    ||
                    (tmpl == 0 && Affix.FullStrip)
                )
                &&
                tmpl + entry.Strip.Length >= entry.Conditions.Count
            )
            {
                // generate new root word by removing suffix and adding
                // back any characters that would have been stripped or
                // or null terminating the shorter string

                var tmpstring = word.Substring(0, tmpl);

                if (!string.IsNullOrEmpty(entry.Strip))
                {
                    tmpstring += entry.Strip;
                }

                var tmpword = 0;
                var endword = tmpstring.Length;

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
                                (ep != null && ep.ContainsContClass(group.AFlag))
                            )
                            &&
                            (
                                !optFlags.HasFlag(AffixEntryOptions.CrossProduct)
                                ||
                                (ep != null && he.ContainsFlag(pfxGroup.AFlag))
                                || // enabled by prefix
                                (ep != null && entry.ContainsContClass(pfxGroup.AFlag))
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

        private DictionaryEntry CheckTwoSfx(AffixEntryGroup<SuffixEntry> suffixGroup, SuffixEntry se, string word, AffixEntryOptions optflags, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry ppfx, FlagValue needflag)
        {
            var ep = ppfx;

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

            int tmpl = word.Length - se.Append.Length; // length of tmpword

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


                var tmpword = word;
                tmpword = tmpword.Substring(0, Math.Min(tmpl, tmpword.Length));
                tmpword += se.Strip;
                tmpl = tmpword.Length;

                // now make sure all of the conditions on characters
                // are met.  Please see the appendix at the end of
                // this file for more info on exactly what is being
                // tested

                // if all conditions are met then recall suffix_check

                if (TestCondition(se, tmpword))
                {
                    DictionaryEntry he;

                    if (ppfx != null)
                    {
                        // handle conditional suffix
                        if (se.ContainsContClass(pfxGroup.AFlag))
                        {
                            he = SuffixCheck(tmpword, AffixEntryOptions.None, null, null, suffixGroup.AFlag, needflag, CompoundOptions.Not);
                        }
                        else
                        {
                            he = SuffixCheck(tmpword, optflags, pfxGroup, ppfx, suffixGroup.AFlag, needflag, CompoundOptions.Not);
                        }

                        throw new NotImplementedException();
                    }
                    else
                    {
                        he = SuffixCheck(tmpword, AffixEntryOptions.None, null, null, suffixGroup.AFlag, needflag, CompoundOptions.Not);
                    }

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
            var entries = Lookup(word);
            if (!entries.IsDefaultOrEmpty)
            {
                return true;
            }

            var rv = AffixCheck(word, new FlagValue(), CompoundOptions.Not);
            return rv != null;
        }

        [Obsolete("Inline")]
        private bool TestCondition(PrefixEntry entry, string word)
        {
            return entry.Conditions.IsStartingMatch(word);
        }

        [Obsolete("Inline")]
        private bool TestCondition(SuffixEntry entry, string word)
        {
            return entry.Conditions.IsEndingMatch(word);
        }

        private bool ConvertInput(string word, out string dest)
        {
            dest = string.Empty;

            var change = false;
            for (var i = 0; i < word.Length; i++)
            {
                var entry = FindInput(word.Substring(i));
                var l = ReplaceInput(word.Substring(i), entry, i == 0);
                if (l.Length != 0)
                {
                    dest += l;
                    i += entry.Pattern.Length - 1;
                    change = true;
                }
                else
                {
                    dest += word[i];
                }
            }

            return change;
        }

        [Obsolete("Should be moved closer to InputConversions")]
        private MultiReplacementEntry FindInput(string word)
        {
            MultiReplacementEntry entry = null;
            for (var i = 0; i < word.Length - 1; i++)
            {
                if (Affix.InputConversions.TryGetValue(word.Substring(i), out entry))
                {
                    return entry;
                }
            }

            return entry;
        }

        [Obsolete("Should be a member of ReplacementEntry")]
        private string ReplaceInput(string word, ReplacementEntry entry, bool atStart)
        {
            if (entry == null)
            {
                return string.Empty;
            }

            var type = word.Length == entry.Pattern.Length
                ? (atStart ? ReplacementValueType.Isol : ReplacementValueType.Fin)
                : (atStart ? ReplacementValueType.Ini : ReplacementValueType.Med);

            while (type != ReplacementValueType.Med && string.IsNullOrEmpty(entry[type]))
            {
                type = (type == ReplacementValueType.Fin && !atStart) ? ReplacementValueType.Med : type - 1;
            }

            return entry[type];
        }

        private int CleanWord2(out string dest, string src, out CapitalizationType capType, out int abbv)
        {
            dest = string.Empty;

            int q = 0;

            // first skip over any leading blanks
            while (q < src.Length && src[q] == ' ')
            {
                q++;
            }

            // now strip off any trailing periods (recording their presence)
            abbv = 0;
            var nl = src.Length - q;
            while (nl > 0 && src[q + nl - 1] == '.')
            {
                nl--;
                abbv++;
            }

            // if no characters are left it can't be capitalized
            if (nl <= 0)
            {
                capType = CapitalizationType.None;
                return 0;
            }

            dest = src.Substring(q, nl);
            nl = dest.Length;

            capType = CapitalizationTypeUtilities.GetCapitalizationType(dest);

            return nl;
        }

        private bool IsNumericWord(string word)
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
