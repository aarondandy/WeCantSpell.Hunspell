using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunspell
{
    public enum ReplacementEntryType : byte
    {
        /// <summary>
        /// Indicates that text can contain the pattern.
        /// </summary>
        Med = 0,
        /// <summary>
        /// Indicates that text can start with the pattern.
        /// </summary>
        Ini = 1,
        /// <summary>
        /// Indicates that text can end with the pattern.
        /// </summary>
        Fin = 2,
        /// <summary>
        /// Indicates that text must match the pattern exactly.
        /// </summary>
        Isol = 3
    }
}
