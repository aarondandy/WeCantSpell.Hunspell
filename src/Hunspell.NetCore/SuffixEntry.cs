using Hunspell.Utilities;

namespace Hunspell
{
    public sealed class SuffixEntry : AffixEntry
    {
        public SuffixEntry()
        {
            RAppend = Append.Reverse();
        }

        public string RAppend { get; }

        public override string Key => RAppend;
    }
}
