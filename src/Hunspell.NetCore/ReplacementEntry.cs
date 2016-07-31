namespace Hunspell
{
    public abstract class ReplacementEntry
    {
        protected ReplacementEntry(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }

        /// <seealso cref="ReplacementValueType.Med"/>
        public abstract string Med { get; }

        /// <seealso cref="ReplacementValueType.Ini"/>
        public abstract string Ini { get; }

        /// <seealso cref="ReplacementValueType.Fin"/>
        public abstract string Fin { get; }

        /// <seealso cref="ReplacementValueType.Isol"/>
        public abstract string Isol { get; }

        public abstract string this[ReplacementValueType type] { get; }
    }
}
