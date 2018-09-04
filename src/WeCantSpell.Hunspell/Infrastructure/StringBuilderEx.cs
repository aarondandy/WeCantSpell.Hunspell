using System;
using System.Runtime.InteropServices;
using System.Text;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class StringBuilderEx
    {

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Swap(this StringBuilder @this, int indexA, int indexB)
        {
#if DEBUG
            if (indexA < 0 || indexA > @this.Length) throw new ArgumentOutOfRangeException(nameof(indexA));
            if (indexB < 0 || indexB > @this.Length) throw new ArgumentOutOfRangeException(nameof(indexB));
#endif

            var temp = @this[indexA];
            @this[indexA] = @this[indexB];
            @this[indexB] = temp;
        }

        public static string ToStringTerminated(this StringBuilder @this)
        {
            var terminatedIndex = @this.IndexOfNullChar();
            return terminatedIndex >= 0
                ? @this.ToString(0, terminatedIndex)
                : @this.ToString();
        }

        public static string ToStringTerminated(this StringBuilder @this, int startIndex)
        {
            var terminatedIndex = @this.IndexOfNullChar(startIndex);
            if (terminatedIndex < 0)
            {
                terminatedIndex = @this.Length;
            }

            return @this.ToString(startIndex, terminatedIndex - startIndex);
        }

        public static int IndexOfNullChar(this StringBuilder @this)
        {
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == '\0')
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOfNullChar(this StringBuilder @this, int offset)
        {
            for (; offset < @this.Length; offset++)
            {
                if (@this[offset] == '\0')
                {
                    return offset;
                }
            }

            return -1;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this StringBuilder @this, int index) =>
            index < @this.Length ? @this[index] : '\0';

        public static void RemoveChars(this StringBuilder @this, CharacterSet chars)
        {
            var nextWriteLocation = 0;
            for (var searchLocation = 0; searchLocation < @this.Length; searchLocation++)
            {
                var c = @this[searchLocation];
                if (!chars.Contains(c))
                {
                    @this[nextWriteLocation] = c;
                    nextWriteLocation++;
                }
            }

            @this.Remove(nextWriteLocation, @this.Length - nextWriteLocation);
        }

        public static void Reverse(this StringBuilder @this)
        {
            if (@this == null || @this.Length <= 1)
            {
                return;
            }

            var swapOtherIndexOffset = @this.Length - 1;
            var stopIndex = @this.Length / 2;
            for (var i = 0; i < stopIndex; i++)
            {
                @this.Swap(i, swapOtherIndexOffset - i);
            }
        }

        public static void Replace(this StringBuilder @this, int index, int removeCount, string replacement)
        {
            if (replacement.Length <= removeCount)
            {
                for (var i = 0; i < replacement.Length; i++)
                {
                    @this[index + i] = replacement[i];
                }

                if (replacement.Length != removeCount)
                {
                    @this.Remove(index + replacement.Length, removeCount - replacement.Length);
                }
            }
            else
            {
                @this.Remove(index, removeCount);
                @this.Insert(index, replacement);
            }
        }

        public static bool StartsWith(this StringBuilder builder, char c) =>
            builder.Length != 0 && builder[0] == c;

        public static bool EndsWith(this StringBuilder builder, char c) =>
            builder.Length != 0 && builder[builder.Length - 1] == c;

        public static string ToStringWithInsert(this StringBuilder builder, int index, char value)
        {
#if DEBUG
            if (index < 0 || index > builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
#endif
            if (index == 0)
            {
                return value.ToString() + builder.ToString();
            }

            if (index == builder.Length)
            {
                return builder.ToString() + value.ToString();
            }

            return builder.ToString(0, index)
                + value.ToString()
                + builder.ToString(index, builder.Length - index);
        }

        public static StringBuilder Append(this StringBuilder builder, ReadOnlySpan<char> value)
        {
#if DEBUG
            if (builder == null) throw new ArgumentNullException(nameof(builder));
#endif

            if (!value.IsEmpty)
            {
#if !NO_SB_POINTERS
                unsafe
                {
                    fixed (char* start = &MemoryMarshal.GetReference(value))
                    {
                        builder.Append(start, value.Length);
                    }
                }
#else
                for (var i = 0; i < value.Length; i++)
                {
                    builder.Append(value[i]);
                }
#endif
            }

            return builder;
        }
    }
}
