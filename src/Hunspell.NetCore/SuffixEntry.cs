namespace Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
        public string RAppend { get; set; }

        public override string Key
        {
            get
            {
                return RAppend;
            }
        }
    }
}
