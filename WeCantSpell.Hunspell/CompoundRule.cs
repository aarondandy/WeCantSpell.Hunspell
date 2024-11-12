using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct CompoundRule : IReadOnlyList<FlagValue>
{
    public static CompoundRule Empty { get; } = new([]);

    public static CompoundRule Create(IEnumerable<FlagValue> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        return new(values.ToArray());
    }

    internal CompoundRule(FlagValue[] items)
    {
        _values = items;
        _nonWildcardRuleFlags = prepareNonWildcardValues(items);

        static FlagSet prepareNonWildcardValues(FlagValue[] values)
        {
            var builder = new StringBuilderSpan(values.Length);
            foreach (var value in values)
            {
                if (value.HasValue && value.IsNotWildcard)
                {
                    builder.Append(value);
                }
            }

            builder.Sort();
            builder.RemoveAdjacentDuplicates();

            return FlagSet.CreateFromPreparedValues(builder.GetStringAndDispose());
        }
    }

    private readonly FlagValue[]? _values;
    private readonly FlagSet _nonWildcardRuleFlags;

    public int Count => _values is not null ? _values.Length : 0;

    public bool IsEmpty => _values is not { Length: > 0 };

    public bool HasItems => _values is { Length: > 0 };

    public FlagValue this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif
            if (_values is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _values![index];
        }
    }

    public IEnumerator<FlagValue> GetEnumerator() => (_values is null ? Enumerable.Empty<FlagValue>() : _values).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal bool IsWildcard(int index) => _values![index].IsWildcard;

    internal bool ContainsRuleFlagForEntry(in FlagSet flags) => _nonWildcardRuleFlags.ContainsAny(flags);
}
