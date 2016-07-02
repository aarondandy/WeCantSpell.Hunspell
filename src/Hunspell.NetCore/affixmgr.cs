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

using Flag = System.UInt16;

namespace Hunspell
{
    public class AffixMgr
    {
        private PfxEntry[] PStart { get; } = new PfxEntry[ATypes.SetSize];

        private SfxEntry[] SStart { get; } = new SfxEntry[ATypes.SetSize];

        private PfxEntry[] PFlag { get; } = new PfxEntry[ATypes.SetSize];

        private SfxEntry[] SFlag { get; } = new SfxEntry[ATypes.SetSize];

        private List<HashMgr> AllDic { get; } = new List<HashMgr>();

        [CLSCompliant(false)]
        public sbyte[] KeyString { get; }

        [CLSCompliant(false)]
        public sbyte[] TryString { get; }

        [CLSCompliant(false)]
        public sbyte[] Encoding { get; }

        private CsInfo CsConv { get; }

        public int Utf8 { get; }

        public int ComplexPrefixes { get; }

        [CLSCompliant(false)]
        public Flag CompoundFlag { get; }

        private Flag CompoundBegin { get; }

        private Flag CompoundMiddle { get; }

        private Flag CompoundEnd { get; }

        private Flag CompoundRoot { get; }

        private Flag CompoundForbidFlag { get; }

        private Flag CompoundPermitFlag { get; }

        private int CompoundMoreSuffixes { get; }

        private int CheckCompoundDup { get; }

        private int CheckCompoundRep { get; }

        private int CheckCompoundCase { get; }

        private int CheckCompoundTriple { get; }

        private int SimplifiedTriple { get; }

        [CLSCompliant(false)]
        public Flag ForbiddenWord { get; }

        [CLSCompliant(false)]
        public Flag NoSuggest { get; }

        [CLSCompliant(false)]
        public Flag NoNgramSuggest { get; }

        [CLSCompliant(false)]
        public Flag NeedAffix { get; }

        private int CpdMin { get; }

        private bool ParsedRep { get; }

        public List<ReplEntry> RepTable { get; } = new List<ReplEntry>();

        public RepList IConvTable { get; }

        public RepList OConvTable { get; }

        private bool ParsedMapTable { get; }

        public List<MapEntry> MapTable { get; } = new List<MapEntry>();

        private bool ParsedBreakTable { get; }

        public List<sbyte[]> BreakTable { get; } = new List<sbyte[]>();

        private bool ParsedCheckCpd { get; }

        private List<PatEntry> CheckCpdTable { get; } = new List<PatEntry>();

        private int SimplifiedCpd { get; }

        private bool ParsedDefCpd { get; }

        private List<FlagEntry> DefCpdTable { get; } = new List<FlagEntry>();

        public PhoneTable Phone { get; }

        public int MaxNgramSugs { get; }

        public int MaxCpdSugs { get; }

        public int MaxDiff { get; }

        public int OnlyMaxDiff { get; }

        public int NoSplitSugs { get; }

        public int SugsWithDots { get; }

        private int CpdWordMax { get; }

        private int CpdMaxSyllable { get; }

        private sbyte[] CpdVowels { get; }

        private List<char> CpdVowelsUtf16 { get; }

        private sbyte[] CpdSyllableNum { get; }

        private sbyte[] PfxAppnd { get; }

        private sbyte[] SfxAppnd { get; }

        private int SfxExtra { get; }

        private Flag SfxFlag { get; }

        public sbyte[] Derived { get; }

        private SfxEntry Sfx { get; }

        private PfxEntry Pfx { get; }

        private int CheckNum { get; }

        [CLSCompliant(false)]
        public sbyte[] WordChars { get; }

        public List<char> WordCharsUtf16 { get; } = new List<char>();

        [CLSCompliant(false)]
        public sbyte[] IgnoredChars { get; }

        public List<char> IgnoredCharsUtf16 { get; } = new List<char>();

        [CLSCompliant(false)]
        public sbyte[] Version { get; }

        private sbyte[] Lang { get; }

        public int LangNum { get; }

        private Flag LemmaPresent { get; }

        private Flag Circumfix { get; }

        [CLSCompliant(false)]
        public Flag OnlyInCompound { get; }

        [CLSCompliant(false)]
        public Flag KeepCase { get; }

        [CLSCompliant(false)]
        public Flag ForceUCase { get; }

        [CLSCompliant(false)]
        public Flag Warn { get; }

        public int ForbidWarn { get; }

        private Flag SubStandard { get; }

        public int CheckSharps { get; }

        public int FullStrip { get; }

        public int HaveContClass { get; }

        private sbyte[] ContClasses { get; } = new sbyte[ATypes.ContSize];

        public int Compound
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AffixMgr(sbyte[] affPath, List<HashMgr> ptr, sbyte[] key = null)
        {
            throw new NotImplementedException();
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

        [CLSCompliant(false)]
        public int IsSubset(sbyte[] s1, sbyte[] s2)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
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
        public int CompoundCheckMorph(sbyte[] word, int len, short wordNum, short numSyllable, short maxWordNum, short wNum, HEntry[] words, HEntry[] rWords, ubyte huMovRule, ref sbyte[] result, sbyte[] partResult)
        {
            // NOTE: may require pointers or an array slice
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public List<sbyte[]> GetSuffixWords(ushort[] suff, int len, sbute[] rootWord)
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

        private int ParseFile(sbyte[] affPath, sbyte[] key)
        {
            throw new NotImplementedException();
        }

        private bool ParseFlag(sbyte[] line, ushort[] @out, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseNum(sbyte[] line, out int @out, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseCpdSyllable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseRepTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
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

        private bool ParseDefCpdTable(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private bool ParseAffix(sbyte[] line, sbyte at, FileMgr af, sbyte[] dupFlags)
        {
            throw new NotImplementedException();
        }

        private void ReverseCondition(sbyte[] _)
        {
            throw new NotImplementedException();
        }

        private sbyte[] DebugFlag(sbyte[] result, ushort flag)
        {
            throw new NotImplementedException();
        }

        private int CondLen(sbyte[] _)
        {
            throw new NotImplementedException();
        }

        private int EncodeIt(AffEntry entry, sbyte[] cs)
        {
            throw new NotImplementedException();
        }

        private int BuildPfxTree(PfxEntry pfx)
        {
            throw new NotImplementedException();
        }

        private int BuildSfxTree(SfxEntry sfx)
        {
            throw new NotImplementedException();
        }

        private int ProcessPfxOrder()
        {
            throw new NotImplementedException();
        }

        private int ProcessSfxOrder()
        {
            throw new NotImplementedException();
        }

        private PfxEntry ProcessPfxInOrder(PfxEntry ptr, PfxEntry nPtr)
        {
            throw new NotImplementedException();
        }

        private PfxEntry ProcessSfxInOrder(SfxEntry ptr, SfxEntry nPtr)
        {
            throw new NotImplementedException();
        }

        private int ProcessPfxTreeToList()
        {
            throw new NotImplementedException();
        }

        private int ProcessSfxTreeToList()
        {
            throw new NotImplementedException();
        }

        private int RedundantCondition(sbyte _1, sbyte[] strip, int strIpl, sbyte[] cond, int _2)
        {
            throw new NotImplementedException();
        }

        private void FinishFileMgr(FileMgr affLst)
        {
            throw new NotImplementedException();
        }
    }
}
