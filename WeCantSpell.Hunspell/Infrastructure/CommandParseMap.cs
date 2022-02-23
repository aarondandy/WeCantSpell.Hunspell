using System;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell.Infrastructure;

class CommandParseMap<TCommand> where TCommand : struct
{
    public CommandParseMap()
    {
        _map = new();
    }

    private readonly Dictionary<int, List<KeyValuePair<string, TCommand>>> _map;

    public void Add(string key, TCommand command)
    {
        var hash = Hash(key.AsSpan());
        if (!_map.TryGetValue(hash, out var bucket))
        {
            bucket = new();
            _map.Add(hash, bucket);
        }

        if (bucket.Any(p => key.Equals(p.Key, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException();
        }

        bucket.Add(new(key, command));
    }

    public TCommand? TryParse(string value) => TryParse(value.AsSpan());

    public TCommand? TryParse(ReadOnlySpan<char> value)
    {
        var hash = Hash(value);

        if (_map.TryGetValue(hash, out var bucket))
        {
            foreach (var pair in bucket)
            {
                if (value.Equals(pair.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Value;
                }
            }
        }

        return null;
    }

    private int Hash(ReadOnlySpan<char> value) => value.Length switch
    {
        >3 => HashCode.Combine(value.Length, char.ToUpperInvariant(value[0]), char.ToUpperInvariant(value[value.Length - 2]), char.ToUpperInvariant(value[value.Length - 1])),
        3 => HashCode.Combine(char.ToUpperInvariant(value[0]), char.ToUpperInvariant(value[1]), char.ToUpperInvariant(value[2])),
        2 => HashCode.Combine(char.ToUpperInvariant(value[0]), char.ToUpperInvariant(value[1])),
        _ => value.Length,
    };
}
