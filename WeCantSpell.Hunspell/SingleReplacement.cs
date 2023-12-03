namespace WeCantSpell.Hunspell;

public sealed class SingleReplacement : ReplacementEntry
{
    public SingleReplacement(string pattern, string outString, ReplacementValueType type) : base(pattern)
    {
        OutString = outString ?? string.Empty;
        Type = type;
    }

    public string OutString { get; }

    public ReplacementValueType Type { get; }

    public override string? Med => this[ReplacementValueType.Med];

    public override string? Ini => this[ReplacementValueType.Ini];

    public override string? Fin => this[ReplacementValueType.Fin];

    public override string? Isol => this[ReplacementValueType.Isol];

    public override string? this[ReplacementValueType type] => Type == type ? OutString : null;
}
