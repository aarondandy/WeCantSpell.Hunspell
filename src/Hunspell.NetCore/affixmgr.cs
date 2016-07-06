/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Hunspell, based on MySpell.
 *
 * The Initial Developers of the Original Code are
 * Kevin Hendricks (MySpell) and Németh László (Hunspell).
 * Portions created by the Initial Developers are Copyright (C) 2002-2005
 * the Initial Developers. All Rights Reserved.
 *
 * Contributor(s): David Einstein, Davide Prina, Giuseppe Modugno,
 * Gianluca Turconi, Simon Brouwer, Noll János, Bíró Árpád,
 * Goldman Eleonóra, Sarlós Tamás, Bencsáth Boldizsár, Halácsy Péter,
 * Dvornik László, Gefferth András, Nagy Viktor, Varga Dániel, Chris Halls,
 * Rene Engelhard, Bram Moolenaar, Dafydd Jones, Harri Pitkänen
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
/*
 * Copyright 2002 Kevin B. Hendricks, Stratford, Ontario, Canada
 * And Contributors.  All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * 3. All modifications to the source code must be clearly marked as
 *    such.  Binary redistributions based on modified source code
 *    must be clearly marked as modified versions in the documentation
 *    and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY KEVIN B. HENDRICKS AND CONTRIBUTORS
 * ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL
 * KEVIN B. HENDRICKS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 */

/*
 *
 * This is a modified version of the Hunspell source code for the
 * purpose of creating an idiomatic port.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using Hunspell.Utilities;

using Flag = System.UInt16;
using MapEntry = System.Collections.Generic.List<string>;
using FlagEntry = System.Collections.Generic.List<ushort>;

namespace Hunspell
{
    public class AffixMgr
    {
        private const int DupSfxFlag = 1;
        private const int DupPfxFlag = 2;

        public AffixMgr(string affPath, List<HashMgr> ptr, string key = null)
        {
            AllDic = ptr;
            HMgr = ptr.First();

            if (!ParseFile(affPath, key))
            {
                throw new HunspellException($"Failure loading aff file {affPath}");
            }

            if (CpdMin == -1)
            {
                CpdMin = ATypes.MinCpdLen;
            }
        }

        public PfxEntry[] PStart { get; private set; } = new PfxEntry[ATypes.SetSize];

        public SfxEntry[] SStart { get; private set; } = new SfxEntry[ATypes.SetSize];

        public PfxEntry[] PFlag { get; private set; } = new PfxEntry[ATypes.SetSize];

        public SfxEntry[] SFlag { get; private set; } = new SfxEntry[ATypes.SetSize];

        private List<HashMgr> AllDic { get; } = new List<HashMgr>();

        private HashMgr HMgr { get; }

        /// <summary>
        /// The keyboard string.
        /// </summary>
        public string KeyString { get; private set; }

        /// <summary>
        /// The try string.
        /// </summary>
        public string TryString { get; private set; }

        /// <summary>
        /// The name of the character set used by the .dict and .aff .
        /// </summary>
        public string Encoding { get; private set; }

        private CsInfo CsConv { get; }

        /// <summary>
        /// Indicates if the aff file used a UTF-8 encoding.
        /// </summary>
        public bool IsUtf8 { get; private set; }

        /// <summary>
        /// Indicates if agglutinative languages with right-to-left writing system are in use.
        /// </summary>
        public bool ComplexPrefixes { get; private set; }

        /// <summary>
        /// The flag used by the controlled compound words.
        /// </summary>
        public Flag CompoundFlag { get; private set; }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        private Flag CompoundBegin { get; set; }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        private Flag CompoundMiddle { get; set; }

        /// <summary>
        /// A flag used by compound words.
        /// </summary>
        private Flag CompoundEnd { get; set; }

        /// <summary>
        /// The flag sign compounds in dictionary.
        /// </summary>
        private Flag CompoundRoot { get; set; }

        private Flag CompoundForbidFlag { get; set; }

        private Flag CompoundPermitFlag { get; }

        private bool CompoundMoreSuffixes { get; set; }

        private bool CheckCompoundDup { get; set; }

        private bool CheckCompoundRep { get; set; }

        private bool CheckCompoundCase { get; set; }

        private bool CheckCompoundTriple { get; set; }

        private bool SimplifiedTriple { get; set; }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        public Flag ForbiddenWord { get; private set; } = 65510;

        public Flag NoSuggest { get; private set; }

        public Flag NoNgramSuggest { get; private set; }

        /// <summary>
        /// A flag used by needaffixs.
        /// </summary>
        public Flag NeedAffix { get; private set; }

        /// <summary>
        /// The minimal length for words in compounds.
        /// </summary>
        private int CpdMin { get; set; } = -1;

        private bool ParsedRep { get; }

        public List<ReplEntry> RepTable { get; private set; }

        public RepList IConvTable { get; }

        public RepList OConvTable { get; }

        private bool ParsedMapTable { get; }

        public List<MapEntry> MapTable { get; } = new List<MapEntry>();

        private bool ParsedBreakTable { get; set; }

        public List<string> BreakTable { get; } = new List<string>();

        private bool ParsedCheckCpd { get; }

        private List<PatEntry> CheckCpdTable { get; } = new List<PatEntry>();

        private int SimplifiedCpd { get; }

        private bool ParsedDefCpd { get; }

        public List<FlagEntry> DefCpdTable { get; private set; }

        public PhoneTable Phone { get; }

        public int MaxNgramSugs { get; set; } = -1;

        public int MaxCpdSugs { get; private set; } = -1;

        public int MaxDiff { get; private set; } = -1;

        public bool OnlyMaxDiff { get; private set; }

        public bool NoSplitSugs { get; private set; }

        public bool SugsWithDots { get; private set; }

        private int CpdWordMax { get; set; } = -1;

        private int CpdMaxSyllable { get; }

        private sbyte[] CpdVowels { get; }

        private List<char> CpdVowelsUtf16 { get; }

        /// <summary>
        /// A flag used by <see cref="CompoundCheck(sbyte[], short, short, short, short, HEntry[], HEntry[], sbyte, sbyte, ref int)"/>.
        /// </summary>
        private string CpdSyllableNum { get; set; }

        private sbyte[] PfxAppnd { get; }

        private sbyte[] SfxAppnd { get; }

        private int SfxExtra { get; }

        private Flag SfxFlag { get; }

        public sbyte[] Derived { get; }

        private SfxEntry Sfx { get; }

        private PfxEntry Pfx { get; }

        /// <summary>
        /// A flag used by the controlled compound words.
        /// </summary>
        private bool CheckNum { get; set; }

        /// <summary>
        /// Extra word characters.
        /// </summary>
        public string WordChars { get; private set; }

        /// <summary>
        /// Extra word characters.
        /// </summary>
        public string WordCharsUtf16 { get; private set; }

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics charachters).
        /// </summary>
        public string IgnoredChars { get; private set; }

        /// <summary>
        /// Ignored characters (for example, Arabic optional diacretics charachters).
        /// </summary>
        public string IgnoredCharsUtf16 { get; private set; }

        public string Version { get; }

        /// <summary>
        /// The language for language specific codes.
        /// </summary>
        private string Lang { get; set; }

        public int LangNum { get; }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        private Flag LemmaPresent { get; set; }

        /// <summary>
        /// A flag used by circumfixes.
        /// </summary>
        private Flag Circumfix { get; set; }

        /// <summary>
        /// A flag used by fogemorphemes.
        /// </summary>
        public Flag OnlyInCompound { get; private set; }

        /// <summary>
        /// A flag used by forbidden words.
        /// </summary>
        public Flag KeepCase { get; private set; }

        /// <summary>
        /// A flag used by forceucase.
        /// </summary>
        public Flag ForceUCase { get; private set; }

        /// <summary>
        /// A flag used by warn.
        /// </summary>
        public Flag Warn { get; private set; }

        public bool ForbidWarn { get; private set; }

        /// <summary>
        /// A flag used by the affix generator.
        /// </summary>
        private Flag SubStandard { get; set; }

        public bool CheckSharps { get; private set; }

        public bool FullStrip { get; private set; }

        public bool HaveContClass { get; private set; }

        private sbyte[] ContClasses { get; } = new sbyte[ATypes.ContSize];

        public int Compound
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [CLSCompliant(false)]
        public HEntry AffixCheck(sbyte[] word, int len, ushort needFlag = 0, sbyte inCompound = ATypes.InCpdNot)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry PrefixCheck(sbyte[] word, int len, sbyte inCompound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return <c>true</c> is <paramref name="sub"/> is a leading subset of <paramref name="super"/>. Dots are for infixes.
        /// </summary>
        /// <param name="sub">The subset to check.</param>
        /// <param name="super">The string to compare the subset against.</param>
        /// <returns>A boolean indicating if <paramref name="sub"/> is a leading subset of <paramref name="super"/>.</returns>
        public bool IsSubset(string sub, string super)
        {
            if (sub == null || super == null || sub.Length > super.Length)
            {
                return false;
            }

            for (int i = 0; i < sub.Length; i++)
            {
                var subChar = sub[i];
                if (subChar == '.')
                {
                    break;
                }
                if (subChar != super[i])
                {
                    return false;
                }
            }

            return true;
        }

        [CLSCompliant(false)]
        public HEntry PrefixCheckTwoSfx(sbyte[] word, int len, sbyte inCompound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int IsRevSubset(sbyte[] s1, sbyte[] endOfS2, int len)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry SuffixCheck(sbyte[] word, int len, int sfxOpts, PfxEntry ppfx, Flag cclass = default(Flag), Flag needFlag = default(Flag), sbyte inCompound = ATypes.InCpdNot)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry SuffixCheckTwoSfx(sbyte[] word, int len, int sfxOpts, PfxEntry ppfx, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] AffixCheckMorph(sbyte[] word, int len, Flag needFlag = default(Flag), sbyte inCompound = ATypes.InCpdNot)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] PrefixCheckMorph(sbyte[] word, int len, sbyte inCompound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] SuffixCheckMorph(sbyte[] word, int len, int sfxOpts, PfxEntry ppfx, Flag cclass = default(Flag), Flag needFlag = default(Flag), sbyte inCompound = ATypes.InCpdNot)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] PrefixCheckTwoSfxMorph(sbyte[] word, int len, sbyte inCompound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] SuffixCheckTwoSfxMorph(sbyte[] word, int len, int sfxOpts, PfxEntry ppfx, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] MorphGen(sbyte[] tx, int wl, ushort[] ap, ushort al, sbyte[] morph, sbyte[] targetMorph, int level)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int ExpandRootWord(GuessWord[] wlst, int maxn, sbyte[] ts, int wl, ushort[] ap, ushort al, sbyte[] bad, int unknown1, ushort[] unknown2)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public short GetSyllable(sbyte[] word)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int CpdRepCheck(ushort[] word, int len)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int CpdPatCheck(ushort[] word, int len, HEntry r1, HEntry r2, sbyte affixed)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int DefCpdCheck(HEntry[][] words, short wnum, HEntry rv, HEntry[] rwords, sbyte all)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int CpdCaseCheck(sbyte[] word, int len)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int CandidateCheck(sbyte[] word, int len)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public void SetCMinMax(ref int cmin, ref int cmax, sbyte[] word, int len)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry CompoundCheck(sbyte[] word, short wordNum, short numSyllable, short maxWordNum, short wNum, HEntry[] words, HEntry[] rWords, sbyte huMovRule, sbyte isSug, ref int info)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int CompoundCheckMorph(sbyte[] word, int len, short wordNum, short numSyllable, short maxWordNum, short wNum, HEntry[] words, HEntry[] rWords, sbyte huMovRule, ref sbyte[] result, sbyte[] partResult)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public List<sbyte[]> GetSuffixWords(ushort[] suff, int len, sbyte[] rootWord)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry Lookup(sbyte[] word)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] GetSuffixed(sbyte _)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] EncodeFlag(ushort aFlag)
        {
            throw new NotImplementedException();
        }

        private static readonly Regex LineStringParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]+(.+)[ \t]*$");
        private static readonly Regex SingleCommandParseRegex = new Regex(@"^[ \t]*(\w+)[ \t]*$");
        private static readonly Regex CommentLineRegex = new Regex(@"^\s*[#].*");

        private bool ParseFile(string affPath, string key)
        {
            var dupFlags = new sbyte[ATypes.ContSize];
            var dupFlagsNeedsCleared = true; // TODO: with .NET there is no need to clear the array
            var firstLine = true;
            int tempInt;
            string line;
            Flag tempFlag;
            EntriesContainer sAffentries = null;
            EntriesContainer pAffentries = null;

            using (var afflst = new FileMgr(affPath, key))
            {
                while (afflst.GetLine(out line))
                {
                    /* remove byte order mark */
                    if (firstLine)
                    {
                        firstLine = false;
                        if (line.StartsWith("\xEF\xBB\xBF"))
                        {
                            // Affix file begins with byte order mark: possible incompatibility with old Hunspell versions
                            line = line.Substring(3);
                        }
                    }

                    if (CommentLineRegex.IsMatch(line))
                    {
                        continue; // skip the comment lines
                    }

                    var singleCommandParsed = SingleCommandParseRegex.Match(line);
                    if (singleCommandParsed.Success)
                    {
                        switch (singleCommandParsed.Groups[1].Value)
                        {
                            /* parse COMPLEXPREFIXES for agglutinative languages with right-to-left writing system */
                            case "COMPLEXPREFIXES":
                                ComplexPrefixes = true;
                                break; // the origin code continues on after reading this indicator
                            case "COMPOUNDMORESUFFIXES":
                                CompoundMoreSuffixes = true;
                                break;
                            case "CHECKCOMPOUNDDUP":
                                CheckCompoundDup = true;
                                break;
                            case "CHECKCOMPOUNDREP":
                                CheckCompoundRep = true;
                                break;
                            case "CHECKCOMPOUNDTRIPLE":
                                CheckCompoundTriple = true;
                                break;
                            case "SIMPLIFIEDTRIPLE":
                                SimplifiedTriple = true;
                                break;
                            case "CHECKCOMPOUNDCASE":
                                CheckCompoundCase = true;
                                break;
                            /* parse in the flag used by the controlled compound words */
                            case "CHECKNUM":
                                CheckNum = true;
                                break;
                            case "ONLYMAXDIFF":
                                OnlyMaxDiff = true;
                                break;
                            case "NOSPLITSUGS":
                                NoSplitSugs = true;
                                break;
                            case "FULLSTRIP":
                                FullStrip = true;
                                break;
                            case "SUGSWITHDOTS":
                                SugsWithDots = true;
                                break;
                            case "FORBIDWARN":
                                ForbidWarn = true;
                                break;
                            case "CHECKSHARPS":
                                CheckSharps = true;
                                break;
                        }
                    }

                    var lineParsed = LineStringParseRegex.Match(line);
                    if (lineParsed.Success)
                    {
                        var commandName = lineParsed.Groups[1].Value;
                        var value = lineParsed.Groups[2].Value;
                        switch (commandName)
                        {
                            /* parse in the keyboard string */
                            case "KEY":
                                KeyString = value;
                                continue;
                            /* parse in the try string */
                            case "TRY":
                                TryString = value;
                                continue;
                            /* parse in the name of the character set used by the .dict and .aff */
                            case "SET":
                                Encoding = value;
                                IsUtf8 = value == "UTF-8";
                                continue;
                            /* parse in the language for language specific codes */
                            case "LANG":
                                Lang = value;
                                continue;
                            /* parse in the flag used by the controlled compound words */
                            case "COMPOUNDFLAG":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    CompoundFlag = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by compound words */
                            case "COMPOUNDBEGIN":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    if (ComplexPrefixes)
                                    {
                                        CompoundEnd = tempFlag;
                                    }
                                    else
                                    {
                                        CompoundBegin = tempFlag;
                                    }

                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by compound words */
                            case "COMPOUNDMIDDLE":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    CompoundMiddle = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by compound words */
                            case "COMPOUNDEND":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    if (ComplexPrefixes)
                                    {
                                        CompoundBegin = tempFlag;
                                    }
                                    else
                                    {
                                        CompoundEnd = tempFlag;
                                    }

                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the data used by compound_check() method */
                            case "COMPOUNDWORDMAX":
                                if (ParseNum(value, out tempInt, afflst))
                                {
                                    CpdWordMax = tempInt;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag sign compounds in dictionary */
                            case "COMPOUNDROOT":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    CompoundRoot = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by compound_check() method */
                            case "COMPOUNDFORBIDFLAG":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    CompoundForbidFlag = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            case "NOSUGGEST":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    NoSuggest = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            case "NONGRAMSUGGEST":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    NoNgramSuggest = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by forbidden words */
                            case "FORBIDDENWORD":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    ForbiddenWord = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by forbidden words */
                            case "LEMMA_PRESENT":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    LemmaPresent = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by circumfixes */
                            case "CIRCUMFIX":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    Circumfix = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by fogemorphemes */
                            case "ONLYINCOMPOUND":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    OnlyInCompound = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by `needaffixs' */
                            case "PSEUDOROOT":
                            case "NEEDAFFIX":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    NeedAffix = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the minimal length for words in compounds */
                            case "COMPOUNDMIN":
                                if (ParseNum(value, out tempInt, afflst))
                                {
                                    CpdMin = tempInt;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the max. words and syllables in compounds */
                            case "COMPOUNDSYLLABLE":
                                throw new NotImplementedException();
                            /* parse in the flag used by compound_check() method */
                            case "SYLLABLENUM":
                                CpdSyllableNum = value;
                                continue;
                            /* parse in the extra word characters */
                            case "WORDCHARS":
                                WordChars = value;
                                WordCharsUtf16 = value;
                                continue;
                            /* parse in the ignored characters (for example, Arabic optional diacretics charachters) */
                            case "IGNORE":
                                IgnoredChars = value;
                                IgnoredCharsUtf16 = value;
                                continue;
                            /* parse in the typical fault correcting table */
                            case "REP":
                                if (ParseRepTable(value, afflst))
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the input conversion table */
                            case "ICONV":
                                throw new NotImplementedException("TODO: conv table");
                            /* parse in the input conversion table */
                            case "OCONV":
                                throw new NotImplementedException("TODO: conv table");
                            /* parse in the input conversion table */
                            case "PHONE":
                                throw new NotImplementedException("TODO: phonetable");
                            /* parse in the checkcompoundpattern table */
                            case "CHECKCOMPOUNDPATTERN":
                                throw new NotImplementedException("TODO: checkcpdtable");
                            /* parse in the defcompound table */
                            case "COMPOUNDRULE":
                                if (ParseDefCpdTable(value, afflst))
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the related character map table */
                            case "MAP":
                                throw new NotImplementedException("TODO: map");
                            /* parse in the word breakpoints table */
                            case "BREAK":
                                throw new NotImplementedException("TODO: break");
                            case "VERSION":
                                throw new NotImplementedException("TODO: persion parsing stuff");
                            case "MAXNGRAMSUGS":
                                if (ParseNum(value, out tempInt, afflst))
                                {
                                    MaxNgramSugs = tempInt;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            case "MAXDIFF":
                                if (ParseNum(value, out tempInt, afflst))
                                {
                                    MaxDiff = tempInt;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            case "MAXCPDSUGS":
                                if (ParseNum(value, out tempInt, afflst))
                                {
                                    MaxCpdSugs = tempInt;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by forbidden words */
                            case "KEEPCASE":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    KeepCase = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by `forceucase' */
                            case "FORCEUCASE":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    ForceUCase = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by `warn' */
                            case "WARN":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    Warn = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /* parse in the flag used by the affix generator */
                            case "SUBSTANDARD":
                                if (ParseFlag(value, out tempFlag, afflst))
                                {
                                    SubStandard = tempFlag;
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            /*parse this affix: P - prefix, S - suffix */
                            case "PFX":
                            case "SFX":
                                char ft = commandName[0];
                                if (ComplexPrefixes)
                                {
                                    ft = (ft == 'S') ? 'P' : 'S';
                                }
                                if (dupFlagsNeedsCleared)
                                {
                                    Array.Clear(dupFlags, 0, dupFlags.Length);
                                    dupFlagsNeedsCleared = false;
                                }

                                var parsedOk = ft == 'P'
                                    ? ParseAffix(value, ft, afflst, dupFlags, ref pAffentries)
                                    : ParseAffix(value, ft, afflst, dupFlags, ref sAffentries);

                                if (parsedOk)
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }

                        }
                    }
                } // end read loop

                FinishFileMgr(afflst);
            }

            // affix trees are sorted now

            // now we can speed up performance greatly taking advantage of the
            // relationship between the affixes and the idea of "subsets".

            // View each prefix as a potential leading subset of another and view
            // each suffix (reversed) as a potential trailing subset of another.

            // To illustrate this relationship if we know the prefix "ab" is found in the
            // word to examine, only prefixes that "ab" is a leading subset of need be
            // examined.
            // Furthermore is "ab" is not present then none of the prefixes that "ab" is
            // is a subset need be examined.
            // The same argument goes for suffix string that are reversed.

            // Then to top this off why not examine the first char of the word to quickly
            // limit the set of prefixes to examine (i.e. the prefixes to examine must
            // be leading supersets of the first character of the word (if they exist)

            // To take advantage of this "subset" relationship, we need to add two links
            // from entry.  One to take next if the current prefix is found (call it
            // nexteq)
            // and one to take next if the current prefix is not found (call it nextne).

            // Since we have built ordered lists, all that remains is to properly
            // initialize
            // the nextne and nexteq pointers that relate them

            ProcessPfxOrder();
            ProcessSfxOrder();

            /* get encoding for CHECKCOMPOUNDCASE */
            if (!IsUtf8)
            {
                // TODO: considering making it all UTF16
            }

            // default BREAK definition
            if (!ParsedBreakTable)
            {
                BreakTable.Add("-");
                BreakTable.Add("^-");
                BreakTable.Add("-$");
                ParsedBreakTable = true;
            }

            return true;
        }

        private bool ParseFlag(string textValue, out ushort @out, FileMgr af)
        {
            @out = HMgr.DecodeFlag(textValue);

            return true;
        }

        private bool ParseNum(string textValue, out int @out, FileMgr af)
        {
            return int.TryParse(textValue, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out @out);
        }

        private bool ParseCpdSyllable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseRepTable(string entryLine, FileMgr af)
        {
            entryLine = entryLine.TrimStartTabOrSpace().TrimEndOfLine();

            if (string.IsNullOrEmpty(entryLine))
            {
                return false;
            }

            int expectedTableSize;
            if (RepTable == null && int.TryParse(entryLine, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out expectedTableSize))
            {
                RepTable = new List<ReplEntry>(expectedTableSize);
                return true;
            }

            var entryTextParts = entryLine.SplitOnTabOrSpace();
            if (entryTextParts.Length > 0)
            {
                var replEntry = new ReplEntry();
                int type = 0;

                replEntry.Pattern = entryTextParts[0];
                if (replEntry.Pattern.StartsWith('^'))
                {
                    replEntry.Pattern = replEntry.Pattern.Substring(1);
                    type = 1;
                }

                replEntry.Pattern = replEntry.Pattern.Replace('_', ' ');

                if (replEntry.Pattern.Length == 0 && replEntry.Pattern.EndsWith('$'))
                {
                    type += 2;
                    replEntry.Pattern = replEntry.Pattern.SubstringFromEnd(1);
                }

                if (entryTextParts.Length > 1)
                {
                    replEntry.OutStrings[type] = entryTextParts[1].Replace('_', ' ');
                }

                if (RepTable == null)
                {
                    RepTable = new List<ReplEntry>();
                }

                RepTable.Add(replEntry);
                return true;
            }

            return false;
        }

        private bool ParseConvTable(sbyte[] line, FileMgr af, ref RepList[] rl)
        {
            throw new NotImplementedException();
        }

        private bool ParsePhoneTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseMapTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseBreakTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseCheckCpdTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseDefCpdTable(string entryLine, FileMgr af)
        {
            entryLine = entryLine.TrimStartTabOrSpace().TrimEndOfLine();

            if (string.IsNullOrEmpty(entryLine))
            {
                return false;
            }

            int expectedTableSize;
            if (DefCpdTable == null && int.TryParse(entryLine, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out expectedTableSize))
            {
                DefCpdTable = new List<FlagEntry>(expectedTableSize);
                return true;
            }

            var entry = new FlagEntry();

            if (entryLine.IndexOf('(') >= 0)
            {
                for (var k = 0; k < entryLine.Length; k++)
                {
                    var chb = k;
                    var che = k + 1;
                    if (entryLine[k] == '(')
                    {
                        var parpos = entryLine.IndexOf(')', k);
                        if (parpos >= 0)
                        {
                            chb = k + 1;
                            che = parpos;
                            k = parpos;
                        }
                    }

                    if (entryLine[chb] == '*' || entryLine[chb] == '?')
                    {
                        entry.Add((Flag)entryLine[chb]);
                    }
                    else
                    {
                        if (!HMgr.DecodeFlags(entry, entryLine.Substring(chb, che - chb), af))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (!HMgr.DecodeFlags(entry, entryLine, af))
                {
                    return false;
                }
            }

            DefCpdTable.Add(entry);
            return true;
        }

        public class EntriesContainer
        {
            public EntriesContainer(char at, AffixMgr mgr)
            {
                At = at;
                Mgr = mgr;
                Entries = null;
                NumberEntriesRead = 0;
                NumberEntriesExpected = 0;
            }

            public List<AffEntry> Entries { get; private set; }

            private AffixMgr Mgr { get; }

            private char At { get; }

            public int NumberEntriesRead { get; set; }

            public int NumberEntriesExpected { get; set; }

            public AffEntry FirstEntry
            {
                get
                {
                    return Entries?.FirstOrDefault();
                }
            }

            public void Initialize(int numents, sbyte opts, ushort aflag)
            {
                NumberEntriesExpected = numents;

                if (Entries == null)
                {
                    Entries = new List<AffEntry>(numents);
                }

                var affEntry = At == 'P'
                    ? new PfxEntry(Mgr)
                    : (AffEntry)new SfxEntry(Mgr);
                affEntry.Opts = opts;
                affEntry.AFlag = aflag;
                Entries.Add(affEntry);
            }

            public AffEntry AddEntry(sbyte opts)
            {
                if (Entries == null)
                {
                    Entries = new List<AffEntry>();
                }

                var affEntry = At == 'P'
                    ? new PfxEntry(Mgr)
                    : (AffEntry)new SfxEntry(Mgr);
                Entries.Add(affEntry);
                affEntry.Opts = (sbyte)(Entries[0].Opts & opts);
                return affEntry;
            }
        }

        private bool ParseAffix(string entryLine, char at, FileMgr af, sbyte[] dupFlags, ref EntriesContainer affentries)
        {
            int numents = 0; // number of AffEntry structures to parse
            ushort aflag = 0; // affix char identifier
            sbyte ff = 0;
            int i = 0;
            var entryLineParts = entryLine.RegexSplitOnTabOrSpace();
            AffEntry entry = null;

            var needsToModifyFirstEntry = affentries != null && affentries.Entries?.Count == 1 && affentries.NumberEntriesRead == 0;
            var firstEntry = affentries?.FirstEntry;

            // split affix header line into pieces
            int np = 0;

            // piece 1 - is type of affix
            np++; // because we already parse PFX and SFX before calling this method just increment it
            if (affentries != null)
            {
                affentries.NumberEntriesRead++;
                entry = needsToModifyFirstEntry
                    ? firstEntry
                    : affentries.AddEntry(ATypes.AeXProduct | ATypes.AeUtf8 | ATypes.AeAliasF | ATypes.AeAliasM);
            }

            // piece 2 - is affix char
            if (entryLineParts.Count > 0)
            {
                np++;
                aflag = HMgr.DecodeFlag(entryLineParts[0].Value);
                if (affentries == null)
                {
                    if (
                        ((at == 'S') && (dupFlags[aflag] & DupSfxFlag) != 0) ||
                        ((at == 'P') && (dupFlags[aflag] & DupPfxFlag) != 0)
                    )
                    {
                        // TODO: warn of multiple definitions of an affix flag
                    }

                    dupFlags[aflag] |= (sbyte)(at == 'S' ? DupSfxFlag : DupPfxFlag);
                }
                else
                {
                    if (firstEntry.AFlag != aflag)
                    {
                        // TODO: warning
                        return false;
                    }

                    if (affentries != null && !needsToModifyFirstEntry && firstEntry != null)
                    {
                        entry.AFlag = firstEntry.AFlag;
                    }
                }
            }

            // piece 3 - is cross product indicator
            if (entryLineParts.Count > 1)
            {
                var piece3 = entryLineParts[1].Value;
                np++;
                if (affentries == null)
                {
                    if (piece3.StartsWith('Y'))
                    {
                        ff = ATypes.AeXProduct;
                    }
                }
                else
                {
                    entry.Strip = piece3;
                    if (ComplexPrefixes)
                    {
                        entry.Strip = entry.Strip.Reverse();
                    }

                    if (entry.Strip == "0")
                    {
                        entry.Strip = string.Empty;
                    }
                }
            }

            // piece 4 - is number of affentries
            if (entryLineParts.Count > 2)
            {
                var piece4 = entryLineParts[2].Value;
                np++;
                if (affentries == null)
                {
                    if (!int.TryParse(piece4, out numents) || numents <= 0 || numents == int.MaxValue)
                    {
                        return false;
                    }

                    sbyte opts = ff;
                    if (IsUtf8)
                    {
                        opts |= ATypes.AeUtf8;
                    }
                    if (HMgr.IsAliasF)
                    {
                        opts |= ATypes.AeAliasF;
                    }
                    if (HMgr.IsAliasM)
                    {
                        opts |= ATypes.AeAliasM;
                    }

                    if (affentries == null)
                    {
                        affentries = new EntriesContainer(at, this);
                        affentries.Initialize(numents, opts, aflag);
                    }
                }
                else
                {
                    entry.MorphCode = null;
                    entry.ContClass = null;
                    entry.ContClassLen = 0;
                    var dashIndex = piece4.IndexOf('/');
                    if (dashIndex >= 0)
                    {
                        entry.Appnd = piece4.Substring(0, dashIndex);
                        var dashStr = piece4.Substring(dashIndex + 1);

                        if (!string.IsNullOrEmpty(IgnoredChars))
                        {
                            entry.Appnd = RemoveChars(entry.Appnd, IsUtf8 ? IgnoredCharsUtf16 : IgnoredChars);
                        }

                        if (ComplexPrefixes)
                        {
                            entry.Appnd = entry.Appnd.Reverse();
                        }

                        if (HMgr.IsAliasF)
                        {
                            int index;
                            if (!int.TryParse(dashStr, out index))
                            {
                                return false;
                            }

                            var contClass = (entry.ContClass ?? Enumerable.Empty<Flag>()).ToList();
                            entry.ContClassLen = checked((short)HMgr.GetAliasF(index, contClass, af));
                            entry.ContClass = contClass.ToArray();

                            if (entry.ContClassLen == 0)
                            {
                                // TODO: warn bad affix flag alias
                            }
                        }
                        else
                        {
                            var contClass = (entry.ContClass ?? Enumerable.Empty<Flag>()).ToList();
                            if (!HMgr.DecodeFlags(contClass, dashStr, af))
                            {
                                return false;
                            }

                            entry.ContClassLen = checked((short)contClass.Count);
                            contClass.Sort(0, entry.ContClassLen, Comparer<Flag>.Default);
                            entry.ContClass = contClass.ToArray();
                        }

                        HaveContClass = true;

                        for (ushort _i = 0; _i < entry.ContClassLen; _i++)
                        {
                            ContClasses[_i] = 1;
                        }
                    }
                    else
                    {
                        entry.Appnd = piece4;
                        if (!string.IsNullOrEmpty(IgnoredChars))
                        {
                            entry.Appnd = entry.Appnd.Reverse();
                        }

                        if (ComplexPrefixes)
                        {
                            entry.Appnd = entry.Appnd.Reverse();
                        }
                    }

                    if (entry.Appnd == "0")
                    {
                        entry.Appnd = string.Empty;
                    }
                }
            }

            // piece 5 - is the conditions descriptions
            if (entryLineParts.Count > 3)
            {
                var chunk = entryLineParts[3].Value;
                np++;
                if (ComplexPrefixes)
                {
                    chunk = chunk.Reverse();
                    chunk = ReverseCondition(chunk);
                }

                if (
                    !string.IsNullOrEmpty(entry.Strip)
                    && chunk != "."
                    && RedundantCondition(at, entry.Strip, entry.Strip.Length, chunk, af.LineNum))
                {
                    chunk = ".";
                }

                if (at == 'S')
                {
                    chunk = chunk.Reverse();
                    chunk = ReverseCondition(chunk);
                }

                if (!EncodeIt(entry, chunk))
                {
                    return false;
                }
            }

            // piece 6
            if (entryLineParts.Count > 4)
            {
                var piece6 = entryLineParts[4];
                var chunk = piece6.Value;
                np++;
                if (HMgr.IsAliasM)
                {
                    int index;
                    if (int.TryParse(chunk, out index))
                    {
                        entry.MorphCode = HMgr.GetAliasM(index);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (ComplexPrefixes)
                    {
                        chunk = chunk.Reverse();
                    }

                    int indexAfterThisPiece = piece6.Index + piece6.Length + 1;
                    if (indexAfterThisPiece >= entryLine.Length)
                    {
                        chunk = chunk + entryLine.Substring(indexAfterThisPiece);
                    }

                    entry.MorphCode = chunk;

                    if (entry.MorphCode == null)
                    {
                        return false;
                    }
                }
            }

            if (np < 4)
            {
                return false;
            }

            var lastEntry = affentries != null && affentries.NumberEntriesRead == affentries.NumberEntriesExpected;
            if (lastEntry)
            {
                foreach (var affEntry in affentries.Entries)
                {
                    var pfxEntry = affEntry as PfxEntry;
                    if (pfxEntry != null)
                    {
                        BuildPfxTree(pfxEntry);
                    }
                    else
                    {
                        BuildSfxTree(affEntry as SfxEntry);
                    }
                }
            }

            return true;
        }

        private string ReverseCondition(string piece)
        {
            if (string.IsNullOrEmpty(piece))
            {
                return piece;
            }

            bool neg = false;
            var chars = piece.ToCharArray();
            for (int k = chars.Length - 1; k >= 0; k--)
            {
                switch (chars[k])
                {
                    case '[':
                        if (neg)
                        {
                            if (k < chars.Length - 1)
                            {
                                chars[k + 1] = '[';
                            }
                        }
                        else
                        {
                            chars[k] = ']';
                        }

                        break;
                    case ']':
                        chars[k] = '[';
                        if (neg && k < chars.Length - 1)
                        {
                            chars[k + 1] = '^';
                        }

                        neg = false;

                        break;
                    case '^':
                        if (k < chars.Length - 1)
                        {
                            if (chars[k + 1] == ']')
                            {
                                neg = true;
                            }
                            else
                            {
                                chars[k + 1] = chars[k];
                            }
                        }

                        break;
                    default:
                        if (neg && k < chars.Length - 1)
                        {
                            chars[k + 1] = chars[k];
                        }
                        break;
                }
            }

            return new string(chars);
        }

        private sbyte[] DebugFlag(sbyte[] result, ushort flag)
        {
            throw new NotImplementedException();
        }

        private int CondLen(string cond)
        {
            int length = 0;
            bool group = false;

            foreach (var st in cond)
            {
                if (st == '[')
                {
                    group = true;
                    length++;
                }
                else if (st == ']')
                {
                    group = false;
                }
                else if (!group && (!IsUtf8 || ((st & 0x80) != 0 || ((st & 0xc0) == 0x80))))
                {
                    length++;
                }
            }

            return length;
        }

        private bool EncodeIt(AffEntry entry, string cs)
        {
            if (cs != ".")
            {
                entry.NumConds = CondLen(cs);
                entry.Conds = cs;
            }
            else
            {
                entry.NumConds = 0;
                entry.Conds = string.Empty;
            }

            return true;
        }

        /// <summary>
        /// we want to be able to quickly access prefix information
        /// both by prefix flag, and sorted by prefix string itself
        /// so we need to set up two indexes
        /// </summary>
        private bool BuildPfxTree(PfxEntry pfxptr)
        {
            PfxEntry ptr;
            PfxEntry pptr;
            PfxEntry ep = pfxptr;

            // get the right starting points
            string key = ep.Key;
            byte flg = (byte)(ep.Flag & 0xff);

            // first index by flag which must exist
            ptr = PFlag[flg];
            ep.FlgNxt = ptr;
            PFlag[flg] = ep;

            // handle the special case of null affix string
            if (string.IsNullOrEmpty(key))
            {
                // always inset them at head of list at element 0
                ptr = PStart[0];
                ep.Next = ptr;
                PStart[0] = ep;
                return false;
            }

            // now handle the normal case
            ep.NextEq = null;
            ep.NextNe = null;

            byte sp = (byte)key[0];
            ptr = PStart[sp];

            // handle the first insert
            if (ptr == null)
            {
                PStart[sp] = ep;
                return false;
            }

            // otherwise use binary tree insertion so that a sorted
            // list can easily be generated later
            pptr = null;
            for (;;)
            {
                pptr = ptr;
                if (string.CompareOrdinal(ep.Key, ptr.Key) <= 0)
                {
                    ptr = ptr.NextEq;
                    if (ptr == null)
                    {
                        pptr.NextEq = ep;
                        break;
                    }
                }
                else
                {
                    ptr = ptr.NextEq;
                    if (ptr == null)
                    {
                        pptr.NextEq = ep;
                        break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// we want to be able to quickly access suffix information
        /// both by suffix flag, and sorted by the reverse of the
        /// suffix string itself; so we need to set up two indexes
        /// </summary>
        private bool BuildSfxTree(SfxEntry sfxptr)
        {
            sfxptr.InitReverseWord();

            SfxEntry ptr;
            SfxEntry pptr;
            SfxEntry ep = sfxptr;

            /* get the right starting point */
            string key = ep.Key;
            byte flg = (byte)(ep.Flag & 0xff);

            // first index by flag which must exist
            ptr = SFlag[flg];
            ep.FlgNxt = ptr;
            SFlag[flg] = ep;

            // next index by affix string

            // handle the special case of null affix string
            if (string.IsNullOrEmpty(key))
            {
                // always inset them at head of list at element 0
                ptr = SStart[0];
                ep.Next = ptr;
                SStart[0] = ep;
                return false;
            }

            // now handle the normal case
            ep.NextEq = null;
            ep.NextNe = null;

            byte sp = (byte)key[0];
            ptr = SStart[sp];

            // handle the first insert
            if (ptr == null)
            {
                SStart[sp] = ep;
                return false;
            }

            // otherwise use binary tree insertion so that a sorted
            // list can easily be generated later
            pptr = null;
            for (;;)
            {
                pptr = ptr;
                if (string.CompareOrdinal(ep.Key, ptr.Key) <= 0)
                {
                    ptr = ptr.NextEq;
                    if (ptr == null)
                    {
                        pptr.NextEq = ep;
                        break;
                    }
                }
                else
                {
                    ptr = ptr.NextNe;
                    if (ptr == null)
                    {
                        ptr.NextNe = ep;
                        break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// reinitialize the PfxEntry links NextEQ and NextNE to speed searching
        /// using the idea of leading subsets this time
        /// </summary>
        private bool ProcessPfxOrder()
        {
            PfxEntry ptr;

            // loop through each prefix list starting point
            for (int i = 1; i < PStart.Length; i++)
            {
                ptr = PStart[i];

                // look through the remainder of the list
                //  and find next entry with affix that
                // the current one is not a subset of
                // mark that as destination for NextNE
                // use next in list that you are a subset
                // of as NextEQ

                for (; ptr != null; ptr = ptr.Next)
                {
                    var nptr = ptr.Next;
                    for (; nptr != null; nptr = nptr.Next)
                    {
                        if (!IsSubset(ptr.Key, nptr.Key))
                        {
                            break;
                        }
                    }

                    ptr.NextNe = nptr;
                    ptr.NextEq = null;
                    if (ptr.Next != null && IsSubset(ptr.Key, ptr.Next.Key))
                    {
                        ptr.NextEq = ptr.Next;
                    }
                }

                // now clean up by adding smart search termination strings:
                // if you are already a superset of the previous prefix
                // but not a subset of the next, search can end here
                // so set NextNE properly

                ptr = PStart[i];
                for (; ptr != null; ptr = ptr.Next)
                {
                    var nptr = ptr.Next;
                    PfxEntry mptr = null;
                    for (; nptr != null; nptr = nptr.Next)
                    {
                        if (!IsSubset(ptr.Key, nptr.Key))
                        {
                            break;
                        }

                        mptr = nptr;
                    }

                    if (mptr != null)
                    {
                        mptr.NextNe = null;
                    }
                }

            }

            return true;
        }

        /// <summary>
        /// initialize the SfxEntry links NextEQ and NextNE to speed searching
        /// using the idea of leading subsets this time
        /// </summary>
        private bool ProcessSfxOrder()
        {
            SfxEntry ptr;

            // loop through each prefix list starting point
            for (int i = 1; i < SStart.Length; i++)
            {
                ptr = SStart[i];

                // look through the remainder of the list
                //  and find next entry with affix that
                // the current one is not a subset of
                // mark that as destination for NextNE
                // use next in list that you are a subset
                // of as NextEQ

                for (; ptr != null; ptr = ptr.Next)
                {
                    var nptr = ptr.Next;
                    for (; nptr != null; nptr = nptr.Next)
                    {
                        if (!IsSubset(ptr.Key, nptr.Key))
                        {
                            break;
                        }
                    }

                    ptr.NextNe = nptr;
                    ptr.NextEq = null;
                    if (ptr.Next != null && IsSubset(ptr.Key, ptr.Next.Key))
                    {
                        ptr.NextEq = ptr.Next;
                    }
                }

                // now clean up by adding smart search termination strings:
                // if you are already a superset of the previous suffix
                // but not a subset of the next, search can end here
                // so set NextNE properly

                ptr = SStart[i];
                for (; ptr != null; ptr = ptr.Next)
                {
                    var nptr = ptr.Next;
                    SfxEntry mptr = null;
                    for (; nptr != null; nptr = nptr.Next)
                    {
                        if (!IsSubset(ptr.Key, nptr.Key))
                        {
                            break;
                        }

                        mptr = nptr;
                    }

                    if(mptr != null)
                    {
                        mptr.NextNe = null;
                    }
                }
            }

            return true;
        }

        private PfxEntry ProcessPfxInOrder(PfxEntry ptr, PfxEntry nPtr)
        {
            if (ptr != null)
            {
                nPtr = ProcessPfxInOrder(ptr.NextNe, nPtr);
                ptr.Next = nPtr;
                nPtr = ProcessPfxInOrder(ptr.NextEq, ptr);
            }
            return nPtr;
        }

        private SfxEntry ProcessSfxInOrder(SfxEntry ptr, SfxEntry nPtr)
        {
            if (ptr != null)
            {
                nPtr = ProcessSfxInOrder(ptr.NextNe, nPtr);
                ptr.Next = nPtr;
                nPtr = ProcessSfxInOrder(ptr.NextEq, ptr);
            }
            return nPtr;
        }

        private bool ProcessPfxTreeToList()
        {
            for (int i = 0; i < PStart.Length; i++)
            {
                PStart[i] = ProcessPfxInOrder(PStart[i], null);
            }

            return true;
        }

        private bool ProcessSfxTreeToList()
        {
            for (int i = 0; i < SStart.Length; i++)
            {
                SStart[i] = ProcessSfxInOrder(SStart[i], null);
            }
            return true;
        }

        private bool RedundantCondition(char _, string strip, int stripLength, string cond, int lineNumber)
        {
            throw new NotImplementedException();
        }

        private void FinishFileMgr(FileMgr affLst)
        {
            // TODO: consider disposing affList here
            ProcessPfxTreeToList();
            ProcessSfxTreeToList();
        }

        private static string RemoveChars(string word, string ignored)
        {
            return new string(word.Where(c => !ignored.Contains(c)).ToArray());
        }
    }
}
