using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

class CommandParseMap<TCommand> where TCommand : struct
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
            var cmp = key.CompareTo(_map[mid].Key.AsSpan(), StringComparison.OrdinalIgnoreCase);
            if (cmp < 0)
            {
                max = mid - 1;
            }
            else if (cmp > 0)
            {
                min = mid + 1;
            }
            else
            {
                return _map[mid].Value;
            }
        }

        return null;
    }
}
