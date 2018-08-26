using System;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class PatternEntry
    {
        public PatternEntry(string pattern, string pattern2, string pattern3, FlagValue condition, FlagValue condition2)
        {
            Pattern = pattern ?? string.Empty;
            Pattern2 = pattern2 ?? string.Empty;
            Pattern3 = pattern3 ?? string.Empty;
            Condition = condition;
            Condition2 = condition2;
        }

        public string Pattern { get; }

        public string Pattern2 { get; }

        public string Pattern3 { get; }

        public FlagValue Condition { get; }

        public FlagValue Condition2 { get; }

        internal bool Pattern3DoesNotMatch(ReadOnlySpan<char> word, int offset) =>
            Pattern3.Length == 0
            ||
            !word.Slice(offset).Limit(Pattern3.Length).Equals(Pattern3.AsSpan(), StringComparison.Ordinal);
    }
}
