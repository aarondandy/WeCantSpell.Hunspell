using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hunspell.Infrastructure
{
    internal static class StringBuilderEx
    {
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Swap(this StringBuilder @this, int indexA, int indexB)
        {
#if DEBUG
            if (indexA < 0 || indexA > @this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(indexA));
            }
            if (indexB < 0 || indexB > @this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(indexB));
            }
#endif

            if (indexA != indexB)
            {
                var temp = @this[indexA];
                @this[indexA] = @this[indexB];
                @this[indexB] = temp;
            }
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
            for (var i = offset; i < @this.Length; i++)
            {
                if (@this[i] == '\0')
                {
                    return i;
                }
            }

            return -1;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static char GetCharOrTerminator(this StringBuilder @this, int index)
        {
            return index < @this.Length ? @this[index] : '\0';
        }

        public static void RemoveChars(this StringBuilder @this, char[] chars)
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

        public static void RemoveChars(this StringBuilder @this, ImmutableSortedSet<char> chars)
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

        public static void WriteChars(this StringBuilder @this, string text, int destinationIndex)
        {
            if (destinationIndex >= @this.Length)
            {
                if (destinationIndex > @this.Length)
                {
                    var characterGap = destinationIndex - @this.Length;
                    @this.Append('\0', characterGap);
                }

                @this.Append(text);
            }
            else
            {
                var writeUntilIndex = destinationIndex + text.Length;
                if (writeUntilIndex <= @this.Length)
                {
                    for (var i = 0; i < text.Length; i++)
                    {
                        @this[destinationIndex + i] = text[i];
                    }
                }
                else
                {
                    @this.Remove(destinationIndex, @text.Length - destinationIndex);
                    @this.Append(text);
                }
            }
        }

        public static void WriteChars(this StringBuilder @this, int sourceIndex, string text, int destinationIndex)
        {
            if (destinationIndex >= @this.Length)
            {
                if (destinationIndex > @this.Length)
                {
                    var characterGap = destinationIndex - @this.Length;
                    @this.Append('\0', characterGap);
                }

                @this.Append(text, sourceIndex, text.Length - sourceIndex);
            }
            else
            {
                var charactersToWrite = text.Length - sourceIndex;
                var writeUntilIndex = destinationIndex + charactersToWrite;
                if (writeUntilIndex <= @this.Length)
                {
                    for (var i = 0; i < charactersToWrite; i++)
                    {
                        @this[destinationIndex + i] = text[sourceIndex + i];
                    }
                }
                else
                {
                    @this.Remove(destinationIndex, @text.Length - destinationIndex);
                    @this.Append(text, sourceIndex, text.Length - sourceIndex);
                }
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string AsTerminatedString(this StringBuilder builder)
        {
            var nullTerminatorIndex = builder.IndexOfNullChar();
            return nullTerminatorIndex < 0 ? builder.ToString() : builder.ToString(0, nullTerminatorIndex);
        }
    }
}
