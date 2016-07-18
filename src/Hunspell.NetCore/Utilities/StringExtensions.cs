using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hunspell.Utilities
{
    internal static class StringExtensions
    {
        private static readonly char[] EndOfLineCharacters = new[] { '\r', '\n' };
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private static readonly char[] CommaArray = new[] { ',' };
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
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitOnComma(this string @this)
        {
            return @this.Split(CommaArray);
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
            if (@this == null || @this.Length <= 1)
            {
                return @this;
            }

            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string RemoveChars(this string @this, string remove)
        {
            if (string.IsNullOrEmpty(@this) || string.IsNullOrEmpty(remove))
            {
                return @this;
            }

            var removeLookup = new HashSet<char>(remove);
            return new string(
                Array.FindAll(
                    @this.ToCharArray(),
                    c => !removeLookup.Contains(c)));
        }

        public static string RemoveChars(this string @this, char[] remove)
        {
            if (string.IsNullOrEmpty(@this) || remove == null || remove.Length == 0)
            {
                return @this;
            }

            var removeLookup = new HashSet<char>(remove);
            var chars = @this.ToCharArray();
            return new string(
                Array.FindAll(
                    @this.ToCharArray(),
                    c => !removeLookup.Contains(c)));
        }

        public static bool Contains(this string @this, char c)
        {
            return @this.IndexOf(c) >= 0;
        }

        public static bool ContainsOrdinalIgnoreCase(this string @this, string value)
        {
            return @this.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
