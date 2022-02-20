using System;
using System.Collections.Generic;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class MultiReplacementEntry : ReplacementEntry
{
    public MultiReplacementEntry(string pattern) : base(pattern)
    {
    }

    public MultiReplacementEntry(string pattern, ReplacementValueType type, string value) : base(pattern)
    {
        Set(type, value);
    }

    private string _med;
    private string _ini;
    private string _fin;
    private string _isol;

    public override string Med => _med;

    public override string Ini => _ini;

    public override string Fin => _fin;

    public override string Isol => _isol;

    public override string this[ReplacementValueType type] =>
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
    public static bool AddReplacementEntry(this Dictionary<string, MultiReplacementEntry> list, string pattern1, string pattern2)
    {
        if (string.IsNullOrEmpty(pattern1) || pattern2 is null)
        {
            return false;
        }

        var pattern1Builder = StringBuilderPool.Get(pattern1);
        ReplacementValueType type;
        var trailingUnderscore = pattern1Builder.EndsWith('_');
        if (pattern1Builder.StartsWith('_'))
        {
            if (trailingUnderscore)
            {
                type = ReplacementValueType.Isol;
                pattern1Builder.Remove(pattern1Builder.Length - 1, 1);
            }
            else
            {
                type = ReplacementValueType.Ini;
            }

            pattern1Builder.Remove(0, 1);
        }
        else
        {
            if (trailingUnderscore)
            {
                type = ReplacementValueType.Fin;
                pattern1Builder.Remove(pattern1Builder.Length - 1, 1);
            }
            else
            {
                type = ReplacementValueType.Med;
            }
        }

        pattern1Builder.Replace('_', ' ');

        pattern1 = StringBuilderPool.GetStringAndReturn(pattern1Builder);
        pattern2 = pattern2.Replace('_', ' ');

        // find existing entry
        if (list .TryGetValue(pattern1, out MultiReplacementEntry entry))
        {
            entry.Set(type, pattern2);
        }
        else
        {
            // make a new entry if none exists
            entry = new MultiReplacementEntry(pattern1, type, pattern2);
        }

        list[pattern1] = entry;

        return true;
    }
}
