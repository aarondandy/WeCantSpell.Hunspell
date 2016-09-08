using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hunspell.Infrastructure
{
    internal static class StringEx
    {
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private static readonly char[] CommaArray = new[] { ',' };

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[0] == character;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character)
        {
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this)
        {
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnComma(this string @this)
        {
            return @this.Split(CommaArray);
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

        public static string RemoveChars(this string @this, ImmutableSortedSet<char> chars)
        {
            if (string.IsNullOrEmpty(@this) || chars == null || chars.IsEmpty)
            {
                return @this;
            }

            var builder = StringBuilderPool.Get(@this);
            builder.RemoveChars(chars);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool Contains(this string @this, char c)
        {
            return @this.IndexOf(c) >= 0;
        }

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = StringBuilderPool.Get(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
            builder.Replace(index, removeCount, replacement);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset)
        {
            if (ReferenceEquals(a, b) && aOffset == bOffset)
            {
                return true;
            }

            var aRemaining = (a?.Length).GetValueOrDefault() - aOffset;
            var bRemaining = (b?.Length).GetValueOrDefault() - bOffset;

            if (aRemaining != bRemaining)
            {
                return false;
            }

            if (aRemaining < 0)
            {
                return false;
            }

            return string.CompareOrdinal(a, aOffset, b, bOffset, aRemaining) == 0;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EqualsOffset(string a, int aOffset, string b, int bOffset, int length)
        {
            return (ReferenceEquals(a, b) && aOffset == bOffset && length >= 0)
                || string.CompareOrdinal(a, aOffset, b, bOffset, length) == 0;
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index)
        {
            return index < @this.Length ? @this[index] : '\0';
        }
    }
}
