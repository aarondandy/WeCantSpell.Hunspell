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

namespace Hunspell
{
    public class HashMgr
    {
        public HashMgr(sbyte[] tPath, sbyte[] aPath, sbyte[] key = null)
        {
            throw new NotImplementedException();
        }

        private int TableSize { get; }

        private HEntry[] Table { get; }

        private Flag Mode { get; }

        private int ComplexPrefixes { get; }

        private int Utf8 { get; }

        private ushort ForbiddenWord { get; }

        private int LangNum { get; }
        private sbyte[] Enc { get; }

        private sbyte[] Lang { get; }

        private CsInfo CsConv { get; }

        private sbyte[] IgnoreChars { get; }

        private List<char> IgnoreCharsUtf16 { get; }

        /// <summary>
        /// Flag vector `compression' with aliases.
        /// </summary>
        private int NumAliasF { get; }

        private ushort[][] AliasF { get; }

        private ushort[] AliasFLen { get; }

        /// <summary>
        /// Morphological desciption `compression' with aliases.
        /// </summary>
        private int NumAliasM { get; }

        public sbyte[][] AliasM { get; private set; }

        public HEntry Lookup(sbyte[] _)
        {
            throw new NotImplementedException();
        }

        public int Hash(sbyte[] _)
        {
            throw new NotImplementedException();
        }

        public HEntry WalkHashTable(ref int col, HEntry hp)
        {
            throw new NotImplementedException();
        }

        public int Add(sbyte[] word)
        {
            throw new NotImplementedException();
        }

        public int AddWithAffix(sbyte[] word, sbyte[] patterns)
        {
            throw new NotImplementedException();
        }

        public int Remove(sbyte[] word)
        {
            throw new NotImplementedException();
        }

        public int DecodeFlags(ushort[][] result, sbyte[] flags, FileMgr af)
        {
            throw new NotImplementedException();
        }

        public bool DecodeFlags(List<ushort> result, sbyte[] flags, FileMgr af)
        {
            throw new NotImplementedException();
        }

        public ushort DecodeFlag(sbyte[] flag)
        {
            throw new NotImplementedException();
        }

        public sbyte[] EncodeFlags(ushort flag)
        {
            throw new NotImplementedException();
        }

        public int IsAliasF()
        {
            throw new NotImplementedException();
        }

        public int GetAliasF(int index, ushort[][] fVec, FileMgr af)
        {
            throw new NotImplementedException();
        }

        public int IsAliasM()
        {
            throw new NotImplementedException();
        }

        public sbyte[] GetAliasM(int index)
        {
            throw new NotImplementedException();
        }

        private int GetCleanAndCapType(sbyte[] word, ref int capType)
        {
            throw new NotImplementedException();
        }

        private int LoadTables(sbyte[] tPath, sbyte[] key)
        {
            throw new NotImplementedException();
        }

        private int AddWord(sbyte[] word, int wcl, ushort[] ap, int al, sbyte[] desc, bool onlyUpCase)
        {
            throw new NotImplementedException();
        }

        private int LoadConfig(sbyte[] affPath, sbyte[] key)
        {
            throw new NotImplementedException();
        }

        private bool ParseAliasF(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private int AddHiddenCapitalizedWord(sbyte[] word, int wcl, ushort[] flags, int al, sbyte[] dp, int capType)
        {
            throw new NotImplementedException();
        }

        private bool ParseAliasM(sbyte[] line, FileMgr af)
        {
            throw new NotImplementedException();
        }

        private int RemoveForbiddenFlag(sbyte[] word)
        {
            throw new NotImplementedException();
        }

        public enum Flag : int
        {
            Char,
            Long,
            Num,
            Uni
        }
    }
}
