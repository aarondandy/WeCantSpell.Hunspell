using Hunspell.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Hunspell
{
    public class Hunspell
    {
        private const string DefaultXmlToken = "<?xml?>";

        public Hunspell(Dictionary dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            Dictionary = dictionary;
        }

        public Dictionary Dictionary { get; }

        public AffixConfig Affix => Dictionary.Affix;

        public bool Check(string word)
        {
            return CheckDetails(word).Correct;
        }

        public SpellCheckResult CheckDetails(string word)
        {
            if (string.IsNullOrEmpty(word) || Dictionary.Entries.Count == 0)
            {
                return new SpellCheckResult(false);
            }

            if (word == DefaultXmlToken)
            {
                return new SpellCheckResult(true);
            }

            string wordToClean;
            if (Affix.InputConversions.Count > 0)
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
                if (rv.Flags.Contains(Affix.Warn))
                {
                    if (Affix.ForbiddenWord != 0)
                    {
                        isFound = false;
                    }
                }

                return new SpellCheckResult(root, resultType, isFound);
            }

            // recursive breaking at break points
            if (Affix.BreakTable.Length != 0)
            {
                int nbr = 0;
                wl = scw.Length;

                // calculate break points for recursion limit
                for (var j = 0; j < Affix.BreakTable.Length; j++)
                {
                    int pos = 0;
                    while ((pos = scw.IndexOf(Affix.BreakTable[j], pos)) >= 0)
                    {
                        ++nbr;
                        pos += Affix.BreakTable[j].Length;
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
                        if (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu") && breakEntry == "-")
                        {
                            suffix = scw.Substring(found + 1);
                            scw = scw.Substring(found + 1);
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

        private string ReplaceInput(string word, ReplacementEntry entry, bool atStart)
        {
            if (entry == null)
            {
                return string.Empty;
            }

            ReplacementValueType type;
            if (word.Length == entry.Pattern.Length)
            {
                type = atStart ? ReplacementValueType.Isol : ReplacementValueType.Fin;
            }
            else
            {
                type = atStart ? ReplacementValueType.Ini : ReplacementValueType.Med;
            }

            while (type != 0 && string.IsNullOrEmpty(entry[type]))
            {
                type = (type == ReplacementValueType.Fin && !atStart) ? 0 : type - 1;
            }

            return entry[type];
        }

        private DictionaryEntry CheckWord(string w, ref SpellCheckResultType info, out string root)
        {
            root = string.Empty;
            var useBuffer = false;
            string w2;
            string word;

            if (Affix.IgnoredChars.Length != 0)
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

            if (word.Length == 0)
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
                foreach (var entry in entries)
                {
                    he = entry;

                    // check forbidden and onlyincompound words
                    if (entry.Flags.Length != 0 && entry.Flags.Contains(Affix.ForbiddenWord))
                    {
                        info |= SpellCheckResultType.Forbidden;

                        if (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu"))
                        {
                            if (entry.Flags.Contains(Affix.CompoundFlag))
                            {
                                info |= SpellCheckResultType.Compound;
                            }
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
                        (Affix.OnlyInCompound != 0 && he.Flags.Contains(Affix.OnlyInCompound))
                        ||
                        (info.HasFlag(SpellCheckResultType.InitCap) && he.Flags.Contains(SpecialFlags.OnlyUpcaseFlag))
                    )
                )
                {
                    he = null;
                }

                if (he != null)
                {
                    if (he.HasFlags && he.Flags.Contains(Affix.ForbiddenWord))
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
                    List<DictionaryEntry> rwords;
                    he = CompoundCheck(word, 0, 0, int.MaxValue, 0, null, out rwords, 0, 0, ref info);

                    // LANG_hu section: `moving rule' with last dash
                    if (he != null && StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu") && word.EndsWith('-'))
                    {
                        var dup = word.Substring(0, word.Length - 1);
                        he = CompoundCheck(dup, -5, 0, int.MaxValue, 0, null, out rwords, 1, 0, ref info);
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

        private DictionaryEntry CompoundCheck(string word, int wordnum, int numsyllable, int maxwordnum, int wnum, ImmutableList<DictionaryEntry> words, out List<DictionaryEntry> rwords, int hu_mov_rule, int isSug, ref SpellCheckResultType info)
        {
            rwords = null;

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
            int affixed = 0;
            var oldwords = words;
            var len = word.Length;

            int checked_prefix;
            var cmin = Affix.CompoundMin;
            var cmax = word.Length - Affix.CompoundMin + 1;

            var st = word;

            for (i = cmin; i < cmax; i++)
            {
                words = oldwords;
                var onlycpdrule = words != null && words.Count > 0 ? 1 : 0;
                do
                {
                    // onlycpdrule loop

                    oldnumsyllable = numsyllable;
                    oldwordnum = wordnum;
                    checked_prefix = 0;

                    do
                    {
                        // simplified checkcompoundpattern loop

                        if (scpd > 0)
                        {
                            for (; scpd <= Affix.CompoundPatterns.Length &&
                                (string.IsNullOrEmpty(Affix.CompoundPatterns[scpd - 1].Pattern3) ||
                                word.Substring(i) != Affix.CompoundPatterns[scpd - 1].Pattern3);
                                scpd++)
                            {
                                ;
                            }

                            if (scpd > Affix.CompoundPatterns.Length)
                            {
                                break;
                            }

                            //st = st.replace(i, -1, Affix.CompoundPatterns[scpd - 1].Pattern);
                            st = st.Substring(0, i) + Affix.CompoundPatterns[scpd - 1].Pattern;

                            soldi = i;
                            i += Affix.CompoundPatterns[scpd - 1].Pattern.Length;

                            //st = st.replace(i, -1, Affix.CompoundPatterns[scpd - 1].Pattern2);
                            st = st.Substring(0, i) + Affix.CompoundPatterns[scpd - 1].Pattern2;

                            //st = st.replace(i + Affix.CompoundPatterns[scpd - 1].Pattern2.Length, -1, word.Substring(soldi + Affix.CompoundPatterns[scpd - 1].Pattern3.Length));
                            st = st.Substring(0, i + Affix.CompoundPatterns[scpd - 1].Pattern2.Length) + word.Substring(soldi + Affix.CompoundPatterns[scpd - 1].Pattern3.Length);

                            oldlen = len;
                            len += Affix.CompoundPatterns[scpd - 1].Pattern.Length +
                                Affix.CompoundPatterns[scpd - 1].Pattern2.Length -
                                Affix.CompoundPatterns[scpd - 1].Pattern3.Length;
                            oldcmin = cmin;
                            oldcmax = cmax;
                            cmin = Affix.CompoundMin;
                            cmax = st.Length - Affix.CompoundMin + 1;

                            cmax = len - Affix.CompoundMin + 1;
                        }

                        //ch = st[i];
                        st = st.Substring(0, i);

                        SuffixEntry sfx = null; // TODO: this currently relies on some shared mutable state
                        PrefixEntry pfx = null; // TODO: this currently relies on some shared mutable state

                        // FIRST WORD

                        affixed = 1;
                        var searchEntries = Lookup(st); // perhaps without prefix
                        var searchEntriesIndex = 0;

                        rv = searchEntriesIndex < searchEntries.Length ? searchEntries[searchEntriesIndex] : null;

                        // search homonym with compound flag
                        while ((rv != null) && hu_mov_rule == 0 &&
               ((Affix.NeedAffix != 0 && rv.Flags.Contains(Affix.NeedAffix)) ||
                !((Affix.CompoundFlag != 0 && (words == null || words.Count == 0) && onlycpdrule == 0 &&
                   rv.Flags.Contains(Affix.CompoundFlag)) ||
                  (Affix.CompoundBegin != 0 && wordnum == 0 && onlycpdrule == 0 &&
                   rv.Flags.Contains(Affix.CompoundBegin)) ||
                  (Affix.CompoundMiddle != 0 && wordnum != 0 && (words == null || words.Count == 0) && onlycpdrule == 0 &&
                   rv.Flags.Contains(Affix.CompoundMiddle)) ||
                  (Affix.HasCompoundRules && onlycpdrule != 0 &&
                   (((words == null || words.Count == 0) && wordnum == 0 &&
                     DefCpdCheck(ref words, wnum, rv, out rwords, 0)) ||
                    (words != null &&
                     DefCpdCheck(ref words, wnum, rv, out rwords, 0))))) ||
                (scpd != 0 && Affix.CompoundPatterns[scpd - 1].Condition != 0 &&
                 !rv.Flags.Contains(Affix.CompoundPatterns[scpd - 1].Condition))))
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

                            if (Affix.CompoundFlag != 0 &&
                                  (rv = PrefixCheck(st.Substring(0, i),
                                                      hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin,
                                                      Affix.CompoundFlag)) == null)
                            {
                                if (((rv = SuffixCheck(
                                          st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundFlag,
                                          hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)) != null ||
                                     (Affix.CompoundMoreSuffixes &&
                                      (rv = SuffixCheckTwoSfx(st.Substring(0, i), 0, null, Affix.CompoundFlag)) != null)) &&
                                    hu_mov_rule == 0 && sfx.HasContClass &&
                                    ((Affix.CompoundForbidFlag != 0 &&
                                      sfx.ContClass.Contains(Affix.CompoundForbidFlag)) ||
                                     (Affix.CompoundEnd != 0 &&
                                      sfx.ContClass.Contains(Affix.CompoundEnd))))
                                {
                                    rv = null;
                                }
                            }

                            if (rv != null ||
                                (((wordnum == 0) && Affix.CompoundBegin != 0 &&
                                  ((rv = SuffixCheck(
                                        st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundBegin,
                                        hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)) != null ||
                                   (Affix.CompoundMoreSuffixes &&
                                    (rv = SuffixCheckTwoSfx(
                                         st.Substring(0, i), 0, null,
                                         Affix.CompoundBegin)) != null) ||  // twofold suffixes + compound
                                   (rv = PrefixCheck(st.Substring(0, i),
                                                      hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin,
                                                      Affix.CompoundBegin)) != null)) ||
                                 ((wordnum > 0) && Affix.CompoundMiddle != 0 &&
                                  ((rv = SuffixCheck(
                                        st.Substring(0, i), 0, null, null, new FlagValue(), Affix.CompoundMiddle,
                                        hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin)) != null ||
                                   (Affix.CompoundMoreSuffixes &&
                                    (rv = SuffixCheckTwoSfx(
                                         st.Substring(0, i), 0, null,
                                         Affix.CompoundMiddle)) != null) ||  // twofold suffixes + compound
                                   (rv = PrefixCheck(st.Substring(0, i),
                                                      hu_mov_rule != 0 ? CompoundOptions.Other : CompoundOptions.Begin,
                                                      Affix.CompoundMiddle)) != null))))
                            {
                                checked_prefix = 1;
                            }
                        }
                        else if (rv.HasFlags && (rv.Flags.Contains(Affix.ForbiddenWord) ||
                                rv.Flags.Contains(Affix.NeedAffix) ||
                                rv.Flags.Contains(SpecialFlags.OnlyUpcaseFlag) ||
                                (isSug != 0 && Affix.NoSuggest != 0 &&
                                 rv.Flags.Contains(Affix.NoSuggest))))
                        {
                            // else check forbiddenwords and needaffix
                            st = st.SetChar(ch, i);
                            break;
                        }

                        // check non_compound flag in suffix and prefix
                        if ((rv != null) && hu_mov_rule == 0 &&
                            ((pfx != null && pfx.HasContClass &&
                              pfx.ContClass.Contains(Affix.CompoundForbidFlag)) ||
                             (sfx != null && sfx.HasContClass &&
                              sfx.ContClass.Contains(Affix.CompoundForbidFlag))))
                        {
                            rv = null;
                        }

                        // check compoundend flag in suffix and prefix
                        if ((rv != null) && checked_prefix == 0 && Affix.CompoundEnd != 0 && hu_mov_rule == 0 &&
                            ((pfx != null && pfx.HasContClass &&
                              pfx.ContClass.Contains(Affix.CompoundEnd)) ||
                             (sfx != null && sfx.HasContClass &&
                              sfx.ContClass.Contains(Affix.CompoundEnd))))
                        {
                            rv = null;
                        }

                        // check compoundmiddle flag in suffix and prefix
                        if ((rv != null) && checked_prefix == 0 && (wordnum == 0) && Affix.CompoundMiddle != 0 &&
                            hu_mov_rule == 0 &&
                            ((pfx != null && pfx.HasContClass &&
                              pfx.ContClass.Contains(Affix.CompoundMiddle)) ||
                             (sfx != null && sfx.HasContClass &&
                              sfx.ContClass.Contains(Affix.CompoundMiddle))))
                        {
                            rv = null;
                        }

                        // check forbiddenwords
                        if ((rv != null) && (rv.HasFlags) &&
                            (rv.Flags.Contains(Affix.ForbiddenWord) ||
                             rv.Flags.Contains(SpecialFlags.OnlyUpcaseFlag) ||
                             (isSug != 0 && Affix.NoSuggest != 0 && rv.Flags.Contains(Affix.NoSuggest))))
                        {
                            rwords = null;
                            return null;
                        }

                        // increment word number, if the second root has a compoundroot flag
                        if ((rv != null) && Affix.CompoundRoot != 0 &&
                            (rv.Flags.Contains(Affix.CompoundRoot)))
                        {
                            wordnum++;
                        }

                        // first word is acceptable in compound words?

                        if (((rv != null) &&
             (checked_prefix != 0 || (words != null && wnum < words.Count && words[wnum] != null) ||
              (Affix.CompoundFlag != 0 && rv.Flags.Contains(Affix.CompoundFlag)) ||
              ((oldwordnum == 0) && Affix.CompoundBegin != 0 &&
               rv.Flags.Contains(Affix.CompoundBegin)) ||
              ((oldwordnum > 0) && Affix.CompoundMiddle != 0 &&
               rv.Flags.Contains(Affix.CompoundMiddle))

              // LANG_hu section: spec. Hungarian rule
              || (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu") && hu_mov_rule != 0 &&
                  (rv.Flags.Contains(SpecialFlags.LetterF) ||  // XXX hardwired Hungarian dictionary codes
                   rv.Flags.Contains(SpecialFlags.LetterG) ||
                   rv.Flags.Contains(SpecialFlags.LetterH)))
              // END of LANG_hu section
              ) &&
             (
                 // test CHECKCOMPOUNDPATTERN conditions
                 scpd == 0 || Affix.CompoundPatterns[scpd - 1].Condition == 0 ||
                 rv.Flags.Contains(Affix.CompoundPatterns[scpd - 1].Condition)) &&
             !((Affix.CheckCompoundTriple && scpd == 0 &&
                (words == null || words.Count == 0) &&  // test triple letters
                (word[i - 1] == word[i]) &&
                (((i > 1) && (word[i - 1] == word[i - 2])) ||
                 ((word[i - 1] == word[i + 1]))  // may be word[i+1] == '\0'
                 )) ||
               (Affix.CheckCompoundCase && scpd == 0 && (words == null || words.Count == 0) &&
                CompoundCaseCheck(word, i))))
            // LANG_hu section: spec. Hungarian rule
            || ((rv == null) && StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu") && hu_mov_rule != 0 &&
                (rv = AffixCheck(st.Substring(0, i), new FlagValue(), CompoundOptions.Not)) != null &&
                (sfx != null && sfx.HasContClass &&
                 (  // XXX hardwired Hungarian dic. codes
                     sfx.ContClass.Contains(SpecialFlags.LetterXLower) ||
                     sfx.ContClass.Contains(SpecialFlags.LetterPercent)))))
                        {
                            // first word is ok condition

                            if (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu"))
                            {
                                // calculate syllable number of the word
                                numsyllable += GetSyllable(st.Substring(i));
                                // + 1 word, if syllable number of the prefix > 1 (hungarian
                                // convention)
                                if (pfx != null && (GetSyllable(pfx.Key) > 1))
                                {
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
                                List<DictionaryEntry> junkDictionaryEntryList;

                                rv = homonymIndex < homonyms.Length ? homonyms[homonymIndex] : null;
                                // search homonym with compound flag
                                while ((rv != null) &&
                                       ((Affix.NeedAffix != 0 && rv.Flags.Contains(Affix.NeedAffix)) ||
                                        !((Affix.CompoundFlag != 0 && (words == null || words.Count == 0) &&
                                           rv.Flags.Contains(Affix.CompoundFlag)) ||
                                          (Affix.CompoundEnd != 0 && (words == null || words.Count == 0) &&
                                           rv.Flags.Contains(Affix.CompoundEnd)) ||
                                          (Affix.HasCompoundRules && words != null && words.Count != 0 &&
                                           DefCpdCheck(ref words, wnum + 1, rv, out junkDictionaryEntryList, 1))) ||
                                        (scpd != 0 && Affix.CompoundPatterns[scpd - 1].Condition2 != 0 &&
                                         !rv.Flags.Contains(Affix.CompoundPatterns[scpd - 1].Condition2))))
                                {
                                    homonymIndex++;
                                    rv = homonymIndex < homonyms.Length ? homonyms[homonymIndex] : null;
                                }

                                // check FORCEUCASE
                                if (rv != null && Affix.ForceUpperCase != 0 && (rv != null) &&
                                    (rv.Flags.Contains(Affix.ForceUpperCase)) &&
                                    !(info.HasFlag(SpellCheckResultType.OrigCap)))
                                {
                                    rv = null;
                                }

                                if (rv != null && words != null && words.Count > 0 && words[wnum + 1] != null)
                                {
                                    return rv_first;
                                }

                                oldnumsyllable2 = numsyllable;
                                oldwordnum2 = wordnum;

                                if ((rv != null) && StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu") &&
                                    (rv.Flags.Contains(SpecialFlags.LetterI)) &&
                                    !(rv.Flags.Contains(SpecialFlags.LetterJ)))
                                {
                                    numsyllable--;
                                }

                                // increment word number, if the second root has a compoundroot flag
                                if ((rv != null) && (Affix.CompoundRoot != 0) &&
                                    (rv.Flags.Contains(Affix.CompoundRoot)))
                                {
                                    wordnum++;
                                }

                                // check forbiddenwords
                                if ((rv != null) && (rv.HasFlags) &&
                                    (rv.Flags.Contains(Affix.ForbiddenWord) ||
                                     rv.Flags.Contains(SpecialFlags.OnlyUpcaseFlag) ||
                                     (isSug != 0 && Affix.NoSuggest != 0 &&
                                      rv.Flags.Contains(Affix.NoSuggest))))
                                {
                                    return null;
                                }

                                // second word is acceptable, as a root?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist of 2 words, or if more,
                                // then the syllable number of root words must be 6, or lesser.

                                if ((rv != null) &&
                                    ((Affix.CompoundFlag != 0 && rv.Flags.Contains(Affix.CompoundFlag)) ||
                                     (Affix.CompoundEnd != 0 && rv.Flags.Contains(Affix.CompoundEnd))) &&
                                    (((Affix.CompoundWordMax == -1) || (wordnum + 1 < Affix.CompoundWordMax)) ||
                                     ((Affix.CompoundMaxSyllable != 0) &&
                                      ((numsyllable + GetSyllable(rv.Word)) <=
                                       Affix.CompoundMaxSyllable))) &&
                                    (
                                        // test CHECKCOMPOUNDPATTERN
                                        !Affix.HasCompoundPatterns || scpd != 0 ||
                                        !CompoundPatternCheck(word, i, rv_first, rv, 0)) &&
                                    ((!Affix.CheckCompoundDup || (rv != rv_first)))
                                    // test CHECKCOMPOUNDPATTERN conditions
                                    &&
                                    (scpd == 0 || Affix.CompoundPatterns[scpd - 1].Condition2 == 0 ||
                                     rv.Flags.Contains(Affix.CompoundPatterns[scpd - 1].Condition2)))
                                {
                                    // forbid compound word, if it is a non compound word with typical
                                    // fault
                                    if (Affix.CheckCompoundRep && CompoundReplacementCheck(word.Substring(0, len)))
                                    {
                                        return null;
                                    }
                                    return rv_first;
                                }

                                numsyllable = oldnumsyllable2;
                                wordnum = oldwordnum2;

                                // perhaps second word has prefix or/and suffix
                                sfx = null; // TODO: shared mutable state bug
                                var sfxflag = 0; // TODO: shared mutable state bug

                                rv = (Affix.CompoundFlag != 0 && onlycpdrule == 0)
                                     ? AffixCheck(word.Substring(i), Affix.CompoundFlag, CompoundOptions.End)
                                     : null;

                                if (rv == null && Affix.CompoundEnd != 0 && onlycpdrule == 0)
                                {
                                    sfx = null; // TODO: shared mutable state bug
                                    pfx = null; // TODO: shared mutable state bug
                                    rv = AffixCheck(word.Substring(i), Affix.CompoundEnd, CompoundOptions.End);
                                }

                                if (rv == null && Affix.HasCompoundRules && words != null && words.Count != 0)
                                {
                                    rv = AffixCheck(word.Substring(i), new FlagValue(), CompoundOptions.End);
                                    List<DictionaryEntry> junkEntries;
                                    if (rv != null && DefCpdCheck(ref words, wnum + 1, rv, out junkEntries, 1))
                                    {
                                        return rv_first;
                                    }

                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (allowed forms)
                                if (rv != null &&
                                    !(scpd == 0 || Affix.CompoundPatterns[scpd - 1].Condition2 == 0 ||
                                      rv.Flags.Contains(Affix.CompoundPatterns[scpd - 1].Condition2)))
                                {
                                    rv = null;
                                }

                                // test CHECKCOMPOUNDPATTERN conditions (forbidden compounds)
                                if (rv != null && Affix.HasCompoundPatterns && scpd == 0 &&
                                    CompoundPatternCheck(word, i, rv_first, rv, affixed))
                                {
                                    rv = null;
                                }

                                // check non_compound flag in suffix and prefix
                                if ((rv != null) && ((pfx != null && pfx.HasContClass &&
                                              pfx.ContClass.Contains(Affix.CompoundForbidFlag)) ||
                                             (sfx != null && sfx.HasContClass &&
                                              sfx.ContClass.Contains(Affix.CompoundForbidFlag))))
                                {
                                    rv = null;
                                }

                                // check FORCEUCASE
                                if (rv != null && Affix.ForceUpperCase != 0 &&
                                    (rv.Flags.Contains(Affix.ForceUpperCase)) &&
                                    !(info.HasFlag(SpellCheckResultType.OrigCap)))
                                {
                                    rv = null;
                                }

                                // check forbiddenwords
                                if ((rv != null) && (rv.HasFlags) &&
                                    (rv.Flags.Contains(Affix.ForbiddenWord) ||
                                     rv.Flags.Contains(SpecialFlags.OnlyUpcaseFlag) ||
                                     (isSug != 0 && Affix.NoSuggest != 0 &&
                                      rv.Flags.Contains(Affix.NoSuggest))))
                                {
                                    return null;
                                }

                                // pfxappnd = prefix of word+i, or NULL
                                // calculate syllable number of prefix.
                                // hungarian convention: when syllable number of prefix is more,
                                // than 1, the prefix+word counts as two words.

                                if (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu"))
                                {
                                    throw new NotImplementedException();
                                }

                                // increment word number, if the second word has a compoundroot flag
                                if ((rv != null) && (Affix.CompoundRoot != 0) &&
                                    (rv.Flags.Contains(Affix.CompoundRoot)))
                                {
                                    wordnum++;
                                }

                                // second word is acceptable, as a word with prefix or/and suffix?
                                // hungarian conventions: compounding is acceptable,
                                // when compound forms consist 2 word, otherwise
                                // the syllable number of root words is 6, or lesser.
                                if ((rv != null) &&
                                    (((Affix.CompoundWordMax == -1) || (wordnum + 1 < Affix.CompoundWordMax)) ||
                                     ((Affix.CompoundMaxSyllable != 0) && (numsyllable <= Affix.CompoundMaxSyllable))) &&
                                    ((!Affix.CheckCompoundDup || (rv != rv_first))))
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
                                    rv = CompoundCheck(st.Substring(i), wordnum + 1,
                                      numsyllable, maxwordnum, wnum + 1, words, out rwords, 0,
                                      isSug, ref info);

                                    if (rv != null && Affix.HasCompoundPatterns &&
                                      ((scpd == 0 &&
                                        CompoundPatternCheck(word, i, rv_first, rv, affixed)) ||
                                       (scpd != 0 &&
                                        !CompoundPatternCheck(word, i, rv_first, rv, affixed))))
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

        /// <summary>
        /// Forbid compoundings when there are special patterns at word bound.
        /// </summary>
        private bool CompoundPatternCheck(string word, int pos, DictionaryEntry r1, DictionaryEntry r2, int affixed)
        {
            for (var i = 0; i < Affix.CompoundPatterns.Length; i++)
            {
                var patternEntry = Affix.CompoundPatterns[i];

                int len;
                if (IsSubset(patternEntry.Pattern2, word.Substring(pos)) &&
        (r1 == null || patternEntry.Condition == 0 ||
         (r1.HasFlags && r1.Flags.Contains(patternEntry.Condition))) &&
        (r2 == null || patternEntry.Condition2 == 0 ||
         (r2.HasFlags && r2.Flags.Contains(patternEntry.Condition2))) &&
        // zero length pattern => only TESTAFF
        // zero pattern (0/flag) => unmodified stem (zero affixes allowed)
        (string.IsNullOrEmpty(patternEntry.Pattern) ||
         ((patternEntry.Pattern.StartsWith('0') && r1.Word.Length <= pos &&
           word.Substring(pos - r1.Word.Length).StartsWith(r1.Word)) || //strncmp(0 + pos - r1.Word.Length, r1.Word, r1.Word.Length) == 0) ||
          (!patternEntry.Pattern.StartsWith('0') &&
           ((len = patternEntry.Pattern.Length) != 0) &&
           word.Substring(pos - len).StartsWith(patternEntry.Pattern.Substring(0, len))))))
                //strncmp(0 + pos - len, patternEntry.Pattern, len) == 0))))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSubset(string s1, string s2)
        {
            if (s1.Length > s2.Length)
            {
                return false;
            }

            for (var i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    if (s1[i] != '.')
                    {
                        return false;
                    }
                }
            }

            return true;
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
                    ReplacementValueType type;

                    if (rIndex + replacementEntry.Pattern.Length == lenp)
                    {
                        type = rIndex == 0 ? ReplacementValueType.Isol : ReplacementValueType.Fin;
                    }
                    else
                    {
                        type = rIndex == 0 ? ReplacementValueType.Ini : ReplacementValueType.Med;
                    }

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
        /// Forbid compounding with neighbouring upper and lower case characters at word bounds.
        /// </summary>
        private bool CompoundCaseCheck(string word, int pos)
        {
            var a = pos - 1;
            var b = pos;
            if ((char.IsUpper(word, a) || char.IsUpper(word, b)) && word[a] != '-' && word[b] != '-')
            {
                return true;
            }

            return false;
        }

        private DictionaryEntry SuffixCheckTwoSfx(string word, int sfxopts, PrefixEntry ppfx, int needflag)
        {
            DictionaryEntry rv = null;

            // first handle the special case of 0 length suffixes
            foreach (var suffixGroup in Affix.Suffixes)
            {
                foreach (var se in suffixGroup.Entries)
                {
                    if (Affix.ContClasses.Contains(suffixGroup.AFlag))
                    {
                        rv = CheckTwoSfx(suffixGroup, se, word, sfxopts, ppfx, needflag);
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
                foreach (var sptr in suffixGroup.Entries)
                {
                    if (word.EndsWith(sptr.Key))
                    {
                        if (Affix.ContClasses.Contains(suffixGroup.AFlag))
                        {
                            rv = CheckTwoSfx(suffixGroup, sptr, word, sfxopts, ppfx, needflag);
                            if (rv != null)
                            {
                                // TODO: work around shared mutable state bugs
                                return rv;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private DictionaryEntry CheckTwoSfx(AffixEntryGroup<SuffixEntry> suffixGroup, SuffixEntry se, string word, int sfxopts, PrefixEntry ppfx, int needflag)
        {
            throw new NotImplementedException();
        }

        private bool DefCpdCheck(ref ImmutableList<DictionaryEntry> words, int wnum, DictionaryEntry rv, out List<DictionaryEntry> def, int all)
        {
            var w = 0;
            def = new List<DictionaryEntry>();

            if (words == null || words.IsEmpty)
            {
                w = 1;
                words = def.ToImmutableList();
            }

            if (words == null || words.IsEmpty)
            {
                return false;
            }

            throw new NotImplementedException();
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

            if (rv != null)
            {
                return rv;
            }

            if (Affix.HasContClass)
            {
                throw new NotImplementedException();
            }

            return rv;
        }

        private DictionaryEntry PrefixCheck(string word, CompoundOptions inCompound, FlagValue needFlag)
        {
            if (Affix.Prefixes.Length == 0)
            {
                return null;
            }

            // first handle the special case of 0 length prefixes
            foreach (var affixGroup in Affix.Prefixes)
            {
                foreach (var affixEntry in affixGroup.Entries)
                {
                    var fogemorpheme = inCompound != CompoundOptions.Not || !affixEntry.ContClass.Contains(Affix.OnlyInCompound);
                    var permitPrefixInCompounds = inCompound != CompoundOptions.End || affixEntry.ContClass.Contains(Affix.CompoundPermitFlag);
                    if (fogemorpheme && permitPrefixInCompounds)
                    {
                        var entry = CheckWordPrefix(affixGroup, affixEntry, word, inCompound, needFlag);
                        if (entry != null)
                        {
                            return entry;
                        }
                    }
                }
            }

            throw new NotImplementedException();
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
                            dictionaryEntry.Flags.Contains(group.AFlag)
                            && !entry.ContClass.Contains(Affix.NeedAffix) // forbid single prefixes with needaffix flag
                            &&
                            (
                                needFlag != 0
                                || dictionaryEntry.Flags.Contains(needFlag)
                                ||
                                (
                                    entry.ContClass.Length != 0
                                    && entry.ContClass.Contains(needFlag)
                                )
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

        private ImmutableArray<DictionaryEntry> Lookup(string word)
        {
            ImmutableArray<DictionaryEntry> entries;
            return Dictionary.Entries.TryGetValue(word, out entries) ? entries : ImmutableArray<DictionaryEntry>.Empty;
        }

        [Obsolete("Inline")]
        private bool TestCondition(PrefixEntry entry, string word)
        {
            return entry.Conditions.IsStartingMatch(word);
        }

        private DictionaryEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
        {
            if (Affix.Suffixes.Length == 0)
            {
                return null;
            }

            foreach (var affixGroup in Affix.Suffixes)
            {
                foreach (var affixEntry in affixGroup.Entries)
                {
                    if (cclass == 0 || affixEntry.ContClass.Length != 0)
                    {
                        // suffixes are not allowed in beginning of compounds
                        if ((((inCompound != CompoundOptions.Begin)) ||  // && !cclass
                                                                         // except when signed with compoundpermitflag flag
                       (affixEntry.ContClass.Length != 0 && Affix.CompoundPermitFlag != 0 &&

                        affixEntry.ContClass.Contains(Affix.CompoundPermitFlag))) &&
                      (Affix.Circumfix == 0 ||
                       // no circumfix flag in prefix and suffix
                       ((pfx == null || pfx.ContClass.Length == 0 ||
                         !pfx.ContClass.Contains(Affix.Circumfix)) &&
                        (affixEntry.ContClass.Length == 0 ||
                         !(affixEntry.ContClass.Contains(Affix.Circumfix)))) ||
                       // circumfix flag in prefix AND suffix
                       ((pfx != null && pfx.ContClass.Length != 0 &&
                         pfx.ContClass.Contains(Affix.Circumfix)) &&
                        (affixEntry.ContClass.Length != 0 &&
                         (affixEntry.ContClass.Contains(Affix.Circumfix))))) &&
                      // fogemorpheme
                      (inCompound != CompoundOptions.Not ||
                       !(affixEntry.ContClass.Length != 0 &&
                         (affixEntry.ContClass.Contains(Affix.OnlyInCompound)))) &&
                      // needaffix on prefix or first suffix
                      (cclass != 0 ||
                       !(affixEntry.ContClass.Length != 0 &&
                         affixEntry.ContClass.Contains(Affix.NeedAffix)) ||
                       (pfx != null &&
                        !((pfx.ContClass.Length != 0) &&
                          pfx.ContClass.Contains(Affix.NeedAffix)))))
                        {
                            var rv = CheckWordSuffix(affixGroup, affixEntry, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, (inCompound != 0 ? new FlagValue() : Affix.OnlyInCompound));
                            if (rv != null)
                            {
                                return rv;
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(word))
            {
                return null;
            }

            var ep = pfx;

            foreach (var affixGroup in Affix.Suffixes)
            {
                foreach (var sptr in affixGroup.Entries)
                {
                    if (word.EndsWith(sptr.Append))
                    {
                        if ((((inCompound != CompoundOptions.Begin)) ||  // && !cclass
                                                                         // except when signed with compoundpermitflag flag
           (sptr.HasContClass && Affix.CompoundPermitFlag != 0 &&
            sptr.ContClass.Contains(Affix.CompoundPermitFlag))) &&
          (Affix.Circumfix == 0 ||
           // no circumfix flag in prefix and suffix
           ((pfx == null || !(ep.HasContClass) ||
             !ep.ContClass.Contains(Affix.Circumfix)) &&
            (!sptr.HasContClass ||
             !(sptr.ContClass.Contains(Affix.Circumfix)))) ||
           // circumfix flag in prefix AND suffix
           ((pfx != null && (ep.HasContClass) &&
             ep.ContClass.Contains(Affix.Circumfix)) &&
            (sptr.HasContClass &&
             (sptr.ContClass.Contains(Affix.Circumfix))))) &&
          // fogemorpheme
          (inCompound != CompoundOptions.Not ||
           !((sptr.HasContClass && (sptr.ContClass.Contains(Affix.OnlyInCompound))))) &&
          // needaffix on prefix or first suffix
          (cclass != 0 ||
           !(sptr.HasContClass &&
             sptr.ContClass.Contains(Affix.NeedAffix)) ||
           (pfx != null &&
            !((ep.HasContClass) &&
              ep.ContClass.Contains(Affix.NeedAffix)))))
                        {
                            if (inCompound != CompoundOptions.End || pfx != null ||
            !(sptr.HasContClass &&
              sptr.ContClass.Contains(Affix.OnlyInCompound)))
                            {
                                var rv = CheckWordSuffix(affixGroup, sptr, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound);
                                if (rv != null)
                                {
                                    if (!sptr.HasContClass)
                                    {
                                        // TODO: need to work around some thread safety bugs
                                        throw new NotImplementedException();
                                    }
                                    else if (StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu")
                                        && sptr.Key.StartsWith('i')
                                        && sptr.Key.Length >= 2
                                        && sptr.Key[1] != 'y'
                                        && sptr.Key[1] != 't'
                                    )
                                    {
                                        // TODO: need to work around some thread safety bugs
                                        throw new NotImplementedException();
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

            if ((tmpl > 0 || (tmpl == 0 && Affix.FullStrip)) &&
                (tmpl + entry.Strip.Length >= entry.Conditions.Count))
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
                        if ((he.Flags.Contains(group.AFlag) ||
               (ep != null && ep.HasContClass &&
                ep.ContClass.Contains(group.AFlag))) &&
              ((!optFlags.HasFlag(AffixEntryOptions.CrossProduct)) ||
               (ep != null && he.Flags.Contains(pfxGroup.AFlag)) ||
               // enabled by prefix
               ((entry.HasContClass) &&
                (ep != null && entry.ContClass.Contains(pfxGroup.AFlag)))) &&
              // handle cont. class
              ((cclass == 0) ||
               ((entry.HasContClass) && entry.ContClass.Contains(cclass))) &&
              // check only in compound homonyms (bad flags)
              (badFlag == 0 || !he.Flags.Contains(badFlag)) &&
              // handle required flag
              ((needFlag == 0) ||
               (he.Flags.Contains(needFlag) ||
                ((entry.HasContClass) && entry.ContClass.Contains(needFlag)))))
                        {
                            return he;
                        }
                    }
                }
            }

            return null;
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
