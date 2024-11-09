﻿using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
    public static int IndexOf(this ReadOnlySpan<char> @this, char value, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static int IndexOf(this ReadOnlySpan<char> @this, ReadOnlySpan<char> value, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static int IndexOf(this Span<char> @this, ReadOnlySpan<char> value, int startIndex)
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static ReadOnlySpan<char> Limit(this ReadOnlySpan<char> @this, int maxLength) =>
        @this.Length > maxLength ? @this.Slice(0, maxLength) : @this;

    public static void Swap<T>(ref T value0, ref T value1)
    {
        (value1, value0) = (value0, value1);
    }

    public static void Swap(this Span<char> span, int index0, int index1)
    {
        (span[index1], span[index0]) = (span[index0], span[index1]);
    }

    public static void CopyToReversed(this ReadOnlySpan<char> source, Span<char> target)
    {
#if DEBUG
        if (source.Length > target.Length) throw new ArgumentOutOfRangeException(nameof(target));
#endif

        for (var index = 0; index < source.Length; index++)
        {
            target[target.Length - index - 1] = source[index];
        }
    }

#if NO_SPAN_REPLACE

    public static void Replace(this Span<char> span, char oldValue, char newValue)
    {
        do
        {
            if (span.IsEmpty)
            {
                return;
            }

            var index = span.IndexOf(oldValue);
            if (index < 0)
            {
                return;
            }

            span[index] = newValue;
            span = span.Slice(index + 1);
        }
        while (true);
    }

#endif

#if NO_SPAN_SORT

    public static void Sort<T>(this Span<T> span) where T : IComparable<T>
    {
        // This should be called on small collections and I'm lazy, so it's bubblesort.

        while (span.Length >= 2)
        {
            var hasSwapped = false;

            for (var i = span.Length - 2; i >= 0; i--)
            {
                ref var value0 = ref span[i];
                ref var value1 = ref span[i + 1];
                if (value0.CompareTo(value1) > 0)
                {
                    Swap(ref value0, ref value1);
                    hasSwapped = true;
                }
            }

            if (!hasSwapped)
            {
                break;
            }

            span = span.Slice(1);
        }
    }

#endif

#if NO_SPAN_COMPARISON_SORT

    public static void Sort<T>(this Span<T> span, Comparison<T> comparer)
    {
        // This should be called on small collections and I'm lazy, so it's bubblesort.

        while (span.Length >= 2)
        {
            var hasSwapped = false;

            for (var i = span.Length - 2; i >= 0; i--)
            {
                ref var value0 = ref span[i];
                ref var value1 = ref span[i + 1];
                if (comparer(value0, value1) > 0)
                {
                    Swap(ref value0, ref value1);
                    hasSwapped = true;
                }
            }

            if (!hasSwapped)
            {
                break;
            }

            span = span.Slice(1);
        }
    }

#endif

    public static void RemoveAll<T>(ref Span<T> span, T value) where T : notnull, IEquatable<T>
    {
        var readIndex = span.IndexOf(value);
        if (readIndex < 0)
        {
            return;
        }

        var writeIndex = readIndex;

        for (; readIndex < span.Length; readIndex++)
        {
            if (!value.Equals(span[readIndex]))
            {
                if (readIndex != writeIndex)
                {
                    span[writeIndex] = span[readIndex];
                }

                writeIndex++;
            }
        }

        if (writeIndex < span.Length)
        {
            span = span.Slice(0, writeIndex);
        }
    }

    public static void RemoveAdjacentDuplicates<T>(ref Span<T> span) where T : notnull, IEquatable<T>
    {
        if (span.Length < 2)
        {
            return;
        }

        var writeIndex = 1;
        var readIndex = 1;

        for (; readIndex < span.Length; readIndex++)
        {
            if (!span[readIndex].Equals(span[writeIndex - 1]))
            {
                if (readIndex != writeIndex)
                {
                    span[writeIndex] = span[readIndex];
                }

                writeIndex++;
            }
        }

        if (writeIndex < span.Length)
        {
            span = span.Slice(0, writeIndex);
        }
    }
}
