using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

static class CollectionsEx
{
    internal const int CollectionPreallocationLimit = 16384;

#if NO_NONENUMERATED_COUNT

    public static int GetNonEnumeratedCountOrDefault<T>(this IEnumerable<T> enumerable) => enumerable switch
    {
        ICollection<T> c => c.Count,
        ICollection c => c.Count,
        IReadOnlyCollection<T> c => c.Count,
        _ => 0
    };

#else

    public static int GetNonEnumeratedCountOrDefault<T>(this IEnumerable<T> enumerable) =>
        enumerable.TryGetNonEnumeratedCount(out var count) ? count : 0;

#endif

    public static void ReplaceLast<T>(this List<T> list, T item)
    {
        var index = list.Count - 1;
        if (index >= 0)
        {
            list[index] = item;
        }
        else
        {
            list.Add(item);
        }
    }

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
            if (duplicateCount > 0)
            {
                list.RemoveRange(writeIndex, duplicateCount);
            }

            return duplicateCount;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this List<T> list) => list.Count != 0;

#if NO_SPAN_CONTAINS

    public static bool Contains<T>(this T[] values, T value) where T : IEquatable<T> => Array.IndexOf(values, value) >= 0;

#else

    public static bool Contains<T>(this T[] values, T value) where T : IEquatable<T> => values.AsSpan().Contains(value);

#endif

#if NO_DICTIONARY_GETVALUE

    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var result) ? result : default;

#endif

#if NO_KVP_DECONSTRUCT

    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
    {
        key = pair.Key;
        value = pair.Value;
    }

#endif

    public static ImmutableArray<T> ToImmutable<T>(this ImmutableArray<T>.Builder builder, bool allowDestructive) =>
        allowDestructive && builder.Capacity == builder.Count ? builder.MoveToImmutable() : builder.ToImmutable();
}
