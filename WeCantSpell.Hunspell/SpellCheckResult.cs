namespace WeCantSpell.Hunspell;

public struct SpellCheckResult
{
    public SpellCheckResult(bool correct)
    {
        Root = null;
        Info = SpellCheckResultType.None;
        Correct = correct;
    }

    public SpellCheckResult(string root, SpellCheckResultType info, bool correct)
    {
        Root = root;
        Info = info;
        Correct = correct;
    }

    public string Root { get; }

    public SpellCheckResultType Info { get; }

    public bool Correct { get; }
}
