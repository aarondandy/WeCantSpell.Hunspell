using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct PatternSet : IReadOnlyList<PatternEntry>
{
    public static PatternSet Empty { get; } = new(Array.Empty<PatternEntry>());

    public static PatternSet Create(IEnumerable<PatternEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        if (entries is null) throw new ArgumentNullException(nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal PatternSet(PatternEntry[] patterns)
    {
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
    internal bool Check(ReadOnlySpan<char> word, int pos, WordEntry r1, WordEntry r2, bool affixed)
    {
        var wordAfterPos = word.Slice(pos);

        foreach (var patternEntry in _patterns)
        {
            if (
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
                &&
                HunspellTextFunctions.IsSubset(patternEntry.Pattern2, wordAfterPos)
            )
            {
                return true;
            }
        }

        return false;
    }

    private static bool PatternWordCheck(ReadOnlySpan<char> word, int pos, string other) =>
        other.Length <= pos
        && word.Slice(pos - other.Length).StartsWith(other.AsSpan());
}
