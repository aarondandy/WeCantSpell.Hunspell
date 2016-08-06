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
            int len;

            if (Affix.IgnoredChars.Length != 0)
            {
                w2 = w;
                w2 = w2.RemoveChars(Affix.IgnoredChars);
                word = w2;
                len = w2.Length;
                useBuffer = true;
            }
            else
            {
                w2 = string.Empty;
                word = w;
                len = w.Length;
            }

            if (len == 0)
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
                he = AffixCheck(word, len, 0);

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

        private DictionaryEntry AffixCheck(string word, int len, int needFlag, CompoundOptions inCompound = CompoundOptions.Not)
        {
            DictionaryEntry rv = null;

            rv = PrefixCheck(word, len, inCompound, needFlag);
            if (rv != null)
            {
                return rv;
            }

            rv = SuffixCheck(word, len, 0, null, 0, needFlag, inCompound);

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

        private DictionaryEntry PrefixCheck(string word, int len, CompoundOptions inCompound, int needFlag)
        {
            if (Affix.Prefixes.Length == 0)
            {
                return null;
            }

            throw new NotImplementedException();
        }

        private DictionaryEntry SuffixCheck(string word, int len, int sfxOpts, PrefixEntry pfx, int cclass, int needFlag, CompoundOptions inCompound)
        {
            if (Affix.Suffixes.Length == 0)
            {
                return null;
            }

            throw new NotImplementedException();
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
