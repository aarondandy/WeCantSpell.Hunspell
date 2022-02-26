using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

readonly struct CommandParseMap<TCommand> where TCommand : struct
{
    internal CommandParseMap(KeyValuePair<string, TCommand>[] values)
    {
        _map = values;
        Array.Sort(_map, (a, b) => string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase));
    }

    private readonly KeyValuePair<string, TCommand>[] _map;

    public TCommand? TryParse(string key) => TryParse(key.AsSpan());

    public TCommand? TryParse(ReadOnlySpan<char> key)
    {
        var min = 0;
        var max = _map.Length - 1;

        while (min <= max)
        {
            var mid = (min + max) / 2;

            switch (key.CompareTo(_map[mid].Key.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                case < 0:
                    max = mid - 1;
                    break;
                case > 0:
                    min = mid + 1;
                    break;
                default:
                    return _map[mid].Value;
            }
        }

        return null;
    }
}
