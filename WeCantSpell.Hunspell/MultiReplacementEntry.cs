using System;

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

    public override string? this[ReplacementValueType type] => type switch
    {
        ReplacementValueType.Med => _med,
        ReplacementValueType.Ini => _ini,
        ReplacementValueType.Fin => _fin,
        ReplacementValueType.Isol => _isol,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

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
}
