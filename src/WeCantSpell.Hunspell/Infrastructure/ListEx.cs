using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class ListEx
    {
        internal static void RemoveFromIndexThenInsertAtFront<TValue>(this List<TValue> list, int removeIndex, TValue insertValue)
        {
            if (list.Count != 0)
            {
                while (removeIndex > 0)
                {
                    var sourceIndex = removeIndex - 1;
                    list[removeIndex] = list[sourceIndex];
                    removeIndex = sourceIndex;
                }

                list[0] = insertValue;
            }
        }
    }
}
