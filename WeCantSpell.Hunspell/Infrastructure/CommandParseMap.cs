using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

readonly struct CommandParseMap<TCommand> where TCommand : struct
{
    internal CommandParseMap(KeyValuePair<string, TCommand>[] values)
    {
        _map = values;
        Array.Sort(_map, static (a, b) => string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase));
    }

    private readonly KeyValuePair<string, TCommand>[] _map;

    public TCommand? TryParse(string key) => TryParse(key.AsSpan());

    public TCommand? TryParse(ReadOnlySpan<char> key) =>
        (_map.BinarySearch(key, StringComparison.OrdinalIgnoreCase) is int index and >= 0)
            ? _map[index].Value
            : null;
}
