namespace WeCantSpell.Hunspell
{
    public abstract class ReplacementEntry
    {
        protected ReplacementEntry(string pattern)
        {
            Pattern = pattern ?? string.Empty;
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

        internal string ExtractReplacementTextInternal(int remainingCharactersToReplace, bool atStart)
        {
            var type = remainingCharactersToReplace == Pattern.Length
                ? ReplacementValueType.Fin
                : ReplacementValueType.Med;

            if (atStart)
            {
                type |= ReplacementValueType.Ini;
            }

            while (type != ReplacementValueType.Med && string.IsNullOrEmpty(this[type]))
            {
                type = (type == ReplacementValueType.Fin && !atStart) ? ReplacementValueType.Med : type - 1;
            }

            return this[type];
        }
    }
}
