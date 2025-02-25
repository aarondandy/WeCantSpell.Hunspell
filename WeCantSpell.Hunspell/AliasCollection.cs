using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct AliasCollection<TEntry> : IReadOnlyList<TEntry>
{
    public static AliasCollection<TEntry> Empty { get; } = new([]);

    public static AliasCollection<TEntry> Craete(IEnumerable<TEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal AliasCollection(TEntry[] entries)
    {
        _entries = entries;
    }

    private readonly TEntry[]? _entries;

    public int Count => _entries is not null ? _entries.Length : 0;

    public bool IsEmpty => _entries is not { Length: > 0 };

    public bool HasItems => _entries is { Length: > 0 };

    internal TEntry[] RawArray => _entries ?? [];

    public TEntry this[int index]
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

            if (_entries is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _entries![index];
        }
    }

    public bool TryGetByNumber(int number, out TEntry result)
    {
        if (number > 0 && _entries is not null && number <= _entries.Length)
        {
            result = _entries[number - 1];
            return true;
        }

        result = default!;
        return false;
    }

    public bool TryGetByNumber(ReadOnlySpan<char> numberText, out TEntry result)
    {
        if (IntEx.TryParseInvariant(numberText, out var numberValue))
        {
            return TryGetByNumber(numberValue, out result);
        }

        result = default!;
        return false;
    }

    public IEnumerator<TEntry> GetEnumerator() => ((IEnumerable<TEntry>)RawArray).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
