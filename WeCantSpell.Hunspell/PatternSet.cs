﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Count = {Count}")]
public readonly struct PatternSet : IReadOnlyList<PatternEntry>
{
    public static PatternSet Empty { get; } = new([]);

    public static PatternSet Create(IEnumerable<PatternEntry> entries)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(entries);
#else
        ExceptionEx.ThrowIfArgumentNull(entries, nameof(entries));
#endif

        return new(entries.ToArray());
    }

    internal PatternSet(PatternEntry[] patterns)
    {
        _patterns = patterns;
    }

    private readonly PatternEntry[]? _patterns;

    public int Count => _patterns is not null ? _patterns.Length : 0;

    public bool IsEmpty => _patterns is not { Length: > 0 };

    public bool HasItems => _patterns is { Length: > 0 };

    public PatternEntry this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif

            if (_patterns is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _patterns![index];
        }
    }

    public IEnumerator<PatternEntry> GetEnumerator() => ((IEnumerable<PatternEntry>)(_patterns ?? [])).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Forbid compoundings when there are special patterns at word bound.
    /// </summary>
#pragma warning disable IDE0060 // Remove unused parameter
    internal bool Check(ReadOnlySpan<char> word, int pos, WordEntry r1, WordEntry r2, bool affixed)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        if (_patterns is { Length: > 0 })
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
                    StringEx.IsSubset(patternEntry.Pattern2, wordAfterPos)
                )
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool PatternWordCheck(ReadOnlySpan<char> word, int pos, string other) =>
        other.Length <= pos
        && word.Slice(pos - other.Length).StartsWith(other.AsSpan());
}
