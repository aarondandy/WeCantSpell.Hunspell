using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace WeCantSpell.Hunspell.Infrastructure;

static class CollectionsEx
{
    public static bool Contains<T>(this T[] values, T value) => Array.IndexOf(values, value) >= 0;

#if NO_DICTIONARY_GETVALUE

    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var result) ? result : default;

    public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
        dictionary.TryGetValue(key, out var result) ? result : defaultValue;

#endif

    public static int RemoveDuplicates<T>(this List<T> list, IEqualityComparer<T> comparer)
    {
        if (list.Count < 2)
        {
            return 0;
        }

        if (list.Count == 2)
        {
            if (comparer.Equals(list[0], list[1]))
            {
                list.RemoveAt(1);
                return 1;
            }

            return 0;
        }

        return generalCase();

        int generalCase()
        {

#if NO_HASHSET_CAPACITY
            var set = new HashSet<T>(comparer);
#else
            var set = new HashSet<T>(list.Count, comparer);
#endif

            set.Add(list[0]);

            var writeIndex = 1;
            for (var readIndex = 1; readIndex < list.Count; readIndex++)
            {
                var value = list[readIndex];
                if (set.Add(value))
                {
                    if (readIndex != writeIndex)
                    {
                        list[writeIndex] = value;
                    }

                    writeIndex++;
                }
            }

            var duplicateCount = list.Count - writeIndex;

#if DEBUG
            if (duplicateCount < 0)
            {
                throw new InvalidOperationException();
            }
#endif

            if (duplicateCount > 0)
            {
                list.RemoveRange(writeIndex, duplicateCount);
            }

            return duplicateCount;
        }
    }

    public static ImmutableArray<T> ToImmutable<T>(this ImmutableArray<T>.Builder builder, bool allowDestructive) =>
        allowDestructive && builder.Capacity == builder.Count ? builder.MoveToImmutable() : builder.ToImmutable();

    public static int RemoveSortedDuplicates<T>(ref T[] values) where T : notnull, IEquatable<T>
    {
        var shiftSpan = values.AsSpan();

        var removed = 0;
        while (shiftSpan.Length > 1)
        {
            if (shiftSpan[0].Equals(shiftSpan[1]))
            {
                for (var shiftIndex = 1; shiftIndex < shiftSpan.Length; shiftIndex++)
                {
                    shiftSpan[shiftIndex - 1] = shiftSpan[shiftIndex];
                }

                removed++;
                shiftSpan = shiftSpan.Slice(0, shiftSpan.Length - 1);
            }
            else
            {
                shiftSpan = shiftSpan.Slice(1);
            }
        }

        if (removed > 0)
        {
            Array.Resize(ref values, values.Length - removed);
        }

        return removed;
    }

    public static bool Contains(this List<string> list, ReadOnlySpan<char> value)
    {
        foreach (var item in list)
        {
            if (item is not null && value.Equals(item.AsSpan(), StringComparison.Ordinal))
            {
                return true;
            }

        }
        return false;
    }

#if NO_KVP_DECONSTRUCT

    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
    {
        key = pair.Key;
        value = pair.Value;
    }

#endif

}
