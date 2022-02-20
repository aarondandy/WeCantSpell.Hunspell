﻿using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class FlagSet : ArrayWrapper<FlagValue>, IEquatable<FlagSet>
    {
        public static readonly FlagSet Empty = new FlagSet(ArrayEx<FlagValue>.Empty);

        public static readonly ArrayWrapperComparer<FlagValue, FlagSet> DefaultComparer = new ArrayWrapperComparer<FlagValue, FlagSet>();

        public static FlagSet Create(IEnumerable<FlagValue> given) =>
            given == null ? Empty : TakeArray(given.Distinct().ToArray());

        public static FlagSet Union(FlagSet a, FlagSet b) => Create(Enumerable.Concat(a, b));

        public static bool ContainsAny(FlagSet a, FlagSet b)
        {
            if (a != null && !a.IsEmpty && b != null && !b.IsEmpty)
            {
                if (a.Count == 1)
                {
                    return b.Contains(a[0]);
                }
                if (b.Count == 1)
                {
                    return a.Contains(b[0]);
                }

                if (a.Count > b.Count)
                {
                    ReferenceHelpers.Swap(ref a, ref b);
                }

                foreach (var item in a)
                {
                    if (b.Contains(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static FlagSet TakeArray(FlagValue[] values)
        {
            if (values == null || values.Length == 0)
            {
                return Empty;
            }

            Array.Sort(values);
            return new FlagSet(values);
        }

        internal static FlagSet Union(FlagSet set, FlagValue value)
        {
            var valueIndex = Array.BinarySearch(set.items, value);
            if (valueIndex >= 0)
            {
                return set;
            }

            valueIndex = ~valueIndex; // locate the best insertion point

            var newItems = new FlagValue[set.items.Length + 1];
            if (valueIndex >= set.items.Length)
            {
                Array.Copy(set.items, newItems, set.items.Length);
                newItems[set.items.Length] = value;
            }
            else
            {
                Array.Copy(set.items, newItems, valueIndex);
                Array.Copy(set.items, valueIndex, newItems, valueIndex + 1, set.items.Length - valueIndex);
                newItems[valueIndex] = value;
            }

            return new FlagSet(newItems);
        }

        private FlagSet(FlagValue[] values)
            : base(values)
        {
            mask = default;
            for (var i = 0; i < values.Length; i++)
            {
                unchecked
                {
                    mask |= values[i];
                }
            }
        }

        private readonly char mask;

        public bool Contains(FlagValue value)
        {
            if (!value.HasValue || IsEmpty)
            {
                return false;
            }
            if (items.Length == 1)
            {
                return value.Equals(items[0]);
            }

            return (unchecked(value & mask) != default)
                && value >= items[0]
                && value <= items[items.Length - 1]
                && Array.BinarySearch(items, value) >= 0;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAny(FlagSet values) => ContainsAny(this, values);

        public bool ContainsAny(FlagValue a, FlagValue b) =>
            HasItems && (Contains(a) || Contains(b));

        public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
            HasItems && (Contains(a) || Contains(b) || Contains(c));

        public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
            HasItems && (Contains(a) || Contains(b) || Contains(c) || Contains(d));

        public bool Equals(FlagSet other) =>
            !ReferenceEquals(other, null)
            &&
            (
                ReferenceEquals(this, other)
                || ArrayComparer<FlagValue>.Default.Equals(other.items, items)
            );

        public override bool Equals(object obj) =>
             Equals(obj as FlagSet);

        public override int GetHashCode() =>
            ArrayComparer<FlagValue>.Default.GetHashCode(items);
    }
}
