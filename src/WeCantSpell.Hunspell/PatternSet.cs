using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public class PatternSet : ListWrapper<PatternEntry>
    {
        public static readonly PatternSet Empty = TakeList(new List<PatternEntry>(0));

        public static PatternSet Create(IEnumerable<PatternEntry> patterns) =>
            patterns == null ? Empty : TakeList(patterns.ToList());

        internal static PatternSet TakeList(List<PatternEntry> patterns) =>
            patterns == null ? Empty : new PatternSet(patterns);

        private PatternSet(List<PatternEntry> patterns)
            : base(patterns)
        {
        }

        /// <summary>
        /// Forbid compoundings when there are special patterns at word bound.
        /// </summary>
        internal bool Check(ReadOnlySpan<char> word, int pos, WordEntry r1, WordEntry r2, bool affixed)
        {
#if DEBUG
            if (r1 == null) throw new ArgumentNullException(nameof(r1));
            if (r2 == null) throw new ArgumentNullException(nameof(r2));
#endif

            var wordAfterPos = word.Slice(pos);

            foreach (var patternEntry in items)
            {
                if (
                    HunspellTextFunctions.IsSubset(patternEntry.Pattern2, wordAfterPos)
                    &&
                    (
                        patternEntry.Condition.IsZero
                        ||
                        r1.ContainsFlag(patternEntry.Condition)
                    )
                    &&
                    (
                        patternEntry.Condition2.IsZero
                        ||
                        r2.ContainsFlag(patternEntry.Condition2)
                    )
                    &&
                    // zero length pattern => only TESTAFF
                    // zero pattern (0/flag) => unmodified stem (zero affixes allowed)
                    (
                        string.IsNullOrEmpty(patternEntry.Pattern)
                        ||
                        PatternWordCheck(word, pos, patternEntry.Pattern.StartsWith('0') ? r1.Word : patternEntry.Pattern)
                    )
                )
                {
                    return true;
                }
            }

            return false;
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool PatternWordCheck(ReadOnlySpan<char> word, int pos, string other) =>
            other.Length <= pos
            && word.Slice(pos - other.Length).StartsWith(other.AsSpan());
    }
}
