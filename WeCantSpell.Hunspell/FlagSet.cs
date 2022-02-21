using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public sealed class FlagSet : ArrayWrapper<FlagValue>, IEquatable<FlagSet>
{
    public static readonly FlagSet Empty = new FlagSet(Array.Empty<FlagValue>());

    public static readonly ArrayWrapperComparer<FlagValue, FlagSet> DefaultComparer = new ArrayWrapperComparer<FlagValue, FlagSet>();

    public static FlagSet Create(IEnumerable<FlagValue> given) => given is null ? Empty : TakeArray(given.Distinct().Where(static v => v.HasValue).ToArray());

    public static FlagSet Union(FlagSet a, FlagSet b) => Create(Enumerable.Concat(a, b));

    internal static FlagSet TakeArray(FlagValue[] values)
    {
        if (values is null || values.Length == 0)
        {
            return Empty;
        }

        Array.Sort(values);
        return new FlagSet(values);
    }

    internal static FlagSet Union(FlagSet set, FlagValue value)
    {
        var valueIndex = Array.BinarySearch(set.Items, value);
        if (valueIndex >= 0)
        {
            return set;
        }

        valueIndex = ~valueIndex; // locate the best insertion point

        var newItems = new FlagValue[set.Items.Length + 1];
        if (valueIndex >= set.Items.Length)
        {
            Array.Copy(set.Items, newItems, set.Items.Length);
            newItems[set.Items.Length] = value;
        }
        else
        {
            Array.Copy(set.Items, newItems, valueIndex);
            Array.Copy(set.Items, valueIndex, newItems, valueIndex + 1, set.Items.Length - valueIndex);
            newItems[valueIndex] = value;
        }

        return new FlagSet(newItems);
    }

    private FlagSet(FlagValue[] values) : base(values)
    {
        _mask = default;
        for (var i = 0; i < values.Length; i++)
        {
            unchecked
            {
                _mask |= values[i];
            }
        }
    }

    private readonly char _mask;

    public bool Contains(FlagValue value)
    {
        if (value.HasValue && HasItems)
        {
            if (Items.Length == 1)
            {
                return value.Equals(Items[0]);
            }

            if (unchecked(value & _mask) != default)
            {
                return search();
                bool search()
                {
                    if (Items.Length <= 8)
                    {
                        return Array.IndexOf(Items, value) >= 0;
                    }

                    return value >= Items[0]
                        && value <= Items[Items.Length - 1]
                        && Array.BinarySearch(Items, value) >= 0;
                }
            }
        }

        return false;
    }

#if !NO_INLINE
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
    public bool ContainsAny(FlagSet values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        if (IsEmpty || values.IsEmpty)
        {
            return false;
        }

        if (Count == 1)
        {
            return values.Contains(Items[0]);
        }

        if (values.Count == 1)
        {
            return Contains(values[0]);
        }

        if ((_mask & values._mask) == default)
        {
            return false;
        }

        return Count <= values.Count
            ? checkIterative(this, values)
            : checkIterative(values, this);

        static bool checkIterative(FlagSet a, FlagSet b)
        {
            foreach (var value in a)
            {
                if (b.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool ContainsAny(FlagValue a, FlagValue b) =>
        HasItems && (Contains(a) || Contains(b));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c));

    public bool ContainsAny(FlagValue a, FlagValue b, FlagValue c, FlagValue d) =>
        HasItems && (Contains(a) || Contains(b) || Contains(c) || Contains(d));

    public bool Equals(FlagSet other) =>
        other is not null
        &&
        (
            ReferenceEquals(this, other)
            || ArrayComparer<FlagValue>.Default.Equals(other.Items, Items)
        );

    public override bool Equals(object obj) => Equals(obj as FlagSet);

    public override int GetHashCode() => ArrayComparer<FlagValue>.Default.GetHashCode(Items);
}
