using System.Diagnostics;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("{Correct}: Info = {Info}, Root = {Root}")]
public readonly struct SpellCheckResult
{
    public static SpellCheckResult DefaultCorrect { get; } = new SpellCheckResult(root: null, SpellCheckResultType.None, correct: true);

    public static SpellCheckResult DefaultWrong { get; } = new SpellCheckResult(root: null, SpellCheckResultType.None, correct: false);

    internal static SpellCheckResult Fail(string? root, SpellCheckResultType info) =>
        new(root, info: info, correct: false);

    internal static SpellCheckResult Success(string? root, SpellCheckResultType info) =>
        new(root, info: info, correct: true);

    internal static SpellCheckResult CompoundSuccess(string? root, SpellCheckResultType info) =>
        new(root, info: info | SpellCheckResultType.Compound, correct: true);

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
