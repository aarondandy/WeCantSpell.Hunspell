using System;
using System.Buffers;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class StringEx
    {
        private static readonly char[] SpaceOrTab = { ' ', '\t' };

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Length != 0 && @this[0] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWith(this string @this, char character)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Length != 0 && @this[@this.Length - 1] == character;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string[] SplitOnTabOrSpace(this string @this)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            return @this.Split(SpaceOrTab, StringSplitOptions.RemoveEmptyEntries);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsTabOrSpace(this char c) => c == ' ' || c == '\t';

        public static string GetReversed(this string @this)
        {
            if (@this == null || @this.Length <= 1)
            {
                return @this;
            }

            using (var mo = MemoryPool<char>.Shared.Rent(@this.Length))
            {
                var buffer = mo.Memory.Span.Slice(0, @this.Length);
                var lastIndex = @this.Length - 1;
                for (var i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = @this[lastIndex - i];
                }
                
                return buffer.ToString();
            }
        }

        public static bool Contains(this string @this, char value) => @this.IndexOf(value) >= 0;

        public static string Replace(this string @this, int index, int removeCount, string replacement)
        {
            var builder = StringBuilderPool.Get(@this, Math.Max(@this.Length, @this.Length + replacement.Length - removeCount));
            builder.Replace(index, removeCount, replacement);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this string @this, int index)
        {
#if DEBUG
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            return index < @this.Length ? @this[index] : '\0';
        }

        public static string ConcatString(string str0, int startIndex0, int count0, string str1) =>
            str0.AsSpan(startIndex0, count0).ConcatString(str1);

        public static string ConcatString(string str0, string str1, int startIndex1, int count1) =>
            str0.ConcatString(str1.AsSpan(startIndex1, count1));

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, char char2, string str3, int startIndex3)
        {
            var count3 = str3.Length - startIndex3;
            var builder = StringBuilderPool.Get(count0 + str1.Length + 1 + count3);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            builder.Append(char2);
            builder.Append(str3, startIndex3, count3);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, int startIndex0, int count0, string str1, string str2, int startIndex2)
        {
            var count2 = str2.Length - startIndex2;
            var builder = StringBuilderPool.Get(count0 + str1.Length + count2);
            builder.Append(str0, startIndex0, count0);
            builder.Append(str1);
            builder.Append(str2, startIndex2, count2);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

        public static string ConcatString(string str0, int startIndex0, int count0, char char1, string str2, int startIndex2) =>
            ConcatString(str0, startIndex0, count0, char1.ToString(), str2, startIndex2);

        public static string ConcatString(this string @this, ReadOnlySpan<char> value)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
#endif
            if (@this.Length == 0)
            {
                return value.ToString();
            }
            if (value.IsEmpty)
            {
                return @this;
            }

            var builder = StringBuilderPool.Get(@this.Length + value.Length);
            builder.Append(@this);
            builder.Append(value);
            return StringBuilderPool.GetStringAndReturn(builder);
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsLineBreakChar(this char c) => c == '\n' || c == '\r';

        public static string WithoutIndex(this string @this, int index)
        {
#if DEBUG
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index >= @this.Length) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            if (index == 0)
            {
                return @this.Substring(1);
            }
            var lastIndex = @this.Length - 1;
            if (index == lastIndex)
            {
                return @this.Substring(0, lastIndex);
            }

            var builder = StringBuilderPool.Get(lastIndex);
            builder.Append(@this, 0, index);
            builder.Append(@this, index + 1, lastIndex - index);
            return StringBuilderPool.GetStringAndReturn(builder);
        }
    }
}
