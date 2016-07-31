namespace Hunspell
{
    public sealed class PhoneticEntry
    {
        public PhoneticEntry(string rule, string replace)
        {
            Rule = rule;
            Replace = replace;
        }

        public string Rule { get; }

        public string Replace { get; }
    }
}
