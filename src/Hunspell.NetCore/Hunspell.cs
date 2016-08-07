using Hunspell.Utilities;
using System;
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

            CapitalizationType capType;
            int abbv;
            int wl;
            string scw;

            string wordToClean;
            if (Affix.InputConversions.Count > 0)
            {
                ConvertInput(word, out wordToClean);
            }
            else
            {
                wordToClean = word;
            }

            wl = CleanWord2(out scw, wordToClean, out capType, out abbv);

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
                    if (abbv != 0 && rv == null)
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
                he = AffixCheck(word, 0);

                // check compound restriction and onlyupcase
                if (he != null)
                {
                    throw new NotImplementedException();
                }

                if (he != null)
                {
                    throw new NotImplementedException();
                }
                else if (Affix.HasCompound)
                {
                    // try check compound word
                    throw new NotImplementedException();
                }
            }

            return he;
        }

        private DictionaryEntry AffixCheck(string word, int needFlag, CompoundOptions inCompound = CompoundOptions.Not)
        {
            DictionaryEntry rv = null;

            rv = PrefixCheck(word, inCompound, needFlag);
            if (rv != null)
            {
                return rv;
            }

            rv = SuffixCheck(word, 0, null, null, 0, needFlag, inCompound);

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

        private DictionaryEntry PrefixCheck(string word, CompoundOptions inCompound, int needFlag)
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

        private DictionaryEntry CheckWordPrefix(AffixEntryGroup<PrefixEntry> group, PrefixEntry entry, string word, CompoundOptions inCompound, int needFlag)
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
                        he = SuffixCheck(tmpword, AffixEntryOptions.CrossProduct, group, entry, 0, needFlag, inCompound);
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

        private DictionaryEntry SuffixCheck(string word, AffixEntryOptions sfxOpts, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, int cclass, int needFlag, CompoundOptions inCompound)
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
                            var rv = CheckWordSuffix(affixGroup, affixEntry, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, (inCompound != 0 ? 0 : Affix.OnlyInCompound));
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
            (sptr.HasContClass&&
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
                                var rv = CheckWordSuffix(affixGroup, sptr, word, sfxOpts, pfxGroup, pfx, cclass, needFlag, inCompound != CompoundOptions.Not ? 0 : Affix.OnlyInCompound);
                                if(rv != null)
                                {
                                    if (!sptr.HasContClass)
                                    {
                                        // TODO: need to work around some thread safety bugs
                                        throw new NotImplementedException();
                                    }
                                    else if(StringComparer.OrdinalIgnoreCase.Equals(Affix.Culture.TwoLetterISOLanguageName, "hu")
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

        private DictionaryEntry CheckWordSuffix(AffixEntryGroup<SuffixEntry> group, SuffixEntry entry, string word, AffixEntryOptions optFlags, AffixEntryGroup<PrefixEntry> pfxGroup, PrefixEntry pfx, int cclass, int needFlag, int badFlag)
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
