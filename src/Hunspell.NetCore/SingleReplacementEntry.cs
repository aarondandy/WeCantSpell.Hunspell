using System;

namespace Hunspell
{
    public sealed class SingleReplacementEntry : ReplacementEntry
    {
        public SingleReplacementEntry(string pattern, string outString, ReplacementEntryType type)
            : base(pattern)
        {
            OutString = outString;
            Type = type;
        }

        public string OutString { get; }

        public ReplacementEntryType Type { get; }

        public override string Med => this[ReplacementEntryType.Med];

        public override string Ini => this[ReplacementEntryType.Ini];

        public override string Fin => this[ReplacementEntryType.Fin];

        public override string Isol => this[ReplacementEntryType.Isol];

        public override string this[ReplacementEntryType type] => type == Type ? OutString : null;
    }
}
