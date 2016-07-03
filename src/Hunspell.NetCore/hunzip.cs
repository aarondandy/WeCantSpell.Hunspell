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

/* hunzip: file decompression for sorted dictionaries with optional encryption,
 * algorithm: prefix-suffix encoding and 16-bit Huffman encoding */

/*
 *
 * This is a modified version of the Hunspell source code for the
 * purpose of creating an idiomatic port.
 *
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Hunspell
{
    public class Hunzip : IDisposable
    {
        internal const int BuffSize = 65536;

        public Hunzip(string filename, string key = null)
        {
            throw new NotImplementedException();
        }

        protected string Filename { get; }

        protected FileStream FIn { get; }

        protected int BufSiz { get; }

        protected int LasBit { get; }

        protected int Inc { get; }

        protected int InBits { get; }

        protected int OutC { get; }

        protected List<Bit> Dec { get; }

        protected sbyte[] In { get; } = new sbyte[Hunzip.BuffSize];

        protected sbyte[] Out { get; } = new sbyte[Hunzip.BuffSize + 1];

        protected sbyte[] Line { get; } = new sbyte[Hunzip.BuffSize + 50];

        public bool IsOpen()
        {
            return FIn?.CanRead ?? false;
        }

        public bool GetLine(string dest)
        {
            throw new NotImplementedException();
        }

        protected int GetCode(string key)
        {
            throw new NotImplementedException();
        }

        protected int GetBuf()
        {
            throw new NotImplementedException();
        }

        protected int Fail(string error, string par)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            FIn?.Dispose();
        }

        protected struct Bit
        {
            byte C0;
            byte C1;
            int V0;
            int V1;
        }
    }
}
