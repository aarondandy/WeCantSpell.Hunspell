using Hunspell.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

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

        public SpellCheckResult CheckDetails()
        {
            var word = WordToCheck;

            if (string.IsNullOrEmpty(word) || Dictionary.Entries.Count == 0)
            {
                return new SpellCheckResult(false);
            }

            // Hunspell supports XML input of the simplified API (see manual)
            if (word == DefaultXmlToken)
            {
                return new SpellCheckResult(true);
            }

            CapitalizationType capType;
            int abbv;
            string scw;

            // input conversion
            string convertedWord;
            if (!Affix.HasInputConversions || !TryConvertInput(word, out convertedWord))
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
            var root = string.Empty;

            DictionaryEntry rv;

            if (capType == CapitalizationType.Huh || capType == CapitalizationType.HuhInit || capType == CapitalizationType.None)
            {
                if (capType == CapitalizationType.HuhInit)
                {
                    resultType |= SpellCheckResultType.OrigCap;
                }

                rv = CheckWord(scw, ref resultType, ref root);
                if (abbv != 0 && rv == null)
                {
                    var u8buffer = scw + ".";
                    rv = CheckWord(u8buffer, ref resultType, ref root);
                }
            }
            else if (capType == CapitalizationType.All)
            {
                rv = CheckDetailsAllCap(abbv, ref scw, ref resultType, ref root);
            }
            else
            {
                rv = null;
            }

            if (capType == CapitalizationType.Init || (capType == CapitalizationType.All && rv == null))
            {
                rv = CheckDetailsInitCap(abbv, capType, ref scw, ref resultType, ref root);
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
                foreach (var breakEntry in Affix.BreakTable)
                {
                    var plen = breakEntry.Length;
                    if (plen == 1 || plen > wl)
                    {
                        continue;
                    }

                    if (
                        breakEntry.StartsWith('^')
                        && StringExtensions.EqualsOffset(scw, 0, breakEntry, 1, plen - 1)
                        && Check(scw.Substring(plen - 1))
                    )
                    {
                        return new SpellCheckResult(root, resultType, true);
                    }

                    if (
                        breakEntry.EndsWith('$')
                        && StringExtensions.EqualsOffset(scw, wl - plen + 1, breakEntry, 0, plen - 1)
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
                foreach (var breakEntry in Affix.BreakTable)
                {
                    var plen = breakEntry.Length;
                    var found = scw.IndexOf(breakEntry);

                    if (found > 0 && found < wl - plen)
                    {
                        if (!Check(scw.Substring(found + plen)))
                        {
                            continue;
                        }

                        var suffix = scw.Substring(found);
                        scw = scw.Substring(0, found);

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


        private DictionaryEntry CheckDetailsAllCap(int abbv, ref string scw, ref SpellCheckResultType resultType, ref string root)
        {
            resultType |= SpellCheckResultType.OrigCap;
            var rv = CheckWord(scw, ref resultType, ref root);
            if (rv != null)
            {
                return rv;
            }

            if (abbv != 0)
            {
                var u8buffer = scw + ".";
                rv = CheckWord(u8buffer, ref resultType, ref root);
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
                MakeAllSmall2(ref scw);

                // conversion may result in string with different len than before MakeAllSmall2 so re-scan

                if (apos < scw.Length - 1)
                {
                    var part1 = scw.Substring(0, apos + 1);
                    var part2 = scw.Substring(apos + 1);

                    MakeInitCap2(ref part2);
                    scw = part1 + part2;
                    rv = CheckWord(scw, ref resultType, ref root);
                    if (rv != null)
                    {
                        return rv;
                    }

                    MakeInitCap2(ref scw);
                    rv = CheckWord(scw, ref resultType, ref root);
                    if (rv != null)
                    {
                        return rv;
                    }
                }
            }

            if (Affix.CheckSharps && scw.Contains("SS"))
            {
                MakeAllSmall2(ref scw);

                var u8buffer = scw;
                rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, ref root);
                if (rv == null)
                {
                    MakeInitCap2(ref scw);
                    rv = SpellSharps(ref scw, 0, 0, 0, ref resultType, ref root);
                }

                if (abbv != 0 && rv == null)
                {
                    u8buffer += ".";
                    rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, ref root);
                    if (rv == null)
                    {
                        u8buffer = scw + ".";
                        rv = SpellSharps(ref u8buffer, 0, 0, 0, ref resultType, ref root);
                    }
                }
            }

            return rv;
        }

        private DictionaryEntry CheckDetailsInitCap(int abbv, CapitalizationType capType, ref string scw, ref SpellCheckResultType resultType, ref string root)
        {
            resultType |= SpellCheckResultType.OrigCap;
            MakeAllSmall2(ref scw);
            var u8buffer = scw;
            MakeInitCap2(ref scw);

            if (capType == CapitalizationType.Init)
            {
                resultType |= SpellCheckResultType.InitCap;
            }

            var rv = CheckWord(scw, ref resultType, ref root);

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

            rv = CheckWord(u8buffer, ref resultType, ref root);

            if (abbv != 0 && rv == null)
            {
                u8buffer += ".";
                rv = CheckWord(u8buffer, ref resultType, ref root);
                if (rv == null)
                {
                    u8buffer = scw;
                    u8buffer += ".";
                    if (capType == CapitalizationType.Init)
                    {
                        resultType |= SpellCheckResultType.InitCap;
                    }

                    rv = CheckWord(u8buffer, ref resultType, ref root);

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

        private bool IsKeepCase(DictionaryEntry rv)
        {
            return rv.ContainsFlag(Affix.KeepCase);
        }

        /// <summary>
        /// Recursive search for right ss - sharp s permutations
        /// </summary>
        private DictionaryEntry SpellSharps(ref string @base, int nPos, int n, int repNum, ref SpellCheckResultType info, ref string root)
        {
            var pos = @base.IndexOf("ss", nPos);
            if (pos >= 0 && n < MaxSharps)
            {
                // TODO: this string manipulation can be simpler

                var baseBuilder = new StringBuilder(@base, @base.Length);
                baseBuilder[pos] = 'ß';
                baseBuilder.Remove(pos + 1, 1);
                @base = baseBuilder.ToString();

                var h = SpellSharps(ref @base, pos + 1, n + 1, repNum + 1, ref info, ref root);
                if (h != null)
                {
                    return h;
                }

                baseBuilder.Clear();
                baseBuilder.Append(@base);
                baseBuilder[pos] = 's';
                baseBuilder.Insert(pos + 1, 's');
                @base = baseBuilder.ToString();

                h = SpellSharps(ref @base, pos + 2, n + 1, repNum, ref info, ref root);
                if (h != null)
                {
                    return h;
                }
            }
            else if (repNum > 0)
            {
                return CheckWord(@base, ref info, ref root);
            }

            return null;
        }

        private int MakeInitCap2(ref string s)
        {
            s = MakeInitCap(s);
            return s.Length;
        }

        private string MakeInitCap(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var builder = new StringBuilder(s, s.Length);
            builder[0] = Affix.Culture.TextInfo.ToUpper(builder[0]);
            return builder.ToString();
        }

        private int MakeAllSmall2(ref string s)
        {
            s = MakeAllSmall(s);
            return s.Length;
        }

        /// <summary>
        /// Convert to all little.
        /// </summary>
        private string MakeAllSmall(string s)
        {
            return Affix.Culture.TextInfo.ToLower(s);
        }

        private bool Check(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Check();
        }

        private DictionaryEntry CheckWord(string w, ref SpellCheckResultType info, ref string root)
        {
            var useBuffer = false;
            string w2 = string.Empty;
            string word;

            if (Affix.HasIgnoredChars)
            {
                w2 = w;
                w2 = w2.RemoveChars(Affix.IgnoredChars);
                word = w2;
                useBuffer = true;
            }
            else
            {
                word = w;
            }

            if (string.IsNullOrEmpty(word))
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
            DictionaryEntry he = null;
            ImmutableArray<DictionaryEntry> entries;
            if (Dictionary.Entries.TryGetValue(word, out entries))
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
                    && he.HasFlags
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

                    if (root != null)
                    {
                        root = he.Word;
                        if (Affix.ComplexPrefixes)
                        {
                            root = root.Reverse();
                        }
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
                        if (root != null)
                        {
                            root = he.Word;
                            if (Affix.ComplexPrefixes)
                            {
                                root = root.Reverse();
                            }
                        }

                        info |= SpellCheckResultType.Compound;
                    }
                }
            }

            return he;
        }

        private DictionaryEntry CompoundCheck(string word, int wordnum, int numsyllable, int maxwordnum, int wnum, Dictionary<int, DictionaryEntry> words, ref Dictionary<int, DictionaryEntry> rwords, int huMovRule, int isSug, ref SpellCheckResultType info)
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
            var cmax = word.Length - cmin + 1;

            var st = word;
            var stBeforeCharacterMangling = st;

            for (i = cmin; i < cmax; i++)
            {
                words = oldwords;
                var onlycpdrule = words != null ? 1 : 0; // TODO: consider converting to boolean
                do // onlycpdrule loop
                {

                    oldnumsyllable = numsyllable;
                    oldwordnum = wordnum;
                    checked_prefix = 0;

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
                                    StringExtensions.EqualsOffset(word, i, Affix.CompoundPatterns[scpd - 1].Pattern3, 0, Affix.CompoundPatterns[scpd - 1].Pattern3.Length)
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
                            cmax = len - Affix.CompoundMin + 1;
                        }

                        ch = i < st.Length ? st[i] : '\0';
                        stBeforeCharacterMangling = st;
                        st = st.Substring(0, i);

                        ClearSuffix();
                        ClearPrefix();

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
                                        && words == null
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
                                        && words == null
                                        && rv.ContainsFlag(Affix.CompoundMiddle)
                                    )
                                    ||
                                    (
                                        Affix.HasCompoundRules
                                        && onlycpdrule != 0
                                        &&
                                        (
                                            (
                                                words == null
                                                && wordnum == 0
                                                && DefCompoundCheck(ref words, wnum, rv, rwords, 0)
                                            )
                                            ||
                                            (
                                                words != null
                                                && DefCompoundCheck(ref words, wnum, rv, rwords, 0)
                                            )
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
                                        || // twofold suffixes + compound
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
                                        || // twofold suffixes + compound
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
                            st = stBeforeCharacterMangling;

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
                            && checked_prefix == 0
                            && Affix.CompoundEnd.HasValue
                            && huMovRule == 0
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
                            && checked_prefix == 0
                            && wordnum == 0
                            && Affix.CompoundMiddle.HasValue
                            && huMovRule == 0
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
                                        && words.ContainsKey(wnum)
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
                                        && words == null
                                        && i - 1 >= 0
                                        && word.Length >= 2
                                        && i < word.Length
                                        && (word[i - 1] == word[i]) // test triple letters
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
                                        && scpd == 0
                                        && words == null
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
                                    wordnum++;
                                }
                            }

                            // NEXT WORD(S)
                            rv_first = rv;
                            st = stBeforeCharacterMangling;
                            //st = st.SetChar(ch, i);

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
                                    && rv.ContainsFlag(Affix.ForceUpperCase)
                                    && !info.HasFlag(SpellCheckResultType.OrigCap)
                                )
                                {
                                    rv = null;
                                }

                                if (
                                    rv != null
                                    && words != null
                                    && words.ContainsKey(wnum + 1)
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
                                        !Affix.CompoundWordMax.HasValue
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

                                if (rv == null && Affix.HasCompoundRules && words != null)
                                {
                                    rv = AffixCheck(word.Substring(i), new FlagValue(), CompoundOptions.End);
                                    if (rv != null && DefCompoundCheck(ref words, wnum + 1, rv, null, 1))
                                    {
                                        return rv_first;
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
                                        !Affix.CompoundWordMax.HasValue
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
                                    // forbid compound word, if it is a non compound word with typical fault
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
                                    // forbid compound word, if it is a non compound word with typical fault
                                    if (Affix.CheckCompoundRep || Affix.ForbiddenWord.HasValue)
                                    {

                                        if (Affix.CheckCompoundRep && CompoundReplacementCheck(word))
                                        {
                                            return null;
                                        }

                                        // check first part
                                        // TODO: is this a StartsWith check?
                                        if (string.CompareOrdinal(rv.Word, 0, word, 0, rv.Word.Length) == 0)
                                        {
                                            var r = st[i + rv.Word.Length];
                                            var stCcrBackup = st;
                                            st = st.Substring(0, i + rv.Word.Length);

                                            if (Affix.CheckCompoundRep && CompoundReplacementCheck(st))
                                            {
                                                st = stCcrBackup;
                                                continue;
                                            }

                                            if (Affix.ForbiddenWord.HasValue)
                                            {
                                                var rv2 = Lookup(word)
                                                    .FirstOrDefault();

                                                if (rv2 == null)
                                                {
                                                    rv2 = AffixCheck(word, default(FlagValue), CompoundOptions.Not);
                                                }

                                                if (
                                                    rv2 != null
                                                    && rv2.ContainsFlag(Affix.ForbiddenWord)
                                                    && string.CompareOrdinal(rv2.Word, 0, st, 0, i + rv.Word.Length) == 0
                                                )
                                                {
                                                    return null;
                                                }
                                            }

                                            st = stCcrBackup;
                                        }
                                    }

                                    return rv_first;
                                }
                            }
                            while (striple != 0 && checkedstriple == 0);  // end of striple loop

                            if (checkedstriple != 0)
                            {
                                i++;
                                checkedstriple = 0;
                                striple = 0;
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
                    while (onlycpdrule == 0 && Affix.SimplifiedCompound && scpd <= Affix.CompoundPatterns.Length); // end of simplifiedcpd loop

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
                        st = stBeforeCharacterMangling;
                    }
                }
                while (Affix.HasCompoundRules && oldwordnum == 0 && onlycpdrule++ < 1);
            }

            return null;
        }

        /// <summary>
        /// Check if word with affixes is correctly spelled.
        /// </summary>
        private DictionaryEntry AffixCheck(string word, FlagValue needFlag, CompoundOptions inCompound)
        {
            DictionaryEntry rv = null;

            // check all prefixes (also crossed with suffixes if allowed)
            rv = PrefixCheck(word, inCompound, needFlag);
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
            DictionaryEntry rv = null;

            ClearPrefix();
            PrefixAppend = null;
            SuffixAppend = null;
            SuffixExtra = 0;

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
                    if (IsSubset(pptr.Key, word))
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

            // first handle the special case of 0 length prefixes
            foreach (var peGroup in Affix.Prefixes)
            {
                foreach (var pe in peGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
                {
                    var rv = CheckTwoSfx(peGroup, pe, word, inCompound, needFlag);
                    if (rv != null)
                    {
                        return rv;
                    }
                }
            }

            // now handle the general case
            foreach (var pptrGroup in Affix.Prefixes)
            {
                foreach (var pptr in pptrGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (IsSubset(pptr.Key, word))
                    {
                        var rv = CheckTwoSfx(pptrGroup, pptr, word, inCompound, needFlag);
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

        private DictionaryEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, FlagValue cclass, FlagValue needFlag, CompoundOptions inCompound)
        {
            DictionaryEntry rv = null;

            if (Affix.Suffixes.Length == 0)
            {
                return null;
            }

            // first handle the special case of 0 length suffixes
            foreach (var suffixGroup in Affix.Suffixes)
            {
                foreach (var se in suffixGroup.Entries.Where(e => string.IsNullOrEmpty(e.Key)))
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
                                        !pfx.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    !se.ContainsContClass(Affix.Circumfix)
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
                            rv = CheckWordSuffix(suffixGroup, se, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound);
                            if (rv != null)
                            {
                                SuffixGroup = suffixGroup;
                                Suffix = se;
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

            foreach (var suffixGroup in Affix.Suffixes)
            {
                foreach (var sptr in suffixGroup.Entries.Where(e => !string.IsNullOrEmpty(e.Key)))
                {
                    if (IsReverseSubset(sptr.Key, word))
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
                                        !pfx.ContainsContClass(Affix.Circumfix)
                                    )
                                    &&
                                    !sptr.ContainsContClass(Affix.Circumfix)
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
                                rv = CheckWordSuffix(suffixGroup, sptr, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? new FlagValue() : Affix.OnlyInCompound);
                                if (rv != null)
                                {
                                    SuffixGroup = suffixGroup;
                                    Suffix = sptr;
                                    SuffixFlag = suffixGroup.AFlag;

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
                                        // LANG_hu section: spec. Hungarian rule
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

        /// <summary>
        /// Check word for two-level suffixes.
        /// </summary>
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
                    if (IsReverseSubset(sptr.Key, word))
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
            var w = 0;

            if (words == null)
            {
                w = 1;
                words = def;
            }

            if (words == null)
            {
                return false;
            }

            var btinfo = new List<MetacharData>
            {
                new MetacharData()
            };

            int bt = 0;

            words[wnum] = rv;

            // has the last word COMPOUNDRULE flag?
            if (!rv.HasFlags)
            {
                words[wnum] = null;
                if (w != 0)
                {
                    words = null;
                }

                return false;
            }

            var ok = 0;
            foreach (var compoundRule in Affix.CompoundRules)
            {
                foreach (var flag in compoundRule)
                {
                    if (!flag.Equals('*') && !flag.Equals('?') && rv.ContainsFlag(flag))
                    {
                        ok = 1;
                        break;
                    }
                }
            }

            if (ok == 0)
            {
                words[wnum] = null;
                if (w != 0)
                {
                    words = null;
                }

                return false;
            }

            for (var i = 0; i < Affix.CompoundRules.Length; i++)
            {
                var pp = 0; // pattern position
                var wp = 0; // "words" position
                int ok2;
                ok = 1;
                ok2 = 1;
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
                            ok2 = 1;
                            pp += 2;
                            btinfo[bt].btpp = pp;
                            btinfo[bt].btwp = wp;

                            while (wp <= wend)
                            {
                                if (!words[wp].HasFlags || !words[wp].ContainsFlag(Affix.CompoundRules[i][pp - 2]))
                                {
                                    ok2 = 0;
                                    break;
                                }

                                wp++;
                            }

                            if (wp <= wnum)
                            {
                                ok2 = 0;
                            }

                            btinfo[bt].btnum = wp - btinfo[bt].btwp;

                            if (btinfo[bt].btnum > 0)
                            {
                                ++bt;
                                btinfo.Add(new MetacharData());
                            }
                            if (ok2 != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            ok2 = 1;
                            if (
                                words[wp] == null
                                ||
                                !words[wp].HasFlags
                                ||
                                !words[wp].ContainsFlag(Affix.CompoundRules[i][pp])
                            )
                            {
                                ok = 0;
                                break;
                            }

                            pp++;
                            wp++;

                            if (Affix.CompoundRules[i].Length == pp && wp <= wnum)
                            {
                                ok = 0;
                            }
                        }
                    }

                    if (ok != 0 && ok2 != 0)
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
                            ok = 1;
                            btinfo[bt - 1].btnum--;
                            pp = btinfo[bt - 1].btpp;
                            wp = btinfo[bt - 1].btwp + btinfo[bt - 1].btnum;
                        }
                        while ((btinfo[bt - 1].btnum < 0) && (--bt != 0));
                    }

                }
                while (bt != 0);

                if (
                    ok != 0
                    && ok2 != 0
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
                    ok != 0
                    && ok2 != 0
                    && pp < Affix.CompoundRules[i].Length
                    && pp + 1 < Affix.CompoundRules[i].Length
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
                    ok != 0
                    && ok2 != 0
                    && Affix.CompoundRules[i].Length <= pp
                )
                {
                    return true;
                }
            }

            words[wnum] = null;
            if (w != 0)
            {
                words = null;
            }

            return false;
        }

        /// <summary>
        /// Forbid compounding with neighbouring upper and lower case characters at word bounds.
        /// </summary>
        private bool CompoundCaseCheck(string word, int pos)
        {
            // NOTE: this implementation could be much simpler but an attempt is made here
            // to preserve the same result when indexes may be out of bounds
            var a = pos - 1;
            var b = pos;
            var hasUpper = false;

            if (a >= 0)
            {
                if (word[a] == '-')
                {
                    return false;
                }

                if (char.IsUpper(word, a))
                {
                    hasUpper = true;
                }
            }

            if (b < word.Length)
            {
                if (word[b] == '-')
                {
                    return false;
                }

                if (!hasUpper && char.IsUpper(word, b))
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
                                && StringExtensions.EqualsOffset(word, pos - r1.Word.Length, r1.Word, 0, r1.Word.Length)
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
                                StringExtensions.EqualsOffset(word, pos - len, patternEntry.Pattern, 0, len)
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

                    rIndex++; // search for the next letter
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

        private bool IsReverseSubset(string s1, string s2)
        {
            var len = s2.Length;
            var index1 = 0;
            var index2 = len - 1;
            while
            (
                len > 0
                &&
                index1 < s1.Length
                &&
                (
                    (s1[index1] == s2[index2])
                    ||
                    (s1[index1] == '.')
                )
            )
            {
                index1++;
                index2--;
                len--;
            }

            return index1 >= s1.Length;
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

        /// <summary>
        /// See if two-level suffix is present in the word.
        /// </summary>
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

                if (TestCondition(se, tmpword)) // TODO: make sure this does not require reversal
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

        private bool TestCondition(PrefixEntry entry, string word)
        {
            return entry.Conditions.IsStartingMatch(word);
        }

        private bool TestCondition(SuffixEntry entry, string word)
        {
            return entry.Conditions.IsEndingMatch(word);
        }

        private bool TryConvertInput(string input, out string converted)
        {
            converted = string.Empty;

            var appliedConversion = false;
            for (var i = 0; i < input.Length; i++)
            {
                var replacementEntry = FindLargestMatchingInputConversion(input.Substring(i));
                var replacementText = replacementEntry == null
                    ? string.Empty
                    : ExtractReplacementText(input.Length - i, replacementEntry, i == 0);

                if (replacementText.Length == 0)
                {
                    converted += input[i];
                }
                else
                {
                    converted += replacementText;
                    i += replacementEntry.Pattern.Length - 1;
                    appliedConversion = true;
                }
            }

            return appliedConversion;
        }

        /// <summary>
        /// Finds an input conversion matching the longest version of the given <paramref name="text"/> from the left.
        /// </summary>
        /// <param name="text">The text to find a matching input conversion for.</param>
        /// <returns>The best matching input conversion.</returns>
        /// <seealso cref="AffixConfig.InputConversions"/>
        private MultiReplacementEntry FindLargestMatchingInputConversion(string text)
        {
            MultiReplacementEntry entry = null;
            for (var searchLength = text.Length; searchLength > 0; searchLength--)
            {
                if (Affix.InputConversions.TryGetValue(text.Substring(0, searchLength), out entry))
                {
                    break;
                }
            }

            return entry;
        }

        private string ExtractReplacementText(int remainingCharactersToReplace, ReplacementEntry entry, bool atStart)
        {
            var type = remainingCharactersToReplace == entry.Pattern.Length
                ? (atStart ? ReplacementValueType.Isol : ReplacementValueType.Fin)
                : (atStart ? ReplacementValueType.Ini : ReplacementValueType.Med);

            while (type != ReplacementValueType.Med && string.IsNullOrEmpty(entry[type]))
            {
                type = (type == ReplacementValueType.Fin && !atStart) ? ReplacementValueType.Med : type - 1;
            }

            return entry[type] ?? string.Empty;
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
            capType = CapitalizationTypeUtilities.GetCapitalizationType(dest);
            return dest.Length;
        }

        private static int CountMatchingFromLeft(string text, char character)
        {
            int count = 0;

            if (text != null)
            {
                while (count < text.Length && text[count] == character)
                {
                    count++;
                }
            }

            return count;
        }

        private static int CountMatchingFromRight(string text, char character)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            int lastIndex = text.Length - 1;
            int searchIndex = lastIndex;
            while (searchIndex >= 0 && text[searchIndex] == character)
            {
                searchIndex--;
            }

            return lastIndex - searchIndex;
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
