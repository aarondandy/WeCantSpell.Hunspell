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
        Items = items;
    }

    internal string[] Items { get; }

    public int Count => Items.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => Items is { Length: > 0 };
    public string this[int index] => Items[index];
    public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)Items).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
}
