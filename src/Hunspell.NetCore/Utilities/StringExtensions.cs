using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

namespace Hunspell.Utilities
{
    internal static class StringExtensions
    {
        private static readonly char[] EndOfLineCharacters = new[] { '\r', '\n' };
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private static readonly char[] CommaArray = new[] { ',' };
        private static readonly Regex NotSpaceOrTabRegex = new Regex(@"[^ \t]+");

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

        public static string RemoveChars(this string @this, ImmutableArray<char> remove)
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

        public static string SetChar(this string @this, char c, int index)
        {
            if (index == -1)
            {
                return c.ToString() + @this;
            }
            if (index >= 0 && index < @this.Length)
            {
                var builder = new StringBuilder(@this, @this.Length);
                builder[index] = c;
                return builder.ToString();
            }
            if (index == @this.Length)
            {
                return string.IsNullOrEmpty(@this) ? c.ToString() : @this + c.ToString();
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = new StringBuilder(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));

            // TODO: consider optimizing this
            builder.Remove(index, removeCount);
            builder.Insert(index, replacement);

            return builder.ToString();
        }
    }
}
