﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace WeCantSpell.Hunspell.Infrastructure;

static class CollectionsEx
{

#if NO_DICTIONARY_GETVALUE

    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var result) ? result : default;

    public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
        dictionary.TryGetValue(key, out var result) ? result : defaultValue;

#endif

    public static IEnumerable<TValue> WhereNotNull<TValue>(this IEnumerable<TValue?> values) where TValue : class =>
        values!.Where<TValue>(static value => value is not null);

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

    public static ImmutableArray<T> ToImmutableArray<T>(this ReadOnlySpan<T> span)
    {
        var builder = ImmutableArray.CreateBuilder<T>(span.Length);

        foreach (var value in span)
        {
            builder.Add(value);
        }

        return builder.ToImmutable(true);
    }
}