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
    public class Hunspell
    {
        /// <summary>
        /// Construct an instance for the given affix and dictionaly files.
        /// </summary>
        /// <param name="affPath">Affix file path.</param>
        /// <param name="dPath">Dictionary file path.</param>
        public Hunspell(string affPath, string dPath, string key = null)
        {
            throw new NotImplementedException();
        }

        private HunspellImpl Impl { get; }

        public string DictEncoding { get; }

        public string Version { get; }

        public int LangNum { get; }

        /// <summary>
        /// Load extra dictionaryies.
        /// </summary>
        /// <param name="dPath">Dictionary file path.</param>
        public int AddDic(string dPath, string key = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Spell-check a given word.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <param name="info">Information bit array fields.</param>
        /// <param name="root">A root (stem), when input is a word with affix(es).</param>
        /// <returns>When spelled correctly return <c>true</c>, otherwise <c>false</c>.</returns>
        /// <remarks>
        /// SpellCompound and SpellForbidden are valid flags that can be provided using <paramref name="info"/>.
        /// </remarks>
        public bool Spell(string word, int[] info = null, string[] root = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Suggest other words.
        /// </summary>
        /// <param name="word">The word to seek suggestions for.</param>
        /// <returns>A list of suggestions.</returns>
        public List<string> Suggest(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Suggests other words from suffix rules.
        /// </summary>
        /// <param name="rootWord">The root word to seek suggestions for.</param>
        /// <returns>A list of suggestions.</returns>
        public List<string> SuffixSuggest(string rootWord)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Perform morphological analysis of the given <paramref name="word"/>.
        /// </summary>
        /// <param name="word">The word to perform morphological analysis on.</param>
        public List<string> Analyze(string word)
        {
            throw new NotImplementedException();
        }

        public List<string> Stem(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get stems from a porphological analysis.
        /// </summary>
        public List<string> Stem(List<string> morph)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs morphological generation by example(s).
        /// </summary>
        public List<string> Generate(string word, string word2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs generation by morphological description(s).
        /// </summary>
        public List<string> Generate(string word, List<string> pl)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a word to the run-time dictionary.
        /// </summary>
        /// <param name="word">The word to be added.</param>
        public int Add(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a word to the run-time dictionary with affix flags of the example (a dictionary word).
        /// </summary>
        /// <param name="word">The word to be added.</param>
        /// <param name="example">The example to use for affix flags.</param>
        /// <remarks>
        /// Hunspell will recognize affixed forms of the new word, too.
        /// </remarks>
        public int AddWithAffix(string word, string example)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a word from the run-time dictionary.
        /// </summary>
        /// <param name="word">The word to be added.</param>
        public int Remove(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get extra word characters definied in affix file for tokenization.
        /// </summary>
        public string GetWordChars()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get extra word characters definied in affix file for tokenization.
        /// </summary>
        public string GetWordCharsUtf16()
        {
            throw new NotImplementedException();
        }

        public bool InputConv(string word, string dest)
        {
            throw new NotImplementedException();
        }
    }

    public class HunspellImpl
    {
        public HunspellImpl(string affPath, string dPath, string key)
        {
            throw new NotImplementedException();
        }

        public int LangNum { get; }

        public sbyte[] WordChars { get; }

        public string WordCharsUtf16 { get; }

        public string DictEncoding { get; }

        public string Version { get; }

        private AffixMgr AMgr { get; }

        private List<HashMgr> HMgrs { get; }

        private SuggestMgr SMgr { get; }

        private string AffixPath { get; }

        private string Encoding { get; }

        private CsInfo CsConv { get; }

        private int Utf8 { get; }

        private List<string> WordBreak { get; }

        public int AddDic(string dPath, string key)
        {
            throw new NotImplementedException();
        }

        public List<string> SuffixSuggest(string roowWord)
        {
            throw new NotImplementedException();
        }

        public List<string> Generate(string word, List<string> pl)
        {
            throw new NotImplementedException();
        }

        public List<string> Generate(string word, string pattern)
        {
            throw new NotImplementedException();
        }

        public List<string> Stem(string word)
        {
            throw new NotImplementedException();
        }

        public List<string> Stem(List<string> morph)
        {
            throw new NotImplementedException();
        }

        public List<string> Analyze(string word)
        {
            throw new NotImplementedException();
        }

        public bool InputConv(string word, string dest)
        {
            throw new NotImplementedException();
        }

        public bool Spell(string word, int[] info = null, string root = null)
        {
            throw new NotImplementedException();
        }

        public List<string> Suggest(string word)
        {
            throw new NotImplementedException();
        }

        public int Add(string word)
        {
            throw new NotImplementedException();
        }

        public int AddWithAffix(string word, string example)
        {
            throw new NotImplementedException();
        }

        public int Remove(string word)
        {
            throw new NotImplementedException();
        }

        private void CleanWord(string dest, string _, ref int pcapType, ref int pAbbrev)
        {
            throw new NotImplementedException();
        }

        private long CleanWord(string dest, string destU, string src, ref int pcapType, ref long pAbbrev)
        {
            throw new NotImplementedException();
        }

        private void MkInitCap(string u8)
        {
            throw new NotImplementedException();
        }

        private int MkInitCap2(string u8, string u16)
        {
            throw new NotImplementedException();
        }

        private int MkInitSmall2(string u8, string u16)
        {
            throw new NotImplementedException();
        }

        private void MkAllCap(string u8)
        {
            throw new NotImplementedException();
        }

        private int MkAllSmall2(string u8, string u16)
        {
            throw new NotImplementedException();
        }

        private HEntry CheckWord(string source, ref int info, string root)
        {
            throw new NotImplementedException();
        }

        private string SharpsU8L1(string source)
        {
            throw new NotImplementedException();
        }

        private HEntry SpellSharps(string @base, long startPos, int _1, int _2, ref int info, string root)
        {
            throw new NotImplementedException();
        }

        private int IsKeepCase(HEntry rv)
        {
            throw new NotImplementedException();
        }

        private void InsertSug(List<string> slst, string word)
        {
            throw new NotImplementedException();
        }

        private void CatResult(string result, string st)
        {
            throw new NotImplementedException();
        }

        private List<string> SpellMl(string word)
        {
            throw new NotImplementedException();
        }

        private string GetXmlPar(string par)
        {
            throw new NotImplementedException();
        }

        private string GetXmlPos(string s, string attr)
        {
            throw new NotImplementedException();
        }

        private List<string> GetXmlList(string list, string tag)
        {
            throw new NotImplementedException();
        }

        private int CheckXmlPar(string q, string attr, string value)
        {
            throw new NotImplementedException();
        }
    }
}
