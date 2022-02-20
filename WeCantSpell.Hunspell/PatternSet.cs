using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell;

public class PatternSet : ArrayWrapper<PatternEntry>
{
    public static readonly PatternSet Empty = TakeArray(ArrayEx<PatternEntry>.Empty);

    public static PatternSet Create(IEnumerable<PatternEntry> patterns) => patterns is null ? Empty : TakeArray(patterns.ToArray());

    internal static PatternSet TakeArray(PatternEntry[] patterns) => patterns is null ? Empty : new PatternSet(patterns);

    private PatternSet(PatternEntry[] patterns) : base(patterns)
    {
    }

    /// <summary>
    /// Forbid compoundings when there are special patterns at word bound.
    /// </summary>
    internal bool Check(string word, int pos, WordEntry r1, WordEntry r2, bool affixed)
    {
#if DEBUG
        if (r1 is null) throw new ArgumentNullException(nameof(r1));
        if (r2 is null) throw new ArgumentNullException(nameof(r2));
#endif

        var wordAfterPos = word.AsSpan(pos);

        foreach (var patternEntry in Items)
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
    private static bool PatternWordCheck(string word, int pos, string other) =>
        other.Length <= pos
        && word.AsSpan(pos - other.Length).StartsWith(other.AsSpan());
}
