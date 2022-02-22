using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class MapTable : ArrayWrapper<MapEntry>
{
    public static readonly MapTable Empty = TakeArray(Array.Empty<MapEntry>());

    public static MapTable Create(IEnumerable<MapEntry> entries) => entries is null ? Empty : TakeArray(entries.ToArray());

    internal static MapTable TakeArray(MapEntry[] entries) => entries is null ? Empty : new MapTable(entries);

    private MapTable(MapEntry[] entries) : base(entries)
    {
    }
}
