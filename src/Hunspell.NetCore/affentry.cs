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

using Hunspell.Utilities;
using System;

using Flag = System.UInt16;

namespace Hunspell
{
    /// <summary>
    /// A prefix entry.
    /// </summary>
    [Obsolete("Use PrefixEntry")]
    public class PfxEntry : AffEntry
    {
        public PfxEntry(AffixMgr pmgr)
        {
            PmyMgr = pmgr;
        }

        private AffixMgr PmyMgr { get; set; }

        public PfxEntry Next { get; set; }

        public PfxEntry NextEq { get; set; }

        public PfxEntry NextNe { get; set; }

        public PfxEntry FlgNxt { get; set; }

        public bool AllowCross
        {
            get
            {
                return (Opts & ATypes.AeXProduct) != 0;
            }
        }

        public Flag Flag
        {
            get
            {
                return checked((Flag)AFlag);
            }
        }

        public string Key
        {
            get
            {
                return Appnd;
            }
        }

        public short KeyLen
        {
            get
            {
                return checked((short)Key.Length);
            }
        }

        public ushort[] Cont
        {
            get
            {
                return ContClass;
            }
        }

        public string Morph
        {
            get
            {
                return MorphCode;
            }
        }

        public short ContLen
        {
            get
            {
                return ContClassLen;
            }
        }

        [CLSCompliant(false)]
        public HEntry CheckWord(sbyte[] word, int len, sbyte in_compound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry CheckTwoSfx(sbyte[] word, int len, sbyte in_compound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] CheckMorph(sbyte[] word, int len, sbyte in_compound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] CheckTwoSfxMorph(sbyte[] word, int len, sbyte in_compound, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] Add(sbyte[] word, uint len)
        {
            throw new NotImplementedException();
        }

        public sbyte[] NextChar(sbyte[] p)
        {
            throw new NotImplementedException();
        }

        public int TestCondition(sbyte[] st)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A suffix entry.
    /// </summary>
    [Obsolete("Use SuffixEntry")]
    public class SfxEntry : AffEntry
    {
        public SfxEntry(AffixMgr pmgr)
        {
            PmyMgr = pmgr;
        }

        private AffixMgr PmyMgr { get; set; }

        private string RAppnd { get; set; }

        public SfxEntry Next { get; set; }

        public SfxEntry NextEq { get; set; }

        public SfxEntry NextNe { get; set; }

        public SfxEntry FlgNxt { get; set; }

        public SfxEntry LMorph { get; }

        public SfxEntry RMorph { get; }

        public SfxEntry EqMorph { get; }

        bool AllowCross
        {
            get
            {
                return (Opts & ATypes.AeXProduct) != 0;
            }
        }

        public Flag Flag
        {
            get
            {
                return checked((Flag)AFlag);
            }
        }

        public string Key
        {
            get
            {
                return RAppnd;
            }
        }

        public short KeyLen
        {
            get
            {
                return checked((short)Appnd.Length);
            }
        }

        public string Morph
        {
            get
            {
                return MorphCode;
            }
        }

        [CLSCompliant(false)]
        public ushort[] Cont
        {
            get
            {
                return ContClass;
            }
        }

        public short ContLen
        {
            get
            {
                return ContClassLen;
            }
        }

        public string Affix
        {
            get
            {
                return Appnd;
            }
        }



        [CLSCompliant(false)]
        public HEntry CheckWord(sbyte[] word, int len, int optFlags, PfxEntry ppfx, Flag cclass, Flag needFlag, Flag badFlag)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public HEntry CheckTwoSfx(sbyte[] word, int len, int optFlags, PfxEntry ppfx, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] CheckTwoSfxMorph(sbyte[] word, int len, int optFlags, PfxEntry ppfx, Flag needFlag = default(Flag))
        {
            throw new NotImplementedException();
        }

        HEntry GetNextHomonym(HEntry he)
        {
            throw new NotImplementedException();
        }

        HEntry GetNextHomonym(HEntry word, int optFlags, PfxEntry ppfx, Flag cclass, Flag needFlag)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public sbyte[] Add(sbyte[] word, uint len)
        {
            throw new NotImplementedException();
        }

        public void InitReverseWord()
        {
            RAppnd = Appnd;
            RAppnd = RAppnd.Reverse();
        }

        [CLSCompliant(false)]
        public sbyte[] NextChar(sbyte[] p)
        {
            throw new NotImplementedException();
        }

        [CLSCompliant(false)]
        public int TestCondition(sbyte[] st, sbyte[] begin)
        {
            throw new NotImplementedException();
        }
    }
}

/*
Appendix:  Understanding Affix Code


An affix is either a  prefix or a suffix attached to root words to make 
other words.

Basically a Prefix or a Suffix is set of AffEntry objects
which store information about the prefix or suffix along 
with supporting routines to check if a word has a particular 
prefix or suffix or a combination.

The structure affentry is defined as follows:

struct affentry
{
   unsigned short aflag;    // ID used to represent the affix
   std::string strip;       // string to strip before adding affix
   std::string appnd;       // the affix string to add
   char numconds;           // the number of conditions that must be met
   char opts;               // flag: aeXPRODUCT- combine both prefix and suffix 
   char   conds[SETSIZE];   // array which encodes the conditions to be met
};


Here is a suffix borrowed from the en_US.aff file.  This file 
is whitespace delimited.

SFX D Y 4 
SFX D   0     e          d
SFX D   y     ied        [^aeiou]y
SFX D   0     ed         [^ey]
SFX D   0     ed         [aeiou]y

This information can be interpreted as follows:

In the first line has 4 fields

Field
-----
1     SFX - indicates this is a suffix
2     D   - is the name of the character flag which represents this suffix
3     Y   - indicates it can be combined with prefixes (cross product)
4     4   - indicates that sequence of 4 affentry structures are needed to
               properly store the affix information

The remaining lines describe the unique information for the 4 SfxEntry 
objects that make up this affix.  Each line can be interpreted
as follows: (note fields 1 and 2 are as a check against line 1 info)

Field
-----
1     SFX         - indicates this is a suffix
2     D           - is the name of the character flag for this affix
3     y           - the string of chars to strip off before adding affix
                         (a 0 here indicates the NULL string)
4     ied         - the string of affix characters to add
5     [^aeiou]y   - the conditions which must be met before the affix
                    can be applied

Field 5 is interesting.  Since this is a suffix, field 5 tells us that
there are 2 conditions that must be met.  The first condition is that 
the next to the last character in the word must *NOT* be any of the 
following "a", "e", "i", "o" or "u".  The second condition is that
the last character of the word must end in "y".

So how can we encode this information concisely and be able to 
test for both conditions in a fast manner?  The answer is found
but studying the wonderful ispell code of Geoff Kuenning, et.al. 
(now available under a normal BSD license).

If we set up a conds array of 256 bytes indexed (0 to 255) and access it
using a character (cast to an unsigned char) of a string, we have 8 bits
of information we can store about that character.  Specifically we
could use each bit to say if that character is allowed in any of the 
last (or first for prefixes) 8 characters of the word.

Basically, each character at one end of the word (up to the number 
of conditions) is used to index into the conds array and the resulting 
value found there says whether the that character is valid for a 
specific character position in the word.  

For prefixes, it does this by setting bit 0 if that char is valid 
in the first position, bit 1 if valid in the second position, and so on. 

If a bit is not set, then that char is not valid for that postion in the
word.

If working with suffixes bit 0 is used for the character closest 
to the front, bit 1 for the next character towards the end, ..., 
with bit numconds-1 representing the last char at the end of the string. 

Note: since entries in the conds[] are 8 bits, only 8 conditions 
(read that only 8 character positions) can be examined at one
end of a word (the beginning for prefixes and the end for suffixes.

So to make this clearer, lets encode the conds array values for the 
first two affentries for the suffix D described earlier.


  For the first affentry:    
     numconds = 1             (only examine the last character)

     conds['e'] =  (1 << 0)   (the word must end in an E)
     all others are all 0

  For the second affentry:
     numconds = 2             (only examine the last two characters)     

     conds[X] = conds[X] | (1 << 0)     (aeiou are not allowed)
         where X is all characters *but* a, e, i, o, or u
         

     conds['y'] = (1 << 1)     (the last char must be a y)
     all other bits for all other entries in the conds array are zero

*/
