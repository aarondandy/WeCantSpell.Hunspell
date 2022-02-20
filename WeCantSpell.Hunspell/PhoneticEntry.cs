namespace WeCantSpell.Hunspell;

public sealed class PhoneticEntry
{
    public PhoneticEntry(string rule, string replace)
    {
        Rule = rule ?? string.Empty;
        Replace = replace ?? string.Empty;
    }

    public string Rule { get; }

    public string Replace { get; }
}
