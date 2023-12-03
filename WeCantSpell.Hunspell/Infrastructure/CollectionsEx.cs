using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static class CollectionsEx
{
    internal const int CollectionPreallocationLimit = 16384;

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

    public static void RemoveLast<T>(this List<T> list)
    {
        list.RemoveAt(list.Count - 1);
    }
}
