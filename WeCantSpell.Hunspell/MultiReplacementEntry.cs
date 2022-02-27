using System;
using System.Collections.Generic;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class MultiReplacementEntry : ReplacementEntry
{
    public MultiReplacementEntry(string pattern) : base(pattern)
    {
    }

    private string? _med;
    private string? _ini;
    private string? _fin;
    private string? _isol;

    public override string? Med => _med;

    public override string? Ini => _ini;

    public override string? Fin => _fin;

    public override string? Isol => _isol;

    public override string? this[ReplacementValueType type] =>
        type switch
        {
            ReplacementValueType.Med => _med,
            ReplacementValueType.Ini => _ini,
            ReplacementValueType.Fin => _fin,
            ReplacementValueType.Isol => _isol,
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };

    public MultiReplacementEntry With(ReplacementValueType type, string value)
    {
        var result = Clone();
        result.Set(type, value);
        return result;
    }

    internal void Set(ReplacementValueType type, string value)
    {
        switch (type)
        {
            case ReplacementValueType.Med:
                _med = value;
                break;
            case ReplacementValueType.Ini:
                _ini = value;
                break;
            case ReplacementValueType.Fin:
                _fin = value;
                break;
            case ReplacementValueType.Isol:
                _isol = value;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    internal MultiReplacementEntry Clone() =>
        new MultiReplacementEntry(Pattern)
        {
            _med = _med,
            _ini = _ini,
            _fin = _fin,
            _isol = _isol
        };
}

static class MultiReplacementEntryExtensions
{
    internal static bool AddReplacementEntry(this Dictionary<string, MultiReplacementEntry> list, string pattern1, string pattern2)
    {
#if DEBUG
        if (pattern1 is null) throw new ArgumentNullException(nameof(pattern1));
        if (pattern2 is null) throw new ArgumentNullException(nameof(pattern2));
#endif

        var pattern1Span = pattern1.AsSpan();
        var type = ReplacementValueType.Med;

        if (pattern1Span.StartsWith('_'))
        {
            type |= ReplacementValueType.Ini;
            pattern1Span = pattern1Span.Slice(1);
        }

        if (pattern1Span.EndsWith('_'))
        {
            type |= ReplacementValueType.Fin;
            pattern1Span = pattern1Span.Slice(0, pattern1Span.Length - 1);
        }

        if (type == ReplacementValueType.Med && pattern1Span.Length == pattern1.Length)
        {
            pattern1 = pattern1.Replace('_', ' ');
        }
        else if (pattern1Span.Contains('_'))
        {
            var pattern1Builder = StringBuilderPool.Get(pattern1Span);
            pattern1Builder.Replace('_', ' ');
            pattern1 = StringBuilderPool.GetStringAndReturn(pattern1Builder);
        }
        else
        {
            pattern1 = pattern1Span.ToString().Replace('_', ' ');
        }

        pattern2 = pattern2.Replace('_', ' ');

        // find existing entry
        if (!list.TryGetValue(pattern1, out var entry))
        {
            // make a new entry if none exists
            entry = new MultiReplacementEntry(pattern1);
            list[pattern1] = entry;
        }

        entry.Set(type, pattern2);

        return true;
    }
}
