using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class MapEntry : ArrayWrapper<string>
{
    public static readonly MapEntry Empty = TakeArray(Array.Empty<string>());

    public static MapEntry Create(IEnumerable<string> values) => values is null ? Empty : TakeArray(values.ToArray());

    internal static MapEntry TakeArray(string[] values) => values is null ? Empty : new MapEntry(values);

    private MapEntry(string[] values) : base(values)
    {
    }
}
