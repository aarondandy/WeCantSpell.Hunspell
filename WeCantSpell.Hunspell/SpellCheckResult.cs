using System.Diagnostics;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("{Correct}: Info = {Info}, Root = {Root}")]
public readonly struct SpellCheckResult
{
    public SpellCheckResult(bool correct)
    {
        Root = string.Empty;
        Info = SpellCheckResultType.None;
        Correct = correct;
    }

    public SpellCheckResult(string? root, SpellCheckResultType info, bool correct)
    {
        Root = root ?? string.Empty;
        Info = info;
        Correct = correct;
    }

    public string Root { get; }

    public SpellCheckResultType Info { get; }

    public bool Correct { get; }
}
