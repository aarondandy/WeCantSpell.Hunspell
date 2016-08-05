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

            CapitalizationType capType;
            int abbv;
            int wl;
            string scw;

            string wordToClean;
            if (Affix.InputConversions.Count > 0)
            {
                throw new NotImplementedException();
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

            if (Affix.BreakTable.Length != 0)
            {
                throw new NotImplementedException();
            }

            return new SpellCheckResult(root, resultType, false);
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
                throw new NotImplementedException();
            }

            return he;
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
