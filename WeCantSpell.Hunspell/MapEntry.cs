using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct MapEntry : IReadOnlyList<string>
{
    public static MapEntry Empty { get; } = new([]);

    public static MapEntry Create(IEnumerable<string> items)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(items);
#else
        ExceptionEx.ThrowIfArgumentNull(items, nameof(items));
#endif

        return new([.. items]);
    }

    internal MapEntry(string[] items)
    {
        _items = items;
    }

    private readonly string[]? _items;

    public int Count => _items is not null ? _items.Length : 0;

    public bool IsEmpty => _items is not { Length: > 0 };

    public bool HasItems => _items is { Length: > 0 };

    internal string[] RawArray => _items ?? [];

    public string this[int index]
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

            if (_items is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _items![index];
        }
    }

    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)RawArray).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
