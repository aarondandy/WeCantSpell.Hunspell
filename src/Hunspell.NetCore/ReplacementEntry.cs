namespace Hunspell
{
    public abstract class ReplacementEntry
    {
        protected ReplacementEntry(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }

        /// <seealso cref="ReplacementEntryType.Med"/>
        public abstract string Med { get; }

        /// <seealso cref="ReplacementEntryType.Ini"/>
        public abstract string Ini { get; }

        /// <seealso cref="ReplacementEntryType.Fin"/>
        public abstract string Fin { get; }

        /// <seealso cref="ReplacementEntryType.Isol"/>
        public abstract string Isol { get; }

        public abstract string this[ReplacementEntryType type] { get; }
    }
}
