using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hunspell.Utilities
{
    internal static class StringExtensions
    {
        private static readonly char[] EndOfLineCharacters = new[] { '\r', '\n' };
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private static readonly Regex NotSpaceOrTabRegex = new Regex(@"[^ \t]+");

        public static string TrimEndOfLine(this string @this)
        {
            return @this.TrimEnd(EndOfLineCharacters);
        }

        public static string TrimStartTabOrSpace(this string @this)
        {
            return @this.TrimStart(SpaceOrTab);
        }

        public static bool StartsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[0] == character;
        }

        public static bool EndsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

        public static string[] SplitOnTabOrSpace(this string @this)
        {
            return @this.Split(SpaceOrTab);
        }

        public static MatchCollection RegexSplitOnTabOrSpace(this string @this)
        {
            return NotSpaceOrTabRegex.Matches(@this);
        }

        public static string SubstringFromEnd(this string @this, int startFromEnd)
        {
            return @this.Substring(0, @this.Length - startFromEnd);
        }

        public static string Reverse(this string @this)
        {
            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
