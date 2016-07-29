namespace Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
        public SuffixEntry(string rAppend)
        {
            RAppend = rAppend;
        }

        public string RAppend { get; }

        public override string Key => RAppend;
    }
}
