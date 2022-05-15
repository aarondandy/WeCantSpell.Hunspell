using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell;

public readonly struct MapEntry : IReadOnlyList<string>
{
    public static MapEntry Empty { get; } = new(Array.Empty<string>());

    public static MapEntry Create(IEnumerable<string> items) =>
        new((items ?? throw new ArgumentNullException(nameof(items))).ToArray());

    internal MapEntry(string[] items)
    {
#if DEBUG
        if (items is null) throw new ArgumentNullException(nameof(items));
#endif
        _items = items;
    }

    private readonly string[] _items;

    public int Count => _items.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _items is { Length: > 0 };
    public string this[int index] => _items[index];
    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)_items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    internal string[] GetInternalArray() => _items;
}
