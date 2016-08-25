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

        [Obsolete]
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

        public static string RemoveChars(this string @this, ImmutableSortedSet<char> remove)
        {
            if (string.IsNullOrEmpty(@this) || remove == null || remove.IsEmpty)
            {
                return @this;
            }

            var builder = new StringBuilder(@this.Length);

            for (var i = 0; i < @this.Length; i++)
            {
                var c = @this[i];
                if (!remove.Contains(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }

        public static bool Contains(this string @this, char c)
        {
            return @this.IndexOf(c) >= 0;
        }

        public static string ReplaceToEnd(this string @this, int index, string replacement)
        {
            if (string.IsNullOrEmpty(replacement))
            {
                return @this.Substring(0, index);
            }

            var builder = new StringBuilder(@this, index + replacement.Length);

            builder.Remove(index, builder.Length - index);
            builder.Append(replacement);

            return builder.ToString();
        }

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = new StringBuilder(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));

            builder.Remove(index, removeCount);
            builder.Insert(index, replacement);

            return builder.ToString();
        }

        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length)
        {
            if (ReferenceEquals(a, b) && aOffset == bOffset)
            {
                return true;
            }

            return string.CompareOrdinal(a, aOffset, b, bOffset, length) == 0;
        }

        public static Encoding GetEncodingByName(string encodingName)
        {
            if (string.IsNullOrEmpty(encodingName))
            {
                return null;
            }

            try
            {
                return Encoding.GetEncoding(encodingName);
            }
            catch (ArgumentException)
            {
                if (encodingName.Length >= 4 && encodingName.StartsWith("ISO") && encodingName[3] != '-')
                {
                    return GetEncodingByName(encodingName.Insert(3, "-"));
                }

                return null;
            }
        }

        public static string AsTerminatedString(this char[] chars)
        {
            var zeroIndex = Array.IndexOf(chars, default(char));
            return zeroIndex < 0 ? new string(chars) : new string(chars, 0, zeroIndex);
        }
    }
}
