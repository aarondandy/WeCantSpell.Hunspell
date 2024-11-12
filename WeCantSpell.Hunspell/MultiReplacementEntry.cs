using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Pattern = {Pattern}")]
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
        _ => null,
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
            default:
                throwOutOfRange();
                break;
        }

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void throwOutOfRange() => throw new ArgumentOutOfRangeException(nameof(type));
    }
}
