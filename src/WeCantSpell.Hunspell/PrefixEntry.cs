namespace WeCantSpell.Hunspell
{
    public sealed class PrefixEntry : AffixEntry
    {
        public sealed override string Key => Append;
    }
}
