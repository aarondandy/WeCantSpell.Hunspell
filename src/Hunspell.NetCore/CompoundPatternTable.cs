using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public class CompoundPatternTable : ListWrapper<PatternEntry>
    {
        public static readonly CompoundPatternTable Empty = TakeList(new List<PatternEntry>(0));

        private CompoundPatternTable(List<PatternEntry> patterns)
            : base(patterns)
        {
        }

        internal static CompoundPatternTable TakeList(List<PatternEntry> patterns) =>
            patterns == null ? Empty : new CompoundPatternTable(patterns);

        public static CompoundPatternTable Create(IEnumerable<PatternEntry> patterns) =>
            patterns == null ? Empty : TakeList(patterns.ToList());

        public bool TryGetPattern(int number, out PatternEntry result)
        {
            if (number > 0 && number <= items.Count)
            {
                result = items[number - 1];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Forbid compoundings when there are special patterns at word bound.
        /// </summary>
        public bool Check(string word, int pos, DictionaryEntry r1, DictionaryEntry r2, bool affixed)
        {
            var wordAfterPos = word.Substring(pos);

            foreach (var patternEntry in items)
            {
                int len;
                if (
                    StringEx.IsSubset(patternEntry.Pattern2, wordAfterPos)
                    &&
                    (
                        r1 == null
                        ||
                        !patternEntry.Condition.HasValue
                        ||
                        r1.ContainsFlag(patternEntry.Condition)
                    )
                    &&
                    (
                        r2 == null
                        ||
                        !patternEntry.Condition2.HasValue
                        ||
                        r2.ContainsFlag(patternEntry.Condition2)
                    )
                    &&
                    // zero length pattern => only TESTAFF
                    // zero pattern (0/flag) => unmodified stem (zero affixes allowed)
                    (
                        string.IsNullOrEmpty(patternEntry.Pattern)
                        ||
                        (
                            (
                                patternEntry.Pattern.StartsWith('0')
                                && r1.Word.Length <= pos
                                && StringEx.EqualsOffset(word, pos - r1.Word.Length, r1.Word, 0, r1.Word.Length)
                            )
                            ||
                            (
                                !patternEntry.Pattern.StartsWith('0')
                                &&
                                (
                                    (
                                        len = patternEntry.Pattern.Length
                                    ) != 0
                                )
                                &&
                                StringEx.EqualsOffset(word, pos - len, patternEntry.Pattern, 0, len)
                            )
                        )
                    )
                )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
