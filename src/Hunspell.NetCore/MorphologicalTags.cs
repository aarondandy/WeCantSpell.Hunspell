using System;

namespace Hunspell
{
    public static class MorphologicalTags
    {
        public static readonly string Stem = "st:";
        public static readonly string AlloMorph = "al:";
        public static readonly string Pos = "po:";
        public static readonly string DeriPfx = "dp:";
        public static readonly string InflPfx = "ip:";
        public static readonly string TermPfx = "tp:";
        public static readonly string DeriSfx = "ds:";
        public static readonly string InflSfx = "is:";
        public static readonly string TermSfx = "ts:";
        public static readonly string SurfPfx = "sp:";
        public static readonly string Freq = "fr:";
        public static readonly string Phon = "ph:";
        public static readonly string Hyph = "hy:";
        public static readonly string Part = "pa:";
        public static readonly string Flag = "fl:";
        [Obsolete("I don't think this will be used.")]
        public static readonly string HashEntry = "_H:";
        public static readonly int TagLength = Stem.Length;
    }
}
