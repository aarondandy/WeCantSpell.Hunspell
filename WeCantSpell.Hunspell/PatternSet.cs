using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct PatternSet : IReadOnlyList<PatternEntry>
{
    public static PatternSet Empty { get; } = new(Array.Empty<PatternEntry>());

    public static PatternSet Create(IEnumerable<PatternEntry> entries) =>
        new((entries ?? throw new ArgumentNullException(nameof(entries))).ToArray());

    internal PatternSet(PatternEntry[] patterns)
    {
#if DEBUG
        if (patterns is null) throw new ArgumentNullException(nameof(patterns));
#endif

        _patterns = patterns;
    }

    private readonly PatternEntry[] _patterns;

    public int Count => _patterns.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _patterns is { Length: > 0 };
    public PatternEntry this[int index] => _patterns[index];
    public IEnumerator<PatternEntry> GetEnumerator() => ((IEnumerable<PatternEntry>)_patterns).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _patterns.GetEnumerator();

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

        foreach (var patternEntry in _patterns)
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

    private static bool PatternWordCheck(string word, int pos, string other) =>
        other.Length <= pos
        && word.AsSpan(pos - other.Length).StartsWith(other.AsSpan());
}
