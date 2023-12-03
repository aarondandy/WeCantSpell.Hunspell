using System;

namespace WeCantSpell.Hunspell.Infrastructure;

static class MemoryEx
{
#if NO_SPAN_CONTAINS

    public static bool Contains<T>(this ReadOnlySpan<T> @this, T value) where T : IEquatable<T> => @this.IndexOf(value) >= 0;

    public static bool Contains<T>(this Span<T> @this, T value) where T : IEquatable<T> => @this.IndexOf(value) >= 0;

#endif

    public static int IndexOf<T>(this ReadOnlySpan<T> @this, T value, int startIndex) where T : IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static int IndexOf<T>(this ReadOnlySpan<T> @this, ReadOnlySpan<T> value, int startIndex) where T : IEquatable<T>
    {
        var result = @this.Slice(startIndex).IndexOf(value);
        return result >= 0 ? result + startIndex : result;
    }

    public static ReadOnlySpan<T> Limit<T>(this ReadOnlySpan<T> @this, int maxLength)
    {
        return @this.Length > maxLength ? @this.Slice(0, maxLength) : @this;
    }

    public static void Swap<T>(ref T value0, ref T value1)
    {
        (value1, value0) = (value0, value1);
    }

    public static void Swap<T>(this Span<T> span, int index0, int index1)
    {
        (span[index1], span[index0]) = (span[index0], span[index1]);
    }

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

#if NO_SPAN_TRIM

    public static ReadOnlySpan<T> TrimStart<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        var start = 0;

        if (value is null)
        {
            for (; start < span.Length && span[start] is not null; start++) ;
        }
        else
        {
            for (; start < span.Length && !value.Equals(span[start]); start++) ;
        }

        return span.Slice(start);
    }

    public static Span<T> TrimStart<T>(this Span<T> span, T value) where T : IEquatable<T>?
    {
        var start = 0;

        if (value is null)
        {
            for (; start < span.Length && span[start] is null; start++) ;
        }
        else
        {
            for (; start < span.Length && value.Equals(span[start]); start++) ;
        }

        return span.Slice(start);
    }

#endif

    public static void RemoveAll<T>(ref Span<T> span, T value) where T : notnull, IEquatable<T>
    {
        var writeIndex = 0;
        var readIndex = 0;

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
